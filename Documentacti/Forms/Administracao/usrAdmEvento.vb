Imports Janus.Windows.GridEX
Imports Janus.Windows.EditControls
Imports Janus.Windows.Schedule
Imports MySqlConnector

Public Class usrAdmEvento

#Region "::: VARIAVEIS :::"

    Private oClsUsrAdmEvento As New clsUsrAdmEvento

    'Eventos do último filtro (alimenta o calendário e a lista)
    Private oTabela As DataTable

    'Mês de referência do calendário (navegação Mês anterior / Mês atual / Próximo mês)
    Private dReferencia As Date = Today

    'Maior quantidade de eventos num mesmo dia do mês exibido (define a altura da linha)
    Private iMaximoEventosDia As Integer = 0

    Private bCarregando As Boolean = False

    'Altura (pixels) do cabeçalho dos dias da semana, do número do dia, de cada evento e a sobra no fim do dia
    Private Const gcAlturaCabecalhoSemana As Integer = 34
    Private Const gcAlturaCabecalhoDia As Integer = 38
    Private Const gcAlturaEvento As Integer = 32
    Private Const gcAlturaSobraDia As Integer = 16

#End Region

#Region "::: CONTROLES :::"

#Region "::: GERAL :::"

    Private Sub usrAdmEvento_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Call ConfigurarUserControl()

    End Sub

    Private Sub usrAdmEvento_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        Try

            Select Case e.KeyCode

                Case Keys.Enter
                    If InStr(sender.name, "Filtro", CompareMethod.Text) > 0 Then
                        Call LoadDados()
                    ElseIf TypeOf sender Is GridEX = False AndAlso sender IsNot txtDescricao Then
                        Control_Enter(sender)
                    End If

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnFiltrar_Click(sender As Object, e As EventArgs) Handles btnFiltrar.Click

        Try

            'Período escolhido à mão: o calendário vai para o mês do início do período
            dReferencia = dtpInicioFiltro.Value.Date
            Call PosicionarCalendario()
            Call LoadDados()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click

        Try

            Call ExportExcel(grdListagem)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnNovo1_Click(sender As Object, e As EventArgs) Handles btnNovo1.Click

        Try

            'Novo evento hoje (se estiver no período) ou no dia 1 do mês exibido, às 09:00
            Dim dData As Date = New Date(dReferencia.Year, dReferencia.Month, 1)
            If Today >= dtpInicioFiltro.Value.Date AndAlso Today <= dtpTerminoFiltro.Value.Date Then dData = Today

            Call Novo(dData.AddHours(9), dData.AddHours(10))
            Call MostrarDados()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub cboTimeFiltro_CheckedValuesChanged(sender As Object, e As EventArgs) Handles cboTimeFiltro.CheckedValuesChanged

        Try

            If bCarregando = True Then Exit Sub

            'Empresas do filtro: somente as dos times marcados
            Call CarregarComboEmpresas(cboEmpresaFiltro, GetCheckedValues(cboTimeFiltro), GetCheckedValues(cboEmpresaFiltro))

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: CALENDÁRIO :::"

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click

        Try

            dReferencia = New Date(dReferencia.Year, dReferencia.Month, 1).AddMonths(-1)
            Call ExibirPeriodo()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnProximo_Click(sender As Object, e As EventArgs) Handles btnProximo.Click

        Try

            dReferencia = New Date(dReferencia.Year, dReferencia.Month, 1).AddMonths(1)
            Call ExibirPeriodo()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnHoje_Click(sender As Object, e As EventArgs) Handles btnHoje.Click

        Try

            dReferencia = Today
            Call ExibirPeriodo()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub pnlCalendario_Resize(sender As Object, e As EventArgs) Handles pnlCalendario.Resize

        Try

            'Tela redimensionada: recalcula a altura das semanas
            If bCarregando = False AndAlso IsNothing(oTabela) = False Then Call PosicionarCalendario()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub schMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles schMain.MouseWheel

        Try

            'Roda do mouse sobre o calendário rola a página (o mês inteiro já está na tela)
            pnlCalendario.AutoScrollPosition = New Point(0, Math.Max(0, -pnlCalendario.AutoScrollPosition.Y - e.Delta))

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub schMain_ViewChanged(sender As Object, e As EventArgs) Handles schMain.ViewChanged

        Try

            'Somente visão de mês: qualquer troca feita pelo próprio calendário volta para o mês
            If bCarregando = False AndAlso schMain.View <> ScheduleView.MonthView Then Call PosicionarCalendario()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub schMain_HiddenAppointmentClick(sender As Object, e As HiddenAppointmentsEventArgs) Handles schMain.HiddenAppointmentClick

        Try

            'Mesmo com a linha ampliada não coube tudo: mostra o dia na Lista por Data
            Dim dData As Date = e.Date.Date
            Me.BeginInvoke(New MethodInvoker(Sub() MostrarDiaNaLista(dData)))

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub schMain_AddingAppointment(sender As Object, e As AddAppointmentEventArgs) Handles schMain.AddingAppointment

        Try

            'A carga feita pelo código (AddAction.Code) não pode ser cancelada.
            'Digitar / duplo clique num dia vazio: abre o cadastro naquele dia.
            If e.AddAction = AddAction.Typing Or e.AddAction = AddAction.DoubleClick Then

                e.Cancel = True

                If e.AddAction = AddAction.DoubleClick AndAlso IsNothing(e.Appointment) = False Then
                    Dim dInicio As Date = e.Appointment.StartTime.Date.AddHours(9)
                    Me.BeginInvoke(New MethodInvoker(Sub() NovoPeloCalendario(dInicio, dInicio.AddHours(1))))
                End If

            End If

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub schMain_AppointmentDoubleClick(sender As Object, e As AppointmentEventArgs) Handles schMain.AppointmentDoubleClick

        Try

            If IsNothing(e.Appointment) OrElse IsNothing(e.Appointment.Tag) Then Exit Sub

            Dim lCodigo As Long = CLng(e.Appointment.Tag)
            Me.BeginInvoke(New MethodInvoker(Sub() EditarPeloCalendario(lCodigo)))

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub schMain_DroppingAppointment(sender As Object, e As DroppingAppointmentEventArgs) Handles schMain.DroppingAppointment

        Try

            Dim lCodigo As Long = CLng(e.Appointment.Tag)
            Dim oRow As DataRow = LinhaEvento(lCodigo)
            If IsNothing(oRow) Then Exit Sub

            'Visão de mês: muda o dia mantendo o horário e a duração do evento
            Dim dInicioAtual As Date = CDate(oRow.Item("data_inicio"))
            Dim dFimAtual As Date = CDate(oRow.Item("data_fim"))
            Dim iDias As Integer = (e.ProposedStartTime.Date - dInicioAtual.Date).Days

            If iDias = 0 Then Exit Sub

            Dim dInicio As Date = dInicioAtual.AddDays(iDias)
            Dim dFim As Date = dFimAtual.AddDays(iDias)

            Cursor.Current = Cursors.WaitCursor

            oClsUsrAdmEvento.UpdateData(lCodigo, dInicio, dFim)

            frmMain.Informacao("Evento '" & oRow.Item("titulo").ToString & "' movido para " & Format(dInicio, "dd/MM/yyyy") & ".", Color.Blue)

            'Recarrega depois que o calendário terminar o movimento (lista e horários atualizados)
            Me.BeginInvoke(New MethodInvoker(AddressOf LoadDados))

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            'Erro ao gravar: desfaz o movimento na tela
            e.Cancel = True
            Call TratarErro(ex)
        End Try

    End Sub

    'NÃO cancelar RemovingAppointment: ele dispara no Appointments.Clear() e o Clear entra em laço infinito
    '(exclusão pelo teclado fica bloqueada por AllowDelete = False).

