Imports System.ComponentModel
Imports System.Drawing

''' <summary>Cores, fontes e medidas das notificações. Alterar qualquer valor redesenha as abertas.</summary>
<TypeConverter(GetType(ExpandableObjectConverter))>
Public Class ToastTheme

    Private _back, _border, _title, _text, _caption, _shadow As Color
    Private _info, _success, _warning, _error, _messageColor As Color
    Private _shadowOpacity As Double
    Private _titleFont As Font
    Private _textFont As Font
    Private _captionFont As Font
    Private _width As Integer = 360
    Private _radius As Integer = 10
    Private _maxLines As Integer = 3
    Private _shadowSize As Integer = 16
    Private _suspend As Boolean

    Public Event Changed As EventHandler

    Public Sub New()
        _titleFont = New Font("Segoe UI Semibold", 10.0F)
        _textFont = New Font("Segoe UI", 9.25F)
        _captionFont = New Font("Segoe UI", 8.0F)
        ApplyLight()
    End Sub

    Public Shared Function CreateLight() As ToastTheme
        Return New ToastTheme()
    End Function

    Public Shared Function CreateDark() As ToastTheme
        Dim t As New ToastTheme()
        t.ApplyDark()
        Return t
    End Function

    ''' <summary>Claro ou escuro conforme a configuração "modo de app" do Windows 10/11 (claro se não der para ler).</summary>
    Public Shared Function FromSystem() As ToastTheme
        Return If(SystemUsesDarkTheme(), CreateDark(), CreateLight())
    End Function

    Public Shared Function SystemUsesDarkTheme() As Boolean
        Try
            Using k = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")
                If k Is Nothing Then Return False
                Dim v = k.GetValue("AppsUseLightTheme")
                Return v IsNot Nothing AndAlso Convert.ToInt32(v) = 0
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Sub ApplyLight()
        _suspend = True
        BackColor = Color.White
        BorderColor = Color.FromArgb(222, 226, 231)
        TitleColor = Color.FromArgb(17, 24, 39)
        TextColor = Color.FromArgb(55, 65, 81)
        CaptionColor = Color.FromArgb(120, 128, 140)
        ShadowColor = Color.Black
        ShadowOpacity = 0.18
        InfoColor = Color.FromArgb(37, 99, 235)
        SuccessColor = Color.FromArgb(22, 163, 74)
        WarningColor = Color.FromArgb(217, 119, 6)
        ErrorColor = Color.FromArgb(220, 38, 38)
        MessageColor = Color.FromArgb(37, 99, 235)
        _suspend = False
        OnChanged()
    End Sub

    Public Sub ApplyDark()
        _suspend = True
        BackColor = Color.FromArgb(32, 36, 42)
        BorderColor = Color.FromArgb(52, 58, 66)
        TitleColor = Color.FromArgb(236, 240, 244)
        TextColor = Color.FromArgb(190, 198, 207)
        CaptionColor = Color.FromArgb(139, 148, 158)
        ShadowColor = Color.Black
        ShadowOpacity = 0.45
        InfoColor = Color.FromArgb(96, 165, 250)
        SuccessColor = Color.FromArgb(74, 222, 128)
        WarningColor = Color.FromArgb(251, 191, 36)
        ErrorColor = Color.FromArgb(248, 113, 113)
        MessageColor = Color.FromArgb(96, 165, 250)
        _suspend = False
        OnChanged()
    End Sub

    ''' <summary>Cor padrão do tipo (usada quando Toast.AccentColor está vazio).</summary>
    Public Function GetTypeColor(type As ToastType) As Color
        Select Case type
            Case ToastType.Success : Return _success
            Case ToastType.Warning : Return _warning
            Case ToastType.Error : Return _error
            Case ToastType.Message : Return _messageColor
            Case Else : Return _info
        End Select
    End Function

    <Category("Cores")> Public Property BackColor As Color
        Get
            Return _back
        End Get
        Set(value As Color)
            SetValue(_back, value)
        End Set
    End Property

    <Category("Cores")> Public Property BorderColor As Color
        Get
            Return _border
        End Get
        Set(value As Color)
            SetValue(_border, value)
        End Set
    End Property

    <Category("Cores")> Public Property TitleColor As Color
        Get
            Return _title
        End Get
        Set(value As Color)
            SetValue(_title, value)
        End Set
    End Property

    <Category("Cores")> Public Property TextColor As Color
        Get
            Return _text
        End Get
        Set(value As Color)
            SetValue(_text, value)
        End Set
    End Property

    <Category("Cores"), Description("Caption, hora e o X.")> Public Property CaptionColor As Color
        Get
            Return _caption
        End Get
        Set(value As Color)
            SetValue(_caption, value)
        End Set
    End Property

    <Category("Cores")> Public Property ShadowColor As Color
        Get
            Return _shadow
        End Get
        Set(value As Color)
            SetValue(_shadow, value)
        End Set
    End Property

    <Category("Cores"), Description("0 a 1. Intensidade da sombra (só no Windows).")>
    Public Property ShadowOpacity As Double
        Get
            Return _shadowOpacity
        End Get
        Set(value As Double)
            SetValue(_shadowOpacity, Math.Max(0, Math.Min(1, value)))
        End Set
    End Property

    <Category("Cores por tipo")> Public Property InfoColor As Color
        Get
            Return _info
        End Get
        Set(value As Color)
            SetValue(_info, value)
        End Set
    End Property

    <Category("Cores por tipo")> Public Property SuccessColor As Color
        Get
            Return _success
        End Get
        Set(value As Color)
            SetValue(_success, value)
        End Set
    End Property

    <Category("Cores por tipo")> Public Property WarningColor As Color
        Get
            Return _warning
        End Get
        Set(value As Color)
            SetValue(_warning, value)
        End Set
    End Property

    <Category("Cores por tipo")> Public Property ErrorColor As Color
        Get
            Return _error
        End Get
        Set(value As Color)
            SetValue(_error, value)
        End Set
    End Property

    <Category("Cores por tipo")> Public Property MessageColor As Color
        Get
            Return _messageColor
        End Get
        Set(value As Color)
            SetValue(_messageColor, value)
        End Set
    End Property

    <Category("Fontes")> Public Property TitleFont As Font
        Get
            Return _titleFont
        End Get
        Set(value As Font)
            If value IsNot Nothing Then _titleFont = value : OnChanged()
        End Set
    End Property

    <Category("Fontes")> Public Property TextFont As Font
        Get
            Return _textFont
        End Get
        Set(value As Font)
            If value IsNot Nothing Then _textFont = value : OnChanged()
        End Set
    End Property

    <Category("Fontes")> Public Property CaptionFont As Font
        Get
            Return _captionFont
        End Get
        Set(value As Font)
            If value IsNot Nothing Then _captionFont = value : OnChanged()
        End Set
    End Property

    <Category("Medidas"), DefaultValue(360), Description("Largura da notificação (px a 96 DPI).")>
    Public Property Width As Integer
        Get
            Return _width
        End Get
        Set(value As Integer)
            SetValue(_width, Math.Max(200, Math.Min(800, value)))
        End Set
    End Property

    <Category("Medidas"), DefaultValue(10)> Public Property CornerRadius As Integer
        Get
            Return _radius
        End Get
        Set(value As Integer)
            SetValue(_radius, Math.Max(0, Math.Min(24, value)))
        End Set
    End Property

    <Category("Medidas"), DefaultValue(3), Description("Linhas máximas do texto antes do '...'.")>
    Public Property MaxLines As Integer
        Get
            Return _maxLines
        End Get
        Set(value As Integer)
            SetValue(_maxLines, Math.Max(1, Math.Min(12, value)))
        End Set
    End Property

    <Category("Medidas"), DefaultValue(16), Description("Tamanho da sombra (px a 96 DPI). 0 = sem sombra.")>
    Public Property ShadowSize As Integer
        Get
            Return _shadowSize
        End Get
        Set(value As Integer)
            SetValue(_shadowSize, Math.Max(0, Math.Min(40, value)))
        End Set
    End Property

    Private Sub SetValue(Of T)(ByRef field As T, value As T)
        If Object.Equals(field, value) Then Return
        field = value
        OnChanged()
    End Sub

    Protected Overridable Sub OnChanged()
        If _suspend Then Return
        RaiseEvent Changed(Me, EventArgs.Empty)
    End Sub

    Public Overrides Function ToString() As String
        Return "(Tema)"
    End Function
End Class
