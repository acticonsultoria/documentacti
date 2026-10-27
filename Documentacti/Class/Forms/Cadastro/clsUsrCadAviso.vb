'===============================================================================================
'Classe:        clsUsrCadAviso
'Objetivo:      Acesso a dados da tela de Cadastro de Avisos (MySQL)
'               Usa as procedures de avisos já existentes; o vínculo com as empresas é regravado
'               por completo no Salvar (sp_insert_aviso / sp_update_aviso, empresas separadas por ";")
'===============================================================================================
Imports System.Data
Imports MySql.Data.MySqlClient
Imports Documentacti.MySQLHelper
Imports Janus.Windows.GridEX

Public Class clsUsrCadAviso

#Region "::: FUNCTION / SUB :::"

#Region "::: GERAL :::"

    Public Sub LoadGrid(ByVal oGrid As GridEX, _
                        ByVal sTitulo As String, _
                        ByVal iAtivo As Integer)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim oDataSet As DataSet
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Aviso (-1 = todos)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_avisos"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = -1 : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Título (-1 = todos)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_titulo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = IIf(sTitulo = "", "-1", sTitulo) : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Empresas (-1 = todas)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = "-1" : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Descrição (-1 = todas)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_descricao"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = "-1" : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Ativo (-1 = todos)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_ativo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iAtivo

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_aviso", oMySqlParameter)

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

    Public Function LoadDados(ByVal lCodigo As Long) As DataTable

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim oDataSet As DataSet
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Aviso
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_avisos"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigo

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_editar_aviso", oMySqlParameter)

            Return oDataSet.Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function LoadEmpresas() As DataTable

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim oDataSet As DataSet
        Dim i As Integer = 0

        Try

            'Mesma listagem da tela de Empresas, sem filtros
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = goUsuario.iEmpresa : i += 1 : ReDim Preserve oMySqlParameter(i)

            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_razao_social"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = "" : i += 1 : ReDim Preserve oMySqlParameter(i)

            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_cnpj_cpf"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = "" : i += 1 : ReDim Preserve oMySqlParameter(i)

            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_ativo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = -1 : i += 1 : ReDim Preserve oMySqlParameter(i)

            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_tipo_time"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = -1

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_cadastro_empresa", oMySqlParameter)

            'Coluna marcada na própria grid (padrão: não recebe o aviso)
            oDataSet.Tables(0).Columns.Add("selecionado", GetType(Boolean)).DefaultValue = False

            For Each oRow As DataRow In oDataSet.Tables(0).Rows
                oRow.Item("selecionado") = False
            Next

            Return oDataSet.Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Insert(ByVal sTitulo As String, _
                           ByVal sDescricao As String, _
                           ByVal sEmpresas As String, _
                           ByVal bAtivo As Boolean) As Long

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            'Seta Parametros - Título
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_titulo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sTitulo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Descrição
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_descricao"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sDescricao : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Empresas (códigos separados por ";")
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sEmpresas : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Ativo
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_ativo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = IIf(bAtivo, 1, 0) : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Aviso (OUT)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_avisos"
            oMySqlParameter(i).Direction = ParameterDirection.Output
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_insert_aviso", oMySqlParameter)

            Return CLng(oMySqlParameter(i).Value)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub Update(ByVal lCodigo As Long, _
                      ByVal sTitulo As String, _
                      ByVal sDescricao As String, _
                      ByVal sEmpresas As String, _
                      ByVal bAtivo As Boolean)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Aviso
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_avisos"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Título
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_titulo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sTitulo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Descrição
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_descricao"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sDescricao : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Empresas (códigos separados por ";"; a proc regrava todos os vínculos)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sEmpresas : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Ativo
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_ativo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = IIf(bAtivo, 1, 0)

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_aviso", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#End Region

End Class
