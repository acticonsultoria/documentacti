Imports System.ComponentModel
Imports System.Drawing

Public Enum ToastType
    ''' <summary>Ícone "i" azul.</summary>
    Info
    ''' <summary>Ícone de check verde.</summary>
    Success
    ''' <summary>Ícone "!" âmbar.</summary>
    Warning
    ''' <summary>Ícone "X" vermelho.</summary>
    [Error]
    ''' <summary>Avatar redondo (foto em Image ou iniciais do Title) — mensagens, menções, pessoas.</summary>
    Message
    ''' <summary>Sem ícone (ou com a Image informada, sem recorte).</summary>
    Plain
End Enum

Public Enum ToastPosition
    BottomRight
    TopRight
    BottomLeft
    TopLeft
End Enum

Public Enum ToastScreenMode
    ''' <summary>Monitor principal.</summary>
    Primary
    ''' <summary>Monitor onde está o mouse quando a primeira notificação da pilha aparece.</summary>
    UnderMouse
End Enum

Public Enum ToastCloseReason
    None
    ''' <summary>O tempo acabou.</summary>
    Timeout
    ''' <summary>Clique no X.</summary>
    CloseButton
    ''' <summary>Arrastada para o lado.</summary>
    Swiped
    ''' <summary>Clique no corpo (com CloseOnClick).</summary>
    Clicked
    ''' <summary>Toast.Close() ou ToastManager.CloseAll() por código.</summary>
    Programmatic
    ''' <summary>Clique no botão OK (ShowOkButton).</summary>
    OkButton
End Enum

