Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Windows.Forms

''' <summary>Medidas calculadas de uma notificação (coordenadas relativas ao cartão).</summary>
Friend NotInheritable Class ToastLayout
    Public Scale As Single
    Public CardW As Integer
    Public CardH As Integer
    Public Radius As Integer
    Public HasIcon As Boolean
    Public IconRect As Rectangle
    Public CaptionRect As Rectangle
    Public TitleRect As Rectangle
    Public MessageRect As Rectangle
    Public CloseRect As Rectangle
    Public OkRect As Rectangle
    Public CaptionText As String
End Class

''' <summary>Desenho das notificações com GDI+ (sem estado).</summary>
Friend NotInheritable Class ToastRenderer
    Private Sub New()
    End Sub

    Private Const SingleFlags As TextFormatFlags = TextFormatFlags.SingleLine Or TextFormatFlags.NoPrefix Or TextFormatFlags.NoPadding Or TextFormatFlags.EndEllipsis
    Private Const MultiFlags As TextFormatFlags = TextFormatFlags.WordBreak Or TextFormatFlags.TextBoxControl Or TextFormatFlags.NoPrefix Or TextFormatFlags.NoPadding Or TextFormatFlags.EndEllipsis

    Private Shared ReadOnly Palette As Color() = {
        Color.FromArgb(229, 72, 77), Color.FromArgb(234, 120, 40), Color.FromArgb(202, 138, 4),
        Color.FromArgb(22, 163, 74), Color.FromArgb(13, 148, 136), Color.FromArgb(8, 145, 178),
        Color.FromArgb(37, 99, 235), Color.FromArgb(79, 70, 229), Color.FromArgb(147, 51, 234),
        Color.FromArgb(219, 39, 119)}

#Region "Utilitários"

    Public Shared Function ScreenScale() As Single
        Try
            Using g = Graphics.FromHwnd(IntPtr.Zero)
                Return Math.Max(1.0F, g.DpiX / 96.0F)
            End Using
        Catch
            Return 1.0F
        End Try
    End Function

    Public Shared Function Blend(a As Color, b As Color, amount As Double) As Color
        amount = Math.Max(0, Math.Min(1, amount))
        Return Color.FromArgb(255,
            CInt(a.R + (CInt(b.R) - a.R) * amount),
            CInt(a.G + (CInt(b.G) - a.G) * amount),
            CInt(a.B + (CInt(b.B) - a.B) * amount))
    End Function

    Public Shared Function ColorFromKey(key As String) As Color
        If String.IsNullOrEmpty(key) Then Return Palette(6)
        Dim h As UInteger = 2166136261UI
        For Each ch In key
            h = h Xor CUInt(Convert.ToInt32(ch))
            h = CUInt((CULng(h) * 16777619UL) And &HFFFFFFFFUL)
        Next
        Return Palette(CInt(h Mod CUInt(Palette.Length)))
    End Function

    Public Shared Function Initials(name As String) As String
        Dim parts = If(name, "").Split(New Char() {" "c, "."c, "_"c, "-"c}, StringSplitOptions.RemoveEmptyEntries)
        If parts.Length = 0 Then Return "?"
        If parts.Length = 1 Then Return parts(0).Substring(0, Math.Min(2, parts(0).Length)).ToUpperInvariant()
        Return (parts(0).Substring(0, 1) & parts(parts.Length - 1).Substring(0, 1)).ToUpperInvariant()
    End Function

    Public Shared Function RoundRect(r As RectangleF, radius As Single) As GraphicsPath
        Dim p As New GraphicsPath()
        radius = Math.Min(radius, Math.Min(r.Width, r.Height) / 2.0F)
        If radius <= 0.5F Then
            p.AddRectangle(r)
            Return p
        End If
        Dim d = radius * 2
        p.AddArc(r.X, r.Y, d, d, 180, 90)
        p.AddArc(r.Right - d, r.Y, d, d, 270, 90)
        p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90)
        p.AddArc(r.X, r.Bottom - d, d, d, 90, 90)
        p.CloseFigure()
        Return p
    End Function

    Public Shared Function AccentOf(t As Toast, th As ToastTheme) As Color
        If Not t.AccentColor.IsEmpty Then Return t.AccentColor
        If t.Type = ToastType.Message AndAlso t.Image Is Nothing Then Return ColorFromKey(t.Title)
        Return th.GetTypeColor(t.Type)
    End Function

#End Region

