'===============================================================================================
'Classe:        clsFrmRecorrencia
'Objetivo:      Acesso a dados da recorrência de chamados (MySQL)
'               O chamado modelo é a 1ª ocorrência; as demais são geradas uma vez por dia
'               (primeiro login do dia) pela sp_insert_administracao_chamado_recorrencia_gerar
'===============================================================================================
Imports System.Data
Imports MySqlConnector
Imports Documentacti.MySQLHelper

Public Class clsFrmRecorrencia

#Region "::: REGRA :::"

    'Regra de recorrência (padrão do Outlook): mesmo formato da tb_cad_atividade_recorrencia
    Public Class RegraRecorrencia

        Public lCodigo As Long = 0                 '0 = ainda não gravada
        Public iTipo As Integer = 2                '1 diário, 2 semanal, 3 mensal, 4 anual
        Public iIntervalo As Integer = 1
        Public bDiasUteis As Boolean = False
        Public iDiasSemana As Integer = 0          '1 dom, 2 seg, 4 ter, 8 qua, 16 qui, 32 sex, 64 sáb
        Public iModoMes As Integer = 1             '1 dia do mês, 2 N-ésimo dia da semana
        Public iDiaMes As Integer = 1
        Public iOrdinal As Integer = 1             '1 a 4, 5 = último(a)
        Public iDiaSemana As Integer = 1           '1 domingo ... 7 sábado
        Public iMes As Integer = 1
        Public dDataInicio As Date = Date.Today
        Public iTipoTermino As Integer = 0         '0 sem término, 1 após N ocorrências, 2 termina em
        Public iOcorrencias As Integer = 10
        Public dDataTermino As Date = Date.Today.AddMonths(3)

        'Somente leitura (situação atual)
        Public bAtivo As Boolean = True
        Public bModelo As Boolean = True
        Public lChamadoModelo As Long = 0
        Public oProximaOcorrencia As Object = Nothing
        Public iChamadosGerados As Integer = 0
        Public sUltimoErro As String = ""

        Public Function Descricao() As String

            'Texto curto para a barra do chamado (ex.: "Semanal: a cada 1 semana(s) - seg, qui")
            Dim sNomesDia() As String = {"dom", "seg", "ter", "qua", "qui", "sex", "sáb"}
            Dim sNomesMes() As String = {"jan", "fev", "mar", "abr", "mai", "jun", "jul", "ago", "set", "out", "nov", "dez"}
            Dim sOrdinal() As String = {"1º(ª)", "2º(ª)", "3º(ª)", "4º(ª)", "último(a)"}
            Dim sTexto As String

            Select Case iTipo
                Case 1
                    sTexto = IIf(bDiasUteis, "Diário: dias úteis", "Diário: a cada " & iIntervalo & " dia(s)")
                Case 2
                    Dim oDias As New List(Of String)
                    For i As Integer = 0 To 6
                        If (iDiasSemana And (1 << i)) <> 0 Then oDias.Add(sNomesDia(i))
                    Next
                    sTexto = "Semanal: a cada " & iIntervalo & " semana(s) - " & String.Join(", ", oDias)
                Case 3
                    sTexto = "Mensal: a cada " & iIntervalo & " mês(es) - " & _
                             IIf(iModoMes = 1, "dia " & iDiaMes, sOrdinal(iOrdinal - 1) & " " & sNomesDia(iDiaSemana - 1))
                Case Else
                    sTexto = "Anual: a cada " & iIntervalo & " ano(s) - " & _
                             IIf(iModoMes = 1, iDiaMes & "/" & sNomesMes(iMes - 1), sOrdinal(iOrdinal - 1) & " " & sNomesDia(iDiaSemana - 1) & " de " & sNomesMes(iMes - 1))
            End Select

            Return sTexto

        End Function

    End Class

#End Region

