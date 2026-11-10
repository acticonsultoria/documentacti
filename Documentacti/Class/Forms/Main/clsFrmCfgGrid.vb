'===============================================================================================
'Classe:        clsFrmCfgGrid
'Objetivo:      Configuração das grids por usuário - colunas visíveis, posição e largura (MySQL)
'===============================================================================================
Imports System.Data
Imports MySqlConnector
Imports Documentacti.MySQLHelper

Public Class clsFrmCfgGrid

#Region "::: CONSTANTES :::"

    'Tipos de configuração gravados pela sp_update_administracao_configuracao_grid
    Public Const gcTipoVisivel As String = "VISIVEL"
    Public Const gcTipoLargura As String = "LARGURA"
    Public Const gcTipoPosicao As String = "POSICAO"

#End Region

#Region "::: FUNCTION / SUB :::"

    Private Function ParametrosGrid(ByVal sNomeFormulario As String, _
                                    ByVal sNomeGrid As String) As MySqlParameter()

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Empresa
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = goUsuario.iEmpresa : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Nome Formulário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_nome_formulario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = Left(sNomeFormulario, 50) : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Nome Grid
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_nome_grid"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = Left(sNomeGrid, 50)

            Return oMySqlParameter

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function LoadConfiguracao(ByVal sNomeFormulario As String, _
                                     ByVal sNomeGrid As String) As DataTable

        Try

            'Executa Query
            Return ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_configuracao_grid", _
                                  ParametrosGrid(sNomeFormulario, sNomeGrid)).Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub Update(ByVal sNomeFormulario As String, _
                      ByVal sNomeGrid As String, _
                      ByVal sTipo As String, _
                      ByVal sLista As String)

        'Variaveis Locais
        Dim oMySqlParameter() As MySqlParameter
        Dim i As Integer

        Try

            If sLista = "" Then Exit Sub

            oMySqlParameter = ParametrosGrid(sNomeFormulario, sNomeGrid)
            i = oMySqlParameter.Length : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Tipo (VISIVEL / LARGURA / POSICAO)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_tipo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sTipo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Lista "campo=valor;campo=valor"
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_lista"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Text
            oMySqlParameter(i).Value = sLista

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_configuracao_grid", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub Delete(ByVal sNomeFormulario As String, _
                      ByVal sNomeGrid As String)

        Try

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_delete_administracao_configuracao_grid", _
                            ParametrosGrid(sNomeFormulario, sNomeGrid))

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class
