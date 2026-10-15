'===============================================================================================
'Classe:        clsUsrAdmChamado
'Objetivo:      Acesso a dados da tela de Chamados / Apontamentos / Comentários de DEV (MySQL)
'===============================================================================================
Imports System.Data
Imports MySql.Data.MySqlClient
Imports Documentacti.MySQLHelper
Imports Janus.Windows.GridEX

Public Class clsUsrAdmChamado

#Region "::: FUNCTION / SUB :::"

#Region "::: GERAL :::"

    Public Function Insert(ByVal iEmpresaCliente As Integer, _
                           ByVal sTitulo As String, _
                           ByVal sDescricao As String, _
                           ByVal sSolicitante As String, _
                           ByVal iCodigoTipoAtividade As Integer, _
                           ByVal iPrioridade As Integer, _
                           ByVal sModulo As String, _
                           ByVal sTela As String, _
                           ByVal sNumeroDocumento As String, _
                           ByVal sResponsaveis As String, _
                           ByVal sTiposAtendimento As String) As Long

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

            'Seta Parametros - Empresa (Cliente)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iEmpresaCliente : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Título (Atividade)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_titulo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sTitulo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Descrição
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_descricao"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.MediumText
            oMySqlParameter(i).Value = sDescricao : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Solicitante
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_solicitante"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sSolicitante : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Tipo Atividade
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_tipo_atividade"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int16
            oMySqlParameter(i).Value = iCodigoTipoAtividade : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Prioridade
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_prioridade"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iPrioridade : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Módulo
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_modulo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sModulo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Tela
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_tela"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sTela : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Número Documento
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_numero_documento"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sNumeroDocumento : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Responsáveis (CSV)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_responsaveis"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sResponsaveis : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Tipos de Atendimento (CSV)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_tipos_atendimento"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sTiposAtendimento : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código (OUT)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Output
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_insert_administracao_chamado", oMySqlParameter)

            Return CLng(oMySqlParameter(i).Value)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub Update(ByVal lCodigo As Long, _
                      ByVal iEmpresaCliente As Integer, _
                      ByVal sTitulo As String, _
                      ByVal sDescricao As String, _
                      ByVal sSolicitante As String, _
                      ByVal iCodigoTipoAtividade As Integer, _
                      ByVal iPrioridade As Integer, _
                      ByVal sModulo As String, _
                      ByVal sTela As String, _
                      ByVal sNumeroDocumento As String, _
                      ByVal sResponsaveis As String, _
                      ByVal sTiposAtendimento As String)

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

            'Seta Parametros - Código
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Empresa (Cliente)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iEmpresaCliente : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Título (Atividade)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_titulo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sTitulo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Descrição
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_descricao"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.MediumText
            oMySqlParameter(i).Value = sDescricao : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Solicitante
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_solicitante"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sSolicitante : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Tipo Atividade
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_tipo_atividade"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int16
            oMySqlParameter(i).Value = iCodigoTipoAtividade : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Prioridade
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_prioridade"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iPrioridade : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Módulo
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_modulo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sModulo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Tela
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_tela"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sTela : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Número Documento
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_numero_documento"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sNumeroDocumento : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Responsáveis (CSV)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_responsaveis"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sResponsaveis : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Tipos de Atendimento (CSV)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_tipos_atendimento"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sTiposAtendimento : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_chamado", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function UpdateStatus(ByVal lCodigo As Long, _
                                 ByVal iStatus As Integer) As Integer

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

            'Seta Parametros - Código
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Status
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_status"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iStatus : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Apontamentos Encerrados (OUT)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_apontamentos_encerrados"
            oMySqlParameter(i).Direction = ParameterDirection.Output
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_chamado_status", oMySqlParameter)

            Return IIf(IsDBNull(oMySqlParameter(i).Value), 0, oMySqlParameter(i).Value)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub LoadGrid(ByVal oGrid As GridEX, _
                        ByVal oDataInicio As Object, _
                        ByVal oDataTermino As Object, _
                        ByVal sCodigo As String, _
                        ByVal iEmpresaCliente As Integer, _
                        ByVal sStatus As String, _
                        ByVal sTitulo As String, _
                        ByVal lResponsavel As Long, _
                        ByVal iTipoAtendimento As Integer)

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

            'Seta Parametros - Data Início
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_data_inicio"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Date
            oMySqlParameter(i).Value = IIf(IsNothing(oDataInicio), DBNull.Value, oDataInicio) : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Data Término
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_data_termino"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Date
            oMySqlParameter(i).Value = IIf(IsNothing(oDataTermino), DBNull.Value, oDataTermino) : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sCodigo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Empresa (Cliente)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iEmpresaCliente : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Status (CSV)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_status"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sStatus : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Título
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_titulo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sTitulo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Responsável
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_responsavel"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = lResponsavel : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Tipo de Atendimento
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_tipo_atendimento"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iTipoAtendimento

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_chamado_lista", oMySqlParameter)

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