#End Region

#Region "::: LISTA :::"

    Private Sub grdListagem_RowDoubleClick(ByVal sender As Object, ByVal e As RowActionEventArgs) Handles grdListagem.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            Dim lCodigo As Long = CLng(e.Row.Cells("codigo").Value)

            If IsNothing(grdListagem.CurrentColumn) = False AndAlso grdListagem.CurrentColumn.Key = "excluir" Then
                Call Excluir(lCodigo)
            Else
                Call Editar(lCodigo)
            End If

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: DADOS :::"

    Private Sub chkDiaInteiro_CheckedChanged(sender As Object, e As EventArgs) Handles chkDiaInteiro.CheckedChanged

        Try

            'Dia inteiro: somente a data
            Dim sFormato As String = IIf(chkDiaInteiro.Checked, "dd/MM/yyyy", "dd/MM/yyyy HH:mm")
            dtpInicio.CustomFormat = sFormato
            dtpFim.CustomFormat = sFormato

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub dtpInicio_ValueChanged(sender As Object, e As EventArgs) Handles dtpInicio.ValueChanged

        Try

            'Início passou do término: o término acompanha mantendo 1 hora de duração
            If bCarregando = False AndAlso dtpFim.Value < dtpInicio.Value Then dtpFim.Value = dtpInicio.Value.AddHours(1)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub cboTimes_CheckedValuesChanged(sender As Object, e As EventArgs) Handles cboTimes.CheckedValuesChanged

        Try

            If bCarregando = True Then Exit Sub

            'Empresas do evento: somente as dos times marcados
            Call CarregarComboEmpresas(cboEmpresas, GetCheckedValues(cboTimes), GetCheckedValues(cboEmpresas))

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        Try

            frmMain.errInfo.Clear()

            If Validacao() = True Then Call Salvar()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click

        Try

            Dim dData As Date = dtpInicio.Value.Date
            Call Novo(dData.AddHours(9), dData.AddHours(10))

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click

        Try

            If IsNumeric(txtCodigo.Text) = False Then Exit Sub

            Call Excluir(CLng(txtCodigo.Text))

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click

        Try

            frmMain.errInfo.Clear()

            Call MostrarLista()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#End Region

