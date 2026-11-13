Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Friend Enum ToastState
    Entering
    Shown
    Dragging
    Returning
    Leaving
    SwipingOut
End Enum

''' <summary>
''' Janela de uma notificação: sem borda, sempre por cima, fora da barra de tarefas e do Alt+Tab,
''' e que não rouba o foco. No Windows usa layered window (alpha por pixel: cantos suaves e sombra).
''' </summary>
Friend NotInheritable Class ToastWindow
    Inherits Form

    Friend ReadOnly Toast As Toast
    Private ReadOnly _mgr As ToastManager
    Private ReadOnly _layered As Boolean
    Private _scale As Single
    Private _layout As ToastLayout
    Private _shadow As Integer
    Private _static As Bitmap
    Private _frame As Bitmap
    Private _memDc As IntPtr
    Private _hBitmap As IntPtr
    Private _lastProgressPx As Integer = -1
    Private _sinceRender As Double

    ' estado da animação (posição do cartão na tela)
    Friend BaseX As Integer
    Friend Y As Double
    Friend TargetY As Double
    Friend OffsetX As Double
    Friend Alpha As Double
    Friend State As ToastState = ToastState.Entering
    Friend DurationMs As Integer
    Friend RemainingMs As Double
    Friend PendingReason As ToastCloseReason
    Private _t As Double
    Private _fromOffset As Double
    Private _fromAlpha As Double
    Private _enterFrom As Double
    Private _velocity As Double
    Private _lastPresented As Rectangle = Rectangle.Empty
    Private _lastAlphaByte As Integer = -1

    ' mouse
    Private _hover As Boolean
    Private _hoverClose As Boolean
    Private _hoverOk As Boolean
    Private _down As Boolean
    Private _downScreen As Point
    Private _dragStartOffset As Double
    Private ReadOnly _samples As New List(Of KeyValuePair(Of Long, Integer))()
    Private ReadOnly _clock As Stopwatch = Stopwatch.StartNew()

    Public Sub New(toast As Toast, mgr As ToastManager, layered As Boolean, scale As Single)
        Me.Toast = toast
        _mgr = mgr
        _layered = layered
        _scale = scale
        FormBorderStyle = FormBorderStyle.None
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        ControlBox = False
        MaximizeBox = False
        MinimizeBox = False
        AutoScaleMode = AutoScaleMode.None
        Text = toast.Title
        SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        If Not _layered Then TopMost = True
        AddHandler toast.Changed, AddressOf OnToastChanged
        AddHandler toast.TimerRestartRequested, AddressOf OnRestartTimer
        Remeasure()
    End Sub

#Region "Janela sem foco / fora da barra de tarefas"

    Protected Overrides ReadOnly Property ShowWithoutActivation As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or NativeMethods.WS_EX_TOOLWINDOW Or NativeMethods.WS_EX_NOACTIVATE Or NativeMethods.WS_EX_TOPMOST
            If _layered Then cp.ExStyle = cp.ExStyle Or NativeMethods.WS_EX_LAYERED
            Return cp
        End Get
    End Property

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = NativeMethods.WM_MOUSEACTIVATE Then
            m.Result = New IntPtr(NativeMethods.MA_NOACTIVATE)
            Return
        End If
        MyBase.WndProc(m)
    End Sub

#End Region

