Imports Janus.Windows.GridEX
Imports Janus.Windows.EditControls
Imports MySql.Data.MySqlClient

Public Class usrCadUsuario

#Region "::: VARIAVEIS :::"

    Private oClsUsrCadUsuario As New clsUsrCadUsuario

    'Código do usuário em edição (0 = novo; a PK não aparece na tela)
    Private lCodigoUsuario As Long = 0

    'Senha atual no formato da plataforma web: regravada quando a senha não é alterada
    Private sSenhaWebAtual As String = ""

#End Region

#Region "::: CONTROLES :::"

#Region "::: GERAL :::"

    Private Sub usrCadUsuario_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

            txtUsuario.Focus()

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
            Call TratamentoErro(ex.Message, "Usuários")
        End Try

    End Sub

    Private Sub btnVoltar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoltar.Click

        Try

            frmMain.errInfo.Clear()

            Call LoadGrid()

            Call MostrarLista()

            txtUsuarioFiltro.Focus()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdListagem_RowDoubleClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdListagem.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            If grdListagem.CurrentColumn.Key = "editar" Then Call Editar()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub usrCadUsuario_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

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
            Call AddKeyDown(Me, New DelegateKeyDown(AddressOf usrCadUsuario_KeyDown))
            Call AddLostFocus(Me, New DelegateLostFocus(AddressOf Control_LostFocus))

            'Combos
            Call LoadComboLista(cboAtivoFiltro, "-1=TODOS;1=SIM;0=NÃO")
            Call LoadCombo(cboEmpresa, "sp_select_combo_administracao_chamado_empresa")

            tabMain.TabPages.Remove(pagDados)

            Call ConfiguraGrid(grdListagem)

            Call LoadGrid()

            txtUsuarioFiltro.Focus()

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
            Call TratamentoErro(ex, "Usuários")
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

            Call oClsUsrCadUsuario.LoadGrid(grdListagem, _
                                            txtUsuarioFiltro.Text.Trim, _
                                            txtNomeFiltro.Text.Trim, _
                                            CInt(cboAtivoFiltro.SelectedValue))

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Novo()

        Try

            frmMain.errInfo.Clear()

            lCodigoUsuario = 0
            txtUsuario.Text = ""
            txtNome.Text = ""
            cboEmpresa.SelectedValue = goUsuario.iEmpresa
            chkAdmin.Checked = False
            chkAtivo.Checked = True
            txtSenha.Text = ""
            txtConfirmarSenha.Text = ""
            sSenhaWebAtual = ""

            'Usuário novo: a senha é obrigatória
            lblSenhaInfo.Visible = False

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Editar()

        Try

            Cursor.Current = Cursors.WaitCursor

            Call Novo()

            Dim oTabela As DataTable = oClsUsrCadUsuario.LoadDados(CLng(grdListagem.CurrentRow.Cells.Item("codigo").Value))

            If oTabela.Rows.Count = 0 Then
                Cursor.Current = Cursors.Default
                MsgBox("Usuário não encontrado.", MsgBoxStyle.Exclamation, gAplicacao)
                Exit Sub
            End If

            With oTabela.Rows(0)

                lCodigoUsuario = CLng(.Item("codigo"))
                txtUsuario.Text = .Item("usuario").ToString
                txtNome.Text = .Item("nome").ToString
                If IsDBNull(.Item("empresa")) Then cboEmpresa.SelectedIndex = -1 Else cboEmpresa.SelectedValue = .Item("empresa")
                chkAdmin.Checked = (IsDBNull(.Item("admin")) = False AndAlso CInt(.Item("admin")) = 1)
                chkAtivo.Checked = (IsDBNull(.Item("ativo")) = False AndAlso CInt(.Item("ativo")) = 1)
                sSenhaWebAtual = .Item("senha").ToString

            End With

            lblSenhaInfo.Visible = True

            Call MostrarDados()

            txtUsuario.Focus()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Salvar()

        Try

            Cursor.Current = Cursors.WaitCursor

            Dim bAlterouSenha As Boolean = (txtSenha.Text <> "")

            'Formato da plataforma web (base64); sem senha nova, mantém a atual
            Dim sSenhaWeb As String = IIf(bAlterouSenha, Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(txtSenha.Text)), sSenhaWebAtual)

            If lCodigoUsuario > 0 Then

                oClsUsrCadUsuario.Update(lCodigoUsuario, _
                                         txtUsuario.Text.Trim, _
                                         sSenhaWeb, _
                                         txtNome.Text.Trim, _
                                         chkAdmin.Checked, _
                                         CInt(cboEmpresa.SelectedValue), _
                                         chkAtivo.Checked)

            Else

                lCodigoUsuario = oClsUsrCadUsuario.Insert(txtUsuario.Text.Trim, _
                                                          sSenhaWeb, _
                                                          txtNome.Text.Trim, _
                                                          chkAdmin.Checked, _
                                                          CInt(cboEmpresa.SelectedValue), _
                                                          chkAtivo.Checked)

            End If

            sSenhaWebAtual = sSenhaWeb

            'Login do Documentacti (BCrypt): a proc só grava para usuário ativo
            Dim sAviso As String = ""

            If bAlterouSenha Then
                If chkAtivo.Checked Then
                    oClsUsrCadUsuario.UpdateSenhaHash(lCodigoUsuario, txtSenha.Text)
                Else
                    sAviso = " A senha do Documentacti só é gravada para usuário ativo."
                End If
            End If

            txtSenha.Text = ""
            txtConfirmarSenha.Text = ""
            lblSenhaInfo.Visible = True

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Registro salvo com sucesso!" & sAviso, IIf(sAviso = "", Color.Blue, gColorInformacaoErro))

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function Validacao() As Boolean

        Try

            Validacao = False

            If ValidaCampo(txtUsuario, lblUsuario, TipoCampo.texto) = False Then Exit Function
            If ValidaCampo(txtNome, lblNome, TipoCampo.texto) = False Then Exit Function
            If ValidaCampo(cboEmpresa, lblEmpresa) = False Then Exit Function

            'Usuário novo exige senha; na alteração, senha em branco mantém a atual
            If lCodigoUsuario = 0 AndAlso ValidaCampo(txtSenha, lblSenha, TipoCampo.texto) = False Then Exit Function

            If txtSenha.Text <> txtConfirmarSenha.Text Then
                frmMain.errInfo.SetError(lblConfirmarSenha, "A confirmação não confere com a senha informada.")
                txtConfirmarSenha.Focus()
                Exit Function
            End If

            'O login é único: a proc da web sobrescreveria o usuário que já tem este login
            If oClsUsrCadUsuario.UsuarioExistente(txtUsuario.Text.Trim, IIf(lCodigoUsuario > 0, lCodigoUsuario, -1)) Then
                frmMain.errInfo.SetError(lblUsuario, "Já existe um usuário com este login.")
                txtUsuario.Focus()
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
