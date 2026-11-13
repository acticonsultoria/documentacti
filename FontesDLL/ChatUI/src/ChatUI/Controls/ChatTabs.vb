Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Enum ChatTabStyle
    ''' <summary>Abas no topo, estilo navegador (com X para fechar).</summary>
    Tabs
    ''' <summary>Lista de conversas à esquerda, estilo WhatsApp/Teams (avatar, prévia, hora, não lidas).</summary>
    List
End Enum

''' <summary>
''' Várias conversas em abas, com um único <see cref="ChatBox"/> que troca de conversa (leve: um só conjunto de handles).
''' Badges de não lidas, "digitando..." na aba, Ctrl+Tab / Ctrl+Shift+Tab / Ctrl+W.
''' </summary>
<ToolboxItem(True), DefaultEvent("MessageSubmitted"), Description("Chat com várias conversas em abas.")>
Public Class ChatTabs
    Inherits UserControl

    Private ReadOnly _strip As ChatTabStrip
    Private ReadOnly _box As ChatBox
    Private ReadOnly _conversations As New ChatConversationCollection()
    Private ReadOnly _hooked As New List(Of ChatConversation)()
    Private _selected As ChatConversation
    Private _style As ChatTabStyle = ChatTabStyle.Tabs
    Private _listWidth As Integer = 290
    Private _showClose As Boolean = True
    Private _listTitle As String = "Conversas"
    Private _form As Form

#Region "Eventos"

    Public Event SelectedConversationChanged As EventHandler(Of ChatConversationEventArgs)
    ''' <summary>Antes de fechar uma aba (e.Cancel = True impede).</summary>
    Public Event ConversationClosing As EventHandler(Of ChatConversationCancelEventArgs)
    Public Event ConversationClosed As EventHandler(Of ChatConversationEventArgs)
    ''' <summary>Mensagem enviada pelo usuário em qualquer aba (e.Conversation diz qual).</summary>
    Public Event MessageSubmitted As EventHandler(Of ChatMessageSubmittedEventArgs)
    ''' <summary>Mensagem recebida adicionada em qualquer conversa (bom para notificar / piscar a janela).</summary>
    Public Event MessageReceived As EventHandler(Of ChatMessageSubmittedEventArgs)
    ''' <summary>O usuário local começou/parou de digitar na conversa e.Conversation.</summary>
    Public Event TypingStateChanged As EventHandler(Of ChatTypingEventArgs)

#End Region

    Public Sub New()
        _box = New ChatBox() With {.Dock = DockStyle.Fill}
        _strip = New ChatTabStrip(Me) With {.Dock = DockStyle.Top, .Height = 42}
        Controls.Add(_box)
        Controls.Add(_strip)
        _strip.Theme = _box.Theme

        AddHandler _conversations.CollectionChanged, AddressOf OnConversationsChanged
        AddHandler _conversations.ItemChanged, AddressOf OnConversationItemChanged
        AddHandler _box.MessageSubmitted, Sub(s, e) RaiseEvent MessageSubmitted(Me, e)
        AddHandler _box.TypingStateChanged, Sub(s, e) RaiseEvent TypingStateChanged(Me, e)
        Size = New Size(720, 520)
    End Sub