Public Class ToastEventArgs
    Inherits EventArgs

    Public Sub New(toast As Toast, closeToast As Boolean)
        Me.Toast = toast
        Me.CloseToast = closeToast
    End Sub

    Public Property Toast As Toast
    ''' <summary>Fecha a notificação depois do clique (padrão = ToastManager.CloseOnClick).</summary>
    Public Property CloseToast As Boolean
End Class

Public Class ToastClosedEventArgs
    Inherits EventArgs

    Public Sub New(toast As Toast, reason As ToastCloseReason)
        Me.Toast = toast
        Me.Reason = reason
    End Sub

    Public Property Toast As Toast
    Public Property Reason As ToastCloseReason
End Class

''' <summary>
''' Uma notificação. Pode ser alterada enquanto está aberta (Title, Message, Image...): a tela é atualizada na hora.
''' Altere pela thread da UI.
''' </summary>
Public Class Toast

    Private _title As String = String.Empty
    Private _message As String = String.Empty
    Private _caption As String = String.Empty
    Private _type As ToastType = ToastType.Info
    Private _image As Image
    Private _accent As Color = Color.Empty
    Private _duration As Integer = -1
    Private _showProgress As Boolean = True
    Private _timestamp As DateTime = DateTime.Now
    Private _showOkButton As Boolean
    Private _okText As String = "OK"

    Friend Window As ToastWindow
    Friend Queued As Boolean
    Friend Owner As ToastManager
    Private _closeReason As ToastCloseReason

    Friend Event Changed As EventHandler
    Friend Event TimerRestartRequested As EventHandler

    ''' <summary>Clique no corpo da notificação.</summary>
    Public Event Click As EventHandler(Of ToastEventArgs)
    ''' <summary>A notificação foi fechada (veja e.Reason).</summary>
    Public Event Closed As EventHandler(Of ToastClosedEventArgs)
    ''' <summary>Clique no botão OK (ShowOkButton). Depois do evento a notificação fecha com Reason = OkButton.</summary>
    Public Event OkClick As EventHandler(Of ToastEventArgs)

    Public Sub New()
    End Sub

    Public Sub New(title As String, message As String, Optional type As ToastType = ToastType.Info)
        _title = If(title, String.Empty)
        _message = If(message, String.Empty)
        _type = type
    End Sub

    Public Property Title As String
        Get
            Return _title
        End Get
        Set(value As String)
            SetValue(_title, If(value, String.Empty))
        End Set
    End Property

    ''' <summary>Texto (quebra em até ToastTheme.MaxLines linhas, depois "...").</summary>
    Public Property Message As String
        Get
            Return _message
        End Get
        Set(value As String)
            SetValue(_message, If(value, String.Empty))
        End Set
    End Property

    ''' <summary>Linha pequena acima do título (ex.: nome do sistema). Vazio = ToastManager.AppCaption.</summary>
    Public Property Caption As String
        Get
            Return _caption
        End Get
        Set(value As String)
            SetValue(_caption, If(value, String.Empty))
        End Set
    End Property

    Public Property Type As ToastType
        Get
            Return _type
        End Get
        Set(value As ToastType)
            SetValue(_type, value)
        End Set
    End Property

    ''' <summary>Ícone/imagem próprio. Em Type = Message vira avatar redondo; nos outros tipos substitui o ícone padrão.</summary>
    Public Property Image As Image
        Get
            Return _image
        End Get
        Set(value As Image)
            If Not Object.ReferenceEquals(_image, value) Then
                _image = value
                OnChanged()
            End If
        End Set
    End Property

    ''' <summary>Cor do ícone, das iniciais e da barra de tempo. Color.Empty = cor do tipo no tema.</summary>
    Public Property AccentColor As Color
        Get
            Return _accent
        End Get
        Set(value As Color)
            SetValue(_accent, value)
        End Set
    End Property

    ''' <summary>Tempo na tela em ms. -1 = ToastManager.DefaultDuration; 0 = fica até o usuário fechar.</summary>
    Public Property Duration As Integer
        Get
            Return _duration
        End Get
        Set(value As Integer)
            SetValue(_duration, Math.Max(-1, value))
        End Set
    End Property

    ''' <summary>Mostra a barrinha de tempo restante.</summary>
    Public Property ShowProgress As Boolean
        Get
            Return _showProgress
        End Get
        Set(value As Boolean)
            SetValue(_showProgress, value)
        End Set
    End Property

    ''' <summary>Hora exibida ao lado do Caption.</summary>
    Public Property Timestamp As DateTime
        Get
            Return _timestamp
        End Get
        Set(value As DateTime)
            SetValue(_timestamp, value)
        End Set
    End Property

    ''' <summary>Mostra o botão OK (com check) no canto inferior direito. Use para "marcar como lida".</summary>
    Public Property ShowOkButton As Boolean
        Get
            Return _showOkButton
        End Get
        Set(value As Boolean)
            SetValue(_showOkButton, value)
        End Set
    End Property

    ''' <summary>Texto do botão OK (padrão "OK").</summary>
    Public Property OkText As String
        Get
            Return _okText
        End Get
        Set(value As String)
            SetValue(_okText, If(String.IsNullOrEmpty(value), "OK", value))
        End Set
    End Property

    ''' <summary>Qualquer objeto seu (ex.: o registro que deve ser aberto ao clicar).</summary>
    Public Property Tag As Object

    ''' <summary>Está na tela (ou entrando/saindo).</summary>
    Public ReadOnly Property IsOpen As Boolean
        Get
            Return Window IsNot Nothing
        End Get
    End Property

    ''' <summary>Aguardando espaço na pilha (MaxVisible atingido).</summary>
    Public ReadOnly Property IsQueued As Boolean
        Get
            Return Queued
        End Get
    End Property

    Public ReadOnly Property CloseReason As ToastCloseReason
        Get
            Return _closeReason
        End Get
    End Property

    ''' <summary>Fecha (com animação) ou tira da fila.</summary>
    Public Sub Close()
        If Owner IsNot Nothing Then Owner.CloseToast(Me, ToastCloseReason.Programmatic)
    End Sub

    ''' <summary>Recomeça a contagem de tempo (ex.: ao atualizar o texto de uma notificação aberta).</summary>
    Public Sub RestartTimer()
        RaiseEvent TimerRestartRequested(Me, EventArgs.Empty)
    End Sub

    Friend Sub RaiseClick(e As ToastEventArgs)
        RaiseEvent Click(Me, e)
    End Sub

    Friend Sub RaiseOkClick(e As ToastEventArgs)
        RaiseEvent OkClick(Me, e)
    End Sub

    Friend Sub RaiseClosed(reason As ToastCloseReason)
        _closeReason = reason
        RaiseEvent Closed(Me, New ToastClosedEventArgs(Me, reason))
    End Sub

    Friend Sub ResetForShow()
        _closeReason = ToastCloseReason.None
    End Sub

    Private Sub SetValue(Of T)(ByRef field As T, value As T)
        If Object.Equals(field, value) Then Return
        field = value
        OnChanged()
    End Sub

    Private Sub OnChanged()
        RaiseEvent Changed(Me, EventArgs.Empty)
    End Sub

    Public Overrides Function ToString() As String
        Return _title
    End Function
End Class
