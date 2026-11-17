'===============================================================================================
'Classe:        clsUsrAdmPatchNote
'Objetivo:      Acesso a dados das notas de atualização (Patch Notes) do Documentacti (MySQL)
'               Todos consultam; somente super administrador publica/edita (validado na proc)
'===============================================================================================
Imports System.Data
Imports MySqlConnector
Imports Documentacti.MySQLHelper
Imports Janus.Windows.GridEX

Public Class clsUsrAdmPatchNote

#Region "::: FUNCTION / SUB :::"

    Public Sub LoadGrid(ByVal oGrid As GridEX, _
                        ByVal sPesquisa As String, _
                        ByVal iAtivo As Integer)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim oDataSet As DataSet
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Empresa
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = goUsuario.iEmpresa : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Pesquisa (versão, título ou texto)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_titulo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sPesquisa : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Ativo (-1 = todos)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_ativo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iAtivo

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_patch_note", oMySqlParameter)

            'Carrega Grid
            Call ConfigurarDataMemberGrid(oGrid)
            oGrid.DataSource = oDataSet.DefaultViewManager
            oGrid.DataMember = oDataSet.Tables(0).TableName

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function Parametros(ByVal lCodigo As Long, _
                                ByVal sVersao As String, _
                                ByVal dDataPublicacao As Date, _
                                ByVal sTitulo As String, _
                                ByVal sDescricao As String, _
                                ByVal bAtivo As Boolean, _
                                ByVal bInsert As Boolean) As MySqlParameter()

        'Parâmetros comuns de insert (código de saída) e update (código de entrada)
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        'Seta Parametros - Código Empresa
        oMySqlParameter(i) = New MySqlParameter
        oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
        oMySqlParameter(i).Direction = ParameterDirection.Input
        oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
        oMySqlParameter(i).Value = goUsuario.iEmpresa : i += 1 : ReDim Preserve oMySqlParameter(i)

        If bInsert = False Then
            'Seta Parametros - Código da nota
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigo : i += 1 : ReDim Preserve oMySqlParameter(i)
        End If

        'Seta Parametros - Versão
        oMySqlParameter(i) = New MySqlParameter
        oMySqlParameter(i).ParameterName = "@p_versao"
        oMySqlParameter(i).Direction = ParameterDirection.Input
        oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
        oMySqlParameter(i).Value = sVersao : i += 1 : ReDim Preserve oMySqlParameter(i)

        'Seta Parametros - Data de publicação
        oMySqlParameter(i) = New MySqlParameter
        oMySqlParameter(i).ParameterName = "@p_data_publicacao"
        oMySqlParameter(i).Direction = ParameterDirection.Input
        oMySqlParameter(i).MySqlDbType = MySqlDbType.Date
        oMySqlParameter(i).Value = dDataPublicacao.Date : i += 1 : ReDim Preserve oMySqlParameter(i)

        'Seta Parametros - Título
        oMySqlParameter(i) = New MySqlParameter
        oMySqlParameter(i).ParameterName = "@p_titulo"
        oMySqlParameter(i).Direction = ParameterDirection.Input
        oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
        oMySqlParameter(i).Value = sTitulo : i += 1 : ReDim Preserve oMySqlParameter(i)

        'Seta Parametros - Descrição (markdown)
        oMySqlParameter(i) = New MySqlParameter
        oMySqlParameter(i).ParameterName = "@p_descricao"
        oMySqlParameter(i).Direction = ParameterDirection.Input
        oMySqlParameter(i).MySqlDbType = MySqlDbType.MediumText
        oMySqlParameter(i).Value = sDescricao : i += 1 : ReDim Preserve oMySqlParameter(i)

        'Seta Parametros - Ativo
        oMySqlParameter(i) = New MySqlParameter
        oMySqlParameter(i).ParameterName = "@p_ativo"
        oMySqlParameter(i).Direction = ParameterDirection.Input
        oMySqlParameter(i).MySqlDbType = MySqlDbType.Int16
        oMySqlParameter(i).Value = IIf(bAtivo, 1, 0) : i += 1 : ReDim Preserve oMySqlParameter(i)

        'Seta Parametros - Usuário
        oMySqlParameter(i) = New MySqlParameter
        oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
        oMySqlParameter(i).Direction = ParameterDirection.Input
        oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
        oMySqlParameter(i).Value = goUsuario.lUsuario

        If bInsert Then
            i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código gerado
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Output
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
        End If

        Return oMySqlParameter

    End Function

    Public Function Insert(ByVal sVersao As String, _
                           ByVal dDataPublicacao As Date, _
                           ByVal sTitulo As String, _
                           ByVal sDescricao As String, _
                           ByVal bAtivo As Boolean) As Long

        Try

            Dim oMySqlParameter() As MySqlParameter = Parametros(0, sVersao, dDataPublicacao, sTitulo, sDescricao, bAtivo, True)

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_insert_administracao_patch_note", oMySqlParameter)

            Return CLng(oMySqlParameter(oMySqlParameter.Length - 1).Value)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub Update(ByVal lCodigo As Long, _
                      ByVal sVersao As String, _
                      ByVal dDataPublicacao As Date, _
                      ByVal sTitulo As String, _
                      ByVal sDescricao As String, _
                      ByVal bAtivo As Boolean)

        Try

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_patch_note", _
                            Parametros(lCodigo, sVersao, dDataPublicacao, sTitulo, sDescricao, bAtivo, False))

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class
