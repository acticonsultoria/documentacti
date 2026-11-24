<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class usrAdmAgrupamento
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
        Dim JanusColorScheme1 As Janus.Windows.Common.JanusColorScheme = New Janus.Windows.Common.JanusColorScheme()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(usrAdmAgrupamento))
        Dim grdListagem_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdListagem_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image")
        Dim grdChamados_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdChamados_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image")
        Dim grdChamados_DesignTimeLayout_Reference_1 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column1.Image")
        Me.tabMain = New Janus.Windows.UI.Tab.UITab()
        Me.pagLista = New Janus.Windows.UI.Tab.UITabPage()
        Me.pagDados = New Janus.Windows.UI.Tab.UITabPage()
        Me.grpFiltro = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblTituloFiltro = New System.Windows.Forms.Label()
        Me.txtTituloFiltro = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblAtivoFiltro = New System.Windows.Forms.Label()
        Me.cboAtivoFiltro = New Janus.Windows.EditControls.UIComboBox()
        Me.btnFiltrar = New Janus.Windows.EditControls.UIButton()
        Me.grdListagem = New Janus.Windows.GridEX.GridEX()
        Me.btnAgruparListagem = New System.Windows.Forms.Button()
        Me.grpControl = New Janus.Windows.EditControls.UIGroupBox()
        Me.btnNovo = New Janus.Windows.EditControls.UIButton()
        Me.btnExcel = New Janus.Windows.EditControls.UIButton()
        Me.btnSair = New Janus.Windows.EditControls.UIButton()
        Me.grpDados = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.txtTitulo = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.txtCodigo = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.chkAtivo = New Janus.Windows.EditControls.UICheckBox()
        Me.grpChamados = New Janus.Windows.EditControls.UIGroupBox()
        Me.grdChamados = New Janus.Windows.GridEX.GridEX()
        Me.btnAgruparChamados = New System.Windows.Forms.Button()
        Me.lblChamado = New System.Windows.Forms.Label()
        Me.txtChamado = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblMotivo = New System.Windows.Forms.Label()
        Me.txtMotivo = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.btnAdicionarChamado = New Janus.Windows.EditControls.UIButton()
        Me.grpControl2 = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblQuantidadeChamados = New System.Windows.Forms.Label()
        Me.btnNovo1 = New Janus.Windows.EditControls.UIButton()
        Me.btnSalvar = New Janus.Windows.EditControls.UIButton()
        Me.btnVoltar = New Janus.Windows.EditControls.UIButton()
        Me.btnSair2 = New Janus.Windows.EditControls.UIButton()
        Me.vsmMain = New Janus.Windows.Common.VisualStyleManager(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMain.SuspendLayout()
        Me.pagLista.SuspendLayout()
        Me.pagDados.SuspendLayout()
        CType(Me.grpFiltro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFiltro.SuspendLayout()
        CType(Me.grdListagem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpControl.SuspendLayout()
        CType(Me.grpDados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDados.SuspendLayout()
        CType(Me.grpChamados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpChamados.SuspendLayout()
        CType(Me.grdChamados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpControl2.SuspendLayout()
        Me.SuspendLayout()
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
        Me.tabMain.TabPages.AddRange(New Janus.Windows.UI.Tab.UITabPage() {Me.pagLista, Me.pagDados})
        Me.tabMain.TabStop = False
        Me.tabMain.VisualStyleManager = Me.vsmMain
        '
        'pagLista
        '
        Me.pagLista.Controls.Add(Me.grpControl)
        Me.pagLista.Controls.Add(Me.btnAgruparListagem)
        Me.pagLista.Controls.Add(Me.grdListagem)
        Me.pagLista.Controls.Add(Me.grpFiltro)
        Me.pagLista.Key = "pagLista"
        Me.pagLista.Location = New System.Drawing.Point(1, 22)
        Me.pagLista.Name = "pagLista"
        Me.pagLista.Size = New System.Drawing.Size(1198, 542)
        Me.pagLista.TabStop = True
        Me.pagLista.Text = "Agrupamentos"
        '
        'pagDados
        '
        Me.pagDados.Controls.Add(Me.grpControl2)
        Me.pagDados.Controls.Add(Me.grpChamados)
        Me.pagDados.Controls.Add(Me.grpDados)
        Me.pagDados.Key = "pagDados"
        Me.pagDados.Location = New System.Drawing.Point(1, 22)
        Me.pagDados.Name = "pagDados"
        Me.pagDados.Size = New System.Drawing.Size(1198, 542)
        Me.pagDados.TabStop = True
        Me.pagDados.Text = "Dados do Agrupamento"
        '
        'grpFiltro
        '
        Me.grpFiltro.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFiltro.BackColor = System.Drawing.Color.Transparent
        Me.grpFiltro.Controls.Add(Me.btnFiltrar)
        Me.grpFiltro.Controls.Add(Me.cboAtivoFiltro)
        Me.grpFiltro.Controls.Add(Me.lblAtivoFiltro)
        Me.grpFiltro.Controls.Add(Me.txtTituloFiltro)
        Me.grpFiltro.Controls.Add(Me.lblTituloFiltro)
        Me.grpFiltro.Location = New System.Drawing.Point(7, 6)
        Me.grpFiltro.Name = "grpFiltro"
        Me.grpFiltro.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpFiltro.Size = New System.Drawing.Size(1182, 64)
        Me.grpFiltro.TabIndex = 0
        Me.grpFiltro.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblTituloFiltro
        '
        Me.lblTituloFiltro.AutoSize = True
        Me.lblTituloFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblTituloFiltro.Location = New System.Drawing.Point(6, 17)
        Me.lblTituloFiltro.Name = "lblTituloFiltro"
        Me.lblTituloFiltro.TabIndex = 0
        Me.lblTituloFiltro.Text = "Título:"
        '
        'txtTituloFiltro
        '
        Me.txtTituloFiltro.Location = New System.Drawing.Point(9, 34)
        Me.txtTituloFiltro.Name = "txtTituloFiltro"
        Me.txtTituloFiltro.MaxLength = 200
        Me.txtTituloFiltro.Size = New System.Drawing.Size(400, 20)
        Me.txtTituloFiltro.TabIndex = 1
        '
        'lblAtivoFiltro
        '
        Me.lblAtivoFiltro.AutoSize = True
        Me.lblAtivoFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblAtivoFiltro.Location = New System.Drawing.Point(412, 17)
        Me.lblAtivoFiltro.Name = "lblAtivoFiltro"
        Me.lblAtivoFiltro.TabIndex = 2
        Me.lblAtivoFiltro.Text = "Ativo:"
        '
        'cboAtivoFiltro
        '
        Me.cboAtivoFiltro.AutoSize = False
        Me.cboAtivoFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboAtivoFiltro.Location = New System.Drawing.Point(415, 34)
        Me.cboAtivoFiltro.Name = "cboAtivoFiltro"
        Me.cboAtivoFiltro.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboAtivoFiltro.Size = New System.Drawing.Size(100, 20)
        Me.cboAtivoFiltro.TabIndex = 3
        '
        'btnFiltrar
        '
        Me.btnFiltrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFiltrar.Image = Global.Documentacti.My.Resources.Resources.procurar
        Me.btnFiltrar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnFiltrar.Location = New System.Drawing.Point(1082, 31)
        Me.btnFiltrar.Name = "btnFiltrar"
        Me.btnFiltrar.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnFiltrar.Size = New System.Drawing.Size(91, 23)
        Me.btnFiltrar.TabIndex = 4
        Me.btnFiltrar.Text = "Filtrar"
        Me.btnFiltrar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnAgruparListagem
        '
        Me.btnAgruparListagem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAgruparListagem.BackColor = System.Drawing.Color.Transparent
        Me.btnAgruparListagem.FlatAppearance.BorderSize = 0
        Me.btnAgruparListagem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAgruparListagem.Image = Global.Documentacti.My.Resources.Resources.merge
        Me.btnAgruparListagem.Location = New System.Drawing.Point(8, 463)
        Me.btnAgruparListagem.Margin = New System.Windows.Forms.Padding(0)
        Me.btnAgruparListagem.Name = "btnAgruparListagem"
        Me.btnAgruparListagem.Size = New System.Drawing.Size(24, 15)
        Me.btnAgruparListagem.TabIndex = 99
        Me.btnAgruparListagem.TabStop = False
        Me.ToolTip.SetToolTip(Me.btnAgruparListagem, "Agrupar: exibe a área para arrastar o cabeçalho da coluna e agrupar")
        Me.btnAgruparListagem.UseVisualStyleBackColor = False
        '
        'grdListagem
        '
        Me.grdListagem.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdListagem.AlternatingColors = True
        Me.grdListagem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdListagem_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("grdListagem_DesignTimeLayout_Reference_0.Instance"), Object)
        grdListagem_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {grdListagem_DesignTimeLayout_Reference_0})
        grdListagem_DesignTimeLayout.LayoutString = resources.GetString("grdListagem_DesignTimeLayout.LayoutString")
        Me.grdListagem.DesignTimeLayout = grdListagem_DesignTimeLayout
        Me.grdListagem.DynamicFiltering = True
        Me.grdListagem.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdListagem.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdListagem.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdListagem.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdListagem.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdListagem.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdListagem.GroupByBoxVisible = False
        Me.grdListagem.Location = New System.Drawing.Point(7, 76)
        Me.grdListagem.Name = "grdListagem"
        Me.grdListagem.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdListagem.RecordNavigator = True
        Me.grdListagem.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdListagem.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdListagem.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdListagem.Size = New System.Drawing.Size(1184, 404)
        Me.grdListagem.TabIndex = 1
        Me.grdListagem.TabStop = False
        Me.grdListagem.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grpControl
        '
        Me.grpControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpControl.BackColor = System.Drawing.Color.Transparent
        Me.grpControl.Controls.Add(Me.btnSair)
        Me.grpControl.Controls.Add(Me.btnExcel)
        Me.grpControl.Controls.Add(Me.btnNovo)
        Me.grpControl.Location = New System.Drawing.Point(7, 486)
        Me.grpControl.Name = "grpControl"
        Me.grpControl.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControl.Size = New System.Drawing.Size(1182, 51)
        Me.grpControl.TabIndex = 2
        Me.grpControl.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnNovo
        '
        Me.btnNovo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNovo.Image = Global.Documentacti.My.Resources.Resources.Novo
        Me.btnNovo.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnNovo.Location = New System.Drawing.Point(887, 17)
        Me.btnNovo.Name = "btnNovo"
        Me.btnNovo.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnNovo.Size = New System.Drawing.Size(91, 23)
        Me.btnNovo.TabIndex = 1
        Me.btnNovo.Text = "Novo"
        Me.btnNovo.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcel.Image = Global.Documentacti.My.Resources.Resources.excel
        Me.btnExcel.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnExcel.Location = New System.Drawing.Point(984, 17)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnExcel.Size = New System.Drawing.Size(91, 23)
        Me.btnExcel.TabIndex = 2
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSair
        '
        Me.btnSair.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSair.Image = CType(resources.GetObject("btnSair.Image"), System.Drawing.Image)
        Me.btnSair.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSair.ImageSize = New System.Drawing.Size(19, 16)
        Me.btnSair.Location = New System.Drawing.Point(1081, 17)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnSair.Size = New System.Drawing.Size(91, 23)
        Me.btnSair.TabIndex = 3
        Me.btnSair.Text = "Sair"
        Me.btnSair.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'grpDados
        '
        Me.grpDados.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpDados.BackColor = System.Drawing.Color.Transparent
        Me.grpDados.Controls.Add(Me.chkAtivo)
        Me.grpDados.Controls.Add(Me.txtTitulo)
        Me.grpDados.Controls.Add(Me.lblTitulo)
        Me.grpDados.Controls.Add(Me.txtCodigo)
        Me.grpDados.Controls.Add(Me.lblCodigo)
        Me.grpDados.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpDados.Location = New System.Drawing.Point(7, 6)
        Me.grpDados.Name = "grpDados"
        Me.grpDados.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpDados.Size = New System.Drawing.Size(1182, 64)
        Me.grpDados.TabIndex = 0
        Me.grpDados.Text = "Dados do Agrupamento"
        Me.grpDados.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.BackColor = System.Drawing.Color.Transparent
        Me.lblCodigo.Location = New System.Drawing.Point(6, 16)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.TabIndex = 0
        Me.lblCodigo.Text = "Código:"
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(9, 32)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.ReadOnly = True
        Me.txtCodigo.Size = New System.Drawing.Size(100, 20)
        Me.txtCodigo.TabIndex = 1
        Me.txtCodigo.TabStop = False
        Me.txtCodigo.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.BackColor = System.Drawing.Color.Transparent
        Me.lblTitulo.Location = New System.Drawing.Point(112, 16)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.TabIndex = 2
        Me.lblTitulo.Text = "Título:"
        '
        'txtTitulo
        '
        Me.txtTitulo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTitulo.Location = New System.Drawing.Point(115, 32)
        Me.txtTitulo.Name = "txtTitulo"
        Me.txtTitulo.MaxLength = 200
        Me.txtTitulo.Size = New System.Drawing.Size(966, 20)
        Me.txtTitulo.TabIndex = 3
        '
        'chkAtivo
        '
        Me.chkAtivo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAtivo.BackColor = System.Drawing.Color.Transparent
        Me.chkAtivo.Location = New System.Drawing.Point(1093, 32)
        Me.chkAtivo.Name = "chkAtivo"
        Me.chkAtivo.Size = New System.Drawing.Size(80, 20)
        Me.chkAtivo.TabIndex = 4
        Me.chkAtivo.Text = "Ativo"
        Me.chkAtivo.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'grpChamados
        '
        Me.grpChamados.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpChamados.BackColor = System.Drawing.Color.Transparent
        Me.grpChamados.Controls.Add(Me.btnAgruparChamados)
        Me.grpChamados.Controls.Add(Me.grdChamados)
        Me.grpChamados.Controls.Add(Me.btnAdicionarChamado)
        Me.grpChamados.Controls.Add(Me.txtMotivo)
        Me.grpChamados.Controls.Add(Me.lblMotivo)
        Me.grpChamados.Controls.Add(Me.txtChamado)
        Me.grpChamados.Controls.Add(Me.lblChamado)
        Me.grpChamados.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpChamados.Location = New System.Drawing.Point(7, 76)
        Me.grpChamados.Name = "grpChamados"
        Me.grpChamados.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpChamados.Size = New System.Drawing.Size(1182, 404)
        Me.grpChamados.TabIndex = 1
        Me.grpChamados.Text = "Chamados do agrupamento (cada inclusão exige o motivo; duplo clique na lixeira remove)"
        Me.grpChamados.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblChamado
        '
        Me.lblChamado.AutoSize = True
        Me.lblChamado.BackColor = System.Drawing.Color.Transparent
        Me.lblChamado.Location = New System.Drawing.Point(6, 16)
        Me.lblChamado.Name = "lblChamado"
        Me.lblChamado.TabIndex = 0
        Me.lblChamado.Text = "Chamado:"
        '
        'txtChamado
        '
        Me.txtChamado.Location = New System.Drawing.Point(9, 32)
        Me.txtChamado.MaxLength = 10
        Me.txtChamado.Name = "txtChamado"
        Me.txtChamado.Size = New System.Drawing.Size(100, 20)
        Me.txtChamado.TabIndex = 1
        Me.txtChamado.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.ToolTip.SetToolTip(Me.txtChamado, "Número do chamado a incluir no agrupamento")
        '
        'lblMotivo
        '
        Me.lblMotivo.AutoSize = True
        Me.lblMotivo.BackColor = System.Drawing.Color.Transparent
        Me.lblMotivo.Location = New System.Drawing.Point(112, 16)
        Me.lblMotivo.Name = "lblMotivo"
        Me.lblMotivo.TabIndex = 2
        Me.lblMotivo.Text = "Motivo da inclusão:"
        '
        'txtMotivo
        '
        Me.txtMotivo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMotivo.Location = New System.Drawing.Point(115, 32)
        Me.txtMotivo.MaxLength = 1000
        Me.txtMotivo.Name = "txtMotivo"
        Me.txtMotivo.Size = New System.Drawing.Size(961, 20)
        Me.txtMotivo.TabIndex = 3
        Me.txtMotivo.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'btnAdicionarChamado
        '
        Me.btnAdicionarChamado.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdicionarChamado.Image = Global.Documentacti.My.Resources.Resources.add
        Me.btnAdicionarChamado.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnAdicionarChamado.Location = New System.Drawing.Point(1082, 30)
        Me.btnAdicionarChamado.Name = "btnAdicionarChamado"
        Me.btnAdicionarChamado.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnAdicionarChamado.Size = New System.Drawing.Size(91, 23)
        Me.btnAdicionarChamado.TabIndex = 4
        Me.btnAdicionarChamado.Text = "Adicionar"
        Me.btnAdicionarChamado.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnAgruparChamados
        '
        Me.btnAgruparChamados.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAgruparChamados.BackColor = System.Drawing.Color.Transparent
        Me.btnAgruparChamados.FlatAppearance.BorderSize = 0
        Me.btnAgruparChamados.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAgruparChamados.Image = Global.Documentacti.My.Resources.Resources.merge
        Me.btnAgruparChamados.Location = New System.Drawing.Point(10, 378)
        Me.btnAgruparChamados.Margin = New System.Windows.Forms.Padding(0)
        Me.btnAgruparChamados.Name = "btnAgruparChamados"
        Me.btnAgruparChamados.Size = New System.Drawing.Size(24, 15)
        Me.btnAgruparChamados.TabIndex = 99
        Me.btnAgruparChamados.TabStop = False
        Me.ToolTip.SetToolTip(Me.btnAgruparChamados, "Agrupar: exibe a área para arrastar o cabeçalho da coluna e agrupar")
        Me.btnAgruparChamados.UseVisualStyleBackColor = False
        '
        'grdChamados
        '
        Me.grdChamados.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdChamados.AlternatingColors = True
        Me.grdChamados.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdChamados_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("grdChamados_DesignTimeLayout_Reference_0.Instance"), Object)
        grdChamados_DesignTimeLayout_Reference_1.Instance = CType(resources.GetObject("grdChamados_DesignTimeLayout_Reference_1.Instance"), Object)
        grdChamados_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {grdChamados_DesignTimeLayout_Reference_0, grdChamados_DesignTimeLayout_Reference_1})
        grdChamados_DesignTimeLayout.LayoutString = resources.GetString("grdChamados_DesignTimeLayout.LayoutString")
        Me.grdChamados.DesignTimeLayout = grdChamados_DesignTimeLayout
        Me.grdChamados.DynamicFiltering = True
        Me.grdChamados.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdChamados.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdChamados.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdChamados.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdChamados.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdChamados.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdChamados.GroupByBoxVisible = False
        Me.grdChamados.Location = New System.Drawing.Point(9, 58)
        Me.grdChamados.Name = "grdChamados"
        Me.grdChamados.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdChamados.RecordNavigator = True
        Me.grdChamados.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdChamados.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdChamados.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdChamados.Size = New System.Drawing.Size(1164, 337)
        Me.grdChamados.TabIndex = 5
        Me.grdChamados.TabStop = False
        Me.grdChamados.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grpControl2
        '
        Me.grpControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpControl2.BackColor = System.Drawing.Color.Transparent
        Me.grpControl2.Controls.Add(Me.btnSair2)
        Me.grpControl2.Controls.Add(Me.btnVoltar)
        Me.grpControl2.Controls.Add(Me.btnSalvar)
        Me.grpControl2.Controls.Add(Me.btnNovo1)
        Me.grpControl2.Controls.Add(Me.lblQuantidadeChamados)
        Me.grpControl2.Location = New System.Drawing.Point(7, 486)
        Me.grpControl2.Name = "grpControl2"
        Me.grpControl2.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControl2.Size = New System.Drawing.Size(1182, 51)
        Me.grpControl2.TabIndex = 2
        Me.grpControl2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblQuantidadeChamados
        '
        Me.lblQuantidadeChamados.AutoSize = True
        Me.lblQuantidadeChamados.BackColor = System.Drawing.Color.Transparent
        Me.lblQuantidadeChamados.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQuantidadeChamados.ForeColor = System.Drawing.Color.Blue
        Me.lblQuantidadeChamados.Location = New System.Drawing.Point(9, 22)
        Me.lblQuantidadeChamados.Name = "lblQuantidadeChamados"
        Me.lblQuantidadeChamados.TabIndex = 0
        Me.lblQuantidadeChamados.Text = "0 chamado(s)"
        '
        'btnNovo1
        '
        Me.btnNovo1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNovo1.Image = Global.Documentacti.My.Resources.Resources.Novo
        Me.btnNovo1.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnNovo1.Location = New System.Drawing.Point(790, 17)
        Me.btnNovo1.Name = "btnNovo1"
        Me.btnNovo1.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnNovo1.Size = New System.Drawing.Size(91, 23)
        Me.btnNovo1.TabIndex = 3
        Me.btnNovo1.Text = "Novo"
        Me.btnNovo1.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSalvar
        '
        Me.btnSalvar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSalvar.Image = Global.Documentacti.My.Resources.Resources.salvar
        Me.btnSalvar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSalvar.Location = New System.Drawing.Point(887, 17)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnSalvar.Size = New System.Drawing.Size(91, 23)
        Me.btnSalvar.TabIndex = 4
        Me.btnSalvar.Text = "Salvar"
        Me.btnSalvar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnVoltar
        '
        Me.btnVoltar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnVoltar.Image = Global.Documentacti.My.Resources.Resources.seta_esquerda
        Me.btnVoltar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnVoltar.Location = New System.Drawing.Point(984, 17)
        Me.btnVoltar.Name = "btnVoltar"
        Me.btnVoltar.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnVoltar.Size = New System.Drawing.Size(91, 23)
        Me.btnVoltar.TabIndex = 5
        Me.btnVoltar.Text = "Voltar"
        Me.btnVoltar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSair2
        '
        Me.btnSair2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSair2.Image = CType(resources.GetObject("btnSair2.Image"), System.Drawing.Image)
        Me.btnSair2.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSair2.ImageSize = New System.Drawing.Size(19, 16)
        Me.btnSair2.Location = New System.Drawing.Point(1081, 17)
        Me.btnSair2.Name = "btnSair2"
        Me.btnSair2.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnSair2.Size = New System.Drawing.Size(91, 23)
        Me.btnSair2.TabIndex = 6
        Me.btnSair2.Text = "Sair"
        Me.btnSair2.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
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
        'ToolTip
        '
        Me.ToolTip.AutomaticDelay = 0
        Me.ToolTip.AutoPopDelay = 5000
        Me.ToolTip.InitialDelay = 0
        Me.ToolTip.ReshowDelay = 0
        Me.ToolTip.ShowAlways = True
        Me.ToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'usrAdmAgrupamento
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.tabMain)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "usrAdmAgrupamento"
        Me.Size = New System.Drawing.Size(1200, 565)
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMain.ResumeLayout(False)
        Me.pagLista.ResumeLayout(False)
        Me.pagLista.PerformLayout()
        Me.pagDados.ResumeLayout(False)
        Me.pagDados.PerformLayout()
        CType(Me.grpFiltro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFiltro.ResumeLayout(False)
        Me.grpFiltro.PerformLayout()
        CType(Me.grdListagem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpControl.ResumeLayout(False)
        Me.grpControl.PerformLayout()
        CType(Me.grpDados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDados.ResumeLayout(False)
        Me.grpDados.PerformLayout()
        CType(Me.grpChamados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpChamados.ResumeLayout(False)
        Me.grpChamados.PerformLayout()
        CType(Me.grdChamados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpControl2.ResumeLayout(False)
        Me.grpControl2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabMain As Janus.Windows.UI.Tab.UITab
    Friend WithEvents pagLista As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents pagDados As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents grpFiltro As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblTituloFiltro As System.Windows.Forms.Label
    Friend WithEvents txtTituloFiltro As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblAtivoFiltro As System.Windows.Forms.Label
    Friend WithEvents cboAtivoFiltro As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents btnFiltrar As Janus.Windows.EditControls.UIButton
    Friend WithEvents grdListagem As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnAgruparListagem As System.Windows.Forms.Button
    Friend WithEvents grpControl As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnNovo As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnExcel As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair As Janus.Windows.EditControls.UIButton
    Friend WithEvents grpDados As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents txtTitulo As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents txtCodigo As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents chkAtivo As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents grpChamados As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents grdChamados As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnAgruparChamados As System.Windows.Forms.Button
    Friend WithEvents lblChamado As System.Windows.Forms.Label
    Friend WithEvents txtChamado As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblMotivo As System.Windows.Forms.Label
    Friend WithEvents txtMotivo As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents btnAdicionarChamado As Janus.Windows.EditControls.UIButton
    Friend WithEvents grpControl2 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblQuantidadeChamados As System.Windows.Forms.Label
    Friend WithEvents btnNovo1 As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSalvar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnVoltar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair2 As Janus.Windows.EditControls.UIButton
    Friend WithEvents vsmMain As Janus.Windows.Common.VisualStyleManager
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip

End Class
