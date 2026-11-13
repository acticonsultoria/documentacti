Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

''' <summary>Parte do balão que foi clicada.</summary>
Public Enum ChatHitArea
    None
    Bubble
    Text
    Image
    Attachment
    Avatar
    SenderName
    ''' <summary>Ícone de status (ex.: clicar no "!" de falha para reenviar).</summary>
    Status
End Enum

Public Class ChatMessageEventArgs
    Inherits EventArgs

    Public Sub New(message As ChatMessage, area As ChatHitArea, button As MouseButtons)
        Me.Message = message
        Me.Area = area
        Me.Button = button
    End Sub

    Public Property Message As ChatMessage
    Public Property Area As ChatHitArea
    Public Property Button As MouseButtons
    ''' <summary>Marque True para impedir o comportamento padrão (abrir imagem/anexo).</summary>
    Public Property Handled As Boolean
End Class

Public Class ChatContextMenuEventArgs
    Inherits CancelEventArgs

    Public Sub New(message As ChatMessage, area As ChatHitArea, menu As ContextMenuStrip)
        Me.Message = message
        Me.Area = area
        Me.Menu = menu
    End Sub

    Public Property Message As ChatMessage
    Public Property Area As ChatHitArea
    ''' <summary>Menu que será exibido: adicione/remova itens à vontade.</summary>
    Public Property Menu As ContextMenuStrip
End Class

Public Class ChatSendEventArgs
    Inherits CancelEventArgs

    Public Sub New(text As String)
        Me.Text = text
    End Sub

    Public Property Text As String
End Class

Public Class ChatTypingEventArgs
    Inherits EventArgs

    Public Sub New(conversation As ChatConversation, isTyping As Boolean)
        Me.Conversation = conversation
        Me.IsTyping = isTyping
    End Sub

    Public Property Conversation As ChatConversation
    Public Property IsTyping As Boolean
End Class

''' <summary>Uma mensagem foi enviada pelo usuário (já está na conversa com Status = Sending).</summary>
Public Class ChatMessageSubmittedEventArgs
    Inherits EventArgs

    Public Sub New(conversation As ChatConversation, message As ChatMessage)
        Me.Conversation = conversation
        Me.Message = message
    End Sub

    Public Property Conversation As ChatConversation
    ''' <summary>Atualize Message.Status (Sent / Delivered / Read / Failed) conforme o servidor responder.</summary>
    Public Property Message As ChatMessage
End Class

''' <summary>Antes de criar a mensagem: permite alterar o texto ou cancelar.</summary>
Public Class ChatMessageSubmittingEventArgs
    Inherits CancelEventArgs

    Public Sub New(conversation As ChatConversation, text As String)
        Me.Conversation = conversation
        Me.Text = text
    End Sub

    Public Property Conversation As ChatConversation
    Public Property Text As String
End Class

''' <summary>Evento com comportamento padrão: marque Handled = True para substituir o padrão.</summary>
Public Class ChatHandledEventArgs
    Inherits EventArgs

    Public Sub New(conversation As ChatConversation)
        Me.Conversation = conversation
    End Sub

    Public Property Conversation As ChatConversation
    Public Property Handled As Boolean
End Class

Public Class ChatFilesEventArgs
    Inherits ChatHandledEventArgs

    Public Sub New(conversation As ChatConversation, files As String())
        MyBase.New(conversation)
        Me.Files = files
    End Sub

    Public Property Files As String()
End Class

Public Class ChatImageEventArgs
    Inherits ChatHandledEventArgs

    Public Sub New(conversation As ChatConversation, image As Image)
        MyBase.New(conversation)
        Me.Image = image
    End Sub

    Public Property Image As Image
End Class

Public Class ChatConversationEventArgs
    Inherits EventArgs

    Public Sub New(conversation As ChatConversation)
        Me.Conversation = conversation
    End Sub

    Public Property Conversation As ChatConversation
End Class

Public Class ChatConversationCancelEventArgs
    Inherits CancelEventArgs

    Public Sub New(conversation As ChatConversation)
        Me.Conversation = conversation
    End Sub

    Public Property Conversation As ChatConversation
End Class