#Region "::: COMENTÁRIO :::"

    Public Sub LoadGridComentario(ByVal oGrid As GridEX, _
                                  ByVal lCodigoAtividade As Long)

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

            'Seta Parametros - Código Atividade
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_atividade"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigoAtividade

            'Executa Query (comentários visíveis ao cliente, sem datetime(3) que o conector não converte)
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_chamado_comentario", oMySqlParameter)

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

    Public Sub InsertComentario(ByVal lCodigoAtividade As Long, _
                                ByVal sComentario As String)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Atividade
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_atividade"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigoAtividade : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = goUsuario.sNome : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Texto
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_texto"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.MediumText
            oMySqlParameter(i).Value = sComentario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Cliente (0 = equipe)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_cliente"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int16
            oMySqlParameter(i).Value = 0 : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código (OUT)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Output
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int16

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_sup_insert_comentario_chamado", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "::: RELATÓRIO :::"

    Public Function LoadGridRelatorio(ByVal oGrid As GridEX, _
                                      ByVal oDataInicio As Object, _
                                      ByVal oDataTermino As Object, _
                                      ByVal iEmpresaCliente As Integer, _
                                      ByVal iTipoAtividade As Integer, _
                                      ByVal iTipoAtendimento As Integer, _
                                      ByVal lUsuario As Long, _
                                      ByVal sAgrupar As String) As DataTable

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

            'Seta Parametros - Data Início
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_data_inicio"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Date
            oMySqlParameter(i).Value = IIf(IsNothing(oDataInicio), DBNull.Value, oDataInicio) : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Data Término
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_data_termino"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Date
            oMySqlParameter(i).Value = IIf(IsNothing(oDataTermino), DBNull.Value, oDataTermino) : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Empresa (Cliente)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iEmpresaCliente : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Tipo de Atividade
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_tipo_atividade"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iTipoAtividade : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Tipo de Atendimento
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_tipo_atendimento"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iTipoAtendimento : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Responsável
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = lUsuario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Agrupar por
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_agrupar"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sAgrupar

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_chamado_relatorio", oMySqlParameter)

            'Carrega Grid
            Call ConfigurarDataMemberGrid(oGrid)
            oGrid.DataSource = oDataSet.DefaultViewManager
            oGrid.DataMember = oDataSet.Tables(0).TableName

            Return oDataSet.Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "::: ARQUIVO :::"

    Public Sub LoadGridArquivo(ByVal oGrid As GridEX, _
                               ByVal lCodigoAtividade As Long)

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

            'Seta Parametros - Código Atividade
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_atividade"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigoAtividade

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_chamado_anexo", oMySqlParameter)

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

    Public Function InsertArquivo(ByVal lCodigoAtividade As Long, _
                                  ByVal sNomeArquivo As String) As Long

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

            'Seta Parametros - Código Atividade
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_atividade"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigoAtividade : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Nome do Arquivo (caminho relativo no FTP)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_nome_arquivo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sNomeArquivo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código (OUT)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Output
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_insert_administracao_chamado_anexo", oMySqlParameter)

            Return CLng(oMySqlParameter(i).Value)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteArquivo(ByVal lCodigoAtividade As Long, _
                                  ByVal lCodigo As Long) As Integer

        'Retorna a quantidade de OUTROS registros que usam o mesmo arquivo
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Empresa
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = goUsuario.iEmpresa : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Atividade
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_atividade"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigoAtividade : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código do Anexo
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = lCodigo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Outras Referências (OUT)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_outras_referencias"
            oMySqlParameter(i).Direction = ParameterDirection.Output
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_delete_administracao_chamado_anexo", oMySqlParameter)

            Return CInt(oMySqlParameter(i).Value)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "::: COMENTÁRIO DEV :::"

    Public Sub LoadGridComentarioDev(ByVal oGrid As GridEX, _
                                     ByVal lCodigoAtividade As Long)

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

            'Seta Parametros - Código Atividade
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_atividade"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigoAtividade : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Usuário (a proc valida se é usuário interno)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_chamado_comentario_dev", oMySqlParameter)

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

    Public Sub InsertComentarioDev(ByVal lCodigoAtividade As Long, _
                                   ByVal sComentario As String)

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

            'Seta Parametros - Código Atividade
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_atividade"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigoAtividade : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Comentário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_comentario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.MediumText
            oMySqlParameter(i).Value = sComentario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Comentário (OUT)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_comentario"
            oMySqlParameter(i).Direction = ParameterDirection.Output
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_insert_administracao_chamado_comentario_dev", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "::: APONTAMENTO :::"

    Public Sub LoadComboChamadoApontamento(ByVal oComboBox As Janus.Windows.EditControls.UIComboBox, _
                                           ByVal iEmpresaCliente As Integer)

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

            'Seta Parametros - Empresa (Cliente)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iEmpresaCliente

            'Carrega Combo
            Call LoadCombo(oComboBox, "sp_select_combo_administracao_chamado_apontamento", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub LoadGridApontamentoHistorico(ByVal oGrid As GridEX, _
                                            ByVal lUsuario As Long, _
                                            ByVal iEmpresaCliente As Integer, _
                                            ByVal lChamado As Long, _
                                            ByVal oDataInicio As Object, _
                                            ByVal oDataTermino As Object)

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

            'Seta Parametros - Usuário (-1 = todos)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = lUsuario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Empresa Cliente (-1 = todas)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iEmpresaCliente : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Chamado (-1 = todos)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_chamado"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lChamado : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Data Início
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_data_inicio"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Date
            oMySqlParameter(i).Value = IIf(IsNothing(oDataInicio), DBNull.Value, oDataInicio) : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Data Término
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_data_termino"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Date
            oMySqlParameter(i).Value = IIf(IsNothing(oDataTermino), DBNull.Value, oDataTermino)

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_chamado_apontamento_historico", oMySqlParameter)

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

    Public Function LoadGridApontamentoAndamento(ByVal oGrid As GridEX) As DataTable

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

            'Seta Parametros - Código Usuário (somente os apontamentos do usuário autenticado)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_chamado_apontamento_andamento", oMySqlParameter)

            'Coluna de duração é atualizada pelo Timer da tela
            oDataSet.Tables(0).Columns("duracao").ReadOnly = False
            oDataSet.Tables(0).Columns("duracao").MaxLength = -1

            'Carrega Grid
            Call ConfigurarDataMemberGrid(oGrid)
            oGrid.DataSource = oDataSet.DefaultViewManager
            oGrid.DataMember = oDataSet.Tables(0).TableName

            Return oDataSet.Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function InsertApontamento(ByVal lChamado As Long) As Integer

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

            'Seta Parametros - Chamado
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_chamado"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lChamado : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Apontamento (OUT)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_apontamento"
            oMySqlParameter(i).Direction = ParameterDirection.Output
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_insert_administracao_chamado_apontamento", oMySqlParameter)

            Return CInt(oMySqlParameter(i).Value)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub EncerrarApontamento(ByVal iEmpresa As Integer, _
                                   ByVal lUsuario As Long, _
                                   ByVal lChamado As Long, _
                                   ByVal iApontamento As Integer)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Empresa
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iEmpresa : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = lUsuario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Chamado
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_chamado"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lChamado : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Apontamento
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_apontamento"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iApontamento : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Usuário Alteração
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario_alteracao"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_chamado_apontamento_encerrar", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub UpdateApontamento(ByVal iEmpresa As Integer, _
                                 ByVal lUsuario As Long, _
                                 ByVal lChamado As Long, _
                                 ByVal iApontamento As Integer, _
                                 ByVal dDataHoraInicio As Date, _
                                 ByVal oDataHoraFim As Object, _
                                 ByVal sObservacao As String)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Empresa
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iEmpresa : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = lUsuario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Chamado
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_chamado"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lChamado : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Código Apontamento
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_apontamento"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = iApontamento : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Data/Hora Início
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_data_hora_inicio"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.DateTime
            oMySqlParameter(i).Value = dDataHoraInicio : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Data/Hora Fim
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_data_hora_fim"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.DateTime
            oMySqlParameter(i).Value = IIf(IsNothing(oDataHoraFim), DBNull.Value, oDataHoraFim) : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Observação
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_observacao"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = sObservacao : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Usuário Alteração
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario_alteracao"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_chamado_apontamento", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#End Region

End Class
