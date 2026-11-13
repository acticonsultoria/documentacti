'===============================================================================================
'Classe:        clsUsrAdmMonitorFiscal
'Objetivo:      Acesso a dados do Monitor Fiscal - notas técnicas e agenda RTC (MySQL)
'===============================================================================================
Imports System.Data
Imports MySqlConnector
Imports Documentacti.MySQLHelper

Public Class clsUsrAdmMonitorFiscal

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

    Public Function LoadNotaTecnica() As DataTable

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa)

            'Executa Query
            Return ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_nota_tecnica", oMySqlParameter).Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function LoadUltimaColeta() As DataRow

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa)

            'Executa Query (ultima_coleta / minutos nulos = nunca coletou)
            Return ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_nota_tecnica_coleta", oMySqlParameter).Tables(0).Rows(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub InsertNotaTecnica(ByVal oNota As clsMonitorFiscal.NotaTecnica, ByVal bNotificar As Boolean)

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_documento", MySqlDbType.VarChar, oNota.sDocumento) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_numero", MySqlDbType.VarChar, oNota.sNumero) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_versao", MySqlDbType.VarChar, oNota.sVersao) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_data_publicacao", MySqlDbType.Date, IIf(oNota.dPublicacao.HasValue, oNota.dPublicacao, Nothing)) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_resumo", MySqlDbType.VarChar, oNota.sResumo) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_url", MySqlDbType.VarChar, oNota.sUrl) : i += 1 : ReDim Preserve oMySqlParameter(i)
            oMySqlParameter(i) = Parametro("@p_notificar", MySqlDbType.Int16, IIf(bNotificar, 1, 0))

            'Executa Query (nota nova ou nova versão notifica os usuários no sino, exceto na primeira coleta)
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_insert_administracao_nota_tecnica", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function AtualizarNotasTecnicas(ByVal bForcar As Boolean) As Boolean

        'Lê os portais oficiais e grava as notas (nota nova / nova versão vai para o sino de todos).
        'Sem forçar, respeita a janela de 60 minutos desde a última coleta de qualquer máquina.
        'Retorna True quando coletou. Roda fora da thread da tela.
        Try

            If bForcar = False Then
                Dim oUltima As DataRow = LoadUltimaColeta()
                If IsDBNull(oUltima.Item("minutos")) = False AndAlso CInt(oUltima.Item("minutos")) < clsMonitorFiscal.gcIntervaloColetaMinutos Then Return False
            End If

            Dim oNotas As List(Of clsMonitorFiscal.NotaTecnica) = clsMonitorFiscal.ColetarNotasTecnicas()

            'Primeira coleta de um documento (nenhuma nota dele gravada antes deste lote): grava sem notificar,
            'para o sino não receber de uma vez todas as notas antigas
            Dim oGravadas As DataTable = LoadNotaTecnica()

            For Each oNota As clsMonitorFiscal.NotaTecnica In oNotas
                Call InsertNotaTecnica(oNota, oGravadas.Select("documento = '" & oNota.sDocumento & "'").Length > 0)
            Next

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function LoadAgenda() As DataTable

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            oMySqlParameter(i) = Parametro("@p_codigo_empresa", MySqlDbType.Int32, goUsuario.iEmpresa)

            'Executa Query
            Return ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_agenda_fiscal", oMySqlParameter).Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

End Class
