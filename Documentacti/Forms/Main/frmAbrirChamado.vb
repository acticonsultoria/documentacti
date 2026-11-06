Public Class frmAbrirChamado

#Region "::: VARIAVEIS :::"

    'Número informado (0 = cancelado)
    Public lChamado As Long = 0

#End Region

#Region "::: CONTROLES :::"

    Private Sub frmAbrirChamado_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            Me.Icon = frmMain.Icon

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub frmAbrirChamado_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        txtCodigo.Focus()

    End Sub

    Private Sub frmAbrirChamado_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        'Enter abre, Esc volta
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnAbrir_Click(btnAbrir, System.EventArgs.Empty)
        ElseIf e.KeyCode = Keys.Escape Then
            e.SuppressKeyPress = True
            Call btnSair_Click(btnSair, System.EventArgs.Empty)
        End If

    End Sub

    Private Sub btnAbrir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbrir.Click

        Try

            Dim lNumero As Long = 0

            If Long.TryParse(txtCodigo.Text.Trim, lNumero) = False OrElse lNumero <= 0 Then
                MsgBox("Informe o número do chamado.", MsgBoxStyle.Exclamation, Me.Text)
                txtCodigo.Focus()
                Exit Sub
            End If

            lChamado = lNumero
            Me.DialogResult = DialogResult.OK

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSair.Click

        lChamado = 0
        Me.DialogResult = DialogResult.Cancel

    End Sub

#End Region

End Class