#Region "Propriedades"

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public ReadOnly Property Conversations As ChatConversationCollection
        Get
            Return _conversations
        End Get
    End Property

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property SelectedConversation As ChatConversation
        Get
            Return _selected
        End Get
        Set(value As ChatConversation)
            If Object.ReferenceEquals(value, _selected) Then Return
            If value IsNot Nothing AndAlso Not _conversations.Contains(value) Then Return
            _selected = value
            _box.Conversation = value
            If value IsNot Nothing Then value.UnreadCount = 0
            _strip.EnsureVisible(value)
            _strip.Invalidate()
            RaiseEvent SelectedConversationChanged(Me, New ChatConversationEventArgs(value))
        End Set
    End Property

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property SelectedIndex As Integer
        Get
            Return If(_selected Is Nothing, -1, _conversations.IndexOf(_selected))
        End Get
        Set(value As Integer)
            If value >= 0 AndAlso value < _conversations.Count Then SelectedConversation = _conversations(value)
        End Set
    End Property

    <Category("Aparência"), DefaultValue(GetType(ChatTabStyle), "Tabs"), Description("Abas no topo ou lista de conversas à esquerda.")>
    Public Property TabStyle As ChatTabStyle
        Get
            Return _style
        End Get
        Set(value As ChatTabStyle)
            If _style = value Then Return
            _style = value
            ApplyStyle()
        End Set
    End Property

    <Category("Aparência"), DefaultValue(290), Description("Largura da lista no estilo List (px a 96 DPI).")>
    Public Property ListWidth As Integer
        Get
            Return _listWidth
        End Get
        Set(value As Integer)
            _listWidth = Math.Max(160, value)
            ApplyStyle()
        End Set
    End Property

    <Category("Aparência"), DefaultValue("Conversas"), Description("Título exibido no topo da lista (estilo List). Vazio = sem título.")>
    Public Property ListTitle As String
        Get
            Return _listTitle
        End Get
        Set(value As String)
            _listTitle = value
            _strip.Invalidate()
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(True)>
    Public Property ShowCloseButtons As Boolean
        Get
            Return _showClose
        End Get
        Set(value As Boolean)
            _showClose = value
            _strip.Invalidate()
        End Set
    End Property

    <Category("Aparência"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property Theme As ChatTheme
        Get
            Return _box.Theme
        End Get
        Set(value As ChatTheme)
            If value Is Nothing Then Return
            _box.Theme = value
            _strip.Theme = value
        End Set
    End Property

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property CurrentUser As ChatUser
        Get
            Return _box.CurrentUser
        End Get
        Set(value As ChatUser)
            _box.CurrentUser = value
        End Set
    End Property

    ''' <summary>O ChatBox interno (para assinar AttachRequested, ImageClick, etc.).</summary>
    <Browsable(False)>
    Public ReadOnly Property ChatBox As ChatBox
        Get
            Return _box
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property TotalUnread As Integer
        Get
            Dim n = 0
            For Each c In _conversations
                n += c.UnreadCount
            Next
            Return n
        End Get
    End Property

#End Region

#Region "Métodos"

    ''' <summary>Abre (ou reaproveita, pelo Id do contato) uma conversa com o contato.</summary>
    Public Function AddConversation(contact As ChatUser, Optional selectIt As Boolean = True) As ChatConversation
        Dim conv = If(contact Is Nothing, Nothing, _conversations.FindById(contact.Id))
        If conv Is Nothing Then
            conv = New ChatConversation(contact)
            _conversations.Add(conv)
        End If
        If selectIt Then SelectedConversation = conv
        Return conv
    End Function

    Public Function AddConversation(conversation As ChatConversation, Optional selectIt As Boolean = True) As ChatConversation
        If Not _conversations.Contains(conversation) Then _conversations.Add(conversation)
        If selectIt Then SelectedConversation = conversation
        Return conversation
    End Function

    ''' <summary>Fecha a aba (dispara ConversationClosing/Closed). Retorna False se foi cancelado.</summary>
    Public Function CloseConversation(conversation As ChatConversation) As Boolean
        If conversation Is Nothing OrElse Not _conversations.Contains(conversation) Then Return False
        Dim e As New ChatConversationCancelEventArgs(conversation)
        RaiseEvent ConversationClosing(Me, e)
        If e.Cancel Then Return False
        _conversations.Remove(conversation)
        RaiseEvent ConversationClosed(Me, New ChatConversationEventArgs(conversation))
        Return True
    End Function

    Public Sub SelectNext(forward As Boolean)
        If _conversations.Count = 0 Then Return
        Dim i = SelectedIndex
        i = If(forward, i + 1, i - 1)
        If i >= _conversations.Count Then i = 0
        If i < 0 Then i = _conversations.Count - 1
        SelectedIndex = i
    End Sub

#End Region

#Region "Internos"

    Private Sub ApplyStyle()
        If _style = ChatTabStyle.Tabs Then
            _strip.Dock = DockStyle.Top
            _strip.Height = _strip.Scaled(42)
        Else
            _strip.Dock = DockStyle.Left
            _strip.Width = _strip.Scaled(_listWidth)
        End If
        _strip.ResetScroll()
        _strip.Invalidate()
    End Sub

    Private Sub OnConversationsChanged(sender As Object, e As ChatCollectionChangedEventArgs(Of ChatConversation))
        If InvokeRequired Then
            BeginInvoke(New Action(Sub() OnConversationsChanged(sender, e)))
            Return
        End If
        Dim removedIndex = If(e.Action = ChatCollectionAction.Remove, e.Index, -1)
        ' sincroniza assinaturas
        For i = _hooked.Count - 1 To 0 Step -1
            Dim c = _hooked(i)
            If Not _conversations.Contains(c) Then
                RemoveHandler c.Messages.CollectionChanged, AddressOf OnAnyMessagesChanged
                RemoveHandler c.Messages.ItemChanged, AddressOf OnAnyMessageItemChanged
                _hooked.RemoveAt(i)
            End If
        Next
        For Each c In _conversations
            If Not _hooked.Contains(c) Then
                AddHandler c.Messages.CollectionChanged, AddressOf OnAnyMessagesChanged
                AddHandler c.Messages.ItemChanged, AddressOf OnAnyMessageItemChanged
                _hooked.Add(c)
            End If
        Next
        ' seleção
        If _selected IsNot Nothing AndAlso Not _conversations.Contains(_selected) Then
            Dim idx = If(removedIndex >= 0, Math.Min(removedIndex, _conversations.Count - 1), 0)
            _selected = Nothing
            SelectedConversation = If(_conversations.Count > 0 AndAlso idx >= 0, _conversations(idx), Nothing)
            If _selected Is Nothing Then
                _box.Conversation = Nothing
                RaiseEvent SelectedConversationChanged(Me, New ChatConversationEventArgs(Nothing))
            End If
        ElseIf _selected Is Nothing AndAlso _conversations.Count > 0 Then
            SelectedConversation = _conversations(0)
        End If
        _strip.Invalidate()
    End Sub

    Private Sub OnConversationItemChanged(sender As Object, e As ChatItemChangedEventArgs(Of ChatConversation))
        If IsDisposed Then Return
        If InvokeRequired Then
            BeginInvoke(New Action(AddressOf _strip.Invalidate))
        Else
            _strip.Invalidate()
        End If
    End Sub

    Private Function FindByMessages(msgs As Object) As ChatConversation
        For Each c In _conversations
            If Object.ReferenceEquals(c.Messages, msgs) Then Return c
        Next
        Return Nothing
    End Function

    Private Function IsUserLooking(conv As ChatConversation) As Boolean
        If Not Object.ReferenceEquals(conv, _selected) Then Return False
        Dim f = FindForm()
        Return f Is Nothing OrElse Object.ReferenceEquals(Form.ActiveForm, f)
    End Function

    Private Sub OnAnyMessagesChanged(sender As Object, e As ChatCollectionChangedEventArgs(Of ChatMessage))
        If IsDisposed Then Return
        If InvokeRequired Then
            BeginInvoke(New Action(Sub() OnAnyMessagesChanged(sender, e)))
            Return
        End If
        Dim conv = FindByMessages(sender)
        If conv IsNot Nothing AndAlso e.Action = ChatCollectionAction.Add AndAlso e.Item IsNot Nothing AndAlso
           e.Item.Direction = MessageDirection.Incoming AndAlso e.Index >= conv.Messages.Count - 1 Then
            If Not IsUserLooking(conv) Then conv.UnreadCount += 1
            RaiseEvent MessageReceived(Me, New ChatMessageSubmittedEventArgs(conv, e.Item))
        End If
        _strip.Invalidate()
    End Sub

    Private Sub OnAnyMessageItemChanged(sender As Object, e As ChatItemChangedEventArgs(Of ChatMessage))
        If IsDisposed Then Return
        If InvokeRequired Then
            BeginInvoke(New Action(AddressOf _strip.Invalidate))
        Else
            _strip.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnParentChanged(e As EventArgs)
        MyBase.OnParentChanged(e)
        HookForm()
    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        HookForm()
        ApplyStyle()
    End Sub

    Private Sub HookForm()
        Dim f = FindForm()
        If Object.ReferenceEquals(f, _form) Then Return
        If _form IsNot Nothing Then RemoveHandler _form.Activated, AddressOf OnFormActivated
        _form = f
        If _form IsNot Nothing Then AddHandler _form.Activated, AddressOf OnFormActivated
    End Sub

    Private Sub OnFormActivated(sender As Object, e As EventArgs)
        If _selected IsNot Nothing Then _selected.UnreadCount = 0
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Control Or Keys.Tab
                SelectNext(True) : Return True
            Case Keys.Control Or Keys.Shift Or Keys.Tab
                SelectNext(False) : Return True
            Case Keys.Control Or Keys.W, Keys.Control Or Keys.F4
                If _showClose AndAlso _selected IsNot Nothing Then CloseConversation(_selected)
                Return True
        End Select
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            If _form IsNot Nothing Then RemoveHandler _form.Activated, AddressOf OnFormActivated
            For Each c In _hooked
                RemoveHandler c.Messages.CollectionChanged, AddressOf OnAnyMessagesChanged
                RemoveHandler c.Messages.ItemChanged, AddressOf OnAnyMessageItemChanged
            Next
            _hooked.Clear()
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

End Class
