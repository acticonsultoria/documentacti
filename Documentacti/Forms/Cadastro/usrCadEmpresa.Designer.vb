<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class usrCadEmpresa
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(usrCadEmpresa))
        Dim grdListagem_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdListagem_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image")
        Dim cboModulo_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdArquivos_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdArquivos_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image")
        Dim grdHistorico_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.tabMain = New Janus.Windows.UI.Tab.UITab()
        Me.pagLista = New Janus.Windows.UI.Tab.UITabPage()
        Me.pagDados = New Janus.Windows.UI.Tab.UITabPage()
        Me.grpFiltro = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblCnpjCpfFiltro = New System.Windows.Forms.Label()
        Me.txtCnpjCpfFiltro = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblRazaoSocialFiltro = New System.Windows.Forms.Label()
        Me.txtRazaoSocialFiltro = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblAtivoFiltro = New System.Windows.Forms.Label()
        Me.cboAtivoFiltro = New Janus.Windows.EditControls.UIComboBox()
        Me.lblTimeFiltro = New System.Windows.Forms.Label()
        Me.cboTimeFiltro = New Janus.Windows.EditControls.UIComboBox()
        Me.btnFiltrar = New Janus.Windows.EditControls.UIButton()
        Me.grdListagem = New Janus.Windows.GridEX.GridEX()
        Me.grpControl = New Janus.Windows.EditControls.UIGroupBox()
        Me.btnExcel = New Janus.Windows.EditControls.UIButton()
        Me.btnSair = New Janus.Windows.EditControls.UIButton()
        Me.grpDadosGerais = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblCnpjCpf = New System.Windows.Forms.Label()
        Me.txtCnpjCpf = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblRazaoSocial = New System.Windows.Forms.Label()
        Me.txtRazaoSocial = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblNomeFantasia = New System.Windows.Forms.Label()
        Me.txtNomeFantasia = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblTelefone = New System.Windows.Forms.Label()
        Me.txtTelefone = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblAtivo = New System.Windows.Forms.Label()
        Me.cboAtivo = New Janus.Windows.EditControls.UIComboBox()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.cboTime = New Janus.Windows.EditControls.UIComboBox()
        Me.lblModulo = New System.Windows.Forms.Label()
        Me.cboModulo = New Janus.Windows.GridEX.EditControls.CheckedComboBox()
        Me.lblFocalPoint = New System.Windows.Forms.Label()
        Me.txtFocalPoint = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblCelular = New System.Windows.Forms.Label()
        Me.txtCelular = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.txtEmail = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.grpTeamViewer = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblIdTeamViewer = New System.Windows.Forms.Label()
        Me.txtIdTeamViewer = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblSenhaTeamViewer = New System.Windows.Forms.Label()
        Me.txtSenhaTeamViewer = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.grpMstsc = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblIpMstsc = New System.Windows.Forms.Label()
        Me.txtIpMstsc = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblUsuarioMstsc = New System.Windows.Forms.Label()
        Me.txtUsuarioMstsc = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblSenhaMstsc = New System.Windows.Forms.Label()
        Me.txtSenhaMstsc = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.grpVpn = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblIpVpn = New System.Windows.Forms.Label()
        Me.txtIpVpn = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblUsuarioVpn = New System.Windows.Forms.Label()
        Me.txtUsuarioVpn = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblSenhaVpn = New System.Windows.Forms.Label()
        Me.txtSenhaVpn = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.lblLocalPastaServidor = New System.Windows.Forms.Label()
        Me.txtLocalPastaServidor = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.grpSql = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblServidorSql = New System.Windows.Forms.Label()
        Me.txtServidorSql = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblUsuarioSql = New System.Windows.Forms.Label()
        Me.txtUsuarioSql = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblSenhaSql = New System.Windows.Forms.Label()
        Me.txtSenhaSql = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.grpWindows = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblDominioUsuarioWindows = New System.Windows.Forms.Label()
        Me.txtDominioUsuarioWindows = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblSenhaWindows = New System.Windows.Forms.Label()
        Me.txtSenhaWindows = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.tabDadosEmpresa = New Janus.Windows.UI.Tab.UITab()
        Me.pagObservacao = New Janus.Windows.UI.Tab.UITabPage()
        Me.pagArquivos = New Janus.Windows.UI.Tab.UITabPage()
        Me.pagHistorico = New Janus.Windows.UI.Tab.UITabPage()
        Me.txtObservacao = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.grdArquivos = New Janus.Windows.GridEX.GridEX()
        Me.grdHistorico = New Janus.Windows.GridEX.GridEX()
        Me.grpControl2 = New Janus.Windows.EditControls.UIGroupBox()
        Me.chkMostrarSenha = New Janus.Windows.EditControls.UICheckBox()
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
        CType(Me.grpDadosGerais, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDadosGerais.SuspendLayout()
        CType(Me.grpTeamViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTeamViewer.SuspendLayout()
        CType(Me.grpMstsc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMstsc.SuspendLayout()
        CType(Me.grpVpn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpVpn.SuspendLayout()
        CType(Me.grpSql, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSql.SuspendLayout()
        CType(Me.grpWindows, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpWindows.SuspendLayout()
        CType(Me.tabDadosEmpresa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabDadosEmpresa.SuspendLayout()
        Me.pagObservacao.SuspendLayout()
        Me.pagArquivos.SuspendLayout()
        Me.pagHistorico.SuspendLayout()
        CType(Me.grdArquivos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdHistorico, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pagLista.Text = "Empresas"
        '
        'pagDados
        '
        Me.pagDados.Controls.Add(Me.grpControl2)
        Me.pagDados.Controls.Add(Me.tabDadosEmpresa)
        Me.pagDados.Controls.Add(Me.grpWindows)
        Me.pagDados.Controls.Add(Me.grpSql)
        Me.pagDados.Controls.Add(Me.grpVpn)
        Me.pagDados.Controls.Add(Me.grpMstsc)
        Me.pagDados.Controls.Add(Me.grpTeamViewer)
        Me.pagDados.Controls.Add(Me.grpDadosGerais)
        Me.pagDados.Key = "pagDados"
        Me.pagDados.Location = New System.Drawing.Point(1, 22)
        Me.pagDados.Name = "pagDados"
        Me.pagDados.Size = New System.Drawing.Size(1198, 542)
        Me.pagDados.TabStop = True
        Me.pagDados.Text = "Dados da Empresa"
        '
        'grpFiltro
        '
        Me.grpFiltro.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFiltro.BackColor = System.Drawing.Color.Transparent
        Me.grpFiltro.Controls.Add(Me.btnFiltrar)
        Me.grpFiltro.Controls.Add(Me.cboTimeFiltro)
        Me.grpFiltro.Controls.Add(Me.lblTimeFiltro)
        Me.grpFiltro.Controls.Add(Me.cboAtivoFiltro)
        Me.grpFiltro.Controls.Add(Me.lblAtivoFiltro)
        Me.grpFiltro.Controls.Add(Me.txtRazaoSocialFiltro)
        Me.grpFiltro.Controls.Add(Me.lblRazaoSocialFiltro)
        Me.grpFiltro.Controls.Add(Me.txtCnpjCpfFiltro)
        Me.grpFiltro.Controls.Add(Me.lblCnpjCpfFiltro)
        Me.grpFiltro.Location = New System.Drawing.Point(7, 6)
        Me.grpFiltro.Name = "grpFiltro"
        Me.grpFiltro.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpFiltro.Size = New System.Drawing.Size(1182, 64)
        Me.grpFiltro.TabIndex = 0
        Me.grpFiltro.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblCnpjCpfFiltro
        '
        Me.lblCnpjCpfFiltro.AutoSize = True
        Me.lblCnpjCpfFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblCnpjCpfFiltro.Location = New System.Drawing.Point(6, 17)
        Me.lblCnpjCpfFiltro.Name = "lblCnpjCpfFiltro"
        Me.lblCnpjCpfFiltro.TabIndex = 0
        Me.lblCnpjCpfFiltro.Text = "CNPJ/CPF:"
        '
        'txtCnpjCpfFiltro
        '
        Me.txtCnpjCpfFiltro.Location = New System.Drawing.Point(9, 34)
        Me.txtCnpjCpfFiltro.Name = "txtCnpjCpfFiltro"
        Me.txtCnpjCpfFiltro.MaxLength = 20
        Me.txtCnpjCpfFiltro.Size = New System.Drawing.Size(150, 20)
        Me.txtCnpjCpfFiltro.TabIndex = 1
        '
        'lblRazaoSocialFiltro
        '
        Me.lblRazaoSocialFiltro.AutoSize = True
        Me.lblRazaoSocialFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblRazaoSocialFiltro.Location = New System.Drawing.Point(162, 17)
        Me.lblRazaoSocialFiltro.Name = "lblRazaoSocialFiltro"
        Me.lblRazaoSocialFiltro.TabIndex = 2
        Me.lblRazaoSocialFiltro.Text = "Razão Social:"
        '
        'txtRazaoSocialFiltro
        '
        Me.txtRazaoSocialFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRazaoSocialFiltro.Location = New System.Drawing.Point(165, 34)
        Me.txtRazaoSocialFiltro.Name = "txtRazaoSocialFiltro"
        Me.txtRazaoSocialFiltro.MaxLength = 150
        Me.txtRazaoSocialFiltro.Size = New System.Drawing.Size(400, 20)
        Me.txtRazaoSocialFiltro.TabIndex = 3
        '
        'lblAtivoFiltro
        '
        Me.lblAtivoFiltro.AutoSize = True
        Me.lblAtivoFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblAtivoFiltro.Location = New System.Drawing.Point(568, 17)
        Me.lblAtivoFiltro.Name = "lblAtivoFiltro"
        Me.lblAtivoFiltro.TabIndex = 4
        Me.lblAtivoFiltro.Text = "Ativo:"
        '
        'cboAtivoFiltro
        '
        Me.cboAtivoFiltro.AutoSize = False
        Me.cboAtivoFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboAtivoFiltro.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboAtivoFiltro.Location = New System.Drawing.Point(571, 34)
        Me.cboAtivoFiltro.Name = "cboAtivoFiltro"
        Me.cboAtivoFiltro.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboAtivoFiltro.Size = New System.Drawing.Size(100, 20)
        Me.cboAtivoFiltro.TabIndex = 5
        '
        'lblTimeFiltro
        '
        Me.lblTimeFiltro.AutoSize = True
        Me.lblTimeFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblTimeFiltro.Location = New System.Drawing.Point(674, 17)
        Me.lblTimeFiltro.Name = "lblTimeFiltro"
        Me.lblTimeFiltro.TabIndex = 6
        Me.lblTimeFiltro.Text = "Time:"
        '
        'cboTimeFiltro
        '
        Me.cboTimeFiltro.AutoSize = False
        Me.cboTimeFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboTimeFiltro.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboTimeFiltro.Location = New System.Drawing.Point(677, 34)
        Me.cboTimeFiltro.Name = "cboTimeFiltro"
        Me.cboTimeFiltro.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboTimeFiltro.Size = New System.Drawing.Size(150, 20)
        Me.cboTimeFiltro.TabIndex = 7
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
        Me.grpControl.Location = New System.Drawing.Point(7, 486)
        Me.grpControl.Name = "grpControl"
        Me.grpControl.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControl.Size = New System.Drawing.Size(1182, 51)
        Me.grpControl.TabIndex = 2
        Me.grpControl.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
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
        Me.btnExcel.TabIndex = 1
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
        Me.btnSair.TabIndex = 2
        Me.btnSair.Text = "Sair"
        Me.btnSair.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'grpDadosGerais
        '
        Me.grpDadosGerais.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpDadosGerais.BackColor = System.Drawing.Color.Transparent
        Me.grpDadosGerais.Controls.Add(Me.txtEmail)
        Me.grpDadosGerais.Controls.Add(Me.lblEmail)
        Me.grpDadosGerais.Controls.Add(Me.txtCelular)
        Me.grpDadosGerais.Controls.Add(Me.lblCelular)
        Me.grpDadosGerais.Controls.Add(Me.txtFocalPoint)
        Me.grpDadosGerais.Controls.Add(Me.lblFocalPoint)
        Me.grpDadosGerais.Controls.Add(Me.cboModulo)
        Me.grpDadosGerais.Controls.Add(Me.lblModulo)
        Me.grpDadosGerais.Controls.Add(Me.cboTime)
        Me.grpDadosGerais.Controls.Add(Me.lblTime)
        Me.grpDadosGerais.Controls.Add(Me.cboAtivo)
        Me.grpDadosGerais.Controls.Add(Me.lblAtivo)
        Me.grpDadosGerais.Controls.Add(Me.txtTelefone)
        Me.grpDadosGerais.Controls.Add(Me.lblTelefone)
        Me.grpDadosGerais.Controls.Add(Me.txtNomeFantasia)
        Me.grpDadosGerais.Controls.Add(Me.lblNomeFantasia)
        Me.grpDadosGerais.Controls.Add(Me.txtRazaoSocial)
        Me.grpDadosGerais.Controls.Add(Me.lblRazaoSocial)
        Me.grpDadosGerais.Controls.Add(Me.txtCnpjCpf)
        Me.grpDadosGerais.Controls.Add(Me.lblCnpjCpf)
        Me.grpDadosGerais.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpDadosGerais.Location = New System.Drawing.Point(7, 6)
        Me.grpDadosGerais.Name = "grpDadosGerais"
        Me.grpDadosGerais.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpDadosGerais.Size = New System.Drawing.Size(1182, 98)
        Me.grpDadosGerais.TabIndex = 0
        Me.grpDadosGerais.Text = "Dados da Empresa"
        Me.grpDadosGerais.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblCnpjCpf
        '
        Me.lblCnpjCpf.AutoSize = True
        Me.lblCnpjCpf.BackColor = System.Drawing.Color.Transparent
        Me.lblCnpjCpf.Location = New System.Drawing.Point(6, 16)
        Me.lblCnpjCpf.Name = "lblCnpjCpf"
        Me.lblCnpjCpf.TabIndex = 0
        Me.lblCnpjCpf.Text = "CNPJ/CPF:"
        '
        'txtCnpjCpf
        '
        Me.txtCnpjCpf.Location = New System.Drawing.Point(9, 32)
        Me.txtCnpjCpf.Name = "txtCnpjCpf"
        Me.txtCnpjCpf.MaxLength = 20
        Me.txtCnpjCpf.Size = New System.Drawing.Size(150, 20)
        Me.txtCnpjCpf.TabIndex = 1
        '
        'lblRazaoSocial
        '
        Me.lblRazaoSocial.AutoSize = True
        Me.lblRazaoSocial.BackColor = System.Drawing.Color.Transparent
        Me.lblRazaoSocial.Location = New System.Drawing.Point(162, 16)
        Me.lblRazaoSocial.Name = "lblRazaoSocial"
        Me.lblRazaoSocial.TabIndex = 2
        Me.lblRazaoSocial.Text = "Razão Social:"
        '
        'txtRazaoSocial
        '
        Me.txtRazaoSocial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRazaoSocial.Location = New System.Drawing.Point(165, 32)
        Me.txtRazaoSocial.Name = "txtRazaoSocial"
        Me.txtRazaoSocial.MaxLength = 500
        Me.txtRazaoSocial.Size = New System.Drawing.Size(420, 20)
        Me.txtRazaoSocial.TabIndex = 3
        '
        'lblNomeFantasia
        '
        Me.lblNomeFantasia.AutoSize = True
        Me.lblNomeFantasia.BackColor = System.Drawing.Color.Transparent
        Me.lblNomeFantasia.Location = New System.Drawing.Point(588, 16)
        Me.lblNomeFantasia.Name = "lblNomeFantasia"
        Me.lblNomeFantasia.TabIndex = 4
        Me.lblNomeFantasia.Text = "Nome Fantasia:"
        '
        'txtNomeFantasia
        '
        Me.txtNomeFantasia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNomeFantasia.Location = New System.Drawing.Point(591, 32)
        Me.txtNomeFantasia.Name = "txtNomeFantasia"
        Me.txtNomeFantasia.MaxLength = 500
        Me.txtNomeFantasia.Size = New System.Drawing.Size(350, 20)
        Me.txtNomeFantasia.TabIndex = 5
        '
        'lblTelefone
        '
        Me.lblTelefone.AutoSize = True
        Me.lblTelefone.BackColor = System.Drawing.Color.Transparent
        Me.lblTelefone.Location = New System.Drawing.Point(944, 16)
        Me.lblTelefone.Name = "lblTelefone"
        Me.lblTelefone.TabIndex = 6
        Me.lblTelefone.Text = "Telefone:"
        '
        'txtTelefone
        '
        Me.txtTelefone.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTelefone.Location = New System.Drawing.Point(947, 32)
        Me.txtTelefone.Name = "txtTelefone"
        Me.txtTelefone.MaxLength = 50
        Me.txtTelefone.Size = New System.Drawing.Size(226, 20)
        Me.txtTelefone.TabIndex = 7
        '
        'lblAtivo
        '
        Me.lblAtivo.AutoSize = True
        Me.lblAtivo.BackColor = System.Drawing.Color.Transparent
        Me.lblAtivo.Location = New System.Drawing.Point(6, 54)
        Me.lblAtivo.Name = "lblAtivo"
        Me.lblAtivo.TabIndex = 8
        Me.lblAtivo.Text = "Ativo:"
        '
        'cboAtivo
        '
        Me.cboAtivo.AutoSize = False
        Me.cboAtivo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboAtivo.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboAtivo.Location = New System.Drawing.Point(9, 70)
        Me.cboAtivo.Name = "cboAtivo"
        Me.cboAtivo.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboAtivo.Size = New System.Drawing.Size(100, 20)
        Me.cboAtivo.TabIndex = 9
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.BackColor = System.Drawing.Color.Transparent
        Me.lblTime.Location = New System.Drawing.Point(112, 54)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.TabIndex = 10
        Me.lblTime.Text = "Time:"
        '
        'cboTime
        '
        Me.cboTime.AutoSize = False
        Me.cboTime.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboTime.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboTime.Location = New System.Drawing.Point(115, 70)
        Me.cboTime.Name = "cboTime"
        Me.cboTime.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboTime.Size = New System.Drawing.Size(150, 20)
        Me.cboTime.TabIndex = 11
        '
        'lblModulo
        '
        Me.lblModulo.AutoSize = True
        Me.lblModulo.BackColor = System.Drawing.Color.Transparent
        Me.lblModulo.Location = New System.Drawing.Point(268, 54)
        Me.lblModulo.Name = "lblModulo"
        Me.lblModulo.TabIndex = 12
        Me.lblModulo.Text = "Módulos:"
        '
        'cboModulo
        '
        Me.cboModulo.ButtonCancelText = "Cancelar"
        cboModulo_DesignTimeLayout.LayoutString = resources.GetString("cboModulo_DesignTimeLayout.LayoutString")
        Me.cboModulo.DesignTimeLayout = cboModulo_DesignTimeLayout
        Me.cboModulo.Location = New System.Drawing.Point(271, 70)
        Me.cboModulo.Name = "cboModulo"
        Me.cboModulo.SaveSettings = False
        Me.cboModulo.Size = New System.Drawing.Size(314, 20)
        Me.cboModulo.TabIndex = 13
        Me.cboModulo.ValuesDataMember = Nothing
        '
        'lblFocalPoint
        '
        Me.lblFocalPoint.AutoSize = True
        Me.lblFocalPoint.BackColor = System.Drawing.Color.Transparent
        Me.lblFocalPoint.Location = New System.Drawing.Point(588, 54)
        Me.lblFocalPoint.Name = "lblFocalPoint"
        Me.lblFocalPoint.TabIndex = 14
        Me.lblFocalPoint.Text = "Focal Point:"
        '
        'txtFocalPoint
        '
        Me.txtFocalPoint.Location = New System.Drawing.Point(591, 70)
        Me.txtFocalPoint.Name = "txtFocalPoint"
        Me.txtFocalPoint.MaxLength = 100
        Me.txtFocalPoint.Size = New System.Drawing.Size(200, 20)
        Me.txtFocalPoint.TabIndex = 15
        '
        'lblCelular
        '
        Me.lblCelular.AutoSize = True
        Me.lblCelular.BackColor = System.Drawing.Color.Transparent
        Me.lblCelular.Location = New System.Drawing.Point(794, 54)
        Me.lblCelular.Name = "lblCelular"
        Me.lblCelular.TabIndex = 16
        Me.lblCelular.Text = "Celular:"
        '
        'txtCelular
        '
        Me.txtCelular.Location = New System.Drawing.Point(797, 70)
        Me.txtCelular.Name = "txtCelular"
        Me.txtCelular.MaxLength = 100
        Me.txtCelular.Size = New System.Drawing.Size(144, 20)
        Me.txtCelular.TabIndex = 17
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.BackColor = System.Drawing.Color.Transparent
        Me.lblEmail.Location = New System.Drawing.Point(944, 54)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.TabIndex = 18
        Me.lblEmail.Text = "E-mail:"
        '
        'txtEmail
        '
        Me.txtEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEmail.Location = New System.Drawing.Point(947, 70)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.MaxLength = 100
        Me.txtEmail.Size = New System.Drawing.Size(226, 20)
        Me.txtEmail.TabIndex = 19
        '
        'grpTeamViewer
        '
        Me.grpTeamViewer.BackColor = System.Drawing.Color.Transparent
        Me.grpTeamViewer.Controls.Add(Me.txtSenhaTeamViewer)
        Me.grpTeamViewer.Controls.Add(Me.lblSenhaTeamViewer)
        Me.grpTeamViewer.Controls.Add(Me.txtIdTeamViewer)
        Me.grpTeamViewer.Controls.Add(Me.lblIdTeamViewer)
        Me.grpTeamViewer.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpTeamViewer.Location = New System.Drawing.Point(7, 110)
        Me.grpTeamViewer.Name = "grpTeamViewer"
        Me.grpTeamViewer.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpTeamViewer.Size = New System.Drawing.Size(386, 58)
        Me.grpTeamViewer.TabIndex = 1
        Me.grpTeamViewer.Text = "Team Viewer"
        Me.grpTeamViewer.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblIdTeamViewer
        '
        Me.lblIdTeamViewer.AutoSize = True
        Me.lblIdTeamViewer.BackColor = System.Drawing.Color.Transparent
        Me.lblIdTeamViewer.Location = New System.Drawing.Point(6, 16)
        Me.lblIdTeamViewer.Name = "lblIdTeamViewer"
        Me.lblIdTeamViewer.TabIndex = 0
        Me.lblIdTeamViewer.Text = "ID:"
        '
        'txtIdTeamViewer
        '
        Me.txtIdTeamViewer.Location = New System.Drawing.Point(9, 32)
        Me.txtIdTeamViewer.Name = "txtIdTeamViewer"
        Me.txtIdTeamViewer.MaxLength = 50
        Me.txtIdTeamViewer.Size = New System.Drawing.Size(180, 20)
        Me.txtIdTeamViewer.TabIndex = 1
        '
        'lblSenhaTeamViewer
        '
        Me.lblSenhaTeamViewer.AutoSize = True
        Me.lblSenhaTeamViewer.BackColor = System.Drawing.Color.Transparent
        Me.lblSenhaTeamViewer.Location = New System.Drawing.Point(192, 16)
        Me.lblSenhaTeamViewer.Name = "lblSenhaTeamViewer"
        Me.lblSenhaTeamViewer.TabIndex = 2
        Me.lblSenhaTeamViewer.Text = "Senha:"
        '
        'txtSenhaTeamViewer
        '
        Me.txtSenhaTeamViewer.Location = New System.Drawing.Point(195, 32)
        Me.txtSenhaTeamViewer.Name = "txtSenhaTeamViewer"
        Me.txtSenhaTeamViewer.MaxLength = 50
        Me.txtSenhaTeamViewer.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSenhaTeamViewer.Size = New System.Drawing.Size(182, 20)
        Me.txtSenhaTeamViewer.TabIndex = 3
        '
        'grpMstsc
        '
        Me.grpMstsc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpMstsc.BackColor = System.Drawing.Color.Transparent
        Me.grpMstsc.Controls.Add(Me.txtSenhaMstsc)
        Me.grpMstsc.Controls.Add(Me.lblSenhaMstsc)
        Me.grpMstsc.Controls.Add(Me.txtUsuarioMstsc)
        Me.grpMstsc.Controls.Add(Me.lblUsuarioMstsc)
        Me.grpMstsc.Controls.Add(Me.txtIpMstsc)
        Me.grpMstsc.Controls.Add(Me.lblIpMstsc)
        Me.grpMstsc.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpMstsc.Location = New System.Drawing.Point(399, 110)
        Me.grpMstsc.Name = "grpMstsc"
        Me.grpMstsc.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpMstsc.Size = New System.Drawing.Size(790, 58)
        Me.grpMstsc.TabIndex = 2
        Me.grpMstsc.Text = "Acesso Remoto - MSTSC"
        Me.grpMstsc.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblIpMstsc
        '
        Me.lblIpMstsc.AutoSize = True
        Me.lblIpMstsc.BackColor = System.Drawing.Color.Transparent
        Me.lblIpMstsc.Location = New System.Drawing.Point(6, 16)
        Me.lblIpMstsc.Name = "lblIpMstsc"
        Me.lblIpMstsc.TabIndex = 0
        Me.lblIpMstsc.Text = "IP:"
        '
        'txtIpMstsc
        '
        Me.txtIpMstsc.Location = New System.Drawing.Point(9, 32)
        Me.txtIpMstsc.Name = "txtIpMstsc"
        Me.txtIpMstsc.MaxLength = 50
        Me.txtIpMstsc.Size = New System.Drawing.Size(250, 20)
        Me.txtIpMstsc.TabIndex = 1
        '
        'lblUsuarioMstsc
        '
        Me.lblUsuarioMstsc.AutoSize = True
        Me.lblUsuarioMstsc.BackColor = System.Drawing.Color.Transparent
        Me.lblUsuarioMstsc.Location = New System.Drawing.Point(262, 16)
        Me.lblUsuarioMstsc.Name = "lblUsuarioMstsc"
        Me.lblUsuarioMstsc.TabIndex = 2
        Me.lblUsuarioMstsc.Text = "Usuário:"
        '
        'txtUsuarioMstsc
        '
        Me.txtUsuarioMstsc.Location = New System.Drawing.Point(265, 32)
        Me.txtUsuarioMstsc.Name = "txtUsuarioMstsc"
        Me.txtUsuarioMstsc.MaxLength = 50
        Me.txtUsuarioMstsc.Size = New System.Drawing.Size(250, 20)
        Me.txtUsuarioMstsc.TabIndex = 3
        '
        'lblSenhaMstsc
        '
        Me.lblSenhaMstsc.AutoSize = True
        Me.lblSenhaMstsc.BackColor = System.Drawing.Color.Transparent
        Me.lblSenhaMstsc.Location = New System.Drawing.Point(518, 16)
        Me.lblSenhaMstsc.Name = "lblSenhaMstsc"
        Me.lblSenhaMstsc.TabIndex = 4
        Me.lblSenhaMstsc.Text = "Senha:"
        '
        'txtSenhaMstsc
        '
        Me.txtSenhaMstsc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSenhaMstsc.Location = New System.Drawing.Point(521, 32)
        Me.txtSenhaMstsc.Name = "txtSenhaMstsc"
        Me.txtSenhaMstsc.MaxLength = 50
        Me.txtSenhaMstsc.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSenhaMstsc.Size = New System.Drawing.Size(260, 20)
        Me.txtSenhaMstsc.TabIndex = 5
        '
        'grpVpn
        '
        Me.grpVpn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpVpn.BackColor = System.Drawing.Color.Transparent
        Me.grpVpn.Controls.Add(Me.txtLocalPastaServidor)
        Me.grpVpn.Controls.Add(Me.lblLocalPastaServidor)
        Me.grpVpn.Controls.Add(Me.txtSenhaVpn)
        Me.grpVpn.Controls.Add(Me.lblSenhaVpn)
        Me.grpVpn.Controls.Add(Me.txtUsuarioVpn)
        Me.grpVpn.Controls.Add(Me.lblUsuarioVpn)
        Me.grpVpn.Controls.Add(Me.txtIpVpn)
        Me.grpVpn.Controls.Add(Me.lblIpVpn)
        Me.grpVpn.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpVpn.Location = New System.Drawing.Point(7, 172)
        Me.grpVpn.Name = "grpVpn"
        Me.grpVpn.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpVpn.Size = New System.Drawing.Size(1182, 58)
        Me.grpVpn.TabIndex = 3
        Me.grpVpn.Text = "VPN"
        Me.grpVpn.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblIpVpn
        '
        Me.lblIpVpn.AutoSize = True
        Me.lblIpVpn.BackColor = System.Drawing.Color.Transparent
        Me.lblIpVpn.Location = New System.Drawing.Point(6, 16)
        Me.lblIpVpn.Name = "lblIpVpn"
        Me.lblIpVpn.TabIndex = 0
        Me.lblIpVpn.Text = "IP:"
        '
        'txtIpVpn
        '
        Me.txtIpVpn.Location = New System.Drawing.Point(9, 32)
        Me.txtIpVpn.Name = "txtIpVpn"
        Me.txtIpVpn.MaxLength = 50
        Me.txtIpVpn.Size = New System.Drawing.Size(200, 20)
        Me.txtIpVpn.TabIndex = 1
        '
        'lblUsuarioVpn
        '
        Me.lblUsuarioVpn.AutoSize = True
        Me.lblUsuarioVpn.BackColor = System.Drawing.Color.Transparent
        Me.lblUsuarioVpn.Location = New System.Drawing.Point(212, 16)
        Me.lblUsuarioVpn.Name = "lblUsuarioVpn"
        Me.lblUsuarioVpn.TabIndex = 2
        Me.lblUsuarioVpn.Text = "Usuário:"
        '
        'txtUsuarioVpn
        '
        Me.txtUsuarioVpn.Location = New System.Drawing.Point(215, 32)
        Me.txtUsuarioVpn.Name = "txtUsuarioVpn"
        Me.txtUsuarioVpn.MaxLength = 50
        Me.txtUsuarioVpn.Size = New System.Drawing.Size(200, 20)
        Me.txtUsuarioVpn.TabIndex = 3
        '
        'lblSenhaVpn
        '
        Me.lblSenhaVpn.AutoSize = True
        Me.lblSenhaVpn.BackColor = System.Drawing.Color.Transparent
        Me.lblSenhaVpn.Location = New System.Drawing.Point(418, 16)
        Me.lblSenhaVpn.Name = "lblSenhaVpn"
        Me.lblSenhaVpn.TabIndex = 4
        Me.lblSenhaVpn.Text = "Senha:"
        '
        'txtSenhaVpn
        '
        Me.txtSenhaVpn.Location = New System.Drawing.Point(421, 32)
        Me.txtSenhaVpn.Name = "txtSenhaVpn"
        Me.txtSenhaVpn.MaxLength = 50
        Me.txtSenhaVpn.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSenhaVpn.Size = New System.Drawing.Size(200, 20)
        Me.txtSenhaVpn.TabIndex = 5
        '
        'lblLocalPastaServidor
        '
        Me.lblLocalPastaServidor.AutoSize = True
        Me.lblLocalPastaServidor.BackColor = System.Drawing.Color.Transparent
        Me.lblLocalPastaServidor.Location = New System.Drawing.Point(624, 16)
        Me.lblLocalPastaServidor.Name = "lblLocalPastaServidor"
        Me.lblLocalPastaServidor.TabIndex = 6
        Me.lblLocalPastaServidor.Text = "Local da Pasta no Servidor:"
        '
        'txtLocalPastaServidor
        '
        Me.txtLocalPastaServidor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLocalPastaServidor.Location = New System.Drawing.Point(627, 32)
        Me.txtLocalPastaServidor.Name = "txtLocalPastaServidor"
        Me.txtLocalPastaServidor.MaxLength = 100
        Me.txtLocalPastaServidor.Size = New System.Drawing.Size(546, 20)
        Me.txtLocalPastaServidor.TabIndex = 7
        '
        'grpSql
        '
        Me.grpSql.BackColor = System.Drawing.Color.Transparent
        Me.grpSql.Controls.Add(Me.txtSenhaSql)
        Me.grpSql.Controls.Add(Me.lblSenhaSql)
        Me.grpSql.Controls.Add(Me.txtUsuarioSql)
        Me.grpSql.Controls.Add(Me.lblUsuarioSql)
        Me.grpSql.Controls.Add(Me.txtServidorSql)
        Me.grpSql.Controls.Add(Me.lblServidorSql)
        Me.grpSql.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpSql.Location = New System.Drawing.Point(7, 234)
        Me.grpSql.Name = "grpSql"
        Me.grpSql.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpSql.Size = New System.Drawing.Size(588, 58)
        Me.grpSql.TabIndex = 4
        Me.grpSql.Text = "SQL Server"
        Me.grpSql.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblServidorSql
        '
        Me.lblServidorSql.AutoSize = True
        Me.lblServidorSql.BackColor = System.Drawing.Color.Transparent
        Me.lblServidorSql.Location = New System.Drawing.Point(6, 16)
        Me.lblServidorSql.Name = "lblServidorSql"
        Me.lblServidorSql.TabIndex = 0
        Me.lblServidorSql.Text = "Servidor:"
        '
        'txtServidorSql
        '
        Me.txtServidorSql.Location = New System.Drawing.Point(9, 32)
        Me.txtServidorSql.Name = "txtServidorSql"
        Me.txtServidorSql.MaxLength = 50
        Me.txtServidorSql.Size = New System.Drawing.Size(250, 20)
        Me.txtServidorSql.TabIndex = 1
        '
        'lblUsuarioSql
        '
        Me.lblUsuarioSql.AutoSize = True
        Me.lblUsuarioSql.BackColor = System.Drawing.Color.Transparent
        Me.lblUsuarioSql.Location = New System.Drawing.Point(262, 16)
        Me.lblUsuarioSql.Name = "lblUsuarioSql"
        Me.lblUsuarioSql.TabIndex = 2
        Me.lblUsuarioSql.Text = "Usuário:"
        '
        'txtUsuarioSql
        '
        Me.txtUsuarioSql.Location = New System.Drawing.Point(265, 32)
        Me.txtUsuarioSql.Name = "txtUsuarioSql"
        Me.txtUsuarioSql.MaxLength = 50
        Me.txtUsuarioSql.Size = New System.Drawing.Size(150, 20)
        Me.txtUsuarioSql.TabIndex = 3
        '
        'lblSenhaSql
        '
        Me.lblSenhaSql.AutoSize = True
        Me.lblSenhaSql.BackColor = System.Drawing.Color.Transparent
        Me.lblSenhaSql.Location = New System.Drawing.Point(418, 16)
        Me.lblSenhaSql.Name = "lblSenhaSql"
        Me.lblSenhaSql.TabIndex = 4
        Me.lblSenhaSql.Text = "Senha:"
        '
        'txtSenhaSql
        '
        Me.txtSenhaSql.Location = New System.Drawing.Point(421, 32)
        Me.txtSenhaSql.Name = "txtSenhaSql"
        Me.txtSenhaSql.MaxLength = 50
        Me.txtSenhaSql.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSenhaSql.Size = New System.Drawing.Size(158, 20)
        Me.txtSenhaSql.TabIndex = 5
        '
        'grpWindows
        '
        Me.grpWindows.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpWindows.BackColor = System.Drawing.Color.Transparent
        Me.grpWindows.Controls.Add(Me.txtSenhaWindows)
        Me.grpWindows.Controls.Add(Me.lblSenhaWindows)
        Me.grpWindows.Controls.Add(Me.txtDominioUsuarioWindows)
        Me.grpWindows.Controls.Add(Me.lblDominioUsuarioWindows)
        Me.grpWindows.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpWindows.Location = New System.Drawing.Point(601, 234)
        Me.grpWindows.Name = "grpWindows"
        Me.grpWindows.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpWindows.Size = New System.Drawing.Size(588, 58)
        Me.grpWindows.TabIndex = 5
        Me.grpWindows.Text = "Windows"
        Me.grpWindows.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblDominioUsuarioWindows
        '
        Me.lblDominioUsuarioWindows.AutoSize = True
        Me.lblDominioUsuarioWindows.BackColor = System.Drawing.Color.Transparent
        Me.lblDominioUsuarioWindows.Location = New System.Drawing.Point(6, 16)
        Me.lblDominioUsuarioWindows.Name = "lblDominioUsuarioWindows"
        Me.lblDominioUsuarioWindows.TabIndex = 0
        Me.lblDominioUsuarioWindows.Text = "Domínio / Usuário:"
        '
        'txtDominioUsuarioWindows
        '
        Me.txtDominioUsuarioWindows.Location = New System.Drawing.Point(9, 32)
        Me.txtDominioUsuarioWindows.Name = "txtDominioUsuarioWindows"
        Me.txtDominioUsuarioWindows.MaxLength = 100
        Me.txtDominioUsuarioWindows.Size = New System.Drawing.Size(330, 20)
        Me.txtDominioUsuarioWindows.TabIndex = 1
        '
        'lblSenhaWindows
        '
        Me.lblSenhaWindows.AutoSize = True
        Me.lblSenhaWindows.BackColor = System.Drawing.Color.Transparent
        Me.lblSenhaWindows.Location = New System.Drawing.Point(342, 16)
        Me.lblSenhaWindows.Name = "lblSenhaWindows"
        Me.lblSenhaWindows.TabIndex = 2
        Me.lblSenhaWindows.Text = "Senha:"
        '
        'txtSenhaWindows
        '
        Me.txtSenhaWindows.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSenhaWindows.Location = New System.Drawing.Point(345, 32)
        Me.txtSenhaWindows.Name = "txtSenhaWindows"
        Me.txtSenhaWindows.MaxLength = 100
        Me.txtSenhaWindows.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSenhaWindows.Size = New System.Drawing.Size(234, 20)
        Me.txtSenhaWindows.TabIndex = 3
        '
        'tabDadosEmpresa
        '
        Me.tabDadosEmpresa.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabDadosEmpresa.BackColor = System.Drawing.Color.Transparent
        Me.tabDadosEmpresa.Location = New System.Drawing.Point(7, 298)
        Me.tabDadosEmpresa.Name = "tabDadosEmpresa"
        Me.tabDadosEmpresa.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.tabDadosEmpresa.Size = New System.Drawing.Size(1182, 182)
        Me.tabDadosEmpresa.TabIndex = 6
        Me.tabDadosEmpresa.TabPages.AddRange(New Janus.Windows.UI.Tab.UITabPage() {Me.pagObservacao, Me.pagArquivos, Me.pagHistorico})
        Me.tabDadosEmpresa.TabStop = False
        Me.tabDadosEmpresa.VisualStyleManager = Me.vsmMain
        '
        'pagObservacao
        '
        Me.pagObservacao.Controls.Add(Me.txtObservacao)
        Me.pagObservacao.Key = "pagObservacao"
        Me.pagObservacao.Location = New System.Drawing.Point(1, 22)
        Me.pagObservacao.Name = "pagObservacao"
        Me.pagObservacao.Size = New System.Drawing.Size(1180, 159)
        Me.pagObservacao.TabStop = True
        Me.pagObservacao.Text = "Observação"
        '
        'pagArquivos
        '
        Me.pagArquivos.Controls.Add(Me.grdArquivos)
        Me.pagArquivos.Key = "pagArquivos"
        Me.pagArquivos.Location = New System.Drawing.Point(1, 22)
        Me.pagArquivos.Name = "pagArquivos"
        Me.pagArquivos.Size = New System.Drawing.Size(1180, 159)
        Me.pagArquivos.TabStop = True
        Me.pagArquivos.Text = "Arquivos Anexados"
        '
        'pagHistorico
        '
        Me.pagHistorico.Controls.Add(Me.grdHistorico)
        Me.pagHistorico.Key = "pagHistorico"
        Me.pagHistorico.Location = New System.Drawing.Point(1, 22)
        Me.pagHistorico.Name = "pagHistorico"
        Me.pagHistorico.Size = New System.Drawing.Size(1180, 159)
        Me.pagHistorico.TabStop = True
        Me.pagHistorico.Text = "Histórico"
        '
        'txtObservacao
        '
        Me.txtObservacao.AcceptsReturn = True
        Me.txtObservacao.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtObservacao.Location = New System.Drawing.Point(0, 0)
        Me.txtObservacao.Name = "txtObservacao"
        Me.txtObservacao.MaxLength = 4000
        Me.txtObservacao.Multiline = True
        Me.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservacao.Size = New System.Drawing.Size(1180, 159)
        Me.txtObservacao.TabIndex = 0
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
        Me.grdArquivos.Size = New System.Drawing.Size(1180, 159)
        Me.grdArquivos.TabIndex = 0
        Me.grdArquivos.TabStop = False
        Me.grdArquivos.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grdHistorico
        '
        Me.grdHistorico.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdHistorico.AlternatingColors = True
        Me.grdHistorico.Dock = System.Windows.Forms.DockStyle.Fill
        grdHistorico_DesignTimeLayout.LayoutString = resources.GetString("grdHistorico_DesignTimeLayout.LayoutString")
        Me.grdHistorico.DesignTimeLayout = grdHistorico_DesignTimeLayout
        Me.grdHistorico.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdHistorico.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdHistorico.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdHistorico.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdHistorico.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdHistorico.GroupByBoxVisible = False
        Me.grdHistorico.Location = New System.Drawing.Point(0, 0)
        Me.grdHistorico.Name = "grdHistorico"
        Me.grdHistorico.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdHistorico.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdHistorico.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdHistorico.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdHistorico.Size = New System.Drawing.Size(1180, 159)
        Me.grdHistorico.TabIndex = 0
        Me.grdHistorico.TabStop = False
        Me.grdHistorico.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grpControl2
        '
        Me.grpControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpControl2.BackColor = System.Drawing.Color.Transparent
        Me.grpControl2.Controls.Add(Me.btnSair2)
        Me.grpControl2.Controls.Add(Me.btnVoltar)
        Me.grpControl2.Controls.Add(Me.btnSalvar)
        Me.grpControl2.Controls.Add(Me.chkMostrarSenha)
        Me.grpControl2.Location = New System.Drawing.Point(7, 486)
        Me.grpControl2.Name = "grpControl2"
        Me.grpControl2.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControl2.Size = New System.Drawing.Size(1182, 51)
        Me.grpControl2.TabIndex = 7
        Me.grpControl2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'chkMostrarSenha
        '
        Me.chkMostrarSenha.BackColor = System.Drawing.Color.Transparent
        Me.chkMostrarSenha.Location = New System.Drawing.Point(9, 18)
        Me.chkMostrarSenha.Name = "chkMostrarSenha"
        Me.chkMostrarSenha.Size = New System.Drawing.Size(150, 20)
        Me.chkMostrarSenha.TabIndex = 0
        Me.chkMostrarSenha.Text = "Mostrar senhas"
        Me.chkMostrarSenha.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
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
        Me.btnSalvar.TabIndex = 1
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
        Me.btnVoltar.TabIndex = 2
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
        Me.btnSair2.TabIndex = 3
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
        'usrCadEmpresa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.tabMain)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "usrCadEmpresa"
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
        CType(Me.grpDadosGerais, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDadosGerais.ResumeLayout(False)
        Me.grpDadosGerais.PerformLayout()
        CType(Me.grpTeamViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTeamViewer.ResumeLayout(False)
        Me.grpTeamViewer.PerformLayout()
        CType(Me.grpMstsc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMstsc.ResumeLayout(False)
        Me.grpMstsc.PerformLayout()
        CType(Me.grpVpn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpVpn.ResumeLayout(False)
        Me.grpVpn.PerformLayout()
        CType(Me.grpSql, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSql.ResumeLayout(False)
        Me.grpSql.PerformLayout()
        CType(Me.grpWindows, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpWindows.ResumeLayout(False)
        Me.grpWindows.PerformLayout()
        CType(Me.tabDadosEmpresa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabDadosEmpresa.ResumeLayout(False)
        Me.pagObservacao.ResumeLayout(False)
        Me.pagObservacao.PerformLayout()
        Me.pagArquivos.ResumeLayout(False)
        Me.pagArquivos.PerformLayout()
        Me.pagHistorico.ResumeLayout(False)
        Me.pagHistorico.PerformLayout()
        CType(Me.grdArquivos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdHistorico, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpControl2.ResumeLayout(False)
        Me.grpControl2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabMain As Janus.Windows.UI.Tab.UITab
    Friend WithEvents pagLista As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents pagDados As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents grpFiltro As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblCnpjCpfFiltro As System.Windows.Forms.Label
    Friend WithEvents txtCnpjCpfFiltro As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblRazaoSocialFiltro As System.Windows.Forms.Label
    Friend WithEvents txtRazaoSocialFiltro As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblAtivoFiltro As System.Windows.Forms.Label
    Friend WithEvents cboAtivoFiltro As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblTimeFiltro As System.Windows.Forms.Label
    Friend WithEvents cboTimeFiltro As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents btnFiltrar As Janus.Windows.EditControls.UIButton
    Friend WithEvents grdListagem As Janus.Windows.GridEX.GridEX
    Friend WithEvents grpControl As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnExcel As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair As Janus.Windows.EditControls.UIButton
    Friend WithEvents grpDadosGerais As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblCnpjCpf As System.Windows.Forms.Label
    Friend WithEvents txtCnpjCpf As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblRazaoSocial As System.Windows.Forms.Label
    Friend WithEvents txtRazaoSocial As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblNomeFantasia As System.Windows.Forms.Label
    Friend WithEvents txtNomeFantasia As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblTelefone As System.Windows.Forms.Label
    Friend WithEvents txtTelefone As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblAtivo As System.Windows.Forms.Label
    Friend WithEvents cboAtivo As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents cboTime As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblModulo As System.Windows.Forms.Label
    Friend WithEvents cboModulo As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents lblFocalPoint As System.Windows.Forms.Label
    Friend WithEvents txtFocalPoint As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblCelular As System.Windows.Forms.Label
    Friend WithEvents txtCelular As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents txtEmail As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents grpTeamViewer As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblIdTeamViewer As System.Windows.Forms.Label
    Friend WithEvents txtIdTeamViewer As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblSenhaTeamViewer As System.Windows.Forms.Label
    Friend WithEvents txtSenhaTeamViewer As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents grpMstsc As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblIpMstsc As System.Windows.Forms.Label
    Friend WithEvents txtIpMstsc As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblUsuarioMstsc As System.Windows.Forms.Label
    Friend WithEvents txtUsuarioMstsc As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblSenhaMstsc As System.Windows.Forms.Label
    Friend WithEvents txtSenhaMstsc As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents grpVpn As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblIpVpn As System.Windows.Forms.Label
    Friend WithEvents txtIpVpn As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblUsuarioVpn As System.Windows.Forms.Label
    Friend WithEvents txtUsuarioVpn As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblSenhaVpn As System.Windows.Forms.Label
    Friend WithEvents txtSenhaVpn As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents lblLocalPastaServidor As System.Windows.Forms.Label
    Friend WithEvents txtLocalPastaServidor As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents grpSql As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblServidorSql As System.Windows.Forms.Label
    Friend WithEvents txtServidorSql As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblUsuarioSql As System.Windows.Forms.Label
    Friend WithEvents txtUsuarioSql As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblSenhaSql As System.Windows.Forms.Label
    Friend WithEvents txtSenhaSql As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents grpWindows As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblDominioUsuarioWindows As System.Windows.Forms.Label
    Friend WithEvents txtDominioUsuarioWindows As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblSenhaWindows As System.Windows.Forms.Label
    Friend WithEvents txtSenhaWindows As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents tabDadosEmpresa As Janus.Windows.UI.Tab.UITab
    Friend WithEvents pagObservacao As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents pagArquivos As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents pagHistorico As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents txtObservacao As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents grdArquivos As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdHistorico As Janus.Windows.GridEX.GridEX
    Friend WithEvents grpControl2 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents chkMostrarSenha As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents btnSalvar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnVoltar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair2 As Janus.Windows.EditControls.UIButton
    Friend WithEvents vsmMain As Janus.Windows.Common.VisualStyleManager
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip

End Class
