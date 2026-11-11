<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class usrAdmEvento
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(usrAdmEvento))
        Dim JanusColorScheme1 As Janus.Windows.Common.JanusColorScheme = New Janus.Windows.Common.JanusColorScheme()
        Dim cboTimeFiltro_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cboParticipanteFiltro_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cboEmpresaFiltro_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdListagem_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdListagem_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image")
        Dim grdListagem_DesignTimeLayout_Reference_1 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column1.Image")
        Dim cboParticipantes_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cboTimes_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cboEmpresas_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.vsmMain = New Janus.Windows.Common.VisualStyleManager(Me.components)
        Me.tabMain = New Janus.Windows.UI.Tab.UITab()
        Me.pagLista = New Janus.Windows.UI.Tab.UITabPage()
        Me.grpFiltro = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblPeriodoFiltro = New System.Windows.Forms.Label()
        Me.dtpInicioFiltro = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.dtpTerminoFiltro = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.lblTipoFiltro = New System.Windows.Forms.Label()
        Me.cboTipoFiltro = New Janus.Windows.EditControls.UIComboBox()
        Me.lblTimeFiltro = New System.Windows.Forms.Label()
        Me.cboTimeFiltro = New Janus.Windows.GridEX.EditControls.CheckedComboBox()
        Me.lblParticipanteFiltro = New System.Windows.Forms.Label()
        Me.cboParticipanteFiltro = New Janus.Windows.GridEX.EditControls.CheckedComboBox()
        Me.lblEmpresaFiltro = New System.Windows.Forms.Label()
        Me.cboEmpresaFiltro = New Janus.Windows.GridEX.EditControls.CheckedComboBox()
        Me.lblTituloFiltro = New System.Windows.Forms.Label()
        Me.txtTituloFiltro = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.btnFiltrar = New Janus.Windows.EditControls.UIButton()
        Me.tabVisao = New Janus.Windows.UI.Tab.UITab()
        Me.pagCalendario = New Janus.Windows.UI.Tab.UITabPage()
        Me.btnAnterior = New Janus.Windows.EditControls.UIButton()
        Me.btnHoje = New Janus.Windows.EditControls.UIButton()
        Me.btnProximo = New Janus.Windows.EditControls.UIButton()
        Me.lblPeriodo = New System.Windows.Forms.Label()
        Me.pnlCalendario = New System.Windows.Forms.Panel()
        Me.schMain = New Janus.Windows.Schedule.Schedule()
        Me.pagGrid = New Janus.Windows.UI.Tab.UITabPage()
        Me.grdListagem = New Janus.Windows.GridEX.GridEX()
        Me.grpControl = New Janus.Windows.EditControls.UIGroupBox()
        Me.btnExcel = New Janus.Windows.EditControls.UIButton()
        Me.btnNovo1 = New Janus.Windows.EditControls.UIButton()
        Me.pagDados = New Janus.Windows.UI.Tab.UITabPage()
        Me.grpDados = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.txtCodigo = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblTipo = New System.Windows.Forms.Label()
        Me.cboTipo = New Janus.Windows.EditControls.UIComboBox()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.txtTitulo = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.chkDiaInteiro = New Janus.Windows.EditControls.UICheckBox()
        Me.lblInicio = New System.Windows.Forms.Label()
        Me.dtpInicio = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.lblFim = New System.Windows.Forms.Label()
        Me.dtpFim = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.lblLocal = New System.Windows.Forms.Label()
        Me.txtLocal = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblParticipantes = New System.Windows.Forms.Label()
        Me.cboParticipantes = New Janus.Windows.GridEX.EditControls.CheckedComboBox()
        Me.lblTimes = New System.Windows.Forms.Label()
        Me.cboTimes = New Janus.Windows.GridEX.EditControls.CheckedComboBox()
        Me.lblEmpresas = New System.Windows.Forms.Label()
        Me.cboEmpresas = New Janus.Windows.GridEX.EditControls.CheckedComboBox()
        Me.lblChamado = New System.Windows.Forms.Label()
        Me.txtChamado = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblDescricao = New System.Windows.Forms.Label()
        Me.txtDescricao = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.grpControl2 = New Janus.Windows.EditControls.UIGroupBox()
        Me.btnExcluir = New Janus.Windows.EditControls.UIButton()
        Me.btnNovo = New Janus.Windows.EditControls.UIButton()
        Me.btnSalvar = New Janus.Windows.EditControls.UIButton()
        Me.btnVoltar = New Janus.Windows.EditControls.UIButton()
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMain.SuspendLayout()
        Me.pagLista.SuspendLayout()
        CType(Me.grpFiltro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFiltro.SuspendLayout()
        CType(Me.tabVisao, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabVisao.SuspendLayout()
        Me.pagCalendario.SuspendLayout()
        Me.pnlCalendario.SuspendLayout()
        CType(Me.schMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pagGrid.SuspendLayout()
        CType(Me.grdListagem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpControl.SuspendLayout()
        Me.pagDados.SuspendLayout()
        CType(Me.grpDados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDados.SuspendLayout()
        CType(Me.grpControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpControl2.SuspendLayout()
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
        Me.tabMain.TabPages.AddRange(New Janus.Windows.UI.Tab.UITabPage() {Me.pagLista, Me.pagDados})
        Me.tabMain.TabStop = False
        Me.tabMain.VisualStyleManager = Me.vsmMain
        '
        'pagLista
        '
        Me.pagLista.Controls.Add(Me.grpControl)
        Me.pagLista.Controls.Add(Me.tabVisao)
        Me.pagLista.Controls.Add(Me.grpFiltro)
        Me.pagLista.Key = "pagLista"
        Me.pagLista.Location = New System.Drawing.Point(1, 24)
        Me.pagLista.Name = "pagLista"
        Me.pagLista.Size = New System.Drawing.Size(1198, 540)
        Me.pagLista.TabStop = True
        Me.pagLista.Text = "Calendário de Eventos"
        '
        'grpFiltro
        '
        Me.grpFiltro.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFiltro.BackColor = System.Drawing.Color.Transparent
        Me.grpFiltro.Controls.Add(Me.btnFiltrar)
        Me.grpFiltro.Controls.Add(Me.txtTituloFiltro)
        Me.grpFiltro.Controls.Add(Me.lblTituloFiltro)
        Me.grpFiltro.Controls.Add(Me.cboEmpresaFiltro)
        Me.grpFiltro.Controls.Add(Me.lblEmpresaFiltro)
        Me.grpFiltro.Controls.Add(Me.cboParticipanteFiltro)
        Me.grpFiltro.Controls.Add(Me.lblParticipanteFiltro)
        Me.grpFiltro.Controls.Add(Me.cboTimeFiltro)
        Me.grpFiltro.Controls.Add(Me.lblTimeFiltro)
        Me.grpFiltro.Controls.Add(Me.cboTipoFiltro)
        Me.grpFiltro.Controls.Add(Me.lblTipoFiltro)
        Me.grpFiltro.Controls.Add(Me.dtpTerminoFiltro)
        Me.grpFiltro.Controls.Add(Me.dtpInicioFiltro)
        Me.grpFiltro.Controls.Add(Me.lblPeriodoFiltro)
        Me.grpFiltro.Location = New System.Drawing.Point(7, 6)
        Me.grpFiltro.Name = "grpFiltro"
        Me.grpFiltro.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpFiltro.Size = New System.Drawing.Size(1184, 64)
        Me.grpFiltro.TabIndex = 0
        Me.grpFiltro.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblPeriodoFiltro
        '
        Me.lblPeriodoFiltro.AutoSize = True
        Me.lblPeriodoFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblPeriodoFiltro.Location = New System.Drawing.Point(6, 13)
        Me.lblPeriodoFiltro.Name = "lblPeriodoFiltro"
        Me.lblPeriodoFiltro.Size = New System.Drawing.Size(59, 16)
        Me.lblPeriodoFiltro.TabIndex = 0
        Me.lblPeriodoFiltro.Text = "Período:"
        '
        'dtpInicioFiltro
        '
        Me.dtpInicioFiltro.CustomFormat = "dd/MM/yyyy"
        Me.dtpInicioFiltro.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpInicioFiltro.DropDownCalendar.Name = ""
        Me.dtpInicioFiltro.Location = New System.Drawing.Point(9, 29)
        Me.dtpInicioFiltro.Name = "dtpInicioFiltro"
        Me.dtpInicioFiltro.Size = New System.Drawing.Size(108, 20)
        Me.dtpInicioFiltro.TabIndex = 1
        '
        'dtpTerminoFiltro
        '
        Me.dtpTerminoFiltro.CustomFormat = "dd/MM/yyyy"
        Me.dtpTerminoFiltro.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpTerminoFiltro.DropDownCalendar.Name = ""
        Me.dtpTerminoFiltro.Location = New System.Drawing.Point(123, 29)
        Me.dtpTerminoFiltro.Name = "dtpTerminoFiltro"
        Me.dtpTerminoFiltro.Size = New System.Drawing.Size(108, 20)
        Me.dtpTerminoFiltro.TabIndex = 2
        '
        'lblTipoFiltro
        '
        Me.lblTipoFiltro.AutoSize = True
        Me.lblTipoFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblTipoFiltro.Location = New System.Drawing.Point(234, 13)
        Me.lblTipoFiltro.Name = "lblTipoFiltro"
        Me.lblTipoFiltro.Size = New System.Drawing.Size(39, 16)
        Me.lblTipoFiltro.TabIndex = 3
        Me.lblTipoFiltro.Text = "Tipo:"
        '
        'cboTipoFiltro
        '
        Me.cboTipoFiltro.AutoSize = False
        Me.cboTipoFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboTipoFiltro.Location = New System.Drawing.Point(237, 29)
        Me.cboTipoFiltro.Name = "cboTipoFiltro"
        Me.cboTipoFiltro.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboTipoFiltro.Size = New System.Drawing.Size(150, 20)
        Me.cboTipoFiltro.TabIndex = 4
        '
        'lblTimeFiltro
        '
        Me.lblTimeFiltro.AutoSize = True
        Me.lblTimeFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblTimeFiltro.Location = New System.Drawing.Point(390, 13)
        Me.lblTimeFiltro.Name = "lblTimeFiltro"
        Me.lblTimeFiltro.Size = New System.Drawing.Size(45, 16)
        Me.lblTimeFiltro.TabIndex = 5
        Me.lblTimeFiltro.Text = "Times:"
        '
        'cboTimeFiltro
        '
        Me.cboTimeFiltro.ButtonCancelText = "Cancelar"
        cboTimeFiltro_DesignTimeLayout.LayoutString = resources.GetString("cboTimeFiltro_DesignTimeLayout.LayoutString")
        Me.cboTimeFiltro.DesignTimeLayout = cboTimeFiltro_DesignTimeLayout
        Me.cboTimeFiltro.Location = New System.Drawing.Point(393, 28)
        Me.cboTimeFiltro.Name = "cboTimeFiltro"
        Me.cboTimeFiltro.SaveSettings = False
        Me.cboTimeFiltro.Size = New System.Drawing.Size(150, 23)
        Me.cboTimeFiltro.TabIndex = 6
        Me.cboTimeFiltro.ValuesDataMember = Nothing
        '
        'lblParticipanteFiltro
        '
        Me.lblParticipanteFiltro.AutoSize = True
        Me.lblParticipanteFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblParticipanteFiltro.Location = New System.Drawing.Point(546, 13)
        Me.lblParticipanteFiltro.Name = "lblParticipanteFiltro"
        Me.lblParticipanteFiltro.Size = New System.Drawing.Size(95, 16)
        Me.lblParticipanteFiltro.TabIndex = 7
        Me.lblParticipanteFiltro.Text = "Participantes:"
        '
        'cboParticipanteFiltro
        '
        Me.cboParticipanteFiltro.ButtonCancelText = "Cancelar"
        cboParticipanteFiltro_DesignTimeLayout.LayoutString = resources.GetString("cboParticipanteFiltro_DesignTimeLayout.LayoutString")
        Me.cboParticipanteFiltro.DesignTimeLayout = cboParticipanteFiltro_DesignTimeLayout
        Me.cboParticipanteFiltro.Location = New System.Drawing.Point(549, 28)
        Me.cboParticipanteFiltro.Name = "cboParticipanteFiltro"
        Me.cboParticipanteFiltro.SaveSettings = False
        Me.cboParticipanteFiltro.Size = New System.Drawing.Size(200, 23)
        Me.cboParticipanteFiltro.TabIndex = 8
        Me.cboParticipanteFiltro.ValuesDataMember = Nothing
        '
        'lblEmpresaFiltro
        '
        Me.lblEmpresaFiltro.AutoSize = True
        Me.lblEmpresaFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpresaFiltro.Location = New System.Drawing.Point(752, 13)
        Me.lblEmpresaFiltro.Name = "lblEmpresaFiltro"
        Me.lblEmpresaFiltro.Size = New System.Drawing.Size(75, 16)
        Me.lblEmpresaFiltro.TabIndex = 9
        Me.lblEmpresaFiltro.Text = "Empresas:"
        '
        'cboEmpresaFiltro
        '
        Me.cboEmpresaFiltro.ButtonCancelText = "Cancelar"
        cboEmpresaFiltro_DesignTimeLayout.LayoutString = resources.GetString("cboEmpresaFiltro_DesignTimeLayout.LayoutString")
        Me.cboEmpresaFiltro.DesignTimeLayout = cboEmpresaFiltro_DesignTimeLayout
        Me.cboEmpresaFiltro.Location = New System.Drawing.Point(755, 28)
        Me.cboEmpresaFiltro.Name = "cboEmpresaFiltro"
        Me.cboEmpresaFiltro.SaveSettings = False
        Me.cboEmpresaFiltro.Size = New System.Drawing.Size(170, 23)
        Me.cboEmpresaFiltro.TabIndex = 10
        Me.cboEmpresaFiltro.ValuesDataMember = Nothing
        '
        'lblTituloFiltro
        '
        Me.lblTituloFiltro.AutoSize = True
        Me.lblTituloFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblTituloFiltro.Location = New System.Drawing.Point(928, 13)
        Me.lblTituloFiltro.Name = "lblTituloFiltro"
        Me.lblTituloFiltro.Size = New System.Drawing.Size(46, 16)
        Me.lblTituloFiltro.TabIndex = 11
        Me.lblTituloFiltro.Text = "Título:"
        '
        'txtTituloFiltro
        '
        Me.txtTituloFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTituloFiltro.Location = New System.Drawing.Point(931, 28)
        Me.txtTituloFiltro.MaxLength = 150
        Me.txtTituloFiltro.Name = "txtTituloFiltro"
        Me.txtTituloFiltro.Size = New System.Drawing.Size(145, 23)
        Me.txtTituloFiltro.TabIndex = 12
        '
        'btnFiltrar
        '
        Me.btnFiltrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFiltrar.Image = Global.Documentacti.My.Resources.Resources.procurar
        Me.btnFiltrar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnFiltrar.Location = New System.Drawing.Point(1084, 28)
        Me.btnFiltrar.Name = "btnFiltrar"
        Me.btnFiltrar.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnFiltrar.Size = New System.Drawing.Size(91, 23)
        Me.btnFiltrar.TabIndex = 13
        Me.btnFiltrar.Text = "Filtrar"
        Me.btnFiltrar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'tabVisao
        '
        Me.tabVisao.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabVisao.BackColor = System.Drawing.Color.Transparent
        Me.tabVisao.Location = New System.Drawing.Point(7, 76)
        Me.tabVisao.Name = "tabVisao"
        Me.tabVisao.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.tabVisao.Size = New System.Drawing.Size(1184, 414)
        Me.tabVisao.TabIndex = 1
        Me.tabVisao.TabPages.AddRange(New Janus.Windows.UI.Tab.UITabPage() {Me.pagCalendario, Me.pagGrid})
        Me.tabVisao.TabStop = False
        Me.tabVisao.VisualStyleManager = Me.vsmMain
        '
        'pagCalendario
        '
        Me.pagCalendario.Controls.Add(Me.pnlCalendario)
        Me.pagCalendario.Controls.Add(Me.lblPeriodo)
        Me.pagCalendario.Controls.Add(Me.btnProximo)
        Me.pagCalendario.Controls.Add(Me.btnHoje)
        Me.pagCalendario.Controls.Add(Me.btnAnterior)
        Me.pagCalendario.Key = "pagCalendario"
        Me.pagCalendario.Location = New System.Drawing.Point(1, 24)
        Me.pagCalendario.Name = "pagCalendario"
        Me.pagCalendario.Size = New System.Drawing.Size(1182, 389)
        Me.pagCalendario.TabStop = True
        Me.pagCalendario.Text = "Calendário"
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(6, 6)
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnAnterior.Size = New System.Drawing.Size(100, 23)
        Me.btnAnterior.TabIndex = 0
        Me.btnAnterior.Text = "< Mês anterior"
        Me.btnAnterior.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnHoje
        '
        Me.btnHoje.Location = New System.Drawing.Point(112, 6)
        Me.btnHoje.Name = "btnHoje"
        Me.btnHoje.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnHoje.Size = New System.Drawing.Size(91, 23)
        Me.btnHoje.TabIndex = 1
        Me.btnHoje.Text = "Mês atual"
        Me.btnHoje.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnProximo
        '
        Me.btnProximo.Location = New System.Drawing.Point(209, 6)
        Me.btnProximo.Name = "btnProximo"
        Me.btnProximo.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnProximo.Size = New System.Drawing.Size(100, 23)
        Me.btnProximo.TabIndex = 2
        Me.btnProximo.Text = "Próximo mês >"
        Me.btnProximo.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'lblPeriodo
        '
        Me.lblPeriodo.AutoSize = True
        Me.lblPeriodo.BackColor = System.Drawing.Color.Transparent
        Me.lblPeriodo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPeriodo.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblPeriodo.Location = New System.Drawing.Point(320, 10)
        Me.lblPeriodo.Name = "lblPeriodo"
        Me.lblPeriodo.Size = New System.Drawing.Size(0, 16)
        Me.lblPeriodo.TabIndex = 3
        '
        'pnlCalendario
        '
        Me.pnlCalendario.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlCalendario.AutoScroll = True
        Me.pnlCalendario.BackColor = System.Drawing.Color.Transparent
        Me.pnlCalendario.Controls.Add(Me.schMain)
        Me.pnlCalendario.Location = New System.Drawing.Point(6, 35)
        Me.pnlCalendario.Name = "pnlCalendario"
        Me.pnlCalendario.Size = New System.Drawing.Size(1170, 348)
        Me.pnlCalendario.TabIndex = 4
        '
        'schMain
        '
        Me.schMain.Location = New System.Drawing.Point(0, 0)
        Me.schMain.Name = "schMain"
        Me.schMain.Size = New System.Drawing.Size(1170, 348)
        Me.schMain.TabIndex = 0
        Me.schMain.VisualStyleManager = Me.vsmMain
        '
        'pagGrid
        '
        Me.pagGrid.Controls.Add(Me.grdListagem)
        Me.pagGrid.Key = "pagGrid"
        Me.pagGrid.Location = New System.Drawing.Point(1, 24)
        Me.pagGrid.Name = "pagGrid"
        Me.pagGrid.Size = New System.Drawing.Size(1182, 389)
        Me.pagGrid.TabStop = True
        Me.pagGrid.Text = "Lista por Data"
        '
        'grdListagem
        '
        Me.grdListagem.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdListagem.AlternatingColors = True
        Me.grdListagem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdListagem_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("grdListagem_DesignTimeLayout_Reference_0.Instance"), Object)
        grdListagem_DesignTimeLayout_Reference_1.Instance = CType(resources.GetObject("grdListagem_DesignTimeLayout_Reference_1.Instance"), Object)
        grdListagem_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {grdListagem_DesignTimeLayout_Reference_0, grdListagem_DesignTimeLayout_Reference_1})
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
        Me.grdListagem.Location = New System.Drawing.Point(6, 6)
        Me.grdListagem.Name = "grdListagem"
        Me.grdListagem.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdListagem.RecordNavigator = True
        Me.grdListagem.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdListagem.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdListagem.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdListagem.Size = New System.Drawing.Size(1170, 377)
        Me.grdListagem.TabIndex = 0
        Me.grdListagem.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grpControl
        '
        Me.grpControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpControl.BackColor = System.Drawing.Color.Transparent
        Me.grpControl.Controls.Add(Me.btnNovo1)
        Me.grpControl.Controls.Add(Me.btnExcel)
        Me.grpControl.Location = New System.Drawing.Point(7, 496)
        Me.grpControl.Name = "grpControl"
        Me.grpControl.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControl.Size = New System.Drawing.Size(1184, 40)
        Me.grpControl.TabIndex = 2
        Me.grpControl.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnExcel
        '
        Me.btnExcel.Image = Global.Documentacti.My.Resources.Resources.excel
        Me.btnExcel.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnExcel.Location = New System.Drawing.Point(9, 9)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnExcel.Size = New System.Drawing.Size(91, 23)
        Me.btnExcel.TabIndex = 0
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnNovo1
        '
        Me.btnNovo1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNovo1.Image = Global.Documentacti.My.Resources.Resources.Novo
        Me.btnNovo1.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnNovo1.Location = New System.Drawing.Point(1084, 9)
        Me.btnNovo1.Name = "btnNovo1"
        Me.btnNovo1.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnNovo1.Size = New System.Drawing.Size(91, 23)
        Me.btnNovo1.TabIndex = 1
        Me.btnNovo1.Text = "Novo"
        Me.btnNovo1.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'pagDados
        '
        Me.pagDados.Controls.Add(Me.grpControl2)
        Me.pagDados.Controls.Add(Me.grpDados)
        Me.pagDados.Key = "pagDados"
        Me.pagDados.Location = New System.Drawing.Point(1, 24)
        Me.pagDados.Name = "pagDados"
        Me.pagDados.Size = New System.Drawing.Size(1198, 540)
        Me.pagDados.TabStop = True
        Me.pagDados.Text = "Dados do Evento"
        '
        'grpDados
        '
        Me.grpDados.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpDados.BackColor = System.Drawing.Color.Transparent
        Me.grpDados.Controls.Add(Me.txtDescricao)
        Me.grpDados.Controls.Add(Me.lblDescricao)
        Me.grpDados.Controls.Add(Me.txtChamado)
        Me.grpDados.Controls.Add(Me.lblChamado)
        Me.grpDados.Controls.Add(Me.cboEmpresas)
        Me.grpDados.Controls.Add(Me.lblEmpresas)
        Me.grpDados.Controls.Add(Me.cboTimes)
        Me.grpDados.Controls.Add(Me.lblTimes)
        Me.grpDados.Controls.Add(Me.cboParticipantes)
        Me.grpDados.Controls.Add(Me.lblParticipantes)
        Me.grpDados.Controls.Add(Me.txtLocal)
        Me.grpDados.Controls.Add(Me.lblLocal)
        Me.grpDados.Controls.Add(Me.dtpFim)
        Me.grpDados.Controls.Add(Me.lblFim)
        Me.grpDados.Controls.Add(Me.dtpInicio)
        Me.grpDados.Controls.Add(Me.lblInicio)
        Me.grpDados.Controls.Add(Me.chkDiaInteiro)
        Me.grpDados.Controls.Add(Me.txtTitulo)
        Me.grpDados.Controls.Add(Me.lblTitulo)
        Me.grpDados.Controls.Add(Me.cboTipo)
        Me.grpDados.Controls.Add(Me.lblTipo)
        Me.grpDados.Controls.Add(Me.txtCodigo)
        Me.grpDados.Controls.Add(Me.lblCodigo)
        Me.grpDados.Location = New System.Drawing.Point(7, 6)
        Me.grpDados.Name = "grpDados"
        Me.grpDados.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpDados.Size = New System.Drawing.Size(1184, 474)
        Me.grpDados.TabIndex = 0
        Me.grpDados.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.BackColor = System.Drawing.Color.Transparent
        Me.lblCodigo.Location = New System.Drawing.Point(6, 13)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(57, 16)
        Me.lblCodigo.TabIndex = 0
        Me.lblCodigo.Text = "Código:"
        '
        'txtCodigo
        '
        Me.txtCodigo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCodigo.Location = New System.Drawing.Point(9, 29)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.ReadOnly = True
        Me.txtCodigo.Size = New System.Drawing.Size(80, 23)
        Me.txtCodigo.TabIndex = 1
        Me.txtCodigo.TabStop = False
        '
        'lblTipo
        '
        Me.lblTipo.AutoSize = True
        Me.lblTipo.BackColor = System.Drawing.Color.Transparent
        Me.lblTipo.Location = New System.Drawing.Point(92, 13)
        Me.lblTipo.Name = "lblTipo"
        Me.lblTipo.Size = New System.Drawing.Size(39, 16)
        Me.lblTipo.TabIndex = 2
        Me.lblTipo.Text = "Tipo:"
        '
        'cboTipo
        '
        Me.cboTipo.AutoSize = False
        Me.cboTipo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboTipo.Location = New System.Drawing.Point(95, 30)
        Me.cboTipo.Name = "cboTipo"
        Me.cboTipo.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboTipo.Size = New System.Drawing.Size(200, 20)
        Me.cboTipo.TabIndex = 3
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.BackColor = System.Drawing.Color.Transparent
        Me.lblTitulo.Location = New System.Drawing.Point(298, 13)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(46, 16)
        Me.lblTitulo.TabIndex = 4
        Me.lblTitulo.Text = "Título:"
        '
        'txtTitulo
        '
        Me.txtTitulo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTitulo.Location = New System.Drawing.Point(301, 29)
        Me.txtTitulo.MaxLength = 150
        Me.txtTitulo.Name = "txtTitulo"
        Me.txtTitulo.Size = New System.Drawing.Size(560, 23)
        Me.txtTitulo.TabIndex = 5
        '
        'chkDiaInteiro
        '
        Me.chkDiaInteiro.BackColor = System.Drawing.Color.Transparent
        Me.chkDiaInteiro.Location = New System.Drawing.Point(870, 31)
        Me.chkDiaInteiro.Name = "chkDiaInteiro"
        Me.chkDiaInteiro.Size = New System.Drawing.Size(120, 18)
        Me.chkDiaInteiro.TabIndex = 6
        Me.chkDiaInteiro.Text = "Dia inteiro"
        Me.chkDiaInteiro.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'lblInicio
        '
        Me.lblInicio.AutoSize = True
        Me.lblInicio.BackColor = System.Drawing.Color.Transparent
        Me.lblInicio.Location = New System.Drawing.Point(6, 53)
        Me.lblInicio.Name = "lblInicio"
        Me.lblInicio.Size = New System.Drawing.Size(45, 16)
        Me.lblInicio.TabIndex = 7
        Me.lblInicio.Text = "Início:"
        '
        'dtpInicio
        '
        Me.dtpInicio.CustomFormat = "dd/MM/yyyy HH:mm"
        Me.dtpInicio.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpInicio.DropDownCalendar.Name = ""
        Me.dtpInicio.Location = New System.Drawing.Point(9, 69)
        Me.dtpInicio.Name = "dtpInicio"
        Me.dtpInicio.Size = New System.Drawing.Size(140, 20)
        Me.dtpInicio.TabIndex = 8
        '
        'lblFim
        '
        Me.lblFim.AutoSize = True
        Me.lblFim.BackColor = System.Drawing.Color.Transparent
        Me.lblFim.Location = New System.Drawing.Point(152, 53)
        Me.lblFim.Name = "lblFim"
        Me.lblFim.Size = New System.Drawing.Size(64, 16)
        Me.lblFim.TabIndex = 9
        Me.lblFim.Text = "Término:"
        '
        'dtpFim
        '
        Me.dtpFim.CustomFormat = "dd/MM/yyyy HH:mm"
        Me.dtpFim.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpFim.DropDownCalendar.Name = ""
        Me.dtpFim.Location = New System.Drawing.Point(155, 69)
        Me.dtpFim.Name = "dtpFim"
        Me.dtpFim.Size = New System.Drawing.Size(140, 20)
        Me.dtpFim.TabIndex = 10
        '
        'lblLocal
        '
        Me.lblLocal.AutoSize = True
        Me.lblLocal.BackColor = System.Drawing.Color.Transparent
        Me.lblLocal.Location = New System.Drawing.Point(298, 53)
        Me.lblLocal.Name = "lblLocal"
        Me.lblLocal.Size = New System.Drawing.Size(91, 16)
        Me.lblLocal.TabIndex = 11
        Me.lblLocal.Text = "Local / Link:"
        '
        'txtLocal
        '
        Me.txtLocal.Location = New System.Drawing.Point(301, 68)
        Me.txtLocal.MaxLength = 250
        Me.txtLocal.Name = "txtLocal"
        Me.txtLocal.Size = New System.Drawing.Size(560, 23)
        Me.txtLocal.TabIndex = 12
        '
        'lblParticipantes
        '
        Me.lblParticipantes.AutoSize = True
        Me.lblParticipantes.BackColor = System.Drawing.Color.Transparent
        Me.lblParticipantes.Location = New System.Drawing.Point(6, 93)
        Me.lblParticipantes.Name = "lblParticipantes"
        Me.lblParticipantes.Size = New System.Drawing.Size(95, 16)
        Me.lblParticipantes.TabIndex = 13
        Me.lblParticipantes.Text = "Participantes:"
        '
        'cboParticipantes
        '
        Me.cboParticipantes.ButtonCancelText = "Cancelar"
        cboParticipantes_DesignTimeLayout.LayoutString = resources.GetString("cboParticipantes_DesignTimeLayout.LayoutString")
        Me.cboParticipantes.DesignTimeLayout = cboParticipantes_DesignTimeLayout
        Me.cboParticipantes.Location = New System.Drawing.Point(9, 109)
        Me.cboParticipantes.Name = "cboParticipantes"
        Me.cboParticipantes.SaveSettings = False
        Me.cboParticipantes.Size = New System.Drawing.Size(380, 23)
        Me.cboParticipantes.TabIndex = 14
        Me.cboParticipantes.ValuesDataMember = Nothing
        '
        'lblTimes
        '
        Me.lblTimes.AutoSize = True
        Me.lblTimes.BackColor = System.Drawing.Color.Transparent
        Me.lblTimes.Location = New System.Drawing.Point(392, 93)
        Me.lblTimes.Name = "lblTimes"
        Me.lblTimes.Size = New System.Drawing.Size(45, 16)
        Me.lblTimes.TabIndex = 15
        Me.lblTimes.Text = "Times:"
        '
        'cboTimes
        '
        Me.cboTimes.ButtonCancelText = "Cancelar"
        cboTimes_DesignTimeLayout.LayoutString = resources.GetString("cboTimes_DesignTimeLayout.LayoutString")
        Me.cboTimes.DesignTimeLayout = cboTimes_DesignTimeLayout
        Me.cboTimes.Location = New System.Drawing.Point(395, 109)
        Me.cboTimes.Name = "cboTimes"
        Me.cboTimes.SaveSettings = False
        Me.cboTimes.Size = New System.Drawing.Size(240, 23)
        Me.cboTimes.TabIndex = 16
        Me.cboTimes.ValuesDataMember = Nothing
        '
        'lblEmpresas
        '
        Me.lblEmpresas.AutoSize = True
        Me.lblEmpresas.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpresas.Location = New System.Drawing.Point(638, 93)
        Me.lblEmpresas.Name = "lblEmpresas"
        Me.lblEmpresas.Size = New System.Drawing.Size(75, 16)
        Me.lblEmpresas.TabIndex = 17
        Me.lblEmpresas.Text = "Empresas:"
        '
        'cboEmpresas
        '
        Me.cboEmpresas.ButtonCancelText = "Cancelar"
        cboEmpresas_DesignTimeLayout.LayoutString = resources.GetString("cboEmpresas_DesignTimeLayout.LayoutString")
        Me.cboEmpresas.DesignTimeLayout = cboEmpresas_DesignTimeLayout
        Me.cboEmpresas.Location = New System.Drawing.Point(641, 109)
        Me.cboEmpresas.Name = "cboEmpresas"
        Me.cboEmpresas.SaveSettings = False
        Me.cboEmpresas.Size = New System.Drawing.Size(220, 23)
        Me.cboEmpresas.TabIndex = 18
        Me.cboEmpresas.ValuesDataMember = Nothing
        '
        'lblChamado
        '
        Me.lblChamado.AutoSize = True
        Me.lblChamado.BackColor = System.Drawing.Color.Transparent
        Me.lblChamado.Location = New System.Drawing.Point(864, 93)
        Me.lblChamado.Name = "lblChamado"
        Me.lblChamado.Size = New System.Drawing.Size(67, 16)
        Me.lblChamado.TabIndex = 19
        Me.lblChamado.Text = "Chamado:"
        '
        'txtChamado
        '
        Me.txtChamado.Location = New System.Drawing.Point(867, 109)
        Me.txtChamado.MaxLength = 10
        Me.txtChamado.Name = "txtChamado"
        Me.txtChamado.Size = New System.Drawing.Size(90, 23)
        Me.txtChamado.TabIndex = 20
        '
        'lblDescricao
        '
        Me.lblDescricao.AutoSize = True
        Me.lblDescricao.BackColor = System.Drawing.Color.Transparent
        Me.lblDescricao.Location = New System.Drawing.Point(6, 133)
        Me.lblDescricao.Name = "lblDescricao"
        Me.lblDescricao.Size = New System.Drawing.Size(75, 16)
        Me.lblDescricao.TabIndex = 21
        Me.lblDescricao.Text = "Descrição:"
        '
        'txtDescricao
        '
        Me.txtDescricao.AcceptsReturn = True
        Me.txtDescricao.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescricao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescricao.Location = New System.Drawing.Point(9, 149)
        Me.txtDescricao.MaxLength = 4000
        Me.txtDescricao.Multiline = True
        Me.txtDescricao.Name = "txtDescricao"
        Me.txtDescricao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescricao.Size = New System.Drawing.Size(1164, 316)
        Me.txtDescricao.TabIndex = 22
        '
        'grpControl2
        '
        Me.grpControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpControl2.BackColor = System.Drawing.Color.Transparent
        Me.grpControl2.Controls.Add(Me.btnVoltar)
        Me.grpControl2.Controls.Add(Me.btnSalvar)
        Me.grpControl2.Controls.Add(Me.btnNovo)
        Me.grpControl2.Controls.Add(Me.btnExcluir)
        Me.grpControl2.Location = New System.Drawing.Point(7, 486)
        Me.grpControl2.Name = "grpControl2"
        Me.grpControl2.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControl2.Size = New System.Drawing.Size(1184, 51)
        Me.grpControl2.TabIndex = 1
        Me.grpControl2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnExcluir
        '
        Me.btnExcluir.Image = Global.Documentacti.My.Resources.Resources.cancelar
        Me.btnExcluir.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnExcluir.Location = New System.Drawing.Point(9, 17)
        Me.btnExcluir.Name = "btnExcluir"
        Me.btnExcluir.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnExcluir.Size = New System.Drawing.Size(91, 23)
        Me.btnExcluir.TabIndex = 0
        Me.btnExcluir.Text = "Excluir"
        Me.btnExcluir.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnNovo
        '
        Me.btnNovo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNovo.Image = Global.Documentacti.My.Resources.Resources.Novo
        Me.btnNovo.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnNovo.Location = New System.Drawing.Point(889, 17)
        Me.btnNovo.Name = "btnNovo"
        Me.btnNovo.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnNovo.Size = New System.Drawing.Size(91, 23)
        Me.btnNovo.TabIndex = 1
        Me.btnNovo.Text = "Novo"
        Me.btnNovo.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSalvar
        '
        Me.btnSalvar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSalvar.Image = Global.Documentacti.My.Resources.Resources.salvar
        Me.btnSalvar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSalvar.Location = New System.Drawing.Point(986, 17)
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
        Me.btnVoltar.Location = New System.Drawing.Point(1083, 17)
        Me.btnVoltar.Name = "btnVoltar"
        Me.btnVoltar.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnVoltar.Size = New System.Drawing.Size(91, 23)
        Me.btnVoltar.TabIndex = 3
        Me.btnVoltar.Text = "Voltar"
        Me.btnVoltar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'usrAdmEvento
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.tabMain)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "usrAdmEvento"
        Me.Size = New System.Drawing.Size(1200, 565)
        CType(Me.grpControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpControl2.ResumeLayout(False)
        Me.grpControl2.PerformLayout()
        CType(Me.grpDados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDados.ResumeLayout(False)
        Me.grpDados.PerformLayout()
        Me.pagDados.ResumeLayout(False)
        Me.pagDados.PerformLayout()
        CType(Me.grpControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpControl.ResumeLayout(False)
        Me.grpControl.PerformLayout()
        CType(Me.grdListagem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pagGrid.ResumeLayout(False)
        Me.pagGrid.PerformLayout()
        CType(Me.schMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCalendario.ResumeLayout(False)
        Me.pagCalendario.ResumeLayout(False)
        Me.pagCalendario.PerformLayout()
        CType(Me.tabVisao, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabVisao.ResumeLayout(False)
        CType(Me.grpFiltro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFiltro.ResumeLayout(False)
        Me.grpFiltro.PerformLayout()
        Me.pagLista.ResumeLayout(False)
        Me.pagLista.PerformLayout()
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents vsmMain As Janus.Windows.Common.VisualStyleManager
    Friend WithEvents tabMain As Janus.Windows.UI.Tab.UITab
    Friend WithEvents pagLista As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents grpFiltro As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblPeriodoFiltro As System.Windows.Forms.Label
    Friend WithEvents dtpInicioFiltro As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtpTerminoFiltro As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents lblTipoFiltro As System.Windows.Forms.Label
    Friend WithEvents cboTipoFiltro As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblTimeFiltro As System.Windows.Forms.Label
    Friend WithEvents cboTimeFiltro As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents lblParticipanteFiltro As System.Windows.Forms.Label
    Friend WithEvents cboParticipanteFiltro As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents lblEmpresaFiltro As System.Windows.Forms.Label
    Friend WithEvents cboEmpresaFiltro As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents lblTituloFiltro As System.Windows.Forms.Label
    Friend WithEvents txtTituloFiltro As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents btnFiltrar As Janus.Windows.EditControls.UIButton
    Friend WithEvents tabVisao As Janus.Windows.UI.Tab.UITab
    Friend WithEvents pagCalendario As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents btnAnterior As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnHoje As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnProximo As Janus.Windows.EditControls.UIButton
    Friend WithEvents lblPeriodo As System.Windows.Forms.Label
    Friend WithEvents pnlCalendario As System.Windows.Forms.Panel
    Friend WithEvents schMain As Janus.Windows.Schedule.Schedule
    Friend WithEvents pagGrid As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents grdListagem As Janus.Windows.GridEX.GridEX
    Friend WithEvents grpControl As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnExcel As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnNovo1 As Janus.Windows.EditControls.UIButton
    Friend WithEvents pagDados As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents grpDados As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents txtCodigo As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblTipo As System.Windows.Forms.Label
    Friend WithEvents cboTipo As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents txtTitulo As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents chkDiaInteiro As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents lblInicio As System.Windows.Forms.Label
    Friend WithEvents dtpInicio As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents lblFim As System.Windows.Forms.Label
    Friend WithEvents dtpFim As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents lblLocal As System.Windows.Forms.Label
    Friend WithEvents txtLocal As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblParticipantes As System.Windows.Forms.Label
    Friend WithEvents cboParticipantes As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents lblTimes As System.Windows.Forms.Label
    Friend WithEvents cboTimes As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents lblEmpresas As System.Windows.Forms.Label
    Friend WithEvents cboEmpresas As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents lblChamado As System.Windows.Forms.Label
    Friend WithEvents txtChamado As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblDescricao As System.Windows.Forms.Label
    Friend WithEvents txtDescricao As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents grpControl2 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnExcluir As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnNovo As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSalvar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnVoltar As Janus.Windows.EditControls.UIButton

End Class
