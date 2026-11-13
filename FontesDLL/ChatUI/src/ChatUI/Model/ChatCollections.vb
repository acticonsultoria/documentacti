Imports System.Collections.ObjectModel
Imports System.ComponentModel

Public Enum ChatCollectionAction
    Add
    Remove
    Replace
    Reset
End Enum

Public Class ChatCollectionChangedEventArgs(Of T)
    Inherits EventArgs

    Public Sub New(action As ChatCollectionAction, index As Integer, item As T)
        Me.Action = action
        Me.Index = index
        Me.Item = item
    End Sub

    Public Property Action As ChatCollectionAction
    Public Property Index As Integer
    Public Property Item As T
End Class

Public Class ChatItemChangedEventArgs(Of T)
    Inherits EventArgs

    Public Sub New(item As T, propertyName As String)
        Me.Item = item
        Me.PropertyName = propertyName
    End Sub

    Public Property Item As T
    Public Property PropertyName As String
End Class

''' <summary>
''' Coleção observável genérica: avisa inclusões/remoções e também mudanças de propriedade dos itens.
''' Altere-a sempre pela thread da UI (use Control.BeginInvoke se os dados vierem de um socket/thread).
''' </summary>
Public Class ChatObservableCollection(Of T As INotifyPropertyChanged)
    Inherits Collection(Of T)

    Private _suspend As Integer

    Public Event CollectionChanged As EventHandler(Of ChatCollectionChangedEventArgs(Of T))
    Public Event ItemChanged As EventHandler(Of ChatItemChangedEventArgs(Of T))

    ''' <summary>Adiciona vários itens disparando um único Reset (bem mais rápido para carregar histórico).</summary>
    Public Sub AddRange(items As IEnumerable(Of T))
        _suspend += 1
        Try
            For Each it In items
                Add(it)
            Next
        Finally
            _suspend -= 1
        End Try
        RaiseChanged(ChatCollectionAction.Reset, -1, Nothing)
    End Sub

    ''' <summary>Insere vários itens no início (ex.: "carregar mensagens anteriores").</summary>
    Public Sub InsertRange(index As Integer, items As IEnumerable(Of T))
        _suspend += 1
        Dim i = index
        Try
            For Each it In items
                Insert(i, it)
                i += 1
            Next
        Finally
            _suspend -= 1
        End Try
        RaiseChanged(ChatCollectionAction.Reset, index, Nothing)
    End Sub

    Protected Overrides Sub InsertItem(index As Integer, item As T)
        If item Is Nothing Then Throw New ArgumentNullException("item")
        MyBase.InsertItem(index, item)
        AddHandler item.PropertyChanged, AddressOf OnItemPropertyChanged
        RaiseChanged(ChatCollectionAction.Add, index, item)
    End Sub

    Protected Overrides Sub RemoveItem(index As Integer)
        Dim item = Me(index)
        RemoveHandler item.PropertyChanged, AddressOf OnItemPropertyChanged
        MyBase.RemoveItem(index)
        RaiseChanged(ChatCollectionAction.Remove, index, item)
    End Sub

    Protected Overrides Sub SetItem(index As Integer, item As T)
        If item Is Nothing Then Throw New ArgumentNullException("item")
        RemoveHandler Me(index).PropertyChanged, AddressOf OnItemPropertyChanged
        MyBase.SetItem(index, item)
        AddHandler item.PropertyChanged, AddressOf OnItemPropertyChanged
        RaiseChanged(ChatCollectionAction.Replace, index, item)
    End Sub

    Protected Overrides Sub ClearItems()
        For Each it In Me
            RemoveHandler it.PropertyChanged, AddressOf OnItemPropertyChanged
        Next
        MyBase.ClearItems()
        RaiseChanged(ChatCollectionAction.Reset, -1, Nothing)
    End Sub

    Private Sub OnItemPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        RaiseEvent ItemChanged(Me, New ChatItemChangedEventArgs(Of T)(DirectCast(sender, T), e.PropertyName))
    End Sub

    Private Sub RaiseChanged(action As ChatCollectionAction, index As Integer, item As T)
        If _suspend > 0 Then Return
        RaiseEvent CollectionChanged(Me, New ChatCollectionChangedEventArgs(Of T)(action, index, item))
    End Sub
End Class

Public Class ChatMessageCollection
    Inherits ChatObservableCollection(Of ChatMessage)

    ''' <summary>Última mensagem (ou Nothing).</summary>
    Public ReadOnly Property Last As ChatMessage
        Get
            If Count = 0 Then Return Nothing
            Return Me(Count - 1)
        End Get
    End Property

    Public Function FindById(id As String) As ChatMessage
        For Each m In Me
            If String.Equals(m.Id, id, StringComparison.Ordinal) Then Return m
        Next
        Return Nothing
    End Function
End Class

Public Class ChatConversationCollection
    Inherits ChatObservableCollection(Of ChatConversation)

    Public Function FindById(id As String) As ChatConversation
        For Each c In Me
            If String.Equals(c.Id, id, StringComparison.Ordinal) Then Return c
        Next
        Return Nothing
    End Function
End Class
