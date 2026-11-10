<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCfgGrid
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
        Dim grdColunas_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCfgGrid))
        Me.grpColunas = New Janus.Windows.EditControls.UIGroupBox()
        Me.grdColunas = New Janus.Windows.GridEX.GridEX()
        Me.lblInformacao = New System.Windows.Forms.Label()
        Me.btnRestaurar = New Janus.Windows.EditControls.UIButton()
        Me.btnSalvar = New Janus.Windows.EditControls.UIButton()
        Me.btnVoltar = New Janus.Windows.EditControls.UIButton()
        CType(Me.grpColunas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpColunas.SuspendLayout()
        CType(Me.grdColunas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpColunas
        '
        Me.grpColunas.BackColor = System.Drawing.Color.Transparent
        Me.grpColunas.Controls.Add(Me.lblInformacao)
        Me.grpColunas.Controls.Add(Me.grdColunas)
        Me.grpColunas.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpColunas.Location = New System.Drawing.Point(12, 12)
        Me.grpColunas.Name = "grpColunas"
        Me.grpColunas.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpColunas.Size = New System.Drawing.Size(376, 340)
        Me.grpColunas.TabIndex = 0
        Me.grpColunas.Text = "Colunas da Grid"
        Me.grpColunas.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'grdColunas
        '
        Me.grdColunas.AlternatingColors = True
        grdColunas_DesignTimeLayout.LayoutString = resources.GetString("grdColunas_DesignTimeLayout.LayoutString")
        Me.grdColunas.DesignTimeLayout = grdColunas_DesignTimeLayout
        Me.grdColunas.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdColunas.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdColunas.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdColunas.GroupByBoxVisible = False
        Me.grdColunas.Location = New System.Drawing.Point(9, 20)
        Me.grdColunas.Name = "grdColunas"
        Me.grdColunas.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdColunas.ScrollBars = Janus.Windows.GridEX.ScrollBars.Vertical
        Me.grdColunas.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdColunas.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdColunas.Size = New System.Drawing.Size(358, 287)
        Me.grdColunas.TabIndex = 0
        Me.grdColunas.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblInformacao
        '
        Me.lblInformacao.BackColor = System.Drawing.Color.Transparent
        Me.lblInformacao.ForeColor = System.Drawing.Color.DimGray
        Me.lblInformacao.Location = New System.Drawing.Point(6, 310)
        Me.lblInformacao.Name = "lblInformacao"
        Me.lblInformacao.Size = New System.Drawing.Size(364, 26)
        Me.lblInformacao.TabIndex = 1
        Me.lblInformacao.Text = "Para mudar a ordem, arraste o título da coluna na própria grid (a ordem e a largura são gravadas automaticamente)."
        '
        'btnRestaurar
        '
        Me.btnRestaurar.Image = Global.Documentacti.My.Resources.Resources.config
        Me.btnRestaurar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnRestaurar.Location = New System.Drawing.Point(12, 364)
        Me.btnRestaurar.Name = "btnRestaurar"
        Me.btnRestaurar.Size = New System.Drawing.Size(91, 23)
        Me.btnRestaurar.TabIndex = 1
        Me.btnRestaurar.Text = "Padrão"
        Me.btnRestaurar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSalvar
        '
        Me.btnSalvar.Image = Global.Documentacti.My.Resources.Resources.salvar
        Me.btnSalvar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSalvar.Location = New System.Drawing.Point(200, 364)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(91, 23)
        Me.btnSalvar.TabIndex = 2
        Me.btnSalvar.Text = "Salvar"
        Me.btnSalvar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnVoltar
        '
        Me.btnVoltar.Image = Global.Documentacti.My.Resources.Resources.seta_esquerda
        Me.btnVoltar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnVoltar.Location = New System.Drawing.Point(297, 364)
        Me.btnVoltar.Name = "btnVoltar"
        Me.btnVoltar.Size = New System.Drawing.Size(91, 23)
        Me.btnVoltar.TabIndex = 3
        Me.btnVoltar.Text = "Voltar"
        Me.btnVoltar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmCfgGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(400, 399)
        Me.Controls.Add(Me.btnVoltar)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.btnRestaurar)
        Me.Controls.Add(Me.grpColunas)
        Me.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCfgGrid"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Documentacti - Configurar Colunas"
        CType(Me.grpColunas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpColunas.ResumeLayout(False)
        CType(Me.grdColunas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpColunas As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents grdColunas As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblInformacao As System.Windows.Forms.Label
    Friend WithEvents btnRestaurar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSalvar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnVoltar As Janus.Windows.EditControls.UIButton

End Class