#Region "::: FUNCTION / SUB :::"

#Region "::: GERAL :::"

    Private Sub ConfigurarUserControl()

        Try

            Cursor.Current = Cursors.WaitCursor

            bCarregando = True

            Call AddGotFocus(Me, New DelegateGotFocus(AddressOf Control_GotFocus))
            Call AddKeyDown(Me, New DelegateKeyDown(AddressOf usrAdmEvento_KeyDown))
            Call AddLostFocus(Me, New DelegateLostFocus(AddressOf Control_LostFocus))
            Call AddComboAutoComplete(New UIComboBox() {cboTipoFiltro, cboTipo})

            'Combos - Filtro (nenhum time / participante marcado = todos)
            Call LoadCombo(cboTipoFiltro, "sp_select_combo_administracao_evento_tipo")
            Call LoadCombo(cboTimeFiltro, "sp_select_combo_tipo_time_empresa")
            Call LoadCombo(cboParticipanteFiltro, "sp_select_combo_administracao_chamado_usuario")
            Call CarregarComboEmpresas(cboEmpresaFiltro, "", "")

            'Combos - Dados
            Call LoadCombo(cboTipo, "sp_select_combo_administracao_evento_tipo")
            Call LoadCombo(cboTimes, "sp_select_combo_tipo_time_empresa")
            Call LoadCombo(cboParticipantes, "sp_select_combo_administracao_chamado_usuario")
            Call CarregarComboEmpresas(cboEmpresas, "", "")

            'Calendário somente em visão de mês, domingo a sábado; arrastar muda o dia; editar/incluir pelo cadastro
            schMain.View = ScheduleView.MonthView
            schMain.FirstDayOfWeek = ScheduleDayOfWeek.Sunday
            schMain.ViewNavigationMode = Janus.Windows.Schedule.TriState.False
            schMain.HiddenAppointmentMode = HiddenAppointmentMode.Manual
            schMain.Font = New Font("Arial", 7.5!)
            schMain.AllowAppointmentDrag = AllowAppointmentDrag.Move
            schMain.AllowEdit = False
            schMain.AllowDelete = False
            schMain.AddNewMode = AddNewMode.Manual
            schMain.MultiSelect = False
            schMain.ShowToolTips = True
            'Sem rolagem própria: o mês inteiro fica no calendário e quem rola é o painel em volta
            schMain.VerticalScrollBar = VerticalScrollBar.Never

            Call ConfiguraGrid(grdListagem)

            tabMain.TabPages.Remove(pagDados)

            bCarregando = False

            dReferencia = Today
            Call ExibirPeriodo()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            bCarregando = False
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub TratarErro(ByVal ex As Exception)

        'Erro de regra de negócio (SIGNAL nas procs) x erro de sistema
        If TypeOf ex Is MySqlException AndAlso CType(ex, MySqlException).Number = 1644 Then
            Cursor.Current = Cursors.Default
            frmMain.Informacao(ex.Message, gColorInformacaoErro)
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, gAplicacao)
        Else
            Call TratamentoErro(ex, "Calendário de Eventos")
        End If

    End Sub

    Private Sub MostrarDados()

        tabMain.TabPages.Remove(pagLista)
        If tabMain.TabPages.Contains(pagDados) = False Then tabMain.TabPages.Insert(0, pagDados)
        tabMain.SelectedTab = pagDados

        txtTitulo.Focus()

    End Sub

    Private Sub MostrarLista()

        tabMain.TabPages.Remove(pagDados)
        If tabMain.TabPages.Contains(pagLista) = False Then tabMain.TabPages.Insert(0, pagLista)
        tabMain.SelectedTab = pagLista

    End Sub

    Private Sub CarregarComboEmpresas(ByVal oCombo As Janus.Windows.GridEX.EditControls.CheckedComboBox, ByVal sTimes As String, ByVal sMarcadas As String)

        'Empresas dos times informados (vazio = todas), mantendo as marcadas que continuam na lista.
        'Evento já gravado com empresa de outro time: carrega todas para não perder a empresa ao salvar.
        Try

            Dim oTabela As DataTable = oClsUsrAdmEvento.LoadComboEmpresa(sTimes)

            If sTimes <> "" AndAlso sMarcadas <> "" Then
                For Each sCodigo As String In sMarcadas.Split(","c)
                    If sCodigo.Trim <> "" AndAlso oTabela.Select("codigo = " & CLng(sCodigo)).Length = 0 AndAlso oCombo Is cboEmpresas Then
                        oTabela = oClsUsrAdmEvento.LoadComboEmpresa("")
                        Exit For
                    End If
                Next
            End If

            Call LoadCombo(oCombo, oTabela)
            Call SetCheckedValues(oCombo, sMarcadas)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function LinhaEvento(ByVal lCodigo As Long) As DataRow

        If IsNothing(oTabela) Then Return Nothing

        Dim oRows() As DataRow = oTabela.Select("codigo = " & lCodigo)

        If oRows.Length = 0 Then Return Nothing

        Return oRows(0)

    End Function

    Public Sub AbrirEvento(ByVal lCodigo As Long, ByVal dData As Date, ByVal bAbrirCadastro As Boolean)

        'Clique na notificação: vai para o mês do evento e abre o cadastro (evento excluído: só o mês)
        Try

            Call MostrarLista()

            dReferencia = dData.Date
            Call ExibirPeriodo()

            If bAbrirCadastro = True Then Call Editar(lCodigo)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: CALENDÁRIO :::"

    Private Function PrimeiroDiaMes() As Date

        Return New Date(dReferencia.Year, dReferencia.Month, 1)

    End Function

    Private Function SemanasMes() As Integer

        'Semanas (domingo a sábado) que o mês ocupa: 4 a 6
        Dim dPrimeiro As Date = PrimeiroDiaMes()

        Return CInt(Math.Ceiling((CInt(dPrimeiro.DayOfWeek) + Date.DaysInMonth(dPrimeiro.Year, dPrimeiro.Month)) / 7))

    End Function

    Private Function InicioCalendario() As Date

        'Domingo da semana do dia 1
        Dim dPrimeiro As Date = PrimeiroDiaMes()

        Return dPrimeiro.AddDays(-CInt(dPrimeiro.DayOfWeek))

    End Function

    Private Sub ExibirPeriodo()

        'Calendário no mês de referência e filtro de período igual ao mês exibido (semanas completas)
        Try

            bCarregando = True
            dtpInicioFiltro.Value = InicioCalendario()
            dtpTerminoFiltro.Value = InicioCalendario().AddDays(7 * SemanasMes() - 1)
            bCarregando = False

            Call PosicionarCalendario()
            Call LoadDados()

        Catch ex As Exception
            bCarregando = False
            Throw ex
        End Try

    End Sub

    Private Sub PosicionarCalendario()

        Dim bCarregandoAnterior As Boolean = bCarregando

        Try

            bCarregando = True

            schMain.View = ScheduleView.MonthView

            'Mês inteiro sempre visível; cada semana alta o suficiente para o dia com mais eventos.
            'Sem eventos demais, as semanas dividem a altura da tela; com muitos, o painel ganha rolagem.
            Dim iSemanas As Integer = SemanasMes()
            Dim iAlturaNecessaria As Integer = gcAlturaCabecalhoDia + Math.Max(iMaximoEventosDia, 1) * gcAlturaEvento + gcAlturaSobraDia
            Dim iAlturaMinima As Integer = (pnlCalendario.Height - gcAlturaCabecalhoSemana) \ iSemanas
            Dim iAlturaSemana As Integer = Math.Max(iAlturaNecessaria, iAlturaMinima)
            Dim iAltura As Integer = gcAlturaCabecalhoSemana + iSemanas * iAlturaSemana
            Dim bRolagem As Boolean = (iAltura > pnlCalendario.Height)

            pnlCalendario.AutoScrollPosition = New Point(0, 0)
            schMain.Location = New Point(0, 0)
            schMain.Size = New Size(pnlCalendario.Width - IIf(bRolagem, SystemInformation.VerticalScrollBarWidth, 0), _
                                    IIf(bRolagem, iAltura, pnlCalendario.Height))

            'Período fixo no mês: o calendário não sai das semanas carregadas
            schMain.ResetMinDate()
            schMain.ResetMaxDate()
            schMain.Weeks = iSemanas
            schMain.Date = InicioCalendario()
            Try
                schMain.MinDate = InicioCalendario()
                schMain.MaxDate = InicioCalendario().AddDays(7 * iSemanas - 1)
            Catch
                'Limite é só proteção contra a rolagem interna: sem ele o calendário continua funcionando
            End Try

            lblPeriodo.Text = StrConv(Format(PrimeiroDiaMes(), "MMMM/yyyy"), VbStrConv.ProperCase) & _
                              IIf(bRolagem, "   (role a tela para ver todas as semanas)", "")

        Catch ex As Exception
            Throw ex
        Finally
            bCarregando = bCarregandoAnterior
        End Try

    End Sub

    Private Sub LoadCalendario()

        Try

            schMain.Appointments.Clear()

            Dim oEventosDia As New Dictionary(Of Date, Integer)

            For Each oRow As DataRow In oTabela.Rows

                Dim dInicio As Date = CDate(oRow.Item("data_inicio"))
                Dim dFim As Date = CDate(oRow.Item("data_fim"))
                Dim oAppointment As ScheduleAppointment

                If CInt(oRow.Item("dia_inteiro")) = 1 Then
                    oAppointment = New ScheduleAppointment(dInicio.Date, dFim.Date, oRow.Item("titulo").ToString)
                    oAppointment.AllDayEvent = True
                Else
                    oAppointment = New ScheduleAppointment(dInicio, dFim, oRow.Item("titulo").ToString)
                End If

                oAppointment.Tag = oRow.Item("codigo")

                'Dica do evento: tipo, horário, times, participantes, empresa, local e chamado
                oAppointment.Description = oRow.Item("tipo_evento").ToString & " - " & oRow.Item("horario").ToString & _
                                           IIf(oRow.Item("times").ToString = "", "", vbCrLf & "Times: " & oRow.Item("times").ToString) & _
                                           IIf(oRow.Item("participantes").ToString = "", "", vbCrLf & "Participantes: " & oRow.Item("participantes").ToString) & _
                                           IIf(oRow.Item("empresas").ToString = "", "", vbCrLf & "Empresas: " & oRow.Item("empresas").ToString) & _
                                           IIf(oRow.Item("local").ToString = "", "", vbCrLf & "Local: " & oRow.Item("local").ToString) & _
                                           IIf(IsDBNull(oRow.Item("codigo_atividade")), "", vbCrLf & "Chamado: " & oRow.Item("codigo_atividade").ToString)

                'Cor do tipo de evento
                oAppointment.FormatStyle.BackColor = ColorTranslator.FromHtml(oRow.Item("cor").ToString.Trim)
                oAppointment.FormatStyle.ForeColor = Color.Black

                schMain.Appointments.Add(oAppointment)

                'Conta o evento em cada dia que ele ocupa
                Dim dDia As Date = dInicio.Date
                Do While dDia <= dFim.Date
                    If oEventosDia.ContainsKey(dDia) Then oEventosDia(dDia) += 1 Else oEventosDia(dDia) = 1
                    dDia = dDia.AddDays(1)
                Loop

            Next

            iMaximoEventosDia = 0
            For Each iQuantidade As Integer In oEventosDia.Values
                iMaximoEventosDia = Math.Max(iMaximoEventosDia, iQuantidade)
            Next

            'Ajusta a altura da linha do dia à nova quantidade de eventos
            Call PosicionarCalendario()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub MostrarDiaNaLista(ByVal dData As Date)

        'Dia com mais eventos do que cabem: vai para a Lista por Data no grupo daquele dia
        Try

            tabVisao.SelectedTab = pagGrid

            For Each oRow As GridEXRow In grdListagem.GetRows
                If oRow.RowType = RowType.GroupHeader AndAlso IsDate(oRow.GroupValue) AndAlso CDate(oRow.GroupValue).Date = dData Then
                    grdListagem.MoveTo(oRow)
                    Exit For
                End If
            Next

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: LISTA :::"

    Private Sub LoadDados()

        Try

            If bCarregando = True Then Exit Sub

            Cursor.Current = Cursors.WaitCursor

            oTabela = oClsUsrAdmEvento.LoadGrid(dtpInicioFiltro.Value.Date, _
                                                dtpTerminoFiltro.Value.Date, _
                                                IIf(cboTipoFiltro.SelectedIndex = -1, -1, cboTipoFiltro.SelectedValue), _
                                                GetCheckedValues(cboParticipanteFiltro), _
                                                GetCheckedValues(cboTimeFiltro), _
                                                GetCheckedValues(cboEmpresaFiltro), _
                                                txtTituloFiltro.Text.Trim)

            'Lista por data
            Call ConfigurarDataMemberGrid(grdListagem)
            grdListagem.DataSource = oTabela

            'Calendário
            Call LoadCalendario()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: DADOS :::"

    Private Sub NovoPeloCalendario(ByVal dInicio As Date, ByVal dFim As Date)

        Try

            Call Novo(dInicio, dFim)
            Call MostrarDados()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub EditarPeloCalendario(ByVal lCodigo As Long)

        Try

            Call Editar(lCodigo)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub Novo(ByVal dInicio As Date, ByVal dFim As Date)

        Try

            frmMain.errInfo.Clear()

            bCarregando = True

            txtCodigo.Text = ""
            cboTipo.SelectedIndex = -1
            txtTitulo.Text = ""
            chkDiaInteiro.Checked = False
            dtpInicio.Value = dInicio
            dtpFim.Value = dFim
            txtLocal.Text = ""
            txtChamado.Text = ""
            txtDescricao.Text = ""

            'Sugere o usuário autenticado como participante e o time dele
            Call SetCheckedValues(cboParticipantes, goUsuario.lUsuario.ToString)
            Call SetCheckedValues(cboTimes, IIf(goUsuario.iTime > 0, goUsuario.iTime.ToString, ""))
            Call CarregarComboEmpresas(cboEmpresas, GetCheckedValues(cboTimes), "")

            btnExcluir.Enabled = False

            bCarregando = False

        Catch ex As Exception
            bCarregando = False
            Throw ex
        End Try

    End Sub

    Private Sub Editar(ByVal lCodigo As Long)

        Try

            Dim oRow As DataRow = LinhaEvento(lCodigo)

            If IsNothing(oRow) Then
                frmMain.Informacao("Evento " & lCodigo & " não encontrado no período filtrado.", gColorInformacaoErro)
                Exit Sub
            End If

            Call Novo(CDate(oRow.Item("data_inicio")), CDate(oRow.Item("data_fim")))

            bCarregando = True

            txtCodigo.Text = oRow.Item("codigo").ToString
            cboTipo.SelectedValue = oRow.Item("codigo_tipo_evento")
            txtTitulo.Text = oRow.Item("titulo").ToString
            chkDiaInteiro.Checked = (CInt(oRow.Item("dia_inteiro")) = 1)
            txtLocal.Text = oRow.Item("local").ToString
            txtChamado.Text = oRow.Item("codigo_atividade").ToString
            txtDescricao.Text = oRow.Item("descricao").ToString
            Call SetCheckedValues(cboParticipantes, oRow.Item("codigos_participante").ToString)
            Call SetCheckedValues(cboTimes, oRow.Item("codigos_time").ToString)
            Call CarregarComboEmpresas(cboEmpresas, GetCheckedValues(cboTimes), oRow.Item("codigos_empresa").ToString)

            btnExcluir.Enabled = True

            bCarregando = False

            Call MostrarDados()

        Catch ex As Exception
            bCarregando = False
            Throw ex
        End Try

    End Sub

    Private Function Validacao() As Boolean

        Try

            Validacao = False

            If ValidaCampo(cboTipo, lblTipo) = False Then Exit Function
            If ValidaCampo(txtTitulo, lblTitulo, TipoCampo.texto) = False Then Exit Function

            If DataFim() < DataInicio() Then
                frmMain.errInfo.SetError(lblFim, "O término do evento não pode ser anterior ao início.")
                dtpFim.Focus()
                Exit Function
            End If

            If txtChamado.Text.Trim <> "" AndAlso IsNumeric(txtChamado.Text.Trim) = False Then
                frmMain.errInfo.SetError(lblChamado, "Informe o número do chamado (somente números).")
                txtChamado.Focus()
                Exit Function
            End If

            Validacao = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function DataInicio() As Date

        Return IIf(chkDiaInteiro.Checked, dtpInicio.Value.Date, dtpInicio.Value)

    End Function

    Private Function DataFim() As Date

        Return IIf(chkDiaInteiro.Checked, dtpFim.Value.Date, dtpFim.Value)

    End Function

    Private Sub Salvar()

        Try

            Cursor.Current = Cursors.WaitCursor

            Dim oChamado As Object = DBNull.Value
            If txtChamado.Text.Trim <> "" Then oChamado = CInt(txtChamado.Text.Trim)

            If IsNumeric(txtCodigo.Text) Then

                oClsUsrAdmEvento.Update(CLng(txtCodigo.Text), _
                                        txtTitulo.Text.Trim, _
                                        cboTipo.SelectedValue, _
                                        DataInicio(), _
                                        DataFim(), _
                                        chkDiaInteiro.Checked, _
                                        txtLocal.Text.Trim, _
                                        GetCheckedValues(cboEmpresas), _
                                        oChamado, _
                                        txtDescricao.Text.Trim, _
                                        GetCheckedValues(cboParticipantes), _
                                        GetCheckedValues(cboTimes))

                frmMain.Informacao("Evento alterado com sucesso!", Color.Blue)

            Else

                txtCodigo.Text = oClsUsrAdmEvento.Insert(txtTitulo.Text.Trim, _
                                                         cboTipo.SelectedValue, _
                                                         DataInicio(), _
                                                         DataFim(), _
                                                         chkDiaInteiro.Checked, _
                                                         txtLocal.Text.Trim, _
                                                         GetCheckedValues(cboEmpresas), _
                                                         oChamado, _
                                                         txtDescricao.Text.Trim, _
                                                         GetCheckedValues(cboParticipantes), _
                                                         GetCheckedValues(cboTimes))

                btnExcluir.Enabled = True

                frmMain.Informacao("Evento " & txtCodigo.Text & " inserido com sucesso! Os participantes serão notificados.", Color.Blue)

            End If

            'Calendário e lista já com o evento gravado
            Call LoadDados()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Excluir(ByVal lCodigo As Long)

        Try

            Dim oRow As DataRow = LinhaEvento(lCodigo)
            Dim sTitulo As String = lCodigo.ToString
            If IsNothing(oRow) = False Then sTitulo = oRow.Item("titulo").ToString

            If MsgBox("Deseja excluir o evento '" & sTitulo & "'?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gAplicacao) = MsgBoxResult.No Then Exit Sub

            Cursor.Current = Cursors.WaitCursor

            oClsUsrAdmEvento.Delete(lCodigo)

            frmMain.Informacao("Evento excluído com sucesso!", Color.Blue)

            Call LoadDados()
            Call MostrarLista()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#End Region

End Class