#Region "Medidas"

    Public Shared Function Measure(t As Toast, th As ToastTheme, scale As Single, appCaption As String) As ToastLayout
        Dim L As New ToastLayout() With {.Scale = scale}
        Dim s = Function(v As Integer) CInt(Math.Round(v * scale))
        Dim P = s(14)
        Dim icon = s(36)
        L.CardW = s(th.Width)
        L.Radius = s(th.CornerRadius)
        L.HasIcon = (t.Type <> ToastType.Plain) OrElse t.Image IsNot Nothing

        Dim textX = P + If(L.HasIcon, icon + s(12), 0)
        Dim titleW = L.CardW - textX - s(32)
        Dim msgW = L.CardW - textX - P

        Dim cap = If(String.IsNullOrEmpty(t.Caption), appCaption, t.Caption)
        L.CaptionText = If(String.IsNullOrEmpty(cap), Nothing, cap & "  ·  " & t.Timestamp.ToString("HH:mm"))

        Dim captionH = If(L.CaptionText Is Nothing, 0, TextRenderer.MeasureText("Ag", th.CaptionFont, Size.Empty, SingleFlags).Height)
        Dim titleH = If(t.Title.Length = 0, 0, TextRenderer.MeasureText("Ag", th.TitleFont, Size.Empty, SingleFlags).Height)
        Dim lineH = TextRenderer.MeasureText("Ag", th.TextFont, Size.Empty, SingleFlags).Height
        Dim msgH = 0
        If t.Message.Length > 0 Then
            msgH = TextRenderer.MeasureText(t.Message, th.TextFont, New Size(msgW, Integer.MaxValue), MultiFlags).Height
            msgH = Math.Min(msgH, lineH * th.MaxLines + s(1))
        End If

        Dim contentH = captionH + If(captionH > 0, s(3), 0) + titleH + If(titleH > 0 AndAlso msgH > 0, s(2), 0) + msgH
        Dim inner = Math.Max(If(L.HasIcon, icon, 0), contentH)
        L.CardH = Math.Max(s(48), P + inner + P)

        ' botão OK (check) no canto inferior direito, numa linha própria abaixo do texto
        If t.ShowOkButton Then
            Dim okTextW = TextRenderer.MeasureText(t.OkText, th.TitleFont, Size.Empty, SingleFlags).Width
            Dim okW = s(28) + okTextW + s(12)
            Dim okH = s(26)
            L.OkRect = New Rectangle(L.CardW - P - okW, P + inner + s(8), okW, okH)
            L.CardH = L.OkRect.Bottom + P
        End If

        Dim y = P + Math.Max(0, (If(L.HasIcon, icon, 0) - contentH) \ 2)
        If L.HasIcon Then L.IconRect = New Rectangle(P, P, icon, icon)
        If captionH > 0 Then
            L.CaptionRect = New Rectangle(textX, y, titleW, captionH)
            y += captionH + s(3)
        End If
        If titleH > 0 Then
            L.TitleRect = New Rectangle(textX, y, titleW, titleH)
            y += titleH + If(msgH > 0, s(2), 0)
        End If
        If msgH > 0 Then L.MessageRect = New Rectangle(textX, y, msgW, msgH)
        L.CloseRect = New Rectangle(L.CardW - s(30), s(9), s(22), s(22))
        Return L
    End Function

#End Region

