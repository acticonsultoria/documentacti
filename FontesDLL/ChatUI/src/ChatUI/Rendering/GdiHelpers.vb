Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

''' <summary>Rotinas de desenho GDI/GDI+ compartilhadas pelos controles.</summary>
Friend Module GdiHelpers

    Private ReadOnly Palette As Color() = {
        Color.FromArgb(229, 72, 77), Color.FromArgb(234, 120, 40), Color.FromArgb(202, 138, 4),
        Color.FromArgb(22, 163, 74), Color.FromArgb(13, 148, 136), Color.FromArgb(8, 145, 178),
        Color.FromArgb(37, 99, 235), Color.FromArgb(79, 70, 229), Color.FromArgb(147, 51, 234),
        Color.FromArgb(219, 39, 119)}

    Private ReadOnly InitialFonts As New Dictionary(Of Integer, Font)()

    ''' <summary>Cor estável a partir de um texto (FNV-1a, igual em qualquer máquina).</summary>
    Public Function ColorFromKey(key As String) As Color
        If String.IsNullOrEmpty(key) Then Return Palette(0)
        Dim h As UInteger = 2166136261UI
        For Each ch In key
            h = h Xor CUInt(Convert.ToInt32(ch))
            h = CUInt((CULng(h) * 16777619UL) And &HFFFFFFFFUL)
        Next
        Return Palette(CInt(h Mod CUInt(Palette.Length)))
    End Function

    Public Function Blend(a As Color, b As Color, amount As Double) As Color
        amount = Math.Max(0, Math.Min(1, amount))
        Return Color.FromArgb(
            CInt(a.A + (CInt(b.A) - a.A) * amount),
            CInt(a.R + (CInt(b.R) - a.R) * amount),
            CInt(a.G + (CInt(b.G) - a.G) * amount),
            CInt(a.B + (CInt(b.B) - a.B) * amount))
    End Function

    Public Function IsDark(c As Color) As Boolean
        Return (0.299 * c.R + 0.587 * c.G + 0.114 * c.B) < 128
    End Function

    ''' <summary>Retângulo arredondado com raio independente em cada canto.</summary>
    Public Function RoundRect(r As RectangleF, tl As Single, tr As Single, br As Single, bl As Single) As GraphicsPath
        Dim p As New GraphicsPath()
        Dim maxR = Math.Min(r.Width, r.Height) / 2.0F
        tl = Math.Min(tl, maxR) : tr = Math.Min(tr, maxR) : br = Math.Min(br, maxR) : bl = Math.Min(bl, maxR)
        If tl > 0 Then p.AddArc(r.X, r.Y, tl * 2, tl * 2, 180, 90) Else p.AddLine(r.X, r.Y, r.X, r.Y)
        If tr > 0 Then p.AddArc(r.Right - tr * 2, r.Y, tr * 2, tr * 2, 270, 90) Else p.AddLine(r.Right, r.Y, r.Right, r.Y)
        If br > 0 Then p.AddArc(r.Right - br * 2, r.Bottom - br * 2, br * 2, br * 2, 0, 90) Else p.AddLine(r.Right, r.Bottom, r.Right, r.Bottom)
        If bl > 0 Then p.AddArc(r.X, r.Bottom - bl * 2, bl * 2, bl * 2, 90, 90) Else p.AddLine(r.X, r.Bottom, r.X, r.Bottom)
        p.CloseFigure()
        Return p
    End Function

    Public Function RoundRect(r As RectangleF, radius As Single) As GraphicsPath
        Return RoundRect(r, radius, radius, radius, radius)
    End Function

    Public Sub FillRound(g As Graphics, color As Color, r As Rectangle, radius As Single)
        Using p = RoundRect(r, radius), b As New SolidBrush(color)
            g.FillPath(b, p)
        End Using
    End Sub

    Private Function InitialsFont(px As Integer) As Font
        Dim f As Font = Nothing
        If Not InitialFonts.TryGetValue(px, f) Then
            f = New Font("Segoe UI Semibold", Math.Max(6, px), FontStyle.Regular, GraphicsUnit.Pixel)
            InitialFonts(px) = f
        End If
        Return f
    End Function

    ''' <summary>Avatar circular: foto (recortada em círculo com antialias) ou iniciais.</summary>
    Public Sub DrawAvatar(g As Graphics, user As ChatUser, rect As Rectangle)
        Dim old = g.SmoothingMode
        g.SmoothingMode = SmoothingMode.AntiAlias
        If user IsNot Nothing AndAlso user.Avatar IsNot Nothing Then
            Dim bmp = GetAvatarBitmap(user, rect.Width)
            Using tb As New TextureBrush(bmp, WrapMode.Clamp)
                tb.TranslateTransform(rect.X, rect.Y)
                g.FillEllipse(tb, rect)
            End Using
        Else
            Dim c = If(user Is Nothing, Color.Gray, user.EffectiveColor)
            Using b As New SolidBrush(c)
                g.FillEllipse(b, rect)
            End Using
            Dim txt = If(user Is Nothing, "?", user.Initials)
            TextRenderer.DrawText(g, txt, InitialsFont(CInt(rect.Height * 0.38)), rect, Color.White,
                                  TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPadding Or TextFormatFlags.SingleLine Or TextFormatFlags.NoPrefix)
        End If
        g.SmoothingMode = old
    End Sub

    Private Function GetAvatarBitmap(user As ChatUser, size As Integer) As Bitmap
        If user.AvatarCache IsNot Nothing AndAlso user.AvatarCacheSize = size Then Return user.AvatarCache
        user.ClearAvatarCache()
        Dim bmp As New Bitmap(size, size, Imaging.PixelFormat.Format32bppPArgb)
        Using g = Graphics.FromImage(bmp)
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.PixelOffsetMode = PixelOffsetMode.HighQuality
            Dim src = user.Avatar
            ' "cover": recorta o centro em quadrado
            Dim side = Math.Min(src.Width, src.Height)
            Dim srcRect As New Rectangle((src.Width - side) \ 2, (src.Height - side) \ 2, side, side)
            g.DrawImage(src, New Rectangle(0, 0, size, size), srcRect, GraphicsUnit.Pixel)
        End Using
        user.AvatarCache = bmp
        user.AvatarCacheSize = size
        Return bmp
    End Function

    Public Sub DrawOnlineDot(g As Graphics, avatar As Rectangle, dotColor As Color, ringColor As Color)
        Dim d = Math.Max(8, CInt(avatar.Width * 0.3))
        Dim r As New Rectangle(avatar.Right - d + 1, avatar.Bottom - d + 1, d, d)
        Dim old = g.SmoothingMode
        g.SmoothingMode = SmoothingMode.AntiAlias
        Using b As New SolidBrush(ringColor)
            g.FillEllipse(b, r)
        End Using
        r.Inflate(-2, -2)
        Using b As New SolidBrush(dotColor)
            g.FillEllipse(b, r)
        End Using
        g.SmoothingMode = old
    End Sub

    ''' <summary>Largura reservada para o ícone de status.</summary>
    Public Function StatusIconWidth(status As MessageStatus, scale As Single) As Integer
        Select Case status
            Case MessageStatus.None : Return 0
            Case MessageStatus.Delivered, MessageStatus.Read : Return CInt(16 * scale)
            Case Else : Return CInt(13 * scale)
        End Select
    End Function

    ''' <summary>Relógio, ✓, ✓✓, ✓✓ (lida) ou ! (falha). x = borda esquerda, cy = centro vertical.</summary>
    Public Sub DrawStatus(g As Graphics, status As MessageStatus, x As Single, cy As Single, color As Color, readColor As Color, failedColor As Color, scale As Single)
        If status = MessageStatus.None Then Return
        Dim old = g.SmoothingMode
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim s = scale
        Select Case status
            Case MessageStatus.Sending
                Using p As New Pen(color, 1.2F * s)
                    g.DrawEllipse(p, x + 1 * s, cy - 5 * s, 10 * s, 10 * s)
                    g.DrawLine(p, x + 6 * s, cy - 3 * s, x + 6 * s, cy)
                    g.DrawLine(p, x + 6 * s, cy, x + 8.5F * s, cy + 1.5F * s)
                End Using
            Case MessageStatus.Sent
                DrawCheck(g, x + 1 * s, cy, color, s)
            Case MessageStatus.Delivered
                DrawCheck(g, x, cy, color, s)
                DrawCheck(g, x + 5 * s, cy, color, s)
            Case MessageStatus.Read
                DrawCheck(g, x, cy, readColor, s)
                DrawCheck(g, x + 5 * s, cy, readColor, s)
            Case MessageStatus.Failed
                Using b As New SolidBrush(failedColor)
                    g.FillEllipse(b, x, cy - 6 * s, 12 * s, 12 * s)
                End Using
                Using p As New Pen(Color.White, 1.6F * s)
                    p.StartCap = LineCap.Round : p.EndCap = LineCap.Round
                    g.DrawLine(p, x + 6 * s, cy - 3 * s, x + 6 * s, cy + 0.8F * s)
                    g.DrawLine(p, x + 6 * s, cy + 3 * s, x + 6 * s, cy + 3.1F * s)
                End Using
        End Select
        g.SmoothingMode = old
    End Sub

    Private Sub DrawCheck(g As Graphics, x As Single, cy As Single, color As Color, s As Single)
        Using p As New Pen(color, 1.5F * s)
            p.StartCap = LineCap.Round : p.EndCap = LineCap.Round : p.LineJoin = LineJoin.Round
            g.DrawLines(p, New PointF() {New PointF(x, cy), New PointF(x + 3 * s, cy + 3 * s), New PointF(x + 10 * s, cy - 4 * s)})
        End Using
    End Sub

    ''' <summary>Ícone de documento com a extensão escrita.</summary>
    Public Sub DrawFileIcon(g As Graphics, r As Rectangle, ext As String, color As Color, font As Font)
        Dim old = g.SmoothingMode
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim fold = r.Width * 0.3F
        Using p As New GraphicsPath()
            p.AddLine(r.X, r.Y, r.Right - fold, r.Y)
            p.AddLine(r.Right - fold, r.Y, r.Right, r.Y + fold)
            p.AddLine(r.Right, r.Y + fold, r.Right, r.Bottom)
            p.AddLine(r.Right, r.Bottom, r.X, r.Bottom)
            p.CloseFigure()
            Using b As New SolidBrush(color)
                g.FillPath(b, p)
            End Using
        End Using
        Using b As New SolidBrush(Color.FromArgb(70, Color.White))
            g.FillPolygon(b, New PointF() {New PointF(r.Right - fold, r.Y), New PointF(r.Right - fold, r.Y + fold), New PointF(r.Right, r.Y + fold)})
        End Using
        g.SmoothingMode = old
        If Not String.IsNullOrEmpty(ext) Then
            If ext.Length > 4 Then ext = ext.Substring(0, 4)
            Dim tr As New Rectangle(r.X, r.Y + r.Height \ 3, r.Width, r.Height - r.Height \ 3)
            TextRenderer.DrawText(g, ext, font, tr, Color.White,
                                  TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPadding Or TextFormatFlags.SingleLine Or TextFormatFlags.NoPrefix)
        End If
    End Sub

    ''' <summary>Pílula com número (não lidas).</summary>
    Public Function MeasureBadge(text As String, font As Font, scale As Single) As Size
        Dim sz = TextRenderer.MeasureText(text, font, Size.Empty, TextFormatFlags.NoPadding)
        Dim h = CInt(18 * scale)
        Return New Size(Math.Max(h, sz.Width + CInt(10 * scale)), h)
    End Function

    Public Sub DrawBadge(g As Graphics, r As Rectangle, text As String, back As Color, fore As Color, font As Font)
        Dim old = g.SmoothingMode
        g.SmoothingMode = SmoothingMode.AntiAlias
        FillRound(g, back, r, r.Height / 2.0F)
        g.SmoothingMode = old
        TextRenderer.DrawText(g, text, font, r, fore,
                              TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPadding Or TextFormatFlags.SingleLine)
    End Sub

    Public Sub DrawCross(g As Graphics, r As Rectangle, color As Color, width As Single)
        Dim old = g.SmoothingMode
        g.SmoothingMode = SmoothingMode.AntiAlias
        Using p As New Pen(color, width)
            p.StartCap = LineCap.Round : p.EndCap = LineCap.Round
            g.DrawLine(p, r.Left, r.Top, r.Right, r.Bottom)
            g.DrawLine(p, r.Left, r.Bottom, r.Right, r.Top)
        End Using
        g.SmoothingMode = old
    End Sub

    Public Sub DrawPlus(g As Graphics, center As Point, half As Single, color As Color, width As Single)
        Dim old = g.SmoothingMode
        g.SmoothingMode = SmoothingMode.AntiAlias
        Using p As New Pen(color, width)
            p.StartCap = LineCap.Round : p.EndCap = LineCap.Round
            g.DrawLine(p, center.X - half, center.Y, center.X + half, center.Y)
            g.DrawLine(p, center.X, center.Y - half, center.X, center.Y + half)
        End Using
        g.SmoothingMode = old
    End Sub

    ''' <summary>Seta "aviãozinho" de enviar, dentro de r.</summary>
    Public Sub DrawSendGlyph(g As Graphics, r As RectangleF, color As Color)
        Dim old = g.SmoothingMode
        g.SmoothingMode = SmoothingMode.AntiAlias
        Using p As New GraphicsPath()
            p.AddPolygon(New PointF() {
                New PointF(r.Left, r.Top),
                New PointF(r.Right, r.Top + r.Height / 2),
                New PointF(r.Left, r.Bottom),
                New PointF(r.Left + r.Width * 0.22F, r.Top + r.Height / 2)})
            Using b As New SolidBrush(color)
                g.FillPath(b, p)
            End Using
        End Using
        g.SmoothingMode = old
    End Sub

    Public Sub DrawChevronDown(g As Graphics, center As PointF, half As Single, color As Color, width As Single)
        Dim old = g.SmoothingMode
        g.SmoothingMode = SmoothingMode.AntiAlias
        Using p As New Pen(color, width)
            p.StartCap = LineCap.Round : p.EndCap = LineCap.Round : p.LineJoin = LineJoin.Round
            g.DrawLines(p, New PointF() {
                New PointF(center.X - half, center.Y - half / 2),
                New PointF(center.X, center.Y + half / 2),
                New PointF(center.X + half, center.Y - half / 2)})
        End Using
        g.SmoothingMode = old
    End Sub

    Public Function FormatSize(bytes As Long) As String
        If bytes < 0 Then Return String.Empty
        If bytes < 1024 Then Return bytes.ToString() & " B"
        Dim kb = bytes / 1024.0
        If kb < 1024 Then Return kb.ToString("0.#") & " KB"
        Dim mb = kb / 1024.0
        If mb < 1024 Then Return mb.ToString("0.#") & " MB"
        Return (mb / 1024.0).ToString("0.##") & " GB"
    End Function

    ''' <summary>Fator de escala do DPI atual (1.0 = 96 DPI). Compatível com .NET 4.5.1.</summary>
    Public Function GetDpiScale(c As Control) As Single
        Try
            Using g = c.CreateGraphics()
                Return g.DpiX / 96.0F
            End Using
        Catch
            Return 1.0F
        End Try
    End Function

    Public Function ImageExtensions() As String()
        Return New String() {".png", ".jpg", ".jpeg", ".gif", ".bmp"}
    End Function

    ''' <summary>Carrega uma imagem sem manter o arquivo travado.</summary>
    Public Function LoadImageUnlocked(path As String) As Image
        Dim bytes = IO.File.ReadAllBytes(path)
        Dim ms As New IO.MemoryStream(bytes)
        Return Image.FromStream(ms)
    End Function
End Module
