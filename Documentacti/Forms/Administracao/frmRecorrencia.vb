Imports Janus.Windows.EditControls

Public Class frmRecorrencia

#Region "::: VARIAVEIS :::"

    Public Enum ResultadoRecorrencia
        Cancelado = 0
        Salvar = 1
        Remover = 2
    End Enum

    'Regra aberta na tela (Nothing = chamado ainda sem recorrência) e resultado escolhido
    Public oRegra As clsFrmRecorrencia.RegraRecorrencia = Nothing
    Public eResultado As ResultadoRecorrencia = ResultadoRecorrencia.Cancelado

    Private oDiasSemana() As UICheckBox

#End Region

#Region "::: CONTROLES :::"

    Private Sub frmRecorrencia_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            Call ConfigurarForm()

        Catch ex As Exception
            Call TratamentoErro(ex, Me.Text)
        End Try

    End Sub

    Private Sub optPadrao_CheckedChanged(sender As Object, e As EventArgs) Handles optDiario.CheckedChanged, optSemanal.CheckedChanged, optMensal.CheckedChanged, optAnual.CheckedChanged

        'Mostra somente as opções do padrão escolhido (como no Outlook)
        pnlDiario.Visible = optDiario.Checked
        pnlSemanal.Visible = optSemanal.Checked
        pnlMensal.Visible = optMensal.Checked
        pnlAnual.Visible = optAnual.Checked

    End Sub

    Private Sub optTermino_CheckedChanged(sender As Object, e As EventArgs) Handles optTerminaEm.CheckedChanged, optTerminaApos.CheckedChanged, optSemTermino.CheckedChanged

        dtpTermino.Enabled = optTerminaEm.Checked
        numOcorrencias.Enabled = optTerminaApos.Checked

    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click

        Try

            If Validacao() = False Then Exit Sub

            oRegra = MontarRegra()
            eResultado = ResultadoRecorrencia.Salvar
            Me.Close()

        Catch ex As Exception
            Call TratamentoErro(ex, Me.Text)
        End Try

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

        eResultado = ResultadoRecorrencia.Cancelado
        Me.Close()

    End Sub

    Private Sub btnRemover_Click(sender As Object, e As EventArgs) Handles btnRemover.Click

        Try

            If MsgBox("Deseja remover a recorrência deste chamado?" & vbCrLf & vbCrLf & _
                      "Nenhum chamado novo será gerado. Os chamados já gerados continuam como estão.", _
                      MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gAplicacao) = MsgBoxResult.No Then Exit Sub

            eResultado = ResultadoRecorrencia.Remover
            Me.Close()

        Catch ex As Exception
            Call TratamentoErro(ex, Me.Text)
        End Try

    End Sub

#End Region

