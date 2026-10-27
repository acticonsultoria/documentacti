<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class usrCadBaseConhecimento
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(usrCadBaseConhecimento))
        Dim grdListagem_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdListagem_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image")
        Dim cboEmpresa_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cboMarcador_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdArquivos_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdArquivos_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image")
        Me.tabMain = New Janus.Windows.UI.Tab.UITab()
        Me.pagLista = New Janus.Windows.UI.Tab.UITabPage()
        Me.pagDados = New Janus.Windows.UI.Tab.UITabPage()
        Me.grpFiltro = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblTituloFiltro = New System.Windows.Forms.Label()
        Me.txtTituloFiltro = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblMarcadorFiltro = New System.Windows.Forms.Label()
        Me.txtMarcadorFiltro = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblModuloFiltro = New System.Windows.Forms.Label()
        Me.cboModuloFiltro = New Janus.Windows.EditControls.UIComboBox()
        Me.lblEmpresaFiltro = New System.Windows.Forms.Label()
        Me.cboEmpresaFiltro = New Janus.Windows.EditControls.UIComboBox()
        Me.btnFiltrar = New Janus.Windows.EditControls.UIButton()
        Me.grdListagem = New Janus.Windows.GridEX.GridEX()
        Me.grpControl = New Janus.Windows.EditControls.UIGroupBox()
        Me.btnNovo = New Janus.Windows.EditControls.UIButton()
        Me.btnExcel = New Janus.Windows.EditControls.UIButton()
        Me.btnSair = New Janus.Windows.EditControls.UIButton()
        Me.grpDados = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.txtTitulo = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblModulo = New System.Windows.Forms.Label()
        Me.cboModulo = New Janus.Windows.EditControls.UIComboBox()
        Me.lblEmpresa = New System.Windows.Forms.Label()
        Me.cboEmpresa = New Janus.Windows.GridEX.EditControls.CheckedComboBox()
        Me.lblMarcador = New System.Windows.Forms.Label()
        Me.cboMarcador = New Janus.Windows.GridEX.EditControls.CheckedComboBox()
        Me.tabConteudo = New Janus.Windows.UI.Tab.UITab()
        Me.pagVisualizar = New Janus.Windows.UI.Tab.UITabPage()
        Me.pagEditar = New Janus.Windows.UI.Tab.UITabPage()
        Me.pagArquivos = New Janus.Windows.UI.Tab.UITabPage()
        Me.webDescricao = New System.Windows.Forms.WebBrowser()
        Me.txtDescricao = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.grdArquivos = New Janus.Windows.GridEX.GridEX()
        Me.grpControl2 = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblCaracteres = New System.Windows.Forms.Label()
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
        CType(Me.tabConteudo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabConteudo.SuspendLayout()
        Me.pagVisualizar.SuspendLayout()
        Me.pagEditar.SuspendLayout()
        Me.pagArquivos.SuspendLayout()
        CType(Me.grdArquivos, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pagLista.Controls.Add(Me.grdListagem)
        Me.pagLista.Controls.Add(Me.grpFiltro)
        Me.pagLista.Key = "pagLista"
        Me.pagLista.Location = New System.Drawing.Point(1, 22)
        Me.pagLista.Name = "pagLista"
        Me.pagLista.Size = New System.Drawing.Size(1198, 542)
        Me.pagLista.TabStop = True
        Me.pagLista.Text = "Base de Conhecimento"
        '
        'pagDados
        '
        Me.pagDados.Controls.Add(Me.grpControl2)
        Me.pagDados.Controls.Add(Me.tabConteudo)
        Me.pagDados.Controls.Add(Me.grpDados)
        Me.pagDados.Key = "pagDados"
        Me.pagDados.Location = New System.Drawing.Point(1, 22)
        Me.pagDados.Name = "pagDados"
        Me.pagDados.Size = New System.Drawing.Size(1198, 542)
        Me.pagDados.TabStop = True
        Me.pagDados.Text = "Dados do Artigo"
        '
        'grpFiltro
        '
        Me.grpFiltro.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFiltro.BackColor = System.Drawing.Color.Transparent
        Me.grpFiltro.Controls.Add(Me.btnFiltrar)
        Me.grpFiltro.Controls.Add(Me.cboEmpresaFiltro)
        Me.grpFiltro.Controls.Add(Me.lblEmpresaFiltro)
        Me.grpFiltro.Controls.Add(Me.cboModuloFiltro)
        Me.grpFiltro.Controls.Add(Me.lblModuloFiltro)
        Me.grpFiltro.Controls.Add(Me.txtMarcadorFiltro)
        Me.grpFiltro.Controls.Add(Me.lblMarcadorFiltro)
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
        Me.txtTituloFiltro.MaxLength = 100
        Me.txtTituloFiltro.Size = New System.Drawing.Size(350, 20)
        Me.txtTituloFiltro.TabIndex = 1
        '
        'lblMarcadorFiltro
        '
        Me.lblMarcadorFiltro.AutoSize = True
        Me.lblMarcadorFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblMarcadorFiltro.Location = New System.Drawing.Point(362, 17)
        Me.lblMarcadorFiltro.Name = "lblMarcadorFiltro"
        Me.lblMarcadorFiltro.TabIndex = 2
        Me.lblMarcadorFiltro.Text = "Marcador:"
        '
        'txtMarcadorFiltro
        '
        Me.txtMarcadorFiltro.Location = New System.Drawing.Point(365, 34)
        Me.txtMarcadorFiltro.Name = "txtMarcadorFiltro"
        Me.txtMarcadorFiltro.MaxLength = 100
        Me.txtMarcadorFiltro.Size = New System.Drawing.Size(180, 20)
        Me.txtMarcadorFiltro.TabIndex = 3
        '
        'lblModuloFiltro
        '
        Me.lblModuloFiltro.AutoSize = True
        Me.lblModuloFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblModuloFiltro.Location = New System.Drawing.Point(548, 17)
        Me.lblModuloFiltro.Name = "lblModuloFiltro"
        Me.lblModuloFiltro.TabIndex = 4
        Me.lblModuloFiltro.Text = "Módulo:"
        '
        'cboModuloFiltro
        '
        Me.cboModuloFiltro.AutoSize = False
        Me.cboModuloFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboModuloFiltro.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboModuloFiltro.Location = New System.Drawing.Point(551, 34)
        Me.cboModuloFiltro.Name = "cboModuloFiltro"
        Me.cboModuloFiltro.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboModuloFiltro.Size = New System.Drawing.Size(200, 20)
        Me.cboModuloFiltro.TabIndex = 5
        '
        'lblEmpresaFiltro
        '
        Me.lblEmpresaFiltro.AutoSize = True
        Me.lblEmpresaFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpresaFiltro.Location = New System.Drawing.Point(754, 17)
        Me.lblEmpresaFiltro.Name = "lblEmpresaFiltro"
        Me.lblEmpresaFiltro.TabIndex = 6
        Me.lblEmpresaFiltro.Text = "Empresa:"
        '
        'cboEmpresaFiltro
        '
        Me.cboEmpresaFiltro.AutoSize = False
        Me.cboEmpresaFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboEmpresaFiltro.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboEmpresaFiltro.Location = New System.Drawing.Point(757, 34)
        Me.cboEmpresaFiltro.Name = "cboEmpresaFiltro"
        Me.cboEmpresaFiltro.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboEmpresaFiltro.Size = New System.Drawing.Size(300, 20)
        Me.cboEmpresaFiltro.TabIndex = 7
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
        Me.btnFiltrar.TabIndex = 8
        Me.btnFiltrar.Text = "Filtrar"
        Me.btnFiltrar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
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
        Me.grpDados.Controls.Add(Me.cboMarcador)
        Me.grpDados.Controls.Add(Me.lblMarcador)
        Me.grpDados.Controls.Add(Me.cboEmpresa)
        Me.grpDados.Controls.Add(Me.lblEmpresa)
        Me.grpDados.Controls.Add(Me.cboModulo)
        Me.grpDados.Controls.Add(Me.lblModulo)
        Me.grpDados.Controls.Add(Me.txtTitulo)
        Me.grpDados.Controls.Add(Me.lblTitulo)
        Me.grpDados.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpDados.Location = New System.Drawing.Point(7, 6)
        Me.grpDados.Name = "grpDados"
        Me.grpDados.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpDados.Size = New System.Drawing.Size(1182, 98)
        Me.grpDados.TabIndex = 0
        Me.grpDados.Text = "Dados do Artigo"
        Me.grpDados.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.BackColor = System.Drawing.Color.Transparent
        Me.lblTitulo.Location = New System.Drawing.Point(6, 16)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Text = "Título:"
        '
        'txtTitulo
        '
        Me.txtTitulo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTitulo.Location = New System.Drawing.Point(9, 32)
        Me.txtTitulo.Name = "txtTitulo"
        Me.txtTitulo.MaxLength = 100
        Me.txtTitulo.Size = New System.Drawing.Size(900, 20)
        Me.txtTitulo.TabIndex = 1
        '
        'lblModulo
        '
        Me.lblModulo.AutoSize = True
        Me.lblModulo.BackColor = System.Drawing.Color.Transparent
        Me.lblModulo.Location = New System.Drawing.Point(912, 16)
        Me.lblModulo.Name = "lblModulo"
        Me.lblModulo.TabIndex = 2
        Me.lblModulo.Text = "Módulo:"
        '
        'cboModulo
        '
        Me.cboModulo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboModulo.AutoSize = False
        Me.cboModulo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboModulo.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboModulo.Location = New System.Drawing.Point(915, 32)
        Me.cboModulo.Name = "cboModulo"
        Me.cboModulo.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboModulo.Size = New System.Drawing.Size(258, 20)
        Me.cboModulo.TabIndex = 3
        '
        'lblEmpresa
        '
        Me.lblEmpresa.AutoSize = True
        Me.lblEmpresa.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpresa.Location = New System.Drawing.Point(6, 54)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.TabIndex = 4
        Me.lblEmpresa.Text = "Empresas:"
        '
        'cboEmpresa
        '
        Me.cboEmpresa.ButtonCancelText = "Cancelar"
        cboEmpresa_DesignTimeLayout.LayoutString = resources.GetString("cboEmpresa_DesignTimeLayout.LayoutString")
        Me.cboEmpresa.DesignTimeLayout = cboEmpresa_DesignTimeLayout
        Me.cboEmpresa.Location = New System.Drawing.Point(9, 70)
        Me.cboEmpresa.Name = "cboEmpresa"
        Me.cboEmpresa.SaveSettings = False
        Me.cboEmpresa.Size = New System.Drawing.Size(560, 20)
        Me.cboEmpresa.TabIndex = 5
        Me.cboEmpresa.ValuesDataMember = Nothing
        '
        'lblMarcador
        '
        Me.lblMarcador.AutoSize = True
        Me.lblMarcador.BackColor = System.Drawing.Color.Transparent
        Me.lblMarcador.Location = New System.Drawing.Point(572, 54)
        Me.lblMarcador.Name = "lblMarcador"
        Me.lblMarcador.TabIndex = 6
        Me.lblMarcador.Text = "Marcadores:"
        '
        'cboMarcador
        '
        Me.cboMarcador.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboMarcador.ButtonCancelText = "Cancelar"
        cboMarcador_DesignTimeLayout.LayoutString = resources.GetString("cboMarcador_DesignTimeLayout.LayoutString")
        Me.cboMarcador.DesignTimeLayout = cboMarcador_DesignTimeLayout
        Me.cboMarcador.Location = New System.Drawing.Point(575, 70)
        Me.cboMarcador.Name = "cboMarcador"
        Me.cboMarcador.SaveSettings = False
        Me.cboMarcador.Size = New System.Drawing.Size(598, 20)
        Me.cboMarcador.TabIndex = 7
        Me.cboMarcador.ValuesDataMember = Nothing
        '
        'tabConteudo
        '
        Me.tabConteudo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabConteudo.BackColor = System.Drawing.Color.Transparent
        Me.tabConteudo.Location = New System.Drawing.Point(7, 110)
        Me.tabConteudo.Name = "tabConteudo"
        Me.tabConteudo.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.tabConteudo.Size = New System.Drawing.Size(1182, 370)
        Me.tabConteudo.TabIndex = 1
        Me.tabConteudo.TabPages.AddRange(New Janus.Windows.UI.Tab.UITabPage() {Me.pagVisualizar, Me.pagEditar, Me.pagArquivos})
        Me.tabConteudo.TabStop = False
        Me.tabConteudo.VisualStyleManager = Me.vsmMain
        '
        'pagVisualizar
        '
        Me.pagVisualizar.Controls.Add(Me.webDescricao)
        Me.pagVisualizar.Key = "pagVisualizar"
        Me.pagVisualizar.Location = New System.Drawing.Point(1, 22)
        Me.pagVisualizar.Name = "pagVisualizar"
        Me.pagVisualizar.Size = New System.Drawing.Size(1180, 347)
        Me.pagVisualizar.TabStop = True
        Me.pagVisualizar.Text = "Visualizar"
        '
        'pagEditar
        '
        Me.pagEditar.Controls.Add(Me.txtDescricao)
        Me.pagEditar.Key = "pagEditar"
        Me.pagEditar.Location = New System.Drawing.Point(1, 22)
        Me.pagEditar.Name = "pagEditar"
        Me.pagEditar.Size = New System.Drawing.Size(1180, 347)
        Me.pagEditar.TabStop = True
        Me.pagEditar.Text = "Editar Descrição (HTML)"
        '
        'pagArquivos
        '
        Me.pagArquivos.Controls.Add(Me.grdArquivos)
        Me.pagArquivos.Key = "pagArquivos"
        Me.pagArquivos.Location = New System.Drawing.Point(1, 22)
        Me.pagArquivos.Name = "pagArquivos"
        Me.pagArquivos.Size = New System.Drawing.Size(1180, 347)
        Me.pagArquivos.TabStop = True
        Me.pagArquivos.Text = "Arquivos Anexados"
        '
        'webDescricao
        '
        Me.webDescricao.AllowWebBrowserDrop = False
        Me.webDescricao.Dock = System.Windows.Forms.DockStyle.Fill
        Me.webDescricao.IsWebBrowserContextMenuEnabled = False
        Me.webDescricao.Location = New System.Drawing.Point(0, 0)
        Me.webDescricao.Name = "webDescricao"
        Me.webDescricao.MinimumSize = New System.Drawing.Size(20, 20)
        Me.webDescricao.ScriptErrorsSuppressed = True
        Me.webDescricao.Size = New System.Drawing.Size(1180, 347)
        Me.webDescricao.TabIndex = 0
        Me.webDescricao.WebBrowserShortcutsEnabled = False
        '
        'txtDescricao
        '
        Me.txtDescricao.AcceptsReturn = True
        Me.txtDescricao.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDescricao.Location = New System.Drawing.Point(0, 0)
        Me.txtDescricao.Name = "txtDescricao"
        Me.txtDescricao.MaxLength = 4000
        Me.txtDescricao.Multiline = True
        Me.txtDescricao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescricao.Size = New System.Drawing.Size(1180, 347)
        Me.txtDescricao.TabIndex = 0
        '
        'grdArquivos
        '
        Me.grdArquivos.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdArquivos.AlternatingColors = True
        Me.grdArquivos.Dock = System.Windows.Forms.DockStyle.Fill
        grdArquivos_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("grdArquivos_DesignTimeLayout_Reference_0.Instance"), Object)
        grdArquivos_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {grdArquivos_DesignTimeLayout_Reference_0})
        grdArquivos_DesignTimeLayout.LayoutString = resources.GetString("grdArquivos_DesignTimeLayout.LayoutString")
        Me.grdArquivos.DesignTimeLayout = grdArquivos_DesignTimeLayout
        Me.grdArquivos.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdArquivos.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdArquivos.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdArquivos.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdArquivos.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdArquivos.GroupByBoxVisible = False
        Me.grdArquivos.Location = New System.Drawing.Point(0, 0)
        Me.grdArquivos.Name = "grdArquivos"
        Me.grdArquivos.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdArquivos.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdArquivos.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdArquivos.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdArquivos.Size = New System.Drawing.Size(1180, 347)
        Me.grdArquivos.TabIndex = 0
        Me.grdArquivos.TabStop = False
        Me.grdArquivos.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
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
        Me.grpControl2.Controls.Add(Me.lblCaracteres)
        Me.grpControl2.Location = New System.Drawing.Point(7, 486)
        Me.grpControl2.Name = "grpControl2"
        Me.grpControl2.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControl2.Size = New System.Drawing.Size(1182, 51)
        Me.grpControl2.TabIndex = 2
        Me.grpControl2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblCaracteres
        '
        Me.lblCaracteres.AutoSize = True
        Me.lblCaracteres.BackColor = System.Drawing.Color.Transparent
        Me.lblCaracteres.ForeColor = System.Drawing.Color.Blue
        Me.lblCaracteres.Location = New System.Drawing.Point(9, 22)
        Me.lblCaracteres.Name = "lblCaracteres"
        Me.lblCaracteres.TabIndex = 0
        Me.lblCaracteres.Text = "0 de 4000 caracteres"
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
        Me.btnNovo1.TabIndex = 1
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
        Me.btnSalvar.TabIndex = 2
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
        Me.btnVoltar.TabIndex = 3
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
        Me.btnSair2.TabIndex = 4
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
        'usrCadBaseConhecimento
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.tabMain)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "usrCadBaseConhecimento"
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
        CType(Me.tabConteudo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabConteudo.ResumeLayout(False)
        Me.pagVisualizar.ResumeLayout(False)
        Me.pagVisualizar.PerformLayout()
        Me.pagEditar.ResumeLayout(False)
        Me.pagEditar.PerformLayout()
        Me.pagArquivos.ResumeLayout(False)
        Me.pagArquivos.PerformLayout()
        CType(Me.grdArquivos, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lblMarcadorFiltro As System.Windows.Forms.Label
    Friend WithEvents txtMarcadorFiltro As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblModuloFiltro As System.Windows.Forms.Label
    Friend WithEvents cboModuloFiltro As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblEmpresaFiltro As System.Windows.Forms.Label
    Friend WithEvents cboEmpresaFiltro As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents btnFiltrar As Janus.Windows.EditControls.UIButton
    Friend WithEvents grdListagem As Janus.Windows.GridEX.GridEX
    Friend WithEvents grpControl As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnNovo As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnExcel As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair As Janus.Windows.EditControls.UIButton
    Friend WithEvents grpDados As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents txtTitulo As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblModulo As System.Windows.Forms.Label
    Friend WithEvents cboModulo As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblEmpresa As System.Windows.Forms.Label
    Friend WithEvents cboEmpresa As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents lblMarcador As System.Windows.Forms.Label
    Friend WithEvents cboMarcador As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents tabConteudo As Janus.Windows.UI.Tab.UITab
    Friend WithEvents pagVisualizar As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents pagEditar As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents pagArquivos As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents webDescricao As System.Windows.Forms.WebBrowser
    Friend WithEvents txtDescricao As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents grdArquivos As Janus.Windows.GridEX.GridEX
    Friend WithEvents grpControl2 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblCaracteres As System.Windows.Forms.Label
    Friend WithEvents btnNovo1 As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSalvar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnVoltar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair2 As Janus.Windows.EditControls.UIButton
    Friend WithEvents vsmMain As Janus.Windows.Common.VisualStyleManager
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip

End Class
