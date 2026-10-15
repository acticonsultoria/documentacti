Public Class frmAlterarSenha

#Region "::: VARIAVEIS :::"

    Private oClsLogin As New clsFrmLogin

#End Region

#Region "::: CONTROLES :::"

    Private Sub frmAlterarSenha_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            Call AddGotFocus(Me, New DelegateGotFocus(AddressOf Control_GotFocus))
            Call AddLostFocus(Me, New DelegateLostFocus(AddressOf Control_LostFocus))

            Me.Icon = frmMain.Icon

            txtSenhaAtual.Focus()

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub frmAlterarSenha_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Enter Then Call btnSalvar_Click(btnSalvar, System.EventArgs.Empty)

    End Sub

    Private Sub btnSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalvar.Click

        Try

            frmMain.errInfo.Clear()

            If Validacao() = True Then

                Cursor.Current = Cursors.WaitCursor

                'Confere a senha atual e grava o novo hash (bcrypt)
                If oClsLogin.AlterarSenha(txtSenhaAtual.Text, txtNovaSenha.Text) = False Then
                    Cursor.Current = Cursors.Default
                    frmMain.errInfo.SetError(lblSenhaAtual, "A senha atual informada não confere.")
                    txtSenhaAtual.Focus()
                    Exit Sub
                End If

                Cursor.Current = Cursors.Default

                frmMain.Informacao("Senha alterada com sucesso!", Color.Blue)

                Me.Dispose()

            End If

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSair.Click

        Me.Dispose()

    End Sub

#End Region

#Region "::: FUNCTION / SUB :::"

    Private Function Validacao() As Boolean

        Try

            Validacao = False

            If ValidaCampo(txtSenhaAtual, lblSenhaAtual, TipoCampo.texto) = False Then Exit Function
            If ValidaCampo(txtNovaSenha, lblNovaSenha, TipoCampo.texto) = False Then Exit Function
            If ValidaCampo(txtConfirmacao, lblConfirmacao, TipoCampo.texto) = False Then Exit Function

            If txtNovaSenha.Text.Length < 8 Then
                frmMain.errInfo.SetError(lblNovaSenha, "A nova senha deve ter no mínimo 8 caracteres.")
                txtNovaSenha.Focus()
                Exit Function
            End If

            If txtNovaSenha.Text <> txtConfirmacao.Text Then
                frmMain.errInfo.SetError(lblConfirmacao, "A confirmação não confere com a nova senha.")
                txtConfirmacao.Focus()
                Exit Function
            End If

            If txtNovaSenha.Text = txtSenhaAtual.Text Then
                frmMain.errInfo.SetError(lblNovaSenha, "A nova senha deve ser diferente da senha atual.")
                txtNovaSenha.Focus()
                Exit Function
            End If

            Validacao = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

End Class
