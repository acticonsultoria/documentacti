Imports Janus.Windows.GridEX
Imports Janus.Windows.EditControls
Imports MySqlConnector

Public Class usrRelApontamento

#Region "::: VARIAVEIS :::"

    Private oClsUsrRelApontamento As New clsUsrRelApontamento

    'Apontamentos atuais: tabela ligada à grid (o tempo decorrido é recalculado pelo Timer)
    Private oTabelaAtual As DataTable
    'Diferença entre o relógio do banco e o da estação (o tempo decorrido usa a hora do banco)
    Private oDiferencaServidor As TimeSpan = TimeSpan.Zero
    Private iContadorSincronizacao As Integer = 0
    Private bCarregando As Boolean = False

    Private Const gcIntervaloSincronizacao As Integer = 30 'segundos

#End Region

#Region "::: CONTROLES :::"

#Region "::: GERAL :::"

    Private Sub usrRelApontamento_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Call ConfigurarUserControl()

    End Sub

    Private Sub btnSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSairAtual.Click, btnSairHistorico.Click

        Try

            tmrAtual.Stop()

            If TypeOf Me.Parent Is Form Then Me.Parent.Dispose() Else frmMain.tabMain.TabPages.Remove(Me.Parent)
            Me.Dispose()

        Catch ex As Exception
            Call TratamentoErro(ex.Message, "Relatório de Apontamentos")
        End Try

    End Sub

    Private Sub btnAgrupar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgruparAtual.Click, btnAgruparHistorico.Click

        Try

            Dim oGrid As GridEX = IIf(sender Is btnAgruparAtual, grdAtual, grdHistorico)

            'Exibe/oculta a área de agrupamento (arrastar o cabeçalho da coluna para agrupar)
            If oGrid.GroupByBoxVisible = True Then
                oGrid.GroupByBoxVisible = False
            Else
                oGrid.GroupByBoxVisible = True
                oGrid.HideColumnsWhenGrouped = InheritableBoolean.True
            End If

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub usrRelApontamento_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        Try

            Select Case e.KeyCode

                Case Keys.Enter
                    If InStr(sender.name, "Historico", CompareMethod.Text) > 0 Then
                        Call LoadGridHistorico()
                    End If

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: APONTAMENTOS ATUAIS :::"

    Private Sub btnAtualizarAtual_Click(sender As Object, e As EventArgs) Handles btnAtualizarAtual.Click

        Try

            Call LoadGridAtual()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub cboTimeAtual_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTimeAtual.SelectedIndexChanged

        Try

            If bCarregando Then Exit Sub

            Call LoadGridAtual()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnExcelAtual_Click(sender As Object, e As EventArgs) Handles btnExcelAtual.Click

        Try

            Call ExportExcel(grdAtual)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub tmrAtual_Tick(sender As Object, e As EventArgs) Handles tmrAtual.Tick

        Try

            iContadorSincronizacao += 1

            'Busca novamente no banco periodicamente (apontamentos iniciados/encerrados por outros usuários)
            If iContadorSincronizacao >= gcIntervaloSincronizacao Then
                iContadorSincronizacao = 0
                Call LoadGridAtual()
            Else
                Call AtualizarTempoDecorrido()
            End If

        Catch ex As Exception
            'Falha de conexão na sincronização: interrompe o Timer e informa o usuário
            tmrAtual.Stop()
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: HISTÓRICO :::"

    Private Sub btnFiltrarHistorico_Click(sender As Object, e As EventArgs) Handles btnFiltrarHistorico.Click

        Try

            Call LoadGridHistorico()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnExcelHistorico_Click(sender As Object, e As EventArgs) Handles btnExcelHistorico.Click

        Try

            Call ExportExcel(grdHistorico)

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
            Call AddKeyDown(Me, New DelegateKeyDown(AddressOf usrRelApontamento_KeyDown))
            Call AddLostFocus(Me, New DelegateLostFocus(AddressOf Control_LostFocus))

            'Time: começa no time do usuário logado; sem time, traz todos
            Call LoadComboTime(cboTimeAtual)
            Call LoadComboTime(cboTimeHistorico)

            Call LoadCombo(cboUsuarioHistorico, "sp_select_combo_administracao_chamado_usuario")
            Call LoadCombo(cboEmpresaHistorico, "sp_select_combo_administracao_chamado_empresa")
            Call AdicionarOpcaoTodos(cboEmpresaHistorico, "TODAS")
            Call LoadCombo(cboTipoAtendimentoHistorico, "sp_select_combo_administracao_chamado_tipo_atendimento")
            Call AdicionarOpcaoTodos(cboTipoAtendimentoHistorico, "TODOS")
            Call LoadComboLista(cboSituacaoHistorico, "-1=TODAS;0=EM ANDAMENTO;1=NORMAL;2=ANULADO")

            dtpInicioHistorico.Value = New Date(Now.Year, Now.Month, 1) : dtpInicioHistorico.Checked = True
            dtpTerminoHistorico.Value = Now.Date : dtpTerminoHistorico.Checked = True

            Call ConfiguraGrid(grdAtual)
            Call ConfiguraGrid(grdHistorico)

            bCarregando = False

            Call LoadGridAtual()
            Call LoadGridHistorico()

            tmrAtual.Start()

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
            Call TratamentoErro(ex, "Relatório de Apontamentos")
        End If

    End Sub

    Private Sub LoadComboTime(ByVal oCombo As UIComboBox)

        Try

            Call LoadCombo(oCombo, "sp_select_combo_tipo_time_empresa")

            Dim oTimes As DataTable = CType(oCombo.DataSource, DataTable)

            Dim oSemTime As DataRow = oTimes.NewRow
            oSemTime.Item("codigo") = 0
            oSemTime.Item("descricao") = "SEM TIME"
            oTimes.Rows.InsertAt(oSemTime, 0)

            Dim oTodos As DataRow = oTimes.NewRow
            oTodos.Item("codigo") = -1
            oTodos.Item("descricao") = "TODOS"
            oTimes.Rows.InsertAt(oTodos, 0)

            'Seleção pela posição: o código vem do banco como Int16 e não casa com um Integer no SelectedValue
            Dim oTimeUsuario() As DataRow = oTimes.Select("codigo = " & goUsuario.iTime)
            oCombo.SelectedIndex = If(goUsuario.iTime > 0 AndAlso oTimeUsuario.Length > 0, oTimes.Rows.IndexOf(oTimeUsuario(0)), 0)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AdicionarOpcaoTodos(ByVal oCombo As UIComboBox, ByVal sDescricao As String)

        Try

            Dim oTabela As DataTable = CType(oCombo.DataSource, DataTable)
            Dim oTodos As DataRow = oTabela.NewRow
            oTodos.Item("codigo") = -1
            oTodos.Item("descricao") = sDescricao
            oTabela.Rows.InsertAt(oTodos, 0)

            oCombo.SelectedIndex = 0

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function ValorCombo(ByVal oCombo As UIComboBox) As Integer

        If oCombo.SelectedIndex = -1 OrElse IsDBNull(oCombo.SelectedValue) Then Return -1

        Return CInt(oCombo.SelectedValue)

    End Function

