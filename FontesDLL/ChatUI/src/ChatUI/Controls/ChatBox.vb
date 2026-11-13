Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

''' <summary>
''' Chat completo de UMA conversa: cabeçalho + mensagens + caixa de digitação.
''' Já funciona sozinho: enviar texto, anexar (diálogo), arrastar arquivos e colar imagens.
''' Para várias conversas em abas use <see cref="ChatTabs"/>.
''' </summary>
<ToolboxItem(True), DefaultEvent("MessageSubmitted"), Description("Chat de uma conversa (cabeçalho, mensagens e digitação).")>
Public Class ChatBox
    Inherits UserControl

    Private ReadOnly _header As ChatHeader
    Private ReadOnly _view As ChatView
    Private ReadOnly _composer As ChatComposer
    Private _theme As ChatTheme
    Private _conversation As ChatConversation
    Private _currentUser As ChatUser
    Private _autoAdd As Boolean = True
    ' Guardados à parte: Control.Visible devolve False enquanto o controle não está na tela,
    ' e o designer do Visual Studio gravaria False por engano.
    Private _showHeader As Boolean = True
    Private _showComposer As Boolean = True

#Region "Eventos"

    ''' <summary>Antes de criar a mensagem enviada (pode alterar o texto ou cancelar).</summary>
    Public Event MessageSubmitting As EventHandler(Of ChatMessageSubmittingEventArgs)
    ''' <summary>O usuário enviou uma mensagem. Ela já está na conversa com Status = Sending:
    ''' mande para o seu servidor e depois atualize e.Message.Status.</summary>
    Public Event MessageSubmitted As EventHandler(Of ChatMessageSubmittedEventArgs)
    ''' <summary>Botão de anexo. Padrão (Handled = False): abre um OpenFileDialog e envia os arquivos.</summary>
    Public Event AttachRequested As EventHandler(Of ChatHandledEventArgs)
    ''' <summary>Botão de atualizar (ShowRefreshButton): o sistema busca as mensagens novas da conversa.</summary>
    Public Event RefreshRequested As EventHandler(Of ChatConversationEventArgs)
    ''' <summary>Arquivos arrastados para o chat. Padrão (Handled = False): envia cada arquivo.</summary>
    Public Event FilesDropped As EventHandler(Of ChatFilesEventArgs)
    ''' <summary>Imagem colada com Ctrl+V. Padrão (Handled = False): envia a imagem.</summary>
    Public Event ImagePasted As EventHandler(Of ChatImageEventArgs)
    ''' <summary>O usuário local começou/parou de digitar.</summary>
    Public Event TypingStateChanged As EventHandler(Of ChatTypingEventArgs)
    Public Event MessageClick As EventHandler(Of ChatMessageEventArgs)
    Public Event MessageDoubleClick As EventHandler(Of ChatMessageEventArgs)
    Public Event ImageClick As EventHandler(Of ChatMessageEventArgs)
    Public Event AttachmentClick As EventHandler(Of ChatMessageEventArgs)
    Public Event RetryRequested As EventHandler(Of ChatMessageEventArgs)
    Public Event MessageContextMenuOpening As EventHandler(Of ChatContextMenuEventArgs)
    Public Event LoadOlderRequested As EventHandler(Of ChatConversationEventArgs)

#End Region

    Public Sub New()
        _theme = New ChatTheme()
        _currentUser = New ChatUser("me", "Eu")

        _view = New ChatView() With {.Dock = DockStyle.Fill, .Theme = _theme, .AllowDrop = True}
        _composer = New ChatComposer() With {.Dock = DockStyle.Bottom, .Theme = _theme}
        _header = New ChatHeader() With {.Dock = DockStyle.Top, .Theme = _theme}
        ' ordem importa no docking: o Fill precisa ser o primeiro da coleção
        Controls.Add(_view)
        Controls.Add(_composer)
        Controls.Add(_header)

        AddHandler _composer.SendRequested, AddressOf OnComposerSend
        AddHandler _composer.AttachRequested, Sub(s, e) OnAttachRequested(New ChatHandledEventArgs(_conversation))
        AddHandler _composer.RefreshRequested, Sub(s, e) RaiseEvent RefreshRequested(Me, New ChatConversationEventArgs(_conversation))
        AddHandler _composer.ImagePasted, Sub(s, e) OnImagePasted(New ChatImageEventArgs(_conversation, e.Image))
        AddHandler _composer.TypingStateChanged, Sub(s, e) RaiseEvent TypingStateChanged(Me, New ChatTypingEventArgs(_conversation, e.IsTyping))
        AddHandler _composer.NavigationKey, Sub(s, e) _view.HandleNavigationKey(e.KeyCode)

        AddHandler _view.MessageClick, Sub(s, e) RaiseEvent MessageClick(Me, e)
        AddHandler _view.MessageDoubleClick, Sub(s, e) RaiseEvent MessageDoubleClick(Me, e)
        AddHandler _view.ImageClick, Sub(s, e) RaiseEvent ImageClick(Me, e)
        AddHandler _view.AttachmentClick, Sub(s, e) RaiseEvent AttachmentClick(Me, e)
        AddHandler _view.RetryRequested, Sub(s, e) RaiseEvent RetryRequested(Me, e)
        AddHandler _view.MessageContextMenuOpening, Sub(s, e) RaiseEvent MessageContextMenuOpening(Me, e)
        AddHandler _view.LoadOlderRequested, Sub(s, e) RaiseEvent LoadOlderRequested(Me, e)
        AddHandler _view.KeyPress, AddressOf OnViewKeyPress
        AddHandler _view.DragEnter, AddressOf OnDragEnterInternal
        AddHandler _view.DragDrop, AddressOf OnDragDropInternal

        _composer.Enabled = False
        Size = New Size(380, 520)
    End Sub

#Region "Propriedades"

    ''' <summary>Conversa exibida. Ao trocar, o rascunho e a posição de rolagem são guardados na conversa anterior.</summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Conversation As ChatConversation
        Get
            Return _conversation
        End Get
        Set(value As ChatConversation)
            If Object.ReferenceEquals(value, _conversation) Then Return
            If _conversation IsNot Nothing Then _conversation.Draft = _composer.Text
            _conversation = value
            _view.Conversation = value
            _header.Conversation = value
            _composer.Text = If(value Is Nothing, String.Empty, value.Draft)
            _composer.Enabled = value IsNot Nothing
        End Set
    End Property

    ''' <summary>Usuário local (remetente das mensagens enviadas).</summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property CurrentUser As ChatUser
        Get
            Return _currentUser
        End Get
        Set(value As ChatUser)
            If value IsNot Nothing Then _currentUser = value
        End Set
    End Property

    <Category("Aparência"), Description("Cores, fontes e medidas (compartilhado com as partes internas)."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property Theme As ChatTheme
        Get
            Return _theme
        End Get
        Set(value As ChatTheme)
            If value Is Nothing Then Return
            _theme = value
            _view.Theme = value
            _composer.Theme = value
            _header.Theme = value
        End Set
    End Property

    <Category("Aparência"), DefaultValue(True)>
    Public Property ShowHeader As Boolean
        Get
            Return _showHeader
        End Get
        Set(value As Boolean)
            _showHeader = value
            _header.Visible = value
        End Set
    End Property

    <Category("Aparência"), DefaultValue(True)>
    Public Property ShowComposer As Boolean
        Get
            Return _showComposer
        End Get
        Set(value As Boolean)
            _showComposer = value
            _composer.Visible = value
        End Set
    End Property

    <Category("Aparência"), DefaultValue(True)>
    Public Property ShowAvatars As Boolean
        Get
            Return _view.ShowAvatars
        End Get
        Set(value As Boolean)
            _view.ShowAvatars = value
        End Set
    End Property

    <Category("Aparência"), DefaultValue(GetType(SenderNameMode), "Auto")>
    Public Property SenderNames As SenderNameMode
        Get
            Return _view.SenderNames
        End Get
        Set(value As SenderNameMode)
            _view.SenderNames = value
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(True)>
    Public Property ShowAttachButton As Boolean
        Get
            Return _composer.ShowAttachButton
        End Get
        Set(value As Boolean)
            _composer.ShowAttachButton = value
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(False), Description("Mostra o botão de atualizar ao lado do botão enviar.")>
    Public Property ShowRefreshButton As Boolean
        Get
            Return _composer.ShowRefreshButton
        End Get
        Set(value As Boolean)
            _composer.ShowRefreshButton = value
        End Set
    End Property

    <Category("Comportamento"), DefaultValue(True), Description("Adiciona automaticamente à conversa as mensagens enviadas.")>
    Public Property AutoAddOutgoing As Boolean
        Get
            Return _autoAdd
        End Get
        Set(value As Boolean)
            _autoAdd = value
        End Set
    End Property

    <Browsable(False)> Public ReadOnly Property View As ChatView
        Get
            Return _view
        End Get
    End Property

    <Browsable(False)> Public ReadOnly Property Composer As ChatComposer
        Get
            Return _composer
        End Get
    End Property

    <Browsable(False)> Public ReadOnly Property Header As ChatHeader
        Get
            Return _header
        End Get
    End Property

#End Region

#Region "Enviar"

    ''' <summary>Envia um texto como se o usuário tivesse digitado. Retorna a mensagem criada (ou Nothing se cancelado).</summary>
    Public Function SendText(text As String) As ChatMessage
        If _conversation Is Nothing OrElse String.IsNullOrEmpty(text) Then Return Nothing
        Dim a As New ChatMessageSubmittingEventArgs(_conversation, text)
        RaiseEvent MessageSubmitting(Me, a)
        If a.Cancel OrElse String.IsNullOrEmpty(a.Text) Then Return Nothing
        Dim m As New ChatMessage(_currentUser, a.Text, MessageDirection.Outgoing) With {.Status = MessageStatus.Sending}
        Return Submit(m)
    End Function

    Public Function SendImage(img As Image, Optional caption As String = Nothing) As ChatMessage
        If _conversation Is Nothing OrElse img Is Nothing Then Return Nothing
        Dim m As New ChatMessage(_currentUser, caption, MessageDirection.Outgoing) With {.Status = MessageStatus.Sending, .Image = img}
        Return Submit(m)
    End Function

    ''' <summary>Envia um arquivo. Imagens (png/jpg/gif/bmp) aparecem como foto; o resto como cartão de anexo.</summary>
    Public Function SendFile(path As String, Optional caption As String = Nothing) As ChatMessage
        If _conversation Is Nothing OrElse Not IO.File.Exists(path) Then Return Nothing
        Dim att = ChatAttachment.FromFile(path)
        Dim m As New ChatMessage(_currentUser, caption, MessageDirection.Outgoing) With {.Status = MessageStatus.Sending, .Attachment = att}
        If Array.IndexOf(ImageExtensions(), IO.Path.GetExtension(path).ToLowerInvariant()) >= 0 Then
            Try
                m.Image = LoadImageUnlocked(path)
            Catch
                ' não é uma imagem válida: fica como anexo
            End Try
        End If
        Return Submit(m)
    End Function

    Private Function Submit(m As ChatMessage) As ChatMessage
        Dim conv = _conversation
        If _autoAdd Then conv.Messages.Add(m)
        RaiseEvent MessageSubmitted(Me, New ChatMessageSubmittedEventArgs(conv, m))
        Return m
    End Function

    Public Sub FocusComposer()
        _composer.FocusInput()
    End Sub

    Public Sub ScrollToBottom()
        _view.ScrollToBottom()
    End Sub

#End Region

#Region "Comportamentos padrão"

    Private Sub OnComposerSend(sender As Object, e As ChatSendEventArgs)
        e.Cancel = (SendText(e.Text) Is Nothing)
    End Sub

    Protected Overridable Sub OnAttachRequested(e As ChatHandledEventArgs)
        RaiseEvent AttachRequested(Me, e)
        If e.Handled OrElse _conversation Is Nothing Then Return
        Using dlg As New OpenFileDialog()
            dlg.Title = ChatStrings.AttachDialogTitle
            dlg.Multiselect = True
            dlg.Filter = "Todos os arquivos (*.*)|*.*|Imagens|*.png;*.jpg;*.jpeg;*.gif;*.bmp"
            If dlg.ShowDialog(FindForm()) = DialogResult.OK Then
                For Each f In dlg.FileNames
                    SendFile(f)
                Next
            End If
        End Using
        _composer.FocusInput()
    End Sub

    Protected Overridable Sub OnImagePasted(e As ChatImageEventArgs)
        RaiseEvent ImagePasted(Me, e)
        If Not e.Handled Then SendImage(e.Image)
    End Sub

    Protected Overridable Sub OnFilesDropped(e As ChatFilesEventArgs)
        RaiseEvent FilesDropped(Me, e)
        If e.Handled Then Return
        For Each f In e.Files
            SendFile(f)
        Next
    End Sub

    Private Sub OnDragEnterInternal(sender As Object, e As DragEventArgs)
        If _conversation IsNot Nothing AndAlso e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub OnDragDropInternal(sender As Object, e As DragEventArgs)
        Dim files = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        If files Is Nothing OrElse files.Length = 0 OrElse _conversation Is Nothing Then Return
        OnFilesDropped(New ChatFilesEventArgs(_conversation, files))
    End Sub

    ''' <summary>Digitar com o foco na lista de mensagens manda o caractere para a caixa de texto.</summary>
    Private Sub OnViewKeyPress(sender As Object, e As KeyPressEventArgs)
        If _composer.Enabled AndAlso _composer.Visible AndAlso Not Char.IsControl(e.KeyChar) Then
            _composer.FocusInput()
            _composer.InsertText(e.KeyChar.ToString())
            e.Handled = True
        End If
    End Sub

#End Region

End Class
