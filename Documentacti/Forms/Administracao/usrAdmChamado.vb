Imports Janus.Windows.GridEX
Imports Janus.Windows.EditControls
Imports MySql.Data.MySqlClient
Imports DWC = Dundas.Charting.WinControl

Public Class usrAdmChamado

#Region "::: VARIAVEIS :::"

    Private oClsUsrAdmChamado As New clsUsrAdmChamado
    Private oFtp As New clsFtp

    'Apontamentos em andamento: tabela ligada à grid (a duração é recalculada pelo Timer)
    Private oTabelaAndamento As DataTable
    'Diferença entre o relógio do banco e o da estação (a duração usa a hora do banco)
    Private oDiferencaServidor As TimeSpan = TimeSpan.Zero
    Private iContadorSincronizacao As Integer = 0
    Private bCarregando As Boolean = False
    Private bAtualizandoStatus As Boolean = False
    Private iStatusAtual As Integer = 1

    'Relatório de horas: dados do último filtro (o gráfico é redesenhado sem nova consulta)
    Private oTabelaRelatorio As DataTable
    Private Const gcLimiteGrafico As Integer = 12

    Private Const gcIntervaloSincronizacao As Integer = 60 'segundos

#End Region

#Region "::: CONTROLES :::"

#Region "::: GERAL :::"

    Private Sub usrAdmChamado_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Call ConfigurarUserControl()

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Try

            Call ExportExcel(grdListagem)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnFiltrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFiltrar.Click

        Try

            Call LoadGrid()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnNovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNovo.Click

        Try

            Call Novo()

            txtSolicitante.Focus()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnNovo1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNovo1.Click

        Try

            Call Novo()

            Call MostrarDados()

            txtSolicitante.Focus()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalvar.Click

        Try

            frmMain.errInfo.Clear()

            If Validacao() = True Then

                Call Salvar()

            End If

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSair.Click, btnSair2.Click, btnSair3.Click, btnSair4.Click

        Try

            tmrApontamento.Stop()

            If TypeOf Me.Parent Is Form Then Me.Parent.Dispose() Else frmMain.tabMain.TabPages.Remove(Me.Parent)
            Me.Dispose()

        Catch ex As Exception
            Call TratamentoErro(ex.Message, "Chamados")
        End Try

    End Sub

    Private Sub btnVoltar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoltar.Click

        Try

            frmMain.errInfo.Clear()

            Call LoadGrid()

            Call MostrarLista()

            txtCodigoFiltro.Focus()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdListagem_RowDoubleClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdListagem.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            Select Case grdListagem.CurrentColumn.Key

                Case "editar" : Call Editar()
                Case "cancelar" : Call Cancelar()

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnIniciar_Click(sender As Object, e As EventArgs) Handles btnIniciar.Click

        Try

            If MsgBox("Deseja iniciar o chamado?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gAplicacao) = MsgBoxResult.Yes Then

                Call AlterarStatus(gcStatusChamadoIniciado)

                frmMain.Informacao("Chamado iniciado com sucesso!", Color.Blue)

            End If

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnFinalizarChamado_Click(sender As Object, e As EventArgs) Handles btnFinalizarChamado.Click

        Try

            Dim sMensagem As String = "Deseja concluir o chamado?"

            'Regra: fechar o chamado encerra os apontamentos em andamento
            If QuantidadeApontamentoAndamento() > 0 Then
                sMensagem &= vbCrLf & vbCrLf & "Existem apontamentos em andamento neste chamado. Eles serão ENCERRADOS com a data/hora atual."
            End If

            If MsgBox(sMensagem, MsgBoxStyle.Question + MsgBoxStyle.YesNo, gAplicacao) = MsgBoxResult.Yes Then

                Dim iEncerrados As Integer = AlterarStatus(gcStatusChamadoConcluido)

                frmMain.Informacao("Chamado concluído com sucesso!" & IIf(iEncerrados > 0, " " & iEncerrados & " apontamento(s) em andamento encerrado(s).", ""), Color.Blue)

            End If

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub usrAdmChamado_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        Try

            Select Case e.KeyCode

                Case Keys.Enter
                    If InStr(sender.name, "Filtro", CompareMethod.Text) > 0 Then
                        Call LoadGrid()
                    ElseIf InStr(sender.name, "Relatorio", CompareMethod.Text) > 0 And TypeOf sender Is Janus.Windows.GridEX.EditControls.EditBox = False Then
                        Call LoadRelatorio()
                    ElseIf TypeOf sender Is GridEX = False Then
                        Control_Enter(sender)
                    End If

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: COMENTÁRIO :::"

    Private Sub btnInserirComentario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInserirComentario.Click

        Try

            Call InserirComentario(False)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnInserirComentarioDev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInserirComentarioDev.Click

        Try

            Call InserirComentario(True)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdComentario_RowDoubleClick(sender As Object, e As RowActionEventArgs) Handles grdComentario.RowDoubleClick, grdComentarioDev.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            Dim oGrid As GridEX = sender

            Select Case oGrid.CurrentColumn.Key

                Case "visualizar" : Call VisualizarComentario(oGrid Is grdComentarioDev)

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnArquivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArquivo.Click

        Try

            frmMain.errInfo.Clear()

            If FtpDisponivel() = False Then Exit Sub

            If IsNumeric(txtCodigo.Text) = False Then
                frmMain.Informacao("Salve o chamado antes de anexar arquivos.", gColorInformacaoErro)
                Exit Sub
            End If

            Call InserirArquivos()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdArquivos_RowDoubleClick(sender As Object, e As RowActionEventArgs) Handles grdArquivos.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            If FtpDisponivel() = False Then Exit Sub

            Select Case grdArquivos.CurrentColumn.Key

                Case "visualizar" : Call VisualizarArquivo()
                Case "excluir" : Call ExcluirArquivo()

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub tabDadosChamado_SelectedTabChanged(sender As Object, e As Janus.Windows.UI.Tab.TabEventArgs) Handles tabDadosChamado.SelectedTabChanged

        Try

            If IsNumeric(txtCodigo.Text) = False Then Exit Sub

            Select Case e.Page.Name

                Case "pagArquivos" : Call LoadGridArquivo()
                Case "pagComentario" : Call LoadGridComentario()
                Case "pagComentarioDev" : Call LoadGridComentarioDev()

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: APONTAMENTO :::"

    Private Sub cboEmpresaApontamento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEmpresaApontamento.SelectedIndexChanged

        Try

            If bCarregando = True Then Exit Sub

            Call LoadComboChamadoApontamento()
            Call LoadGridApontamentoHistorico()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnIniciarApontamento_Click(sender As Object, e As EventArgs) Handles btnIniciarApontamento.Click

        Try

            frmMain.errInfo.Clear()

            If ValidaCampo(cboChamadoApontamento, lblChamadoApontamento) = False Then Exit Sub

            Cursor.Current = Cursors.WaitCursor

            Dim lChamado As Long = cboChamadoApontamento.SelectedValue

            'Grava o início no banco (horário do servidor MySQL)
            Dim iApontamento As Integer = oClsUsrAdmChamado.InsertApontamento(lChamado)

            Call LoadGridApontamentoAndamento()

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Apontamento " & iApontamento & " do chamado " & lChamado & " iniciado com sucesso!", Color.Blue)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdApontamentoHistorico_RowDoubleClick(sender As Object, e As RowActionEventArgs) Handles grdApontamentoHistorico.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            'A coluna do botão fica fixa à esquerda; CurrentColumn pode vir vazio na grid editável,
            'então a coluna é identificada pela posição do clique
            Dim iColuna As Integer = grdApontamentoHistorico.PointToClient(Control.MousePosition).X

            If iColuna <= grdApontamentoHistorico.RootTable.Columns("reapontar").Width + 6 Then Call Reapontar()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnEncerrarApontamento_Click(sender As Object, e As EventArgs) Handles btnEncerrarApontamento.Click

        Try

            Call EncerrarApontamento()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdApontamentoAndamento_RowDoubleClick(sender As Object, e As RowActionEventArgs) Handles grdApontamentoAndamento.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            Select Case grdApontamentoAndamento.CurrentColumn.Key

                Case "encerrar" : Call EncerrarApontamento()

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdApontamentoHistorico_CellUpdated(sender As Object, e As ColumnActionEventArgs) Handles grdApontamentoHistorico.CellUpdated

        Try

            Select Case e.Column.Key

                Case "data_hora_inicio", "data_hora_fim", "observacao"
                    Call AtualizarApontamento(grdApontamentoHistorico.CurrentRow)

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
            'Recarrega para descartar o valor não gravado
            Me.BeginInvoke(New MethodInvoker(AddressOf RecarregarHistorico))
        End Try

    End Sub

    Private Sub btnFiltrarApontamento_Click(sender As Object, e As EventArgs) Handles btnFiltrarApontamento.Click

        Try

            Call LoadGridApontamentoHistorico()
            Call LoadGridApontamentoAndamento()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub cboResponsavelApontamento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboResponsavelApontamento.SelectedIndexChanged

        Try

            If bCarregando = True Then Exit Sub

            Call LoadGridApontamentoHistorico()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnExcelApontamento_Click(sender As Object, e As EventArgs) Handles btnExcelApontamento.Click

        Try

            Call ExportExcel(grdApontamentoHistorico)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnGerarRelatorio_Click(sender As Object, e As EventArgs) Handles btnGerarRelatorio.Click

        Try

            Call LoadRelatorio()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub cboGraficoRelatorio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGraficoRelatorio.SelectedIndexChanged

        Try

            If bCarregando = True Then Exit Sub

            Call GerarGraficoRelatorio()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnExcelRelatorio_Click(sender As Object, e As EventArgs) Handles btnExcelRelatorio.Click

        Try

            Call ExportExcel(grdRelatorio)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub tmrApontamento_Tick(sender As Object, e As EventArgs) Handles tmrApontamento.Tick

        Try

            iContadorSincronizacao += 1

            'Sincroniza periodicamente com o banco (ex.: apontamento encerrado pelo fechamento do chamado)
            If iContadorSincronizacao >= gcIntervaloSincronizacao Then
                iContadorSincronizacao = 0
                Call LoadGridApontamentoAndamento()
            Else
                Call AtualizarDuracaoAndamento()
            End If

        Catch ex As Exception
            'Falha de conexão na sincronização: interrompe o Timer e informa o usuário
            tmrApontamento.Stop()
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
            Call AddKeyDown(Me, New DelegateKeyDown(AddressOf usrAdmChamado_KeyDown))
            Call AddLostFocus(Me, New DelegateLostFocus(AddressOf Control_LostFocus))

            'Combos - Chamados
            Call LoadCombo(cboStatusFiltro, "sp_select_combo_status_atividade")
            Call LoadCombo(cboStatus, "sp_select_combo_status_atividade")
            Call LoadCombo(cboEmpresaFiltro, "sp_select_combo_administracao_chamado_empresa")
            Call LoadCombo(cboTipoAtendimentoFiltro, "sp_select_combo_administracao_chamado_tipo_atendimento")
            Call LoadCombo(cboResponsavelFiltro, "sp_select_combo_administracao_chamado_usuario")
            Call LoadCombo(cboEmpresa, "sp_select_combo_administracao_chamado_empresa")
            Call LoadCombo(cboPrioridade, "sp_select_combo_chamados_prioridade")
            Call LoadCombo(cboTipoAtividade, "sp_select_combo_tipo_atividade")
            Call LoadCombo(cboResponsavel, "sp_select_combo_administracao_chamado_usuario")
            Call LoadCombo(cboTipoAtendimento, "sp_select_combo_administracao_chamado_tipo_atendimento")

            'Combos - Apontamentos
            Call LoadCombo(cboEmpresaApontamento, "sp_select_combo_administracao_chamado_empresa")
            Call LoadCombo(cboResponsavelApontamento, "sp_select_combo_administracao_chamado_usuario")

            'Combos - Relatório
            Call LoadCombo(cboEmpresaRelatorio, "sp_select_combo_administracao_chamado_empresa")
            Call LoadCombo(cboTipoAtividadeRelatorio, "sp_select_combo_tipo_atividade")
            Call LoadCombo(cboTipoAtendimentoRelatorio, "sp_select_combo_administracao_chamado_tipo_atendimento")
            Call LoadCombo(cboResponsavelRelatorio, "sp_select_combo_administracao_chamado_usuario")
            Call LoadComboLista(cboAgruparRelatorio, "EMPRESA=Empresa (cliente);RESPONSAVEL=Responsável;TIPO_ATIVIDADE=Tipo de atividade;TIPO_ATENDIMENTO=Tipo de atendimento;CHAMADO=Chamado;DIA=Dia")
            Call LoadComboLista(cboGraficoRelatorio, "PIZZA=Pizza;COLUNA=Colunas;BARRA=Barras")
            cboResponsavelApontamento.SelectedValue = goUsuario.lUsuario
            'Apontamentos sempre do usuário logado: o responsável fica travado
            cboResponsavelApontamento.Enabled = False

            tabMain.TabPages.Remove(pagDados)

            'Comentários de DEV: somente usuário interno (as procs também validam no banco)
            If goUsuario.bInterno = False Then
                tabDadosChamado.TabPages.Remove(pagComentarioDev)
                btnInserirComentarioDev.Visible = False
            End If

            Call ConfiguraGrid(grdListagem)
            Call ConfiguraGrid(grdArquivos)
            Call ConfiguraGrid(grdRelatorio)
            Call ConfiguraGrid(grdComentario)
            Call ConfiguraGrid(grdComentarioDev)
            Call ConfiguraGrid(grdApontamentoHistorico)
            Call ConfiguraGrid(grdApontamentoAndamento)

            'Observação do apontamento no padrão maiúsculo dos campos de texto
            grdApontamentoHistorico.RootTable.Columns("observacao").CharacterCasing = CharacterCasing.Upper

            dtpDataChamado.Value = Date.Now
            dtpDataInicioFiltro.Value = DateAdd(DateInterval.Month, -1, Now.Date) : dtpDataInicioFiltro.Checked = True
            dtpDataTerminoFiltro.Value = Now.Date : dtpDataTerminoFiltro.Checked = True

            dtpInicioRelatorio.Value = New Date(Now.Year, Now.Month, 1) : dtpInicioRelatorio.Checked = True
            dtpTerminoRelatorio.Value = Now.Date : dtpTerminoRelatorio.Checked = True

            dtpInicioApontamento.Value = New Date(Now.Year, Now.Month, 1) : dtpInicioApontamento.Checked = True
            dtpTerminoApontamento.Value = Now.Date : dtpTerminoApontamento.Checked = True

            bCarregando = False

            Call LoadGrid()
            Call LoadComboChamadoApontamento()
            Call LoadGridApontamentoHistorico()
            Call LoadGridApontamentoAndamento()
            Call LoadRelatorio()

            tmrApontamento.Start()

            txtCodigoFiltro.Focus()

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
            Call TratamentoErro(ex, "Chamados")
        End If

    End Sub

    Private Sub MostrarDados()

        'A aba de Apontamentos permanece sempre visível
        tabMain.TabPages.Remove(pagLista)
        If tabMain.TabPages.Contains(pagDados) = False Then tabMain.TabPages.Insert(0, pagDados)
        tabMain.SelectedTab = pagDados

    End Sub

    Private Sub MostrarLista()

        tabMain.TabPages.Remove(pagDados)
        If tabMain.TabPages.Contains(pagLista) = False Then tabMain.TabPages.Insert(0, pagLista)
        tabMain.SelectedTab = pagLista

    End Sub

    Private Sub Editar()

        Try

            Call Novo()

            With grdListagem.CurrentRow.Cells

                txtCodigo.Text = .Item("codigo_atividade").Value
                If IsDBNull(.Item("data_criacao").Value) = False Then dtpDataChamado.Value = .Item("data_criacao").Value
                txtSolicitante.Text = .Item("solicitante").Value.ToString
                If IsDBNull(.Item("codigo_empresa_cliente").Value) = False Then cboEmpresa.SelectedValue = .Item("codigo_empresa_cliente").Value
                If IsDBNull(.Item("codigo_tipo_atividade").Value) = False Then cboTipoAtividade.SelectedValue = .Item("codigo_tipo_atividade").Value
                If IsDBNull(.Item("codigo_prioridade").Value) = False Then cboPrioridade.SelectedValue = .Item("codigo_prioridade").Value
                txtTitulo.Text = .Item("titulo").Value.ToString
                txtDescricao.Text = .Item("descricao").Value.ToString
                txtModulo.Text = .Item("modulo").Value.ToString
                txtTela.Text = .Item("tela").Value.ToString
                txtNumeroDocumento.Text = .Item("numero_documento").Value.ToString
                Call DefinirStatus(CInt(.Item("codigo_status_atividade").Value))

                chkExibirUsuario.Checked = (IsDBNull(.Item("exibir_usuario").Value) = False AndAlso CInt(.Item("exibir_usuario").Value) = 1)
                Call SetCheckedValues(cboResponsavel, .Item("codigos_responsavel").Value.ToString)
                Call SetCheckedValues(cboTipoAtendimento, .Item("codigos_tipo_atendimento").Value.ToString)

                btnInserirComentario.Enabled = True
                btnInserirComentarioDev.Enabled = True

            End With

            Call LoadGridArquivo()
            Call LoadGridComentario()
            If goUsuario.bInterno = True Then Call LoadGridComentarioDev()

            Call MostrarDados()

            txtSolicitante.Focus()

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub Cancelar()

        Try

            With grdListagem.CurrentRow.Cells

                Dim sMensagem As String = "Deseja cancelar o chamado " & .Item("codigo_atividade").Value & "?"

                If .Item("apontamentos_andamento").Value > 0 Then
                    sMensagem &= vbCrLf & vbCrLf & "Existem " & .Item("apontamentos_andamento").Value & " apontamento(s) em andamento neste chamado. Eles serão ENCERRADOS com a data/hora atual."
                End If

                If MsgBox(sMensagem, MsgBoxStyle.Question + MsgBoxStyle.YesNo, gAplicacao) = MsgBoxResult.Yes Then

                    Cursor.Current = Cursors.WaitCursor

                    Dim iEncerrados As Integer = oClsUsrAdmChamado.UpdateStatus(.Item("codigo_atividade").Value, _
                                                                                gcStatusChamadoCancelado)

                    frmMain.Informacao("Chamado cancelado com sucesso!" & IIf(iEncerrados > 0, " " & iEncerrados & " apontamento(s) em andamento encerrado(s).", ""), Color.Blue)

                    Call LoadGrid()
                    Call AtualizarApontamentos()

                    Cursor.Current = Cursors.Default

                End If

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub DefinirStatus(ByVal iStatus As Integer)

        'Posiciona o combo sem disparar a gravação do status
        bAtualizandoStatus = True

        Try
            cboStatus.SelectedValue = iStatus
            iStatusAtual = iStatus
            cboStatus.Enabled = IsNumeric(txtCodigo.Text)
        Finally
            bAtualizandoStatus = False
        End Try

    End Sub

    Private Sub cboStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStatus.SelectedIndexChanged

        Try

            If bCarregando OrElse bAtualizandoStatus OrElse IsNumeric(txtCodigo.Text) = False OrElse IsNothing(cboStatus.SelectedValue) Then Exit Sub

            Dim iNovo As Integer = CInt(cboStatus.SelectedValue)

            If iNovo = iStatusAtual Then Exit Sub

            Dim sMensagem As String = "Deseja alterar o status do chamado para '" & cboStatus.Text & "'?"

            If (iNovo = gcStatusChamadoConcluido OrElse iNovo = gcStatusChamadoCancelado) AndAlso QuantidadeApontamentoAndamento() > 0 Then
                sMensagem &= vbCrLf & vbCrLf & "Existem apontamentos em andamento neste chamado. Eles serão ENCERRADOS com a data/hora atual."
            End If

            If MsgBox(sMensagem, MsgBoxStyle.Question + MsgBoxStyle.YesNo, gAplicacao) = MsgBoxResult.No Then
                Call DefinirStatus(iStatusAtual)
                Exit Sub
            End If

            Try
                Dim iEncerrados As Integer = AlterarStatus(iNovo)
                frmMain.Informacao("Status alterado com sucesso!" & IIf(iEncerrados > 0, " " & iEncerrados & " apontamento(s) em andamento encerrado(s).", ""), Color.Blue)
            Catch
                Call DefinirStatus(iStatusAtual)
                Throw
            End Try

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Function AlterarStatus(ByVal iStatus As Integer) As Integer

        Try

            If IsNumeric(txtCodigo.Text) = False Then
                frmMain.Informacao("Salve o chamado antes de alterar o status.", gColorInformacaoErro)
                Return 0
            End If

            Cursor.Current = Cursors.WaitCursor

            'O status do chamado é independente do status dos apontamentos
            Dim iEncerrados As Integer = oClsUsrAdmChamado.UpdateStatus(txtCodigo.Text, iStatus)

            Call DefinirStatus(iStatus)

            Call AtualizarApontamentos()

            Cursor.Current = Cursors.Default

            Return iEncerrados

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function QuantidadeApontamentoAndamento() As Integer

        Try

            If IsNumeric(txtCodigo.Text) = False Then Return 0

            For Each oRow As GridEXRow In grdListagem.GetRows
                If oRow.RowType = RowType.Record AndAlso oRow.Cells("codigo_atividade").Value.ToString = txtCodigo.Text Then
                    Return oRow.Cells("apontamentos_andamento").Value
                End If
            Next

            Return 0

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub Novo()

        Try

            frmMain.errInfo.Clear()

            txtCodigo.Text = ""
            btnSalvar.Enabled = True
            btnIniciar.Enabled = True
            btnFinalizarChamado.Enabled = True
            btnInserirComentario.Enabled = False
            btnInserirComentarioDev.Enabled = False
            dtpDataChamado.Value = Date.Now()
            txtSolicitante.Text = goUsuario.sNome
            cboEmpresa.SelectedIndex = -1
            cboTipoAtividade.SelectedIndex = -1
            cboPrioridade.SelectedIndex = -1
            cboResponsavel.UncheckAll()
            cboTipoAtendimento.UncheckAll()
            chkExibirUsuario.Checked = False
            txtTitulo.Text = ""
            txtDescricao.Text = ""
            txtModulo.Text = ""
            txtTela.Text = ""
            txtNumeroDocumento.Text = ""
            Call DefinirStatus(1)
            cboStatus.Enabled = False
            grdArquivos.DataSource = Nothing
            grdComentario.DataSource = Nothing
            grdComentarioDev.DataSource = Nothing

            'Sugere o usuário autenticado como responsável
            Call SetCheckedValues(cboResponsavel, goUsuario.lUsuario.ToString)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Salvar()

        Try

            Cursor.Current = Cursors.WaitCursor

            If IsNumeric(txtCodigo.Text) Then

                oClsUsrAdmChamado.Update(txtCodigo.Text, _
                                         cboEmpresa.SelectedValue, _
                                         txtTitulo.Text.Trim, _
                                         txtDescricao.Text.Trim, _
                                         txtSolicitante.Text.Trim, _
                                         cboTipoAtividade.SelectedValue, _
                                         cboPrioridade.SelectedValue, _
                                         txtModulo.Text.Trim, _
                                         txtTela.Text.Trim, _
                                         txtNumeroDocumento.Text.Trim, _
                                         GetCheckedValues(cboResponsavel), _
                                         GetCheckedValues(cboTipoAtendimento), _
                                         chkExibirUsuario.Checked)

                frmMain.Informacao("Registro alterado com sucesso!", Color.Blue)

            Else

                txtCodigo.Text = oClsUsrAdmChamado.Insert(cboEmpresa.SelectedValue, _
                                                          txtTitulo.Text.Trim, _
                                                          txtDescricao.Text.Trim, _
                                                          txtSolicitante.Text.Trim, _
                                                          cboTipoAtividade.SelectedValue, _
                                                          cboPrioridade.SelectedValue, _
                                                          txtModulo.Text.Trim, _
                                                          txtTela.Text.Trim, _
                                                          txtNumeroDocumento.Text.Trim, _
                                                          GetCheckedValues(cboResponsavel), _
                                                          GetCheckedValues(cboTipoAtendimento), _
                                         chkExibirUsuario.Checked)

                Call DefinirStatus(1)
                btnInserirComentario.Enabled = True
                btnInserirComentarioDev.Enabled = True

                Call LoadGridComentario()

                frmMain.Informacao("Registro inserido com sucesso! Chamado " & txtCodigo.Text & " disponível para apontamento.", Color.Blue)

            End If

            'Chamado novo/alterado fica disponível automaticamente no combo de apontamentos
            Call LoadComboChamadoApontamento()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub LoadGrid()

        Try

            Cursor.Current = Cursors.WaitCursor

            Call oClsUsrAdmChamado.LoadGrid(grdListagem, _
                                           IIf(dtpDataInicioFiltro.Checked = False, Nothing, dtpDataInicioFiltro.Value.Date), _
                                           IIf(dtpDataTerminoFiltro.Checked = False, Nothing, dtpDataTerminoFiltro.Value.Date), _
                                           txtCodigoFiltro.Text.Trim, _
                                           IIf(cboEmpresaFiltro.SelectedIndex = -1, -1, cboEmpresaFiltro.SelectedValue), _
                                           GetCheckedValues(cboStatusFiltro), _
                                           txtTituloFiltro.Text.Trim, _
                                           IIf(cboResponsavelFiltro.SelectedIndex = -1, -1, cboResponsavelFiltro.SelectedValue), _
                                           IIf(cboTipoAtendimentoFiltro.SelectedIndex = -1, -1, cboTipoAtendimentoFiltro.SelectedValue))

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function Validacao() As Boolean

        Try

            Validacao = False

            If ValidaCampo(cboEmpresa, lblEmpresa) = False Then Exit Function
            If ValidaCampo(cboPrioridade, lblPrioridade) = False Then Exit Function
            If ValidaCampo(txtTitulo, lblTitulo, TipoCampo.texto) = False Then Exit Function
            If ValidaCampo(cboTipoAtividade, lblTipoChamado) = False Then Exit Function
            If ValidaCampo(cboResponsavel, lblResponsavel) = False Then Exit Function
            If ValidaCampo(cboTipoAtendimento, lblTipoAtendimento) = False Then Exit Function
            If ValidaCampo(txtDescricao, lblDefinicao, TipoCampo.texto) = False Then Exit Function

            Validacao = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "::: COMENTÁRIO :::"

    Private Function FtpDisponivel() As Boolean

        'O FTP é configurado no XmlConfig\Config.xml (nó Documentacti/Ftp)
        If oFtp.Configurado() = False Then
            frmMain.Informacao("FTP dos anexos não configurado no Config.xml.", gColorInformacaoErro)
            Return False
        End If

        Return True

    End Function

    Private Sub InserirArquivos()

        Try

            Dim oDialog As New OpenFileDialog
            oDialog.Multiselect = True
            oDialog.Title = "Selecione os arquivos do chamado " & txtCodigo.Text

            If oDialog.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim iEnviados As Integer = 0

            Cursor.Current = Cursors.WaitCursor

            Try

                For Each sCaminho As String In oDialog.FileNames

                    Dim oInfo As New System.IO.FileInfo(sCaminho)

                    If oInfo.Length > gcTamanhoMaximoAnexo Then
                        Throw New ArgumentException("O arquivo '" & oInfo.Name & "' excede o limite de " & (gcTamanhoMaximoAnexo / 1048576) & " MB.")
                    End If

                    'Mesmo padrão do INTERACTI: somente o nome do arquivo (sem acento), direto na pasta de anexos.
                    'Se já existir um arquivo com o mesmo nome no servidor, acrescenta um sufixo para não sobrescrevê-lo
                    Dim sRelativo As String = NomeArquivoSeguro(oInfo.Name)
                    Dim iSufixo As Integer = 1

                    Do While oFtp.Existe(sRelativo)
                        sRelativo = System.IO.Path.GetFileNameWithoutExtension(NomeArquivoSeguro(oInfo.Name)) & "_" & iSufixo & System.IO.Path.GetExtension(NomeArquivoSeguro(oInfo.Name))
                        iSufixo += 1
                    Loop

                    oFtp.Upload(sRelativo, sCaminho)

                    Try
                        oClsUsrAdmChamado.InsertArquivo(txtCodigo.Text, sRelativo)
                    Catch
                        'Falha ao registrar no banco: remove somente o arquivo que acabou de ser enviado
                        Try
                            oFtp.Delete(sRelativo)
                        Catch
                        End Try
                        Throw
                    End Try

                    iEnviados += 1

                Next

            Finally

                Call LoadGridArquivo()

            End Try

            Cursor.Current = Cursors.Default

            frmMain.Informacao(iEnviados & " arquivo(s) anexado(s) com sucesso!", Color.Blue)

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub VisualizarArquivo()

        Try

            Dim sRelativo As String = grdArquivos.CurrentRow.Cells.Item("nome_arquivo").Value.ToString

            Cursor.Current = Cursors.WaitCursor

            'Somente o nome do arquivo vai para a pasta temporária (descarta qualquer subpasta do caminho)
            Dim sPasta As String = System.IO.Path.Combine(System.IO.Path.GetTempPath, "Documentacti")
            System.IO.Directory.CreateDirectory(sPasta)

            Dim sDestino As String = System.IO.Path.Combine(sPasta, System.IO.Path.GetFileName(sRelativo.Replace("/", "\")))

            oFtp.Download(sRelativo, sDestino)

            Cursor.Current = Cursors.Default

            'Arquivos executáveis/scripts não são abertos automaticamente
            Dim sExtensoesBloqueadas As String = ".exe.bat.cmd.com.msi.scr.js.jse.vbs.vbe.ps1.lnk.jar.hta.reg.dll.wsf"

            If sExtensoesBloqueadas.Contains(System.IO.Path.GetExtension(sDestino).ToLower) AndAlso System.IO.Path.GetExtension(sDestino) <> "" Then
                MsgBox("Por segurança, este tipo de arquivo não é aberto automaticamente." & vbCrLf & "O arquivo foi salvo em:" & vbCrLf & sDestino, MsgBoxStyle.Information, gAplicacao)
                Process.Start("explorer.exe", "/select," & Chr(34) & sDestino & Chr(34))
            Else
                Process.Start(sDestino)
            End If

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub ExcluirArquivo()

        Try

            With grdArquivos.CurrentRow.Cells

                Dim sRelativo As String = .Item("nome_arquivo").Value.ToString

                If MsgBox("Deseja excluir o anexo '" & System.IO.Path.GetFileName(sRelativo.Replace("/", "\")) & "'?" & vbCrLf & vbCrLf & _
                          "O arquivo será removido do servidor FTP (produção).", _
                          MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gAplicacao) = MsgBoxResult.No Then Exit Sub

                Cursor.Current = Cursors.WaitCursor

                'Remove o registro; o arquivo só sai do FTP se nenhum outro registro o utiliza
                Dim iOutras As Integer = oClsUsrAdmChamado.DeleteArquivo(txtCodigo.Text, .Item("codigo").Value)

                Dim sAviso As String = ""

                If iOutras = 0 Then
                    Try
                        oFtp.Delete(sRelativo)
                    Catch exFtp As Exception
                        sAviso = " Atenção: o arquivo não pôde ser removido do FTP (" & exFtp.Message & ")."
                    End Try
                End If

                Call LoadGridArquivo()

                Cursor.Current = Cursors.Default

                frmMain.Informacao("Anexo excluído com sucesso!" & sAviso, IIf(sAviso = "", Color.Blue, gColorInformacaoErro))

            End With

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub LoadGridArquivo()

        Try

            If IsNumeric(txtCodigo.Text) = False Then Exit Sub

            Call oClsUsrAdmChamado.LoadGridArquivo(grdArquivos, txtCodigo.Text)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub LoadGridComentario()

        Try

            If IsNumeric(txtCodigo.Text) = False Then Exit Sub

            Call oClsUsrAdmChamado.LoadGridComentario(grdComentario, txtCodigo.Text)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub LoadGridComentarioDev()

        Try

            If IsNumeric(txtCodigo.Text) = False Or goUsuario.bInterno = False Then Exit Sub

            Call oClsUsrAdmChamado.LoadGridComentarioDev(grdComentarioDev, txtCodigo.Text)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub InserirComentario(ByVal bComentarioDev As Boolean)

        Try

            If IsNumeric(txtCodigo.Text) = False Then
                frmMain.Informacao("Salve o chamado antes de inserir comentários.", gColorInformacaoErro)
                Exit Sub
            End If

            Dim oForm As New frmAdmChamadoComentario

            oForm.ComentarioDev = bComentarioDev
            oForm.txtTitulo.Text = txtTitulo.Text
            oForm.txtCodigoChamado.Text = txtCodigo.Text
            oForm.txtDataChamado.Text = Format(dtpDataChamado.Value, "dd/MM/yyyy")
            oForm.txtSolicitante.Text = txtSolicitante.Text
            oForm.ShowDialog(Me)

            If bComentarioDev = True Then
                tabDadosChamado.SelectedTab = pagComentarioDev
                Call LoadGridComentarioDev()
            Else
                tabDadosChamado.SelectedTab = pagComentario
                Call LoadGridComentario()
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub VisualizarComentario(ByVal bComentarioDev As Boolean)

        Try

            Dim oGrid As GridEX = IIf(bComentarioDev, grdComentarioDev, grdComentario)
            Dim oForm As New frmAdmChamadoComentario

            oForm.ComentarioDev = bComentarioDev
            oForm.Visualizacao = True
            oForm.txtTitulo.Text = txtTitulo.Text
            oForm.txtCodigoChamado.Text = txtCodigo.Text
            oForm.txtDataChamado.Text = Format(dtpDataChamado.Value, "dd/MM/yyyy")
            oForm.txtSolicitante.Text = oGrid.CurrentRow.Cells("usuario").Value.ToString
            oForm.txtComentario.Text = oGrid.CurrentRow.Cells(IIf(bComentarioDev, "comentario", "texto")).Value.ToString
            oForm.ShowDialog(Me)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "::: RELATÓRIO :::"

    Private Sub LoadRelatorio()

        Try

            Cursor.Current = Cursors.WaitCursor

            oTabelaRelatorio = oClsUsrAdmChamado.LoadGridRelatorio(grdRelatorio, _
                                                                   IIf(dtpInicioRelatorio.Checked = False, Nothing, dtpInicioRelatorio.Value.Date), _
                                                                   IIf(dtpTerminoRelatorio.Checked = False, Nothing, dtpTerminoRelatorio.Value.Date), _
                                                                   IIf(cboEmpresaRelatorio.SelectedIndex = -1, -1, cboEmpresaRelatorio.SelectedValue), _
                                                                   IIf(cboTipoAtividadeRelatorio.SelectedIndex = -1, -1, cboTipoAtividadeRelatorio.SelectedValue), _
                                                                   IIf(cboTipoAtendimentoRelatorio.SelectedIndex = -1, -1, cboTipoAtendimentoRelatorio.SelectedValue), _
                                                                   IIf(cboResponsavelRelatorio.SelectedIndex = -1, -1, cboResponsavelRelatorio.SelectedValue), _
                                                                   IIf(cboAgruparRelatorio.SelectedIndex = -1, "EMPRESA", cboAgruparRelatorio.SelectedValue))

            Dim lTotal As Long = 0

            For Each oDataRow As DataRow In oTabelaRelatorio.Rows
                lTotal += CLng(oDataRow.Item("segundos"))
            Next

            lblTotalRelatorio.Text = "Total: " & FormatarDuracao(lTotal) & " (" & Format(lTotal / 3600, "N2") & " h)" & _
                                     IIf(cboAgruparRelatorio.SelectedValue = "TIPO_ATENDIMENTO", " - chamado com mais de um tipo conta em cada tipo", "")

            Call GerarGraficoRelatorio()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub GerarGraficoRelatorio()

        Try

            chrRelatorio.Titles.Clear()
            chrRelatorio.Series.Clear()

            If IsNothing(oTabelaRelatorio) OrElse oTabelaRelatorio.Rows.Count = 0 Then Exit Sub

            'No gráfico, os maiores grupos aparecem individualmente e o restante é agrupado em OUTROS (a grid mostra todos)
            Dim oDados As New DataTable
            oDados.Columns.Add("grupo", GetType(String))
            oDados.Columns.Add("horas", GetType(Double))
            oDados.Columns.Add("descricao", GetType(String))

            Dim lOutros As Long = 0
            Dim i As Integer = 0

            For Each oDataRow As DataRow In oTabelaRelatorio.Rows
                If i < gcLimiteGrafico Then
                    oDados.Rows.Add(oDataRow.Item("grupo").ToString, CLng(oDataRow.Item("segundos")) / 3600, _
                                    oDataRow.Item("duracao").ToString & " (" & Format(CDbl(oDataRow.Item("percentual")), "N2") & "%)")
                Else
                    lOutros += CLng(oDataRow.Item("segundos"))
                End If
                i += 1
            Next

            If lOutros > 0 Then oDados.Rows.Add("OUTROS", lOutros / 3600, FormatarDuracao(lOutros))

            Dim sTipo As String = IIf(cboGraficoRelatorio.SelectedIndex = -1, "PIZZA", cboGraficoRelatorio.SelectedValue)

            'Adiciona Serie
            chrRelatorio.Series.Add("main")
            chrRelatorio.Titles.Add("Horas por " & cboAgruparRelatorio.Text)

            'Carrega Grafico
            chrRelatorio.Series("main").Points.DataBind(New DataTableReader(oDados), "grupo", "horas", "Tooltip=descricao")

            With chrRelatorio.Series("main")

                Select Case sTipo
                    Case "COLUNA" : .Type = DWC.SeriesChartType.Column
                    Case "BARRA" : .Type = DWC.SeriesChartType.Bar
                    Case Else : .Type = DWC.SeriesChartType.Pie
                End Select

                .ShowInLegend = True
                .SmartLabels.Enabled = True
                .MarkerSize = 1
                .BorderWidth = 1
                .BorderStyle = DWC.ChartDashStyle.Solid
                .BorderColor = Color.FromArgb(255, 26, 59, 105)
                .ShadowOffset = 2
                .ShowLabelAsValue = True
                .LabelFormat = "N2"
                .Palette = DWC.ChartColorPalette.Dundas
                .EmptyPointStyle.Color = Color.Transparent

                If sTipo = "PIZZA" Then
                    .SmartLabels.AllowOutsidePlotArea = DWC.LabelOutsidePlotAreaStyle.Yes
                    .Item("PieLabelStyle") = "Outside"
                End If

            End With

            With chrRelatorio.ChartAreas(0)

                .Area3DStyle.Enable3D = False
                .BackColor = Color.White
                .BorderColor = Color.FromArgb(255, 26, 59, 105)
                .ShadowOffset = 2
                .BorderStyle = DWC.ChartDashStyle.Solid
                .AxisX.MajorGrid.LineColor = Color.Silver
                .AxisY.MajorGrid.LineColor = Color.Silver
                .AxisX.MajorGrid.LineStyle = DWC.ChartDashStyle.Solid
                .AxisY.MajorGrid.LineStyle = DWC.ChartDashStyle.Solid
                .AxisX.Interval = 1
                .AxisY.Title = "Horas"
                .AxisY.TitleFont = New Font("Arial", 8.25!)
                .AxisX.LabelStyle.Font = New Font("Arial", 8.25!)
                .AxisY.LabelStyle.Font = New Font("Arial", 8.25!)
                .ReCalc()

            End With

            With chrRelatorio.Legends(0)

                .Alignment = StringAlignment.Center
                .Docking = DWC.LegendDocking.Bottom
                .LegendStyle = DWC.LegendStyle.Table
                .AutoFitText = False
                .BackColor = Color.White
                .BorderColor = Color.FromArgb(255, 26, 59, 105)
                .BorderStyle = DWC.ChartDashStyle.Solid
                .BorderWidth = 1

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "::: APONTAMENTO :::"

    Private Sub AtualizarApontamentos()

        Try

            Call LoadComboChamadoApontamento()
            Call LoadGridApontamentoHistorico()
            Call LoadGridApontamentoAndamento()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub LoadComboChamadoApontamento()

        Try

            'Mantém o chamado selecionado, se continuar elegível
            Dim oSelecionado As Object = IIf(cboChamadoApontamento.SelectedIndex = -1, Nothing, cboChamadoApontamento.SelectedValue)

            Call oClsUsrAdmChamado.LoadComboChamadoApontamento(cboChamadoApontamento, _
                                                               IIf(cboEmpresaApontamento.SelectedIndex = -1, -1, cboEmpresaApontamento.SelectedValue))

            If IsNothing(oSelecionado) = False Then cboChamadoApontamento.SelectedValue = oSelecionado

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub LoadGridApontamentoHistorico()

        Try

            Cursor.Current = Cursors.WaitCursor

            Call oClsUsrAdmChamado.LoadGridApontamentoHistorico(grdApontamentoHistorico, _
                                                                IIf(cboResponsavelApontamento.SelectedIndex = -1, -1, cboResponsavelApontamento.SelectedValue), _
                                                                IIf(cboEmpresaApontamento.SelectedIndex = -1, -1, cboEmpresaApontamento.SelectedValue), _
                                                                -1, _
                                                                IIf(dtpInicioApontamento.Checked = False, Nothing, dtpInicioApontamento.Value.Date), _
                                                                IIf(dtpTerminoApontamento.Checked = False, Nothing, dtpTerminoApontamento.Value.Date))

            Call AtualizarTotalApontamento()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub RecarregarHistorico()

        Try

            Call LoadGridApontamentoHistorico()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub AtualizarTotalApontamento()

        Try

            Dim lTotal As Long = 0

            For Each oRow As GridEXRow In grdApontamentoHistorico.GetRows
                If oRow.RowType = RowType.Record AndAlso IsDBNull(oRow.Cells("duracao_segundos").Value) = False Then
                    lTotal += CLng(oRow.Cells("duracao_segundos").Value)
                End If
            Next

            lblTotalApontamento.Text = "Total no período: " & FormatarDuracao(lTotal) & " (" & Format(lTotal / 3600, "N2") & " h)"

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub LoadGridApontamentoAndamento()

        Try

            'Mantém a linha selecionada após recarregar
            Dim sChave As String = ChaveApontamentoSelecionado()

            oTabelaAndamento = oClsUsrAdmChamado.LoadGridApontamentoAndamento(grdApontamentoAndamento)

            'Diferença entre o relógio do banco e o da estação
            If oTabelaAndamento.Rows.Count > 0 Then
                oDiferencaServidor = CDate(oTabelaAndamento.Rows(0).Item("data_hora_servidor")).Subtract(Date.Now)
            End If

            iContadorSincronizacao = 0

            Call AtualizarDuracaoAndamento()

            If sChave <> "" Then
                For Each oRow As GridEXRow In grdApontamentoAndamento.GetRows
                    If ChaveApontamento(oRow) = sChave Then grdApontamentoAndamento.MoveTo(oRow) : Exit For
                Next
            End If

            btnEncerrarApontamento.Enabled = (oTabelaAndamento.Rows.Count > 0)

            If tmrApontamento.Enabled = False And bCarregando = False Then tmrApontamento.Start()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AtualizarDuracaoAndamento()

        Try

            If IsNothing(oTabelaAndamento) Then Exit Sub

            'Duração provisória: hora atual do banco (estimada) - início persistido
            Dim dAgora As Date = Date.Now.Add(oDiferencaServidor)

            For Each oDataRow As DataRow In oTabelaAndamento.Rows
                Dim lSegundos As Long = CLng(Math.Floor(dAgora.Subtract(CDate(oDataRow.Item("data_hora_inicio"))).TotalSeconds))
                Dim sDuracao As String = FormatarDuracao(lSegundos)
                If oDataRow.Item("duracao").ToString <> sDuracao Then oDataRow.Item("duracao") = sDuracao
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function ChaveApontamento(ByVal oRow As GridEXRow) As String

        Return oRow.Cells("codigo_empresa").Value.ToString & "|" & _
               oRow.Cells("codigo_usuario").Value.ToString & "|" & _
               oRow.Cells("codigo_chamado").Value.ToString & "|" & _
               oRow.Cells("codigo_apontamento").Value.ToString

    End Function

    Private Function ChaveApontamentoSelecionado() As String

        Try

            If IsNothing(grdApontamentoAndamento.CurrentRow) OrElse grdApontamentoAndamento.CurrentRow.RowType <> RowType.Record Then Return ""

            Return ChaveApontamento(grdApontamentoAndamento.CurrentRow)

        Catch
            Return ""
        End Try

    End Function

    Private Sub Reapontar()

        Try

            Dim lChamado As Long = grdApontamentoHistorico.CurrentRow.Cells("codigo_chamado").Value

            Cursor.Current = Cursors.WaitCursor

            'Inicia um novo apontamento (horário do banco) no mesmo chamado do registro selecionado
            Dim iApontamento As Integer = oClsUsrAdmChamado.InsertApontamento(lChamado)

            Call LoadGridApontamentoAndamento()

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Apontamento " & iApontamento & " do chamado " & lChamado & " iniciado com sucesso!", Color.Blue)

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub EncerrarApontamento()

        Try

            If IsNothing(grdApontamentoAndamento.CurrentRow) OrElse grdApontamentoAndamento.CurrentRow.RowType <> RowType.Record Then
                frmMain.Informacao("Selecione o apontamento em andamento que deseja encerrar.", gColorInformacaoErro)
                Exit Sub
            End If

            With grdApontamentoAndamento.CurrentRow.Cells

                If MsgBox("Deseja encerrar o apontamento do chamado " & .Item("codigo_chamado").Value & " - " & .Item("titulo").Value & "?" & vbCrLf & vbCrLf & _
                          "O chamado NÃO será fechado; somente este apontamento será encerrado.", _
                          MsgBoxStyle.Question + MsgBoxStyle.YesNo, gAplicacao) = MsgBoxResult.No Then Exit Sub

                Cursor.Current = Cursors.WaitCursor

                'Encerra somente o registro da chave composta selecionada
                Call oClsUsrAdmChamado.EncerrarApontamento(.Item("codigo_empresa").Value, _
                                                           .Item("codigo_usuario").Value, _
                                                           .Item("codigo_chamado").Value, _
                                                           .Item("codigo_apontamento").Value)

                frmMain.Informacao("Apontamento do chamado " & .Item("codigo_chamado").Value & " encerrado com sucesso!", Color.Blue)

            End With

            Call LoadGridApontamentoAndamento()
            Call LoadGridApontamentoHistorico()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AtualizarApontamento(ByVal oRow As GridEXRow)

        Try

            If IsNothing(oRow) OrElse oRow.RowType <> RowType.Record Then Exit Sub

            Dim oInicio As Object = oRow.Cells("data_hora_inicio").Value
            Dim oFim As Object = oRow.Cells("data_hora_fim").Value
            Dim dAgora As Date = Date.Now.Add(oDiferencaServidor)

            'Validação antes de persistir (o banco valida novamente)
            If IsDBNull(oInicio) OrElse IsNothing(oInicio) OrElse IsDate(oInicio) = False Then
                Throw New ArgumentException("Informe uma data/hora de início válida (dd/MM/yyyy HH:mm:ss).")
            End If

            If IsDBNull(oFim) OrElse IsNothing(oFim) OrElse IsDate(oFim) = False Then
                Throw New ArgumentException("Informe uma data/hora de término válida (dd/MM/yyyy HH:mm:ss). Apontamento encerrado não pode ficar sem término.")
            End If

            If CDate(oFim) <= CDate(oInicio) Then
                Throw New ArgumentException("A data/hora de término deve ser maior que a data/hora de início.")
            End If

            If CDate(oInicio) > dAgora Or CDate(oFim) > dAgora Then
                Throw New ArgumentException("A data/hora do apontamento não pode ser maior que a data/hora atual.")
            End If

            Cursor.Current = Cursors.WaitCursor

            'Atualiza o registro existente localizado pela chave composta completa
            Call oClsUsrAdmChamado.UpdateApontamento(oRow.Cells("codigo_empresa").Value, _
                                                     oRow.Cells("codigo_usuario").Value, _
                                                     oRow.Cells("codigo_chamado").Value, _
                                                     oRow.Cells("codigo_apontamento").Value, _
                                                     CDate(oInicio), _
                                                     CDate(oFim), _
                                                     oRow.Cells("observacao").Value.ToString.Trim)

            frmMain.Informacao("Apontamento atualizado com sucesso!", Color.Blue)

            'Recarrega após o evento para recalcular duração e totais com os valores gravados
            Me.BeginInvoke(New MethodInvoker(AddressOf RecarregarHistorico))

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#End Region

End Class
