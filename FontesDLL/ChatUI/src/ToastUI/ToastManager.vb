Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Drawing
Imports System.Media
Imports System.Threading
Imports System.Windows.Forms

''' <summary>
''' Mostra e empilha as notificações. Use <see cref="ToastManager.Default"/> ou arraste um ToastManager para o form
''' (configure posição, tempo e tema no designer).
''' Pode chamar Show de qualquer thread depois que o gerenciador foi criado/usado pela thread da UI.
''' </summary>
<ToolboxItem(True), DefaultEvent("ToastClicked"), Description("Notificações (toasts) desenhadas com GDI+.")>
Public Class ToastManager
    Inherits Component

    Private Shared _default As ToastManager

    Private _sync As SynchronizationContext
    Private _uiThread As Integer = -1
    Private ReadOnly _windows As New List(Of ToastWindow)()     ' todas as janelas vivas (inclusive saindo)
    Private ReadOnly _stack As New List(Of ToastWindow)()       ' as que ocupam lugar na pilha, da mais nova p/ a mais antiga
    Private ReadOnly _queue As New List(Of Toast)()
    Private _timer As System.Windows.Forms.Timer
    Private ReadOnly _clock As Stopwatch = Stopwatch.StartNew()
    Private _lastTick As Long
    Private _theme As ToastTheme
    Private _stackScreen As Screen
    Private _scale As Single = 0

    Private _position As ToastPosition = ToastPosition.BottomRight
    Private _screenMode As ToastScreenMode = ToastScreenMode.Primary
    Private _maxVisible As Integer = 5
    Private _defaultDuration As Integer = 5000
    Private _spacing As Integer = 10
    Private _edgeMargin As Integer = 16
    Private _pauseOnHover As Boolean = True
    Private _closeOnClick As Boolean = True
    Private _allowSwipe As Boolean = True
    Private _playSounds As Boolean
    Private _appCaption As String = String.Empty
    Private _useLayered As Boolean = True

#Region "Eventos"

    ''' <summary>Uma notificação apareceu na tela.</summary>
    Public Event ToastShown As EventHandler(Of ToastEventArgs)
    ''' <summary>Clique no corpo de qualquer notificação (dispara depois do Toast.Click).</summary>
    Public Event ToastClicked As EventHandler(Of ToastEventArgs)
    ''' <summary>Clique no botão OK de uma notificação (Toast.ShowOkButton).</summary>
    Public Event ToastOkClicked As EventHandler(Of ToastEventArgs)
    ''' <summary>Uma notificação foi fechada (e.Reason diz como).</summary>
    Public Event ToastClosed As EventHandler(Of ToastClosedEventArgs)

#End Region

    Public Sub New()
        _theme = New ToastTheme()
        AddHandler _theme.Changed, AddressOf OnThemeChanged
        CaptureContext()
    End Sub

    Public Sub New(container As IContainer)
        Me.New()
        If container IsNot Nothing Then container.Add(Me)
    End Sub

    ''' <summary>Gerenciador compartilhado. Acesse a primeira vez pela thread da UI (ex.: no Form_Load).</summary>
    Public Shared ReadOnly Property [Default] As ToastManager
        Get
            If _default Is Nothing Then _default = New ToastManager()
            Return _default
        End Get
    End Property

