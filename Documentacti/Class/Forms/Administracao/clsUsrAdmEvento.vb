'===============================================================================================
'Classe:        clsUsrAdmEvento
'Objetivo:      Acesso a dados do Calendário de Eventos (MySQL)
'===============================================================================================
Imports System.Data
Imports MySqlConnector
Imports Documentacti.MySQLHelper

Public Class clsUsrAdmEvento

#Region "::: FUNCTION / SUB :::"

    Private Function Parametro(ByVal sNome As String, _
                               ByVal oTipo As MySqlDbType, _
                               ByVal oValor As Object, _
                               Optional ByVal oDirecao As ParameterDirection = ParameterDirection.Input) As MySqlParameter

        'Monta um parâmetro de entrada (nulo quando o valor não foi informado)
        Dim oMySqlParameter As New MySqlParameter

        oMySqlParameter.ParameterName = sNome
        oMySqlParameter.Direction = oDirecao
        oMySqlParameter.MySqlDbType = oTipo
        oMySqlParameter.Value = IIf(IsNothing(oValor), DBNull.Value, oValor)

        Return oMySqlParameter

    End Function

    Public Function LoadGrid(ByVal oDataInicio As Object, _
                             ByVal oDataTermino As Object, _
                             ByVal iTipoEvento As Integer, _
                             ByVal sParticipantes As String, _
                             ByVal sTimes As String, _
                             ByVal sEmpresas As String, _
                             ByVal sTitulo As String) As DataTable

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_data_inicio", MySqlDbType.Date, oDataInicio) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_data_termino", MySqlDbType.Date, oDataTermino) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo_tipo_evento", MySqlDbType.Int32, iTipoEvento) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_participantes", MySqlDbType.VarChar, sParticipantes) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_times", MySqlDbType.VarChar, sTimes) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_empresas", MySqlDbType.VarChar, sEmpresas) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_titulo", MySqlDbType.VarChar, sTitulo)

            'Executa Query
            Return ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_evento_lista", oMySqlParameter).Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function ParametrosEvento(ByVal sTitulo As String, _
                                      ByVal iTipoEvento As Integer, _
                                      ByVal dInicio As Date, _
                                      ByVal dFim As Date, _
                                      ByVal bDiaInteiro As Boolean, _
                                      ByVal sLocal As String, _
                                      ByVal sEmpresas As String, _
                                      ByVal oCodigoAtividade As Object, _
                                      ByVal sDescricao As String, _
                                      ByVal sParticipantes As String, _
                                      ByVal sTimes As String) As List(Of MySqlParameter)

        'Campos comuns ao Insert e ao Update (na ordem da proc, depois do código)
        Dim oLista As New List(Of MySqlParameter)

        oLista.Add(Parametro("@p_titulo", MySqlDbType.VarChar, sTitulo))
        oLista.Add(Parametro("@p_codigo_tipo_evento", MySqlDbType.Int16, iTipoEvento))
        oLista.Add(Parametro("@p_data_inicio", MySqlDbType.DateTime, dInicio))
        oLista.Add(Parametro("@p_data_fim", MySqlDbType.DateTime, dFim))
        oLista.Add(Parametro("@p_dia_inteiro", MySqlDbType.Int16, IIf(bDiaInteiro, 1, 0)))
        oLista.Add(Parametro("@p_local", MySqlDbType.VarChar, sLocal))
        oLista.Add(Parametro("@p_empresas", MySqlDbType.VarChar, sEmpresas))
        oLista.Add(Parametro("@p_codigo_atividade", MySqlDbType.Int32, oCodigoAtividade))
        oLista.Add(Parametro("@p_descricao", MySqlDbType.VarChar, sDescricao))
        oLista.Add(Parametro("@p_participantes", MySqlDbType.VarChar, sParticipantes))
        oLista.Add(Parametro("@p_times", MySqlDbType.VarChar, sTimes))

        Return oLista

    End Function

    Public Function Insert(ByVal sTitulo As String, _
                           ByVal iTipoEvento As Integer, _
                           ByVal dInicio As Date, _
                           ByVal dFim As Date, _
                           ByVal bDiaInteiro As Boolean, _
                           ByVal sLocal As String, _
                           ByVal sEmpresas As String, _
                           ByVal oCodigoAtividade As Object, _
                           ByVal sDescricao As String, _
                           ByVal sParticipantes As String, _
                           ByVal sTimes As String) As Long

        'Variaveis Locais
        Dim oLista As New List(Of MySqlParameter)
        Dim oCodigo As MySqlParameter

        Try

            oLista.Add(Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa))
            oLista.AddRange(ParametrosEvento(sTitulo, iTipoEvento, dInicio, dFim, bDiaInteiro, sLocal, sEmpresas, oCodigoAtividade, sDescricao, sParticipantes, sTimes))
            oLista.Add(Parametro("@p_codigo_usuario", MySqlDbType.Int64, goUsuario.lUsuario))
            oCodigo = Parametro("@p_codigo", MySqlDbType.Int32, Nothing, ParameterDirection.Output)
            oLista.Add(oCodigo)

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_insert_administracao_evento", oLista.ToArray)

            Return CLng(oCodigo.Value)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub Update(ByVal lCodigo As Long, _
                      ByVal sTitulo As String, _
                      ByVal iTipoEvento As Integer, _
                      ByVal dInicio As Date, _
                      ByVal dFim As Date, _
                      ByVal bDiaInteiro As Boolean, _
                      ByVal sLocal As String, _
                      ByVal sEmpresas As String, _
                      ByVal oCodigoAtividade As Object, _
                      ByVal sDescricao As String, _
                      ByVal sParticipantes As String, _
                      ByVal sTimes As String)

        'Variaveis Locais
        Dim oLista As New List(Of MySqlParameter)

        Try

            oLista.Add(Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa))
            oLista.Add(Parametro("@p_codigo", MySqlDbType.Int32, lCodigo))
            oLista.AddRange(ParametrosEvento(sTitulo, iTipoEvento, dInicio, dFim, bDiaInteiro, sLocal, sEmpresas, oCodigoAtividade, sDescricao, sParticipantes, sTimes))
            oLista.Add(Parametro("@p_codigo_usuario", MySqlDbType.Int64, goUsuario.lUsuario))

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_evento", oLista.ToArray)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub UpdateData(ByVal lCodigo As Long, _
                          ByVal dInicio As Date, _
                          ByVal dFim As Date)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo", MySqlDbType.Int32, lCodigo) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_data_inicio", MySqlDbType.DateTime, dInicio) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_data_fim", MySqlDbType.DateTime, dFim) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo_usuario", MySqlDbType.Int64, goUsuario.lUsuario)

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_evento_data", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub Delete(ByVal lCodigo As Long)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo", MySqlDbType.Int32, lCodigo) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo_usuario", MySqlDbType.Int64, goUsuario.lUsuario)

            'Executa Query (exclusão lógica)
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_delete_administracao_evento", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function LoadComboEmpresa(ByVal sTimes As String) As DataTable

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_times", MySqlDbType.VarChar, sTimes)

            'Executa Query (times vazio = todas as empresas ativas)
            Return ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_combo_administracao_evento_empresa", oMySqlParameter).Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function LoadNotificacao() As DataTable

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo_usuario", MySqlDbType.Int64, goUsuario.lUsuario)

            'Executa Query
            Return ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_evento_notificacao", oMySqlParameter).Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub UpdateNotificacao(ByVal lCodigoEvento As Long)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo_evento", MySqlDbType.Int32, lCodigoEvento) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_codigo_usuario", MySqlDbType.Int64, goUsuario.lUsuario)

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_evento_notificacao", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class
