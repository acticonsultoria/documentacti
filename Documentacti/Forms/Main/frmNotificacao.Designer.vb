<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNotificacao
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNotificacao))
        Dim grdNotificacao_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdNotificacao_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image")
        Dim grdNotificacao_DesignTimeLayout_Reference_1 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column1.Image")
        Me.grpNotificacao = New Janus.Windows.EditControls.UIGroupBox()
        Me.grdNotificacao = New Janus.Windows.GridEX.GridEX()
        Me.lblDica = New System.Windows.Forms.Label()
        Me.btnMarcarLidas = New Janus.Windows.EditControls.UIButton()
        Me.btnExcluirTodas = New Janus.Windows.EditControls.UIButton()
        Me.btnSair = New Janus.Windows.EditControls.UIButton()
        CType(Me.grpNotificacao, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpNotificacao.SuspendLayout()
        CType(Me.grdNotificacao, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpNotificacao
        '
        Me.grpNotificacao.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpNotificacao.BackColor = System.Drawing.Color.Transparent
        Me.grpNotificacao.Controls.Add(Me.grdNotificacao)
        Me.grpNotificacao.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpNotificacao.Location = New System.Drawing.Point(12, 8)
        Me.grpNotificacao.Name = "grpNotificacao"
        Me.grpNotificacao.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpNotificacao.Size = New System.Drawing.Size(776, 318)
        Me.grpNotificacao.TabIndex = 0
        Me.grpNotificacao.Text = "Notificações"
        Me.grpNotificacao.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'grdNotificacao
        '
        Me.grdNotificacao.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdNotificacao.AlternatingColors = True
        Me.grdNotificacao.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdNotificacao_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("grdNotificacao_DesignTimeLayout_Reference_0.Instance"), Object)
        grdNotificacao_DesignTimeLayout_Reference_1.Instance = CType(resources.GetObject("grdNotificacao_DesignTimeLayout_Reference_1.Instance"), Object)
        grdNotificacao_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {grdNotificacao_DesignTimeLayout_Reference_0, grdNotificacao_DesignTimeLayout_Reference_1})
        grdNotificacao_DesignTimeLayout.LayoutString = resources.GetString("grdNotificacao_DesignTimeLayout.LayoutString")
        Me.grdNotificacao.DesignTimeLayout = grdNotificacao_DesignTimeLayout
        Me.grdNotificacao.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdNotificacao.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdNotificacao.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdNotificacao.GroupByBoxVisible = False
        Me.grdNotificacao.Location = New System.Drawing.Point(8, 20)
        Me.grdNotificacao.Name = "grdNotificacao"
        Me.grdNotificacao.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdNotificacao.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdNotificacao.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdNotificacao.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdNotificacao.Size = New System.Drawing.Size(760, 290)
        Me.grdNotificacao.TabIndex = 0
        Me.grdNotificacao.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblDica
        '
        Me.lblDica.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDica.BackColor = System.Drawing.Color.Transparent
        Me.lblDica.ForeColor = System.Drawing.Color.Gray
        Me.lblDica.Location = New System.Drawing.Point(12, 330)
        Me.lblDica.Name = "lblDica"
        Me.lblDica.Size = New System.Drawing.Size(320, 30)
        Me.lblDica.TabIndex = 4
        Me.lblDica.Text = "Duplo clique (ou Enter) abre a notificação. Duplo clique no check marca como lida e no X exclui."
        '
        'btnExcluirTodas
        '
        Me.btnExcluirTodas.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcluirTodas.Image = Global.Documentacti.My.Resources.Resources.cancelar
        Me.btnExcluirTodas.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnExcluirTodas.Location = New System.Drawing.Point(342, 333)
        Me.btnExcluirTodas.Name = "btnExcluirTodas"
        Me.btnExcluirTodas.Size = New System.Drawing.Size(170, 23)
        Me.btnExcluirTodas.TabIndex = 1
        Me.btnExcluirTodas.Text = "Excluir todas"
        Me.btnExcluirTodas.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnMarcarLidas
        '
        Me.btnMarcarLidas.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMarcarLidas.Image = Global.Documentacti.My.Resources.Resources.aprovar
        Me.btnMarcarLidas.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnMarcarLidas.Location = New System.Drawing.Point(518, 333)
        Me.btnMarcarLidas.Name = "btnMarcarLidas"
        Me.btnMarcarLidas.Size = New System.Drawing.Size(170, 23)
        Me.btnMarcarLidas.TabIndex = 2
        Me.btnMarcarLidas.Text = "Marcar todas como lidas"
        Me.btnMarcarLidas.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSair
        '
        Me.btnSair.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSair.Image = Global.Documentacti.My.Resources.Resources.seta_esquerda
        Me.btnSair.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSair.Location = New System.Drawing.Point(697, 333)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Size = New System.Drawing.Size(91, 23)
        Me.btnSair.TabIndex = 3
        Me.btnSair.Text = "Voltar"
        Me.btnSair.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmNotificacao
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(800, 365)
        Me.Controls.Add(Me.lblDica)
        Me.Controls.Add(Me.btnSair)
        Me.Controls.Add(Me.btnMarcarLidas)
        Me.Controls.Add(Me.btnExcluirTodas)
        Me.Controls.Add(Me.grpNotificacao)
        Me.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNotificacao"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Documentacti - Notificações"
        CType(Me.grpNotificacao, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpNotificacao.ResumeLayout(False)
        CType(Me.grdNotificacao, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpNotificacao As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents grdNotificacao As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblDica As System.Windows.Forms.Label
    Friend WithEvents btnMarcarLidas As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnExcluirTodas As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair As Janus.Windows.EditControls.UIButton

End Class
