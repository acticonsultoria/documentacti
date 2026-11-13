Imports System.ComponentModel
Imports System.Drawing

''' <summary>
''' Participante de uma conversa (você, o contato, membros de um grupo).
''' </summary>
<Serializable()>
Public Class ChatUser
    Implements INotifyPropertyChanged

    Private _id As String
    Private _displayName As String
    Private _avatar As Image
    Private _avatarColor As Color = Color.Empty
    Private _isOnline As Boolean
    Private _statusText As String

    ' Cache do avatar já recortado/redimensionado (uso interno do renderizador).
    <NonSerialized()> Friend AvatarCache As Bitmap
    Friend AvatarCacheSize As Integer

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New()
        _id = Guid.NewGuid().ToString("N")
        _displayName = String.Empty
    End Sub

    Public Sub New(id As String, displayName As String)
        _id = If(String.IsNullOrEmpty(id), Guid.NewGuid().ToString("N"), id)
        _displayName = If(displayName, String.Empty)
    End Sub

    Public Sub New(id As String, displayName As String, avatar As Image)
        Me.New(id, displayName)
        _avatar = avatar
    End Sub

    ''' <summary>Identificador único (usado para agrupar mensagens do mesmo remetente).</summary>
    Public Property Id As String
        Get
            Return _id
        End Get
        Set(value As String)
            If _id <> value Then _id = value : OnPropertyChanged("Id")
        End Set
    End Property

    Public Property DisplayName As String
        Get
            Return _displayName
        End Get
        Set(value As String)
            If _displayName <> value Then
                _displayName = If(value, String.Empty)
                OnPropertyChanged("DisplayName")
            End If
        End Set
    End Property

    ''' <summary>Foto do usuário. Se Nothing, são desenhadas as iniciais sobre <see cref="AvatarColor"/>.</summary>
    Public Property Avatar As Image
        Get
            Return _avatar
        End Get
        Set(value As Image)
            If Not Object.ReferenceEquals(_avatar, value) Then
                _avatar = value
                ClearAvatarCache()
                OnPropertyChanged("Avatar")
            End If
        End Set
    End Property

    ''' <summary>Cor de fundo das iniciais e do nome no balão. Color.Empty = cor automática pelo nome.</summary>
    Public Property AvatarColor As Color
        Get
            Return _avatarColor
        End Get
        Set(value As Color)
            If _avatarColor <> value Then _avatarColor = value : OnPropertyChanged("AvatarColor")
        End Set
    End Property

    Public Property IsOnline As Boolean
        Get
            Return _isOnline
        End Get
        Set(value As Boolean)
            If _isOnline <> value Then _isOnline = value : OnPropertyChanged("IsOnline")
        End Set
    End Property

    ''' <summary>Texto de status mostrado no cabeçalho quando offline (ex.: "visto por último às 14:02").</summary>
    Public Property StatusText As String
        Get
            Return _statusText
        End Get
        Set(value As String)
            If _statusText <> value Then _statusText = value : OnPropertyChanged("StatusText")
        End Set
    End Property

    Public Property Tag As Object

    ''' <summary>Até duas iniciais a partir do nome.</summary>
    Public ReadOnly Property Initials As String
        Get
            Dim parts = _displayName.Split(New Char() {" "c, "."c, "_"c, "-"c}, StringSplitOptions.RemoveEmptyEntries)
            If parts.Length = 0 Then Return "?"
            If parts.Length = 1 Then
                Return parts(0).Substring(0, Math.Min(2, parts(0).Length)).ToUpperInvariant()
            End If
            Return (parts(0).Substring(0, 1) & parts(parts.Length - 1).Substring(0, 1)).ToUpperInvariant()
        End Get
    End Property

    ''' <summary>Cor efetiva (a definida ou uma cor estável derivada do Id/nome).</summary>
    Public ReadOnly Property EffectiveColor As Color
        Get
            If Not _avatarColor.IsEmpty Then Return _avatarColor
            Return GdiHelpers.ColorFromKey(If(String.IsNullOrEmpty(_displayName), _id, _displayName))
        End Get
    End Property

    Friend Sub ClearAvatarCache()
        If AvatarCache IsNot Nothing Then AvatarCache.Dispose()
        AvatarCache = Nothing
        AvatarCacheSize = 0
    End Sub

    Protected Overridable Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub

    Friend Shared Function SameUser(a As ChatUser, b As ChatUser) As Boolean
        If a Is Nothing AndAlso b Is Nothing Then Return True
        If a Is Nothing OrElse b Is Nothing Then Return False
        If Object.ReferenceEquals(a, b) Then Return True
        Return String.Equals(a.Id, b.Id, StringComparison.Ordinal)
    End Function

    Public Overrides Function ToString() As String
        Return _displayName
    End Function
End Class
