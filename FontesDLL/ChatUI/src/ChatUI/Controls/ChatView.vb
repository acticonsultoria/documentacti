Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Enum SenderNameMode
    ''' <summary>Mostra o nome do remetente apenas em conversas de grupo.</summary>
    Auto
    Always
    Never
End Enum

''' <summary>
''' Área de mensagens 100% desenhada (GDI/GDI+): balões, avatares, status, imagens, anexos,
''' separadores de data e indicador "digitando...". Virtualizada: só desenha o que está visível.
''' </summary>
<ToolboxItem(True), DefaultEvent("MessageClick"), Description("Lista de mensagens desenhada com GDI+.")>
Public Class ChatView
    Inherits Control

#Region "Tipos internos"

    Private Enum RowKind
        Day
        Message
        Typing
    End Enum

    ''' <summary>Uma linha já medida. Retângulos em coordenadas de conteúdo (Y absoluto, sem rolagem).</summary>
    Private NotInheritable Class Row
        Public Kind As RowKind
        Public Message As ChatMessage
        Public Day As Date
        Public Y As Integer
        Public Height As Integer
        Public Bubble As Rectangle
        Public NameRect As Rectangle
        Public TextRect As Rectangle
        Public ImageRect As Rectangle
        Public AttachRect As Rectangle
        Public MetaRect As Rectangle
        Public StatusRect As Rectangle
        Public AvatarRect As Rectangle
        Public ShowAvatar As Boolean
        Public GroupTop As Boolean
        Public GroupBottom As Boolean
        Public MetaOverlay As Boolean
        Public MetaText As String
    End Class

#End Region

#Region "Campos"

    Private Const TextFlags As TextFormatFlags = TextFormatFlags.WordBreak Or TextFormatFlags.TextBoxControl Or TextFormatFlags.NoPrefix Or TextFormatFlags.NoPadding
    Private Const SingleFlags As TextFormatFlags = TextFormatFlags.SingleLine Or TextFormatFlags.NoPrefix Or TextFormatFlags.NoPadding
    Private Const EllipsisFlags As TextFormatFlags = SingleFlags Or TextFormatFlags.EndEllipsis

    Private _conversation As ChatConversation
    Private _theme As ChatTheme
    Private _nameFont As Font
    Private ReadOnly _rows As New List(Of Row)()
    Private _contentHeight As Integer
    Private _scroll As Integer
    Private _scrollTarget As Integer
    Private _stickToBottom As Boolean = True
    Private _layoutDirty As Boolean = True
    Private _layoutWidth As Integer = -1
    Private _measureVersion As Integer = 1
    Private _singleLineHeight As Integer
    Private _scale As Single = 1.0F
    Private _pendingNew As Integer
    Private _typingPhase As Integer
    Private ReadOnly _typingTimer As Timer
    Private ReadOnly _scrollTimer As Timer
    Private _showAvatars As Boolean = True
    Private _senderNames As SenderNameMode = SenderNameMode.Auto
    Private _defaultMenu As Boolean = True
    Private _topRaised As Boolean

    ' interação
    Private _sbHover As Boolean
    Private _sbDrag As Boolean
    Private _sbDragOffset As Integer
    Private _downOnChrome As Boolean
    Private _jumpHover As Boolean
    Private _hoverRow As Row
    Private _hoverArea As ChatHitArea

#End Region

#Region "Eventos"

    ''' <summary>Clique em qualquer parte de uma mensagem (veja e.Area).</summary>
    Public Event MessageClick As EventHandler(Of ChatMessageEventArgs)
    Public Event MessageDoubleClick As EventHandler(Of ChatMessageEventArgs)
    ''' <summary>Clique na imagem. Padrão (se e.Handled = False): abre o visualizador embutido.</summary>
    Public Event ImageClick As EventHandler(Of ChatMessageEventArgs)
    ''' <summary>Clique no anexo. Padrão (se e.Handled = False): abre o arquivo com o programa associado.</summary>
    Public Event AttachmentClick As EventHandler(Of ChatMessageEventArgs)
    ''' <summary>Usuário pediu para reenviar uma mensagem com Status = Failed.</summary>
    Public Event RetryRequested As EventHandler(Of ChatMessageEventArgs)
    ''' <summary>Antes de abrir o menu de contexto de uma mensagem (adicione itens ou cancele).</summary>
    Public Event MessageContextMenuOpening As EventHandler(Of ChatContextMenuEventArgs)
    ''' <summary>A rolagem chegou ao topo: bom momento para carregar o histórico (Messages.InsertRange(0, ...)).</summary>
    Public Event LoadOlderRequested As EventHandler(Of ChatConversationEventArgs)

#End Region

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer Or
                 ControlStyles.ResizeRedraw Or ControlStyles.Selectable Or ControlStyles.StandardClick Or ControlStyles.StandardDoubleClick, True)
        _theme = New ChatTheme()
        AddHandler _theme.Changed, AddressOf OnThemeChanged
        RebuildFonts()
        _typingTimer = New Timer() With {.Interval = 140}
        AddHandler _typingTimer.Tick, AddressOf OnTypingTick
        _scrollTimer = New Timer() With {.Interval = 15}
        AddHandler _scrollTimer.Tick, AddressOf OnScrollTick
        Size = New Size(360, 420)
    End Sub

