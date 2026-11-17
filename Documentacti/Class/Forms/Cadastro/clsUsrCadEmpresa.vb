'===============================================================================================
'Classe:        clsUsrCadEmpresa
'Objetivo:      Acesso a dados da tela de Cadastro de Empresas (MySQL)
'               Informações de acesso (Tipo / Subtipo / Valor) em tb_cad_empresa_acesso
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

        'Parâmetros na ordem da sp_update_cadastro_empresa
        'Os dados de acesso (TeamViewer, MSTSC, VPN, SQL, Windows) ficam em tb_cad_empresa_acesso (região ACESSO)
        Dim sParametros() As String = {"codigo_empresa", "cnpj_cpf", "razao_social", "nome_fantasia", "ativo", "telefone", _
                                       "arquivo", "modulo_empresa", "tipo_time", "codigo_usuario_bkp", _
                                       "focal_point", "celular", "email", "observacao"}

        Try

            For Each sParametro As String In sParametros

                'Seta Parametros
                oMySqlParameter(i) = New MySqlParameter
                oMySqlParameter(i).ParameterName = "@p_" & sParametro
                oMySqlParameter(i).Direction = ParameterDirection.Input

                If sParametro = "codigo_empresa" OrElse sParametro = "ativo" OrElse sParametro = "tipo_time" Then
                    oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
                ElseIf sParametro = "codigo_usuario_bkp" Then
                    oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
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

    Public Function ParametrosResponsavelBkp(ByVal iTipoTime As Integer, _
                                             ByVal lCodigo As Long) As MySqlParameter()

        'Parâmetros do combo de Responsável BKP (usuários do time da empresa)
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        'Seta Parametros - Código Empresa
        oMySqlParameter(i) = New MySqlParameter
        oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
        oMySqlParameter(i).Direction = ParameterDirection.Input
        oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
        oMySqlParameter(i).Value = goUsuario.iEmpresa : i += 1 : ReDim Preserve oMySqlParameter(i)

        'Seta Parametros - Time da empresa
        oMySqlParameter(i) = New MySqlParameter
        oMySqlParameter(i).ParameterName = "@p_tipo_time"
        oMySqlParameter(i).Direction = ParameterDirection.Input
        oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
        oMySqlParameter(i).Value = iTipoTime : i += 1 : ReDim Preserve oMySqlParameter(i)

        'Seta Parametros - Empresa (cadastro): mantém o responsável atual no combo
        oMySqlParameter(i) = New MySqlParameter
        oMySqlParameter(i).ParameterName = "@p_empresa"
        oMySqlParameter(i).Direction = ParameterDirection.Input
        oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
        oMySqlParameter(i).Value = lCodigo

        Return oMySqlParameter

    End Function

#End Region

#Region "::: ACESSO :::"

    Public Sub LoadGridAcesso(ByVal oGrid As GridEX, _
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
            oMySqlParameter(i).ParameterName = "@p_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigo

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_cadastro_empresa_acesso", oMySqlParameter)

            'Tipo, subtipo e valor são editados na própria grid
            For Each oColuna As DataColumn In oDataSet.Tables(0).Columns
                oColuna.ReadOnly = False
            Next

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

    Public Function InsertAcesso(ByVal lCodigo As Long, _
                                 ByVal sTipo As String, _
                                 ByVal sSubtipo As String, _
                                 ByVal sValor As String) As Long

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

            'Seta Parametros - Empresa (cadastro)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Tipo
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_tipo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sTipo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Subtipo
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_subtipo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sSubtipo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Valor
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_valor"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sValor : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código gerado
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Output
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_insert_cadastro_empresa_acesso", oMySqlParameter)

            Return CLng(oMySqlParameter(i).Value)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub UpdateAcesso(ByVal lCodigoAcesso As Long, _
                            ByVal sTipo As String, _
                            ByVal sSubtipo As String, _
                            ByVal sValor As String)

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

            'Seta Parametros - Código do acesso
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = lCodigoAcesso : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Tipo
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_tipo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sTipo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Subtipo
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_subtipo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sSubtipo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Valor
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_valor"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sValor : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_cadastro_empresa_acesso", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub DeleteAcesso(ByVal lCodigoAcesso As Long)

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

            'Seta Parametros - Código do acesso
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = lCodigoAcesso

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_delete_cadastro_empresa_acesso", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function LoadSugestaoSubtipoAcesso() As DataTable

        'Todos os subtipos já usados (sugestões da coluna Subtipo da grid)
        Dim oDataSet As DataSet

        Try

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_combo_cadastro_empresa_acesso_subtipo", ParametrosSubtipoAcesso(""))

            Return oDataSet.Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ParametrosSubtipoAcesso(ByVal sTipo As String) As MySqlParameter()

        'Parâmetros do combo de subtipo (sugestões já usadas para o tipo informado)
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        'Seta Parametros - Código Empresa
        oMySqlParameter(i) = New MySqlParameter
        oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
        oMySqlParameter(i).Direction = ParameterDirection.Input
        oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
        oMySqlParameter(i).Value = goUsuario.iEmpresa : i += 1 : ReDim Preserve oMySqlParameter(i)

        'Seta Parametros - Tipo
        oMySqlParameter(i) = New MySqlParameter
        oMySqlParameter(i).ParameterName = "@p_tipo"
        oMySqlParameter(i).Direction = ParameterDirection.Input
        oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
        oMySqlParameter(i).Value = sTipo

        Return oMySqlParameter

    End Function

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