#End Region

#Region "::: APONTAMENTOS ATUAIS :::"

    Private Sub LoadGridAtual()

        Try

            Cursor.Current = Cursors.WaitCursor

            oTabelaAtual = oClsUsrRelApontamento.LoadGridAtual(grdAtual, ValorCombo(cboTimeAtual))

            'Diferença entre o relógio do banco e o da estação
            If oTabelaAtual.Rows.Count > 0 Then
                oDiferencaServidor = CDate(oTabelaAtual.Rows(0).Item("data_hora_servidor")).Subtract(Date.Now)
            End If

            iContadorSincronizacao = 0

            Call AtualizarTempoDecorrido()

            Dim iEmApontamento As Integer = oTabelaAtual.Select("em_apontamento = 1").Length
            Dim iUsuarios As Integer = oTabelaAtual.DefaultView.ToTable(True, "codigo_usuario").Rows.Count
            Dim iSemApontamento As Integer = oTabelaAtual.Select("em_apontamento = 0").Length

            lblResumoAtual.Text = iUsuarios & " usuário(s) | " & iEmApontamento & " apontamento(s) em andamento | " & _
                                  iSemApontamento & " sem apontamento | atualizado às " & Format(Date.Now.Add(oDiferencaServidor), "HH:mm:ss")

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AtualizarTempoDecorrido()

        Try

            If IsNothing(oTabelaAtual) Then Exit Sub

            'Tempo decorrido provisório: hora atual do banco (estimada) - início do apontamento
            Dim dAgora As Date = Date.Now.Add(oDiferencaServidor)

            For Each oDataRow As DataRow In oTabelaAtual.Rows
                If IsDBNull(oDataRow.Item("data_hora_inicio")) Then Continue For
                Dim sDuracao As String = FormatarDuracao(CLng(Math.Floor(dAgora.Subtract(CDate(oDataRow.Item("data_hora_inicio"))).TotalSeconds)))
                If oDataRow.Item("duracao").ToString <> sDuracao Then oDataRow.Item("duracao") = sDuracao
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "::: HISTÓRICO :::"

    Private Sub LoadGridHistorico()

        Try

            Dim lChamado As Long = -1

            If txtChamadoHistorico.Text.Trim <> "" Then
                If IsNumeric(txtChamadoHistorico.Text.Trim) = False Then
                    frmMain.Informacao("Informe somente números no filtro de chamado.", gColorInformacaoErro)
                    txtChamadoHistorico.Focus()
                    Exit Sub
                End If
                lChamado = CLng(txtChamadoHistorico.Text.Trim)
            End If

            Cursor.Current = Cursors.WaitCursor

            Dim oTabela As DataTable = oClsUsrRelApontamento.LoadGridHistorico(grdHistorico, _
                                                                               ValorCombo(cboTimeHistorico), _
                                                                               GetCheckedValues(cboUsuarioHistorico), _
                                                                               lChamado, _
                                                                               ValorCombo(cboEmpresaHistorico), _
                                                                               ValorCombo(cboTipoAtendimentoHistorico), _
                                                                               CInt(cboSituacaoHistorico.SelectedValue), _
                                                                               IIf(dtpInicioHistorico.Checked = False, Nothing, dtpInicioHistorico.Value.Date), _
                                                                               IIf(dtpTerminoHistorico.Checked = False, Nothing, dtpTerminoHistorico.Value.Date))

            'Total no período: anulados e em andamento não contam nas horas
            Dim lTotal As Long = 0
            For Each oDataRow As DataRow In oTabela.Rows
                If oDataRow.Item("situacao").ToString = "NORMAL" AndAlso IsDBNull(oDataRow.Item("duracao_segundos")) = False Then
                    lTotal += CLng(oDataRow.Item("duracao_segundos"))
                End If
            Next

            lblTotalHistorico.Text = oTabela.Rows.Count & " apontamento(s) | Total: " & FormatarDuracao(lTotal) & " (" & Format(lTotal / 3600, "N2") & " h)"

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#End Region

End Class
