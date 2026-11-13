Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

''' <summary>
''' Cabeçalho da conversa: avatar, nome, "online"/"digitando...".
''' Aceita controles filhos (ex.: botões ancorados à direita).
''' </summary>
<ToolboxItem(True)>
Public Class ChatHeader
    Inherits Control

    Private _conversation As ChatConversation
    Private _theme As ChatTheme
    Private _scale As Single = 1.0F

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.Selectable, False)
        _theme = New ChatTheme()
        AddHandler _theme.Changed, AddressOf OnThemeChanged
        Height = 56
    End Sub

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Conversation As ChatConversation
        Get
            Return _conversation
        End Get
        Set(value As ChatConversation)
            If Object.ReferenceEquals(value, _conversation) Then Return
            If _conversation IsNot Nothing Then RemoveHandler _conversation.PropertyChanged, AddressOf OnConversationChanged
            _conversation = value
            If _conversation IsNot Nothing Then AddHandler _conversation.PropertyChanged, AddressOf OnConversationChanged
            Invalidate()
        End Set
    End Property

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
            Invalidate()
        End Set
    End Property

    ''' <summary>Texto da segunda linha calculado a partir do estado da conversa.</summary>
    <Browsable(False)>
    Public ReadOnly Property SubtitleText As String
        Get
            If _conversation Is Nothing Then Return String.Empty
            If _conversation.IsTyping Then
                Dim tu = _conversation.TypingUser
                If _conversation.IsGroup AndAlso tu IsNot Nothing AndAlso Not String.IsNullOrEmpty(tu.DisplayName) Then
                    Return String.Format(ChatStrings.TypingNamed, tu.DisplayName)
                End If
                Return ChatStrings.Typing
            End If
            Dim c = _conversation.Contact
            If c Is Nothing Then Return String.Empty
            If Not String.IsNullOrEmpty(c.StatusText) AndAlso (Not c.IsOnline OrElse _conversation.IsGroup) Then Return c.StatusText
            If c.IsOnline AndAlso Not _conversation.IsGroup Then Return ChatStrings.Online
            Return String.Empty
        End Get
    End Property

    Private Function S(v As Integer) As Integer
        Return CInt(Math.Round(v * _scale))
    End Function

    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g = e.Graphics
        Dim t = _theme
        g.Clear(t.HeaderBackColor)
        Using p As New Pen(t.BorderColor)
            g.DrawLine(p, 0, Height - 1, Width, Height - 1)
        End Using
        If _conversation Is Nothing Then Return

        Dim av = S(38)
        Dim avRect As New Rectangle(S(14), (Height - av) \ 2, av, av)
        DrawAvatar(g, _conversation.Contact, avRect)
        If _conversation.Contact IsNot Nothing AndAlso _conversation.Contact.IsOnline AndAlso Not _conversation.IsGroup Then
            DrawOnlineDot(g, avRect, t.OnlineColor, t.HeaderBackColor)
        End If

        Dim x = avRect.Right + S(12)
        Dim rightLimit = Width - S(12)
        For Each c As Control In Controls
            If c.Visible Then rightLimit = Math.Min(rightLimit, c.Left - S(8))
        Next
        Dim w = Math.Max(10, rightLimit - x)
        Dim title = _conversation.Title
        Dim sub_ = SubtitleText
        Dim flags = TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis Or TextFormatFlags.NoPrefix Or TextFormatFlags.NoPadding
        Dim th = TextRenderer.MeasureText("Ag", t.TitleFont, Size.Empty, flags).Height
        If sub_.Length = 0 Then
            TextRenderer.DrawText(g, title, t.TitleFont, New Rectangle(x, (Height - th) \ 2, w, th), t.HeaderTextColor, flags)
        Else
            Dim sh = TextRenderer.MeasureText("Ag", t.MetaFont, Size.Empty, flags).Height
            Dim y0 = (Height - th - sh - S(1)) \ 2
            TextRenderer.DrawText(g, title, t.TitleFont, New Rectangle(x, y0, w, th), t.HeaderTextColor, flags)
            Dim subColor = If(_conversation.IsTyping, t.AccentColor, If(_conversation.Contact IsNot Nothing AndAlso _conversation.Contact.IsOnline AndAlso Not _conversation.IsGroup, t.OnlineColor, t.HeaderSubTextColor))
            TextRenderer.DrawText(g, sub_, t.MetaFont, New Rectangle(x, y0 + th + S(1), w, sh), subColor, flags)
        End If
    End Sub

    Private Sub OnConversationChanged(sender As Object, e As PropertyChangedEventArgs)
        If IsDisposed Then Return
        If InvokeRequired Then
            BeginInvoke(New Action(AddressOf Invalidate))
        Else
            Invalidate()
        End If
    End Sub

    Private Sub OnThemeChanged(sender As Object, e As EventArgs)
        Invalidate()
    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        _scale = GetDpiScale(Me)
        Height = S(56)
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            Conversation = Nothing
            RemoveHandler _theme.Changed, AddressOf OnThemeChanged
        End If
        MyBase.Dispose(disposing)
    End Sub
End Class
