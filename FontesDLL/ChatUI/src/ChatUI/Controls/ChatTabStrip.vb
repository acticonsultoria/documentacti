Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

''' <summary>Faixa de abas / lista de conversas desenhada com GDI+ (uso interno do ChatTabs).</summary>
Friend Class ChatTabStrip
    Inherits Control

    Private Structure Item
        Public Conv As ChatConversation
        Public Bounds As Rectangle
        Public CloseRect As Rectangle
    End Structure

    Private Const SingleFlags As TextFormatFlags = TextFormatFlags.SingleLine Or TextFormatFlags.NoPrefix Or TextFormatFlags.NoPadding Or TextFormatFlags.EndEllipsis

    Private ReadOnly _owner As ChatTabs
    Private ReadOnly _items As New List(Of Item)()
    Private ReadOnly _tip As New ToolTip()
    Private _theme As ChatTheme
    Private _scale As Single = 1.0F
    Private _offset As Integer
    Private _contentSize As Integer
    Private _overflowRect As Rectangle
    Private _listTop As Integer
    Private _hoverIndex As Integer = -1
    Private _hoverClose As Boolean
    Private _hoverOverflow As Boolean
    Private _pressIndex As Integer = -1

    Public Sub New(owner As ChatTabs)
        _owner = owner
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.Selectable, False)
        _theme = New ChatTheme()
    End Sub

    Public Property Theme As ChatTheme
        Get
            Return _theme
        End Get
        Set(value As ChatTheme)
            If value Is Nothing Then Return
            RemoveHandler _theme.Changed, AddressOf OnThemeChanged
            _theme = value
            AddHandler _theme.Changed, AddressOf OnThemeChanged
            Invalidate()
        End Set
    End Property

    Private Sub OnThemeChanged(sender As Object, e As EventArgs)
        Invalidate()
    End Sub

    Friend Function Scaled(v As Integer) As Integer
        Return CInt(Math.Round(v * _scale))
    End Function

    Private Function S(v As Integer) As Integer
        Return Scaled(v)
    End Function

    Private ReadOnly Property IsList As Boolean
        Get
            Return _owner.TabStyle = ChatTabStyle.List
        End Get
    End Property

    Friend Sub ResetScroll()
        _offset = 0
        _hoverIndex = -1
    End Sub

#Region "Layout"

    Private Function TabWidth(c As ChatConversation) As Integer
        Dim titleW = TextRenderer.MeasureText(c.Title, _theme.MessageFont, Size.Empty, SingleFlags).Width
        Dim badgeW = If(c.UnreadCount > 0, MeasureBadge(BadgeText(c), _theme.MetaFont, _scale).Width + S(6), 0)
        Dim closeW = If(_owner.ShowCloseButtons, S(24), 0)
        Dim w = S(10) + S(22) + S(8) + titleW + badgeW + closeW + S(10)
        Return Math.Max(S(120), Math.Min(S(240), w))
    End Function

    Private Function VisibleExtent() As Integer
        If IsList Then Return Height
        Return If(_overflowRect.IsEmpty, Width, _overflowRect.Left - S(4))
    End Function

    Private Sub BuildLayout()
        _items.Clear()
        Dim convs = _owner.Conversations
        If IsList Then
            _listTop = If(String.IsNullOrEmpty(_owner.ListTitle), S(8), S(54))
            Dim itemH = S(68)
            _contentSize = _listTop + convs.Count * itemH + S(8)
            _overflowRect = Rectangle.Empty
            _offset = Math.Max(0, Math.Min(_offset, _contentSize - Height))
            Dim y = _listTop - _offset
            For Each c In convs
                Dim b As New Rectangle(S(8), y, Width - S(16) - 1, itemH - S(2))
                Dim it As New Item() With {.Conv = c, .Bounds = b}
                If _owner.ShowCloseButtons Then it.CloseRect = New Rectangle(b.Right - S(26), b.Y + S(9), S(18), S(18))
                _items.Add(it)
                y += itemH
            Next
        Else
            Dim widths As New List(Of Integer)()
            Dim total = S(8)
            For Each c In convs
                Dim w = TabWidth(c)
                widths.Add(w)
                total += w + S(2)
            Next
            total += S(6)
            _contentSize = total
            Dim top = S(6)
            If total > Width Then
                _overflowRect = New Rectangle(Width - S(36), top + (Height - top - S(28)) \ 2, S(28), S(28))
            Else
                _overflowRect = Rectangle.Empty
            End If
            _offset = Math.Max(0, Math.Min(_offset, _contentSize - VisibleExtent()))
            Dim x = S(8) - _offset
            For i = 0 To convs.Count - 1
                Dim b As New Rectangle(x, top, widths(i), Height - top)
                Dim it As New Item() With {.Conv = convs(i), .Bounds = b}
                If _owner.ShowCloseButtons Then it.CloseRect = New Rectangle(b.Right - S(28), b.Y + (b.Height - S(18)) \ 2, S(18), S(18))
                _items.Add(it)
                x += widths(i) + S(2)
            Next
        End If
    End Sub

    Friend Sub EnsureVisible(conv As ChatConversation)
        If conv Is Nothing OrElse Width <= 0 OrElse Height <= 0 Then Return
        BuildLayout()
        For Each it In _items
            If Not Object.ReferenceEquals(it.Conv, conv) Then Continue For
            If IsList Then
                If it.Bounds.Y < _listTop Then
                    _offset -= _listTop - it.Bounds.Y
                ElseIf it.Bounds.Bottom > Height Then
                    _offset += it.Bounds.Bottom - Height + S(4)
                End If
            Else
                Dim ext = VisibleExtent()
                If it.Bounds.X < S(4) Then
                    _offset -= S(8) - it.Bounds.X
                ElseIf it.Bounds.Right > ext Then
                    _offset += it.Bounds.Right - ext + S(8)
                End If
            End If
            Exit For
        Next
        Invalidate()
    End Sub

    Private Shared Function BadgeText(c As ChatConversation) As String
        Return If(c.UnreadCount > 99, "99+", c.UnreadCount.ToString())
    End Function

#End Region

#Region "Pintura"

    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g = e.Graphics
        g.Clear(_theme.TabStripBackColor)
        BuildLayout()
        If IsList Then PaintList(g) Else PaintTabs(g)
    End Sub

    Private Sub PaintTabs(g As Graphics)
        Dim t = _theme
        Dim sel = _owner.SelectedConversation
        Dim ext = VisibleExtent()
        g.SetClip(New Rectangle(0, 0, ext, Height))
        For i = 0 To _items.Count - 1
            Dim it = _items(i)
            Dim b = it.Bounds
            If b.Right < 0 OrElse b.Left > ext Then Continue For
            Dim c = it.Conv
            Dim selected = Object.ReferenceEquals(c, sel)
            Dim hover = (i = _hoverIndex)
            g.SmoothingMode = SmoothingMode.AntiAlias
            If selected Then
                Using p = RoundRect(New RectangleF(b.X, b.Y, b.Width, b.Height + 2), S(8), S(8), 0, 0), br As New SolidBrush(t.TabSelectedBackColor)
                    g.FillPath(br, p)
                End Using
            ElseIf hover Then
                FillRound(g, t.TabHoverBackColor, New Rectangle(b.X, b.Y + S(2), b.Width, b.Height - S(6)), S(8))
            End If

            Dim av = S(22)
            Dim avRect As New Rectangle(b.X + S(10), b.Y + (b.Height - av) \ 2, av, av)
            DrawAvatar(g, c.Contact, avRect)
            If c.Contact IsNot Nothing AndAlso c.Contact.IsOnline AndAlso Not c.IsGroup Then
                DrawOnlineDot(g, avRect, t.OnlineColor, If(selected, t.TabSelectedBackColor, If(hover, t.TabHoverBackColor, t.TabStripBackColor)))
            End If

            Dim right = b.Right - S(10)
            If Not it.CloseRect.IsEmpty Then right = it.CloseRect.Left - S(4)
            If c.UnreadCount > 0 Then
                Dim txt = BadgeText(c)
                Dim bs = MeasureBadge(txt, t.MetaFont, _scale)
                Dim br As New Rectangle(right - bs.Width, b.Y + (b.Height - bs.Height) \ 2, bs.Width, bs.Height)
                DrawBadge(g, br, txt, t.AccentColor, Color.White, t.MetaFont)
                right = br.Left - S(6)
            End If
            Dim textColor = If(c.IsTyping, t.AccentColor, If(selected, t.TabTextColor, Blend(t.TabTextColor, t.TabSubTextColor, 0.35)))
            Dim tx = avRect.Right + S(8)
            TextRenderer.DrawText(g, c.Title, t.MessageFont, New Rectangle(tx, b.Y, Math.Max(0, right - tx), b.Height), textColor,
                                  SingleFlags Or TextFormatFlags.VerticalCenter)

            If Not it.CloseRect.IsEmpty AndAlso (selected OrElse hover) Then
                Dim cr = it.CloseRect
                If hover AndAlso _hoverClose Then
                    Using br As New SolidBrush(Blend(If(selected, t.TabSelectedBackColor, t.TabHoverBackColor), t.TabTextColor, 0.12))
                        g.FillEllipse(br, cr)
                    End Using
                End If
                Dim x = Rectangle.Inflate(cr, -S(5), -S(5))
                DrawCross(g, x, t.TabSubTextColor, 1.4F * _scale)
            End If
        Next
        g.ResetClip()

        If Not _overflowRect.IsEmpty Then
            g.SmoothingMode = SmoothingMode.AntiAlias
            If _hoverOverflow Then
                Using br As New SolidBrush(t.TabHoverBackColor)
                    g.FillEllipse(br, _overflowRect)
                End Using
            End If
            Dim cx = _overflowRect.X + _overflowRect.Width / 2.0F
            Dim cy = _overflowRect.Y + _overflowRect.Height / 2.0F + S(1)
            DrawChevronDown(g, New PointF(cx, cy), S(5), t.TabSubTextColor, 1.8F * _scale)
            ' ponto indicando não lidas escondidas
            Dim hiddenUnread = False
            For Each it In _items
                If it.Conv.UnreadCount > 0 AndAlso (it.Bounds.Right > VisibleExtent() OrElse it.Bounds.Left < 0) Then hiddenUnread = True
            Next
            If hiddenUnread Then
                Using br As New SolidBrush(t.AccentColor)
                    g.FillEllipse(br, _overflowRect.Right - S(8), _overflowRect.Y + S(2), S(7), S(7))
                End Using
            End If
        End If
    End Sub

    Private Function TimeLabel(d As DateTime) As String
        Dim today = Date.Today
        If d.Date = today Then Return d.ToString(ChatStrings.TimeFormat)
        If d.Date = today.AddDays(-1) Then Return ChatStrings.Yesterday
        If d.Date > today.AddDays(-7) Then Return d.ToString("ddd")
        Return d.ToString("dd/MM/yy")
    End Function

    Private Shared Function OneLine(s As String) As String
        If String.IsNullOrEmpty(s) Then Return String.Empty
        Return s.Replace(Convert.ToChar(13), " "c).Replace(Convert.ToChar(10), " "c)
    End Function

    Private Function PreviewText(c As ChatConversation, last As ChatMessage) As String
        If last Is Nothing Then Return String.Empty
        Dim body As String
        If last.Image IsNot Nothing Then
            body = If(last.Text.Length > 0, ChatStrings.PhotoPreview & ": " & OneLine(last.Text), ChatStrings.PhotoPreview)
        ElseIf last.Attachment IsNot Nothing Then
            body = String.Format(ChatStrings.FilePreview, last.Attachment.FileName)
        Else
            body = OneLine(last.Text)
        End If
        If c.IsGroup AndAlso last.Direction = MessageDirection.Incoming AndAlso last.Sender IsNot Nothing Then
            body = last.Sender.DisplayName.Split(" "c)(0) & ": " & body
        End If
        Return body
    End Function

    Private Sub PaintList(g As Graphics)
        Dim t = _theme
        Dim sel = _owner.SelectedConversation
        Using p As New Pen(t.BorderColor)
            g.DrawLine(p, Width - 1, 0, Width - 1, Height)
        End Using
        If Not String.IsNullOrEmpty(_owner.ListTitle) Then
            Using f As New Font(t.TitleFont.FontFamily, t.TitleFont.Size + 2.0F, FontStyle.Bold)
                TextRenderer.DrawText(g, _owner.ListTitle, f, New Rectangle(S(18), 0, Width - S(36), _listTop), t.TabTextColor,
                                      SingleFlags Or TextFormatFlags.VerticalCenter)
            End Using
        End If
        g.SetClip(New Rectangle(0, _listTop, Width - 1, Height - _listTop))
        Dim metaH = TextRenderer.MeasureText("Ag", t.MetaFont, Size.Empty, SingleFlags).Height
        Dim titleH = TextRenderer.MeasureText("Ag", t.TitleFont, Size.Empty, SingleFlags).Height
        Dim lineH = TextRenderer.MeasureText("Ag", t.MessageFont, Size.Empty, SingleFlags).Height
        For i = 0 To _items.Count - 1
            Dim it = _items(i)
            Dim b = it.Bounds
            If b.Bottom < _listTop OrElse b.Top > Height Then Continue For
            Dim c = it.Conv
            Dim selected = Object.ReferenceEquals(c, sel)
            Dim hover = (i = _hoverIndex)
            g.SmoothingMode = SmoothingMode.AntiAlias
            If selected Then
                FillRound(g, t.TabSelectedBackColor, b, S(10))
            ElseIf hover Then
                FillRound(g, t.TabHoverBackColor, b, S(10))
            End If
            Dim itemBack = If(selected, t.TabSelectedBackColor, If(hover, t.TabHoverBackColor, t.TabStripBackColor))

            Dim av = S(46)
            Dim avRect As New Rectangle(b.X + S(10), b.Y + (b.Height - av) \ 2, av, av)
            DrawAvatar(g, c.Contact, avRect)
            If c.Contact IsNot Nothing AndAlso c.Contact.IsOnline AndAlso Not c.IsGroup Then DrawOnlineDot(g, avRect, t.OnlineColor, itemBack)

            Dim tx = avRect.Right + S(12)
            Dim right = b.Right - S(12)
            Dim y1 = b.Y + (b.Height - titleH - lineH - S(4)) \ 2
            Dim y2 = y1 + titleH + S(4)
            Dim last = c.Messages.Last

            ' linha 1: título + hora (ou X ao passar o mouse)
            Dim titleRight = right
            If hover AndAlso Not it.CloseRect.IsEmpty Then
                Dim cr = it.CloseRect
                cr.Y = y1 + (titleH - cr.Height) \ 2
                it.CloseRect = cr
                _items(i) = it
                If _hoverClose Then
                    Using br As New SolidBrush(Blend(itemBack, t.TabTextColor, 0.12))
                        g.FillEllipse(br, cr)
                    End Using
                End If
                DrawCross(g, Rectangle.Inflate(cr, -S(5), -S(5)), t.TabSubTextColor, 1.4F * _scale)
                titleRight = cr.Left - S(6)
            ElseIf last IsNot Nothing Then
                Dim tl = TimeLabel(last.Timestamp)
                Dim tw = TextRenderer.MeasureText(tl, t.MetaFont, Size.Empty, SingleFlags).Width
                TextRenderer.DrawText(g, tl, t.MetaFont, New Rectangle(right - tw, y1 + (titleH - metaH) \ 2, tw + 2, metaH),
                                      If(c.UnreadCount > 0, t.AccentColor, t.TabSubTextColor), SingleFlags)
                titleRight = right - tw - S(8)
            End If
            TextRenderer.DrawText(g, c.Title, t.TitleFont, New Rectangle(tx, y1, Math.Max(0, titleRight - tx), titleH), t.TabTextColor, SingleFlags)

            ' linha 2: prévia + não lidas
            Dim prevRight = right
            If c.UnreadCount > 0 Then
                Dim txt = BadgeText(c)
                Dim bs = MeasureBadge(txt, t.MetaFont, _scale)
                Dim br As New Rectangle(right - bs.Width, y2 + (lineH - bs.Height) \ 2, bs.Width, bs.Height)
                DrawBadge(g, br, txt, t.AccentColor, Color.White, t.MetaFont)
                prevRight = br.Left - S(8)
            End If
            Dim px = tx
            If c.IsTyping Then
                TextRenderer.DrawText(g, ChatStrings.Typing, t.MessageFont, New Rectangle(px, y2, Math.Max(0, prevRight - px), lineH), t.AccentColor, SingleFlags)
            ElseIf last IsNot Nothing Then
                If last.Direction = MessageDirection.Outgoing AndAlso last.Status <> MessageStatus.None Then
                    Dim sw = StatusIconWidth(last.Status, _scale)
                    DrawStatus(g, last.Status, px, y2 + lineH / 2.0F, t.TabSubTextColor, t.AccentColor, t.FailedColor, _scale)
                    px += sw + S(4)
                End If
                TextRenderer.DrawText(g, PreviewText(c, last), t.MessageFont, New Rectangle(px, y2, Math.Max(0, prevRight - px), lineH),
                                      If(c.UnreadCount > 0, t.TabTextColor, t.TabSubTextColor), SingleFlags)
            End If
        Next
        g.ResetClip()

        ' indicador de rolagem
        If _contentSize > Height Then
            Dim trackH = Height - _listTop - S(8)
            Dim thH = Math.Max(S(24), CInt(trackH * (Height / CDbl(_contentSize))))
            Dim thY = _listTop + CInt((trackH - thH) * (_offset / CDbl(Math.Max(1, _contentSize - Height))))
            g.SmoothingMode = SmoothingMode.AntiAlias
            FillRound(g, Color.FromArgb(70, t.TabSubTextColor), New Rectangle(Width - S(6), thY, S(3), thH), S(2))
        End If
    End Sub

#End Region

#Region "Mouse"

    Private Function HitTest(pt As Point, ByRef onClose As Boolean) As Integer
        onClose = False
        If IsList AndAlso pt.Y < _listTop Then Return -1
        If Not IsList AndAlso pt.X > VisibleExtent() Then Return -1
        For i = 0 To _items.Count - 1
            Dim it = _items(i)
            If it.Bounds.Contains(pt) Then
                If Not it.CloseRect.IsEmpty Then
                    Dim cr = it.CloseRect
                    If IsList Then
                        ' no modo lista o X fica na linha do título
                        Dim titleH = TextRenderer.MeasureText("Ag", _theme.TitleFont, Size.Empty, SingleFlags).Height
                        Dim lineH = TextRenderer.MeasureText("Ag", _theme.MessageFont, Size.Empty, SingleFlags).Height
                        Dim y1 = it.Bounds.Y + (it.Bounds.Height - titleH - lineH - S(4)) \ 2
                        cr.Y = y1 + (titleH - cr.Height) \ 2
                    End If
                    onClose = Rectangle.Inflate(cr, S(2), S(2)).Contains(pt)
                End If
                Return i
            End If
        Next
        Return -1
    End Function

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        BuildLayout()
        Dim onClose As Boolean
        Dim idx = HitTest(e.Location, onClose)
        Dim ov = Not _overflowRect.IsEmpty AndAlso _overflowRect.Contains(e.Location)
        If idx <> _hoverIndex OrElse onClose <> _hoverClose OrElse ov <> _hoverOverflow Then
            If idx <> _hoverIndex Then
                _tip.SetToolTip(Me, If(idx >= 0, _items(idx).Conv.Title, Nothing))
            End If
            _hoverIndex = idx
            _hoverClose = onClose AndAlso _owner.ShowCloseButtons
            _hoverOverflow = ov
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        _hoverIndex = -1 : _hoverClose = False : _hoverOverflow = False
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        BuildLayout()
        If e.Button = MouseButtons.Left AndAlso Not _overflowRect.IsEmpty AndAlso _overflowRect.Contains(e.Location) Then
            ShowOverflowMenu()
            Return
        End If
        Dim onClose As Boolean
        _pressIndex = HitTest(e.Location, onClose)
        If _pressIndex < 0 Then Return
        If e.Button = MouseButtons.Left AndAlso Not (onClose AndAlso _owner.ShowCloseButtons) Then
            _owner.SelectedConversation = _items(_pressIndex).Conv
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        BuildLayout()
        Dim onClose As Boolean
        Dim idx = HitTest(e.Location, onClose)
        If idx >= 0 AndAlso idx = _pressIndex AndAlso _owner.ShowCloseButtons Then
            If (e.Button = MouseButtons.Left AndAlso onClose) OrElse e.Button = MouseButtons.Middle Then
                _owner.CloseConversation(_items(idx).Conv)
                _hoverIndex = -1
            End If
        End If
        _pressIndex = -1
    End Sub

    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        MyBase.OnMouseWheel(e)
        _offset -= CInt(e.Delta / 120.0 * S(60))
        If _offset < 0 Then _offset = 0
        Invalidate()
    End Sub

    Private Sub ShowOverflowMenu()
        Dim menu As New ContextMenuStrip()
        For Each c In _owner.Conversations
            Dim conv = c
            Dim text = If(conv.UnreadCount > 0, String.Format("{0}  ({1})", conv.Title, conv.UnreadCount), conv.Title)
            Dim mi = New ToolStripMenuItem(text, Nothing, Sub() _owner.SelectedConversation = conv)
            mi.Checked = Object.ReferenceEquals(conv, _owner.SelectedConversation)
            menu.Items.Add(mi)
        Next
        AddHandler menu.Closed, Sub() BeginInvoke(New Action(AddressOf menu.Dispose))
        menu.Show(Me, New Point(_overflowRect.Left, _overflowRect.Bottom))
    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        Dim sc = GetDpiScale(Me)
        If Math.Abs(sc - _scale) > 0.01 Then
            _scale = sc
            If IsList Then Width = S(_owner.ListWidth) Else Height = S(42)
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)
        If _owner.SelectedConversation IsNot Nothing Then EnsureVisible(_owner.SelectedConversation)
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            RemoveHandler _theme.Changed, AddressOf OnThemeChanged
            _tip.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

End Class
