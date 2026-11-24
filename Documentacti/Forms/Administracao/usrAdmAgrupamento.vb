Imports Janus.Windows.GridEX
Imports Janus.Windows.EditControls
Imports MySqlConnector

Public Class usrAdmAgrupamento

#Region "::: VARIAVEIS :::"

    Private oClsUsrAdmAgrupamento As New clsUsrAdmAgrupamento

    'Código do agrupamento em edição (0 = novo)
    Private lCodigoAgrupamento As Long = 0

#End Region

#Region "::: CONTROLES :::"

#Region "::: GERAL :::"

    Private Sub usrAdmAgrupamento_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Call ConfigurarUserControl()

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Try

            Call ExportExcel(grdListagem)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnAgrupar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgruparListagem.Click, btnAgruparChamados.Click

        Try

            Call AlternarAgrupamentoGrid(IIf(sender Is btnAgruparChamados, grdChamados, grdListagem))

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

    Private Sub btnNovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNovo.Click, btnNovo1.Click

        Try

            Call Novo()

            Call MostrarDados()

            txtTitulo.Focus()

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

    Private Sub btnSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSair.Click, btnSair2.Click

        Try

            If TypeOf Me.Parent Is Form Then Me.Parent.Dispose() Else frmMain.tabMain.TabPages.Remove(Me.Parent)
            Me.Dispose()

        Catch ex As Exception
            Call TratamentoErro(ex.Message, "Agrupamentos")
        End Try

    End Sub

    Private Sub btnVoltar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoltar.Click

        Try

            frmMain.errInfo.Clear()

            Call LoadGrid()

            Call MostrarLista()

            txtTituloFiltro.Focus()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnAdicionarChamado_Click(sender As Object, e As EventArgs) Handles btnAdicionarChamado.Click

        Try

            Call AdicionarChamado()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub txtMotivo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMotivo.KeyDown

        Try

            'Enter no motivo inclui o chamado (mesmo comportamento do botão Adicionar)
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                Call AdicionarChamado()
            End If

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdListagem_RowDoubleClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdListagem.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            If IsNothing(grdListagem.CurrentColumn) Then Exit Sub
            If grdListagem.CurrentColumn.Key = "editar" Then Call Editar(CLng(grdListagem.CurrentRow.Cells("codigo").Value))

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdChamados_RowDoubleClick(sender As Object, e As RowActionEventArgs) Handles grdChamados.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            If IsNothing(grdChamados.CurrentColumn) Then Exit Sub
            Select Case grdChamados.CurrentColumn.Key

                Case "excluir" : Call RemoverChamado()

                Case "visualizar_chamado" : Call frmMain.AbrirChamado(CLng(grdChamados.CurrentRow.Cells("codigo_atividade").Value))

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub usrAdmAgrupamento_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        Try

            Select Case e.KeyCode

                Case Keys.Enter
                    If InStr(sender.name, "Filtro", CompareMethod.Text) > 0 Then
                        Call LoadGrid()
                    End If

            End Select

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

            Call AddGotFocus(Me, New DelegateGotFocus(AddressOf Control_GotFocus))
            Call AddKeyDown(Me, New DelegateKeyDown(AddressOf usrAdmAgrupamento_KeyDown))
            Call AddLostFocus(Me, New DelegateLostFocus(AddressOf Control_LostFocus))
            Call AddComboAutoComplete(New UIComboBox() {cboAtivoFiltro})

            Call LoadComboLista(cboAtivoFiltro, "-1=TODOS;1=SIM;0=NÃO")
            cboAtivoFiltro.SelectedValue = "1"

            tabMain.TabPages.Remove(pagDados)

            Call ConfiguraGrid(grdListagem)
            Call ConfiguraGrid(grdChamados)
            grdChamados.RootTable.Columns("excluir").CellToolTipText = "Duplo clique para remover o chamado do agrupamento."

            Call LoadGrid()

            txtTituloFiltro.Focus()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
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
            Call TratamentoErro(ex, "Agrupamentos")
        End If

    End Sub

    Private Sub MostrarDados()

        tabMain.TabPages.Remove(pagLista)
        If tabMain.TabPages.Contains(pagDados) = False Then tabMain.TabPages.Insert(0, pagDados)
        tabMain.SelectedTab = pagDados

    End Sub

    Private Sub MostrarLista()

        tabMain.TabPages.Remove(pagDados)
        If tabMain.TabPages.Contains(pagLista) = False Then tabMain.TabPages.Insert(0, pagLista)
        tabMain.SelectedTab = pagLista

    End Sub

    Private Sub LoadGrid()

        Try

            Cursor.Current = Cursors.WaitCursor

            Call oClsUsrAdmAgrupamento.LoadGrid(grdListagem, _
                                                txtTituloFiltro.Text.Trim, _
                                                CInt(cboAtivoFiltro.SelectedValue))

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Novo()

        Try

            frmMain.errInfo.Clear()

            lCodigoAgrupamento = 0
            txtCodigo.Text = ""
            txtTitulo.Text = ""
            chkAtivo.Checked = True

            'Chamados só podem ser incluídos depois de salvar o agrupamento
            Call HabilitarChamados()
            Call LoadGridChamados()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Editar(ByVal lCodigo As Long)

        Try

            Cursor.Current = Cursors.WaitCursor

            frmMain.errInfo.Clear()

            With grdListagem.CurrentRow.Cells

                lCodigoAgrupamento = lCodigo
                txtCodigo.Text = .Item("codigo_agrupamento").Value.ToString
                txtTitulo.Text = .Item("titulo").Value.ToString
                chkAtivo.Checked = (IsDBNull(.Item("ativo").Value) = False AndAlso CInt(.Item("ativo").Value) = 1)

            End With

            Call HabilitarChamados()
            Call LoadGridChamados()

            Call MostrarDados()

            txtChamado.Focus()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Salvar()

        Try

            Cursor.Current = Cursors.WaitCursor

            If lCodigoAgrupamento > 0 Then
                oClsUsrAdmAgrupamento.Update(lCodigoAgrupamento, txtTitulo.Text.Trim, chkAtivo.Checked)
            Else
                lCodigoAgrupamento = oClsUsrAdmAgrupamento.Insert(txtTitulo.Text.Trim)
                txtCodigo.Text = "AG-" & lCodigoAgrupamento
            End If

            Call HabilitarChamados()

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Agrupamento " & txtCodigo.Text & " salvo com sucesso!", Color.Blue)

            If txtChamado.Enabled Then txtChamado.Focus()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function Validacao() As Boolean

        Try

            Validacao = False

            If ValidaCampo(txtTitulo, lblTitulo, TipoCampo.texto) = False Then Exit Function

            Validacao = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "::: CHAMADOS :::"

    Private Sub HabilitarChamados()

        'Inclusão de chamados: somente com o agrupamento salvo e ativo
        Dim bHabilitar As Boolean = (lCodigoAgrupamento > 0 AndAlso chkAtivo.Checked)

        txtChamado.Enabled = bHabilitar
        txtMotivo.Enabled = bHabilitar
        btnAdicionarChamado.Enabled = bHabilitar

    End Sub

    Private Sub LoadGridChamados()

        Try

            Dim iQuantidade As Integer = oClsUsrAdmAgrupamento.LoadGridChamado(grdChamados, IIf(lCodigoAgrupamento > 0, lCodigoAgrupamento, 0), -1)

            lblQuantidadeChamados.Text = iQuantidade & " chamado(s)"

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AdicionarChamado()

        Try

            If lCodigoAgrupamento = 0 Then Exit Sub

            frmMain.errInfo.Clear()

            If IsNumeric(txtChamado.Text.Trim) = False Then
                frmMain.errInfo.SetError(txtChamado, "Informe o número do chamado.")
                txtChamado.Focus()
                Exit Sub
            End If

            If txtMotivo.Text.Trim = "" Then
                frmMain.errInfo.SetError(txtMotivo, "Informe o motivo da inclusão do chamado no agrupamento.")
                txtMotivo.Focus()
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor

            Call oClsUsrAdmAgrupamento.InsertChamado(lCodigoAgrupamento, CLng(txtChamado.Text.Trim), txtMotivo.Text.Trim)

            Dim sChamado As String = txtChamado.Text.Trim
            txtChamado.Text = ""
            txtMotivo.Text = ""

            Call LoadGridChamados()

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Chamado " & sChamado & " incluído no agrupamento " & txtCodigo.Text & "!", Color.Blue)

            txtChamado.Focus()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub RemoverChamado()

        Try

            Dim lChamado As Long = CLng(grdChamados.CurrentRow.Cells("codigo_atividade").Value)

            If MsgBox("Deseja remover o chamado " & lChamado & " do agrupamento " & txtCodigo.Text & "?" & vbCrLf & vbCrLf & _
                      "O chamado não é alterado; somente o vínculo com o agrupamento é removido.", _
                      MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gAplicacao) = MsgBoxResult.No Then Exit Sub

            Cursor.Current = Cursors.WaitCursor

            Call oClsUsrAdmAgrupamento.DeleteChamado(lCodigoAgrupamento, lChamado)

            Call LoadGridChamados()

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Chamado " & lChamado & " removido do agrupamento " & txtCodigo.Text & ".", Color.Blue)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#End Region

End Class
