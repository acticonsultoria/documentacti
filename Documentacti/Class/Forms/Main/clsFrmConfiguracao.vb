'===============================================================================================
'Classe:        clsFrmConfiguracao
'Objetivo:      Configurações do usuário logado (MySQL - tb_cad_usuario_configuracao)
'===============================================================================================
Imports System.Data
Imports MySqlConnector
Imports Documentacti.MySQLHelper

Public Class clsFrmConfiguracao

#Region "::: FUNCTION / SUB :::"

    Public Function SilenciarNotificacao() As Boolean

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario

            'Executa Query (sem configuração gravada a proc devolve o padrão: notificações ligadas)
            Dim oTabela As DataTable = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_usuario_configuracao", oMySqlParameter).Tables(0)

            Return oTabela.Rows.Count > 0 AndAlso CInt(oTabela.Rows(0).Item("silenciar_notificacao")) = 1

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub Update(ByVal bSilenciarNotificacao As Boolean)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Silenciar Notificações
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_silenciar_notificacao"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int16
            oMySqlParameter(i).Value = IIf(bSilenciarNotificacao, 1, 0)

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_usuario_configuracao", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class
