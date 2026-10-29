'===============================================================================================
'Classe:        clsDllEmbutida
'Objetivo:      Carrega as DLLs do Lib (Janus, Dundas, MySqlConnector e dependências, BCrypt, ChatUI, ToastUI) que estão
'               embutidas como recurso no executável, para distribuir somente o Documentacti.exe.
'               Registrado na partida (My\ApplicationEvents.vb), antes de qualquer tela ser criada.
'               ATENÇÃO: esta classe não pode usar tipos dessas DLLs.
'===============================================================================================
Imports System.IO
Imports System.Reflection

Public Class clsDllEmbutida

#Region "::: VARIAVEIS :::"

    'Prefixo dos recursos (LogicalName no Documentacti.vbproj)
    Private Const gcPrefixoRecurso As String = "Documentacti.Lib."

    Private Shared ReadOnly oCarregadas As New Dictionary(Of String, Assembly)(StringComparer.OrdinalIgnoreCase)
    Private Shared bRegistrado As Boolean = False

#End Region

#Region "::: FUNCTION / SUB :::"

    Public Shared Sub Registrar()

        If bRegistrado Then Exit Sub

        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf CarregarDll
        bRegistrado = True

    End Sub

    Private Shared Function CarregarDll(ByVal sender As Object, ByVal e As ResolveEventArgs) As Assembly

        Dim sNome As String = New AssemblyName(e.Name).Name

        SyncLock oCarregadas

            'A mesma DLL é carregada uma única vez
            If oCarregadas.ContainsKey(sNome) Then Return oCarregadas(sNome)

            Using oRecurso As Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(gcPrefixoRecurso & sNome & ".dll")

                'Não é uma DLL embutida (ex.: recursos de idioma): segue a busca padrão do .NET
                If IsNothing(oRecurso) Then Return Nothing

                Dim oBytes(CInt(oRecurso.Length) - 1) As Byte
                Dim iLidos As Integer = 0

                Do While iLidos < oBytes.Length
                    Dim iParte As Integer = oRecurso.Read(oBytes, iLidos, oBytes.Length - iLidos)
                    If iParte = 0 Then Exit Do
                    iLidos += iParte
                Loop

                Dim oAssembly As Assembly = Assembly.Load(oBytes)
                oCarregadas(sNome) = oAssembly

                Return oAssembly

            End Using

        End SyncLock

    End Function

#End Region

End Class
