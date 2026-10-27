'===============================================================================================
'Classe:        clsUsrCadBaseConhecimento
'Objetivo:      Acesso a dados da tela de Base de Conhecimento (MySQL)
'               Empresas por artigo na tb_cad_base_conhecimento_empresa (N:N); -1 = TODOS
'===============================================================================================
Imports System.Data
Imports MySql.Data.MySqlClient
Imports Documentacti.MySQLHelper
Imports Janus.Windows.GridEX

Public Class clsUsrCadBaseConhecimento

#Region "::: FUNCTION / SUB :::"

#Region "::: GERAL :::"

    Public Sub LoadGrid(ByVal oGrid As GridEX, _
                        ByVal sTitulo As String, _
                        ByVal sMarcador As String, _
                        ByVal iModulo As Integer, _
                        ByVal iEmpresa As Integer)

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

            'Seta Parametros - Título
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_titulo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sTitulo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Marcador
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_marcador"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sMarcador : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Módulo (-1 = todos)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_modulo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iModulo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Empresa (-2 = sem filtro / -1 = somente TODOS)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iEmpresa

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_base_conhecimento_lista", oMySqlParameter)

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

            'Seta Parametros - Código Empresa
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = goUsuario.iEmpresa : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Artigo
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigo

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_base_conhecimento_dados", oMySqlParameter)

            Return oDataSet.Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function LoadEmpresas() As DataTable

        Try

            Dim oTabela As DataTable = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_combo_administracao_chamado_empresa", ParametroEmpresa()).Tables(0)

            'Opção "TODOS" (código padrão -1) no topo da lista
            Dim oTodos As DataRow = oTabela.NewRow
            oTodos.Item("codigo") = -1
            oTodos.Item("descricao") = "TODOS"
            oTabela.Rows.InsertAt(oTodos, 0)

            Return oTabela

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function LoadMarcadores() As DataTable

        Try

            'Mesma proc do filtro de marcadores da plataforma web (sem parâmetros)
            Return ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_combo_base_conhecimento_marcador_filtro", New MySqlParameter() {}).Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub LoadGridArquivo(ByVal oGrid As GridEX, _
                               ByVal lCodigo As Long)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim oDataSet As DataSet
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Artigo
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_base_conhecimento"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Empresa
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = goUsuario.iEmpresa

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_anexo_base_conhecimento", oMySqlParameter)

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

    Public Function Gravar(ByVal lCodigo As Long, _
                           ByVal sTitulo As String, _
                           ByVal sDescricao As String, _
                           ByVal iModulo As Integer, _
                           ByVal sMarcadores As String, _
                           ByVal sEmpresas As String) As Long

        'lCodigo = 0: insere / > 0: altera. Retorna o código do artigo
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Empresa
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = goUsuario.iEmpresa : i += 1 : ReDim Preserve oMySqlParameter(i)

            If lCodigo > 0 Then
                'Seta Parametros - Código Artigo
                oMySqlParameter(i) = New MySqlParameter
                oMySqlParameter(i).ParameterName = "@p_codigo"
                oMySqlParameter(i).Direction = ParameterDirection.Input
                oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
                oMySqlParameter(i).Value = lCodigo : i += 1 : ReDim Preserve oMySqlParameter(i)
            End If

            'Seta Parametros - Título
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_titulo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sTitulo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Descrição (HTML)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_descricao"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sDescricao : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = goUsuario.lUsuario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Módulo
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_modulo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iModulo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Marcadores (códigos separados por ";", formato da web)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_marcadores"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sMarcadores : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Empresas (-1 = TODOS)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_empresas"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sEmpresas

            If lCodigo > 0 Then

                'Executa Query
                ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_base_conhecimento", oMySqlParameter)

                Return lCodigo

            End If

            i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Artigo (OUT)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Output
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_insert_administracao_base_conhecimento", oMySqlParameter)

            Return CLng(oMySqlParameter(i).Value)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#End Region

End Class
