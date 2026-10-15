<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdmChamadoComentario
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
        Me.components = New System.ComponentModel.Container()
        Dim JanusColorScheme1 As Janus.Windows.Common.JanusColorScheme = New Janus.Windows.Common.JanusColorScheme()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdmChamadoComentario))
        Me.vsmMain = New Janus.Windows.Common.VisualStyleManager(Me.components)
        Me.tabMain = New Janus.Windows.UI.Tab.UITab()
        Me.pagTabela = New Janus.Windows.UI.Tab.UITabPage()
        Me.txtComentario = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.lblHistorico = New System.Windows.Forms.Label()
        Me.grpControle = New Janus.Windows.EditControls.UIGroupBox()
        Me.btnInserir = New Janus.Windows.EditControls.UIButton()
        Me.btnSair = New Janus.Windows.EditControls.UIButton()
        Me.grpDados = New Janus.Windows.EditControls.UIGroupBox()
        Me.txtTitulo = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.txtSolicitante = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.lblSolicitante = New System.Windows.Forms.Label()
        Me.txtCodigoChamado = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtDataChamado = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.lblDataChamado = New System.Windows.Forms.Label()
        Me.lblCodigoChamado = New System.Windows.Forms.Label()
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMain.SuspendLayout()
        Me.pagTabela.SuspendLayout()
        CType(Me.grpControle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpControle.SuspendLayout()
        CType(Me.grpDados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDados.SuspendLayout()
        Me.SuspendLayout()
        '
        'vsmMain
        '
        JanusColorScheme1.HighlightTextColor = System.Drawing.SystemColors.HighlightText
        JanusColorScheme1.Name = "Scheme"
        JanusColorScheme1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.Silver
        JanusColorScheme1.Office2007CustomColor = System.Drawing.Color.Empty
        JanusColorScheme1.VisualStyle = Janus.Windows.Common.VisualStyle.Office2007
        Me.vsmMain.ColorSchemes.Add(JanusColorScheme1)
        Me.vsmMain.DefaultColorScheme = Nothing
        '
        'tabMain
        '
        Me.tabMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabMain.Location = New System.Drawing.Point(7, 9)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.tabMain.Size = New System.Drawing.Size(927, 433)
        Me.tabMain.TabIndex = 2
        Me.tabMain.TabPages.AddRange(New Janus.Windows.UI.Tab.UITabPage() {Me.pagTabela})
        Me.tabMain.TabStop = False
        Me.tabMain.VisualStyleManager = Me.vsmMain
        '
        'pagTabela
        '
        Me.pagTabela.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pagTabela.Controls.Add(Me.txtComentario)
        Me.pagTabela.Controls.Add(Me.lblHistorico)
        Me.pagTabela.Controls.Add(Me.grpControle)
        Me.pagTabela.Controls.Add(Me.grpDados)
        Me.pagTabela.Key = "pagTabela"
        Me.pagTabela.Location = New System.Drawing.Point(1, 24)
        Me.pagTabela.Name = "pagTabela"
        Me.pagTabela.Size = New System.Drawing.Size(925, 408)
        Me.pagTabela.TabStop = True
        Me.pagTabela.Text = "Comentário"
        '
        'txtComentario
        '
        Me.txtComentario.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtComentario.Location = New System.Drawing.Point(4, 128)
        Me.txtComentario.MaxLength = 5000
        Me.txtComentario.Multiline = True
        Me.txtComentario.Name = "txtComentario"
        Me.txtComentario.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComentario.Size = New System.Drawing.Size(918, 218)
        Me.txtComentario.TabIndex = 7
        '
        'lblHistorico
        '
        Me.lblHistorico.AutoSize = True
        Me.lblHistorico.BackColor = System.Drawing.Color.Transparent
        Me.lblHistorico.Location = New System.Drawing.Point(1, 111)
        Me.lblHistorico.Name = "lblHistorico"
        Me.lblHistorico.Size = New System.Drawing.Size(85, 16)
        Me.lblHistorico.TabIndex = 6
        Me.lblHistorico.Text = "Comentário:"
        '
        'grpControle
        '
        Me.grpControle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpControle.BackColor = System.Drawing.Color.Transparent
        Me.grpControle.Controls.Add(Me.btnInserir)
        Me.grpControle.Controls.Add(Me.btnSair)
        Me.grpControle.Location = New System.Drawing.Point(4, 352)
        Me.grpControle.Name = "grpControle"
        Me.grpControle.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControle.Size = New System.Drawing.Size(918, 51)
        Me.grpControle.TabIndex = 4
        Me.grpControle.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        Me.grpControle.VisualStyleManager = Me.vsmMain
        '
        'btnInserir
        '
        Me.btnInserir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInserir.Image = Global.Documentacti.My.Resources.Resources.add
        Me.btnInserir.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnInserir.Location = New System.Drawing.Point(720, 17)
        Me.btnInserir.Name = "btnInserir"
        Me.btnInserir.Size = New System.Drawing.Size(91, 23)
        Me.btnInserir.TabIndex = 0
        Me.btnInserir.Text = "Inserir"
        Me.btnInserir.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSair
        '
        Me.btnSair.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSair.Image = CType(resources.GetObject("btnSair.Image"), System.Drawing.Image)
        Me.btnSair.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSair.ImageSize = New System.Drawing.Size(19, 16)
        Me.btnSair.Location = New System.Drawing.Point(817, 17)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnSair.Size = New System.Drawing.Size(91, 23)
        Me.btnSair.TabIndex = 1
        Me.btnSair.Text = "Sair"
        Me.btnSair.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'grpDados
        '
        Me.grpDados.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpDados.BackColor = System.Drawing.Color.Transparent
        Me.grpDados.Controls.Add(Me.txtTitulo)
        Me.grpDados.Controls.Add(Me.lblTitulo)
        Me.grpDados.Controls.Add(Me.txtSolicitante)
        Me.grpDados.Controls.Add(Me.lblSolicitante)
        Me.grpDados.Controls.Add(Me.txtCodigoChamado)
        Me.grpDados.Controls.Add(Me.txtDataChamado)
        Me.grpDados.Controls.Add(Me.lblDataChamado)
        Me.grpDados.Controls.Add(Me.lblCodigoChamado)
        Me.grpDados.Location = New System.Drawing.Point(4, 3)
        Me.grpDados.Name = "grpDados"
        Me.grpDados.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpDados.Size = New System.Drawing.Size(918, 105)
        Me.grpDados.TabIndex = 3
        Me.grpDados.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        Me.grpDados.VisualStyleManager = Me.vsmMain
        '
        'txtTitulo
        '
        Me.txtTitulo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTitulo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtTitulo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTitulo.Location = New System.Drawing.Point(9, 74)
        Me.txtTitulo.MaxLength = 100
        Me.txtTitulo.Name = "txtTitulo"
        Me.txtTitulo.ReadOnly = True
        Me.txtTitulo.Size = New System.Drawing.Size(903, 23)
        Me.txtTitulo.TabIndex = 9
        Me.txtTitulo.TabStop = False
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Location = New System.Drawing.Point(6, 57)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(48, 16)
        Me.lblTitulo.TabIndex = 8
        Me.lblTitulo.Text = "Título:"
        '
        'txtSolicitante
        '
        Me.txtSolicitante.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSolicitante.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSolicitante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSolicitante.Location = New System.Drawing.Point(267, 34)
        Me.txtSolicitante.MaxLength = 50
        Me.txtSolicitante.Name = "txtSolicitante"
        Me.txtSolicitante.ReadOnly = True
        Me.txtSolicitante.Size = New System.Drawing.Size(645, 23)
        Me.txtSolicitante.TabIndex = 11
        Me.txtSolicitante.TabStop = False
        '
        'lblSolicitante
        '
        Me.lblSolicitante.AutoSize = True
        Me.lblSolicitante.Location = New System.Drawing.Point(264, 17)
        Me.lblSolicitante.Name = "lblSolicitante"
        Me.lblSolicitante.Size = New System.Drawing.Size(77, 16)
        Me.lblSolicitante.TabIndex = 10
        Me.lblSolicitante.Text = "Solicitante:"
        '
        'txtCodigoChamado
        '
        Me.txtCodigoChamado.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCodigoChamado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCodigoChamado.Location = New System.Drawing.Point(9, 34)
        Me.txtCodigoChamado.MaxLength = 50
        Me.txtCodigoChamado.Name = "txtCodigoChamado"
        Me.txtCodigoChamado.ReadOnly = True
        Me.txtCodigoChamado.Size = New System.Drawing.Size(95, 23)
        Me.txtCodigoChamado.TabIndex = 1
        Me.txtCodigoChamado.TabStop = False
        '
        'txtDataChamado
        '
        Me.txtDataChamado.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtDataChamado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDataChamado.Location = New System.Drawing.Point(110, 34)
        Me.txtDataChamado.MaxLength = 20
        Me.txtDataChamado.Name = "txtDataChamado"
        Me.txtDataChamado.ReadOnly = True
        Me.txtDataChamado.Size = New System.Drawing.Size(151, 23)
        Me.txtDataChamado.TabIndex = 3
        Me.txtDataChamado.TabStop = False
        '
        'lblDataChamado
        '
        Me.lblDataChamado.AutoSize = True
        Me.lblDataChamado.Location = New System.Drawing.Point(107, 17)
        Me.lblDataChamado.Name = "lblDataChamado"
        Me.lblDataChamado.Size = New System.Drawing.Size(107, 16)
        Me.lblDataChamado.TabIndex = 2
        Me.lblDataChamado.Text = "Data Chamado:"
        '
        'lblCodigoChamado
        '
        Me.lblCodigoChamado.AutoSize = True
        Me.lblCodigoChamado.Location = New System.Drawing.Point(6, 17)
        Me.lblCodigoChamado.Name = "lblCodigoChamado"
        Me.lblCodigoChamado.Size = New System.Drawing.Size(57, 16)
        Me.lblCodigoChamado.TabIndex = 0
        Me.lblCodigoChamado.Text = "Código:"
        '
        'frmAdmChamadoComentario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(946, 448)
        Me.Controls.Add(Me.tabMain)
        Me.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAdmChamadoComentario"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Chamado - Comentário"
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMain.ResumeLayout(False)
        Me.pagTabela.ResumeLayout(False)
        Me.pagTabela.PerformLayout()
        CType(Me.grpControle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpControle.ResumeLayout(False)
        CType(Me.grpDados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDados.ResumeLayout(False)
        Me.grpDados.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents vsmMain As Janus.Windows.Common.VisualStyleManager
    Friend WithEvents tabMain As Janus.Windows.UI.Tab.UITab
    Friend WithEvents pagTabela As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents txtComentario As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents lblHistorico As System.Windows.Forms.Label
    Friend WithEvents grpControle As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnInserir As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair As Janus.Windows.EditControls.UIButton
    Friend WithEvents grpDados As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents txtTitulo As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents txtSolicitante As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents lblSolicitante As System.Windows.Forms.Label
    Friend WithEvents txtCodigoChamado As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtDataChamado As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents lblDataChamado As System.Windows.Forms.Label
    Friend WithEvents lblCodigoChamado As System.Windows.Forms.Label
End Class
