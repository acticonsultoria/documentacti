Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

''' <summary>Visualizador simples de imagem (padrão do clique em imagem). Esc ou clique fecha.</summary>
Friend Class ChatImageViewer
    Inherits Form

    Private ReadOnly _image As Image
    Private ReadOnly _caption As String

    Private Sub New(img As Image, caption As String)
        _image = img
        _caption = caption
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        FormBorderStyle = FormBorderStyle.None
        StartPosition = FormStartPosition.Manual
        ShowInTaskbar = False
        KeyPreview = True
        BackColor = Color.FromArgb(18, 18, 18)
        Text = If(String.IsNullOrEmpty(caption), "Imagem", caption)
    End Sub

    Public Shared Sub ShowImage(owner As Form, img As Image, caption As String)
        Dim scr = If(owner IsNot Nothing, Screen.FromControl(owner), Screen.PrimaryScreen).WorkingArea
        Using f As New ChatImageViewer(img, caption)
            f.Bounds = Rectangle.Inflate(scr, -scr.Width \ 12, -scr.Height \ 12)
            If owner IsNot Nothing Then f.ShowDialog(owner) Else f.ShowDialog()
        End Using
    End Sub

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.KeyCode = Keys.Escape OrElse e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Space Then Close()
    End Sub

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        MyBase.OnMouseClick(e)
        Close()
    End Sub

    Protected Overrides Sub OnDeactivate(e As EventArgs)
        MyBase.OnDeactivate(e)
        Close()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g = e.Graphics
        g.Clear(BackColor)
        If _image Is Nothing Then Return
        Dim area = Rectangle.Inflate(ClientRectangle, -16, -16)
        If Not String.IsNullOrEmpty(_caption) Then area.Height -= 28
        Dim k = Math.Min(1.0, Math.Min(area.Width / CDbl(_image.Width), area.Height / CDbl(_image.Height)))
        Dim w = CInt(_image.Width * k), h = CInt(_image.Height * k)
        Dim rc As New Rectangle(area.X + (area.Width - w) \ 2, area.Y + (area.Height - h) \ 2, w, h)
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.PixelOffsetMode = PixelOffsetMode.HighQuality
        g.DrawImage(_image, rc)
        If Not String.IsNullOrEmpty(_caption) Then
            TextRenderer.DrawText(g, _caption, Font, New Rectangle(0, ClientSize.Height - 36, ClientSize.Width, 28), Color.Gainsboro,
                                  TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.EndEllipsis Or TextFormatFlags.NoPrefix)
        End If
    End Sub
End Class
