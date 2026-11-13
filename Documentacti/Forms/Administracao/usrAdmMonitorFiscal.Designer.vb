<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class usrAdmMonitorFiscal
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(usrAdmMonitorFiscal))
        Dim JanusColorScheme1 As Janus.Windows.Common.JanusColorScheme = New Janus.Windows.Common.JanusColorScheme()
        Dim grdNFe_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdNFSe_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdAgenda_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdAutorizador_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdUF_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.vsmMain = New Janus.Windows.Common.VisualStyleManager(Me.components)
        Me.tabMain = New Janus.Windows.UI.Tab.UITab()
        Me.pagNotas = New Janus.Windows.UI.Tab.UITabPage()
        Me.grpNFe = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblNFeNumero = New System.Windows.Forms.Label()
        Me.lblNFeData = New System.Windows.Forms.Label()
        Me.lblNFeResumo = New System.Windows.Forms.Label()
        Me.lnkNFe = New System.Windows.Forms.LinkLabel()
        Me.grpNFSe = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblNFSeNumero = New System.Windows.Forms.Label()
        Me.lblNFSeData = New System.Windows.Forms.Label()
        Me.lblNFSeResumo = New System.Windows.Forms.Label()
        Me.lnkNFSe = New System.Windows.Forms.LinkLabel()
        Me.tabNotas = New Janus.Windows.UI.Tab.UITab()
        Me.pagNFe = New Janus.Windows.UI.Tab.UITabPage()
        Me.grdNFe = New Janus.Windows.GridEX.GridEX()
        Me.pagNFSe = New Janus.Windows.UI.Tab.UITabPage()
        Me.grdNFSe = New Janus.Windows.GridEX.GridEX()
        Me.grpAgenda = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblProximo = New System.Windows.Forms.Label()
        Me.lblProximoData = New System.Windows.Forms.Label()
        Me.lblProximoMarco = New System.Windows.Forms.Label()
        Me.grdAgenda = New Janus.Windows.GridEX.GridEX()
        Me.grpControl = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblFonte = New System.Windows.Forms.Label()
        Me.btnFontes = New Janus.Windows.EditControls.UIButton()
        Me.btnAtualizar = New Janus.Windows.EditControls.UIButton()
        Me.pagPortais = New Janus.Windows.UI.Tab.UITabPage()
        Me.grpResumo = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblResumo = New System.Windows.Forms.Label()
        Me.lblMedido = New System.Windows.Forms.Label()
        Me.btnVerificar = New Janus.Windows.EditControls.UIButton()
        Me.grdAutorizador = New Janus.Windows.GridEX.GridEX()
        Me.grdUF = New Janus.Windows.GridEX.GridEX()
        Me.lblAviso = New System.Windows.Forms.Label()
        Me.mnuFontes = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuFonteNFe = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFonteNFSe = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFonteNoticias = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFonteOrientacoes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFonteDisponibilidade = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmrPortais = New System.Windows.Forms.Timer(Me.components)
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMain.SuspendLayout()
        Me.pagNotas.SuspendLayout()
        CType(Me.grpNFe, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpNFe.SuspendLayout()
        CType(Me.grpNFSe, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpNFSe.SuspendLayout()
        CType(Me.tabNotas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabNotas.SuspendLayout()
        Me.pagNFe.SuspendLayout()
        CType(Me.grdNFe, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pagNFSe.SuspendLayout()
        CType(Me.grdNFSe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpAgenda, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAgenda.SuspendLayout()
        CType(Me.grdAgenda, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpControl.SuspendLayout()
        Me.pagPortais.SuspendLayout()
        CType(Me.grpResumo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpResumo.SuspendLayout()
        CType(Me.grdAutorizador, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuFontes.SuspendLayout()
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
        Me.tabMain.BackColor = System.Drawing.Color.Transparent
        Me.tabMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMain.FocusOnClick = False
        Me.tabMain.Location = New System.Drawing.Point(0, 0)
        Me.tabMain.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.tabMain.Size = New System.Drawing.Size(1200, 565)
        Me.tabMain.TabIndex = 0
        Me.tabMain.TabPages.AddRange(New Janus.Windows.UI.Tab.UITabPage() {Me.pagNotas, Me.pagPortais})
        Me.tabMain.TabStop = False
        Me.tabMain.VisualStyleManager = Me.vsmMain
        '
        'pagNotas
        '
        Me.pagNotas.Controls.Add(Me.grpControl)
        Me.pagNotas.Controls.Add(Me.grpAgenda)
        Me.pagNotas.Controls.Add(Me.tabNotas)
        Me.pagNotas.Controls.Add(Me.grpNFSe)
        Me.pagNotas.Controls.Add(Me.grpNFe)
        Me.pagNotas.Key = "pagNotas"
        Me.pagNotas.Location = New System.Drawing.Point(1, 24)
        Me.pagNotas.Name = "pagNotas"
        Me.pagNotas.Size = New System.Drawing.Size(1198, 540)
        Me.pagNotas.TabStop = True
        Me.pagNotas.Text = "Notas Técnicas"
        '
        'grpNFe
        '
        Me.grpNFe.BackColor = System.Drawing.Color.Transparent
        Me.grpNFe.Controls.Add(Me.lnkNFe)
        Me.grpNFe.Controls.Add(Me.lblNFeResumo)
        Me.grpNFe.Controls.Add(Me.lblNFeData)
        Me.grpNFe.Controls.Add(Me.lblNFeNumero)
        Me.grpNFe.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpNFe.Location = New System.Drawing.Point(7, 6)
        Me.grpNFe.Name = "grpNFe"
        Me.grpNFe.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpNFe.Size = New System.Drawing.Size(588, 100)
        Me.grpNFe.TabIndex = 0
        Me.grpNFe.Text = "NF-e / NFC-e · última publicada"
        Me.grpNFe.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblNFeNumero
        '
        Me.lblNFeNumero.AutoSize = True
        Me.lblNFeNumero.BackColor = System.Drawing.Color.Transparent
        Me.lblNFeNumero.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblNFeNumero.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblNFeNumero.Location = New System.Drawing.Point(10, 20)
        Me.lblNFeNumero.Name = "lblNFeNumero"
        Me.lblNFeNumero.Size = New System.Drawing.Size(15, 19)
        Me.lblNFeNumero.TabIndex = 1
        Me.lblNFeNumero.Text = "—"
        '
        'lblNFeData
        '
        Me.lblNFeData.AutoSize = True
        Me.lblNFeData.BackColor = System.Drawing.Color.Transparent
        Me.lblNFeData.ForeColor = System.Drawing.Color.Gray
        Me.lblNFeData.Location = New System.Drawing.Point(12, 44)
        Me.lblNFeData.Name = "lblNFeData"
        Me.lblNFeData.Size = New System.Drawing.Size(10, 14)
        Me.lblNFeData.TabIndex = 2
        Me.lblNFeData.Text = "—"
        '
        'lblNFeResumo
        '
        Me.lblNFeResumo.AutoEllipsis = True
        Me.lblNFeResumo.BackColor = System.Drawing.Color.Transparent
        Me.lblNFeResumo.Location = New System.Drawing.Point(12, 62)
        Me.lblNFeResumo.Name = "lblNFeResumo"
        Me.lblNFeResumo.Size = New System.Drawing.Size(564, 32)
        Me.lblNFeResumo.TabIndex = 3
        '
        'lnkNFe
        '
        Me.lnkNFe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkNFe.BackColor = System.Drawing.Color.Transparent
        Me.lnkNFe.Location = New System.Drawing.Point(420, 22)
        Me.lnkNFe.Name = "lnkNFe"
        Me.lnkNFe.Size = New System.Drawing.Size(156, 16)
        Me.lnkNFe.TabIndex = 0
        Me.lnkNFe.TabStop = True
        Me.lnkNFe.Text = "Abrir a nota técnica"
        Me.lnkNFe.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpNFSe
        '
        Me.grpNFSe.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpNFSe.BackColor = System.Drawing.Color.Transparent
        Me.grpNFSe.Controls.Add(Me.lnkNFSe)
        Me.grpNFSe.Controls.Add(Me.lblNFSeResumo)
        Me.grpNFSe.Controls.Add(Me.lblNFSeData)
        Me.grpNFSe.Controls.Add(Me.lblNFSeNumero)
        Me.grpNFSe.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpNFSe.Location = New System.Drawing.Point(603, 6)
        Me.grpNFSe.Name = "grpNFSe"
        Me.grpNFSe.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpNFSe.Size = New System.Drawing.Size(588, 100)
        Me.grpNFSe.TabIndex = 1
        Me.grpNFSe.Text = "NFS-e nacional · última publicada"
        Me.grpNFSe.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblNFSeNumero
        '
        Me.lblNFSeNumero.AutoSize = True
        Me.lblNFSeNumero.BackColor = System.Drawing.Color.Transparent
        Me.lblNFSeNumero.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblNFSeNumero.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblNFSeNumero.Location = New System.Drawing.Point(10, 20)
        Me.lblNFSeNumero.Name = "lblNFSeNumero"
        Me.lblNFSeNumero.Size = New System.Drawing.Size(15, 19)
        Me.lblNFSeNumero.TabIndex = 1
        Me.lblNFSeNumero.Text = "—"
        '
        'lblNFSeData
        '
        Me.lblNFSeData.AutoSize = True
        Me.lblNFSeData.BackColor = System.Drawing.Color.Transparent
        Me.lblNFSeData.ForeColor = System.Drawing.Color.Gray
        Me.lblNFSeData.Location = New System.Drawing.Point(12, 44)
        Me.lblNFSeData.Name = "lblNFSeData"
        Me.lblNFSeData.Size = New System.Drawing.Size(10, 14)
        Me.lblNFSeData.TabIndex = 2
        Me.lblNFSeData.Text = "—"
        '
        'lblNFSeResumo
        '
        Me.lblNFSeResumo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNFSeResumo.AutoEllipsis = True
        Me.lblNFSeResumo.BackColor = System.Drawing.Color.Transparent
        Me.lblNFSeResumo.Location = New System.Drawing.Point(12, 62)
        Me.lblNFSeResumo.Name = "lblNFSeResumo"
        Me.lblNFSeResumo.Size = New System.Drawing.Size(564, 32)
        Me.lblNFSeResumo.TabIndex = 3
        '
        'lnkNFSe
        '
        Me.lnkNFSe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkNFSe.BackColor = System.Drawing.Color.Transparent
        Me.lnkNFSe.Location = New System.Drawing.Point(420, 22)
        Me.lnkNFSe.Name = "lnkNFSe"
        Me.lnkNFSe.Size = New System.Drawing.Size(156, 16)
        Me.lnkNFSe.TabIndex = 0
        Me.lnkNFSe.TabStop = True
        Me.lnkNFSe.Text = "Abrir a nota técnica"
        Me.lnkNFSe.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tabNotas
        '
        Me.tabNotas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabNotas.BackColor = System.Drawing.Color.Transparent
        Me.tabNotas.FocusOnClick = False
        Me.tabNotas.Location = New System.Drawing.Point(7, 112)
        Me.tabNotas.Name = "tabNotas"
        Me.tabNotas.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.tabNotas.Size = New System.Drawing.Size(780, 366)
        Me.tabNotas.TabIndex = 2
        Me.tabNotas.TabPages.AddRange(New Janus.Windows.UI.Tab.UITabPage() {Me.pagNFe, Me.pagNFSe})
        Me.tabNotas.VisualStyleManager = Me.vsmMain
        '
        'pagNFe
        '
        Me.pagNFe.Controls.Add(Me.grdNFe)
        Me.pagNFe.Key = "pagNFe"
        Me.pagNFe.Location = New System.Drawing.Point(1, 24)
        Me.pagNFe.Name = "pagNFe"
        Me.pagNFe.Size = New System.Drawing.Size(778, 341)
        Me.pagNFe.TabStop = True
        Me.pagNFe.Text = "NF-e / NFC-e"
        '
        'grdNFe
        '
        Me.grdNFe.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdNFe.AlternatingColors = True
        Me.grdNFe.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdNFe_DesignTimeLayout.LayoutString = resources.GetString("grdNFe_DesignTimeLayout.LayoutString")
        Me.grdNFe.DesignTimeLayout = grdNFe_DesignTimeLayout
        Me.grdNFe.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdNFe.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdNFe.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdNFe.GroupByBoxVisible = False
        Me.grdNFe.Location = New System.Drawing.Point(6, 6)
        Me.grdNFe.Name = "grdNFe"
        Me.grdNFe.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdNFe.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdNFe.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdNFe.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdNFe.Size = New System.Drawing.Size(766, 329)
        Me.grdNFe.TabIndex = 0
        Me.grdNFe.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'pagNFSe
        '
        Me.pagNFSe.Controls.Add(Me.grdNFSe)
        Me.pagNFSe.Key = "pagNFSe"
        Me.pagNFSe.Location = New System.Drawing.Point(1, 24)
        Me.pagNFSe.Name = "pagNFSe"
        Me.pagNFSe.Size = New System.Drawing.Size(778, 341)
        Me.pagNFSe.TabStop = True
        Me.pagNFSe.Text = "NFS-e"
        '
        'grdNFSe
        '
        Me.grdNFSe.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdNFSe.AlternatingColors = True
        Me.grdNFSe.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdNFSe_DesignTimeLayout.LayoutString = resources.GetString("grdNFSe_DesignTimeLayout.LayoutString")
        Me.grdNFSe.DesignTimeLayout = grdNFSe_DesignTimeLayout
        Me.grdNFSe.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdNFSe.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdNFSe.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdNFSe.GroupByBoxVisible = False
        Me.grdNFSe.Location = New System.Drawing.Point(6, 6)
        Me.grdNFSe.Name = "grdNFSe"
        Me.grdNFSe.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdNFSe.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdNFSe.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdNFSe.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdNFSe.Size = New System.Drawing.Size(766, 329)
        Me.grdNFSe.TabIndex = 0
        Me.grdNFSe.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grpAgenda
        '
        Me.grpAgenda.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpAgenda.BackColor = System.Drawing.Color.Transparent
        Me.grpAgenda.Controls.Add(Me.grdAgenda)
        Me.grpAgenda.Controls.Add(Me.lblProximoMarco)
        Me.grpAgenda.Controls.Add(Me.lblProximoData)
        Me.grpAgenda.Controls.Add(Me.lblProximo)
        Me.grpAgenda.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpAgenda.Location = New System.Drawing.Point(793, 112)
        Me.grpAgenda.Name = "grpAgenda"
        Me.grpAgenda.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpAgenda.Size = New System.Drawing.Size(398, 366)
        Me.grpAgenda.TabIndex = 3
        Me.grpAgenda.Text = "Agenda RTC 2026 / 2027"
        Me.grpAgenda.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblProximo
        '
        Me.lblProximo.AutoSize = True
        Me.lblProximo.BackColor = System.Drawing.Color.Transparent
        Me.lblProximo.ForeColor = System.Drawing.Color.Gray
        Me.lblProximo.Location = New System.Drawing.Point(10, 20)
        Me.lblProximo.Name = "lblProximo"
        Me.lblProximo.Size = New System.Drawing.Size(84, 14)
        Me.lblProximo.TabIndex = 0
        Me.lblProximo.Text = "Próximo marco:"
        '
        'lblProximoData
        '
        Me.lblProximoData.AutoSize = True
        Me.lblProximoData.BackColor = System.Drawing.Color.Transparent
        Me.lblProximoData.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblProximoData.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblProximoData.Location = New System.Drawing.Point(10, 36)
        Me.lblProximoData.Name = "lblProximoData"
        Me.lblProximoData.Size = New System.Drawing.Size(15, 19)
        Me.lblProximoData.TabIndex = 1
        Me.lblProximoData.Text = "—"
        '
        'lblProximoMarco
        '
        Me.lblProximoMarco.AutoEllipsis = True
        Me.lblProximoMarco.BackColor = System.Drawing.Color.Transparent
        Me.lblProximoMarco.Location = New System.Drawing.Point(12, 60)
        Me.lblProximoMarco.Name = "lblProximoMarco"
        Me.lblProximoMarco.Size = New System.Drawing.Size(376, 44)
        Me.lblProximoMarco.TabIndex = 2
        '
        'grdAgenda
        '
        Me.grdAgenda.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAgenda.AlternatingColors = True
        Me.grdAgenda.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdAgenda_DesignTimeLayout.LayoutString = resources.GetString("grdAgenda_DesignTimeLayout.LayoutString")
        Me.grdAgenda.DesignTimeLayout = grdAgenda_DesignTimeLayout
        Me.grdAgenda.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdAgenda.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdAgenda.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdAgenda.GroupByBoxVisible = False
        Me.grdAgenda.Location = New System.Drawing.Point(8, 108)
        Me.grdAgenda.Name = "grdAgenda"
        Me.grdAgenda.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdAgenda.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdAgenda.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdAgenda.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdAgenda.Size = New System.Drawing.Size(382, 250)
        Me.grdAgenda.TabIndex = 3
        Me.grdAgenda.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grpControl
        '
        Me.grpControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpControl.BackColor = System.Drawing.Color.Transparent
        Me.grpControl.Controls.Add(Me.btnAtualizar)
        Me.grpControl.Controls.Add(Me.btnFontes)
        Me.grpControl.Controls.Add(Me.lblFonte)
        Me.grpControl.Location = New System.Drawing.Point(7, 484)
        Me.grpControl.Name = "grpControl"
        Me.grpControl.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControl.Size = New System.Drawing.Size(1184, 51)
        Me.grpControl.TabIndex = 4
        Me.grpControl.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblFonte
        '
        Me.lblFonte.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFonte.AutoEllipsis = True
        Me.lblFonte.BackColor = System.Drawing.Color.Transparent
        Me.lblFonte.ForeColor = System.Drawing.Color.Gray
        Me.lblFonte.Location = New System.Drawing.Point(10, 19)
        Me.lblFonte.Name = "lblFonte"
        Me.lblFonte.Size = New System.Drawing.Size(860, 16)
        Me.lblFonte.TabIndex = 2
        Me.lblFonte.Text = "—"
        '
        'btnFontes
        '
        Me.btnFontes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFontes.Image = Global.Documentacti.My.Resources.Resources.preview
        Me.btnFontes.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnFontes.Location = New System.Drawing.Point(887, 15)
        Me.btnFontes.Name = "btnFontes"
        Me.btnFontes.Size = New System.Drawing.Size(140, 23)
        Me.btnFontes.TabIndex = 0
        Me.btnFontes.Text = "Fontes Oficiais"
        Me.btnFontes.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnAtualizar
        '
        Me.btnAtualizar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAtualizar.Image = Global.Documentacti.My.Resources.Resources.procurar
        Me.btnAtualizar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnAtualizar.Location = New System.Drawing.Point(1035, 15)
        Me.btnAtualizar.Name = "btnAtualizar"
        Me.btnAtualizar.Size = New System.Drawing.Size(140, 23)
        Me.btnAtualizar.TabIndex = 1
        Me.btnAtualizar.Text = "Atualizar Notas"
        Me.btnAtualizar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'pagPortais
        '
        Me.pagPortais.Controls.Add(Me.lblAviso)
        Me.pagPortais.Controls.Add(Me.grdUF)
        Me.pagPortais.Controls.Add(Me.grdAutorizador)
        Me.pagPortais.Controls.Add(Me.grpResumo)
        Me.pagPortais.Key = "pagPortais"
        Me.pagPortais.Location = New System.Drawing.Point(1, 24)
        Me.pagPortais.Name = "pagPortais"
        Me.pagPortais.Size = New System.Drawing.Size(1198, 540)
        Me.pagPortais.TabStop = True
        Me.pagPortais.Text = "Disponibilidade dos Portais"
        '
        'grpResumo
        '
        Me.grpResumo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpResumo.BackColor = System.Drawing.Color.Transparent
        Me.grpResumo.Controls.Add(Me.btnVerificar)
        Me.grpResumo.Controls.Add(Me.lblMedido)
        Me.grpResumo.Controls.Add(Me.lblResumo)
        Me.grpResumo.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpResumo.Location = New System.Drawing.Point(7, 6)
        Me.grpResumo.Name = "grpResumo"
        Me.grpResumo.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpResumo.Size = New System.Drawing.Size(1184, 48)
        Me.grpResumo.TabIndex = 0
        Me.grpResumo.Text = "Disponibilidade dos portais (até 1,2 s: no ar · acima: lento · sem resposta em 8 s: fora)"
        Me.grpResumo.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblResumo
        '
        Me.lblResumo.AutoEllipsis = True
        Me.lblResumo.BackColor = System.Drawing.Color.Transparent
        Me.lblResumo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblResumo.Location = New System.Drawing.Point(10, 22)
        Me.lblResumo.Name = "lblResumo"
        Me.lblResumo.Size = New System.Drawing.Size(640, 16)
        Me.lblResumo.TabIndex = 1
        Me.lblResumo.Text = "Verificando os portais..."
        '
        'lblMedido
        '
        Me.lblMedido.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMedido.BackColor = System.Drawing.Color.Transparent
        Me.lblMedido.ForeColor = System.Drawing.Color.Gray
        Me.lblMedido.Location = New System.Drawing.Point(830, 22)
        Me.lblMedido.Name = "lblMedido"
        Me.lblMedido.Size = New System.Drawing.Size(195, 16)
        Me.lblMedido.TabIndex = 2
        Me.lblMedido.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnVerificar
        '
        Me.btnVerificar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnVerificar.Image = Global.Documentacti.My.Resources.Resources.procurar
        Me.btnVerificar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnVerificar.Location = New System.Drawing.Point(1035, 17)
        Me.btnVerificar.Name = "btnVerificar"
        Me.btnVerificar.Size = New System.Drawing.Size(140, 23)
        Me.btnVerificar.TabIndex = 0
        Me.btnVerificar.Text = "Verificar Agora"
        Me.btnVerificar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'grdAutorizador
        '
        Me.grdAutorizador.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAutorizador.AlternatingColors = True
        Me.grdAutorizador.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        grdAutorizador_DesignTimeLayout.LayoutString = resources.GetString("grdAutorizador_DesignTimeLayout.LayoutString")
        Me.grdAutorizador.DesignTimeLayout = grdAutorizador_DesignTimeLayout
        Me.grdAutorizador.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdAutorizador.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdAutorizador.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdAutorizador.GroupByBoxVisible = False
        Me.grdAutorizador.Location = New System.Drawing.Point(7, 60)
        Me.grdAutorizador.Name = "grdAutorizador"
        Me.grdAutorizador.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdAutorizador.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdAutorizador.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdAutorizador.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdAutorizador.Size = New System.Drawing.Size(560, 404)
        Me.grdAutorizador.TabIndex = 1
        Me.grdAutorizador.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grdUF
        '
        Me.grdUF.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdUF.AlternatingColors = True
        Me.grdUF.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdUF_DesignTimeLayout.LayoutString = resources.GetString("grdUF_DesignTimeLayout.LayoutString")
        Me.grdUF.DesignTimeLayout = grdUF_DesignTimeLayout
        Me.grdUF.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdUF.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdUF.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdUF.GroupByBoxVisible = False
        Me.grdUF.Location = New System.Drawing.Point(573, 60)
        Me.grdUF.Name = "grdUF"
        Me.grdUF.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdUF.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdUF.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdUF.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdUF.Size = New System.Drawing.Size(618, 404)
        Me.grdUF.TabIndex = 2
        Me.grdUF.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblAviso
        '
        Me.lblAviso.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAviso.BackColor = System.Drawing.Color.Transparent
        Me.lblAviso.ForeColor = System.Drawing.Color.Gray
        Me.lblAviso.Location = New System.Drawing.Point(7, 470)
        Me.lblAviso.Name = "lblAviso"
        Me.lblAviso.Size = New System.Drawing.Size(1184, 64)
        Me.lblAviso.TabIndex = 3
        Me.lblAviso.Text = resources.GetString("lblAviso.Text")
        '
        'mnuFontes
        '
        Me.mnuFontes.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFonteNFe, Me.mnuFonteNFSe, Me.mnuFonteNoticias, Me.mnuFonteOrientacoes, Me.mnuFonteDisponibilidade})
        Me.mnuFontes.Name = "mnuFontes"
        Me.mnuFontes.Size = New System.Drawing.Size(330, 114)
        '
        'mnuFonteNFe
        '
        Me.mnuFonteNFe.Name = "mnuFonteNFe"
        Me.mnuFonteNFe.Size = New System.Drawing.Size(329, 22)
        Me.mnuFonteNFe.Text = "Notas técnicas NF-e / NFC-e (Portal Nacional da NF-e)"
        '
        'mnuFonteNFSe
        '
        Me.mnuFonteNFSe.Name = "mnuFonteNFSe"
        Me.mnuFonteNFSe.Size = New System.Drawing.Size(329, 22)
        Me.mnuFonteNFSe.Text = "Notas técnicas NFS-e - pasta RTC (gov.br/nfse)"
        '
        'mnuFonteNoticias
        '
        Me.mnuFonteNoticias.Name = "mnuFonteNoticias"
        Me.mnuFonteNoticias.Size = New System.Drawing.Size(329, 22)
        Me.mnuFonteNoticias.Text = "Notícias da NFS-e (prazos e indisponibilidade)"
        '
        'mnuFonteOrientacoes
        '
        Me.mnuFonteOrientacoes.Name = "mnuFonteOrientacoes"
        Me.mnuFonteOrientacoes.Size = New System.Drawing.Size(329, 22)
        Me.mnuFonteOrientacoes.Text = "Orientações RTC 2026 (Receita Federal)"
        '
        'mnuFonteDisponibilidade
        '
        Me.mnuFonteDisponibilidade.Name = "mnuFonteDisponibilidade"
        Me.mnuFonteDisponibilidade.Size = New System.Drawing.Size(329, 22)
        Me.mnuFonteDisponibilidade.Text = "Disponibilidade NF-e por UF (Portal Nacional da NF-e)"
        '
        'tmrPortais
        '
        Me.tmrPortais.Interval = 300000
        '
        'usrAdmMonitorFiscal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.tabMain)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "usrAdmMonitorFiscal"
        Me.Size = New System.Drawing.Size(1200, 565)
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMain.ResumeLayout(False)
        Me.pagNotas.ResumeLayout(False)
        CType(Me.grpNFe, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpNFe.ResumeLayout(False)
        Me.grpNFe.PerformLayout()
        CType(Me.grpNFSe, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpNFSe.ResumeLayout(False)
        Me.grpNFSe.PerformLayout()
        CType(Me.tabNotas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabNotas.ResumeLayout(False)
        Me.pagNFe.ResumeLayout(False)
        CType(Me.grdNFe, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pagNFSe.ResumeLayout(False)
        CType(Me.grdNFSe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpAgenda, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAgenda.ResumeLayout(False)
        Me.grpAgenda.PerformLayout()
        CType(Me.grdAgenda, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpControl.ResumeLayout(False)
        Me.pagPortais.ResumeLayout(False)
        CType(Me.grpResumo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpResumo.ResumeLayout(False)
        CType(Me.grdAutorizador, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuFontes.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents vsmMain As Janus.Windows.Common.VisualStyleManager
    Friend WithEvents tabMain As Janus.Windows.UI.Tab.UITab
    Friend WithEvents pagNotas As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents grpNFe As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblNFeNumero As System.Windows.Forms.Label
    Friend WithEvents lblNFeData As System.Windows.Forms.Label
    Friend WithEvents lblNFeResumo As System.Windows.Forms.Label
    Friend WithEvents lnkNFe As System.Windows.Forms.LinkLabel
    Friend WithEvents grpNFSe As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblNFSeNumero As System.Windows.Forms.Label
    Friend WithEvents lblNFSeData As System.Windows.Forms.Label
    Friend WithEvents lblNFSeResumo As System.Windows.Forms.Label
    Friend WithEvents lnkNFSe As System.Windows.Forms.LinkLabel
    Friend WithEvents tabNotas As Janus.Windows.UI.Tab.UITab
    Friend WithEvents pagNFe As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents grdNFe As Janus.Windows.GridEX.GridEX
    Friend WithEvents pagNFSe As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents grdNFSe As Janus.Windows.GridEX.GridEX
    Friend WithEvents grpAgenda As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblProximo As System.Windows.Forms.Label
    Friend WithEvents lblProximoData As System.Windows.Forms.Label
    Friend WithEvents lblProximoMarco As System.Windows.Forms.Label
    Friend WithEvents grdAgenda As Janus.Windows.GridEX.GridEX
    Friend WithEvents grpControl As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblFonte As System.Windows.Forms.Label
    Friend WithEvents btnFontes As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnAtualizar As Janus.Windows.EditControls.UIButton
    Friend WithEvents pagPortais As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents grpResumo As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblResumo As System.Windows.Forms.Label
    Friend WithEvents lblMedido As System.Windows.Forms.Label
    Friend WithEvents btnVerificar As Janus.Windows.EditControls.UIButton
    Friend WithEvents grdAutorizador As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdUF As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblAviso As System.Windows.Forms.Label
    Friend WithEvents mnuFontes As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuFonteNFe As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFonteNFSe As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFonteNoticias As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFonteOrientacoes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFonteDisponibilidade As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrPortais As System.Windows.Forms.Timer

End Class
