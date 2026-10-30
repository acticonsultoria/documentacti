<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class usrRelApontamento
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(usrRelApontamento))
        Dim grdAtual_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdHistorico_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cboUsuarioHistorico_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.tabMain = New Janus.Windows.UI.Tab.UITab()
        Me.pagAtual = New Janus.Windows.UI.Tab.UITabPage()
        Me.pagHistorico = New Janus.Windows.UI.Tab.UITabPage()
        Me.grpFiltroAtual = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblTimeAtual = New System.Windows.Forms.Label()
        Me.cboTimeAtual = New Janus.Windows.EditControls.UIComboBox()
        Me.lblResumoAtual = New System.Windows.Forms.Label()
        Me.btnAtualizarAtual = New Janus.Windows.EditControls.UIButton()
        Me.grdAtual = New Janus.Windows.GridEX.GridEX()
        Me.btnAgruparAtual = New System.Windows.Forms.Button()
        Me.grpControlAtual = New Janus.Windows.EditControls.UIGroupBox()
        Me.btnExcelAtual = New Janus.Windows.EditControls.UIButton()
        Me.btnSairAtual = New Janus.Windows.EditControls.UIButton()
        Me.grpFiltroHistorico = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblPeriodoHistorico = New System.Windows.Forms.Label()
        Me.dtpInicioHistorico = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.dtpTerminoHistorico = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.lblTimeHistorico = New System.Windows.Forms.Label()
        Me.cboTimeHistorico = New Janus.Windows.EditControls.UIComboBox()
        Me.lblUsuarioHistorico = New System.Windows.Forms.Label()
        Me.cboUsuarioHistorico = New Janus.Windows.GridEX.EditControls.CheckedComboBox()
        Me.lblChamadoHistorico = New System.Windows.Forms.Label()
        Me.txtChamadoHistorico = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblEmpresaHistorico = New System.Windows.Forms.Label()
        Me.cboEmpresaHistorico = New Janus.Windows.EditControls.UIComboBox()
        Me.lblTipoAtendimentoHistorico = New System.Windows.Forms.Label()
        Me.cboTipoAtendimentoHistorico = New Janus.Windows.EditControls.UIComboBox()
        Me.lblSituacaoHistorico = New System.Windows.Forms.Label()
        Me.cboSituacaoHistorico = New Janus.Windows.EditControls.UIComboBox()
        Me.lblTotalHistorico = New System.Windows.Forms.Label()
        Me.btnFiltrarHistorico = New Janus.Windows.EditControls.UIButton()
        Me.grdHistorico = New Janus.Windows.GridEX.GridEX()
        Me.btnAgruparHistorico = New System.Windows.Forms.Button()
        Me.grpControlHistorico = New Janus.Windows.EditControls.UIGroupBox()
        Me.btnExcelHistorico = New Janus.Windows.EditControls.UIButton()
        Me.btnSairHistorico = New Janus.Windows.EditControls.UIButton()
        Me.vsmMain = New Janus.Windows.Common.VisualStyleManager(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.tmrAtual = New System.Windows.Forms.Timer(Me.components)
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMain.SuspendLayout()
        Me.pagAtual.SuspendLayout()
        Me.pagHistorico.SuspendLayout()
        CType(Me.grpFiltroAtual, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFiltroAtual.SuspendLayout()
        CType(Me.grpControlAtual, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpControlAtual.SuspendLayout()
        CType(Me.grpFiltroHistorico, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFiltroHistorico.SuspendLayout()
        CType(Me.grpControlHistorico, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpControlHistorico.SuspendLayout()
        CType(Me.grdAtual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdHistorico, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.tabMain.TabPages.AddRange(New Janus.Windows.UI.Tab.UITabPage() {Me.pagAtual, Me.pagHistorico})
        Me.tabMain.TabStop = False
        Me.tabMain.VisualStyleManager = Me.vsmMain
        '
        'pagAtual
        '
        Me.pagAtual.Controls.Add(Me.btnAgruparAtual)
        Me.pagAtual.Controls.Add(Me.grpControlAtual)
        Me.pagAtual.Controls.Add(Me.grdAtual)
        Me.pagAtual.Controls.Add(Me.grpFiltroAtual)
        Me.pagAtual.Key = "pagAtual"
        Me.pagAtual.Location = New System.Drawing.Point(1, 22)
        Me.pagAtual.Name = "pagAtual"
        Me.pagAtual.Size = New System.Drawing.Size(1198, 542)
        Me.pagAtual.TabStop = True
        Me.pagAtual.Text = "Apontamentos Atuais"
        '
        'pagHistorico
        '
        Me.pagHistorico.Controls.Add(Me.btnAgruparHistorico)
        Me.pagHistorico.Controls.Add(Me.grpControlHistorico)
        Me.pagHistorico.Controls.Add(Me.grdHistorico)
        Me.pagHistorico.Controls.Add(Me.grpFiltroHistorico)
        Me.pagHistorico.Key = "pagHistorico"
        Me.pagHistorico.Location = New System.Drawing.Point(1, 22)
        Me.pagHistorico.Name = "pagHistorico"
        Me.pagHistorico.Size = New System.Drawing.Size(1198, 542)
        Me.pagHistorico.TabStop = True
        Me.pagHistorico.Text = "Histórico"
        '
        'grpFiltroAtual
        '
        Me.grpFiltroAtual.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFiltroAtual.BackColor = System.Drawing.Color.Transparent
        Me.grpFiltroAtual.Controls.Add(Me.lblResumoAtual)
        Me.grpFiltroAtual.Controls.Add(Me.btnAtualizarAtual)
        Me.grpFiltroAtual.Controls.Add(Me.cboTimeAtual)
        Me.grpFiltroAtual.Controls.Add(Me.lblTimeAtual)
        Me.grpFiltroAtual.Location = New System.Drawing.Point(7, 6)
        Me.grpFiltroAtual.Name = "grpFiltroAtual"
        Me.grpFiltroAtual.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpFiltroAtual.Size = New System.Drawing.Size(1182, 64)
        Me.grpFiltroAtual.TabIndex = 0
        Me.grpFiltroAtual.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblTimeAtual
        '
        Me.lblTimeAtual.AutoSize = True
        Me.lblTimeAtual.BackColor = System.Drawing.Color.Transparent
        Me.lblTimeAtual.Location = New System.Drawing.Point(6, 17)
        Me.lblTimeAtual.Name = "lblTimeAtual"
        Me.lblTimeAtual.TabIndex = 0
        Me.lblTimeAtual.Text = "Time:"
        '
        'cboTimeAtual
        '
        Me.cboTimeAtual.AutoSize = False
        Me.cboTimeAtual.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboTimeAtual.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboTimeAtual.Location = New System.Drawing.Point(9, 34)
        Me.cboTimeAtual.Name = "cboTimeAtual"
        Me.cboTimeAtual.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboTimeAtual.Size = New System.Drawing.Size(250, 20)
        Me.cboTimeAtual.TabIndex = 1
        '
        'lblResumoAtual
        '
        Me.lblResumoAtual.AutoSize = True
        Me.lblResumoAtual.BackColor = System.Drawing.Color.Transparent
        Me.lblResumoAtual.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResumoAtual.ForeColor = System.Drawing.Color.Blue
        Me.lblResumoAtual.Location = New System.Drawing.Point(275, 37)
        Me.lblResumoAtual.Name = "lblResumoAtual"
        Me.lblResumoAtual.TabIndex = 2
        Me.lblResumoAtual.Text = "Atualização automática a cada 30 segundos"
        '
        'btnAtualizarAtual
        '
        Me.btnAtualizarAtual.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAtualizarAtual.Image = Global.Documentacti.My.Resources.Resources.procurar
        Me.btnAtualizarAtual.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnAtualizarAtual.Location = New System.Drawing.Point(1082, 31)
        Me.btnAtualizarAtual.Name = "btnAtualizarAtual"
        Me.btnAtualizarAtual.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnAtualizarAtual.Size = New System.Drawing.Size(91, 23)
        Me.btnAtualizarAtual.TabIndex = 3
        Me.btnAtualizarAtual.Text = "Atualizar"
        Me.btnAtualizarAtual.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'grdAtual
        '
        Me.grdAtual.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAtual.AlternatingColors = True
        Me.grdAtual.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdAtual_DesignTimeLayout.LayoutString = resources.GetString("grdAtual_DesignTimeLayout.LayoutString")
        Me.grdAtual.DesignTimeLayout = grdAtual_DesignTimeLayout
        Me.grdAtual.DynamicFiltering = True
        Me.grdAtual.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdAtual.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdAtual.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdAtual.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdAtual.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdAtual.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdAtual.BuiltInTextsData = "<LocalizableData ID=""LocalizableStrings"" Collection=""true""><GroupByBoxInfo>Arrast" & _
    "e o cabeçalho da coluna aqui para agrupar</GroupByBoxInfo></LocalizableData>"
        Me.grdAtual.GroupByBoxVisible = False
        Me.grdAtual.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAtual.Location = New System.Drawing.Point(7, 76)
        Me.grdAtual.Name = "grdAtual"
        Me.grdAtual.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdAtual.RecordNavigator = True
        Me.grdAtual.RecordNavigatorText = "        Record:|Of"
        Me.grdAtual.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdAtual.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdAtual.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdAtual.Size = New System.Drawing.Size(1184, 404)
        Me.grdAtual.TabIndex = 1
        Me.grdAtual.TabStop = False
        Me.grdAtual.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnAgruparAtual
        '
        Me.btnAgruparAtual.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAgruparAtual.BackColor = System.Drawing.Color.Transparent
        Me.btnAgruparAtual.FlatAppearance.BorderSize = 0
        Me.btnAgruparAtual.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAgruparAtual.Image = Global.Documentacti.My.Resources.Resources.merge
        Me.btnAgruparAtual.Location = New System.Drawing.Point(8, 463)
        Me.btnAgruparAtual.Margin = New System.Windows.Forms.Padding(0)
        Me.btnAgruparAtual.Name = "btnAgruparAtual"
        Me.btnAgruparAtual.Size = New System.Drawing.Size(24, 15)
        Me.btnAgruparAtual.TabIndex = 9
        Me.btnAgruparAtual.TabStop = False
        Me.ToolTip.SetToolTip(Me.btnAgruparAtual, "Agrupar: exibe a área para arrastar o cabeçalho da coluna e agrupar")
        Me.btnAgruparAtual.UseVisualStyleBackColor = False
        '
        'grpControlAtual
        '
        Me.grpControlAtual.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpControlAtual.BackColor = System.Drawing.Color.Transparent
        Me.grpControlAtual.Controls.Add(Me.btnSairAtual)
        Me.grpControlAtual.Controls.Add(Me.btnExcelAtual)
        Me.grpControlAtual.Location = New System.Drawing.Point(7, 486)
        Me.grpControlAtual.Name = "grpControlAtual"
        Me.grpControlAtual.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControlAtual.Size = New System.Drawing.Size(1182, 51)
        Me.grpControlAtual.TabIndex = 2
        Me.grpControlAtual.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnExcelAtual
        '
        Me.btnExcelAtual.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcelAtual.Image = Global.Documentacti.My.Resources.Resources.excel
        Me.btnExcelAtual.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnExcelAtual.Location = New System.Drawing.Point(984, 17)
        Me.btnExcelAtual.Name = "btnExcelAtual"
        Me.btnExcelAtual.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnExcelAtual.Size = New System.Drawing.Size(91, 23)
        Me.btnExcelAtual.TabIndex = 1
        Me.btnExcelAtual.Text = "Excel"
        Me.btnExcelAtual.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSairAtual
        '
        Me.btnSairAtual.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSairAtual.Image = CType(resources.GetObject("btnSairAtual.Image"), System.Drawing.Image)
        Me.btnSairAtual.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSairAtual.ImageSize = New System.Drawing.Size(19, 16)
        Me.btnSairAtual.Location = New System.Drawing.Point(1081, 17)
        Me.btnSairAtual.Name = "btnSairAtual"
        Me.btnSairAtual.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnSairAtual.Size = New System.Drawing.Size(91, 23)
        Me.btnSairAtual.TabIndex = 2
        Me.btnSairAtual.Text = "Sair"
        Me.btnSairAtual.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'grpFiltroHistorico
        '
        Me.grpFiltroHistorico.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFiltroHistorico.BackColor = System.Drawing.Color.Transparent
        Me.grpFiltroHistorico.Controls.Add(Me.lblTotalHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.btnFiltrarHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.cboSituacaoHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.lblSituacaoHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.cboTipoAtendimentoHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.lblTipoAtendimentoHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.cboEmpresaHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.lblEmpresaHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.txtChamadoHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.lblChamadoHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.cboUsuarioHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.lblUsuarioHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.cboTimeHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.lblTimeHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.dtpTerminoHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.dtpInicioHistorico)
        Me.grpFiltroHistorico.Controls.Add(Me.lblPeriodoHistorico)
        Me.grpFiltroHistorico.Location = New System.Drawing.Point(7, 6)
        Me.grpFiltroHistorico.Name = "grpFiltroHistorico"
        Me.grpFiltroHistorico.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpFiltroHistorico.Size = New System.Drawing.Size(1182, 104)
        Me.grpFiltroHistorico.TabIndex = 0
        Me.grpFiltroHistorico.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblPeriodoHistorico
        '
        Me.lblPeriodoHistorico.AutoSize = True
        Me.lblPeriodoHistorico.BackColor = System.Drawing.Color.Transparent
        Me.lblPeriodoHistorico.Location = New System.Drawing.Point(6, 17)
        Me.lblPeriodoHistorico.Name = "lblPeriodoHistorico"
        Me.lblPeriodoHistorico.TabIndex = 0
        Me.lblPeriodoHistorico.Text = "Período:"
        '
        'dtpInicioHistorico
        '
        '
        '
        '
        Me.dtpInicioHistorico.DropDownCalendar.Name = ""
        Me.dtpInicioHistorico.Location = New System.Drawing.Point(9, 34)
        Me.dtpInicioHistorico.Name = "dtpInicioHistorico"
        Me.dtpInicioHistorico.ShowCheckBox = True
        Me.dtpInicioHistorico.Size = New System.Drawing.Size(108, 20)
        Me.dtpInicioHistorico.TabIndex = 1
        Me.dtpInicioHistorico.Value = New Date(2026, 1, 1, 0, 0, 0, 0)
        '
        'dtpTerminoHistorico
        '
        '
        '
        '
        Me.dtpTerminoHistorico.DropDownCalendar.Name = ""
        Me.dtpTerminoHistorico.Location = New System.Drawing.Point(123, 34)
        Me.dtpTerminoHistorico.Name = "dtpTerminoHistorico"
        Me.dtpTerminoHistorico.ShowCheckBox = True
        Me.dtpTerminoHistorico.Size = New System.Drawing.Size(108, 20)
        Me.dtpTerminoHistorico.TabIndex = 2
        Me.dtpTerminoHistorico.Value = New Date(2026, 1, 1, 0, 0, 0, 0)
        '
        'lblTimeHistorico
        '
        Me.lblTimeHistorico.AutoSize = True
        Me.lblTimeHistorico.BackColor = System.Drawing.Color.Transparent
        Me.lblTimeHistorico.Location = New System.Drawing.Point(237, 17)
        Me.lblTimeHistorico.Name = "lblTimeHistorico"
        Me.lblTimeHistorico.TabIndex = 3
        Me.lblTimeHistorico.Text = "Time:"
        '
        'cboTimeHistorico
        '
        Me.cboTimeHistorico.AutoSize = False
        Me.cboTimeHistorico.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboTimeHistorico.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboTimeHistorico.Location = New System.Drawing.Point(240, 34)
        Me.cboTimeHistorico.Name = "cboTimeHistorico"
        Me.cboTimeHistorico.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboTimeHistorico.Size = New System.Drawing.Size(180, 20)
        Me.cboTimeHistorico.TabIndex = 4
        '
        'lblUsuarioHistorico
        '
        Me.lblUsuarioHistorico.AutoSize = True
        Me.lblUsuarioHistorico.BackColor = System.Drawing.Color.Transparent
        Me.lblUsuarioHistorico.Location = New System.Drawing.Point(426, 17)
        Me.lblUsuarioHistorico.Name = "lblUsuarioHistorico"
        Me.lblUsuarioHistorico.TabIndex = 5
        Me.lblUsuarioHistorico.Text = "Usuários:"
        '
        'cboUsuarioHistorico
        '
        Me.cboUsuarioHistorico.ButtonCancelText = "Cancelar"
        cboUsuarioHistorico_DesignTimeLayout.LayoutString = resources.GetString("cboUsuarioHistorico_DesignTimeLayout.LayoutString")
        Me.cboUsuarioHistorico.DesignTimeLayout = cboUsuarioHistorico_DesignTimeLayout
        Me.cboUsuarioHistorico.Location = New System.Drawing.Point(429, 34)
        Me.cboUsuarioHistorico.Name = "cboUsuarioHistorico"
        Me.cboUsuarioHistorico.SaveSettings = False
        Me.cboUsuarioHistorico.Size = New System.Drawing.Size(250, 20)
        Me.cboUsuarioHistorico.TabIndex = 6
        Me.ToolTip.SetToolTip(Me.cboUsuarioHistorico, "Nenhum usuário marcado = todos")
        Me.cboUsuarioHistorico.ValuesDataMember = Nothing
        '
        'lblChamadoHistorico
        '
        Me.lblChamadoHistorico.AutoSize = True
        Me.lblChamadoHistorico.BackColor = System.Drawing.Color.Transparent
        Me.lblChamadoHistorico.Location = New System.Drawing.Point(685, 17)
        Me.lblChamadoHistorico.Name = "lblChamadoHistorico"
        Me.lblChamadoHistorico.TabIndex = 7
        Me.lblChamadoHistorico.Text = "Chamado:"
        '
        'txtChamadoHistorico
        '
        Me.txtChamadoHistorico.Location = New System.Drawing.Point(688, 34)
        Me.txtChamadoHistorico.MaxLength = 10
        Me.txtChamadoHistorico.Name = "txtChamadoHistorico"
        Me.txtChamadoHistorico.Size = New System.Drawing.Size(90, 20)
        Me.txtChamadoHistorico.TabIndex = 8
        '
        'lblEmpresaHistorico
        '
        Me.lblEmpresaHistorico.AutoSize = True
        Me.lblEmpresaHistorico.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpresaHistorico.Location = New System.Drawing.Point(6, 58)
        Me.lblEmpresaHistorico.Name = "lblEmpresaHistorico"
        Me.lblEmpresaHistorico.TabIndex = 9
        Me.lblEmpresaHistorico.Text = "Empresa:"
        '
        'cboEmpresaHistorico
        '
        Me.cboEmpresaHistorico.AutoSize = False
        Me.cboEmpresaHistorico.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboEmpresaHistorico.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboEmpresaHistorico.Location = New System.Drawing.Point(9, 75)
        Me.cboEmpresaHistorico.Name = "cboEmpresaHistorico"
        Me.cboEmpresaHistorico.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboEmpresaHistorico.Size = New System.Drawing.Size(300, 20)
        Me.cboEmpresaHistorico.TabIndex = 10
        '
        'lblTipoAtendimentoHistorico
        '
        Me.lblTipoAtendimentoHistorico.AutoSize = True
        Me.lblTipoAtendimentoHistorico.BackColor = System.Drawing.Color.Transparent
        Me.lblTipoAtendimentoHistorico.Location = New System.Drawing.Point(315, 58)
        Me.lblTipoAtendimentoHistorico.Name = "lblTipoAtendimentoHistorico"
        Me.lblTipoAtendimentoHistorico.TabIndex = 11
        Me.lblTipoAtendimentoHistorico.Text = "Tipo de Atendimento:"
        '
        'cboTipoAtendimentoHistorico
        '
        Me.cboTipoAtendimentoHistorico.AutoSize = False
        Me.cboTipoAtendimentoHistorico.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboTipoAtendimentoHistorico.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboTipoAtendimentoHistorico.Location = New System.Drawing.Point(318, 75)
        Me.cboTipoAtendimentoHistorico.Name = "cboTipoAtendimentoHistorico"
        Me.cboTipoAtendimentoHistorico.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboTipoAtendimentoHistorico.Size = New System.Drawing.Size(250, 20)
        Me.cboTipoAtendimentoHistorico.TabIndex = 12
        '
        'lblSituacaoHistorico
        '
        Me.lblSituacaoHistorico.AutoSize = True
        Me.lblSituacaoHistorico.BackColor = System.Drawing.Color.Transparent
        Me.lblSituacaoHistorico.Location = New System.Drawing.Point(574, 58)
        Me.lblSituacaoHistorico.Name = "lblSituacaoHistorico"
        Me.lblSituacaoHistorico.TabIndex = 13
        Me.lblSituacaoHistorico.Text = "Situação:"
        '
        'cboSituacaoHistorico
        '
        Me.cboSituacaoHistorico.AutoSize = False
        Me.cboSituacaoHistorico.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboSituacaoHistorico.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboSituacaoHistorico.Location = New System.Drawing.Point(577, 75)
        Me.cboSituacaoHistorico.Name = "cboSituacaoHistorico"
        Me.cboSituacaoHistorico.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboSituacaoHistorico.Size = New System.Drawing.Size(150, 20)
        Me.cboSituacaoHistorico.TabIndex = 14
        '
        'lblTotalHistorico
        '
        Me.lblTotalHistorico.AutoSize = True
        Me.lblTotalHistorico.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalHistorico.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalHistorico.ForeColor = System.Drawing.Color.Blue
        Me.lblTotalHistorico.Location = New System.Drawing.Point(740, 78)
        Me.lblTotalHistorico.Name = "lblTotalHistorico"
        Me.lblTotalHistorico.TabIndex = 15
        Me.lblTotalHistorico.Text = "0 apontamento(s) | Total: 00:00:00"
        '
        'btnFiltrarHistorico
        '
        Me.btnFiltrarHistorico.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFiltrarHistorico.Image = Global.Documentacti.My.Resources.Resources.procurar
        Me.btnFiltrarHistorico.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnFiltrarHistorico.Location = New System.Drawing.Point(1082, 72)
        Me.btnFiltrarHistorico.Name = "btnFiltrarHistorico"
        Me.btnFiltrarHistorico.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnFiltrarHistorico.Size = New System.Drawing.Size(91, 23)
        Me.btnFiltrarHistorico.TabIndex = 16
        Me.btnFiltrarHistorico.Text = "Filtrar"
        Me.btnFiltrarHistorico.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'grdHistorico
        '
        Me.grdHistorico.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdHistorico.AlternatingColors = True
        Me.grdHistorico.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdHistorico_DesignTimeLayout.LayoutString = resources.GetString("grdHistorico_DesignTimeLayout.LayoutString")
        Me.grdHistorico.DesignTimeLayout = grdHistorico_DesignTimeLayout
        Me.grdHistorico.DynamicFiltering = True
        Me.grdHistorico.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdHistorico.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdHistorico.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdHistorico.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdHistorico.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdHistorico.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdHistorico.BuiltInTextsData = "<LocalizableData ID=""LocalizableStrings"" Collection=""true""><GroupByBoxInfo>Arrast" & _
    "e o cabeçalho da coluna aqui para agrupar</GroupByBoxInfo></LocalizableData>"
        Me.grdHistorico.GroupByBoxVisible = False
        Me.grdHistorico.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdHistorico.Location = New System.Drawing.Point(7, 116)
        Me.grdHistorico.Name = "grdHistorico"
        Me.grdHistorico.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdHistorico.RecordNavigator = True
        Me.grdHistorico.RecordNavigatorText = "        Record:|Of"
        Me.grdHistorico.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdHistorico.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdHistorico.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdHistorico.Size = New System.Drawing.Size(1184, 364)
        Me.grdHistorico.TabIndex = 1
        Me.grdHistorico.TabStop = False
        Me.grdHistorico.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdHistorico.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnAgruparHistorico
        '
        Me.btnAgruparHistorico.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAgruparHistorico.BackColor = System.Drawing.Color.Transparent
        Me.btnAgruparHistorico.FlatAppearance.BorderSize = 0
        Me.btnAgruparHistorico.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAgruparHistorico.Image = Global.Documentacti.My.Resources.Resources.merge
        Me.btnAgruparHistorico.Location = New System.Drawing.Point(8, 463)
        Me.btnAgruparHistorico.Margin = New System.Windows.Forms.Padding(0)
        Me.btnAgruparHistorico.Name = "btnAgruparHistorico"
        Me.btnAgruparHistorico.Size = New System.Drawing.Size(24, 15)
        Me.btnAgruparHistorico.TabIndex = 9
        Me.btnAgruparHistorico.TabStop = False
        Me.ToolTip.SetToolTip(Me.btnAgruparHistorico, "Agrupar: exibe a área para arrastar o cabeçalho da coluna e agrupar")
        Me.btnAgruparHistorico.UseVisualStyleBackColor = False
        '
        'grpControlHistorico
        '
        Me.grpControlHistorico.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpControlHistorico.BackColor = System.Drawing.Color.Transparent
        Me.grpControlHistorico.Controls.Add(Me.btnSairHistorico)
        Me.grpControlHistorico.Controls.Add(Me.btnExcelHistorico)
        Me.grpControlHistorico.Location = New System.Drawing.Point(7, 486)
        Me.grpControlHistorico.Name = "grpControlHistorico"
        Me.grpControlHistorico.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControlHistorico.Size = New System.Drawing.Size(1182, 51)
        Me.grpControlHistorico.TabIndex = 2
        Me.grpControlHistorico.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnExcelHistorico
        '
        Me.btnExcelHistorico.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcelHistorico.Image = Global.Documentacti.My.Resources.Resources.excel
        Me.btnExcelHistorico.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnExcelHistorico.Location = New System.Drawing.Point(984, 17)
        Me.btnExcelHistorico.Name = "btnExcelHistorico"
        Me.btnExcelHistorico.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnExcelHistorico.Size = New System.Drawing.Size(91, 23)
        Me.btnExcelHistorico.TabIndex = 1
        Me.btnExcelHistorico.Text = "Excel"
        Me.btnExcelHistorico.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSairHistorico
        '
        Me.btnSairHistorico.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSairHistorico.Image = CType(resources.GetObject("btnSairHistorico.Image"), System.Drawing.Image)
        Me.btnSairHistorico.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSairHistorico.ImageSize = New System.Drawing.Size(19, 16)
        Me.btnSairHistorico.Location = New System.Drawing.Point(1081, 17)
        Me.btnSairHistorico.Name = "btnSairHistorico"
        Me.btnSairHistorico.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnSairHistorico.Size = New System.Drawing.Size(91, 23)
        Me.btnSairHistorico.TabIndex = 2
        Me.btnSairHistorico.Text = "Sair"
        Me.btnSairHistorico.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
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
        'tmrAtual
        '
        'Atualiza o tempo decorrido a cada segundo e busca no banco a cada 30 segundos
        Me.tmrAtual.Interval = 1000
        '
        'usrRelApontamento
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.tabMain)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "usrRelApontamento"
        Me.Size = New System.Drawing.Size(1200, 565)
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMain.ResumeLayout(False)
        Me.pagAtual.ResumeLayout(False)
        Me.pagHistorico.ResumeLayout(False)
        CType(Me.grpFiltroAtual, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFiltroAtual.ResumeLayout(False)
        Me.grpFiltroAtual.PerformLayout()
        CType(Me.grpControlAtual, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpControlAtual.ResumeLayout(False)
        Me.grpControlAtual.PerformLayout()
        CType(Me.grpFiltroHistorico, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFiltroHistorico.ResumeLayout(False)
        Me.grpFiltroHistorico.PerformLayout()
        CType(Me.grpControlHistorico, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpControlHistorico.ResumeLayout(False)
        Me.grpControlHistorico.PerformLayout()
        CType(Me.grdAtual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdHistorico, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabMain As Janus.Windows.UI.Tab.UITab
    Friend WithEvents pagAtual As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents pagHistorico As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents grpFiltroAtual As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblTimeAtual As System.Windows.Forms.Label
    Friend WithEvents cboTimeAtual As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblResumoAtual As System.Windows.Forms.Label
    Friend WithEvents btnAtualizarAtual As Janus.Windows.EditControls.UIButton
    Friend WithEvents grdAtual As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnAgruparAtual As System.Windows.Forms.Button
    Friend WithEvents grpControlAtual As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnExcelAtual As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSairAtual As Janus.Windows.EditControls.UIButton
    Friend WithEvents grpFiltroHistorico As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblPeriodoHistorico As System.Windows.Forms.Label
    Friend WithEvents dtpInicioHistorico As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtpTerminoHistorico As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents lblTimeHistorico As System.Windows.Forms.Label
    Friend WithEvents cboTimeHistorico As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblUsuarioHistorico As System.Windows.Forms.Label
    Friend WithEvents cboUsuarioHistorico As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents lblChamadoHistorico As System.Windows.Forms.Label
    Friend WithEvents txtChamadoHistorico As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblEmpresaHistorico As System.Windows.Forms.Label
    Friend WithEvents cboEmpresaHistorico As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblTipoAtendimentoHistorico As System.Windows.Forms.Label
    Friend WithEvents cboTipoAtendimentoHistorico As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblSituacaoHistorico As System.Windows.Forms.Label
    Friend WithEvents cboSituacaoHistorico As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblTotalHistorico As System.Windows.Forms.Label
    Friend WithEvents btnFiltrarHistorico As Janus.Windows.EditControls.UIButton
    Friend WithEvents grdHistorico As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnAgruparHistorico As System.Windows.Forms.Button
    Friend WithEvents grpControlHistorico As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnExcelHistorico As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSairHistorico As Janus.Windows.EditControls.UIButton
    Friend WithEvents vsmMain As Janus.Windows.Common.VisualStyleManager
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents tmrAtual As System.Windows.Forms.Timer

End Class
