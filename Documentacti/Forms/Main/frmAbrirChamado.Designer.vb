<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbrirChamado
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
        Me.grpChamado = New Janus.Windows.EditControls.UIGroupBox()
        Me.txtCodigo = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.btnAbrir = New Janus.Windows.EditControls.UIButton()
        Me.btnSair = New Janus.Windows.EditControls.UIButton()
        CType(Me.grpChamado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpChamado.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpChamado
        '
        Me.grpChamado.BackColor = System.Drawing.Color.Transparent
        Me.grpChamado.Controls.Add(Me.txtCodigo)
        Me.grpChamado.Controls.Add(Me.lblCodigo)
        Me.grpChamado.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpChamado.Location = New System.Drawing.Point(12, 12)
        Me.grpChamado.Name = "grpChamado"
        Me.grpChamado.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpChamado.Size = New System.Drawing.Size(276, 70)
        Me.grpChamado.TabIndex = 0
        Me.grpChamado.Text = "Abrir Chamado"
        Me.grpChamado.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.BackColor = System.Drawing.Color.Transparent
        Me.lblCodigo.Location = New System.Drawing.Point(12, 22)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(108, 14)
        Me.lblCodigo.TabIndex = 0
        Me.lblCodigo.Text = "Número do chamado:"
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(15, 39)
        Me.txtCodigo.MaxLength = 10
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(120, 23)
        Me.txtCodigo.TabIndex = 1
        '
        'btnAbrir
        '
        Me.btnAbrir.Image = Global.Documentacti.My.Resources.Resources.procurar
        Me.btnAbrir.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnAbrir.Location = New System.Drawing.Point(97, 94)
        Me.btnAbrir.Name = "btnAbrir"
        Me.btnAbrir.Size = New System.Drawing.Size(91, 23)
        Me.btnAbrir.TabIndex = 1
        Me.btnAbrir.Text = "Abrir"
        Me.btnAbrir.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSair
        '
        Me.btnSair.Image = Global.Documentacti.My.Resources.Resources.seta_esquerda
        Me.btnSair.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSair.Location = New System.Drawing.Point(197, 94)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Size = New System.Drawing.Size(91, 23)
        Me.btnSair.TabIndex = 2
        Me.btnSair.Text = "Voltar"
        Me.btnSair.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmAbrirChamado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(300, 129)
        Me.Controls.Add(Me.btnSair)
        Me.Controls.Add(Me.btnAbrir)
        Me.Controls.Add(Me.grpChamado)
        Me.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbrirChamado"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Documentacti - Abrir Chamado"
        CType(Me.grpChamado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpChamado.ResumeLayout(False)
        Me.grpChamado.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpChamado As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents txtCodigo As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents btnAbrir As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair As Janus.Windows.EditControls.UIButton

End Class
