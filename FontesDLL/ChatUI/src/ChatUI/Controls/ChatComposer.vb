Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

''' <summary>
''' Caixa de digitação: botão de anexo, campo que cresce até MaxLines e botão enviar desenhados com GDI+.
''' Enter envia, Shift+Enter quebra linha, Ctrl+V com imagem dispara ImagePasted.
''' </summary>
<ToolboxItem(True), DefaultEvent("SendRequested")>
Public Class ChatComposer
    Inherits Control

    ''' <summary>TextBox multilinha com texto de dica (placeholder) desenhado por cima.</summary>
    Friend NotInheritable Class CueTextBox
        Inherits TextBox

        Private Const WM_PAINT As Integer = &HF

        Public Property Cue As String
        Public Property CueColor As Color = Color.Gray

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)
            If m.Msg = WM_PAINT AndAlso TextLength = 0 AndAlso Not String.IsNullOrEmpty(Cue) Then
                Using g = CreateGraphics()
                    TextRenderer.DrawText(g, Cue, Font, New Rectangle(1, 0, ClientSize.Width - 1, ClientSize.Height), CueColor, BackColor,
                                          TextFormatFlags.Left Or TextFormatFlags.Top Or TextFormatFlags.NoPadding Or TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis Or TextFormatFlags.NoPrefix)
                End Using
            End If
        End Sub

        Protected Overrides Sub OnTextChanged(e As EventArgs)
            MyBase.OnTextChanged(e)
            If TextLength = 0 Then Invalidate()
        End Sub
    End Class

    Private ReadOnly _tb As CueTextBox
    Private ReadOnly _tip As ToolTip
    Private ReadOnly _typingTimer As Timer
    Private _theme As ChatTheme
    Private _scale As Single = 1.0F
    Private _showAttach As Boolean = True
    Private _maxLines As Integer = 5
    Private _sendOnEnter As Boolean = True
    Private _placeholder As String
    Private _hover As Integer       ' 0 nada, 1 anexo, 2 enviar, 3 atualizar
    Private _showRefresh As Boolean
    Private _attachRect As Rectangle
    Private _sendRect As Rectangle
    Private _refreshRect As Rectangle
    Private _pillRect As Rectangle
    Private _inLayout As Boolean
    Private _typing As Boolean
    Private _suppressTyping As Boolean

    ''' <summary>Usuário pediu para enviar (Enter ou botão). e.Cancel = True mantém o texto na caixa.</summary>
    Public Event SendRequested As EventHandler(Of ChatSendEventArgs)
    ''' <summary>Clique no botão de anexo.</summary>
    Public Event AttachRequested As EventHandler
    ''' <summary>Clique no botão de atualizar (busca manual de mensagens novas, para quem não usa conexão em tempo real).</summary>
    Public Event RefreshRequested As EventHandler
    ''' <summary>Ctrl+V com uma imagem na área de transferência.</summary>
    Public Event ImagePasted As EventHandler(Of ChatImageEventArgs)
    ''' <summary>Começou/parou de digitar (para enviar "digitando..." ao outro lado). Para após 3 s sem teclar.</summary>
    Public Event TypingStateChanged As EventHandler(Of ChatTypingEventArgs)
    Friend Event NavigationKey As EventHandler(Of KeyEventArgs)

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.Selectable, False)
        _theme = New ChatTheme()
        AddHandler _theme.Changed, AddressOf OnThemeChanged

        _tb = New CueTextBox() With {
            .Multiline = True,
            .AcceptsReturn = True,
            .AcceptsTab = False,
            .WordWrap = True,
            .BorderStyle = BorderStyle.None,
            .ScrollBars = ScrollBars.None}
        AddHandler _tb.KeyDown, AddressOf OnTextKeyDown
        AddHandler _tb.TextChanged, AddressOf OnTextChangedInternal
        AddHandler _tb.GotFocus, Sub() Invalidate()
        AddHandler _tb.LostFocus, Sub() Invalidate()
        Controls.Add(_tb)

        _tip = New ToolTip()
        _typingTimer = New Timer() With {.Interval = 3000}
        AddHandler _typingTimer.Tick, AddressOf OnTypingTimeout

        ApplyTheme()
        Height = 58
    End Sub