#Region "Desenho"

    ''' <summary>Cartão (e sombra, se margin > 0) sem a barra de tempo. Bitmap ARGB do tamanho da janela.</summary>
    Public Shared Function RenderStatic(t As Toast, th As ToastTheme, L As ToastLayout, margin As Integer, hover As Boolean, hoverClose As Boolean, hoverOk As Boolean) As Bitmap
        ' 1) conteúdo num bitmap opaco: o texto sai com ClearType correto
        Dim content As New Bitmap(L.CardW, L.CardH, PixelFormat.Format24bppRgb)
        Using g = Graphics.FromImage(content)
            g.Clear(th.BackColor)
            g.SmoothingMode = SmoothingMode.AntiAlias
            If L.HasIcon Then DrawIcon(g, t, th, L)
            If L.CaptionText IsNot Nothing Then
                TextRenderer.DrawText(g, L.CaptionText, th.CaptionFont, L.CaptionRect, th.CaptionColor, th.BackColor, SingleFlags)
            End If
            If Not L.TitleRect.IsEmpty Then
                TextRenderer.DrawText(g, t.Title, th.TitleFont, L.TitleRect, th.TitleColor, th.BackColor, SingleFlags)
            End If
            If Not L.MessageRect.IsEmpty Then
                TextRenderer.DrawText(g, t.Message, th.TextFont, L.MessageRect, th.TextColor, th.BackColor, MultiFlags)
            End If
            If Not L.OkRect.IsEmpty Then DrawOkButton(g, t, th, L, hoverOk)
            If hover Then
                Dim cr = L.CloseRect
                If hoverClose Then
                    Using b As New SolidBrush(Blend(th.BackColor, th.TitleColor, 0.1))
                        g.FillEllipse(b, cr)
                    End Using
                End If
                Dim k = CSng(L.Scale)
                Dim x As RectangleF = cr
                x.Inflate(-7 * k, -7 * k)
                Using p As New Pen(If(hoverClose, th.TitleColor, th.CaptionColor), 1.5F * k)
                    p.StartCap = LineCap.Round : p.EndCap = LineCap.Round
                    g.DrawLine(p, x.Left, x.Top, x.Right, x.Bottom)
                    g.DrawLine(p, x.Left, x.Bottom, x.Right, x.Top)
                End Using
            End If
        End Using

        ' 2) composição com cantos arredondados e sombra (alpha por pixel)
        Dim full As New Bitmap(L.CardW + margin * 2, L.CardH + margin * 2, PixelFormat.Format32bppArgb)
        Using g = Graphics.FromImage(full)
            g.SmoothingMode = SmoothingMode.AntiAlias
            Dim card As New Rectangle(margin, margin, L.CardW, L.CardH)
            If margin > 0 AndAlso th.ShadowOpacity > 0 Then DrawShadow(g, card, L.Radius, margin, th, L.Scale)
            Using path = RoundRect(card, L.Radius), tb As New TextureBrush(content, WrapMode.Clamp)
                tb.TranslateTransform(margin, margin)
                g.FillPath(tb, path)
            End Using
            Using path = RoundRect(New RectangleF(card.X + 0.5F, card.Y + 0.5F, card.Width - 1, card.Height - 1), L.Radius), pen As New Pen(th.BorderColor)
                g.DrawPath(pen, path)
            End Using
        End Using
        content.Dispose()
        Return full
    End Function

    Private Shared Sub DrawShadow(g As Graphics, card As Rectangle, radius As Integer, margin As Integer, th As ToastTheme, scale As Single)
        ' camadas concêntricas com alpha baixo: aproximação barata de um desfoque
        Dim layers = Math.Max(1, margin)
        Dim a = 1 - Math.Pow(1 - th.ShadowOpacity, 1.0 / layers)
        Dim alpha = Math.Max(1, CInt(a * 255))
        Dim dy = CInt(Math.Round(3 * scale))
        Using b As New SolidBrush(Color.FromArgb(alpha, th.ShadowColor))
            For i = layers To 1 Step -1
                Dim r As New RectangleF(card.X - i + 1, card.Y - i + 1 + dy, card.Width + (i - 1) * 2, card.Height + (i - 1) * 2 - dy)
                Using p = RoundRect(r, radius + i)
                    g.FillPath(b, p)
                End Using
            Next
        End Using
    End Sub

    Private Shared Sub DrawOkButton(g As Graphics, t As Toast, th As ToastTheme, L As ToastLayout, hoverOk As Boolean)
        ' pílula na cor do tipo: contorno suave e, com o mouse em cima, fundo cheio
        Dim r = L.OkRect
        Dim k = L.Scale
        Dim accent = AccentOf(t, th)
        Dim fundo = If(hoverOk, accent, Blend(th.BackColor, accent, 0.12))
        Dim frente = If(hoverOk, Color.White, accent)
        Using path = RoundRect(New RectangleF(r.X, r.Y, r.Width - 1, r.Height - 1), r.Height / 2.0F)
            Using b As New SolidBrush(fundo)
                g.FillPath(b, path)
            End Using
            Using p As New Pen(Blend(th.BackColor, accent, 0.45), 1.0F)
                g.DrawPath(p, path)
            End Using
        End Using
        ' check
        Dim cx = r.X + 10 * k
        Dim cy = r.Y + r.Height / 2.0F
        Using p As New Pen(frente, 2.0F * k)
            p.StartCap = LineCap.Round : p.EndCap = LineCap.Round : p.LineJoin = LineJoin.Round
            g.DrawLines(p, New PointF() {New PointF(cx, cy), New PointF(cx + 3.5F * k, cy + 3.5F * k), New PointF(cx + 10 * k, cy - 4 * k)})
        End Using
        ' texto
        Dim tx = r.X + CInt(Math.Round(26 * k))
        Dim tr As New Rectangle(tx, r.Y, r.Right - tx - CInt(Math.Round(8 * k)), r.Height)
        TextRenderer.DrawText(g, t.OkText, th.TitleFont, tr, frente, fundo,
                              TextFormatFlags.VerticalCenter Or TextFormatFlags.HorizontalCenter Or SingleFlags)
    End Sub

    Private Shared Sub DrawIcon(g As Graphics, t As Toast, th As ToastTheme, L As ToastLayout)
        Dim r = L.IconRect
        Dim accent = AccentOf(t, th)
        Dim k = L.Scale

        If t.Type = ToastType.Message Then
            If t.Image IsNot Nothing Then
                DrawCircularImage(g, t.Image, r)
            Else
                Using b As New SolidBrush(accent)
                    g.FillEllipse(b, r)
                End Using
                Using f As New Font(th.TitleFont.FontFamily, r.Height * 0.36F, FontStyle.Bold, GraphicsUnit.Pixel)
                    TextRenderer.DrawText(g, Initials(t.Title), f, r, Color.White, accent,
                                          TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine Or TextFormatFlags.NoPadding Or TextFormatFlags.NoPrefix)
                End Using
            End If
            Return
        End If

        If t.Image IsNot Nothing Then
            Dim img = t.Image
            Dim f = Math.Min(r.Width / CDbl(img.Width), r.Height / CDbl(img.Height))
            Dim w = CInt(img.Width * f), h = CInt(img.Height * f)
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.DrawImage(img, New Rectangle(r.X + (r.Width - w) \ 2, r.Y + (r.Height - h) \ 2, w, h))
            Return
        End If

        Using b As New SolidBrush(Blend(th.BackColor, accent, 0.15))
            g.FillEllipse(b, r)
        End Using
        Dim cx = r.X + r.Width / 2.0F
        Dim cy = r.Y + r.Height / 2.0F
        Using p As New Pen(accent, 2.4F * k)
            p.StartCap = LineCap.Round : p.EndCap = LineCap.Round : p.LineJoin = LineJoin.Round
            Select Case t.Type
                Case ToastType.Success
                    g.DrawLines(p, New PointF() {New PointF(cx - 7 * k, cy + 0.5F * k), New PointF(cx - 2 * k, cy + 5.5F * k), New PointF(cx + 7.5F * k, cy - 5 * k)})
                Case ToastType.Error
                    g.DrawLine(p, cx - 6 * k, cy - 6 * k, cx + 6 * k, cy + 6 * k)
                    g.DrawLine(p, cx - 6 * k, cy + 6 * k, cx + 6 * k, cy - 6 * k)
                Case ToastType.Warning
                    g.DrawLine(p, cx, cy - 8 * k, cx, cy + 2 * k)
                    Using b As New SolidBrush(accent)
                        g.FillEllipse(b, cx - 1.7F * k, cy + 5 * k, 3.4F * k, 3.4F * k)
                    End Using
                Case Else ' Info
                    g.DrawLine(p, cx, cy - 2 * k, cx, cy + 8 * k)
                    Using b As New SolidBrush(accent)
                        g.FillEllipse(b, cx - 1.7F * k, cy - 8.5F * k, 3.4F * k, 3.4F * k)
                    End Using
            End Select
        End Using
    End Sub

    Private Shared Sub DrawCircularImage(g As Graphics, img As Image, r As Rectangle)
        Using bmp As New Bitmap(r.Width, r.Height, PixelFormat.Format32bppArgb)
            Using gb = Graphics.FromImage(bmp)
                gb.InterpolationMode = InterpolationMode.HighQualityBicubic
                gb.PixelOffsetMode = PixelOffsetMode.HighQuality
                Dim side = Math.Min(img.Width, img.Height)
                gb.DrawImage(img, New Rectangle(0, 0, r.Width, r.Height), New Rectangle((img.Width - side) \ 2, (img.Height - side) \ 2, side, side), GraphicsUnit.Pixel)
            End Using
            Using tb As New TextureBrush(bmp, WrapMode.Clamp)
                tb.TranslateTransform(r.X, r.Y)
                g.FillEllipse(tb, r)
            End Using
        End Using
    End Sub

    ''' <summary>Barra de tempo restante na base do cartão.</summary>
    Public Shared Sub DrawProgress(g As Graphics, card As Rectangle, radius As Integer, progress As Double, color As Color, scale As Single)
        If progress <= 0 Then Return
        Dim h = Math.Max(2, CInt(Math.Round(3 * scale)))
        Dim w = CInt(card.Width * Math.Min(1, progress))
        If w <= 0 Then Return
        Using clip = RoundRect(card, radius)
            Dim old = g.Clip
            g.SetClip(clip, CombineMode.Intersect)
            Using b As New SolidBrush(Color.FromArgb(210, color))
                g.FillRectangle(b, card.X, card.Bottom - h, w, h)
            End Using
            g.Clip = old
        End Using
    End Sub

#End Region

End Class
