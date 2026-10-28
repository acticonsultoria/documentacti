Imports Janus.Windows.GridEX
Imports Janus.Windows.EditControls
Imports MySql.Data.MySqlClient
Imports DWC = Dundas.Charting.WinControl
Imports ChatUI

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

    'Chat do chamado: uma conversa por canal (1 = cliente / 2 = interno da equipe)
    Private Const gcCanalChatCliente As Integer = 1
    Private Const gcCanalChatDev As Integer = 2
    Private lChamadoChat As Long = 0
    Private oUsuarioChat As ChatUser
    Private oConversaChat As ChatConversation
    Private oConversaChatDev As ChatConversation
    Private oUsuariosChat As New Dictionary(Of Long, ChatUser)
    Private iLidaChat As Integer = 0
    Private iLidaChatDev As Integer = 0
    Private bAtualizandoChat As Boolean = False

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
            tmrChat.Stop()

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
                Case "pagChecklist" : Call LoadGridChecklist()
                Case "pagChat", "pagChatDev" : Call AtualizarLeituraChat()

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: CHECKLIST :::"

    Private Sub btnAdicionarChecklist_Click(sender As Object, e As EventArgs) Handles btnAdicionarChecklist.Click

        Try

            Call AdicionarItemChecklist()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub txtChecklist_KeyDown(sender As Object, e As KeyEventArgs) Handles txtChecklist.KeyDown

        Try

            'Enter adiciona o item, sem precisar do botão
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                Call AdicionarItemChecklist()
            End If

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdChecklist_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdChecklist.CellValueChanged

        Try

            'Check gravado na hora
            If e.Column.Key = "concluido" Then grdChecklist.UpdateData()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdChecklist_CellUpdated(sender As Object, e As ColumnActionEventArgs) Handles grdChecklist.CellUpdated

        Try

            If e.Column.Key = "concluido" Then

                With grdChecklist.CurrentRow.Cells
                    oClsUsrAdmChamado.UpdateChecklist(CLng(.Item("codigo").Value), _
                                                      IsDBNull(.Item("concluido").Value) = False AndAlso CBool(.Item("concluido").Value))
                End With

                'Recarrega após o evento para mostrar quem concluiu e quando
                Me.BeginInvoke(New MethodInvoker(AddressOf LoadGridChecklist))

            End If

        Catch ex As Exception
            Call TratarErro(ex)
            Me.BeginInvoke(New MethodInvoker(AddressOf LoadGridChecklist))
        End Try

    End Sub

    Private Sub grdChecklist_RowDoubleClick(sender As Object, e As RowActionEventArgs) Handles grdChecklist.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            If grdChecklist.CurrentColumn.Key = "excluir" Then Call ExcluirItemChecklist()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: CHAT :::"

    Private Sub chtChat_MessageSubmitted(sender As Object, e As ChatMessageSubmittedEventArgs) Handles chtChat.MessageSubmitted, chtChatDev.MessageSubmitted

        Call EnviarMensagemChat(e.Conversation, e.Message)

    End Sub

    Private Sub chtChat_RetryRequested(sender As Object, e As ChatMessageEventArgs) Handles chtChat.RetryRequested, chtChatDev.RetryRequested

        'Reenvio de mensagem com falha (clique no "!" do balão)
        If e.Message.Status <> MessageStatus.Failed Then Exit Sub

        e.Message.Status = MessageStatus.Sending

        Call EnviarMensagemChat(CType(sender, ChatBox).Conversation, e.Message)

    End Sub

    Private Sub chtChat_RefreshRequested(sender As Object, e As ChatConversationEventArgs) Handles chtChat.RefreshRequested, chtChatDev.RefreshRequested

        'Atualização manual (sem conexão em tempo real): mesma busca do Timer, na hora
        If bAtualizandoChat OrElse lChamadoChat = 0 Then Exit Sub

        bAtualizandoChat = True

        Try

            Cursor.Current = Cursors.WaitCursor

            Call AtualizarChat()

            'Volta a atualização automática, caso tenha sido interrompida por falha de conexão
            tmrChat.Start()

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Chat atualizado.", Color.Blue)

        Catch ex As Exception
            Call TratarErro(ex)
        Finally
            bAtualizandoChat = False
        End Try

    End Sub

    Private Sub chtChat_ImagePasted(sender As Object, e As ChatImageEventArgs) Handles chtChat.ImagePasted, chtChatDev.ImagePasted

        Try

            'Imagem colada (Ctrl+V): grava como PNG para seguir o mesmo caminho dos arquivos (FTP)
            e.Handled = True

            Dim sCaminho As String = System.IO.Path.Combine(PastaArquivosTemporarios(), "imagem_" & Now.ToString("yyyyMMdd_HHmmss") & ".png")

            e.Image.Save(sCaminho, System.Drawing.Imaging.ImageFormat.Png)

            CType(sender, ChatBox).SendFile(sCaminho)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub chtChat_AttachmentClick(sender As Object, e As ChatMessageEventArgs) Handles chtChat.AttachmentClick, chtChatDev.AttachmentClick

        Try

            e.Handled = True

            Call AbrirAnexoChat(e.Message.Attachment)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub tmrChat_Tick(sender As Object, e As EventArgs) Handles tmrChat.Tick

        'Busca mensagens novas somente com um chamado aberto na aba de dados
        If bAtualizandoChat OrElse lChamadoChat = 0 OrElse tabMain.SelectedTab IsNot pagDados Then Exit Sub

        bAtualizandoChat = True

        Try

            Call AtualizarChat()

        Catch ex As Exception
            'Falha de conexão na atualização: interrompe o Timer e informa o usuário (volta ao reabrir o chamado)
            tmrChat.Stop()
            Call TratarErro(ex)
        Finally
            bAtualizandoChat = False
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

                Case "anulado"
                    Call AtualizarSituacaoApontamento(grdApontamentoHistorico.CurrentRow)

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
            'Recarrega para descartar o valor não gravado
            Me.BeginInvoke(New MethodInvoker(AddressOf RecarregarHistorico))
        End Try

    End Sub

    Private Sub grdApontamentoHistorico_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdApontamentoHistorico.CellValueChanged

        Try

            'Combo de situação: grava ao escolher, sem precisar sair da célula
            If e.Column.Key = "anulado" Then grdApontamentoHistorico.UpdateData()

        Catch ex As Exception
            Call TratarErro(ex)
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

            'Time: começa no time do usuário (chamados das empresas daquele time); TODOS mostra os demais
            Call LoadCombo(cboTimeFiltro, "sp_select_combo_tipo_time_empresa")
            Dim oTimes As DataTable = CType(cboTimeFiltro.DataSource, DataTable)
            Dim oTodos As DataRow = oTimes.NewRow
            oTodos.Item("codigo") = -1
            oTodos.Item("descricao") = "TODOS"
            oTimes.Rows.InsertAt(oTodos, 0)
            cboTimeFiltro.SelectedValue = IIf(goUsuario.iTime > 0 AndAlso oTimes.Select("codigo = " & goUsuario.iTime).Length > 0, goUsuario.iTime, -1)
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

            'Comentários e chat de DEV: somente usuário interno (as procs também validam no banco)
            If goUsuario.bInterno = False Then
                tabDadosChamado.TabPages.Remove(pagComentarioDev)
                tabDadosChamado.TabPages.Remove(pagChatDev)
                btnInserirComentarioDev.Visible = False
            End If

            'Chat: o usuário autenticado é o remetente das mensagens enviadas
            oUsuarioChat = New ChatUser(goUsuario.lUsuario.ToString, goUsuario.sNome)
            chtChat.CurrentUser = oUsuarioChat
            chtChatDev.CurrentUser = oUsuarioChat

            Call ConfiguraGrid(grdListagem)
            Call ConfiguraGrid(grdArquivos)
            Call ConfiguraGrid(grdRelatorio)
            Call ConfiguraGrid(grdComentario)
            Call ConfiguraGrid(grdComentarioDev)
            Call ConfiguraGrid(grdChecklist)
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
            Call LoadGridChecklist()
            Call LoadGridComentario()
            If goUsuario.bInterno = True Then Call LoadGridComentarioDev()
            Call CarregarChat()

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
            grdChecklist.DataSource = Nothing
            txtChecklist.Text = ""
            Call LimparChat()

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
                Call CarregarChat()

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
                                           IIf(cboTipoAtendimentoFiltro.SelectedIndex = -1, -1, cboTipoAtendimentoFiltro.SelectedValue), _
                                           IIf(cboTimeFiltro.SelectedIndex = -1, -1, cboTimeFiltro.SelectedValue))

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
            Dim sDestino As String = System.IO.Path.Combine(PastaArquivosTemporarios(), System.IO.Path.GetFileName(sRelativo.Replace("/", "\")))

            oFtp.Download(sRelativo, sDestino)

            Cursor.Current = Cursors.Default

            Call AbrirArquivoLocal(sDestino)

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

#Region "::: CHECKLIST :::"

    Private Sub LoadGridChecklist()

        Try

            If IsNumeric(txtCodigo.Text) = False Then
                grdChecklist.DataSource = Nothing
                Exit Sub
            End If

            Call oClsUsrAdmChamado.LoadGridChecklist(grdChecklist, CLng(txtCodigo.Text))

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub AdicionarItemChecklist()

        Try

            If IsNumeric(txtCodigo.Text) = False Then
                frmMain.Informacao("Salve o chamado antes de montar o checklist.", gColorInformacaoErro)
                Exit Sub
            End If

            If txtChecklist.Text.Trim = "" Then
                txtChecklist.Focus()
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor

            oClsUsrAdmChamado.InsertChecklist(CLng(txtCodigo.Text), txtChecklist.Text.Trim)

            txtChecklist.Text = ""
            Call LoadGridChecklist()
            txtChecklist.Focus()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub ExcluirItemChecklist()

        Try

            With grdChecklist.CurrentRow.Cells

                If MsgBox("Deseja excluir o item '" & .Item("texto").Value.ToString & "' do checklist?", _
                          MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gAplicacao) = MsgBoxResult.No Then Exit Sub

                oClsUsrAdmChamado.DeleteChecklist(CLng(.Item("codigo").Value))

            End With

            Call LoadGridChecklist()

            frmMain.Informacao("Item excluído do checklist.", Color.Blue)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "::: CHAT :::"

    Public Function ChatVisivel(ByVal lChamado As Long, ByVal iCanal As Integer) As Boolean

        'O usuário já está vendo esta conversa: não precisa de notificação
        Return lChamadoChat = lChamado AndAlso tabMain.SelectedTab Is pagDados AndAlso _
               tabDadosChamado.SelectedTab Is IIf(iCanal = gcCanalChatDev, pagChatDev, pagChat)

    End Function

    Public Sub AbrirChat(ByVal lChamado As Long, ByVal iCanal As Integer)

        'Chamado vindo da notificação: filtra somente pelo código e abre direto na aba do chat
        Try

            Cursor.Current = Cursors.WaitCursor

            dtpDataInicioFiltro.Checked = False
            dtpDataTerminoFiltro.Checked = False
            txtCodigoFiltro.Text = lChamado.ToString
            txtTituloFiltro.Text = ""
            cboEmpresaFiltro.SelectedIndex = -1
            cboStatusFiltro.UncheckAll()
            cboResponsavelFiltro.SelectedIndex = -1
            cboTipoAtendimentoFiltro.SelectedIndex = -1
            cboTimeFiltro.SelectedValue = -1

            Call LoadGrid()
            Call MostrarLista()

            For r As Integer = 0 To grdListagem.RowCount - 1
                If CLng(grdListagem.GetRow(r).Cells.Item("codigo_atividade").Value) = lChamado Then

                    grdListagem.Row = r
                    Call Editar()

                    If iCanal = gcCanalChatDev AndAlso tabDadosChamado.TabPages.Contains(pagChatDev) Then
                        tabDadosChamado.SelectedTab = pagChatDev
                    Else
                        tabDadosChamado.SelectedTab = pagChat
                    End If

                    Call AtualizarLeituraChat()

                    Cursor.Current = Cursors.Default
                    Exit Sub

                End If
            Next

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Chamado " & lChamado & " não encontrado na listagem.", gColorInformacaoErro)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub LimparChat()

        Try

            lChamadoChat = 0
            oConversaChat = Nothing
            oConversaChatDev = Nothing
            chtChat.Conversation = Nothing
            chtChatDev.Conversation = Nothing
            pagChat.Text = "Chat"
            pagChatDev.Text = "Chat DEV"

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub CarregarChat()

        Try

            Call LimparChat()

            If IsNumeric(txtCodigo.Text) = False Then Exit Sub

            lChamadoChat = CLng(txtCodigo.Text)
            iLidaChat = 0
            iLidaChatDev = 0
            oUsuariosChat.Clear()

            'Conversa em grupo: o nome de quem enviou aparece acima dos balões recebidos
            oConversaChat = New ChatConversation("chat_" & lChamadoChat, "Chamado " & lChamadoChat, True)
            oConversaChat.Tag = gcCanalChatCliente
            Call AdicionarMensagensChat(oConversaChat, oClsUsrAdmChamado.LoadChat(lChamadoChat, gcCanalChatCliente, 0))
            chtChat.Conversation = oConversaChat

            If goUsuario.bInterno = True Then
                oConversaChatDev = New ChatConversation("chatdev_" & lChamadoChat, "Chamado " & lChamadoChat & " (interno)", True)
                oConversaChatDev.Tag = gcCanalChatDev
                Call AdicionarMensagensChat(oConversaChatDev, oClsUsrAdmChamado.LoadChat(lChamadoChat, gcCanalChatDev, 0))
                chtChatDev.Conversation = oConversaChatDev
            End If

            Call AtualizarLeituraChat()

            tmrChat.Start()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AtualizarChat()

        Try

            Call AdicionarMensagensChat(oConversaChat, oClsUsrAdmChamado.LoadChat(lChamadoChat, gcCanalChatCliente, UltimaMensagemChat(oConversaChat)))

            If Not IsNothing(oConversaChatDev) Then
                Call AdicionarMensagensChat(oConversaChatDev, oClsUsrAdmChamado.LoadChat(lChamadoChat, gcCanalChatDev, UltimaMensagemChat(oConversaChatDev)))
            End If

            Call AtualizarLeituraChat()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AdicionarMensagensChat(ByVal oConversa As ChatConversation, ByVal oTabela As DataTable)

        Try

            Dim oLista As New List(Of ChatMessage)

            For Each oRow As DataRow In oTabela.Rows

                'Mensagem já exibida (ex.: enviada por esta estação): não duplica
                If Not IsNothing(oConversa.Messages.FindById(oRow.Item("codigo_mensagem").ToString)) Then Continue For

                Dim lUsuario As Long = CLng(oRow.Item("codigo_usuario"))
                Dim bEnviada As Boolean = (lUsuario = goUsuario.lUsuario)

                Dim oMensagem As New ChatMessage(UsuarioChat(lUsuario, oRow.Item("nome_usuario").ToString), _
                                                 oRow.Item("mensagem").ToString, _
                                                 If(bEnviada, MessageDirection.Outgoing, MessageDirection.Incoming))

                oMensagem.Id = oRow.Item("codigo_mensagem").ToString
                oMensagem.Timestamp = CDate(oRow.Item("data_hora"))
                oMensagem.Status = If(bEnviada, MessageStatus.Sent, MessageStatus.None)

                If oRow.Item("nome_arquivo").ToString <> "" Then
                    Call DefinirAnexoChat(oMensagem, oRow.Item("nome_arquivo").ToString, CLng(oRow.Item("tamanho_arquivo")))
                End If

                oLista.Add(oMensagem)

            Next

            'Um único redesenho para todas as mensagens
            If oLista.Count > 0 Then oConversa.Messages.AddRange(oLista)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AtualizarLeituraChat()

        Try

            If lChamadoChat = 0 Then Exit Sub

            'Marca como lidas as mensagens da conversa que está na tela (somente quando há mensagem nova)
            If tabDadosChamado.SelectedTab Is pagChat AndAlso UltimaMensagemChat(oConversaChat) > iLidaChat Then
                iLidaChat = UltimaMensagemChat(oConversaChat)
                oClsUsrAdmChamado.UpdateChatLeitura(lChamadoChat, gcCanalChatCliente, iLidaChat)
            End If

            If tabDadosChamado.SelectedTab Is pagChatDev AndAlso UltimaMensagemChat(oConversaChatDev) > iLidaChatDev Then
                iLidaChatDev = UltimaMensagemChat(oConversaChatDev)
                oClsUsrAdmChamado.UpdateChatLeitura(lChamadoChat, gcCanalChatDev, iLidaChatDev)
            End If

            'Leitura dos outros usuários (✓✓ nas mensagens enviadas) e contador de não lidas nas abas
            For Each oRow As DataRow In oClsUsrAdmChamado.LoadChatLeitura(lChamadoChat).Rows

                Dim bCliente As Boolean = (CInt(oRow.Item("canal")) = gcCanalChatCliente)
                Dim oConversa As ChatConversation = If(bCliente, oConversaChat, oConversaChatDev)
                Dim iNaoLidas As Integer = CInt(oRow.Item("nao_lidas"))

                If IsNothing(oConversa) Then Continue For

                Call AtualizarStatusChat(oConversa, CInt(oRow.Item("lida_outros")))

                If bCliente Then
                    pagChat.Text = "Chat" & IIf(iNaoLidas > 0, " (" & iNaoLidas & ")", "")
                Else
                    pagChatDev.Text = "Chat DEV" & IIf(iNaoLidas > 0, " (" & iNaoLidas & ")", "")
                End If

            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AtualizarStatusChat(ByVal oConversa As ChatConversation, ByVal iLidaOutros As Integer)

        'Mensagens enviadas e gravadas: ✓ (enviada) ou ✓✓ azul (lida por outro usuário)
        For Each oMensagem As ChatMessage In oConversa.Messages

            Dim iCodigo As Integer

            If oMensagem.Direction <> MessageDirection.Outgoing OrElse Integer.TryParse(oMensagem.Id, iCodigo) = False Then Continue For

            Dim oStatus As MessageStatus = If(iCodigo <= iLidaOutros, MessageStatus.Read, MessageStatus.Sent)

            If oMensagem.Status <> oStatus Then oMensagem.Status = oStatus

        Next

    End Sub

    Private Function UltimaMensagemChat(ByVal oConversa As ChatConversation) As Integer

        'Maior código gravado no banco (mensagens com falha de envio ainda não têm código)
        Dim iUltima As Integer = 0

        If IsNothing(oConversa) Then Return 0

        For Each oMensagem As ChatMessage In oConversa.Messages

            Dim iCodigo As Integer

            If Integer.TryParse(oMensagem.Id, iCodigo) AndAlso iCodigo > iUltima Then iUltima = iCodigo

        Next

        Return iUltima

    End Function

    Private Function UsuarioChat(ByVal lUsuario As Long, ByVal sNome As String) As ChatUser

        If lUsuario = goUsuario.lUsuario Then Return oUsuarioChat

        If oUsuariosChat.ContainsKey(lUsuario) = False Then
            oUsuariosChat.Add(lUsuario, New ChatUser(lUsuario.ToString, sNome))
        End If

        Return oUsuariosChat(lUsuario)

    End Function

    Private Sub EnviarMensagemChat(ByVal oConversa As ChatConversation, ByVal oMensagem As ChatMessage)

        Dim sNomeArquivo As String = ""
        Dim bEnviouFtp As Boolean = False

        Try

            If IsNothing(oConversa) OrElse lChamadoChat = 0 Then Exit Sub

            Dim lTamanho As Long = -1

            If Not IsNothing(oMensagem.Attachment) Then

                Dim oAnexo As ChatAttachment = oMensagem.Attachment

                If FtpDisponivel() = False Then Throw New ArgumentException("FTP dos anexos não configurado no Config.xml.")

                Dim oInfo As New System.IO.FileInfo(oAnexo.FilePath)

                If oInfo.Length > gcTamanhoMaximoAnexo Then
                    Throw New ArgumentException("O arquivo '" & oInfo.Name & "' excede o limite de " & (gcTamanhoMaximoAnexo / 1048576) & " MB.")
                End If

                lTamanho = oInfo.Length

                If IsNothing(oAnexo.Tag) Then

                    Cursor.Current = Cursors.WaitCursor

                    sNomeArquivo = NomeLivreFtp(oInfo.Name)
                    oFtp.Upload(sNomeArquivo, oInfo.FullName)
                    bEnviouFtp = True

                    'Cópia local com o nome do servidor: a imagem continua visível ao recarregar o chamado
                    Dim sCache As String = System.IO.Path.Combine(PastaArquivosTemporarios(), sNomeArquivo)
                    If String.Equals(oInfo.FullName, sCache, StringComparison.OrdinalIgnoreCase) = False Then System.IO.File.Copy(oInfo.FullName, sCache, True)

                Else
                    'Reenvio: o arquivo já está no servidor, falta somente gravar a mensagem
                    sNomeArquivo = oAnexo.Tag.ToString
                End If

            End If

            Dim dDataHora As DateTime
            Dim iCodigo As Integer = oClsUsrAdmChamado.InsertChat(lChamadoChat, CInt(oConversa.Tag), oMensagem.Text, sNomeArquivo, lTamanho, dDataHora)

            If Not IsNothing(oMensagem.Attachment) Then oMensagem.Attachment.Tag = sNomeArquivo
            oMensagem.Id = iCodigo.ToString
            oMensagem.Timestamp = dDataHora
            oMensagem.Status = MessageStatus.Sent

            'Quem envia já leu a conversa até a própria mensagem
            If CInt(oConversa.Tag) = gcCanalChatCliente Then iLidaChat = iCodigo Else iLidaChatDev = iCodigo

            Cursor.Current = Cursors.Default

        Catch ex As Exception

            'Falha ao gravar no banco: remove somente o arquivo que acabou de ser enviado
            If bEnviouFtp Then
                Try
                    oFtp.Delete(sNomeArquivo)
                Catch
                End Try
            End If

            oMensagem.Status = MessageStatus.Failed

            If TypeOf ex Is ArgumentException Then
                Cursor.Current = Cursors.Default
                frmMain.Informacao(ex.Message, gColorInformacaoErro)
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, gAplicacao)
            Else
                Call TratarErro(ex)
            End If

        End Try

    End Sub

    Private Function NomeLivreFtp(ByVal sNome As String) As String

        'Mesmo padrão dos anexos: somente o nome do arquivo, com sufixo se o nome já existir no servidor
        Try

            Dim sNomeSeguro As String = NomeArquivoSeguro(sNome)
            Dim sNomeLivre As String = sNomeSeguro
            Dim iSufixo As Integer = 1

            Do While oFtp.Existe(sNomeLivre)
                sNomeLivre = System.IO.Path.GetFileNameWithoutExtension(sNomeSeguro) & "_" & iSufixo & System.IO.Path.GetExtension(sNomeSeguro)
                iSufixo += 1
            Loop

            Return sNomeLivre

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub DefinirAnexoChat(ByVal oMensagem As ChatMessage, ByVal sNomeArquivo As String, ByVal lTamanho As Long)

        Try

            'O arquivo só é baixado do FTP ao clicar; se já estiver na pasta temporária, a imagem é exibida no balão
            Dim sLocal As String = System.IO.Path.Combine(PastaArquivosTemporarios(), System.IO.Path.GetFileName(sNomeArquivo))

            oMensagem.Attachment = New ChatAttachment(sNomeArquivo, IIf(System.IO.File.Exists(sLocal), sLocal, Nothing), lTamanho)
            oMensagem.Attachment.Tag = sNomeArquivo

            If System.IO.File.Exists(sLocal) Then oMensagem.Image = CarregarImagemChat(sLocal)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function CarregarImagemChat(ByVal sCaminho As String) As Image

        'Somente imagens; o arquivo não fica bloqueado (cópia em memória)
        If System.IO.Path.GetExtension(sCaminho) = "" OrElse ".png.jpg.jpeg.gif.bmp".Contains(System.IO.Path.GetExtension(sCaminho).ToLower) = False Then Return Nothing

        Try
            Using oStream As New System.IO.MemoryStream(System.IO.File.ReadAllBytes(sCaminho)), _
                  oImagem As Image = Image.FromStream(oStream)
                Return New Bitmap(oImagem)
            End Using
        Catch
            'Arquivo de imagem inválido: fica como anexo
            Return Nothing
        End Try

    End Function

    Private Sub AbrirAnexoChat(ByVal oAnexo As ChatAttachment)

        Try

            If IsNothing(oAnexo) Then Exit Sub

            'Arquivo ainda não baixado nesta estação: busca no FTP
            If String.IsNullOrEmpty(oAnexo.FilePath) OrElse System.IO.File.Exists(oAnexo.FilePath) = False Then

                If IsNothing(oAnexo.Tag) OrElse FtpDisponivel() = False Then Exit Sub

                Cursor.Current = Cursors.WaitCursor

                Dim sDestino As String = System.IO.Path.Combine(PastaArquivosTemporarios(), System.IO.Path.GetFileName(oAnexo.Tag.ToString))

                oFtp.Download(oAnexo.Tag.ToString, sDestino)
                oAnexo.FilePath = sDestino

                Cursor.Current = Cursors.Default

            End If

            Call AbrirArquivoLocal(oAnexo.FilePath)

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

            'Apontamentos anulados ficam só como histórico: não entram no total
            For Each oRow As GridEXRow In grdApontamentoHistorico.GetRows
                If oRow.RowType = RowType.Record AndAlso IsDBNull(oRow.Cells("duracao_segundos").Value) = False _
                   AndAlso ApontamentoAnulado(oRow) = False Then
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

    Private Function ApontamentoAnulado(ByVal oRow As GridEXRow) As Boolean

        Dim oValor As Object = oRow.Cells("anulado").Value

        Return IsDBNull(oValor) = False AndAlso IsNothing(oValor) = False AndAlso CLng(oValor) = 1

    End Function

    Private Sub AtualizarSituacaoApontamento(ByVal oRow As GridEXRow)

        Try

            If IsNothing(oRow) OrElse oRow.RowType <> RowType.Record Then Exit Sub

            Dim bAnulado As Boolean = ApontamentoAnulado(oRow)

            If bAnulado AndAlso MsgBox("Deseja anular este apontamento?" & vbCrLf & vbCrLf & _
                                       "Ele continua no histórico, mas deixa de contar nas horas (totais, relatório e horas do chamado).", _
                                       MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gAplicacao) = MsgBoxResult.No Then
                Me.BeginInvoke(New MethodInvoker(AddressOf RecarregarHistorico))
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor

            Call oClsUsrAdmChamado.UpdateApontamentoSituacao(oRow.Cells("codigo_empresa").Value, _
                                                             oRow.Cells("codigo_usuario").Value, _
                                                             oRow.Cells("codigo_chamado").Value, _
                                                             oRow.Cells("codigo_apontamento").Value, _
                                                             bAnulado)

            Cursor.Current = Cursors.Default

            frmMain.Informacao(IIf(bAnulado, "Apontamento anulado: não conta mais nas horas.", "Apontamento voltou a contar nas horas."), Color.Blue)

            'Recarrega após o evento para atualizar horas, totais e formatação da linha
            Me.BeginInvoke(New MethodInvoker(AddressOf RecarregarHistorico))

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