#Region "Propriedades públicas"

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Conversation As ChatConversation
        Get
            Return _conversation
        End Get
        Set(value As ChatConversation)
            If Object.ReferenceEquals(value, _conversation) Then Return
            If _conversation IsNot Nothing Then
                _conversation.SavedScroll = If(IsAtBottom, -1, _scroll)
                RemoveHandler _conversation.Messages.CollectionChanged, AddressOf OnMessagesChanged
                RemoveHandler _conversation.Messages.ItemChanged, AddressOf OnMessageItemChanged
                RemoveHandler _conversation.PropertyChanged, AddressOf OnConversationPropertyChanged
            End If
            _conversation = value
            _pendingNew = 0
            _hoverRow = Nothing
            _topRaised = False
            _scrollTimer.Stop()
            If value IsNot Nothing Then
                AddHandler value.Messages.CollectionChanged, AddressOf OnMessagesChanged
                AddHandler value.Messages.ItemChanged, AddressOf OnMessageItemChanged
                AddHandler value.PropertyChanged, AddressOf OnConversationPropertyChanged
            End If
            _stickToBottom = (value Is Nothing OrElse value.SavedScroll < 0)
            _layoutDirty = True
            EnsureLayout()
            If Not _stickToBottom Then SetScroll(value.SavedScroll)
            _scrollTarget = _scroll
            UpdateTypingTimer()
            Invalidate()
        End Set
    End Property

    <Category("Aparência"), Description("Cores, fontes e medidas."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property Theme As ChatTheme
        Get
            Return _theme
        End Get
        Set(value As ChatTheme)
            If value Is Nothing OrElse Object.ReferenceEquals(value, _theme) Then Return
            RemoveHandler _theme.Changed, AddressOf OnThemeChanged
            _theme = value
            AddHandler _theme.Changed, AddressOf OnThemeChanged
            OnThemeChanged(Me, EventArgs.Empty)
        End Set
    End Property

    <Category("Aparência"), DefaultValue(True), Description("Mostra o avatar ao lado das mensagens recebidas.")>
    Public Property ShowAvatars As Boolean
        Get
            Return _showAvatars
        End Get
        Set(value As Boolean)
            If _showAvatars <> value Then _showAvatars = value : InvalidateLayout()
        End Set
    End Property

    <Category("Aparência"), DefaultValue(GetType(SenderNameMode), "Auto"), Description("Quando mostrar o nome de quem enviou.")>
    Public Property SenderNames As SenderNameMode
        Get
            Return _senderNames
        End Get
        Set(value As SenderNameMode)
            If _senderNames <> value Then _senderNames = value : InvalidateLayout()
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(True), Description("Menu com Copiar texto / Copiar imagem / Abrir anexo / Tentar novamente.")>
    Public Property EnableDefaultContextMenu As Boolean
        Get
            Return _defaultMenu
        End Get
        Set(value As Boolean)
            _defaultMenu = value
        End Set
    End Property

    <Browsable(False)>
    Public ReadOnly Property IsAtBottom As Boolean
        Get
            Return _scroll >= MaxScroll - S(4)
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property ScrollPosition As Integer
        Get
            Return _scroll
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property ContentHeight As Integer
        Get
            EnsureLayout()
            Return _contentHeight
        End Get
    End Property

#End Region

#Region "Métodos públicos"

    Public Sub ScrollToBottom()
        ScrollToBottom(False)
    End Sub

    Public Sub ScrollToBottom(animated As Boolean)
        EnsureLayout()
        _pendingNew = 0
        If animated Then
            AnimateTo(MaxScroll)
        Else
            _scrollTimer.Stop()
            SetScroll(MaxScroll)
        End If
        Invalidate()
    End Sub

    ''' <summary>Rola até que a mensagem fique visível (centralizada).</summary>
    Public Sub ScrollToMessage(message As ChatMessage)
        EnsureLayout()
        For Each r In _rows
            If Object.ReferenceEquals(r.Message, message) Then
                AnimateTo(r.Y - (ClientSize.Height - r.Height) \ 2)
                Return
            End If
        Next
    End Sub

    Public Sub ScrollBy(delta As Integer)
        AnimateTo(_scrollTarget + delta)
    End Sub

    ''' <summary>Refaz o layout (chame se alterar algo que o controle não percebe sozinho).</summary>
    Public Sub InvalidateLayout()
        _measureVersion += 1
        _layoutDirty = True
        Invalidate()
    End Sub

#End Region

#Region "Escala / helpers"

    Private Function S(v As Integer) As Integer
        Return CInt(Math.Round(v * _scale))
    End Function

    Private ReadOnly Property MaxScroll As Integer
        Get
            Return Math.Max(0, _contentHeight - ClientSize.Height)
        End Get
    End Property

    Private Function V(r As Rectangle) As Rectangle
        Return New Rectangle(r.X, r.Y - _scroll, r.Width, r.Height)
    End Function

    Private Function WantNames() As Boolean
        Select Case _senderNames
            Case SenderNameMode.Always : Return True
            Case SenderNameMode.Never : Return False
            Case Else : Return _conversation IsNot Nothing AndAlso _conversation.IsGroup
        End Select
    End Function

    Private Shared Function SameGroup(a As ChatMessage, b As ChatMessage) As Boolean
        If a Is Nothing OrElse b Is Nothing Then Return False
        If a.Direction <> b.Direction OrElse a.Direction = MessageDirection.System Then Return False
        If a.Timestamp.Date <> b.Timestamp.Date Then Return False
        If Math.Abs((b.Timestamp - a.Timestamp).TotalMinutes) > 5 Then Return False
        Return ChatUser.SameUser(a.Sender, b.Sender)
    End Function

    Private Function DayLabel(d As Date) As String
        Dim today = Date.Today
        If d = today Then Return ChatStrings.Today
        If d = today.AddDays(-1) Then Return ChatStrings.Yesterday
        If d > today.AddDays(-7) AndAlso d < today Then
            Dim n = d.ToString("dddd")
            Return Char.ToUpper(n(0)) & n.Substring(1)
        End If
        Return d.ToLongDateString()
    End Function

    Private Sub RebuildFonts()
        If _nameFont IsNot Nothing Then _nameFont.Dispose()
        _nameFont = New Font(_theme.MetaFont.FontFamily, _theme.MetaFont.Size + 0.5F, FontStyle.Bold)
        _singleLineHeight = TextRenderer.MeasureText("Ag", _theme.MessageFont, Size.Empty, SingleFlags).Height
    End Sub

#End Region

#Region "Layout"

    Private Sub EnsureLayout()
        If _layoutDirty OrElse _layoutWidth <> ClientSize.Width Then PerformChatLayout()
    End Sub

    Private Sub PerformChatLayout()
        _layoutDirty = False
        _layoutWidth = ClientSize.Width
        _rows.Clear()
        _contentHeight = 0
        If _conversation Is Nothing OrElse _layoutWidth <= S(80) Then Return

        Dim msgs = _conversation.Messages
        Dim y = S(10)
        Dim prev As ChatMessage = Nothing
        Dim first = True
        For i = 0 To msgs.Count - 1
            Dim m = msgs(i)
            If first OrElse msgs(i - 1).Timestamp.Date <> m.Timestamp.Date Then
                If Not first Then y += S(8)
                Dim dr As New Row() With {.Kind = RowKind.Day, .Day = m.Timestamp.Date, .Y = y, .Height = S(30)}
                dr.MetaText = DayLabel(dr.Day)
                Dim ts = TextRenderer.MeasureText(dr.MetaText, _theme.MetaFont, Size.Empty, SingleFlags)
                Dim pw = ts.Width + S(20), ph = ts.Height + S(8)
                dr.Bubble = New Rectangle((_layoutWidth - pw) \ 2, y + (dr.Height - ph) \ 2, pw, ph)
                _rows.Add(dr)
                y += dr.Height + S(4)
                prev = Nothing
            End If
            Dim nxt = If(i + 1 < msgs.Count, msgs(i + 1), Nothing)
            Dim top = Not SameGroup(prev, m)
            Dim bottom = Not SameGroup(m, nxt)
            If prev IsNot Nothing Then y += If(top, S(10), S(2))
            Dim r = LayoutMessage(m, y, top, bottom)
            _rows.Add(r)
            y += r.Height
            prev = m
            first = False
        Next
        If _conversation.IsTyping Then
            y += S(10)
            Dim tr = LayoutTyping(y)
            _rows.Add(tr)
            y += tr.Height
        End If
        _contentHeight = y + S(14)

        If _stickToBottom Then
            _scroll = MaxScroll
        Else
            _scroll = Math.Max(0, Math.Min(_scroll, MaxScroll))
        End If
        _scrollTarget = _scroll
    End Sub

    Private Function MeasureMessageText(m As ChatMessage, maxW As Integer) As Size
        If m.MeasureVersion = _measureVersion AndAlso m.MeasureWidth = maxW Then Return m.MeasuredText
        Dim sz = TextRenderer.MeasureText(m.Text, _theme.MessageFont, New Size(maxW, Integer.MaxValue), TextFlags)
        sz.Width = Math.Min(sz.Width, maxW)
        m.MeasureVersion = _measureVersion
        m.MeasureWidth = maxW
        m.MeasuredText = sz
        Return sz
    End Function

    Private Shared Function FitImage(src As Size, maxW As Integer, maxH As Integer) As Size
        If src.Width <= 0 OrElse src.Height <= 0 Then Return New Size(maxW, maxW \ 2)
        Dim k = Math.Min(1.0, Math.Min(maxW / CDbl(src.Width), maxH / CDbl(src.Height)))
        Return New Size(Math.Max(1, CInt(src.Width * k)), Math.Max(1, CInt(src.Height * k)))
    End Function

    Private Function LayoutMessage(m As ChatMessage, y As Integer, top As Boolean, bottom As Boolean) As Row
        Dim r As New Row() With {.Kind = RowKind.Message, .Message = m, .Y = y, .GroupTop = top, .GroupBottom = bottom}
        Dim t = _theme
        Dim left = S(12)
        Dim right = _layoutWidth - S(16)

        ' ---------- mensagem do sistema: pílula centralizada
        If m.Direction = MessageDirection.System Then
            Dim maxW = CInt((right - left) * 0.8)
            Dim ts = TextRenderer.MeasureText(m.Text, t.MetaFont, New Size(maxW, Integer.MaxValue), TextFlags)
            Dim pw = Math.Min(maxW, ts.Width) + S(24)
            Dim ph = ts.Height + S(10)
            r.Bubble = New Rectangle((_layoutWidth - pw) \ 2, y, pw, ph)
            r.TextRect = New Rectangle(r.Bubble.X + S(12), y + S(5), pw - S(24), ts.Height)
            r.Height = ph
            Return r
        End If

        Dim outgoing = (m.Direction = MessageDirection.Outgoing)
        Dim avSize = S(t.AvatarSize)
        Dim avSpace = If(Not outgoing AndAlso _showAvatars, avSize + S(8), 0)
        Dim padX = S(11)
        Dim padY = S(7)
        Dim imgPad = S(3)
        Dim avail = right - left - avSpace - S(56)
        Dim maxBubble = Math.Max(S(120), Math.Min(S(t.MaxBubbleWidth), avail))

        ' nome
        Dim showName = Not outgoing AndAlso top AndAlso m.Sender IsNot Nothing AndAlso WantNames()
        Dim nameSize = Size.Empty
        If showName Then nameSize = TextRenderer.MeasureText(m.Sender.DisplayName, _nameFont, Size.Empty, SingleFlags)

        ' hora + status
        r.MetaText = m.Timestamp.ToString(ChatStrings.TimeFormat)
        Dim metaTs = TextRenderer.MeasureText(r.MetaText, t.MetaFont, Size.Empty, SingleFlags)
        Dim statusW = If(outgoing, StatusIconWidth(m.Status, _scale), 0)
        Dim metaW = metaTs.Width + If(statusW > 0, S(4) + statusW, 0)
        Dim metaH = metaTs.Height

        ' imagem
        Dim hasImage = m.Image IsNot Nothing
        Dim imgSize = Size.Empty
        Dim innerImgW = 0
        If hasImage Then
            Dim srcSize As Size
            Try
                srcSize = m.Image.Size
            Catch
                srcSize = New Size(200, 120)
            End Try
            imgSize = FitImage(srcSize, Math.Min(maxBubble - imgPad * 2, S(300)), S(300))
            innerImgW = Math.Max(imgSize.Width, S(140))
        End If

        ' texto
        Dim hasText = m.Text.Length > 0
        Dim textMax = If(hasImage, innerImgW + imgPad * 2 - padX * 2, maxBubble - padX * 2)
        Dim textSize = Size.Empty
        If hasText Then textSize = MeasureMessageText(m, textMax)

        ' anexo
        Dim hasAttach = Not hasImage AndAlso m.Attachment IsNot Nothing
        Dim attW = If(hasAttach, Math.Min(maxBubble - padX * 2, S(250)), 0)
        Dim attH = S(44)

        Dim singleLine = hasText AndAlso textSize.Height <= CInt(_singleLineHeight * 1.4)
        Dim metaInline = hasText AndAlso Not hasImage AndAlso Not hasAttach AndAlso singleLine AndAlso
                         (textSize.Width + S(10) + metaW <= maxBubble - padX * 2)

        ' largura do balão
        Dim bw As Integer
        If hasImage Then
            bw = innerImgW + imgPad * 2
        Else
            Dim cw = 0
            If showName Then cw = Math.Max(cw, nameSize.Width)
            If hasText Then cw = Math.Max(cw, If(metaInline, textSize.Width + S(10) + metaW, textSize.Width))
            If hasAttach Then cw = Math.Max(cw, attW)
            If Not metaInline Then cw = Math.Max(cw, metaW)
            cw = Math.Min(cw, maxBubble - padX * 2)
            bw = cw + padX * 2
        End If
        bw = Math.Max(bw, S(52))

        Dim bx = If(outgoing, right - bw, left + avSpace)
        Dim innerL = bx + padX
        Dim innerR = bx + bw - padX
        Dim cy = y

        If hasImage Then
            If showName Then
                cy += padY
                r.NameRect = New Rectangle(innerL, cy, innerR - innerL, nameSize.Height)
                cy += nameSize.Height + S(4)
            Else
                cy += imgPad
            End If
            r.ImageRect = New Rectangle(bx + imgPad + (bw - imgPad * 2 - imgSize.Width) \ 2, cy, imgSize.Width, imgSize.Height)
            cy += imgSize.Height
            If hasText Then
                cy += S(5)
                r.TextRect = New Rectangle(innerL, cy, innerR - innerL, textSize.Height)
                cy += textSize.Height + S(2)
                r.MetaRect = New Rectangle(innerR - metaW, cy, metaW, metaH)
                cy += metaH + padY - S(2)
            Else
                r.MetaOverlay = True
                r.MetaRect = New Rectangle(r.ImageRect.Right - metaW - S(9), r.ImageRect.Bottom - metaH - S(7), metaW, metaH)
                cy += imgPad
            End If
        Else
            cy += padY
            If showName Then
                r.NameRect = New Rectangle(innerL, cy, innerR - innerL, nameSize.Height)
                cy += nameSize.Height + S(2)
            End If
            If hasAttach Then
                r.AttachRect = New Rectangle(innerL - S(4), cy, innerR - innerL + S(8), attH)
                cy += attH + If(hasText, S(6), S(3))
            End If
            If hasText Then
                Dim tw = If(metaInline, textSize.Width + S(2), innerR - innerL)
                r.TextRect = New Rectangle(innerL, cy, tw, textSize.Height)
                cy += textSize.Height
            End If
            If metaInline Then
                r.MetaRect = New Rectangle(innerR - metaW, cy - metaH - S(1), metaW, metaH)
                cy += padY
            Else
                cy += S(2)
                r.MetaRect = New Rectangle(innerR - metaW, cy, metaW, metaH)
                cy += metaH + padY - S(2)
            End If
        End If

        r.Bubble = New Rectangle(bx, y, bw, cy - y)
        r.Height = r.Bubble.Height
        If statusW > 0 Then r.StatusRect = New Rectangle(r.MetaRect.Right - statusW, r.MetaRect.Y, statusW, r.MetaRect.Height)
        r.ShowAvatar = Not outgoing AndAlso _showAvatars AndAlso bottom
        If r.ShowAvatar Then
            r.Height = Math.Max(r.Height, avSize)
            r.AvatarRect = New Rectangle(left, y + r.Height - avSize, avSize, avSize)
        End If
        Return r
    End Function

    Private Function LayoutTyping(y As Integer) As Row
        Dim r As New Row() With {.Kind = RowKind.Typing, .Y = y, .GroupTop = True, .GroupBottom = True}
        Dim avSize = S(_theme.AvatarSize)
        Dim left = S(12)
        Dim avSpace = If(_showAvatars, avSize + S(8), 0)
        r.Bubble = New Rectangle(left + avSpace, y, S(62), S(34))
        r.Height = Math.Max(r.Bubble.Height, If(_showAvatars, avSize, 0))
        r.ShowAvatar = _showAvatars
        If _showAvatars Then r.AvatarRect = New Rectangle(left, y + r.Height - avSize, avSize, avSize)
        r.Bubble.Y = y + r.Height - r.Bubble.Height
        Return r
    End Function

#End Region

#Region "Pintura"

    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
        ' tudo é pintado em OnPaint (evita flicker)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g = e.Graphics
        g.Clear(_theme.BackColor)
        EnsureLayout()

        If _conversation Is Nothing Then
            DrawCenteredInfo(g, ChatStrings.NoConversation)
            Return
        End If
        If _rows.Count = 0 Then
            DrawCenteredInfo(g, ChatStrings.EmptyConversation)
            Return
        End If

        Dim viewTop = _scroll
        Dim viewBottom = _scroll + ClientSize.Height
        Dim firstIdx = FindRowAt(viewTop)
        If firstIdx < 0 Then firstIdx = 0
        Dim firstVisibleDayRow As Row = Nothing
        For i = firstIdx To _rows.Count - 1
            Dim r = _rows(i)
            If r.Y > viewBottom Then Exit For
            If r.Y + r.Height < viewTop Then Continue For
            If Not V(New Rectangle(0, r.Y, Width, r.Height)).IntersectsWith(e.ClipRectangle) Then Continue For
            Select Case r.Kind
                Case RowKind.Day : DrawDayRow(g, r)
                Case RowKind.Message : DrawMessageRow(g, r)
                Case RowKind.Typing : DrawTypingRow(g, r)
            End Select
        Next

        DrawStickyDay(g, firstIdx)
        DrawScrollBar(g)
        DrawJumpButton(g)
    End Sub

    Private Sub DrawCenteredInfo(g As Graphics, text As String)
        TextRenderer.DrawText(g, text, _theme.MessageFont, ClientRectangle, _theme.MetaColor,
                              TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.WordBreak Or TextFormatFlags.NoPrefix)
    End Sub

    Private Sub DrawPill(g As Graphics, rc As Rectangle, text As String)
        g.SmoothingMode = SmoothingMode.AntiAlias
        FillRound(g, _theme.SystemBackColor, rc, rc.Height / 2.0F)
        TextRenderer.DrawText(g, text, _theme.MetaFont, rc, _theme.SystemTextColor,
                              TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or SingleFlags)
    End Sub

    Private Sub DrawDayRow(g As Graphics, r As Row)
        DrawPill(g, V(r.Bubble), r.MetaText)
    End Sub

    ''' <summary>Data "grudada" no topo enquanto rola (como no WhatsApp).</summary>
    Private Sub DrawStickyDay(g As Graphics, firstIdx As Integer)
        If _scroll <= 0 OrElse firstIdx < 0 OrElse firstIdx >= _rows.Count Then Return
        Dim dayRow As Row = Nothing
        For i = firstIdx To 0 Step -1
            If _rows(i).Kind = RowKind.Day Then dayRow = _rows(i) : Exit For
        Next
        If dayRow Is Nothing Then Return
        Dim vis = V(dayRow.Bubble)
        If vis.Y >= S(4) Then Return ' o separador real já está visível
        Dim rc As New Rectangle(dayRow.Bubble.X, S(6), dayRow.Bubble.Width, dayRow.Bubble.Height)
        ' empurrado pelo próximo separador
        For i = firstIdx + 1 To _rows.Count - 1
            Dim nr = _rows(i)
            If nr.Y - _scroll > ClientSize.Height Then Exit For
            If nr.Kind = RowKind.Day Then
                Dim ny = V(nr.Bubble).Y
                If ny < rc.Bottom + S(4) Then rc.Y = ny - rc.Height - S(4)
                Exit For
            End If
        Next
        DrawPill(g, rc, dayRow.MetaText)
    End Sub

    Private Sub DrawMessageRow(g As Graphics, r As Row)
        Dim m = r.Message
        Dim t = _theme
        g.SmoothingMode = SmoothingMode.AntiAlias

        If m.Direction = MessageDirection.System Then
            Dim rcS = V(r.Bubble)
            FillRound(g, t.SystemBackColor, rcS, Math.Min(rcS.Height / 2.0F, S(12)))
            TextRenderer.DrawText(g, m.Text, t.MetaFont, V(r.TextRect), t.SystemTextColor, TextFlags Or TextFormatFlags.HorizontalCenter)
            Return
        End If

        Dim outgoing = (m.Direction = MessageDirection.Outgoing)
        Dim bubbleColor = If(outgoing, t.OutgoingBubbleColor, t.IncomingBubbleColor)
        Dim textColor = If(outgoing, t.OutgoingTextColor, t.IncomingTextColor)
        Dim metaColor = If(outgoing, t.OutgoingMetaColor, t.MetaColor)
        Dim big = CSng(S(t.BubbleRadius))
        Dim small = CSng(Math.Min(S(5), S(t.BubbleRadius)))
        Dim tl, tr, br, bl As Single
        If outgoing Then
            tl = big : bl = big
            tr = If(r.GroupTop, big, small) : br = If(r.GroupBottom, big, small)
        Else
            tr = big : br = big
            tl = If(r.GroupTop, big, small) : bl = If(r.GroupBottom, big, small)
        End If

        Dim rc = V(r.Bubble)
        ' sombra suave (tema claro)
        If Not IsDark(t.BackColor) Then
            Using sp = RoundRect(New RectangleF(rc.X, rc.Y + 1, rc.Width, rc.Height), tl, tr, br, bl), sb As New SolidBrush(Color.FromArgb(22, 0, 0, 0))
                g.FillPath(sb, sp)
            End Using
        End If
        Using p = RoundRect(rc, tl, tr, br, bl), b As New SolidBrush(bubbleColor)
            g.FillPath(b, p)
        End Using

        ' nome do remetente
        If Not r.NameRect.IsEmpty AndAlso m.Sender IsNot Nothing Then
            Dim nc = m.Sender.EffectiveColor
            If IsDark(bubbleColor) Then nc = Blend(nc, Color.White, 0.35)
            TextRenderer.DrawText(g, m.Sender.DisplayName, _nameFont, V(r.NameRect), nc, EllipsisFlags)
        End If

        ' imagem
        If Not r.ImageRect.IsEmpty Then
            Dim ir = V(r.ImageRect)
            Dim thumb = GetThumb(m, ir.Size)
            Dim ip = S(3)
            Dim itl = If(r.NameRect.IsEmpty, Math.Max(0.0F, tl - ip), S(4))
            Dim itr = If(r.NameRect.IsEmpty, Math.Max(0.0F, tr - ip), S(4))
            Dim ibr = If(r.TextRect.IsEmpty, Math.Max(0.0F, br - ip), S(4))
            Dim ibl = If(r.TextRect.IsEmpty, Math.Max(0.0F, bl - ip), S(4))
            If thumb IsNot Nothing Then
                Using tb As New TextureBrush(thumb, WrapMode.Clamp), ipath = RoundRect(ir, itl, itr, ibr, ibl)
                    tb.TranslateTransform(ir.X, ir.Y)
                    g.FillPath(tb, ipath)
                End Using
            End If
            If r.MetaOverlay Then
                Dim mr = V(r.MetaRect)
                mr.Inflate(S(6), S(2))
                FillRound(g, Color.FromArgb(120, 0, 0, 0), mr, mr.Height / 2.0F)
                metaColor = Color.White
            End If
        End If

        ' anexo
        If Not r.AttachRect.IsEmpty AndAlso m.Attachment IsNot Nothing Then
            DrawAttachment(g, r, V(r.AttachRect), bubbleColor, textColor, metaColor)
        End If

        ' texto
        If Not r.TextRect.IsEmpty Then
            TextRenderer.DrawText(g, m.Text, t.MessageFont, V(r.TextRect), textColor, TextFlags)
        End If

        ' hora + status
        Dim meta = V(r.MetaRect)
        TextRenderer.DrawText(g, r.MetaText, t.MetaFont, New Point(meta.X, meta.Y), metaColor, SingleFlags)
        If outgoing AndAlso m.Status <> MessageStatus.None Then
            Dim sr = V(r.StatusRect)
            Dim readColor = If(r.MetaOverlay, Color.White, t.ReadTicksColor)
            If Not IsDark(bubbleColor) AndAlso Not r.MetaOverlay Then readColor = t.AccentColor
            DrawStatus(g, m.Status, sr.X, sr.Y + sr.Height / 2.0F, metaColor, readColor, t.FailedColor, _scale)
        End If

        ' avatar
        If r.ShowAvatar Then DrawAvatar(g, m.Sender, V(r.AvatarRect))
    End Sub

    Private Sub DrawAttachment(g As Graphics, r As Row, rc As Rectangle, bubbleColor As Color, textColor As Color, metaColor As Color)
        Dim a = r.Message.Attachment
        Dim hover = Object.ReferenceEquals(r, _hoverRow) AndAlso _hoverArea = ChatHitArea.Attachment
        Dim cardColor = If(IsDark(bubbleColor), Blend(bubbleColor, Color.White, If(hover, 0.16, 0.1)), Blend(bubbleColor, Color.Black, If(hover, 0.08, 0.05)))
        g.SmoothingMode = SmoothingMode.AntiAlias
        FillRound(g, cardColor, rc, S(8))
        Dim icon As New Rectangle(rc.X + S(8), rc.Y + (rc.Height - S(32)) \ 2, S(26), S(32))
        Dim iconColor = If(r.Message.Direction = MessageDirection.Outgoing, Blend(bubbleColor, Color.Black, 0.25), _theme.AccentColor)
        Using f As New Font(_theme.MetaFont.FontFamily, Math.Max(5.5F, _theme.MetaFont.Size - 1.5F), FontStyle.Bold)
            DrawFileIcon(g, icon, a.Extension, iconColor, f)
        End Using
        Dim tx = icon.Right + S(10)
        Dim nameRc As New Rectangle(tx, rc.Y + S(5), rc.Right - tx - S(8), _singleLineHeight)
        TextRenderer.DrawText(g, If(a.FileName, "arquivo"), _theme.MessageFont, nameRc, textColor, EllipsisFlags)
        Dim info = FormatSize(a.Size)
        If a.Extension.Length > 0 Then info = If(info.Length > 0, info & " · " & a.Extension, a.Extension)
        TextRenderer.DrawText(g, info, _theme.MetaFont, New Rectangle(tx, nameRc.Bottom + S(1), nameRc.Width, S(16)), metaColor, EllipsisFlags)
    End Sub

    Private Sub DrawTypingRow(g As Graphics, r As Row)
        Dim t = _theme
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim rc = V(r.Bubble)
        Dim big = CSng(S(t.BubbleRadius))
        If Not IsDark(t.BackColor) Then
            Using sp = RoundRect(New RectangleF(rc.X, rc.Y + 1, rc.Width, rc.Height), big, big, big, S(5)), sb As New SolidBrush(Color.FromArgb(22, 0, 0, 0))
                g.FillPath(sb, sp)
            End Using
        End If
        Using p = RoundRect(rc, big, big, big, S(5)), b As New SolidBrush(t.IncomingBubbleColor)
            g.FillPath(b, p)
        End Using
        Dim d = S(7)
        Dim gap = S(6)
        Dim total = d * 3 + gap * 2
        Dim x0 = rc.X + (rc.Width - total) \ 2
        Dim cy = rc.Y + rc.Height \ 2
        For i = 0 To 2
            Dim ph = _typingPhase * 0.45 - i * 0.9
            Dim k = (Math.Sin(ph) + 1) / 2.0 ' 0..1
            Dim dy = CSng(-k * S(3))
            Dim c = Blend(t.MetaColor, t.IncomingTextColor, k * 0.6)
            Using b As New SolidBrush(c)
                g.FillEllipse(b, CSng(x0 + i * (d + gap)), cy - d / 2.0F + dy, d, d)
            End Using
        Next
        If r.ShowAvatar AndAlso _conversation IsNot Nothing Then DrawAvatar(g, _conversation.TypingUser, V(r.AvatarRect))
    End Sub

    Private Function GetThumb(m As ChatMessage, size As Size) As Bitmap
        If size.Width <= 0 OrElse size.Height <= 0 OrElse m.Image Is Nothing Then Return Nothing
        If m.Thumb IsNot Nothing AndAlso m.ThumbSize = size Then Return m.Thumb
        m.ClearThumb()
        Try
            Dim bmp As New Bitmap(size.Width, size.Height, Imaging.PixelFormat.Format32bppPArgb)
            Using g = Graphics.FromImage(bmp)
                g.InterpolationMode = InterpolationMode.HighQualityBicubic
                g.PixelOffsetMode = PixelOffsetMode.HighQuality
                g.CompositingQuality = CompositingQuality.HighQuality
                g.DrawImage(m.Image, New Rectangle(0, 0, size.Width, size.Height))
            End Using
            m.Thumb = bmp
            m.ThumbSize = size
            Return bmp
        Catch
            Return Nothing
        End Try
    End Function

    Private Function ScrollBarRects(ByRef track As Rectangle, ByRef thumb As Rectangle) As Boolean
        If _contentHeight <= ClientSize.Height OrElse ClientSize.Height <= 0 Then Return False
        Dim w = If(_sbHover OrElse _sbDrag, S(8), S(5))
        track = New Rectangle(ClientSize.Width - w - S(3), S(3), w, ClientSize.Height - S(6))
        Dim th = Math.Max(S(32), CInt(track.Height * (ClientSize.Height / CDbl(_contentHeight))))
        Dim ty = track.Y + CInt((track.Height - th) * (_scroll / CDbl(Math.Max(1, MaxScroll))))
        thumb = New Rectangle(track.X, ty, w, th)
        Return True
    End Function

    Private Sub DrawScrollBar(g As Graphics)
        Dim track, thumb As Rectangle
        If Not ScrollBarRects(track, thumb) Then Return
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim alpha = If(_sbHover OrElse _sbDrag, 150, 80)
        FillRound(g, Color.FromArgb(alpha, _theme.MetaColor), thumb, thumb.Width / 2.0F)
    End Sub

    Private Function JumpButtonRect() As Rectangle
        Dim d = S(38)
        Return New Rectangle(ClientSize.Width - d - S(22), ClientSize.Height - d - S(14), d, d)
    End Function

    Private Function JumpButtonVisible() As Boolean
        Return MaxScroll - _scroll > S(240) OrElse (_pendingNew > 0 AndAlso Not IsAtBottom)
    End Function

    Private Sub DrawJumpButton(g As Graphics)
        If Not JumpButtonVisible() Then Return
        Dim rc = JumpButtonRect()
        Dim t = _theme
        g.SmoothingMode = SmoothingMode.AntiAlias
        Using sb As New SolidBrush(Color.FromArgb(40, 0, 0, 0))
            g.FillEllipse(sb, rc.X, rc.Y + S(2), rc.Width, rc.Height)
        End Using
        Using b As New SolidBrush(If(_jumpHover, Blend(t.IncomingBubbleColor, t.IncomingTextColor, 0.08), t.IncomingBubbleColor))
            g.FillEllipse(b, rc)
        End Using
        DrawChevronDown(g, New PointF(rc.X + rc.Width / 2.0F, rc.Y + rc.Height / 2.0F + S(1)), S(6), t.MetaColor, 2.0F * _scale)
        If _pendingNew > 0 Then
            Dim txt = If(_pendingNew > 99, "99+", _pendingNew.ToString())
            Dim bs = MeasureBadge(txt, t.MetaFont, _scale)
            Dim br As New Rectangle(rc.Right - bs.Width + S(4), rc.Y - S(6), bs.Width, bs.Height)
            DrawBadge(g, br, txt, t.AccentColor, Color.White, t.MetaFont)
        End If
    End Sub

#End Region

#Region "Rolagem"

    Private Sub SetScroll(value As Integer)
        Dim nv = Math.Max(0, Math.Min(value, MaxScroll))
        _stickToBottom = (nv >= MaxScroll - S(4))
        If _stickToBottom Then _pendingNew = 0
        If nv = _scroll Then Return
        _scroll = nv
        Invalidate()
        If _scroll > S(300) Then _topRaised = False
        If _scroll <= S(40) AndAlso MaxScroll > 0 AndAlso Not _topRaised AndAlso _conversation IsNot Nothing Then
            _topRaised = True
            RaiseEvent LoadOlderRequested(Me, New ChatConversationEventArgs(_conversation))
        End If
    End Sub

    Private Sub AnimateTo(target As Integer)
        EnsureLayout()
        _scrollTarget = Math.Max(0, Math.Min(target, MaxScroll))
        If _scrollTarget = _scroll Then Return
        _scrollTimer.Start()
    End Sub

    Private Sub OnScrollTick(sender As Object, e As EventArgs)
        Dim diff = _scrollTarget - _scroll
        If Math.Abs(diff) <= 1 Then
            SetScroll(_scrollTarget)
            _scrollTimer.Stop()
            Return
        End If
        Dim stepPx = CInt(diff * 0.3)
        If stepPx = 0 Then stepPx = Math.Sign(diff)
        Dim before = _scroll
        SetScroll(_scroll + stepPx)
        If _scroll = before Then _scrollTimer.Stop()
    End Sub

    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        MyBase.OnMouseWheel(e)
        Dim lines = SystemInformation.MouseWheelScrollLines
        If lines <= 0 Then lines = 3
        Dim stepPx = CInt(-e.Delta / 120.0 * lines * S(22))
        Dim baseVal = If(_scrollTimer.Enabled, _scrollTarget, _scroll)
        AnimateTo(baseVal + stepPx)
    End Sub

    Protected Overrides Function IsInputKey(keyData As Keys) As Boolean
        Select Case keyData And Keys.KeyCode
            Case Keys.Up, Keys.Down, Keys.PageUp, Keys.PageDown, Keys.Home, Keys.End
                Return True
        End Select
        Return MyBase.IsInputKey(keyData)
    End Function

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        Select Case e.KeyCode
            Case Keys.Up : ScrollBy(-S(40)) : e.Handled = True
            Case Keys.Down : ScrollBy(S(40)) : e.Handled = True
            Case Keys.PageUp : ScrollBy(-(ClientSize.Height - S(40))) : e.Handled = True
            Case Keys.PageDown : ScrollBy(ClientSize.Height - S(40)) : e.Handled = True
            Case Keys.Home : AnimateTo(0) : e.Handled = True
            Case Keys.End : ScrollToBottom(True) : e.Handled = True
        End Select
    End Sub

    ''' <summary>Encaminhado pelo ChatBox (PageUp/PageDown na caixa de digitação).</summary>
    Friend Sub HandleNavigationKey(key As Keys)
        Select Case key
            Case Keys.PageUp : ScrollBy(-(ClientSize.Height - S(40)))
            Case Keys.PageDown : ScrollBy(ClientSize.Height - S(40))
        End Select
    End Sub

#End Region

#Region "Mouse / hit-test"

    Private Function FindRowAt(contentY As Integer) As Integer
        Dim lo = 0, hi = _rows.Count - 1, ans = -1
        While lo <= hi
            Dim mid = (lo + hi) \ 2
            If _rows(mid).Y + _rows(mid).Height >= contentY Then
                ans = mid : hi = mid - 1
            Else
                lo = mid + 1
            End If
        End While
        Return ans
    End Function

    Private Function HitTest(pt As Point, ByRef area As ChatHitArea) As Row
        area = ChatHitArea.None
        EnsureLayout()
        Dim cy = pt.Y + _scroll
        Dim idx = FindRowAt(cy)
        If idx < 0 Then Return Nothing
        For i = idx To Math.Min(idx + 1, _rows.Count - 1)
            Dim r = _rows(i)
            If r.Kind <> RowKind.Message Then Continue For
            Dim cp As New Point(pt.X, cy)
            If r.ShowAvatar AndAlso r.AvatarRect.Contains(cp) Then area = ChatHitArea.Avatar : Return r
            If Not r.Bubble.Contains(cp) Then Continue For
            If Not r.StatusRect.IsEmpty AndAlso r.Message.Status = MessageStatus.Failed AndAlso Rectangle.Inflate(r.StatusRect, S(3), S(3)).Contains(cp) Then
                area = ChatHitArea.Status
            ElseIf Not r.ImageRect.IsEmpty AndAlso r.ImageRect.Contains(cp) Then
                area = ChatHitArea.Image
            ElseIf Not r.AttachRect.IsEmpty AndAlso r.AttachRect.Contains(cp) Then
                area = ChatHitArea.Attachment
            ElseIf Not r.NameRect.IsEmpty AndAlso r.NameRect.Contains(cp) Then
                area = ChatHitArea.SenderName
            ElseIf Not r.TextRect.IsEmpty AndAlso r.TextRect.Contains(cp) Then
                area = ChatHitArea.Text
            Else
                area = ChatHitArea.Bubble
            End If
            Return r
        Next
        Return Nothing
    End Function

    ''' <summary>Mensagem sob um ponto (coordenadas do cliente) ou Nothing.</summary>
    Public Function GetMessageAt(pt As Point) As ChatMessage
        Dim area As ChatHitArea
        Dim r = HitTest(pt, area)
        Return If(r Is Nothing, Nothing, r.Message)
    End Function

    Private Function InScrollBarZone(pt As Point) As Boolean
        Return _contentHeight > ClientSize.Height AndAlso pt.X >= ClientSize.Width - S(14)
    End Function

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        If _sbDrag Then
            Dim track, thumb As Rectangle
            If ScrollBarRects(track, thumb) Then
                Dim free = track.Height - thumb.Height
                If free > 0 Then
                    Dim ratio = (e.Y - _sbDragOffset - track.Y) / CDbl(free)
                    _scrollTimer.Stop()
                    SetScroll(CInt(ratio * MaxScroll))
                    _scrollTarget = _scroll
                End If
            End If
            Return
        End If

        Dim sbh = InScrollBarZone(e.Location)
        Dim jh = JumpButtonVisible() AndAlso JumpButtonRect().Contains(e.Location)
        Dim area As ChatHitArea = ChatHitArea.None
        Dim row As Row = Nothing
        If Not sbh AndAlso Not jh Then row = HitTest(e.Location, area)

        If sbh <> _sbHover OrElse jh <> _jumpHover OrElse Not Object.ReferenceEquals(row, _hoverRow) OrElse area <> _hoverArea Then
            _sbHover = sbh : _jumpHover = jh : _hoverRow = row : _hoverArea = area
            Invalidate()
        End If
        Dim hand = jh OrElse area = ChatHitArea.Image OrElse area = ChatHitArea.Attachment OrElse area = ChatHitArea.Status
        Cursor = If(hand, Cursors.Hand, Cursors.Default)
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        If _sbHover OrElse _jumpHover OrElse _hoverRow IsNot Nothing Then
            _sbHover = False : _jumpHover = False : _hoverRow = Nothing : _hoverArea = ChatHitArea.None
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If Not Focused Then Focus()
        _downOnChrome = False
        If e.Button <> MouseButtons.Left Then Return
        If JumpButtonVisible() AndAlso JumpButtonRect().Contains(e.Location) Then
            _downOnChrome = True
            ScrollToBottom(True)
            Return
        End If
        If InScrollBarZone(e.Location) Then
            _downOnChrome = True
            Dim track, thumb As Rectangle
            If ScrollBarRects(track, thumb) Then
                Dim hitThumb = New Rectangle(track.X - S(4), thumb.Y, track.Width + S(8), thumb.Height)
                If hitThumb.Contains(e.Location) Then
                    _sbDrag = True
                    _sbDragOffset = e.Y - thumb.Y
                    Capture = True
                ElseIf e.Y < thumb.Y Then
                    ScrollBy(-(ClientSize.Height - S(40)))
                Else
                    ScrollBy(ClientSize.Height - S(40))
                End If
            End If
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        If _sbDrag Then
            _sbDrag = False
            Capture = False
            Invalidate()
        End If
        If e.Button = MouseButtons.Right Then ShowMessageMenu(e.Location)
    End Sub

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        MyBase.OnMouseClick(e)
        If _downOnChrome OrElse e.Button <> MouseButtons.Left Then Return
        Dim area As ChatHitArea
        Dim r = HitTest(e.Location, area)
        If r Is Nothing Then Return
        Dim args As New ChatMessageEventArgs(r.Message, area, e.Button)
        RaiseEvent MessageClick(Me, args)
        Select Case area
            Case ChatHitArea.Image
                OnImageClick(args)
            Case ChatHitArea.Attachment
                OnAttachmentClick(args)
            Case ChatHitArea.Status
                RaiseEvent RetryRequested(Me, args)
        End Select
    End Sub

    Protected Overrides Sub OnMouseDoubleClick(e As MouseEventArgs)
        MyBase.OnMouseDoubleClick(e)
        If _downOnChrome Then Return
        Dim area As ChatHitArea
        Dim r = HitTest(e.Location, area)
        If r IsNot Nothing Then RaiseEvent MessageDoubleClick(Me, New ChatMessageEventArgs(r.Message, area, e.Button))
    End Sub

    Protected Overridable Sub OnImageClick(e As ChatMessageEventArgs)
        RaiseEvent ImageClick(Me, e)
        If Not e.Handled AndAlso e.Message.Image IsNot Nothing Then
            ChatImageViewer.ShowImage(FindForm(), e.Message.Image, If(e.Message.Attachment Is Nothing, e.Message.Text, e.Message.Attachment.FileName))
        End If
    End Sub

    Protected Overridable Sub OnAttachmentClick(e As ChatMessageEventArgs)
        RaiseEvent AttachmentClick(Me, e)
        If e.Handled Then Return
        Dim a = e.Message.Attachment
        If a IsNot Nothing AndAlso Not String.IsNullOrEmpty(a.FilePath) AndAlso IO.File.Exists(a.FilePath) Then
            Try
                Process.Start(a.FilePath)
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message, a.FileName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
    End Sub

    Private Sub ShowMessageMenu(pt As Point)
        Dim area As ChatHitArea
        Dim r = HitTest(pt, area)
        If r Is Nothing Then Return
        Dim m = r.Message
        Dim menu As New ContextMenuStrip()
        If _defaultMenu Then
            If m.Text.Length > 0 Then
                menu.Items.Add(ChatStrings.CopyText, Nothing, Sub() Clipboard.SetText(m.Text))
            End If
            If m.Image IsNot Nothing Then
                menu.Items.Add(ChatStrings.CopyImage, Nothing, Sub() Clipboard.SetImage(m.Image))
            End If
            If m.Attachment IsNot Nothing AndAlso m.Image Is Nothing Then
                menu.Items.Add(ChatStrings.OpenAttachment, Nothing, Sub() OnAttachmentClick(New ChatMessageEventArgs(m, ChatHitArea.Attachment, MouseButtons.Left)))
            End If
            If m.Status = MessageStatus.Failed Then
                menu.Items.Add(ChatStrings.Retry, Nothing, Sub() RaiseEvent RetryRequested(Me, New ChatMessageEventArgs(m, ChatHitArea.Status, MouseButtons.Left)))
            End If
        End If
        Dim args As New ChatContextMenuEventArgs(m, area, menu)
        RaiseEvent MessageContextMenuOpening(Me, args)
        If args.Cancel OrElse menu.Items.Count = 0 Then
            menu.Dispose()
            Return
        End If
        AddHandler menu.Closed, Sub() BeginInvoke(New Action(AddressOf menu.Dispose))
        menu.Show(Me, pt)
    End Sub

#End Region

#Region "Reações a mudanças nos dados"

    Private Sub OnMessagesChanged(sender As Object, e As ChatCollectionChangedEventArgs(Of ChatMessage))
        If IsDisposed Then Return
        If InvokeRequired Then
            BeginInvoke(New Action(Sub() OnMessagesChanged(sender, e)))
            Return
        End If
        Dim wasBottom = _stickToBottom OrElse IsAtBottom
        Dim oldContent = _contentHeight
        Dim oldScroll = _scroll
        Dim count = If(_conversation Is Nothing, 0, _conversation.Messages.Count)

        Select Case e.Action
            Case ChatCollectionAction.Add
                If e.Index >= count - 1 Then
                    ' novo no fim
                    If wasBottom OrElse e.Item.Direction = MessageDirection.Outgoing Then
                        _stickToBottom = True
                    ElseIf e.Item.Direction = MessageDirection.Incoming Then
                        _pendingNew += 1
                    End If
                    _layoutDirty = True
                    EnsureLayout()
                Else
                    AnchorAfterInsertAbove(wasBottom, oldContent, oldScroll)
                End If
            Case ChatCollectionAction.Reset
                If e.Index = 0 Then
                    AnchorAfterInsertAbove(wasBottom, oldContent, oldScroll)
                Else
                    _stickToBottom = True
                    _layoutDirty = True
                    EnsureLayout()
                End If
            Case Else
                _stickToBottom = wasBottom
                _layoutDirty = True
                EnsureLayout()
        End Select
        Invalidate()
    End Sub

    Private Sub AnchorAfterInsertAbove(wasBottom As Boolean, oldContent As Integer, oldScroll As Integer)
        _stickToBottom = wasBottom
        _layoutDirty = True
        EnsureLayout()
        If Not wasBottom Then
            _scroll = Math.Max(0, Math.Min(oldScroll + (_contentHeight - oldContent), MaxScroll))
            _scrollTarget = _scroll
        End If
        _topRaised = False
    End Sub

    Private Sub OnMessageItemChanged(sender As Object, e As ChatItemChangedEventArgs(Of ChatMessage))
        If IsDisposed Then Return
        If InvokeRequired Then
            BeginInvoke(New Action(Sub() OnMessageItemChanged(sender, e)))
            Return
        End If
        ' o relayout é barato: o texto medido fica em cache em cada mensagem
        _layoutDirty = True
        If IsAtBottom Then _stickToBottom = True
        Invalidate()
    End Sub

    Private Sub OnConversationPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        If IsDisposed Then Return
        If InvokeRequired Then
            BeginInvoke(New Action(Sub() OnConversationPropertyChanged(sender, e)))
            Return
        End If
        Select Case e.PropertyName
            Case "IsTyping", "IsGroup", "TypingUser"
                If IsAtBottom Then _stickToBottom = True
                _layoutDirty = True
                UpdateTypingTimer()
                Invalidate()
            Case Else
                If e.PropertyName.StartsWith("Contact.") Then Invalidate()
        End Select
    End Sub

    Private Sub OnThemeChanged(sender As Object, e As EventArgs)
        If IsDisposed Then Return
        RebuildFonts()
        InvalidateLayout()
    End Sub

    Private Sub UpdateTypingTimer()
        Dim isOn = _conversation IsNot Nothing AndAlso _conversation.IsTyping AndAlso Visible
        If isOn Then _typingTimer.Start() Else _typingTimer.Stop()
    End Sub

    Private Sub OnTypingTick(sender As Object, e As EventArgs)
        _typingPhase += 1
        For i = _rows.Count - 1 To 0 Step -1
            If _rows(i).Kind = RowKind.Typing Then
                Dim rc = V(_rows(i).Bubble)
                rc.Inflate(S(2), S(6))
                Invalidate(rc)
                Exit For
            End If
        Next
    End Sub

    Protected Overrides Sub OnVisibleChanged(e As EventArgs)
        MyBase.OnVisibleChanged(e)
        UpdateTypingTimer()
    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        Dim sc = GetDpiScale(Me)
        If Math.Abs(sc - _scale) > 0.01 Then
            _scale = sc
            InvalidateLayout()
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)
        _layoutDirty = True
        Invalidate()
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            Conversation = Nothing
            RemoveHandler _theme.Changed, AddressOf OnThemeChanged
            _typingTimer.Dispose()
            _scrollTimer.Dispose()
            If _nameFont IsNot Nothing Then _nameFont.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

End Class
