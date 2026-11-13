Imports System.ComponentModel
Imports System.Drawing

''' <summary>
''' Cores, fontes e medidas de todos os controles do chat. Uma instância pode ser compartilhada
''' entre vários controles; qualquer alteração redesenha todos eles.
''' </summary>
<TypeConverter(GetType(ExpandableObjectConverter))>
Public Class ChatTheme

    Private _backColor As Color
    Private _incomingBubble As Color
    Private _incomingText As Color
    Private _outgoingBubble As Color
    Private _outgoingText As Color
    Private _metaColor As Color
    Private _outgoingMetaColor As Color
    Private _systemBack As Color
    Private _systemText As Color
    Private _accent As Color
    Private _readTicks As Color
    Private _failed As Color
    Private _headerBack As Color
    Private _headerText As Color
    Private _headerSubText As Color
    Private _composerBack As Color
    Private _inputBack As Color
    Private _inputText As Color
    Private _placeholder As Color
    Private _border As Color
    Private _stripBack As Color
    Private _tabSelectedBack As Color
    Private _tabHoverBack As Color
    Private _tabText As Color
    Private _tabSubText As Color
    Private _online As Color
    Private _messageFont As Font
    Private _metaFont As Font
    Private _titleFont As Font
    Private _bubbleRadius As Integer = 14
    Private _avatarSize As Integer = 32
    Private _maxBubbleWidth As Integer = 520
    Private _suspend As Boolean

    ''' <summary>Disparado a cada alteração (os controles se redesenham).</summary>
    Public Event Changed As EventHandler

    Public Sub New()
        _messageFont = New Font("Segoe UI", 9.75F, FontStyle.Regular)
        _metaFont = New Font("Segoe UI", 8.0F, FontStyle.Regular)
        _titleFont = New Font("Segoe UI Semibold", 10.5F, FontStyle.Regular)
        ApplyLight()
    End Sub

    ' ----------------------------------------------------------------- presets

    Public Shared Function CreateLight() As ChatTheme
        Return New ChatTheme()
    End Function

    Public Shared Function CreateDark() As ChatTheme
        Dim t As New ChatTheme()
        t.ApplyDark()
        Return t
    End Function

    Public Sub ApplyLight()
        _suspend = True
        BackColor = Color.FromArgb(244, 245, 247)
        IncomingBubbleColor = Color.White
        IncomingTextColor = Color.FromArgb(31, 35, 40)
        OutgoingBubbleColor = Color.FromArgb(37, 99, 235)
        OutgoingTextColor = Color.White
        MetaColor = Color.FromArgb(120, 128, 140)
        OutgoingMetaColor = Color.FromArgb(205, 220, 252)
        SystemBackColor = Color.FromArgb(228, 231, 236)
        SystemTextColor = Color.FromArgb(90, 98, 110)
        AccentColor = Color.FromArgb(37, 99, 235)
        ReadTicksColor = Color.FromArgb(125, 211, 252)
        FailedColor = Color.FromArgb(220, 38, 38)
        HeaderBackColor = Color.White
        HeaderTextColor = Color.FromArgb(17, 24, 39)
        HeaderSubTextColor = Color.FromArgb(107, 114, 128)
        ComposerBackColor = Color.White
        InputBackColor = Color.FromArgb(241, 243, 245)
        InputTextColor = Color.FromArgb(17, 24, 39)
        PlaceholderColor = Color.FromArgb(140, 148, 160)
        BorderColor = Color.FromArgb(226, 229, 233)
        TabStripBackColor = Color.FromArgb(233, 235, 239)
        TabSelectedBackColor = Color.White
        TabHoverBackColor = Color.FromArgb(222, 226, 231)
        TabTextColor = Color.FromArgb(31, 35, 40)
        TabSubTextColor = Color.FromArgb(107, 114, 128)
        OnlineColor = Color.FromArgb(34, 197, 94)
        _suspend = False
        OnChanged()
    End Sub

    Public Sub ApplyDark()
        _suspend = True
        BackColor = Color.FromArgb(17, 20, 24)
        IncomingBubbleColor = Color.FromArgb(36, 41, 48)
        IncomingTextColor = Color.FromArgb(230, 237, 243)
        OutgoingBubbleColor = Color.FromArgb(47, 111, 237)
        OutgoingTextColor = Color.White
        MetaColor = Color.FromArgb(139, 148, 158)
        OutgoingMetaColor = Color.FromArgb(200, 215, 250)
        SystemBackColor = Color.FromArgb(33, 38, 45)
        SystemTextColor = Color.FromArgb(150, 160, 170)
        AccentColor = Color.FromArgb(77, 141, 255)
        ReadTicksColor = Color.FromArgb(125, 211, 252)
        FailedColor = Color.FromArgb(248, 81, 73)
        HeaderBackColor = Color.FromArgb(24, 28, 33)
        HeaderTextColor = Color.FromArgb(230, 237, 243)
        HeaderSubTextColor = Color.FromArgb(139, 148, 158)
        ComposerBackColor = Color.FromArgb(24, 28, 33)
        InputBackColor = Color.FromArgb(36, 41, 48)
        InputTextColor = Color.FromArgb(230, 237, 243)
        PlaceholderColor = Color.FromArgb(120, 130, 140)
        BorderColor = Color.FromArgb(42, 48, 56)
        TabStripBackColor = Color.FromArgb(13, 16, 20)
        TabSelectedBackColor = Color.FromArgb(24, 28, 33)
        TabHoverBackColor = Color.FromArgb(30, 35, 42)
        TabTextColor = Color.FromArgb(230, 237, 243)
        TabSubTextColor = Color.FromArgb(139, 148, 158)
        OnlineColor = Color.FromArgb(46, 160, 67)
        _suspend = False
        OnChanged()
    End Sub

    ' ----------------------------------------------------------------- cores da área de mensagens

    <Category("Mensagens")> Public Property BackColor As Color
        Get
            Return _backColor
        End Get
        Set(value As Color)
            SetValue(_backColor, value)
        End Set
    End Property

    <Category("Mensagens")> Public Property IncomingBubbleColor As Color
        Get
            Return _incomingBubble
        End Get
        Set(value As Color)
            SetValue(_incomingBubble, value)
        End Set
    End Property

    <Category("Mensagens")> Public Property IncomingTextColor As Color
        Get
            Return _incomingText
        End Get
        Set(value As Color)
            SetValue(_incomingText, value)
        End Set
    End Property

    <Category("Mensagens")> Public Property OutgoingBubbleColor As Color
        Get
            Return _outgoingBubble
        End Get
        Set(value As Color)
            SetValue(_outgoingBubble, value)
        End Set
    End Property

    <Category("Mensagens")> Public Property OutgoingTextColor As Color
        Get
            Return _outgoingText
        End Get
        Set(value As Color)
            SetValue(_outgoingText, value)
        End Set
    End Property

    <Category("Mensagens"), Description("Hora e status nos balões recebidos, separadores de data.")>
    Public Property MetaColor As Color
        Get
            Return _metaColor
        End Get
        Set(value As Color)
            SetValue(_metaColor, value)
        End Set
    End Property

    <Category("Mensagens")> Public Property OutgoingMetaColor As Color
        Get
            Return _outgoingMetaColor
        End Get
        Set(value As Color)
            SetValue(_outgoingMetaColor, value)
        End Set
    End Property

    <Category("Mensagens")> Public Property SystemBackColor As Color
        Get
            Return _systemBack
        End Get
        Set(value As Color)
            SetValue(_systemBack, value)
        End Set
    End Property

    <Category("Mensagens")> Public Property SystemTextColor As Color
        Get
            Return _systemText
        End Get
        Set(value As Color)
            SetValue(_systemText, value)
        End Set
    End Property

    <Category("Geral"), Description("Botão enviar, badges, 'digitando...'.")>
    Public Property AccentColor As Color
        Get
            Return _accent
        End Get
        Set(value As Color)
            SetValue(_accent, value)
        End Set
    End Property

    <Category("Mensagens"), Description("Cor dos dois tiques de mensagem lida.")>
    Public Property ReadTicksColor As Color
        Get
            Return _readTicks
        End Get
        Set(value As Color)
            SetValue(_readTicks, value)
        End Set
    End Property

    <Category("Mensagens")> Public Property FailedColor As Color
        Get
            Return _failed
        End Get
        Set(value As Color)
            SetValue(_failed, value)
        End Set
    End Property

    ' ----------------------------------------------------------------- cabeçalho / compositor

    <Category("Cabeçalho")> Public Property HeaderBackColor As Color
        Get
            Return _headerBack
        End Get
        Set(value As Color)
            SetValue(_headerBack, value)
        End Set
    End Property

    <Category("Cabeçalho")> Public Property HeaderTextColor As Color
        Get
            Return _headerText
        End Get
        Set(value As Color)
            SetValue(_headerText, value)
        End Set
    End Property

    <Category("Cabeçalho")> Public Property HeaderSubTextColor As Color
        Get
            Return _headerSubText
        End Get
        Set(value As Color)
            SetValue(_headerSubText, value)
        End Set
    End Property

    <Category("Caixa de digitação")> Public Property ComposerBackColor As Color
        Get
            Return _composerBack
        End Get
        Set(value As Color)
            SetValue(_composerBack, value)
        End Set
    End Property

    <Category("Caixa de digitação")> Public Property InputBackColor As Color
        Get
            Return _inputBack
        End Get
        Set(value As Color)
            SetValue(_inputBack, value)
        End Set
    End Property

    <Category("Caixa de digitação")> Public Property InputTextColor As Color
        Get
            Return _inputText
        End Get
        Set(value As Color)
            SetValue(_inputText, value)
        End Set
    End Property

    <Category("Caixa de digitação")> Public Property PlaceholderColor As Color
        Get
            Return _placeholder
        End Get
        Set(value As Color)
            SetValue(_placeholder, value)
        End Set
    End Property

    <Category("Geral")> Public Property BorderColor As Color
        Get
            Return _border
        End Get
        Set(value As Color)
            SetValue(_border, value)
        End Set
    End Property

    <Category("Geral")> Public Property OnlineColor As Color
        Get
            Return _online
        End Get
        Set(value As Color)
            SetValue(_online, value)
        End Set
    End Property

    ' ----------------------------------------------------------------- abas

    <Category("Abas")> Public Property TabStripBackColor As Color
        Get
            Return _stripBack
        End Get
        Set(value As Color)
            SetValue(_stripBack, value)
        End Set
    End Property

    <Category("Abas")> Public Property TabSelectedBackColor As Color
        Get
            Return _tabSelectedBack
        End Get
        Set(value As Color)
            SetValue(_tabSelectedBack, value)
        End Set
    End Property

    <Category("Abas")> Public Property TabHoverBackColor As Color
        Get
            Return _tabHoverBack
        End Get
        Set(value As Color)
            SetValue(_tabHoverBack, value)
        End Set
    End Property

    <Category("Abas")> Public Property TabTextColor As Color
        Get
            Return _tabText
        End Get
        Set(value As Color)
            SetValue(_tabText, value)
        End Set
    End Property

    <Category("Abas")> Public Property TabSubTextColor As Color
        Get
            Return _tabSubText
        End Get
        Set(value As Color)
            SetValue(_tabSubText, value)
        End Set
    End Property

    ' ----------------------------------------------------------------- fontes e medidas

    <Category("Fontes")> Public Property MessageFont As Font
        Get
            Return _messageFont
        End Get
        Set(value As Font)
            If value IsNot Nothing AndAlso Not value.Equals(_messageFont) Then _messageFont = value : OnChanged()
        End Set
    End Property

    <Category("Fontes"), Description("Hora, nome do remetente, separadores.")>
    Public Property MetaFont As Font
        Get
            Return _metaFont
        End Get
        Set(value As Font)
            If value IsNot Nothing AndAlso Not value.Equals(_metaFont) Then _metaFont = value : OnChanged()
        End Set
    End Property

    <Category("Fontes"), Description("Título do cabeçalho e das abas.")>
    Public Property TitleFont As Font
        Get
            Return _titleFont
        End Get
        Set(value As Font)
            If value IsNot Nothing AndAlso Not value.Equals(_titleFont) Then _titleFont = value : OnChanged()
        End Set
    End Property

    <Category("Medidas"), DefaultValue(14)> Public Property BubbleRadius As Integer
        Get
            Return _bubbleRadius
        End Get
        Set(value As Integer)
            SetValue(_bubbleRadius, Math.Max(0, Math.Min(30, value)))
        End Set
    End Property

    <Category("Medidas"), DefaultValue(32)> Public Property AvatarSize As Integer
        Get
            Return _avatarSize
        End Get
        Set(value As Integer)
            SetValue(_avatarSize, Math.Max(16, Math.Min(64, value)))
        End Set
    End Property

    <Category("Medidas"), DefaultValue(520), Description("Largura máxima de um balão (px a 96 DPI).")>
    Public Property MaxBubbleWidth As Integer
        Get
            Return _maxBubbleWidth
        End Get
        Set(value As Integer)
            SetValue(_maxBubbleWidth, Math.Max(160, value))
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

''' <summary>Textos exibidos pelos controles. Troque para traduzir.</summary>
Public NotInheritable Class ChatStrings
    Private Sub New()
    End Sub

    Public Shared Property Today As String = "Hoje"
    Public Shared Property Yesterday As String = "Ontem"
    Public Shared Property Typing As String = "digitando..."
    Public Shared Property TypingNamed As String = "{0} está digitando..."
    Public Shared Property Online As String = "online"
    Public Shared Property Placeholder As String = "Digite uma mensagem"
    Public Shared Property EmptyConversation As String = "Nenhuma mensagem ainda. Diga olá!"
    Public Shared Property NoConversation As String = "Selecione uma conversa"
    Public Shared Property CopyText As String = "Copiar texto"
    Public Shared Property CopyImage As String = "Copiar imagem"
    Public Shared Property OpenAttachment As String = "Abrir anexo"
    Public Shared Property Retry As String = "Tentar novamente"
    Public Shared Property PhotoPreview As String = "Foto"
    Public Shared Property FilePreview As String = "Arquivo: {0}"
    Public Shared Property TimeFormat As String = "HH:mm"
    Public Shared Property AttachDialogTitle As String = "Enviar arquivo"
    Public Shared Property AttachButtonTip As String = "Anexar arquivo"
    Public Shared Property SendButtonTip As String = "Enviar (Enter)"
    Public Shared Property RefreshButtonTip As String = "Atualizar mensagens"
    Public Shared Property CloseTab As String = "Fechar"
End Class
