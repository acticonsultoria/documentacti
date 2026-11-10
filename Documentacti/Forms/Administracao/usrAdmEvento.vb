Imports Janus.Windows.GridEX
Imports Janus.Windows.EditControls
Imports Janus.Windows.Schedule
Imports MySqlConnector

Public Class usrAdmEvento

#Region "::: VARIAVEIS :::"

    Private oClsUsrAdmEvento As New clsUsrAdmEvento

    'Eventos do último filtro (alimenta o calendário e a lista)
    Private oTabela As DataTable

    'Data de referência do calendário (navegação Anterior / Hoje / Próximo)
    Private dReferencia As Date = Today

    Private bCarregando As Boolean = False

    'Visões do calendário (cboVisao)
    Private Const gcVisaoMes As String = "MES"
    Private Const gcVisaoSemana As String = "SEMANA"
    Private Const gcVisaoDia As String = "DIA"

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

            'Período escolhido à mão: o calendário vai para o início do período
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

            'Novo evento na data em exibição (hoje, se estiver no período), às 09:00
            Dim dData As Date = IIf(Today >= dtpInicioFiltro.Value.Date AndAlso Today <= dtpTerminoFiltro.Value.Date, Today, dReferencia)
            Call Novo(dData.AddHours(9), dData.AddHours(10))
            Call MostrarDados()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: CALENDÁRIO :::"

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click

        Try

            Call Navegar(-1)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnProximo_Click(sender As Object, e As EventArgs) Handles btnProximo.Click

        Try

            Call Navegar(1)

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

    Private Sub cboVisao_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboVisao.SelectedIndexChanged

        Try

            If bCarregando = True Then Exit Sub

            Call ExibirPeriodo()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub schMain_AddingAppointment(sender As Object, e As AddAppointmentEventArgs) Handles schMain.AddingAppointment

        Try

            'A carga feita pelo código (AddAction.Code) não pode ser cancelada.
            'Digitar / duplo clique num horário vazio: abre o cadastro com aquele horário.
            If e.AddAction = AddAction.Typing Or e.AddAction = AddAction.DoubleClick Then

                e.Cancel = True

                If e.AddAction = AddAction.DoubleClick AndAlso IsNothing(e.Appointment) = False Then
                    Dim dInicio As Date = e.Appointment.StartTime
                    Dim dFim As Date = e.Appointment.EndTime
                    'Visão de mês: o clique é no dia (sem horário)
                    If schMain.View = ScheduleView.MonthView OrElse e.Appointment.AllDayEvent Then
                        dInicio = dInicio.Date.AddHours(9)
                        dFim = dInicio.AddHours(1)
                    End If
                    Me.BeginInvoke(New MethodInvoker(Sub() NovoPeloCalendario(dInicio, dFim)))
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

            Dim dInicio As Date
            Dim dFim As Date

            If CInt(oRow.Item("dia_inteiro")) = 1 Then
                'Dia inteiro: mantém a quantidade de dias do evento
                Dim iDias As Integer = (CDate(oRow.Item("data_fim")).Date - CDate(oRow.Item("data_inicio")).Date).Days
                dInicio = e.ProposedStartTime.Date
                dFim = dInicio.AddDays(iDias).AddHours(23).AddMinutes(59).AddSeconds(59)
            Else
                dInicio = e.ProposedStartTime
                dFim = e.ProposedEndTime
            End If

            If dInicio = CDate(oRow.Item("data_inicio")) Then Exit Sub

            Cursor.Current = Cursors.WaitCursor

            oClsUsrAdmEvento.UpdateData(lCodigo, dInicio, dFim)

            frmMain.Informacao("Evento '" & oRow.Item("titulo").ToString & "' movido para " & _
                               Format(dInicio, IIf(CInt(oRow.Item("dia_inteiro")) = 1, "dd/MM/yyyy", "dd/MM/yyyy HH:mm")) & ".", Color.Blue)

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
            Call AddComboAutoComplete(New UIComboBox() {cboTipoFiltro, cboEmpresaFiltro, cboVisao, cboTipo, cboEmpresa})

            'Combos - Filtro
            Call LoadCombo(cboTipoFiltro, "sp_select_combo_administracao_evento_tipo")
            Call LoadCombo(cboParticipanteFiltro, "sp_select_combo_administracao_chamado_usuario")
            Call LoadCombo(cboEmpresaFiltro, "sp_select_combo_administracao_chamado_empresa")
            Call LoadComboLista(cboVisao, gcVisaoMes & "=Mês;" & gcVisaoSemana & "=Semana;" & gcVisaoDia & "=Dia")

            'Combos - Dados
            Call LoadCombo(cboTipo, "sp_select_combo_administracao_evento_tipo")
            Call LoadCombo(cboParticipantes, "sp_select_combo_administracao_chamado_usuario")
            Call LoadCombo(cboEmpresa, "sp_select_combo_administracao_chamado_empresa")

            'Calendário: domingo a sábado, arrastar para mudar a data; editar/incluir pelo cadastro
            schMain.FirstDayOfWeek = ScheduleDayOfWeek.Sunday
            schMain.AllowAppointmentDrag = AllowAppointmentDrag.Move
            schMain.AllowEdit = False
            schMain.AllowDelete = False
            schMain.AddNewMode = AddNewMode.Manual
            schMain.MultiSelect = False
            schMain.ShowToolTips = True
            schMain.WorkStartTime = New TimeSpan(8, 0, 0)
            schMain.WorkEndTime = New TimeSpan(18, 0, 0)
            schMain.FirstVisibleTime = New TimeSpan(7, 0, 0)

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

    Private Function ValorCombo(ByVal oCombo As UIComboBox) As Object

        'Combo sem seleção: nulo (gravação) / -1 (filtro) conforme o chamador
        If oCombo.SelectedIndex = -1 OrElse IsNothing(oCombo.SelectedValue) OrElse IsDBNull(oCombo.SelectedValue) Then Return DBNull.Value

        Return oCombo.SelectedValue

    End Function

    Private Function LinhaEvento(ByVal lCodigo As Long) As DataRow

        If IsNothing(oTabela) Then Return Nothing

        Dim oRows() As DataRow = oTabela.Select("codigo = " & lCodigo)

        If oRows.Length = 0 Then Return Nothing

        Return oRows(0)

    End Function

    Public Sub AbrirEvento(ByVal lCodigo As Long, ByVal dData As Date)

        'Clique na notificação: vai para o período do evento e abre o cadastro
        Try

            Call MostrarLista()

            dReferencia = dData.Date
            Call ExibirPeriodo()

            Call Editar(lCodigo)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: CALENDÁRIO :::"

    Private Function VisaoAtual() As String

        If cboVisao.SelectedIndex = -1 Then Return gcVisaoMes

        Return cboVisao.SelectedValue.ToString

    End Function

    Private Sub Navegar(ByVal iSentido As Integer)

        Try

            Select Case VisaoAtual()
                Case gcVisaoDia : dReferencia = dReferencia.AddDays(iSentido)
                Case gcVisaoSemana : dReferencia = dReferencia.AddDays(7 * iSentido)
                Case Else : dReferencia = New Date(dReferencia.Year, dReferencia.Month, 1).AddMonths(iSentido)
            End Select

            Call ExibirPeriodo()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub ExibirPeriodo()

        'Calendário na data de referência e filtro de período igual ao que está visível
        Try

            Dim dInicio As Date
            Dim dTermino As Date

            Select Case VisaoAtual()

                Case gcVisaoDia
                    dInicio = dReferencia.Date
                    dTermino = dInicio

                Case gcVisaoSemana
                    dInicio = dReferencia.Date.AddDays(-CInt(dReferencia.DayOfWeek))
                    dTermino = dInicio.AddDays(6)

                Case Else
                    'Mês: 6 semanas a partir do domingo da semana do dia 1 (dias do mês anterior/seguinte também aparecem)
                    Dim dPrimeiro As New Date(dReferencia.Year, dReferencia.Month, 1)
                    dInicio = dPrimeiro.AddDays(-CInt(dPrimeiro.DayOfWeek))
                    dTermino = dInicio.AddDays(41)

            End Select

            bCarregando = True
            dtpInicioFiltro.Value = dInicio
            dtpTerminoFiltro.Value = dTermino
            bCarregando = False

            Call PosicionarCalendario()
            Call LoadDados()

        Catch ex As Exception
            bCarregando = False
            Throw ex
        End Try

    End Sub

    Private Sub PosicionarCalendario()

        Try

            Select Case VisaoAtual()

                Case gcVisaoDia
                    schMain.View = ScheduleView.DayView
                    schMain.Date = dReferencia.Date
                    lblPeriodo.Text = StrConv(Format(dReferencia, "dddd, dd/MM/yyyy"), VbStrConv.ProperCase)

                Case gcVisaoSemana
                    Dim dDomingo As Date = dReferencia.Date.AddDays(-CInt(dReferencia.DayOfWeek))
                    schMain.View = ScheduleView.WeekView
                    schMain.Date = dDomingo
                    lblPeriodo.Text = Format(dDomingo, "dd/MM") & " a " & Format(dDomingo.AddDays(6), "dd/MM/yyyy")

                Case Else
                    Dim dPrimeiro As New Date(dReferencia.Year, dReferencia.Month, 1)
                    schMain.View = ScheduleView.MonthView
                    schMain.Weeks = 6
                    schMain.Date = dPrimeiro.AddDays(-CInt(dPrimeiro.DayOfWeek))
                    lblPeriodo.Text = StrConv(Format(dPrimeiro, "MMMM/yyyy"), VbStrConv.ProperCase)

            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub LoadCalendario()

        Try

            schMain.Appointments.Clear()

            For Each oRow As DataRow In oTabela.Rows

                Dim dInicio As Date = CDate(oRow.Item("data_inicio"))
                Dim dFim As Date = CDate(oRow.Item("data_fim"))
                Dim bDiaInteiro As Boolean = (CInt(oRow.Item("dia_inteiro")) = 1)
                Dim oAppointment As ScheduleAppointment

                If bDiaInteiro Then
                    oAppointment = New ScheduleAppointment(dInicio.Date, dFim.Date, oRow.Item("titulo").ToString)
                    oAppointment.AllDayEvent = True
                Else
                    oAppointment = New ScheduleAppointment(dInicio, dFim, oRow.Item("titulo").ToString)
                End If

                oAppointment.Tag = oRow.Item("codigo")

                'Dica do evento: tipo, horário, participantes, local e chamado
                oAppointment.Description = oRow.Item("tipo_evento").ToString & " - " & oRow.Item("horario").ToString & _
                                           IIf(oRow.Item("participantes").ToString = "", "", vbCrLf & "Participantes: " & oRow.Item("participantes").ToString) & _
                                           IIf(oRow.Item("empresa").ToString = "", "", vbCrLf & "Empresa: " & oRow.Item("empresa").ToString) & _
                                           IIf(oRow.Item("local").ToString = "", "", vbCrLf & "Local: " & oRow.Item("local").ToString) & _
                                           IIf(IsDBNull(oRow.Item("codigo_atividade")), "", vbCrLf & "Chamado: " & oRow.Item("codigo_atividade").ToString)

                'Cor do tipo de evento
                oAppointment.FormatStyle.BackColor = ColorTranslator.FromHtml(oRow.Item("cor").ToString.Trim)
                oAppointment.FormatStyle.ForeColor = Color.Black

                schMain.Appointments.Add(oAppointment)

            Next

        Catch ex As Exception
            Throw ex
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
                                                IIf(cboEmpresaFiltro.SelectedIndex = -1, -1, cboEmpresaFiltro.SelectedValue), _
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
            cboEmpresa.SelectedIndex = -1
            txtChamado.Text = ""
            txtDescricao.Text = ""

            'Sugere o usuário autenticado como participante
            Call SetCheckedValues(cboParticipantes, goUsuario.lUsuario.ToString)

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
            If IsDBNull(oRow.Item("codigo_empresa_cliente")) = False Then cboEmpresa.SelectedValue = oRow.Item("codigo_empresa_cliente")
            txtChamado.Text = oRow.Item("codigo_atividade").ToString
            txtDescricao.Text = oRow.Item("descricao").ToString
            Call SetCheckedValues(cboParticipantes, oRow.Item("codigos_participante").ToString)

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
                                        ValorCombo(cboEmpresa), _
                                        oChamado, _
                                        txtDescricao.Text.Trim, _
                                        GetCheckedValues(cboParticipantes))

                frmMain.Informacao("Evento alterado com sucesso!", Color.Blue)

            Else

                txtCodigo.Text = oClsUsrAdmEvento.Insert(txtTitulo.Text.Trim, _
                                                         cboTipo.SelectedValue, _
                                                         DataInicio(), _
                                                         DataFim(), _
                                                         chkDiaInteiro.Checked, _
                                                         txtLocal.Text.Trim, _
                                                         ValorCombo(cboEmpresa), _
                                                         oChamado, _
                                                         txtDescricao.Text.Trim, _
                                                         GetCheckedValues(cboParticipantes))

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