#Region "Propriedades"

    <Category("Aparência"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property Theme As ChatTheme
        Get
            Return _theme
        End Get
        Set(value As ChatTheme)
            If value Is Nothing OrElse Object.ReferenceEquals(value, _theme) Then Return
            RemoveHandler _theme.Changed, AddressOf OnThemeChanged
            _theme = value
            AddHandler _theme.Changed, AddressOf OnThemeChanged
            ApplyTheme()
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(True)>
    Public Property ShowAttachButton As Boolean
        Get
            Return _showAttach
        End Get
        Set(value As Boolean)
            If _showAttach <> value Then _showAttach = value : PerformLayout() : Invalidate()
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(False), Description("Mostra o botão de atualizar ao lado do botão enviar (evento RefreshRequested).")>
    Public Property ShowRefreshButton As Boolean
        Get
            Return _showRefresh
        End Get
        Set(value As Boolean)
            If _showRefresh <> value Then _showRefresh = value : PerformLayout() : Invalidate()
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(5), Description("Linhas visíveis antes de aparecer a barra de rolagem.")>
    Public Property MaxLines As Integer
        Get
            Return _maxLines
        End Get
        Set(value As Integer)
            _maxLines = Math.Max(1, Math.Min(20, value))
            PerformLayout()
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(True), Description("Enter envia e Shift+Enter quebra linha. False: Ctrl+Enter envia.")>
    Public Property SendOnEnter As Boolean
        Get
            Return _sendOnEnter
        End Get
        Set(value As Boolean)
            _sendOnEnter = value
        End Set
    End Property

    <Category("Aparência"), DefaultValue(CStr(Nothing)), Description("Texto de dica. Vazio = ChatStrings.Placeholder.")>
    Public Property PlaceholderText As String
        Get
            Return _placeholder
        End Get
        Set(value As String)
            _placeholder = value
            _tb.Cue = If(String.IsNullOrEmpty(value), ChatStrings.Placeholder, value)
            _tb.Invalidate()
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(32767)>
    Public Property MaxLength As Integer
        Get
            Return _tb.MaxLength
        End Get
        Set(value As Integer)
            _tb.MaxLength = value
        End Set
    End Property

    <Browsable(True), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property Text As String
        Get
            Return _tb.Text
        End Get
        Set(value As String)
            _suppressTyping = True
            Try
                _tb.Text = If(value, String.Empty)
                _tb.SelectionStart = _tb.TextLength
            Finally
                _suppressTyping = False
            End Try
            UpdateLayoutNow()
        End Set
    End Property

    ''' <summary>Acesso direto ao TextBox interno (para casos especiais).</summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public ReadOnly Property InputBox As TextBox
        Get
            Return _tb
        End Get
    End Property

#End Region

#Region "Métodos"

    ''' <summary>Dispara SendRequested com o texto atual (se não estiver vazio).</summary>
    Public Sub Send()
        Dim txt = _tb.Text.Trim()
        If txt.Length = 0 Then Return
        Dim e As New ChatSendEventArgs(txt)
        RaiseEvent SendRequested(Me, e)
        If Not e.Cancel Then
            Text = String.Empty
            SetTyping(False)
        End If
        FocusInput()
    End Sub

    Public Sub FocusInput()
        If _tb.CanFocus Then _tb.Focus()
    End Sub

    Public Sub InsertText(s As String)
        _tb.SelectedText = s
    End Sub

#End Region

#Region "Layout e pintura"

    Private Function S(v As Integer) As Integer
        Return CInt(Math.Round(v * _scale))
    End Function

    Private Sub ApplyTheme()
        _tb.Font = _theme.MessageFont
        _tb.BackColor = _theme.InputBackColor
        _tb.ForeColor = _theme.InputTextColor
        _tb.Cue = If(String.IsNullOrEmpty(_placeholder), ChatStrings.Placeholder, _placeholder)
        _tb.CueColor = _theme.PlaceholderColor
        _tb.Invalidate()
        UpdateLayoutNow()
        Invalidate()
    End Sub

    Private Sub OnThemeChanged(sender As Object, e As EventArgs)
        ApplyTheme()
    End Sub

    Private Sub UpdateLayoutNow()
        If IsHandleCreated Then PerformLayout()
    End Sub

    Private Function CountLines() As Integer
        If _tb.TextLength = 0 Then Return 1
        Try
            If _tb.IsHandleCreated Then Return _tb.GetLineFromCharIndex(_tb.TextLength) + 1
        Catch
        End Try
        Dim lines = 1
        Dim lf = Convert.ToChar(10)
        For Each ch In _tb.Text
            If ch = lf Then lines += 1
        Next
        Return lines
    End Function

    Protected Overrides Sub OnLayout(levent As LayoutEventArgs)
        MyBase.OnLayout(levent)
        If _inLayout Then Return
        _inLayout = True
        Try
            Dim pad = S(10)
            Dim btn = S(36)
            Dim lineH = _tb.Font.Height
            Dim rawLines = CountLines()
            Dim lines = Math.Max(1, Math.Min(_maxLines, rawLines))
            Dim tbH = lines * lineH
            Dim pillH = Math.Max(btn, tbH + S(16))
            Dim desired = pillH + pad * 2 + 1
            If Height <> desired Then Height = desired

            Dim pillLeft = pad
            If _showAttach Then
                _attachRect = New Rectangle(pad, 1 + pad + pillH - btn, btn, btn)
                pillLeft = _attachRect.Right + S(6)
            Else
                _attachRect = Rectangle.Empty
            End If
            _sendRect = New Rectangle(ClientSize.Width - pad - btn, 1 + pad + pillH - btn, btn, btn)
            Dim pillRight = _sendRect.Left
            If _showRefresh Then
                _refreshRect = New Rectangle(_sendRect.Left - S(6) - btn, _sendRect.Y, btn, btn)
                pillRight = _refreshRect.Left
            Else
                _refreshRect = Rectangle.Empty
            End If
            _pillRect = New Rectangle(pillLeft, 1 + pad, Math.Max(S(40), pillRight - S(8) - pillLeft), pillH)
            Dim sb = If(rawLines > _maxLines, ScrollBars.Vertical, ScrollBars.None)
            If _tb.ScrollBars <> sb Then _tb.ScrollBars = sb
            _tb.Bounds = New Rectangle(_pillRect.X + S(14), _pillRect.Y + (pillH - tbH) \ 2, _pillRect.Width - S(24), tbH)
        Finally
            _inLayout = False
        End Try
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g = e.Graphics
        Dim t = _theme
        g.Clear(t.ComposerBackColor)
        Using p As New Pen(t.BorderColor)
            g.DrawLine(p, 0, 0, Width, 0)
        End Using
        g.SmoothingMode = SmoothingMode.AntiAlias

        ' campo
        Dim radius = If(_pillRect.Height <= S(40), _pillRect.Height / 2.0F, CSng(S(18)))
        FillRound(g, t.InputBackColor, _pillRect, radius)
        If _tb.Focused Then
            Using path = RoundRect(New RectangleF(_pillRect.X + 0.5F, _pillRect.Y + 0.5F, _pillRect.Width - 1, _pillRect.Height - 1), radius), pen As New Pen(Color.FromArgb(110, t.AccentColor), 1.0F)
                g.DrawPath(pen, path)
            End Using
        End If

        ' anexar
        If _showAttach Then
            If _hover = 1 Then
                Using b As New SolidBrush(t.InputBackColor)
                    g.FillEllipse(b, _attachRect)
                End Using
            End If
            Dim c = New Point(_attachRect.X + _attachRect.Width \ 2, _attachRect.Y + _attachRect.Height \ 2)
            DrawPlus(g, c, S(8), If(Enabled, t.MetaColor, Blend(t.MetaColor, t.ComposerBackColor, 0.5)), 2.0F * _scale)
        End If

        ' atualizar
        If _showRefresh Then
            If _hover = 3 Then
                Using b As New SolidBrush(t.InputBackColor)
                    g.FillEllipse(b, _refreshRect)
                End Using
            End If
            DrawRefreshGlyph(g, _refreshRect, If(Enabled, t.MetaColor, Blend(t.MetaColor, t.ComposerBackColor, 0.5)))
        End If

        ' enviar
        Dim hasText = _tb.TextLength > 0 AndAlso _tb.Text.Trim().Length > 0
        Dim sendBack = If(hasText AndAlso Enabled, t.AccentColor, Blend(t.InputBackColor, t.MetaColor, 0.25))
        If hasText AndAlso _hover = 2 Then sendBack = Blend(sendBack, Color.Black, 0.12)
        Using b As New SolidBrush(sendBack)
            g.FillEllipse(b, _sendRect)
        End Using
        Dim gl = S(14)
        Dim gr As New RectangleF(_sendRect.X + (_sendRect.Width - gl) / 2.0F + S(1), _sendRect.Y + (_sendRect.Height - gl) / 2.0F, gl, gl)
        DrawSendGlyph(g, gr, If(hasText, Color.White, t.ComposerBackColor))
    End Sub

    ''' <summary>Seta circular (atualizar).</summary>
    Private Sub DrawRefreshGlyph(g As Graphics, r As Rectangle, color As Color)
        Dim d = S(14)
        Dim arc As New RectangleF(r.X + (r.Width - d) / 2.0F, r.Y + (r.Height - d) / 2.0F, d, d)
        Using pen As New Pen(color, 2.0F * _scale)
            pen.StartCap = LineCap.Round
            g.DrawArc(pen, arc, -60.0F, 300.0F)
        End Using
        ' ponta da seta no fim do arco (ângulo -60°), apontando no sentido horário
        Dim cx = arc.X + arc.Width / 2.0F, cy = arc.Y + arc.Height / 2.0F, rad = arc.Width / 2.0F
        Dim ang = -60.0 * Math.PI / 180.0
        Dim tip As New PointF(CSng(cx + rad * Math.Cos(ang)), CSng(cy + rad * Math.Sin(ang)))
        Dim a = S(5)
        Using b As New SolidBrush(color)
            g.FillPolygon(b, New PointF() {New PointF(tip.X + a * 0.2F, tip.Y - a),
                                           New PointF(tip.X + a * 0.9F, tip.Y + a * 0.35F),
                                           New PointF(tip.X - a * 0.6F, tip.Y + a * 0.25F)})
        End Using
    End Sub

#End Region

#Region "Entrada"

    Private Sub OnTextKeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Dim sendNow = If(_sendOnEnter, Not e.Shift AndAlso Not e.Control, e.Control)
            If sendNow Then
                e.SuppressKeyPress = True
                e.Handled = True
                Send()
            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.A Then
            _tb.SelectAll()
            e.SuppressKeyPress = True
        ElseIf (e.Control AndAlso e.KeyCode = Keys.V) OrElse (e.Shift AndAlso e.KeyCode = Keys.Insert) Then
            Try
                If Clipboard.ContainsImage() AndAlso Not Clipboard.ContainsText() Then
                    Dim img = Clipboard.GetImage()
                    If img IsNot Nothing Then
                        e.SuppressKeyPress = True
                        RaiseEvent ImagePasted(Me, New ChatImageEventArgs(Nothing, img))
                    End If
                End If
            Catch
                ' área de transferência ocupada por outro processo: ignora
            End Try
        ElseIf e.KeyCode = Keys.PageUp OrElse e.KeyCode = Keys.PageDown Then
            RaiseEvent NavigationKey(Me, e)
            e.Handled = True
        End If
    End Sub

    Private Sub OnTextChangedInternal(sender As Object, e As EventArgs)
        PerformLayout()
        Invalidate()
        If _suppressTyping Then Return
        If _tb.TextLength > 0 Then
            SetTyping(True)
            _typingTimer.Stop()
            _typingTimer.Start()
        Else
            SetTyping(False)
        End If
    End Sub

    Private Sub OnTypingTimeout(sender As Object, e As EventArgs)
        SetTyping(False)
    End Sub

    Private Sub SetTyping(value As Boolean)
        If Not value Then _typingTimer.Stop()
        If _typing = value Then Return
        _typing = value
        RaiseEvent TypingStateChanged(Me, New ChatTypingEventArgs(Nothing, value))
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        Dim h = 0
        If _showAttach AndAlso _attachRect.Contains(e.Location) Then
            h = 1
        ElseIf _sendRect.Contains(e.Location) Then
            h = 2
        ElseIf _showRefresh AndAlso _refreshRect.Contains(e.Location) Then
            h = 3
        End If
        If h <> _hover Then
            _hover = h
            Cursor = If(h <> 0, Cursors.Hand, Cursors.Default)
            _tip.SetToolTip(Me, If(h = 1, ChatStrings.AttachButtonTip, If(h = 2, ChatStrings.SendButtonTip, If(h = 3, ChatStrings.RefreshButtonTip, Nothing))))
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        If _hover <> 0 Then _hover = 0 : Invalidate()
    End Sub

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        MyBase.OnMouseClick(e)
        If e.Button <> MouseButtons.Left OrElse Not Enabled Then Return
        If _showAttach AndAlso _attachRect.Contains(e.Location) Then
            RaiseEvent AttachRequested(Me, EventArgs.Empty)
        ElseIf _sendRect.Contains(e.Location) Then
            Send()
        ElseIf _showRefresh AndAlso _refreshRect.Contains(e.Location) Then
            RaiseEvent RefreshRequested(Me, EventArgs.Empty)
        ElseIf _pillRect.Contains(e.Location) Then
            FocusInput()
        End If
    End Sub

    Protected Overrides Sub OnEnabledChanged(e As EventArgs)
        MyBase.OnEnabledChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        _scale = GetDpiScale(Me)
        PerformLayout()
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            RemoveHandler _theme.Changed, AddressOf OnThemeChanged
            _typingTimer.Dispose()
            _tip.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

End Class
