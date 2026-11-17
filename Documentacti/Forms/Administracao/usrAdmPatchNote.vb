Imports Janus.Windows.GridEX
Imports Janus.Windows.EditControls
Imports MySqlConnector

Public Class usrAdmPatchNote

#Region "::: VARIAVEIS :::"

    Private oClsUsrAdmPatchNote As New clsUsrAdmPatchNote

    'Código da nota em edição (0 = nova)
    Private lCodigoNota As Long = 0

    'Durante o bind a grid troca de seleção antes de ter os dados (DataSource antes do DataMember)
    Private bCarregando As Boolean = False

#End Region

#Region "::: CONTROLES :::"

#Region "::: GERAL :::"

    Private Sub usrAdmPatchNote_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

    Private Sub btnNovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNovo.Click, btnNovo1.Click

        Try

            Call Novo()

            Call MostrarDados()

            txtVersao.Focus()

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
            Call TratamentoErro(ex.Message, "Patch Notes")
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

    Private Sub grdListagem_SelectionChanged(sender As Object, e As EventArgs) Handles grdListagem.SelectionChanged

        Try

            If bCarregando Then Exit Sub

            'A nota selecionada aparece formatada ao lado da lista
            Call MostrarNota()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdListagem_RowDoubleClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdListagem.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            If IsNothing(grdListagem.CurrentColumn) Then Exit Sub
            If grdListagem.CurrentColumn.Key = "editar" AndAlso goUsuario.bSuperAdm Then Call Editar()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub txtDescricao_TextChanged(sender As Object, e As EventArgs) Handles txtDescricao.TextChanged

        Try

            'Pré-visualização enquanto edita
            wbPreview.DocumentText = MarkdownParaHtml(txtDescricao.Text)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub usrAdmPatchNote_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

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
            Call AddKeyDown(Me, New DelegateKeyDown(AddressOf usrAdmPatchNote_KeyDown))
            Call AddLostFocus(Me, New DelegateLostFocus(AddressOf Control_LostFocus))
            Call AddComboAutoComplete(New UIComboBox() {cboAtivoFiltro})

            Call LoadComboLista(cboAtivoFiltro, "-1=TODOS;1=SIM;0=NÃO")
            cboAtivoFiltro.SelectedValue = "1"

            tabMain.TabPages.Remove(pagDados)

            Call ConfiguraGrid(grdListagem)

            'Publicar/editar notas: somente super administrador (a proc também valida)
            btnNovo.Visible = goUsuario.bSuperAdm
            btnNovo1.Visible = goUsuario.bSuperAdm
            grdListagem.RootTable.Columns("editar").Visible = goUsuario.bSuperAdm
            grdListagem.RootTable.Columns("ativo").Visible = goUsuario.bSuperAdm
            lblAtivoFiltro.Visible = goUsuario.bSuperAdm
            cboAtivoFiltro.Visible = goUsuario.bSuperAdm

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
            Call TratamentoErro(ex, "Patch Notes")
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

            bCarregando = True

            'Sem acesso de edição: somente notas ativas
            Call oClsUsrAdmPatchNote.LoadGrid(grdListagem, _
                                              txtTituloFiltro.Text.Trim, _
                                              IIf(goUsuario.bSuperAdm, CInt(cboAtivoFiltro.SelectedValue), 1))

            'Mais recente selecionada (a lista vem da mais nova para a mais antiga)
            If grdListagem.RowCount > 0 Then grdListagem.Row = 0

            bCarregando = False

            Call MostrarNota()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        Finally
            bCarregando = False
        End Try

    End Sub

    Private Sub MostrarNota()

        Try

            'Sem linha de dados (lista vazia ou grid ainda sem bind): mensagem em vez da nota
            If IsNothing(grdListagem.CurrentRow) OrElse grdListagem.CurrentRow.RowType <> RowType.Record _
               OrElse IsNothing(grdListagem.CurrentRow.Cells("versao").Value) OrElse IsDBNull(grdListagem.CurrentRow.Cells("versao").Value) Then
                wbNota.DocumentText = MarkdownParaHtml("Nenhuma nota encontrada.")
                Exit Sub
            End If

            With grdListagem.CurrentRow.Cells
                Dim oData As Object = .Item("data_publicacao").Value
                wbNota.DocumentText = MarkdownParaHtml(Convert.ToString(.Item("descricao").Value) & vbLf & vbLf & _
                                                       "*Versão " & Convert.ToString(.Item("versao").Value) & _
                                                       IIf(IsDate(oData), " - publicada em " & Format(CDate(IIf(IsDate(oData), oData, Date.Today)), "dd/MM/yyyy"), "").ToString & "*")
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Novo()

        Try

            frmMain.errInfo.Clear()

            lCodigoNota = 0
            txtVersao.Text = Application.ProductVersion.ToString
            dtpDataPublicacao.Value = Date.Today
            txtTitulo.Text = ""
            txtDescricao.Text = "# Título" & vbCrLf & vbCrLf & "## Novidades" & vbCrLf & "- " & vbCrLf & vbCrLf & "## Melhorias" & vbCrLf & "- "
            chkAtivo.Checked = True

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Editar()

        Try

            frmMain.errInfo.Clear()

            With grdListagem.CurrentRow.Cells

                lCodigoNota = CLng(.Item("codigo").Value)
                txtVersao.Text = .Item("versao").Value.ToString
                dtpDataPublicacao.Value = CDate(.Item("data_publicacao").Value)
                txtTitulo.Text = .Item("titulo").Value.ToString
                txtDescricao.Text = .Item("descricao").Value.ToString.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
                chkAtivo.Checked = (IsDBNull(.Item("ativo").Value) = False AndAlso CInt(.Item("ativo").Value) = 1)

            End With

            Call MostrarDados()

            txtDescricao.Focus()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Salvar()

        Try

            Cursor.Current = Cursors.WaitCursor

            If lCodigoNota > 0 Then
                oClsUsrAdmPatchNote.Update(lCodigoNota, txtVersao.Text.Trim, dtpDataPublicacao.Value, txtTitulo.Text.Trim, txtDescricao.Text.Trim, chkAtivo.Checked)
            Else
                lCodigoNota = oClsUsrAdmPatchNote.Insert(txtVersao.Text.Trim, dtpDataPublicacao.Value, txtTitulo.Text.Trim, txtDescricao.Text.Trim, chkAtivo.Checked)
            End If

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Patch note da versão " & txtVersao.Text.Trim & " salva com sucesso!", Color.Blue)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function Validacao() As Boolean

        Try

            Validacao = False

            If ValidaCampo(txtVersao, lblVersao, TipoCampo.texto) = False Then Exit Function
            If ValidaCampo(txtTitulo, lblTitulo, TipoCampo.texto) = False Then Exit Function

            If txtDescricao.Text.Trim = "" Then
                frmMain.errInfo.SetError(txtDescricao, "Informe a descrição das mudanças.")
                txtDescricao.Focus()
                Exit Function
            End If

            Validacao = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#End Region

End Class
