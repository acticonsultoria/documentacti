<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfiguracao
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
        Me.grpNotificacao = New Janus.Windows.EditControls.UIGroupBox()
        Me.chkSilenciarNotificacao = New Janus.Windows.EditControls.UICheckBox()
        Me.lblSilenciarNotificacao = New System.Windows.Forms.Label()
        Me.btnSalvar = New Janus.Windows.EditControls.UIButton()
        Me.btnSair = New Janus.Windows.EditControls.UIButton()
        CType(Me.grpNotificacao, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpNotificacao.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpNotificacao
        '
        Me.grpNotificacao.BackColor = System.Drawing.Color.Transparent
        Me.grpNotificacao.Controls.Add(Me.lblSilenciarNotificacao)
        Me.grpNotificacao.Controls.Add(Me.chkSilenciarNotificacao)
        Me.grpNotificacao.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpNotificacao.Location = New System.Drawing.Point(12, 12)
        Me.grpNotificacao.Name = "grpNotificacao"
        Me.grpNotificacao.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpNotificacao.Size = New System.Drawing.Size(376, 110)
        Me.grpNotificacao.TabIndex = 0
        Me.grpNotificacao.Text = "Notificações"
        Me.grpNotificacao.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'chkSilenciarNotificacao
        '
        Me.chkSilenciarNotificacao.BackColor = System.Drawing.Color.Transparent
        Me.chkSilenciarNotificacao.Location = New System.Drawing.Point(12, 22)
        Me.chkSilenciarNotificacao.Name = "chkSilenciarNotificacao"
        Me.chkSilenciarNotificacao.Size = New System.Drawing.Size(352, 20)
        Me.chkSilenciarNotificacao.TabIndex = 0
        Me.chkSilenciarNotificacao.Text = "Silenciar notificações das mensagens do chat"
        Me.chkSilenciarNotificacao.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'lblSilenciarNotificacao
        '
        Me.lblSilenciarNotificacao.BackColor = System.Drawing.Color.Transparent
        Me.lblSilenciarNotificacao.ForeColor = System.Drawing.Color.DimGray
        Me.lblSilenciarNotificacao.Location = New System.Drawing.Point(29, 46)
        Me.lblSilenciarNotificacao.Name = "lblSilenciarNotificacao"
        Me.lblSilenciarNotificacao.Size = New System.Drawing.Size(335, 56)
        Me.lblSilenciarNotificacao.TabIndex = 1
        Me.lblSilenciarNotificacao.Text = "Os avisos no canto da tela deixam de aparecer até você sair do sistema. Ao entrar de novo, as notificações voltam a ser ativadas automaticamente. As mensagens continuam chegando na aba Chat de cada chamado."
        '
        'btnSalvar
        '
        Me.btnSalvar.Image = Global.Documentacti.My.Resources.Resources.salvar
        Me.btnSalvar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSalvar.Location = New System.Drawing.Point(197, 134)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(91, 23)
        Me.btnSalvar.TabIndex = 1
        Me.btnSalvar.Text = "Salvar"
        Me.btnSalvar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSair
        '
        Me.btnSair.Image = Global.Documentacti.My.Resources.Resources.seta_esquerda
        Me.btnSair.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSair.Location = New System.Drawing.Point(297, 134)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Size = New System.Drawing.Size(91, 23)
        Me.btnSair.TabIndex = 2
        Me.btnSair.Text = "Voltar"
        Me.btnSair.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmConfiguracao
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(400, 169)
        Me.Controls.Add(Me.btnSair)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.grpNotificacao)
        Me.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfiguracao"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Documentacti - Configurações"
        CType(Me.grpNotificacao, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpNotificacao.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpNotificacao As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents chkSilenciarNotificacao As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents lblSilenciarNotificacao As System.Windows.Forms.Label
    Friend WithEvents btnSalvar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair As Janus.Windows.EditControls.UIButton

End Class