#Region "Medidas e desenho"

    Friend ReadOnly Property CardW As Integer
        Get
            Return _layout.CardW
        End Get
    End Property

    Friend ReadOnly Property CardH As Integer
        Get
            Return _layout.CardH
        End Get
    End Property

    Private ReadOnly Property CardRect As Rectangle
        Get
            Return New Rectangle(_shadow, _shadow, _layout.CardW, _layout.CardH)
        End Get
    End Property

    Private Function S(v As Integer) As Integer
        Return CInt(Math.Round(v * _scale))
    End Function

    Friend Sub Remeasure()
        _layout = ToastRenderer.Measure(Toast, _mgr.Theme, _scale, _mgr.AppCaption)
        _shadow = If(_layered, S(_mgr.Theme.ShadowSize), 0)
        RebuildStatic()
    End Sub

    Friend Sub RebuildStatic()
        If _static IsNot Nothing Then _static.Dispose()
        _static = ToastRenderer.RenderStatic(Toast, _mgr.Theme, _layout, _shadow, _hover, _hoverClose, _hoverOk)
        If Not _layered Then
            Dim sz As New Size(_layout.CardW, _layout.CardH)
            If Size <> sz Then Size = sz
            Using p = ToastRenderer.RoundRect(New Rectangle(0, 0, sz.Width, sz.Height), _layout.Radius)
                Dim old = Region
                Region = New Region(p)
                If old IsNot Nothing Then old.Dispose()
            End Using
        End If
        _lastProgressPx = -1
        RenderFrame()
    End Sub

    Private ReadOnly Property Timed As Boolean
        Get
            Return DurationMs > 0
        End Get
    End Property

    Private Function ProgressPx() As Integer
        If Not Timed OrElse Not Toast.ShowProgress Then Return 0
        Return CInt(_layout.CardW * Math.Max(0, RemainingMs) / DurationMs)
    End Function

    Private Sub RenderFrame()
        Dim w = _static.Width, h = _static.Height
        If _frame Is Nothing OrElse _frame.Width <> w OrElse _frame.Height <> h Then
            If _frame IsNot Nothing Then _frame.Dispose()
            _frame = New Bitmap(w, h, Imaging.PixelFormat.Format32bppArgb)
        End If
        Dim px = ProgressPx()
        Using g = Graphics.FromImage(_frame)
            g.CompositingMode = CompositingMode.SourceCopy
            g.DrawImage(_static, New Rectangle(0, 0, w, h))
            g.CompositingMode = CompositingMode.SourceOver
            If px > 0 Then
                ToastRenderer.DrawProgress(g, CardRect, _layout.Radius, px / CDbl(_layout.CardW), ToastRenderer.AccentOf(Toast, _mgr.Theme), _scale)
            End If
        End Using
        _lastProgressPx = px
        _sinceRender = 0
        If _layered Then
            If Not IsHandleCreated Then Return
            Dim hb = _frame.GetHbitmap(Color.FromArgb(0))
            If _memDc = IntPtr.Zero Then
                Dim screenDc = NativeMethods.GetDC(IntPtr.Zero)
                _memDc = NativeMethods.CreateCompatibleDC(screenDc)
                NativeMethods.ReleaseDC(IntPtr.Zero, screenDc)
            End If
            NativeMethods.SelectObject(_memDc, hb)
            If _hBitmap <> IntPtr.Zero Then NativeMethods.DeleteObject(_hBitmap)
            _hBitmap = hb
            _lastPresented = Rectangle.Empty
        Else
            Invalidate()
        End If
    End Sub

    ''' <summary>Aplica posição/transparência atuais à janela (só se mudou algo).</summary>
    Friend Sub Present()
        Dim x = BaseX + CInt(Math.Round(OffsetX)) - _shadow
        Dim y = CInt(Math.Round(Me.Y)) - _shadow
        Dim r As New Rectangle(x, y, _frame.Width, _frame.Height)
        Dim a = CInt(Math.Max(0, Math.Min(1, Alpha)) * 255)
        If r = _lastPresented AndAlso a = _lastAlphaByte Then Return
        _lastPresented = r
        _lastAlphaByte = a
        If _layered Then
            If Not IsHandleCreated OrElse _memDc = IntPtr.Zero Then Return
            Dim dst As New NativeMethods.POINTAPI(x, y)
            Dim sz As New NativeMethods.SIZEAPI(r.Width, r.Height)
            Dim src As New NativeMethods.POINTAPI(0, 0)
            Dim blend As New NativeMethods.BLENDFUNCTION() With {
                .BlendOp = NativeMethods.AC_SRC_OVER, .BlendFlags = 0,
                .SourceConstantAlpha = CByte(a), .AlphaFormat = NativeMethods.AC_SRC_ALPHA}
            Dim screenDc = NativeMethods.GetDC(IntPtr.Zero)
            NativeMethods.UpdateLayeredWindow(Handle, screenDc, dst, sz, _memDc, src, 0, blend, NativeMethods.ULW_ALPHA)
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDc)
        Else
            Location = New Point(x, y)
            Try
                Dim op = Math.Max(0.01, a / 255.0)
                If Math.Abs(Opacity - op) > 0.02 Then Opacity = op
            Catch
            End Try
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        If _frame IsNot Nothing Then e.Graphics.DrawImage(_frame, New Rectangle(0, 0, _frame.Width, _frame.Height))
    End Sub

    ''' <summary>Cria a janela e mostra sem ativar.</summary>
    Friend Sub ShowToast(enterFromRight As Boolean)
        _enterFrom = If(enterFromRight, S(56), -S(56))
        OffsetX = _enterFrom
        Alpha = 0
        State = ToastState.Entering
        _t = 0
        Size = New Size(_static.Width, _static.Height)
        Location = New Point(BaseX - _shadow + CInt(OffsetX), CInt(Y) - _shadow)
        Dim h = Handle ' força a criação
        Dim previous = If(_layered, NativeMethods.GetForegroundWindow(), IntPtr.Zero)
        RenderFrame()
        Present()
        Show()
        If _layered Then
            NativeMethods.SetWindowPos(Handle, NativeMethods.HWND_TOPMOST, 0, 0, 0, 0,
                                       NativeMethods.SWP_NOMOVE Or NativeMethods.SWP_NOSIZE Or NativeMethods.SWP_NOACTIVATE)
            ' salvaguarda: se por algum motivo virou a janela ativa, devolve o foco para quem estava
            If previous <> IntPtr.Zero AndAlso NativeMethods.GetForegroundWindow() = Handle Then
                NativeMethods.SetForegroundWindow(previous)
            End If
        End If
    End Sub