#Region "::: FUNCTION / SUB :::"

    Public Function Load(ByVal lChamado As Long) As RegraRecorrencia

        'Regra em que o chamado é modelo, ou a regra que o gerou; Nothing quando não há recorrência
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

            'Seta Parametros - Chamado
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_chamado"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lChamado

            'Executa Query
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_chamado_recorrencia", oMySqlParameter)

            If oDataSet.Tables(0).Rows.Count = 0 Then Return Nothing

            Dim oRegra As New RegraRecorrencia

            With oDataSet.Tables(0).Rows(0)
                oRegra.lCodigo = CLng(.Item("codigo"))
                oRegra.iTipo = CInt(.Item("tipo"))
                oRegra.iIntervalo = CInt(.Item("intervalo"))
                oRegra.bDiasUteis = (CInt(.Item("dias_uteis")) = 1)
                oRegra.iDiasSemana = CInt(.Item("dias_semana"))
                oRegra.iModoMes = CInt(.Item("modo_mes"))
                If IsDBNull(.Item("dia_mes")) = False Then oRegra.iDiaMes = CInt(.Item("dia_mes"))
                If IsDBNull(.Item("ordinal")) = False Then oRegra.iOrdinal = CInt(.Item("ordinal"))
                If IsDBNull(.Item("dia_semana")) = False Then oRegra.iDiaSemana = CInt(.Item("dia_semana"))
                If IsDBNull(.Item("mes")) = False Then oRegra.iMes = CInt(.Item("mes"))
                oRegra.dDataInicio = CDate(.Item("data_inicio"))
                oRegra.iTipoTermino = CInt(.Item("tipo_termino"))
                If IsDBNull(.Item("ocorrencias")) = False Then oRegra.iOcorrencias = CInt(.Item("ocorrencias"))
                If IsDBNull(.Item("data_termino")) = False Then oRegra.dDataTermino = CDate(.Item("data_termino"))
                oRegra.bAtivo = (CInt(.Item("ativo")) = 1)
                oRegra.bModelo = (CInt(.Item("modelo")) = 1)
                oRegra.lChamadoModelo = CLng(.Item("codigo_atividade_modelo"))
                oRegra.oProximaOcorrencia = IIf(IsDBNull(.Item("proxima_ocorrencia")), Nothing, .Item("proxima_ocorrencia"))
                oRegra.iChamadosGerados = CInt(.Item("chamados_gerados"))
                oRegra.sUltimoErro = .Item("ultimo_erro").ToString
            End With

            Return oRegra

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function ParametrosRegra(ByVal oRegra As RegraRecorrencia, _
                                     ByVal lChave As Long, _
                                     ByVal sNomeChave As String, _
                                     ByVal bSaidaCodigo As Boolean) As MySqlParameter()

        'Parâmetros comuns de insert (chave = chamado modelo) e update (chave = código da recorrência)
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0
        Dim oValores As New List(Of KeyValuePair(Of String, Object))

        oValores.Add(New KeyValuePair(Of String, Object)("p_codigo_empresa", goUsuario.iEmpresa))
        oValores.Add(New KeyValuePair(Of String, Object)(sNomeChave, lChave))
        oValores.Add(New KeyValuePair(Of String, Object)("p_tipo", oRegra.iTipo))
        oValores.Add(New KeyValuePair(Of String, Object)("p_intervalo", oRegra.iIntervalo))
        oValores.Add(New KeyValuePair(Of String, Object)("p_dias_uteis", IIf(oRegra.bDiasUteis, 1, 0)))
        oValores.Add(New KeyValuePair(Of String, Object)("p_dias_semana", oRegra.iDiasSemana))
        oValores.Add(New KeyValuePair(Of String, Object)("p_modo_mes", oRegra.iModoMes))
        oValores.Add(New KeyValuePair(Of String, Object)("p_dia_mes", oRegra.iDiaMes))
        oValores.Add(New KeyValuePair(Of String, Object)("p_ordinal", oRegra.iOrdinal))
        oValores.Add(New KeyValuePair(Of String, Object)("p_dia_semana", oRegra.iDiaSemana))
        oValores.Add(New KeyValuePair(Of String, Object)("p_mes", oRegra.iMes))
        oValores.Add(New KeyValuePair(Of String, Object)("p_data_inicio", oRegra.dDataInicio.Date))
        oValores.Add(New KeyValuePair(Of String, Object)("p_tipo_termino", oRegra.iTipoTermino))
        oValores.Add(New KeyValuePair(Of String, Object)("p_ocorrencias", oRegra.iOcorrencias))
        oValores.Add(New KeyValuePair(Of String, Object)("p_data_termino", oRegra.dDataTermino.Date))
        oValores.Add(New KeyValuePair(Of String, Object)("p_codigo_usuario", goUsuario.lUsuario))

        For Each oValor As KeyValuePair(Of String, Object) In oValores

            'Seta Parametros
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@" & oValor.Key
            oMySqlParameter(i).Direction = ParameterDirection.Input

            Select Case oValor.Key
                Case "p_data_inicio", "p_data_termino" : oMySqlParameter(i).MySqlDbType = MySqlDbType.Date
                Case "p_codigo_usuario" : oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
                Case Else : oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            End Select

            oMySqlParameter(i).Value = oValor.Value : i += 1 : ReDim Preserve oMySqlParameter(i)

        Next

        If bSaidaCodigo Then
            'Seta Parametros - Código gerado
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Output
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
        Else
            ReDim Preserve oMySqlParameter(i - 1)
        End If

        Return oMySqlParameter

    End Function

    Public Function Insert(ByVal lChamado As Long, _
                           ByVal oRegra As RegraRecorrencia) As Long

        Try

            Dim oMySqlParameter() As MySqlParameter = ParametrosRegra(oRegra, lChamado, "p_codigo_chamado", True)

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_insert_administracao_chamado_recorrencia", oMySqlParameter)

            Return CLng(oMySqlParameter(oMySqlParameter.Length - 1).Value)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub Update(ByVal oRegra As RegraRecorrencia)

        Try

            'Executa Query (altera só as próximas ocorrências; os chamados já gerados não mudam)
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_chamado_recorrencia", _
                            ParametrosRegra(oRegra, oRegra.lCodigo, "p_codigo", False))

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub Encerrar(ByVal lCodigo As Long)

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

            'Seta Parametros - Código da recorrência
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = lCodigo : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Usuário
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_update_administracao_chamado_recorrencia_encerrar", oMySqlParameter)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function GerarChamados() As Integer

        'Gera os chamados recorrentes vencidos; a proc só executa uma vez por dia (primeiro login do dia)
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Empresa
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = goUsuario.iEmpresa : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Usuário (quem disparou a geração do dia)
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_usuario"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int64
            oMySqlParameter(i).Value = goUsuario.lUsuario : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Quantidade gerada
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_gerados"
            oMySqlParameter(i).Direction = ParameterDirection.Output
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32

            'Executa Query
            ExecuteNonQuery(goDatabase.sConnection, CommandType.StoredProcedure, "sp_insert_administracao_chamado_recorrencia_gerar", oMySqlParameter)

            Return IIf(IsDBNull(oMySqlParameter(i).Value), 0, CInt(oMySqlParameter(i).Value))

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

End Class
