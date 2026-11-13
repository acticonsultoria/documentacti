'===============================================================================================
'Classe:        clsFrmNotificacao
'Objetivo:      Acesso a dados da central de notificações (sino da barra de status) (MySQL)
'===============================================================================================
Imports System.Data
Imports MySqlConnector
Imports Documentacti.MySQLHelper

Public Class clsFrmNotificacao

#Region "::: FUNCTION / SUB :::"

    Private Function Parametro(ByVal sNome As String, _
                               ByVal oTipo As MySqlDbType, _
                               ByVal oValor As Object) As MySqlParameter

        'Monta um parâmetro de entrada (nulo quando o valor não foi informado)
        Dim oMySqlParameter As New MySqlParameter

        oMySqlParameter.ParameterName = sNome
        oMySqlParameter.Direction = ParameterDirection.Input
        oMySqlParameter.MySqlDbType = oTipo
        oMySqlParameter.Value = IIf(IsNothing(oValor), DBNull.Value, oValor)

        Return oMySqlParameter

    End Function

    Public Function LoadGrid() As DataTable

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo_usuario", MySqlDbType.Int64, goUsuario.lUsuario)

            'Executa Query (não lidas + lidas dos últimos 15 dias)
            Return ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_notificacao", oMySqlParameter).Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Quantidade() As Integer

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo_usuario", MySqlDbType.Int64, goUsuario.lUsuario)

            'Executa Query (notificações não lidas do usuário)
            Dim oTabela As DataTable = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_notificacao_quantidade", oMySqlParameter).Tables(0)

            If oTabela.Rows.Count = 0 Then Return 0

            Return CInt(oTabela.Rows(0).Item("quantidade"))

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Insert(ByVal sTipo As String, _
                           ByVal lCodigoReferencia As Long, _
                           ByVal sTitulo As String, _
                           ByVal sMensagem As String, _
                           ByVal oDataReferencia As Object) As Long

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo_usuario", MySqlDbType.Int64, goUsuario.lUsuario) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_tipo", MySqlDbType.VarChar, sTipo) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo_referencia", MySqlDbType.Int64, lCodigoReferencia) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_titulo", MySqlDbType.VarChar, sTitulo) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_mensagem", MySqlDbType.VarChar, sMensagem) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_data_referencia", MySqlDbType.DateTime, oDataReferencia)

            'Executa Query (retorna o código gravado: o botão OK do toast marca esse item como lido)
            Dim oTabela As DataTable = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_insert_administracao_notificacao", oMySqlParameter).Tables(0)

            If oTabela.Rows.Count = 0 Then Return 0

            Return CLng(oTabela.Rows(0).Item("codigo"))

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub Delete(ByVal lCodigo As Long)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo_usuario", MySqlDbType.Int64, goUsuario.lUsuario) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo", MySqlDbType.Int32, lCodigo)

            'Executa Query (código 0 = todas as notificações do usuário)
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_delete_administracao_notificacao", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub UpdateLida(ByVal lCodigo As Long)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo_usuario", MySqlDbType.Int64, goUsuario.lUsuario) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo", MySqlDbType.Int32, lCodigo)

            'Executa Query (código 0 = todas as notificações do usuário)
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_notificacao_lida", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class