#Region "::: FUNCTION / SUB :::"

    Private Sub ConfigurarForm()

        Try

            oDiasSemana = New UICheckBox() {chkDomingo, chkSegunda, chkTerca, chkQuarta, chkQuinta, chkSexta, chkSabado}

            Dim sOrdinais As String = "1=primeiro(a);2=segundo(a);3=terceiro(a);4=quarto(a);5=último(a)"
            Dim sDias As String = "1=domingo;2=segunda-feira;3=terça-feira;4=quarta-feira;5=quinta-feira;6=sexta-feira;7=sábado"
            Dim sMeses As String = "1=janeiro;2=fevereiro;3=março;4=abril;5=maio;6=junho;7=julho;8=agosto;9=setembro;10=outubro;11=novembro;12=dezembro"

            Call LoadComboLista(cboMensalOrdinal, sOrdinais)
            Call LoadComboLista(cboAnualOrdinal, sOrdinais)
            Call LoadComboLista(cboMensalDiaSemana, sDias)
            Call LoadComboLista(cboAnualDiaSemana, sDias)
            Call LoadComboLista(cboAnualMesData, sMeses)
            Call LoadComboLista(cboAnualMesOrdinal, sMeses)

            numMensalDia.Maximum = 31
            numAnualDia.Maximum = 31

            'Chamado sem recorrência: sugere semanal no dia da semana de hoje, começando hoje e sem término
            Dim bNova As Boolean = IsNothing(oRegra)
            Dim oAtual As clsFrmRecorrencia.RegraRecorrencia = IIf(bNova, RegraPadrao(), oRegra)

            Call MostrarRegra(oAtual)

            btnRemover.Enabled = (bNova = False AndAlso oAtual.lCodigo > 0 AndAlso oAtual.bAtivo)

            If bNova OrElse oAtual.lCodigo = 0 Then
                lblResumo.Text = "O chamado atual é a 1ª ocorrência. Os próximos serão gerados no 1º login do dia previsto."
            ElseIf oAtual.bAtivo = False Then
                lblResumo.Text = "Recorrência encerrada (" & oAtual.iChamadosGerados & " chamado(s) gerado(s)). OK reativa a recorrência."
            Else
                lblResumo.Text = "Próxima ocorrência: " & IIf(IsNothing(oAtual.oProximaOcorrencia), "-", Format(oAtual.oProximaOcorrencia, "dd/MM/yyyy")).ToString & _
                                 "  |  Chamados gerados: " & oAtual.iChamadosGerados & _
                                 IIf(oAtual.sUltimoErro = "", "", vbCrLf & "Última falha: " & oAtual.sUltimoErro).ToString
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function RegraPadrao() As clsFrmRecorrencia.RegraRecorrencia

        Dim oPadrao As New clsFrmRecorrencia.RegraRecorrencia
        Dim iDia As Integer = Weekday(Date.Today, FirstDayOfWeek.Sunday)

        oPadrao.iTipo = 2
        oPadrao.iDiasSemana = 1 << (iDia - 1)
        oPadrao.iDiaMes = Date.Today.Day
        oPadrao.iDiaSemana = iDia
        oPadrao.iOrdinal = Math.Min((Date.Today.Day - 1) \ 7 + 1, 4)
        oPadrao.iMes = Date.Today.Month
        oPadrao.dDataInicio = Date.Today
        oPadrao.iTipoTermino = 0

        Return oPadrao

    End Function

    Private Sub MostrarRegra(ByVal oAtual As clsFrmRecorrencia.RegraRecorrencia)

        With oAtual

            optDiario.Checked = (.iTipo = 1)
            optSemanal.Checked = (.iTipo = 2)
            optMensal.Checked = (.iTipo = 3)
            optAnual.Checked = (.iTipo = 4)
            Call optPadrao_CheckedChanged(Nothing, Nothing)

            'Os quatro padrões são preenchidos: ao trocar de padrão, os valores já fazem sentido
            numDiarioIntervalo.Value = .iIntervalo
            optDiarioUteis.Checked = .bDiasUteis
            optDiarioIntervalo.Checked = Not .bDiasUteis

            numSemanalIntervalo.Value = .iIntervalo
            For i As Integer = 0 To 6
                oDiasSemana(i).Checked = (.iDiasSemana And (1 << i)) <> 0
            Next

            numMensalIntervalo.Value = .iIntervalo
            optMensalDia.Checked = (.iModoMes = 1)
            optMensalOrdinal.Checked = (.iModoMes = 2)
            numMensalDia.Value = Math.Max(Math.Min(.iDiaMes, 31), 1)
            cboMensalOrdinal.SelectedValue = .iOrdinal.ToString
            cboMensalDiaSemana.SelectedValue = .iDiaSemana.ToString

            numAnualIntervalo.Value = .iIntervalo
            optAnualData.Checked = (.iModoMes = 1)
            optAnualOrdinal.Checked = (.iModoMes = 2)
            numAnualDia.Value = Math.Max(Math.Min(.iDiaMes, 31), 1)
            cboAnualMesData.SelectedValue = .iMes.ToString
            cboAnualOrdinal.SelectedValue = .iOrdinal.ToString
            cboAnualDiaSemana.SelectedValue = .iDiaSemana.ToString
            cboAnualMesOrdinal.SelectedValue = .iMes.ToString

            dtpInicio.Value = .dDataInicio
            dtpTermino.Value = .dDataTermino
            numOcorrencias.Value = Math.Max(.iOcorrencias, 1)
            optSemTermino.Checked = (.iTipoTermino = 0)
            optTerminaApos.Checked = (.iTipoTermino = 1)
            optTerminaEm.Checked = (.iTipoTermino = 2)
            Call optTermino_CheckedChanged(Nothing, Nothing)

        End With

    End Sub

    Private Function MontarRegra() As clsFrmRecorrencia.RegraRecorrencia

        'Mantém código e situação da regra aberta; o padrão vem da tela
        Dim oNova As New clsFrmRecorrencia.RegraRecorrencia

        If IsNothing(oRegra) = False Then
            oNova.lCodigo = oRegra.lCodigo
            oNova.lChamadoModelo = oRegra.lChamadoModelo
        End If

        If optDiario.Checked Then
            oNova.iTipo = 1
            oNova.bDiasUteis = optDiarioUteis.Checked
            oNova.iIntervalo = numDiarioIntervalo.Value
        ElseIf optSemanal.Checked Then
            oNova.iTipo = 2
            oNova.iIntervalo = numSemanalIntervalo.Value
            For i As Integer = 0 To 6
                If oDiasSemana(i).Checked Then oNova.iDiasSemana = oNova.iDiasSemana Or (1 << i)
            Next
        ElseIf optMensal.Checked Then
            oNova.iTipo = 3
            oNova.iIntervalo = numMensalIntervalo.Value
            oNova.iModoMes = IIf(optMensalOrdinal.Checked, 2, 1)
            oNova.iDiaMes = numMensalDia.Value
            oNova.iOrdinal = CInt(cboMensalOrdinal.SelectedValue)
            oNova.iDiaSemana = CInt(cboMensalDiaSemana.SelectedValue)
        Else
            oNova.iTipo = 4
            oNova.iIntervalo = numAnualIntervalo.Value
            oNova.iModoMes = IIf(optAnualOrdinal.Checked, 2, 1)
            oNova.iDiaMes = numAnualDia.Value
            oNova.iMes = CInt(IIf(optAnualOrdinal.Checked, cboAnualMesOrdinal.SelectedValue, cboAnualMesData.SelectedValue))
            oNova.iOrdinal = CInt(cboAnualOrdinal.SelectedValue)
            oNova.iDiaSemana = CInt(cboAnualDiaSemana.SelectedValue)
        End If

        oNova.dDataInicio = dtpInicio.Value.Date
        oNova.iTipoTermino = IIf(optTerminaApos.Checked, 1, IIf(optTerminaEm.Checked, 2, 0))
        oNova.iOcorrencias = numOcorrencias.Value
        oNova.dDataTermino = dtpTermino.Value.Date

        Return oNova

    End Function

    Private Function Validacao() As Boolean

        'O banco valida novamente (sp_validar_administracao_chamado_recorrencia)
        Dim sMensagem As String = ""

        If optSemanal.Checked AndAlso Array.TrueForAll(oDiasSemana, Function(c) c.Checked = False) Then
            sMensagem = "Marque ao menos um dia da semana."
        ElseIf optMensal.Checked AndAlso optMensalOrdinal.Checked AndAlso (cboMensalOrdinal.SelectedIndex = -1 OrElse cboMensalDiaSemana.SelectedIndex = -1) Then
            sMensagem = "Informe a ordem e o dia da semana."
        ElseIf optAnual.Checked AndAlso optAnualData.Checked AndAlso cboAnualMesData.SelectedIndex = -1 Then
            sMensagem = "Informe o mês."
        ElseIf optAnual.Checked AndAlso optAnualOrdinal.Checked AndAlso (cboAnualOrdinal.SelectedIndex = -1 OrElse cboAnualDiaSemana.SelectedIndex = -1 OrElse cboAnualMesOrdinal.SelectedIndex = -1) Then
            sMensagem = "Informe a ordem, o dia da semana e o mês."
        ElseIf (IsNothing(oRegra) OrElse oRegra.lCodigo = 0) AndAlso dtpInicio.Value.Date < Date.Today Then
            sMensagem = "A data de início da recorrência não pode ser anterior a hoje."
        ElseIf optTerminaEm.Checked AndAlso dtpTermino.Value.Date < dtpInicio.Value.Date Then
            sMensagem = "A data de término deve ser maior ou igual à data de início."
        End If

        If sMensagem <> "" Then
            MsgBox(sMensagem, MsgBoxStyle.Exclamation, gAplicacao)
            Return False
        End If

        Return True

    End Function

#End Region

End Class