#Region "Propriedades"

    <Category("Aparência"), DefaultValue(GetType(ToastPosition), "BottomRight")>
    Public Property Position As ToastPosition
        Get
            Return _position
        End Get
        Set(value As ToastPosition)
            If _position = value Then Return
            _position = value
            RelayoutStack()
        End Set
    End Property

    <Category("Aparência"), DefaultValue(GetType(ToastScreenMode), "Primary")>
    Public Property ScreenMode As ToastScreenMode
        Get
            Return _screenMode
        End Get
        Set(value As ToastScreenMode)
            _screenMode = value
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(5), Description("Máximo na tela ao mesmo tempo; as demais esperam na fila.")>
    Public Property MaxVisible As Integer
        Get
            Return _maxVisible
        End Get
        Set(value As Integer)
            _maxVisible = Math.Max(1, value)
            PumpQueue()
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(5000), Description("Tempo padrão na tela (ms). 0 = até o usuário fechar.")>
    Public Property DefaultDuration As Integer
        Get
            Return _defaultDuration
        End Get
        Set(value As Integer)
            _defaultDuration = Math.Max(0, value)
        End Set
    End Property

    <Category("Aparência"), DefaultValue(10), Description("Espaço entre notificações (px a 96 DPI).")>
    Public Property Spacing As Integer
        Get
            Return _spacing
        End Get
        Set(value As Integer)
            _spacing = Math.Max(0, value)
            RelayoutStack()
        End Set
    End Property

    <Category("Aparência"), DefaultValue(16), Description("Distância da borda da tela (px a 96 DPI).")>
    Public Property EdgeMargin As Integer
        Get
            Return _edgeMargin
        End Get
        Set(value As Integer)
            _edgeMargin = Math.Max(0, value)
            RelayoutStack()
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(True), Description("Pausa o tempo de todas enquanto o mouse está sobre uma delas.")>
    Public Property PauseOnHover As Boolean
        Get
            Return _pauseOnHover
        End Get
        Set(value As Boolean)
            _pauseOnHover = value
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(True), Description("Fecha a notificação ao clicar no corpo.")>
    Public Property CloseOnClick As Boolean
        Get
            Return _closeOnClick
        End Get
        Set(value As Boolean)
            _closeOnClick = value
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(True), Description("Permite dispensar arrastando para o lado.")>
    Public Property AllowSwipe As Boolean
        Get
            Return _allowSwipe
        End Get
        Set(value As Boolean)
            _allowSwipe = value
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(False), Description("Toca o som do Windows correspondente ao tipo.")>
    Public Property PlaySounds As Boolean
        Get
            Return _playSounds
        End Get
        Set(value As Boolean)
            _playSounds = value
        End Set
    End Property

    <Category("Aparência"), DefaultValue(""), Description("Linha pequena acima do título quando o Toast não define Caption (ex.: nome do sistema).")>
    Public Property AppCaption As String
        Get
            Return _appCaption
        End Get
        Set(value As String)
            _appCaption = If(value, String.Empty)
        End Set
    End Property

    <Category("Aparência"), DefaultValue(True), Description("No Windows, usa janelas com transparência por pixel (cantos suaves e sombra).")>
    Public Property UseLayeredWindows As Boolean
        Get
            Return _useLayered
        End Get
        Set(value As Boolean)
            _useLayered = value
        End Set
    End Property

    <Category("Aparência"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property Theme As ToastTheme
        Get
            Return _theme
        End Get
        Set(value As ToastTheme)
            If value Is Nothing OrElse Object.ReferenceEquals(value, _theme) Then Return
            RemoveHandler _theme.Changed, AddressOf OnThemeChanged
            _theme = value
            AddHandler _theme.Changed, AddressOf OnThemeChanged
            OnThemeChanged(Me, EventArgs.Empty)
        End Set
    End Property

    ''' <summary>Notificações na tela (da mais nova para a mais antiga).</summary>
    <Browsable(False)>
    Public ReadOnly Property OpenToasts As ReadOnlyCollection(Of Toast)
        Get
            Dim l As New List(Of Toast)()
            For Each w In _stack
                l.Add(w.Toast)
            Next
            Return l.AsReadOnly()
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property QueuedCount As Integer
        Get
            Return _queue.Count
        End Get
    End Property

#End Region

#Region "Mostrar"

    ''' <summary>Mostra (ou põe na fila) uma notificação. Retorna a própria notificação para você poder fechar/alterar depois.</summary>
    Public Function Show(toast As Toast) As Toast
        If toast Is Nothing Then Throw New ArgumentNullException("toast")
        RunOnUi(Sub() ShowCore(toast))
        Return toast
    End Function

    Public Function Show(title As String, message As String, Optional type As ToastType = ToastType.Info, Optional duration As Integer = -1) As Toast
        Return Show(New Toast(title, message, type) With {.Duration = duration})
    End Function

    Public Function ShowInfo(title As String, message As String) As Toast
        Return Show(title, message, ToastType.Info)
    End Function

    Public Function ShowSuccess(title As String, message As String) As Toast
        Return Show(title, message, ToastType.Success)
    End Function

    Public Function ShowWarning(title As String, message As String) As Toast
        Return Show(title, message, ToastType.Warning)
    End Function

    Public Function ShowError(title As String, message As String) As Toast
        Return Show(title, message, ToastType.Error)
    End Function

    ''' <summary>Notificação de pessoa/mensagem: avatar (foto ou iniciais do título).</summary>
    Public Function ShowMessage(title As String, message As String, Optional avatar As Image = Nothing, Optional tag As Object = Nothing) As Toast
        Return Show(New Toast(title, message, ToastType.Message) With {.Image = avatar, .Tag = tag})
    End Function

    ''' <summary>Fecha todas (com animação) e esvazia a fila.</summary>
    Public Sub CloseAll()
        RunOnUi(Sub()
                    For Each q In _queue.ToArray()
                        q.Queued = False
                        q.RaiseClosed(ToastCloseReason.Programmatic)
                        RaiseEvent ToastClosed(Me, New ToastClosedEventArgs(q, ToastCloseReason.Programmatic))
                    Next
                    _queue.Clear()
                    For Each w In _stack.ToArray()
                        CloseToast(w.Toast, ToastCloseReason.Programmatic)
                    Next
                End Sub)
    End Sub

    ''' <summary>Desenha uma notificação num Bitmap (com sombra), sem mostrar. Útil para pré-visualização/documentação.</summary>
    Public Shared Function RenderPreview(toast As Toast, theme As ToastTheme, Optional hovered As Boolean = False, Optional progress As Double = 1.0, Optional appCaption As String = "") As Bitmap
        If theme Is Nothing Then theme = New ToastTheme()
        Dim L = ToastRenderer.Measure(toast, theme, 1.0F, appCaption)
        Dim bmp = ToastRenderer.RenderStatic(toast, theme, L, theme.ShadowSize, hovered, False, False)
        If progress > 0 AndAlso progress < 1 AndAlso toast.ShowProgress Then
            Using g = Graphics.FromImage(bmp)
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                ToastRenderer.DrawProgress(g, New Rectangle(theme.ShadowSize, theme.ShadowSize, L.CardW, L.CardH), L.Radius, progress, ToastRenderer.AccentOf(toast, theme), 1.0F)
            End Using
        End If
        Return bmp
    End Function

#End Region

#Region "Thread da UI"

    Private Sub CaptureContext()
        If _sync IsNot Nothing Then Return
        Dim ctx = SynchronizationContext.Current
        If TypeOf ctx Is WindowsFormsSynchronizationContext Then
            _sync = ctx
            _uiThread = Thread.CurrentThread.ManagedThreadId
        End If
    End Sub

    Private Sub RunOnUi(action As Action)
        CaptureContext()
        If _sync Is Nothing OrElse Thread.CurrentThread.ManagedThreadId = _uiThread Then
            If _sync Is Nothing AndAlso Not Application.MessageLoop AndAlso Thread.CurrentThread.GetApartmentState() <> ApartmentState.STA Then
                Throw New InvalidOperationException("Crie ou use o ToastManager primeiro pela thread da interface (ex.: no Load do form principal); depois ele pode ser chamado de qualquer thread.")
            End If
            action()
        Else
            _sync.Post(Sub(o) action(), Nothing)
        End If
    End Sub

#End Region

#Region "Pilha"

    Private Sub ShowCore(toast As Toast)
        If toast.IsOpen OrElse toast.Queued Then Return
        toast.Owner = Me
        toast.ResetForShow()
        If _scale <= 0 Then _scale = ToastRenderer.ScreenScale()
        If CanShowMore(Nothing) Then
            Open(toast)
        Else
            toast.Queued = True
            _queue.Add(toast)
        End If
    End Sub

    Private Function S(v As Integer) As Integer
        Return CInt(Math.Round(v * If(_scale <= 0, 1.0F, _scale)))
    End Function

    Private Function WorkArea() As Rectangle
        If _stackScreen Is Nothing OrElse _stack.Count = 0 AndAlso _windows.Count = 0 Then
            _stackScreen = If(_screenMode = ToastScreenMode.UnderMouse, Screen.FromPoint(Cursor.Position), Screen.PrimaryScreen)
        End If
        Return _stackScreen.WorkingArea
    End Function

    Private Function StackHeight() As Integer
        Dim h = 0
        For Each w In _stack
            h += w.CardH + S(_spacing)
        Next
        Return h
    End Function

    Private Function CanShowMore(candidate As ToastWindow) As Boolean
        If _stack.Count >= _maxVisible Then Return False
        If _stack.Count = 0 Then Return True
        ' cabe na altura da tela? (estimativa com a altura média das que estão abertas)
        Dim avg = StackHeight() \ Math.Max(1, _stack.Count)
        Return StackHeight() + avg + S(_edgeMargin) * 2 <= WorkArea().Height
    End Function

    Private ReadOnly Property FromRight As Boolean
        Get
            Return _position = ToastPosition.BottomRight OrElse _position = ToastPosition.TopRight
        End Get
    End Property

    Private ReadOnly Property FromBottom As Boolean
        Get
            Return _position = ToastPosition.BottomRight OrElse _position = ToastPosition.BottomLeft
        End Get
    End Property

    Private Sub Open(toast As Toast)
        Dim layered = _useLayered AndAlso NativeMethods.LayeredSupported
        Dim w As New ToastWindow(toast, Me, layered, _scale)
        Dim dur = EffectiveDuration(toast)
        w.DurationMs = dur
        w.RemainingMs = dur
        toast.Window = w
        toast.Queued = False
        _windows.Add(w)
        _stack.Insert(0, w)
        ComputeTargets()
        w.Y = w.TargetY
        w.ShowToast(FromRight)
        EnsureTimer()
        If _playSounds Then PlaySound(toast.Type)
        RaiseEvent ToastShown(Me, New ToastEventArgs(toast, _closeOnClick))
    End Sub

    Private Sub ComputeTargets()
        If _stack.Count = 0 Then Return
        Dim wa = WorkArea()
        Dim margin = S(_edgeMargin)
        Dim acc = 0
        For Each w In _stack
            w.BaseX = If(FromRight, wa.Right - margin - w.CardW, wa.Left + margin)
            If FromBottom Then
                w.TargetY = wa.Bottom - margin - acc - w.CardH
            Else
                w.TargetY = wa.Top + margin + acc
            End If
            acc += w.CardH + S(_spacing)
        Next
    End Sub

    ''' <summary>Recalcula as posições (as janelas deslizam até o novo lugar).</summary>
    Friend Sub RelayoutStack()
        ComputeTargets()
        EnsureTimer()
    End Sub

    Private Sub PumpQueue()
        While _queue.Count > 0 AndAlso CanShowMore(Nothing)
            Dim t = _queue(0)
            _queue.RemoveAt(0)
            t.Queued = False
            Open(t)
        End While
    End Sub

    Friend Sub CloseToast(toast As Toast, reason As ToastCloseReason)
        If toast Is Nothing Then Return
        If _sync IsNot Nothing AndAlso Thread.CurrentThread.ManagedThreadId <> _uiThread Then
            _sync.Post(Sub(o) CloseToast(toast, reason), Nothing)
            Return
        End If
        If toast.Queued Then
            _queue.Remove(toast)
            toast.Queued = False
            toast.RaiseClosed(reason)
            RaiseEvent ToastClosed(Me, New ToastClosedEventArgs(toast, reason))
            Return
        End If
        Dim w = toast.Window
        If w Is Nothing OrElse Not w.IsActiveInStack Then Return
        w.BeginLeave(reason)
        _stack.Remove(w)
        ComputeTargets()
        PumpQueue()
        EnsureTimer()
    End Sub

    Friend Sub OnToastClicked(toast As Toast)
        Dim e As New ToastEventArgs(toast, _closeOnClick)
        toast.RaiseClick(e)
        RaiseEvent ToastClicked(Me, e)
        If e.CloseToast Then CloseToast(toast, ToastCloseReason.Clicked)
    End Sub

    Friend Sub OnToastOkClicked(toast As Toast)
        Dim e As New ToastEventArgs(toast, True)
        toast.RaiseOkClick(e)
        RaiseEvent ToastOkClicked(Me, e)
        If e.CloseToast Then CloseToast(toast, ToastCloseReason.OkButton)
    End Sub

    Friend Function EffectiveDuration(toast As Toast) As Integer
        Return If(toast.Duration < 0, _defaultDuration, toast.Duration)
    End Function

    Private Sub PlaySound(type As ToastType)
        Try
            Select Case type
                Case ToastType.Error : SystemSounds.Hand.Play()
                Case ToastType.Warning : SystemSounds.Exclamation.Play()
                Case Else : SystemSounds.Asterisk.Play()
            End Select
        Catch
        End Try
    End Sub

#End Region

#Region "Animação"

    Private Sub EnsureTimer()
        If _windows.Count = 0 Then Return
        If _timer Is Nothing Then
            _timer = New System.Windows.Forms.Timer() With {.Interval = 15}
            AddHandler _timer.Tick, AddressOf OnTick
        End If
        If Not _timer.Enabled Then
            _lastTick = _clock.ElapsedMilliseconds
            _timer.Start()
        End If
    End Sub

    Private Sub OnTick(sender As Object, e As EventArgs)
        Dim now = _clock.ElapsedMilliseconds
        Dim dt = Math.Min(50.0, Math.Max(1.0, CDbl(now - _lastTick)))
        _lastTick = now

        Dim anyHover = False
        For Each w In _windows
            If w.IsInteracting Then anyHover = True : Exit For
        Next

        For Each w In _windows.ToArray()
            Dim paused = If(_pauseOnHover, anyHover, w.IsInteracting)
            Dim done As Boolean
            Try
                done = w.StepFrame(dt, paused)
            Catch ex As ObjectDisposedException
                done = True
            End Try
            If done Then Destroy(w)
        Next
        If _windows.Count = 0 Then
            _timer.Stop()
            _stackScreen = Nothing
        End If
    End Sub

    Private Sub Destroy(w As ToastWindow)
        _windows.Remove(w)
        _stack.Remove(w)
        Dim t = w.Toast
        Dim reason = w.PendingReason
        t.Window = Nothing
        w.Hide()
        w.Close()
        w.Dispose()
        t.RaiseClosed(reason)
        RaiseEvent ToastClosed(Me, New ToastClosedEventArgs(t, reason))
        PumpQueue()
    End Sub

    Private Sub OnThemeChanged(sender As Object, e As EventArgs)
        For Each w In _windows
            If Not w.IsDisposed Then w.Remeasure()
        Next
        RelayoutStack()
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            If _timer IsNot Nothing Then _timer.Dispose()
            For Each w In _windows.ToArray()
                w.Toast.Window = Nothing
                w.Dispose()
            Next
            _windows.Clear()
            _stack.Clear()
            _queue.Clear()
            RemoveHandler _theme.Changed, AddressOf OnThemeChanged
            If Object.ReferenceEquals(_default, Me) Then _default = Nothing
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

End Class
