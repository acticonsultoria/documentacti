<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAlterarSenha
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblSenhaAtual = New System.Windows.Forms.Label()
        Me.lblNovaSenha = New System.Windows.Forms.Label()
        Me.lblConfirmacao = New System.Windows.Forms.Label()
        Me.txtSenhaAtual = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtNovaSenha = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtConfirmacao = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.btnSalvar = New Janus.Windows.EditControls.UIButton()
        Me.btnSair = New Janus.Windows.EditControls.UIButton()
        Me.SuspendLayout()
        '
        'lblSenhaAtual
        '
        Me.lblSenhaAtual.AutoSize = True
        Me.lblSenhaAtual.Location = New System.Drawing.Point(12, 14)
        Me.lblSenhaAtual.Name = "lblSenhaAtual"
        Me.lblSenhaAtual.Size = New System.Drawing.Size(70, 14)
        Me.lblSenhaAtual.TabIndex = 4
        Me.lblSenhaAtual.Text = "Senha atual:"
        '
        'lblNovaSenha
        '
        Me.lblNovaSenha.AutoSize = True
        Me.lblNovaSenha.Location = New System.Drawing.Point(12, 60)
        Me.lblNovaSenha.Name = "lblNovaSenha"
        Me.lblNovaSenha.Size = New System.Drawing.Size(66, 14)
        Me.lblNovaSenha.TabIndex = 5
        Me.lblNovaSenha.Text = "Nova senha:"
        '
        'lblConfirmacao
        '
        Me.lblConfirmacao.AutoSize = True
        Me.lblConfirmacao.Location = New System.Drawing.Point(12, 106)
        Me.lblConfirmacao.Name = "lblConfirmacao"
        Me.lblConfirmacao.Size = New System.Drawing.Size(103, 14)
        Me.lblConfirmacao.TabIndex = 6
        Me.lblConfirmacao.Text = "Confirmar a senha:"
        '
        'txtSenhaAtual
        '
        Me.txtSenhaAtual.Location = New System.Drawing.Point(12, 31)
        Me.txtSenhaAtual.MaxLength = 72
        Me.txtSenhaAtual.Name = "txtSenhaAtual"
        Me.txtSenhaAtual.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSenhaAtual.Size = New System.Drawing.Size(336, 20)
        Me.txtSenhaAtual.TabIndex = 0
        Me.txtSenhaAtual.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'txtNovaSenha
        '
        Me.txtNovaSenha.Location = New System.Drawing.Point(12, 77)
        Me.txtNovaSenha.MaxLength = 72
        Me.txtNovaSenha.Name = "txtNovaSenha"
        Me.txtNovaSenha.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNovaSenha.Size = New System.Drawing.Size(336, 20)
        Me.txtNovaSenha.TabIndex = 1
        Me.txtNovaSenha.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'txtConfirmacao
        '
        Me.txtConfirmacao.Location = New System.Drawing.Point(12, 123)
        Me.txtConfirmacao.MaxLength = 72
        Me.txtConfirmacao.Name = "txtConfirmacao"
        Me.txtConfirmacao.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirmacao.Size = New System.Drawing.Size(336, 20)
        Me.txtConfirmacao.TabIndex = 2
        Me.txtConfirmacao.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'btnSalvar
        '
        Me.btnSalvar.Image = Global.Documentacti.My.Resources.Resources.salvar
        Me.btnSalvar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSalvar.Location = New System.Drawing.Point(157, 165)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(91, 23)
        Me.btnSalvar.TabIndex = 3
        Me.btnSalvar.Text = "Salvar"
        Me.btnSalvar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSair
        '
        Me.btnSair.Image = Global.Documentacti.My.Resources.Resources.seta_esquerda
        Me.btnSair.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSair.Location = New System.Drawing.Point(257, 165)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Size = New System.Drawing.Size(91, 23)
        Me.btnSair.TabIndex = 4
        Me.btnSair.Text = "Voltar"
        Me.btnSair.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmAlterarSenha
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(360, 200)
        Me.Controls.Add(Me.btnSair)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.txtConfirmacao)
        Me.Controls.Add(Me.txtNovaSenha)
        Me.Controls.Add(Me.txtSenhaAtual)
        Me.Controls.Add(Me.lblConfirmacao)
        Me.Controls.Add(Me.lblNovaSenha)
        Me.Controls.Add(Me.lblSenhaAtual)
        Me.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAlterarSenha"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Documentacti - Alterar Senha"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSenhaAtual As System.Windows.Forms.Label
    Friend WithEvents lblNovaSenha As System.Windows.Forms.Label
    Friend WithEvents lblConfirmacao As System.Windows.Forms.Label
    Friend WithEvents txtSenhaAtual As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtNovaSenha As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtConfirmacao As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents btnSalvar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair As Janus.Windows.EditControls.UIButton

End Class