#End Region

#Region "Animação (chamada pelo timer do ToastManager)"

    Private Shared Function EaseOut(k As Double) As Double
        Return 1 - Math.Pow(1 - k, 3)
    End Function

    Friend ReadOnly Property IsActiveInStack As Boolean
        Get
            Return State <> ToastState.Leaving AndAlso State <> ToastState.SwipingOut
        End Get
    End Property

    Friend ReadOnly Property IsInteracting As Boolean
        Get
            Return _hover OrElse State = ToastState.Dragging
        End Get
    End Property

    ''' <summary>Avança a animação. Retorna True quando terminou de sair (pode ser destruída).</summary>
    Friend Function StepFrame(dt As Double, paused As Boolean) As Boolean
        Dim finished = False
        Select Case State
            Case ToastState.Entering
                _t += dt
                Dim k = Math.Min(1, _t / 260.0)
                Dim e = EaseOut(k)
                OffsetX = _enterFrom * (1 - e)
                Alpha = e
                If k >= 1 Then State = ToastState.Shown
            Case ToastState.Returning
                OffsetX *= Math.Exp(-dt / 55.0)
                If Math.Abs(OffsetX) < 0.5 Then OffsetX = 0 : State = ToastState.Shown
                Alpha = DragAlpha()
            Case ToastState.Leaving
                _t += dt
                Dim k = Math.Min(1, _t / 200.0)
                Dim e = k * k
                Alpha = _fromAlpha * (1 - e)
                OffsetX = _fromOffset + Math.Sign(If(_enterFrom = 0, 1, _enterFrom)) * S(28) * e
                finished = k >= 1
            Case ToastState.SwipingOut
                OffsetX += _velocity * dt
                Alpha = Math.Max(0, DragAlpha() - 0.15)
                finished = Alpha <= 0.01 OrElse Math.Abs(OffsetX) > _layout.CardW + _shadow * 2
        End Select

        ' desliza verticalmente até o lugar na pilha
        Dim diff = TargetY - Y
        If Math.Abs(diff) > 0.5 Then
            Y += diff * (1 - Math.Exp(-dt / 70.0))
        Else
            Y = TargetY
        End If

        ' tempo
        If Timed AndAlso IsActiveInStack AndAlso State <> ToastState.Dragging AndAlso Not paused Then
            RemainingMs -= dt
            If RemainingMs <= 0 Then
                RemainingMs = 0
                _mgr.CloseToast(Toast, ToastCloseReason.Timeout)
            End If
        End If

        _sinceRender += dt
        If Not finished AndAlso ProgressPx() <> _lastProgressPx AndAlso _sinceRender >= 33 Then RenderFrame()
        If Not finished Then Present()
        Return finished
    End Function

    Private Function DragAlpha() As Double
        Return 1 - Math.Min(1, Math.Abs(OffsetX) / _layout.CardW) * 0.85
    End Function

    Friend Sub BeginLeave(reason As ToastCloseReason)
        If Not IsActiveInStack Then Return
        PendingReason = reason
        If reason = ToastCloseReason.Swiped Then
            State = ToastState.SwipingOut
            Dim dir = If(OffsetX = 0, 1, Math.Sign(OffsetX))
            _velocity = dir * Math.Max(Math.Abs(_velocity), 1.4 * _scale)
            If Math.Sign(_velocity) <> dir Then _velocity = -_velocity
        Else
            State = ToastState.Leaving
            _t = 0
            _fromOffset = OffsetX
            _fromAlpha = Alpha
        End If
        If _down Then
            _down = False
            Capture = False
        End If
    End Sub

    Friend Sub ResetTimer()
        RemainingMs = DurationMs
        _lastProgressPx = -1
    End Sub

#End Region

#Region "Mouse: hover, X, clique e arrastar"

    Private Function CloseHit(p As Point) As Boolean
        Dim cr = _layout.CloseRect
        cr.Offset(_shadow, _shadow)
        cr.Inflate(S(3), S(3))
        Return cr.Contains(p)
    End Function

    Private Function OkHit(p As Point) As Boolean
        If _layout.OkRect.IsEmpty Then Return False
        Dim r = _layout.OkRect
        r.Offset(_shadow, _shadow)
        Return r.Contains(p)
    End Function

    Private Sub SetHover(hover As Boolean, hoverClose As Boolean, Optional hoverOk As Boolean = False)
        If hover = _hover AndAlso hoverClose = _hoverClose AndAlso hoverOk = _hoverOk Then Return
        _hover = hover
        _hoverClose = hoverClose
        _hoverOk = hoverOk
        Cursor = If(hoverClose OrElse hoverOk, Cursors.Hand, Cursors.Default)
        RebuildStatic()
        Present()
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        If Not IsActiveInStack Then Return
        If _down Then
            Dim pos = Cursor.Position
            Dim dx = pos.X - _downScreen.X
            If State <> ToastState.Dragging AndAlso Math.Abs(dx) > SystemInformation.DragSize.Width Then
                State = ToastState.Dragging
                Alpha = 1
                SetHover(True, False)
            End If
            If State = ToastState.Dragging Then
                OffsetX = _dragStartOffset + dx
                Alpha = DragAlpha()
                _samples.Add(New KeyValuePair(Of Long, Integer)(_clock.ElapsedMilliseconds, pos.X))
                While _samples.Count > 0 AndAlso _clock.ElapsedMilliseconds - _samples(0).Key > 90
                    _samples.RemoveAt(0)
                End While
                Present()
            End If
            Return
        End If
        SetHover(CardRect.Contains(e.Location), CloseHit(e.Location), OkHit(e.Location))
    End Sub

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        If IsActiveInStack Then SetHover(True, CloseHit(PointToClient(Cursor.Position)))
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        If Not _down AndAlso IsActiveInStack Then SetHover(False, False)
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If e.Button <> MouseButtons.Left OrElse Not IsActiveInStack Then Return
        _down = True
        _downScreen = Cursor.Position
        _dragStartOffset = OffsetX
        _samples.Clear()
        _samples.Add(New KeyValuePair(Of Long, Integer)(_clock.ElapsedMilliseconds, _downScreen.X))
        Capture = True
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        If Not _down Then Return
        _down = False
        Capture = False
        If State = ToastState.Dragging Then
            _velocity = 0
            If _samples.Count >= 2 Then
                Dim a = _samples(0), b = _samples(_samples.Count - 1)
                Dim dtm = Math.Max(1, b.Key - a.Key)
                _velocity = (b.Value - a.Value) / CDbl(dtm)
            End If
            Dim far = Math.Abs(OffsetX) > _layout.CardW * 0.35
            Dim flick = Math.Abs(_velocity) > 0.6 * _scale AndAlso Math.Sign(_velocity) = Math.Sign(OffsetX)
            If (far OrElse flick) AndAlso _mgr.AllowSwipe Then
                _mgr.CloseToast(Toast, ToastCloseReason.Swiped)
            Else
                State = ToastState.Returning
            End If
            Dim inside = ClientRectangle.Contains(PointToClient(Cursor.Position))
            If Not inside Then SetHover(False, False)
            Return
        End If
        If e.Button <> MouseButtons.Left OrElse Not IsActiveInStack Then Return
        If CloseHit(e.Location) Then
            _mgr.CloseToast(Toast, ToastCloseReason.CloseButton)
        ElseIf OkHit(e.Location) Then
            _mgr.OnToastOkClicked(Toast)
        ElseIf CardRect.Contains(e.Location) Then
            _mgr.OnToastClicked(Toast)
        End If
    End Sub

#End Region

#Region "Mudanças"

    Private Sub OnToastChanged(sender As Object, e As EventArgs)
        If IsDisposed Then Return
        If InvokeRequired Then
            BeginInvoke(New Action(Sub() OnToastChanged(sender, e)))
            Return
        End If
        Dim oldH = _layout.CardH
        Dim dur = _mgr.EffectiveDuration(Toast)
        If dur <> DurationMs Then
            DurationMs = dur
            RemainingMs = dur
        End If
        Remeasure()
        If oldH <> _layout.CardH Then _mgr.RelayoutStack()
        Present()
    End Sub

    Private Sub OnRestartTimer(sender As Object, e As EventArgs)
        If IsDisposed Then Return
        If InvokeRequired Then
            BeginInvoke(New Action(Sub() OnRestartTimer(sender, e)))
            Return
        End If
        ResetTimer()
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            RemoveHandler Toast.Changed, AddressOf OnToastChanged
            RemoveHandler Toast.TimerRestartRequested, AddressOf OnRestartTimer
            If _static IsNot Nothing Then _static.Dispose()
            If _frame IsNot Nothing Then _frame.Dispose()
        End If
        If _memDc <> IntPtr.Zero Then NativeMethods.DeleteDC(_memDc) : _memDc = IntPtr.Zero
        If _hBitmap <> IntPtr.Zero Then NativeMethods.DeleteObject(_hBitmap) : _hBitmap = IntPtr.Zero
        MyBase.Dispose(disposing)
    End Sub

#End Region

End Class
