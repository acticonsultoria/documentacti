'===============================================================================================
'Classe:        clsUsrCadEmpresa
'Objetivo:      Acesso a dados da tela de Cadastro de Empresas (MySQL)
'               Usa as mesmas procedures da plataforma web (interactisystem)
'===============================================================================================
Imports System.Data
Imports MySqlConnector
Imports Documentacti.MySQLHelper
Imports Janus.Windows.GridEX

Public Class clsUsrCadEmpresa

#Region "::: FUNCTION / SUB :::"

#Region "::: GERAL :::"

    Public Sub LoadGrid(ByVal oGrid As GridEX, _
                        ByVal sCnpjCpf As String, _
                        ByVal sRazaoSocial As String, _
                        ByVal iAtivo As Integer, _
                        ByVal iTime As Integer)

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

            'Seta Parametros - Razão Social
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_razao_social"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sRazaoSocial : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - CNPJ/CPF
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_cnpj_cpf"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sCnpjCpf : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Ativo (-1 = todos)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_ativo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iAtivo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Time (-1 = todos)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_tipo_time"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iTime

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_cadastro_empresa", oMySqlParameter)

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

            'Seta Parametros - Código da Empresa (cadastro)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigo

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_cadastro_empresa_editar", oMySqlParameter)

            Return oDataSet.Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function LoadModulos() As DataTable

        'Variaveis Locais
        Dim oDataSet As DataSet

        Try

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_multicbo_modulo_empresa", ParametroEmpresa())

            'Mesmo formato dos demais combos (codigo / descricao)
            With oDataSet.Tables(0)
                .Columns("ModuloEmpresaId").ColumnName = "codigo"
                .Columns("Name").ColumnName = "descricao"
            End With

            Return oDataSet.Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub Update(ByVal oDados As Dictionary(Of String, Object))

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        'Parâmetros na ordem da sp_update_cadastro_empresa (mesma procedure da plataforma web)
        Dim sParametros() As String = {"codigo_empresa", "cnpj_cpf", "razao_social", "nome_fantasia", "ativo", "telefone", _
                                       "id_teamviewer", "senha_teamviewer", "ip_MSTSC", "usuario_MSTSC", "senha_MSTSC", "arquivo", _
                                       "ip_vpn", "usuario_vpn", "senha_vpn", "local_pasta_servidor", "servidor_sql", "usuario_sql", _
                                       "senha_sql", "modulo_empresa", "dominio_usuario_windows", "senha_windows", "tipo_time", _
                                       "focal_point", "celular", "email", "observacao"}

        Try

            For Each sParametro As String In sParametros

                'Seta Parametros
                oMySqlParameter(i) = New MySqlParameter
                oMySqlParameter(i).ParameterName = "@p_" & sParametro
                oMySqlParameter(i).Direction = ParameterDirection.Input

                If sParametro = "codigo_empresa" OrElse sParametro = "ativo" OrElse sParametro = "tipo_time" Then
                    oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
                Else
                    oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
                End If

                oMySqlParameter(i).Value = IIf(IsNothing(oDados(sParametro)), DBNull.Value, oDados(sParametro))

                If sParametro <> sParametros(sParametros.Length - 1) Then
                    i += 1 : ReDim Preserve oMySqlParameter(i)
                End If

            Next

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_cadastro_empresa", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "::: ARQUIVOS / HISTÓRICO :::"

    Public Sub LoadGridArquivo(ByVal oGrid As GridEX, _
                               ByVal lCodigo As Long)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim oDataSet As DataSet
        Dim i As Integer = 0

        Try

            'Seta Parametros - Empresa (cadastro)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_empresa"
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
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_anexo_empresa", oMySqlParameter)

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

    Public Sub LoadGridHistorico(ByVal oGrid As GridEX, _
                                 ByVal lCodigo As Long)

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

            'Seta Parametros - Empresa (cadastro)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigo

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_cadastro_empresa_historico", oMySqlParameter)

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

#End Region

#End Region

End Class
