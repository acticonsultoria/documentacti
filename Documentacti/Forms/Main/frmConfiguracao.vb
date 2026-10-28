Public Class frmConfiguracao

#Region "::: VARIAVEIS :::"

    Private oClsConfiguracao As New clsFrmConfiguracao

#End Region

#Region "::: CONTROLES :::"

    Private Sub frmConfiguracao_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            Me.Icon = frmMain.Icon

            Cursor.Current = Cursors.WaitCursor

            chkSilenciarNotificacao.Checked = oClsConfiguracao.SilenciarNotificacao()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalvar.Click

        Try

            Cursor.Current = Cursors.WaitCursor

            oClsConfiguracao.Update(chkSilenciarNotificacao.Checked)

            'Vale na hora, sem precisar entrar de novo no sistema
            frmMain.DefinirSilenciarNotificacao(chkSilenciarNotificacao.Checked)

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Configurações salvas com sucesso!", Color.Blue)

            Me.Dispose()

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSair.Click

        Me.Dispose()

    End Sub

#End Region

End Class
