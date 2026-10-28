'===============================================================================================
'Classe:        clsFrmLogin
'Objetivo:      Autenticação do usuário (MySQL - tb_cad_usuario)
'===============================================================================================
Imports System.Data
Imports MySql.Data.MySqlClient
Imports Documentacti.MySQLHelper

Public Class clsFrmLogin

#Region "::: VARIAVEIS :::"

    Private sUsuario As String
    Private sSenha As String

#End Region

#Region "::: PROPRIEDADES :::"

    Public Property Usuario() As String
        Get
            Return sUsuario
        End Get
        Set(ByVal value As String)
            sUsuario = value
        End Set
    End Property

    Public Property Senha() As String
        Get
            Return sSenha
        End Get
        Set(ByVal value As String)
            sSenha = value
        End Set
    End Property

#End Region

#Region "::: FUNCTION / SUB :::"

    Public Function Login() As Boolean

        'Variaveis Locais
        Dim oDataSet As DataSet

        Try

            'Seta Retorno da Função
            Login = False

            'Obtém o usuário interno ativo (a proc só retorna quem possui senha_hash)
            oDataSet = LoadUsuario(sUsuario.Trim)

            If oDataSet.Tables(0).Rows.Count = 0 Then Exit Function

            With oDataSet.Tables(0).Rows(0)

                'Confere a senha com o hash bcrypt
                If VerificaSenha(sSenha, .Item("senha_hash").ToString) = False Then Exit Function

                goUsuario.sUsuario = .Item("usuario").ToString
                goUsuario.sNome = .Item("nome").ToString
                goUsuario.lUsuario = CLng(.Item("codigo"))
                goUsuario.iEmpresa = CInt(.Item("codigo_empresa"))
                goUsuario.sEmpresa = .Item("empresa").ToString
                goUsuario.sEmail = .Item("email").ToString
                goUsuario.bAdmin = (CInt(.Item("admin")) = 1)
                goUsuario.bInterno = True 'a proc só autentica usuário interno
                goUsuario.iTime = CInt(.Item("time_empresa"))

            End With

            'Registra o login
            Call InsertLog("login")

            'Seta Retorno da Função
            Login = True

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function VerificaSenha(ByVal sSenhaInformada As String, ByVal sHash As String) As Boolean

        Try

            If sHash = "" Or sSenhaInformada = "" Then Return False

            Return BCrypt.Net.BCrypt.Verify(sSenhaInformada, sHash)

        Catch ex As Exception
            'Hash inválido/corrompido: não autentica
            Return False
        End Try

    End Function

    Private Function LoadUsuario(ByVal sLogin As String) As DataSet

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            'Seta Parametros - Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sLogin

            'Executa Query
            Return ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_usuario_login", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function AlterarSenha(ByVal sSenhaAtual As String, ByVal sNovaSenha As String) As Boolean

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim oDataSet As DataSet
        Dim i As Integer = 0

        Try

            'Seta Retorno da Função
            AlterarSenha = False

            'Confere a senha atual do usuário autenticado
            oDataSet = LoadUsuario(goUsuario.sUsuario)

            If oDataSet.Tables(0).Rows.Count = 0 Then Exit Function
            If VerificaSenha(sSenhaAtual, oDataSet.Tables(0).Rows(0).Item("senha_hash").ToString) = False Then Exit Function

            'Seta Parametros - Código Usuário (somente a própria senha)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Hash da nova senha (bcrypt, custo 11)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_senha_hash"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = BCrypt.Net.BCrypt.HashPassword(sNovaSenha, 11)

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_usuario_senha", oMySqlParameter)

            'Seta Retorno da Função
            AlterarSenha = True

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub InsertLog(ByVal sAcao As String)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            'Seta Parametros - Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = goUsuario.sUsuario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - IP
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_ip"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = "" : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Hostname
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_hostname"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = "DOCUMENTACTI - " & System.Net.Dns.GetHostName : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Ação
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_acao"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sAcao : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Log (OUT)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_log"
            oMySqlParameter(i).Direction = ParameterDirection.Output
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_insert_login_log", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class
