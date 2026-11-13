Imports System.ComponentModel
Imports System.Drawing

Public Enum MessageDirection
    ''' <summary>Mensagem recebida (balão à esquerda, com avatar).</summary>
    Incoming
    ''' <summary>Mensagem enviada pelo usuário atual (balão à direita).</summary>
    Outgoing
    ''' <summary>Aviso do sistema, centralizado ("Fulano entrou no grupo").</summary>
    System
End Enum

Public Enum MessageStatus
    None
    ''' <summary>Relógio: ainda não confirmada pelo servidor.</summary>
    Sending
    ''' <summary>✓</summary>
    Sent
    ''' <summary>✓✓ cinza</summary>
    Delivered
    ''' <summary>✓✓ na cor de destaque</summary>
    Read
    ''' <summary>! vermelho (clicável: Area = Status)</summary>
    Failed
End Enum

Public Enum MessageKind
    Text
    Image
    File
End Enum

''' <summary>Arquivo anexado a uma mensagem.</summary>
<Serializable()>
Public Class ChatAttachment
    Public Sub New()
    End Sub

    Public Sub New(fileName As String, filePath As String, size As Long)
        Me.FileName = fileName
        Me.FilePath = filePath
        Me.Size = size
    End Sub

    Public Property FileName As String
    Public Property FilePath As String
    ''' <summary>Tamanho em bytes (-1 = desconhecido).</summary>
    Public Property Size As Long = -1
    Public Property Tag As Object

    Public ReadOnly Property Extension As String
        Get
            If String.IsNullOrEmpty(FileName) Then Return String.Empty
            Dim ext = IO.Path.GetExtension(FileName)
            If String.IsNullOrEmpty(ext) Then Return String.Empty
            Return ext.TrimStart("."c).ToUpperInvariant()
        End Get
    End Property

    Public Shared Function FromFile(path As String) As ChatAttachment
        Dim fi As New IO.FileInfo(path)
        Return New ChatAttachment(fi.Name, fi.FullName, If(fi.Exists, fi.Length, -1L))
    End Function
End Class

''' <summary>
''' Uma mensagem do chat. Alterar qualquer propriedade (Status, Text...) atualiza a tela automaticamente.
''' </summary>
<Serializable()>
Public Class ChatMessage
    Implements INotifyPropertyChanged

    Private _id As String
    Private _sender As ChatUser
    Private _text As String = String.Empty
    Private _timestamp As DateTime
    Private _direction As MessageDirection
    Private _status As MessageStatus
    Private _image As Image
    Private _attachment As ChatAttachment

    ' ---- caches de layout/renderização (uso interno do ChatView) ----
    <NonSerialized()> Friend Thumb As Bitmap
    Friend ThumbSize As Size
    Friend MeasureVersion As Integer = -1
    Friend MeasureWidth As Integer
    Friend MeasuredText As Size

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New()
        _id = Guid.NewGuid().ToString("N")
        _timestamp = DateTime.Now
    End Sub

    Public Sub New(sender As ChatUser, text As String, direction As MessageDirection)
        Me.New()
        _sender = sender
        _text = If(text, String.Empty)
        _direction = direction
    End Sub

    Public Property Id As String
        Get
            Return _id
        End Get
        Set(value As String)
            _id = value
        End Set
    End Property

    Public Property Sender As ChatUser
        Get
            Return _sender
        End Get
        Set(value As ChatUser)
            If Not Object.ReferenceEquals(_sender, value) Then _sender = value : OnPropertyChanged("Sender")
        End Set
    End Property

    Public Property Text As String
        Get
            Return _text
        End Get
        Set(value As String)
            If value Is Nothing Then value = String.Empty
            If _text <> value Then
                _text = value
                MeasureVersion = -1
                OnPropertyChanged("Text")
            End If
        End Set
    End Property

    Public Property Timestamp As DateTime
        Get
            Return _timestamp
        End Get
        Set(value As DateTime)
            If _timestamp <> value Then _timestamp = value : OnPropertyChanged("Timestamp")
        End Set
    End Property

    Public Property Direction As MessageDirection
        Get
            Return _direction
        End Get
        Set(value As MessageDirection)
            If _direction <> value Then
                _direction = value
                MeasureVersion = -1
                OnPropertyChanged("Direction")
            End If
        End Set
    End Property

    Public Property Status As MessageStatus
        Get
            Return _status
        End Get
        Set(value As MessageStatus)
            If _status <> value Then _status = value : OnPropertyChanged("Status")
        End Set
    End Property

    ''' <summary>Imagem exibida no balão (o Text vira legenda).</summary>
    Public Property Image As Image
        Get
            Return _image
        End Get
        Set(value As Image)
            If Not Object.ReferenceEquals(_image, value) Then
                _image = value
                ClearThumb()
                MeasureVersion = -1
                OnPropertyChanged("Image")
            End If
        End Set
    End Property

    ''' <summary>Arquivo anexado (exibido como cartão clicável quando não há Image).</summary>
    Public Property Attachment As ChatAttachment
        Get
            Return _attachment
        End Get
        Set(value As ChatAttachment)
            If Not Object.ReferenceEquals(_attachment, value) Then
                _attachment = value
                MeasureVersion = -1
                OnPropertyChanged("Attachment")
            End If
        End Set
    End Property

    Public Property Tag As Object

    Public ReadOnly Property Kind As MessageKind
        Get
            If _image IsNot Nothing Then Return MessageKind.Image
            If _attachment IsNot Nothing Then Return MessageKind.File
            Return MessageKind.Text
        End Get
    End Property

    Friend Sub ClearThumb()
        If Thumb IsNot Nothing Then Thumb.Dispose()
        Thumb = Nothing
        ThumbSize = Size.Empty
    End Sub

    Protected Overridable Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("[{0:HH:mm}] {1}: {2}", _timestamp, If(_sender Is Nothing, "", _sender.DisplayName), _text)
    End Function
End Class
