Imports System.ComponentModel
Imports System.Drawing

''' <summary>
''' Uma conversa (aba). Contém o contato, as mensagens, o rascunho digitado e o estado "digitando...".
''' </summary>
Public Class ChatConversation
    Implements INotifyPropertyChanged

    Private _id As String
    Private _title As String
    Private _contact As ChatUser
    Private _unread As Integer
    Private _isTyping As Boolean
    Private _typingUser As ChatUser
    Private _isGroup As Boolean
    Private ReadOnly _messages As New ChatMessageCollection()

    ' Estado de rolagem salvo quando a conversa sai da tela (uso interno).
    Friend SavedScroll As Integer = -1   ' -1 = estava no fim

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New()
        _id = Guid.NewGuid().ToString("N")
    End Sub

    Public Sub New(contact As ChatUser)
        Me.New()
        _contact = contact
        If contact IsNot Nothing AndAlso Not String.IsNullOrEmpty(contact.Id) Then _id = contact.Id
        If _contact IsNot Nothing Then AddHandler _contact.PropertyChanged, AddressOf OnContactChanged
    End Sub

    Public Sub New(id As String, title As String, isGroup As Boolean)
        _id = If(String.IsNullOrEmpty(id), Guid.NewGuid().ToString("N"), id)
        _title = title
        _isGroup = isGroup
    End Sub

    Public Property Id As String
        Get
            Return _id
        End Get
        Set(value As String)
            _id = value
        End Set
    End Property

    ''' <summary>Título da aba/cabeçalho. Se vazio, usa Contact.DisplayName.</summary>
    Public Property Title As String
        Get
            If String.IsNullOrEmpty(_title) AndAlso _contact IsNot Nothing Then Return _contact.DisplayName
            Return If(_title, String.Empty)
        End Get
        Set(value As String)
            If _title <> value Then _title = value : OnPropertyChanged("Title")
        End Set
    End Property

    ''' <summary>Contato principal (avatar e status do cabeçalho). Em grupos pode ser um ChatUser representando o grupo.</summary>
    Public Property Contact As ChatUser
        Get
            Return _contact
        End Get
        Set(value As ChatUser)
            If Object.ReferenceEquals(_contact, value) Then Return
            If _contact IsNot Nothing Then RemoveHandler _contact.PropertyChanged, AddressOf OnContactChanged
            _contact = value
            If _contact IsNot Nothing Then AddHandler _contact.PropertyChanged, AddressOf OnContactChanged
            OnPropertyChanged("Contact")
        End Set
    End Property

    ''' <summary>Em grupos, o nome do remetente aparece em cima de cada bloco de mensagens recebidas.</summary>
    Public Property IsGroup As Boolean
        Get
            Return _isGroup
        End Get
        Set(value As Boolean)
            If _isGroup <> value Then _isGroup = value : OnPropertyChanged("IsGroup")
        End Set
    End Property

    Public ReadOnly Property Messages As ChatMessageCollection
        Get
            Return _messages
        End Get
    End Property

    Public Property UnreadCount As Integer
        Get
            Return _unread
        End Get
        Set(value As Integer)
            value = Math.Max(0, value)
            If _unread <> value Then _unread = value : OnPropertyChanged("UnreadCount")
        End Set
    End Property

    ''' <summary>Mostra o indicador animado "digitando..." no fim da conversa e no cabeçalho.</summary>
    Public Property IsTyping As Boolean
        Get
            Return _isTyping
        End Get
        Set(value As Boolean)
            If _isTyping <> value Then _isTyping = value : OnPropertyChanged("IsTyping")
        End Set
    End Property

    ''' <summary>Quem está digitando (opcional; usado para o avatar do indicador). Padrão: Contact.</summary>
    Public Property TypingUser As ChatUser
        Get
            Return If(_typingUser, _contact)
        End Get
        Set(value As ChatUser)
            _typingUser = value
            OnPropertyChanged("TypingUser")
        End Set
    End Property

    ''' <summary>Texto que estava sendo digitado quando o usuário trocou de aba.</summary>
    Public Property Draft As String

    Public Property Tag As Object

    ' ---------- atalhos ----------

    Public Function AddIncoming(text As String) As ChatMessage
        Return AddIncoming(_contact, text)
    End Function

    Public Function AddIncoming(sender As ChatUser, text As String) As ChatMessage
        Dim m As New ChatMessage(sender, text, MessageDirection.Incoming)
        _messages.Add(m)
        Return m
    End Function

    Public Function AddOutgoing(sender As ChatUser, text As String, Optional status As MessageStatus = MessageStatus.Sending) As ChatMessage
        Dim m As New ChatMessage(sender, text, MessageDirection.Outgoing)
        m.Status = status
        _messages.Add(m)
        Return m
    End Function

    Public Function AddSystem(text As String) As ChatMessage
        Dim m As New ChatMessage(Nothing, text, MessageDirection.System)
        _messages.Add(m)
        Return m
    End Function

    Public Function AddImage(sender As ChatUser, direction As MessageDirection, img As Image, Optional caption As String = Nothing) As ChatMessage
        Dim m As New ChatMessage(sender, caption, direction)
        m.Image = img
        If direction = MessageDirection.Outgoing Then m.Status = MessageStatus.Sending
        _messages.Add(m)
        Return m
    End Function

    Public Function AddFile(sender As ChatUser, direction As MessageDirection, attachment As ChatAttachment, Optional caption As String = Nothing) As ChatMessage
        Dim m As New ChatMessage(sender, caption, direction)
        m.Attachment = attachment
        If direction = MessageDirection.Outgoing Then m.Status = MessageStatus.Sending
        _messages.Add(m)
        Return m
    End Function

    Private Sub OnContactChanged(sender As Object, e As PropertyChangedEventArgs)
        OnPropertyChanged("Contact." & e.PropertyName)
    End Sub

    Protected Overridable Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub

    Public Overrides Function ToString() As String
        Return Title
    End Function
End Class
