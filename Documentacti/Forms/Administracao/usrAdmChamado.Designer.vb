<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class usrAdmChamado
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(usrAdmChamado))
        Dim ChartArea1 As Dundas.Charting.WinControl.ChartArea = New Dundas.Charting.WinControl.ChartArea()
        Dim Legend1 As Dundas.Charting.WinControl.Legend = New Dundas.Charting.WinControl.Legend()
        Dim CommandUIItem1 As Dundas.Charting.WinControl.CommandUIItem = New Dundas.Charting.WinControl.CommandUIItem("SaveImage", "")
        Dim CommandUIItem2 As Dundas.Charting.WinControl.CommandUIItem = New Dundas.Charting.WinControl.CommandUIItem("Copy", "")
        Dim CommandUIItem3 As Dundas.Charting.WinControl.CommandUIItem = New Dundas.Charting.WinControl.CommandUIItem("Print", "")
        Dim CommandUIItem4 As Dundas.Charting.WinControl.CommandUIItem = New Dundas.Charting.WinControl.CommandUIItem("PrintPreview", "")
        Dim cboStatusFiltro_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdListagem_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdListagem_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image")
        Dim grdListagem_DesignTimeLayout_Reference_1 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column1.Image")
        Dim cboResponsavel_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cboTipoAtendimento_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdArquivos_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdArquivos_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image")
        Dim grdArquivos_DesignTimeLayout_Reference_1 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column1.Image")
        Dim grdComentario_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdComentario_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image")
        Dim grdComentarioDev_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdComentarioDev_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image")
        Dim grdApontamentoHistorico_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdApontamentoHistorico_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image")
        Dim grdApontamentoAndamento_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdApontamentoAndamento_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image")
        Dim grdRelatorio_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.tabMain = New Janus.Windows.UI.Tab.UITab()
        Me.pagLista = New Janus.Windows.UI.Tab.UITabPage()
        Me.pagDados = New Janus.Windows.UI.Tab.UITabPage()
        Me.pagApontamento = New Janus.Windows.UI.Tab.UITabPage()
        Me.pagRelatorio = New Janus.Windows.UI.Tab.UITabPage()
        Me.grpFiltro = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblDataFiltro = New System.Windows.Forms.Label()
        Me.dtpDataInicioFiltro = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.dtpDataTerminoFiltro = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.lblCodigoFiltro = New System.Windows.Forms.Label()
        Me.txtCodigoFiltro = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblEmpresaFiltro = New System.Windows.Forms.Label()
        Me.cboEmpresaFiltro = New Janus.Windows.EditControls.UIComboBox()
        Me.lblStatusFiltro = New System.Windows.Forms.Label()
        Me.cboStatusFiltro = New Janus.Windows.GridEX.EditControls.CheckedComboBox()
        Me.lblTituloFiltro = New System.Windows.Forms.Label()
        Me.txtTituloFiltro = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblTipoAtendimentoFiltro = New System.Windows.Forms.Label()
        Me.cboTipoAtendimentoFiltro = New Janus.Windows.EditControls.UIComboBox()
        Me.lblResponsavelFiltro = New System.Windows.Forms.Label()
        Me.cboResponsavelFiltro = New Janus.Windows.EditControls.UIComboBox()
        Me.btnFiltrar = New Janus.Windows.EditControls.UIButton()
        Me.grdListagem = New Janus.Windows.GridEX.GridEX()
        Me.grpLegenda = New Janus.Windows.EditControls.UIGroupBox()
        Me.picLegendaAberto = New System.Windows.Forms.PictureBox()
        Me.lblAberto = New System.Windows.Forms.Label()
        Me.picLegendaIniciado = New System.Windows.Forms.PictureBox()
        Me.lblIniciado = New System.Windows.Forms.Label()
        Me.picLegendaConcluido = New System.Windows.Forms.PictureBox()
        Me.lblConcluido = New System.Windows.Forms.Label()
        Me.picLegendaCancelado = New System.Windows.Forms.PictureBox()
        Me.lblCancelado = New System.Windows.Forms.Label()
        Me.picLegendaAgCliente = New System.Windows.Forms.PictureBox()
        Me.lblAgCliente = New System.Windows.Forms.Label()
        Me.grpControl = New Janus.Windows.EditControls.UIGroupBox()
        Me.btnExcel = New Janus.Windows.EditControls.UIButton()
        Me.btnNovo1 = New Janus.Windows.EditControls.UIButton()
        Me.btnSair = New Janus.Windows.EditControls.UIButton()
        Me.grpDados = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.txtCodigo = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblDataChamado = New System.Windows.Forms.Label()
        Me.dtpDataChamado = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtStatus = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblSolicitante = New System.Windows.Forms.Label()
        Me.txtSolicitante = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblEmpresa = New System.Windows.Forms.Label()
        Me.cboEmpresa = New Janus.Windows.EditControls.UIComboBox()
        Me.lblPrioridade = New System.Windows.Forms.Label()
        Me.cboPrioridade = New Janus.Windows.EditControls.UIComboBox()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.txtTitulo = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblTipoChamado = New System.Windows.Forms.Label()
        Me.cboTipoAtividade = New Janus.Windows.EditControls.UIComboBox()
        Me.lblResponsavel = New System.Windows.Forms.Label()
        Me.cboResponsavel = New Janus.Windows.GridEX.EditControls.CheckedComboBox()
        Me.lblTipoAtendimento = New System.Windows.Forms.Label()
        Me.cboTipoAtendimento = New Janus.Windows.GridEX.EditControls.CheckedComboBox()
        Me.lblModulo = New System.Windows.Forms.Label()
        Me.txtModulo = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblTela = New System.Windows.Forms.Label()
        Me.txtTela = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblNumeroDocumento = New System.Windows.Forms.Label()
        Me.txtNumeroDocumento = New Janus.Windows.GridEX.EditControls.MaskedEditBox()
        Me.lblDefinicao = New System.Windows.Forms.Label()
        Me.txtDescricao = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.chkExibirUsuario = New Janus.Windows.EditControls.UICheckBox()
        Me.tabDadosChamado = New Janus.Windows.UI.Tab.UITab()
        Me.pagArquivos = New Janus.Windows.UI.Tab.UITabPage()
        Me.pagComentario = New Janus.Windows.UI.Tab.UITabPage()
        Me.pagComentarioDev = New Janus.Windows.UI.Tab.UITabPage()
        Me.btnArquivo = New Janus.Windows.EditControls.UIButton()
        Me.grdArquivos = New Janus.Windows.GridEX.GridEX()
        Me.grdComentario = New Janus.Windows.GridEX.GridEX()
        Me.grdComentarioDev = New Janus.Windows.GridEX.GridEX()
        Me.grpControl2 = New Janus.Windows.EditControls.UIGroupBox()
        Me.btnIniciar = New Janus.Windows.EditControls.UIButton()
        Me.btnFinalizarChamado = New Janus.Windows.EditControls.UIButton()
        Me.btnInserirComentario = New Janus.Windows.EditControls.UIButton()
        Me.btnInserirComentarioDev = New Janus.Windows.EditControls.UIButton()
        Me.btnNovo = New Janus.Windows.EditControls.UIButton()
        Me.btnSalvar = New Janus.Windows.EditControls.UIButton()
        Me.btnVoltar = New Janus.Windows.EditControls.UIButton()
        Me.btnSair2 = New Janus.Windows.EditControls.UIButton()
        Me.grpApontamento = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblEmpresaApontamento = New System.Windows.Forms.Label()
        Me.cboEmpresaApontamento = New Janus.Windows.EditControls.UIComboBox()
        Me.lblChamadoApontamento = New System.Windows.Forms.Label()
        Me.cboChamadoApontamento = New Janus.Windows.EditControls.UIComboBox()
        Me.btnIniciarApontamento = New Janus.Windows.EditControls.UIButton()
        Me.btnEncerrarApontamento = New Janus.Windows.EditControls.UIButton()
        Me.grpFiltroApontamento = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblPeriodoApontamento = New System.Windows.Forms.Label()
        Me.dtpInicioApontamento = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.dtpTerminoApontamento = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.lblResponsavelApontamento = New System.Windows.Forms.Label()
        Me.cboResponsavelApontamento = New Janus.Windows.EditControls.UIComboBox()
        Me.lblTotalApontamento = New System.Windows.Forms.Label()
        Me.btnFiltrarApontamento = New Janus.Windows.EditControls.UIButton()
        Me.tlpApontamento = New System.Windows.Forms.TableLayoutPanel()
        Me.grpApontamentoHistorico = New Janus.Windows.EditControls.UIGroupBox()
        Me.grdApontamentoHistorico = New Janus.Windows.GridEX.GridEX()
        Me.grpApontamentoAndamento = New Janus.Windows.EditControls.UIGroupBox()
        Me.grdApontamentoAndamento = New Janus.Windows.GridEX.GridEX()
        Me.grpControl3 = New Janus.Windows.EditControls.UIGroupBox()
        Me.btnExcelApontamento = New Janus.Windows.EditControls.UIButton()
        Me.btnSair3 = New Janus.Windows.EditControls.UIButton()
        Me.grpFiltroRelatorio = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblPeriodoRelatorio = New System.Windows.Forms.Label()
        Me.dtpInicioRelatorio = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.dtpTerminoRelatorio = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.lblEmpresaRelatorio = New System.Windows.Forms.Label()
        Me.cboEmpresaRelatorio = New Janus.Windows.EditControls.UIComboBox()
        Me.lblTipoAtividadeRelatorio = New System.Windows.Forms.Label()
        Me.cboTipoAtividadeRelatorio = New Janus.Windows.EditControls.UIComboBox()
        Me.lblTipoAtendimentoRelatorio = New System.Windows.Forms.Label()
        Me.cboTipoAtendimentoRelatorio = New Janus.Windows.EditControls.UIComboBox()
        Me.lblResponsavelRelatorio = New System.Windows.Forms.Label()
        Me.cboResponsavelRelatorio = New Janus.Windows.EditControls.UIComboBox()
        Me.lblAgruparRelatorio = New System.Windows.Forms.Label()
        Me.cboAgruparRelatorio = New Janus.Windows.EditControls.UIComboBox()
        Me.lblGraficoRelatorio = New System.Windows.Forms.Label()
        Me.cboGraficoRelatorio = New Janus.Windows.EditControls.UIComboBox()
        Me.lblTotalRelatorio = New System.Windows.Forms.Label()
        Me.btnGerarRelatorio = New Janus.Windows.EditControls.UIButton()
        Me.tlpRelatorio = New System.Windows.Forms.TableLayoutPanel()
        Me.grpGraficoRelatorio = New Janus.Windows.EditControls.UIGroupBox()
        Me.chrRelatorio = New Dundas.Charting.WinControl.Chart()
        Me.grpResumoRelatorio = New Janus.Windows.EditControls.UIGroupBox()
        Me.grdRelatorio = New Janus.Windows.GridEX.GridEX()
        Me.grpControl4 = New Janus.Windows.EditControls.UIGroupBox()
        Me.btnExcelRelatorio = New Janus.Windows.EditControls.UIButton()
        Me.btnSair4 = New Janus.Windows.EditControls.UIButton()
        Me.vsmMain = New Janus.Windows.Common.VisualStyleManager(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.tmrApontamento = New System.Windows.Forms.Timer(Me.components)
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMain.SuspendLayout()
        Me.pagLista.SuspendLayout()
        Me.pagDados.SuspendLayout()
        Me.pagApontamento.SuspendLayout()
        Me.pagRelatorio.SuspendLayout()
        CType(Me.grpFiltro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFiltro.SuspendLayout()
        CType(Me.grdListagem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpLegenda, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLegenda.SuspendLayout()
        CType(Me.picLegendaAberto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLegendaIniciado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLegendaConcluido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLegendaCancelado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLegendaAgCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpControl.SuspendLayout()
        CType(Me.grpDados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDados.SuspendLayout()
        CType(Me.tabDadosChamado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabDadosChamado.SuspendLayout()
        Me.pagArquivos.SuspendLayout()
        Me.pagComentario.SuspendLayout()
        Me.pagComentarioDev.SuspendLayout()
        CType(Me.grdArquivos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdComentario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdComentarioDev, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpControl2.SuspendLayout()
        CType(Me.grpApontamento, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpApontamento.SuspendLayout()
        CType(Me.grpFiltroApontamento, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFiltroApontamento.SuspendLayout()
        Me.tlpApontamento.SuspendLayout()
        CType(Me.grpApontamentoHistorico, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpApontamentoHistorico.SuspendLayout()
        CType(Me.grdApontamentoHistorico, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpApontamentoAndamento, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpApontamentoAndamento.SuspendLayout()
        CType(Me.grdApontamentoAndamento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpControl3.SuspendLayout()
        CType(Me.grpFiltroRelatorio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFiltroRelatorio.SuspendLayout()
        Me.tlpRelatorio.SuspendLayout()
        CType(Me.grpGraficoRelatorio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGraficoRelatorio.SuspendLayout()
        CType(Me.chrRelatorio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpResumoRelatorio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpResumoRelatorio.SuspendLayout()
        CType(Me.grdRelatorio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpControl4.SuspendLayout()
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
        Me.tabMain.TabPages.AddRange(New Janus.Windows.UI.Tab.UITabPage() {Me.pagLista, Me.pagDados, Me.pagApontamento, Me.pagRelatorio})
        Me.tabMain.TabStop = False
        Me.tabMain.VisualStyleManager = Me.vsmMain
        '
        'pagLista
        '
        Me.pagLista.Controls.Add(Me.grpControl)
        Me.pagLista.Controls.Add(Me.grpLegenda)
        Me.pagLista.Controls.Add(Me.grdListagem)
        Me.pagLista.Controls.Add(Me.grpFiltro)
        Me.pagLista.Key = "pagLista"
        Me.pagLista.Location = New System.Drawing.Point(1, 22)
        Me.pagLista.Name = "pagLista"
        Me.pagLista.Size = New System.Drawing.Size(1198, 542)
        Me.pagLista.TabStop = True
        Me.pagLista.Text = "Controle de Chamados"
        '
        'pagDados
        '
        Me.pagDados.Controls.Add(Me.grpControl2)
        Me.pagDados.Controls.Add(Me.tabDadosChamado)
        Me.pagDados.Controls.Add(Me.grpDados)
        Me.pagDados.Key = "pagDados"
        Me.pagDados.Location = New System.Drawing.Point(1, 22)
        Me.pagDados.Name = "pagDados"
        Me.pagDados.Size = New System.Drawing.Size(1198, 542)
        Me.pagDados.TabStop = True
        Me.pagDados.Text = "Dados do Chamado"
        '
        'pagApontamento
        '
        Me.pagApontamento.Controls.Add(Me.grpControl3)
        Me.pagApontamento.Controls.Add(Me.tlpApontamento)
        Me.pagApontamento.Controls.Add(Me.grpFiltroApontamento)
        Me.pagApontamento.Controls.Add(Me.grpApontamento)
        Me.pagApontamento.Key = "pagApontamento"
        Me.pagApontamento.Location = New System.Drawing.Point(1, 22)
        Me.pagApontamento.Name = "pagApontamento"
        Me.pagApontamento.Size = New System.Drawing.Size(1198, 542)
        Me.pagApontamento.TabStop = True
        Me.pagApontamento.Text = "Apontamentos"
        '
        'pagRelatorio
        '
        Me.pagRelatorio.Controls.Add(Me.grpControl4)
        Me.pagRelatorio.Controls.Add(Me.tlpRelatorio)
        Me.pagRelatorio.Controls.Add(Me.grpFiltroRelatorio)
        Me.pagRelatorio.Key = "pagRelatorio"
        Me.pagRelatorio.Location = New System.Drawing.Point(1, 22)
        Me.pagRelatorio.Name = "pagRelatorio"
        Me.pagRelatorio.Size = New System.Drawing.Size(1198, 542)
        Me.pagRelatorio.TabStop = True
        Me.pagRelatorio.Text = "Relatório de Horas"
        '
        'grpFiltro
        '
        Me.grpFiltro.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFiltro.BackColor = System.Drawing.Color.Transparent
        Me.grpFiltro.Controls.Add(Me.btnFiltrar)
        Me.grpFiltro.Controls.Add(Me.cboResponsavelFiltro)
        Me.grpFiltro.Controls.Add(Me.lblResponsavelFiltro)
        Me.grpFiltro.Controls.Add(Me.cboTipoAtendimentoFiltro)
        Me.grpFiltro.Controls.Add(Me.lblTipoAtendimentoFiltro)
        Me.grpFiltro.Controls.Add(Me.txtTituloFiltro)
        Me.grpFiltro.Controls.Add(Me.lblTituloFiltro)
        Me.grpFiltro.Controls.Add(Me.cboStatusFiltro)
        Me.grpFiltro.Controls.Add(Me.lblStatusFiltro)
        Me.grpFiltro.Controls.Add(Me.cboEmpresaFiltro)
        Me.grpFiltro.Controls.Add(Me.lblEmpresaFiltro)
        Me.grpFiltro.Controls.Add(Me.txtCodigoFiltro)
        Me.grpFiltro.Controls.Add(Me.lblCodigoFiltro)
        Me.grpFiltro.Controls.Add(Me.dtpDataTerminoFiltro)
        Me.grpFiltro.Controls.Add(Me.dtpDataInicioFiltro)
        Me.grpFiltro.Controls.Add(Me.lblDataFiltro)
        Me.grpFiltro.Location = New System.Drawing.Point(7, 6)
        Me.grpFiltro.Name = "grpFiltro"
        Me.grpFiltro.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpFiltro.Size = New System.Drawing.Size(1182, 104)
        Me.grpFiltro.TabIndex = 0
        Me.grpFiltro.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblDataFiltro
        '
        Me.lblDataFiltro.AutoSize = True
        Me.lblDataFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblDataFiltro.Location = New System.Drawing.Point(6, 17)
        Me.lblDataFiltro.Name = "lblDataFiltro"
        Me.lblDataFiltro.TabIndex = 0
        Me.lblDataFiltro.Text = "Data:"
        '
        'dtpDataInicioFiltro
        '
        '
        '
        '
        Me.dtpDataInicioFiltro.DropDownCalendar.Name = ""
        Me.dtpDataInicioFiltro.Location = New System.Drawing.Point(9, 34)
        Me.dtpDataInicioFiltro.Name = "dtpDataInicioFiltro"
        Me.dtpDataInicioFiltro.ShowCheckBox = True
        Me.dtpDataInicioFiltro.Size = New System.Drawing.Size(108, 20)
        Me.dtpDataInicioFiltro.TabIndex = 1
        Me.dtpDataInicioFiltro.Value = New Date(2026, 1, 1, 0, 0, 0, 0)
        '
        'dtpDataTerminoFiltro
        '
        '
        '
        '
        Me.dtpDataTerminoFiltro.DropDownCalendar.Name = ""
        Me.dtpDataTerminoFiltro.Location = New System.Drawing.Point(123, 34)
        Me.dtpDataTerminoFiltro.Name = "dtpDataTerminoFiltro"
        Me.dtpDataTerminoFiltro.ShowCheckBox = True
        Me.dtpDataTerminoFiltro.Size = New System.Drawing.Size(108, 20)
        Me.dtpDataTerminoFiltro.TabIndex = 2
        Me.dtpDataTerminoFiltro.Value = New Date(2026, 1, 1, 0, 0, 0, 0)
        '
        'lblCodigoFiltro
        '
        Me.lblCodigoFiltro.AutoSize = True
        Me.lblCodigoFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblCodigoFiltro.Location = New System.Drawing.Point(234, 17)
        Me.lblCodigoFiltro.Name = "lblCodigoFiltro"
        Me.lblCodigoFiltro.TabIndex = 3
        Me.lblCodigoFiltro.Text = "Código:"
        '
        'txtCodigoFiltro
        '
        Me.txtCodigoFiltro.Location = New System.Drawing.Point(237, 34)
        Me.txtCodigoFiltro.Name = "txtCodigoFiltro"
        Me.txtCodigoFiltro.MaxLength = 10
        Me.txtCodigoFiltro.Size = New System.Drawing.Size(90, 20)
        Me.txtCodigoFiltro.TabIndex = 4
        '
        'lblEmpresaFiltro
        '
        Me.lblEmpresaFiltro.AutoSize = True
        Me.lblEmpresaFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpresaFiltro.Location = New System.Drawing.Point(330, 17)
        Me.lblEmpresaFiltro.Name = "lblEmpresaFiltro"
        Me.lblEmpresaFiltro.TabIndex = 5
        Me.lblEmpresaFiltro.Text = "Empresa:"
        '
        'cboEmpresaFiltro
        '
        Me.cboEmpresaFiltro.AutoSize = False
        Me.cboEmpresaFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboEmpresaFiltro.Location = New System.Drawing.Point(333, 34)
        Me.cboEmpresaFiltro.Name = "cboEmpresaFiltro"
        Me.cboEmpresaFiltro.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboEmpresaFiltro.Size = New System.Drawing.Size(369, 20)
        Me.cboEmpresaFiltro.TabIndex = 6
        '
        'lblStatusFiltro
        '
        Me.lblStatusFiltro.AutoSize = True
        Me.lblStatusFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblStatusFiltro.Location = New System.Drawing.Point(705, 17)
        Me.lblStatusFiltro.Name = "lblStatusFiltro"
        Me.lblStatusFiltro.TabIndex = 7
        Me.lblStatusFiltro.Text = "Status:"
        '
        'cboStatusFiltro
        '
        Me.cboStatusFiltro.ButtonCancelText = "Cancelar"
        cboStatusFiltro_DesignTimeLayout.LayoutString = resources.GetString("cboStatusFiltro_DesignTimeLayout.LayoutString")
        Me.cboStatusFiltro.DesignTimeLayout = cboStatusFiltro_DesignTimeLayout
        Me.cboStatusFiltro.Location = New System.Drawing.Point(708, 34)
        Me.cboStatusFiltro.Name = "cboStatusFiltro"
        Me.cboStatusFiltro.SaveSettings = False
        Me.cboStatusFiltro.Size = New System.Drawing.Size(250, 20)
        Me.cboStatusFiltro.TabIndex = 8
        Me.cboStatusFiltro.ValuesDataMember = Nothing
        '
        'lblTituloFiltro
        '
        Me.lblTituloFiltro.AutoSize = True
        Me.lblTituloFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblTituloFiltro.Location = New System.Drawing.Point(6, 57)
        Me.lblTituloFiltro.Name = "lblTituloFiltro"
        Me.lblTituloFiltro.TabIndex = 9
        Me.lblTituloFiltro.Text = "Atividade:"
        '
        'txtTituloFiltro
        '
        Me.txtTituloFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTituloFiltro.Location = New System.Drawing.Point(9, 74)
        Me.txtTituloFiltro.Name = "txtTituloFiltro"
        Me.txtTituloFiltro.MaxLength = 100
        Me.txtTituloFiltro.Size = New System.Drawing.Size(318, 20)
        Me.txtTituloFiltro.TabIndex = 10
        '
        'lblTipoAtendimentoFiltro
        '
        Me.lblTipoAtendimentoFiltro.AutoSize = True
        Me.lblTipoAtendimentoFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblTipoAtendimentoFiltro.Location = New System.Drawing.Point(330, 57)
        Me.lblTipoAtendimentoFiltro.Name = "lblTipoAtendimentoFiltro"
        Me.lblTipoAtendimentoFiltro.TabIndex = 11
        Me.lblTipoAtendimentoFiltro.Text = "Tipo de Atendimento:"
        '
        'cboTipoAtendimentoFiltro
        '
        Me.cboTipoAtendimentoFiltro.AutoSize = False
        Me.cboTipoAtendimentoFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboTipoAtendimentoFiltro.Location = New System.Drawing.Point(333, 74)
        Me.cboTipoAtendimentoFiltro.Name = "cboTipoAtendimentoFiltro"
        Me.cboTipoAtendimentoFiltro.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboTipoAtendimentoFiltro.Size = New System.Drawing.Size(369, 20)
        Me.cboTipoAtendimentoFiltro.TabIndex = 12
        '
        'lblResponsavelFiltro
        '
        Me.lblResponsavelFiltro.AutoSize = True
        Me.lblResponsavelFiltro.BackColor = System.Drawing.Color.Transparent
        Me.lblResponsavelFiltro.Location = New System.Drawing.Point(705, 57)
        Me.lblResponsavelFiltro.Name = "lblResponsavelFiltro"
        Me.lblResponsavelFiltro.TabIndex = 13
        Me.lblResponsavelFiltro.Text = "Responsável:"
        '
        'cboResponsavelFiltro
        '
        Me.cboResponsavelFiltro.AutoSize = False
        Me.cboResponsavelFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboResponsavelFiltro.Location = New System.Drawing.Point(708, 74)
        Me.cboResponsavelFiltro.Name = "cboResponsavelFiltro"
        Me.cboResponsavelFiltro.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboResponsavelFiltro.Size = New System.Drawing.Size(250, 20)
        Me.cboResponsavelFiltro.TabIndex = 14
        '
        'btnFiltrar
        '
        Me.btnFiltrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFiltrar.Image = Global.Documentacti.My.Resources.Resources.procurar
        Me.btnFiltrar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnFiltrar.Location = New System.Drawing.Point(1082, 71)
        Me.btnFiltrar.Name = "btnFiltrar"
        Me.btnFiltrar.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnFiltrar.Size = New System.Drawing.Size(91, 23)
        Me.btnFiltrar.TabIndex = 15
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
        Me.grdListagem.Location = New System.Drawing.Point(7, 116)
        Me.grdListagem.Name = "grdListagem"
        Me.grdListagem.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdListagem.RecordNavigator = True
        Me.grdListagem.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdListagem.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdListagem.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdListagem.Size = New System.Drawing.Size(1184, 364)
        Me.grdListagem.TabIndex = 1
        Me.grdListagem.TabStop = False
        Me.grdListagem.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdListagem.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grpLegenda
        '
        Me.grpLegenda.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpLegenda.BackColor = System.Drawing.Color.Transparent
        Me.grpLegenda.Controls.Add(Me.lblAgCliente)
        Me.grpLegenda.Controls.Add(Me.picLegendaAgCliente)
        Me.grpLegenda.Controls.Add(Me.lblCancelado)
        Me.grpLegenda.Controls.Add(Me.picLegendaCancelado)
        Me.grpLegenda.Controls.Add(Me.lblConcluido)
        Me.grpLegenda.Controls.Add(Me.picLegendaConcluido)
        Me.grpLegenda.Controls.Add(Me.lblIniciado)
        Me.grpLegenda.Controls.Add(Me.picLegendaIniciado)
        Me.grpLegenda.Controls.Add(Me.lblAberto)
        Me.grpLegenda.Controls.Add(Me.picLegendaAberto)
        Me.grpLegenda.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpLegenda.Location = New System.Drawing.Point(7, 486)
        Me.grpLegenda.Name = "grpLegenda"
        Me.grpLegenda.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpLegenda.Size = New System.Drawing.Size(405, 51)
        Me.grpLegenda.TabIndex = 2
        Me.grpLegenda.Text = "Legenda"
        Me.grpLegenda.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'picLegendaAberto
        '
        Me.picLegendaAberto.BackColor = System.Drawing.Color.DimGray
        Me.picLegendaAberto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picLegendaAberto.Location = New System.Drawing.Point(10, 22)
        Me.picLegendaAberto.Name = "picLegendaAberto"
        Me.picLegendaAberto.Size = New System.Drawing.Size(12, 12)
        Me.picLegendaAberto.TabIndex = 20
        Me.picLegendaAberto.TabStop = False
        '
        'lblAberto
        '
        Me.lblAberto.AutoSize = True
        Me.lblAberto.BackColor = System.Drawing.Color.Transparent
        Me.lblAberto.Location = New System.Drawing.Point(26, 21)
        Me.lblAberto.Name = "lblAberto"
        Me.lblAberto.TabIndex = 21
        Me.lblAberto.Text = "Aberto"
        '
        'picLegendaIniciado
        '
        Me.picLegendaIniciado.BackColor = System.Drawing.Color.SteelBlue
        Me.picLegendaIniciado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picLegendaIniciado.Location = New System.Drawing.Point(85, 22)
        Me.picLegendaIniciado.Name = "picLegendaIniciado"
        Me.picLegendaIniciado.Size = New System.Drawing.Size(12, 12)
        Me.picLegendaIniciado.TabIndex = 22
        Me.picLegendaIniciado.TabStop = False
        '
        'lblIniciado
        '
        Me.lblIniciado.AutoSize = True
        Me.lblIniciado.BackColor = System.Drawing.Color.Transparent
        Me.lblIniciado.Location = New System.Drawing.Point(101, 21)
        Me.lblIniciado.Name = "lblIniciado"
        Me.lblIniciado.TabIndex = 23
        Me.lblIniciado.Text = "Iniciado"
        '
        'picLegendaConcluido
        '
        Me.picLegendaConcluido.BackColor = System.Drawing.Color.ForestGreen
        Me.picLegendaConcluido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picLegendaConcluido.Location = New System.Drawing.Point(165, 22)
        Me.picLegendaConcluido.Name = "picLegendaConcluido"
        Me.picLegendaConcluido.Size = New System.Drawing.Size(12, 12)
        Me.picLegendaConcluido.TabIndex = 24
        Me.picLegendaConcluido.TabStop = False
        '
        'lblConcluido
        '
        Me.lblConcluido.AutoSize = True
        Me.lblConcluido.BackColor = System.Drawing.Color.Transparent
        Me.lblConcluido.Location = New System.Drawing.Point(181, 21)
        Me.lblConcluido.Name = "lblConcluido"
        Me.lblConcluido.TabIndex = 25
        Me.lblConcluido.Text = "Concluído"
        '
        'picLegendaCancelado
        '
        Me.picLegendaCancelado.BackColor = System.Drawing.Color.Firebrick
        Me.picLegendaCancelado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picLegendaCancelado.Location = New System.Drawing.Point(250, 22)
        Me.picLegendaCancelado.Name = "picLegendaCancelado"
        Me.picLegendaCancelado.Size = New System.Drawing.Size(12, 12)
        Me.picLegendaCancelado.TabIndex = 26
        Me.picLegendaCancelado.TabStop = False
        '
        'lblCancelado
        '
        Me.lblCancelado.AutoSize = True
        Me.lblCancelado.BackColor = System.Drawing.Color.Transparent
        Me.lblCancelado.Location = New System.Drawing.Point(266, 21)
        Me.lblCancelado.Name = "lblCancelado"
        Me.lblCancelado.TabIndex = 27
        Me.lblCancelado.Text = "Cancelado"
        '
        'picLegendaAgCliente
        '
        Me.picLegendaAgCliente.BackColor = System.Drawing.Color.Purple
        Me.picLegendaAgCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picLegendaAgCliente.Location = New System.Drawing.Point(330, 22)
        Me.picLegendaAgCliente.Name = "picLegendaAgCliente"
        Me.picLegendaAgCliente.Size = New System.Drawing.Size(12, 12)
        Me.picLegendaAgCliente.TabIndex = 28
        Me.picLegendaAgCliente.TabStop = False
        '
        'lblAgCliente
        '
        Me.lblAgCliente.AutoSize = True
        Me.lblAgCliente.BackColor = System.Drawing.Color.Transparent
        Me.lblAgCliente.Location = New System.Drawing.Point(346, 21)
        Me.lblAgCliente.Name = "lblAgCliente"
        Me.lblAgCliente.TabIndex = 29
        Me.lblAgCliente.Text = "Ag. Cliente"
        '
        'grpControl
        '
        Me.grpControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpControl.BackColor = System.Drawing.Color.Transparent
        Me.grpControl.Controls.Add(Me.btnSair)
        Me.grpControl.Controls.Add(Me.btnNovo1)
        Me.grpControl.Controls.Add(Me.btnExcel)
        Me.grpControl.Location = New System.Drawing.Point(418, 486)
        Me.grpControl.Name = "grpControl"
        Me.grpControl.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControl.Size = New System.Drawing.Size(771, 51)
        Me.grpControl.TabIndex = 3
        Me.grpControl.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcel.Image = Global.Documentacti.My.Resources.Resources.excel
        Me.btnExcel.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnExcel.Location = New System.Drawing.Point(477, 17)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnExcel.Size = New System.Drawing.Size(91, 23)
        Me.btnExcel.TabIndex = 1
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnNovo1
        '
        Me.btnNovo1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNovo1.Image = Global.Documentacti.My.Resources.Resources.Novo
        Me.btnNovo1.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnNovo1.ImageSize = New System.Drawing.Size(13, 16)
        Me.btnNovo1.Location = New System.Drawing.Point(574, 17)
        Me.btnNovo1.Name = "btnNovo1"
        Me.btnNovo1.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnNovo1.Size = New System.Drawing.Size(91, 23)
        Me.btnNovo1.TabIndex = 2
        Me.btnNovo1.Text = "Novo"
        Me.btnNovo1.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSair
        '
        Me.btnSair.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSair.Image = CType(resources.GetObject("btnSair.Image"), System.Drawing.Image)
        Me.btnSair.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSair.ImageSize = New System.Drawing.Size(19, 16)
        Me.btnSair.Location = New System.Drawing.Point(671, 17)
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
        Me.grpDados.Controls.Add(Me.chkExibirUsuario)
        Me.grpDados.Controls.Add(Me.txtDescricao)
        Me.grpDados.Controls.Add(Me.lblDefinicao)
        Me.grpDados.Controls.Add(Me.txtNumeroDocumento)
        Me.grpDados.Controls.Add(Me.lblNumeroDocumento)
        Me.grpDados.Controls.Add(Me.txtTela)
        Me.grpDados.Controls.Add(Me.lblTela)
        Me.grpDados.Controls.Add(Me.txtModulo)
        Me.grpDados.Controls.Add(Me.lblModulo)
        Me.grpDados.Controls.Add(Me.cboTipoAtendimento)
        Me.grpDados.Controls.Add(Me.lblTipoAtendimento)
        Me.grpDados.Controls.Add(Me.cboResponsavel)
        Me.grpDados.Controls.Add(Me.lblResponsavel)
        Me.grpDados.Controls.Add(Me.cboTipoAtividade)
        Me.grpDados.Controls.Add(Me.lblTipoChamado)
        Me.grpDados.Controls.Add(Me.txtTitulo)
        Me.grpDados.Controls.Add(Me.lblTitulo)
        Me.grpDados.Controls.Add(Me.cboPrioridade)
        Me.grpDados.Controls.Add(Me.lblPrioridade)
        Me.grpDados.Controls.Add(Me.cboEmpresa)
        Me.grpDados.Controls.Add(Me.lblEmpresa)
        Me.grpDados.Controls.Add(Me.txtSolicitante)
        Me.grpDados.Controls.Add(Me.lblSolicitante)
        Me.grpDados.Controls.Add(Me.txtStatus)
        Me.grpDados.Controls.Add(Me.lblStatus)
        Me.grpDados.Controls.Add(Me.dtpDataChamado)
        Me.grpDados.Controls.Add(Me.lblDataChamado)
        Me.grpDados.Controls.Add(Me.txtCodigo)
        Me.grpDados.Controls.Add(Me.lblCodigo)
        Me.grpDados.Location = New System.Drawing.Point(7, 6)
        Me.grpDados.Name = "grpDados"
        Me.grpDados.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpDados.Size = New System.Drawing.Size(1182, 235)
        Me.grpDados.TabIndex = 0
        Me.grpDados.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.BackColor = System.Drawing.Color.Transparent
        Me.lblCodigo.Location = New System.Drawing.Point(6, 13)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.TabIndex = 0
        Me.lblCodigo.Text = "Código:"
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(9, 29)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.ReadOnly = True
        Me.txtCodigo.Size = New System.Drawing.Size(80, 20)
        Me.txtCodigo.TabIndex = 1
        Me.txtCodigo.TabStop = False
        '
        'lblDataChamado
        '
        Me.lblDataChamado.AutoSize = True
        Me.lblDataChamado.BackColor = System.Drawing.Color.Transparent
        Me.lblDataChamado.Location = New System.Drawing.Point(92, 13)
        Me.lblDataChamado.Name = "lblDataChamado"
        Me.lblDataChamado.TabIndex = 2
        Me.lblDataChamado.Text = "Data:"
        '
        'dtpDataChamado
        '
        '
        '
        '
        Me.dtpDataChamado.DropDownCalendar.Name = ""
        Me.dtpDataChamado.Location = New System.Drawing.Point(95, 29)
        Me.dtpDataChamado.Name = "dtpDataChamado"
        Me.dtpDataChamado.ReadOnly = True
        Me.dtpDataChamado.Size = New System.Drawing.Size(108, 20)
        Me.dtpDataChamado.TabIndex = 3
        Me.dtpDataChamado.TabStop = False
        Me.dtpDataChamado.Value = New Date(2026, 1, 1, 0, 0, 0, 0)
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Location = New System.Drawing.Point(206, 13)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.TabIndex = 4
        Me.lblStatus.Text = "Status:"
        '
        'txtStatus
        '
        Me.txtStatus.Location = New System.Drawing.Point(209, 29)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(200, 20)
        Me.txtStatus.TabIndex = 5
        Me.txtStatus.TabStop = False
        '
        'lblSolicitante
        '
        Me.lblSolicitante.AutoSize = True
        Me.lblSolicitante.BackColor = System.Drawing.Color.Transparent
        Me.lblSolicitante.Location = New System.Drawing.Point(412, 13)
        Me.lblSolicitante.Name = "lblSolicitante"
        Me.lblSolicitante.TabIndex = 6
        Me.lblSolicitante.Text = "Solicitante:"
        '
        'txtSolicitante
        '
        Me.txtSolicitante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSolicitante.Location = New System.Drawing.Point(415, 29)
        Me.txtSolicitante.Name = "txtSolicitante"
        Me.txtSolicitante.MaxLength = 100
        Me.txtSolicitante.Size = New System.Drawing.Size(250, 20)
        Me.txtSolicitante.TabIndex = 7
        '
        'lblEmpresa
        '
        Me.lblEmpresa.AutoSize = True
        Me.lblEmpresa.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpresa.Location = New System.Drawing.Point(668, 13)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.TabIndex = 8
        Me.lblEmpresa.Text = "Empresa:"
        '
        'cboEmpresa
        '
        Me.cboEmpresa.AutoSize = False
        Me.cboEmpresa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboEmpresa.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboEmpresa.Location = New System.Drawing.Point(671, 29)
        Me.cboEmpresa.Name = "cboEmpresa"
        Me.cboEmpresa.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboEmpresa.Size = New System.Drawing.Size(300, 20)
        Me.cboEmpresa.TabIndex = 9
        '
        'lblPrioridade
        '
        Me.lblPrioridade.AutoSize = True
        Me.lblPrioridade.BackColor = System.Drawing.Color.Transparent
        Me.lblPrioridade.Location = New System.Drawing.Point(974, 13)
        Me.lblPrioridade.Name = "lblPrioridade"
        Me.lblPrioridade.TabIndex = 10
        Me.lblPrioridade.Text = "Prioridade:"
        '
        'cboPrioridade
        '
        Me.cboPrioridade.AutoSize = False
        Me.cboPrioridade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboPrioridade.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboPrioridade.Location = New System.Drawing.Point(977, 29)
        Me.cboPrioridade.Name = "cboPrioridade"
        Me.cboPrioridade.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboPrioridade.Size = New System.Drawing.Size(196, 20)
        Me.cboPrioridade.TabIndex = 11
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.BackColor = System.Drawing.Color.Transparent
        Me.lblTitulo.Location = New System.Drawing.Point(6, 53)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.TabIndex = 12
        Me.lblTitulo.Text = "Atividade:"
        '
        'txtTitulo
        '
        Me.txtTitulo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTitulo.Location = New System.Drawing.Point(9, 69)
        Me.txtTitulo.Name = "txtTitulo"
        Me.txtTitulo.MaxLength = 100
        Me.txtTitulo.Size = New System.Drawing.Size(656, 20)
        Me.txtTitulo.TabIndex = 13
        '
        'lblTipoChamado
        '
        Me.lblTipoChamado.AutoSize = True
        Me.lblTipoChamado.BackColor = System.Drawing.Color.Transparent
        Me.lblTipoChamado.Location = New System.Drawing.Point(668, 53)
        Me.lblTipoChamado.Name = "lblTipoChamado"
        Me.lblTipoChamado.TabIndex = 14
        Me.lblTipoChamado.Text = "Tipo:"
        '
        'cboTipoAtividade
        '
        Me.cboTipoAtividade.AutoSize = False
        Me.cboTipoAtividade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboTipoAtividade.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboTipoAtividade.Location = New System.Drawing.Point(671, 69)
        Me.cboTipoAtividade.Name = "cboTipoAtividade"
        Me.cboTipoAtividade.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboTipoAtividade.Size = New System.Drawing.Size(300, 20)
        Me.cboTipoAtividade.TabIndex = 15
        '
        'lblResponsavel
        '
        Me.lblResponsavel.AutoSize = True
        Me.lblResponsavel.BackColor = System.Drawing.Color.Transparent
        Me.lblResponsavel.Location = New System.Drawing.Point(6, 93)
        Me.lblResponsavel.Name = "lblResponsavel"
        Me.lblResponsavel.TabIndex = 17
        Me.lblResponsavel.Text = "Responsáveis:"
        '
        'cboResponsavel
        '
        Me.cboResponsavel.ButtonCancelText = "Cancelar"
        cboResponsavel_DesignTimeLayout.LayoutString = resources.GetString("cboResponsavel_DesignTimeLayout.LayoutString")
        Me.cboResponsavel.DesignTimeLayout = cboResponsavel_DesignTimeLayout
        Me.cboResponsavel.Location = New System.Drawing.Point(9, 109)
        Me.cboResponsavel.Name = "cboResponsavel"
        Me.cboResponsavel.SaveSettings = False
        Me.cboResponsavel.Size = New System.Drawing.Size(400, 20)
        Me.cboResponsavel.TabIndex = 18
        Me.cboResponsavel.ValuesDataMember = Nothing
        '
        'lblTipoAtendimento
        '
        Me.lblTipoAtendimento.AutoSize = True
        Me.lblTipoAtendimento.BackColor = System.Drawing.Color.Transparent
        Me.lblTipoAtendimento.Location = New System.Drawing.Point(412, 93)
        Me.lblTipoAtendimento.Name = "lblTipoAtendimento"
        Me.lblTipoAtendimento.TabIndex = 19
        Me.lblTipoAtendimento.Text = "Tipos de Atendimento:"
        '
        'cboTipoAtendimento
        '
        Me.cboTipoAtendimento.ButtonCancelText = "Cancelar"
        cboTipoAtendimento_DesignTimeLayout.LayoutString = resources.GetString("cboTipoAtendimento_DesignTimeLayout.LayoutString")
        Me.cboTipoAtendimento.DesignTimeLayout = cboTipoAtendimento_DesignTimeLayout
        Me.cboTipoAtendimento.Location = New System.Drawing.Point(415, 109)
        Me.cboTipoAtendimento.Name = "cboTipoAtendimento"
        Me.cboTipoAtendimento.SaveSettings = False
        Me.cboTipoAtendimento.Size = New System.Drawing.Size(250, 20)
        Me.cboTipoAtendimento.TabIndex = 20
        Me.cboTipoAtendimento.ValuesDataMember = Nothing
        '
        'lblModulo
        '
        Me.lblModulo.AutoSize = True
        Me.lblModulo.BackColor = System.Drawing.Color.Transparent
        Me.lblModulo.Location = New System.Drawing.Point(668, 93)
        Me.lblModulo.Name = "lblModulo"
        Me.lblModulo.TabIndex = 21
        Me.lblModulo.Text = "Módulo:"
        '
        'txtModulo
        '
        Me.txtModulo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtModulo.Location = New System.Drawing.Point(671, 109)
        Me.txtModulo.Name = "txtModulo"
        Me.txtModulo.MaxLength = 200
        Me.txtModulo.Size = New System.Drawing.Size(148, 20)
        Me.txtModulo.TabIndex = 22
        '
        'lblTela
        '
        Me.lblTela.AutoSize = True
        Me.lblTela.BackColor = System.Drawing.Color.Transparent
        Me.lblTela.Location = New System.Drawing.Point(822, 93)
        Me.lblTela.Name = "lblTela"
        Me.lblTela.TabIndex = 23
        Me.lblTela.Text = "Tela:"
        '
        'txtTela
        '
        Me.txtTela.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTela.Location = New System.Drawing.Point(825, 109)
        Me.txtTela.Name = "txtTela"
        Me.txtTela.MaxLength = 200
        Me.txtTela.Size = New System.Drawing.Size(146, 20)
        Me.txtTela.TabIndex = 24
        '
        'lblNumeroDocumento
        '
        Me.lblNumeroDocumento.AutoSize = True
        Me.lblNumeroDocumento.BackColor = System.Drawing.Color.Transparent
        Me.lblNumeroDocumento.Location = New System.Drawing.Point(974, 93)
        Me.lblNumeroDocumento.Name = "lblNumeroDocumento"
        Me.lblNumeroDocumento.TabIndex = 25
        Me.lblNumeroDocumento.Text = "Nº Documento:"
        '
        'txtNumeroDocumento
        '
        Me.txtNumeroDocumento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNumeroDocumento.Location = New System.Drawing.Point(977, 109)
        Me.txtNumeroDocumento.Name = "txtNumeroDocumento"
        Me.txtNumeroDocumento.MaxLength = 200
        Me.txtNumeroDocumento.Size = New System.Drawing.Size(196, 20)
        Me.txtNumeroDocumento.TabIndex = 26
        '
        'lblDefinicao
        '
        Me.lblDefinicao.AutoSize = True
        Me.lblDefinicao.BackColor = System.Drawing.Color.Transparent
        Me.lblDefinicao.Location = New System.Drawing.Point(6, 133)
        Me.lblDefinicao.Name = "lblDefinicao"
        Me.lblDefinicao.TabIndex = 27
        Me.lblDefinicao.Text = "Descrição:"
        '
        'txtDescricao
        '
        Me.txtDescricao.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescricao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescricao.AcceptsReturn = True
        Me.txtDescricao.Location = New System.Drawing.Point(9, 149)
        Me.txtDescricao.Name = "txtDescricao"
        Me.txtDescricao.MaxLength = 0
        Me.txtDescricao.Multiline = True
        Me.txtDescricao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescricao.Size = New System.Drawing.Size(1164, 78)
        Me.txtDescricao.TabIndex = 28
        '
        'chkExibirUsuario
        '
        Me.chkExibirUsuario.BackColor = System.Drawing.Color.Transparent
        Me.chkExibirUsuario.Location = New System.Drawing.Point(977, 70)
        Me.chkExibirUsuario.Name = "chkExibirUsuario"
        Me.chkExibirUsuario.Size = New System.Drawing.Size(196, 18)
        Me.chkExibirUsuario.TabIndex = 16
        Me.chkExibirUsuario.Text = "Exibir para usuário"
        Me.chkExibirUsuario.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'tabDadosChamado
        '
        Me.tabDadosChamado.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabDadosChamado.BackColor = System.Drawing.Color.Transparent
        Me.tabDadosChamado.Location = New System.Drawing.Point(7, 247)
        Me.tabDadosChamado.Name = "tabDadosChamado"
        Me.tabDadosChamado.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.tabDadosChamado.Size = New System.Drawing.Size(1182, 233)
        Me.tabDadosChamado.TabIndex = 1
        Me.tabDadosChamado.TabPages.AddRange(New Janus.Windows.UI.Tab.UITabPage() {Me.pagArquivos, Me.pagComentario, Me.pagComentarioDev})
        Me.tabDadosChamado.TabStop = False
        Me.tabDadosChamado.VisualStyleManager = Me.vsmMain
        '
        'pagArquivos
        '
        Me.pagArquivos.Controls.Add(Me.grdArquivos)
        Me.pagArquivos.Controls.Add(Me.btnArquivo)
        Me.pagArquivos.Key = "pagArquivos"
        Me.pagArquivos.Location = New System.Drawing.Point(1, 22)
        Me.pagArquivos.Name = "pagArquivos"
        Me.pagArquivos.Size = New System.Drawing.Size(1180, 210)
        Me.pagArquivos.TabStop = True
        Me.pagArquivos.Text = "Arquivos"
        '
        'pagComentario
        '
        Me.pagComentario.Controls.Add(Me.grdComentario)
        Me.pagComentario.Key = "pagComentario"
        Me.pagComentario.Location = New System.Drawing.Point(1, 22)
        Me.pagComentario.Name = "pagComentario"
        Me.pagComentario.Size = New System.Drawing.Size(1180, 210)
        Me.pagComentario.TabStop = True
        Me.pagComentario.Text = "Comentários"
        '
        'pagComentarioDev
        '
        Me.pagComentarioDev.Controls.Add(Me.grdComentarioDev)
        Me.pagComentarioDev.Key = "pagComentarioDev"
        Me.pagComentarioDev.Location = New System.Drawing.Point(1, 22)
        Me.pagComentarioDev.Name = "pagComentarioDev"
        Me.pagComentarioDev.Size = New System.Drawing.Size(1180, 210)
        Me.pagComentarioDev.TabStop = True
        Me.pagComentarioDev.Text = "Comentários de DEV"
        '
        'btnArquivo
        '
        Me.btnArquivo.Image = Global.Documentacti.My.Resources.Resources.add
        Me.btnArquivo.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnArquivo.Location = New System.Drawing.Point(3, 3)
        Me.btnArquivo.Name = "btnArquivo"
        Me.btnArquivo.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnArquivo.Size = New System.Drawing.Size(130, 23)
        Me.btnArquivo.TabIndex = 0
        Me.btnArquivo.Text = "Anexar arquivo"
        Me.btnArquivo.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'grdArquivos
        '
        Me.grdArquivos.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdArquivos.AlternatingColors = True
        Me.grdArquivos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdArquivos_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("grdArquivos_DesignTimeLayout_Reference_0.Instance"), Object)
        grdArquivos_DesignTimeLayout_Reference_1.Instance = CType(resources.GetObject("grdArquivos_DesignTimeLayout_Reference_1.Instance"), Object)
        grdArquivos_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {grdArquivos_DesignTimeLayout_Reference_0, grdArquivos_DesignTimeLayout_Reference_1})
        grdArquivos_DesignTimeLayout.LayoutString = resources.GetString("grdArquivos_DesignTimeLayout.LayoutString")
        Me.grdArquivos.DesignTimeLayout = grdArquivos_DesignTimeLayout
        Me.grdArquivos.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdArquivos.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdArquivos.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdArquivos.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdArquivos.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdArquivos.GroupByBoxVisible = False
        Me.grdArquivos.Location = New System.Drawing.Point(3, 30)
        Me.grdArquivos.Name = "grdArquivos"
        Me.grdArquivos.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdArquivos.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdArquivos.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdArquivos.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdArquivos.Size = New System.Drawing.Size(1174, 175)
        Me.grdArquivos.TabIndex = 1
        Me.grdArquivos.TabStop = False
        Me.grdArquivos.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdArquivos.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grdComentario
        '
        Me.grdComentario.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdComentario.AlternatingColors = True
        Me.grdComentario.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdComentario_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("grdComentario_DesignTimeLayout_Reference_0.Instance"), Object)
        grdComentario_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {grdComentario_DesignTimeLayout_Reference_0})
        grdComentario_DesignTimeLayout.LayoutString = resources.GetString("grdComentario_DesignTimeLayout.LayoutString")
        Me.grdComentario.DesignTimeLayout = grdComentario_DesignTimeLayout
        Me.grdComentario.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdComentario.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdComentario.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdComentario.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdComentario.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdComentario.GroupByBoxVisible = False
        Me.grdComentario.Location = New System.Drawing.Point(3, 3)
        Me.grdComentario.Name = "grdComentario"
        Me.grdComentario.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdComentario.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdComentario.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdComentario.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdComentario.Size = New System.Drawing.Size(1174, 202)
        Me.grdComentario.TabIndex = 0
        Me.grdComentario.TabStop = False
        Me.grdComentario.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdComentario.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grdComentarioDev
        '
        Me.grdComentarioDev.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdComentarioDev.AlternatingColors = True
        Me.grdComentarioDev.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdComentarioDev_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("grdComentarioDev_DesignTimeLayout_Reference_0.Instance"), Object)
        grdComentarioDev_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {grdComentarioDev_DesignTimeLayout_Reference_0})
        grdComentarioDev_DesignTimeLayout.LayoutString = resources.GetString("grdComentarioDev_DesignTimeLayout.LayoutString")
        Me.grdComentarioDev.DesignTimeLayout = grdComentarioDev_DesignTimeLayout
        Me.grdComentarioDev.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdComentarioDev.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdComentarioDev.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdComentarioDev.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdComentarioDev.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdComentarioDev.GroupByBoxVisible = False
        Me.grdComentarioDev.Location = New System.Drawing.Point(3, 3)
        Me.grdComentarioDev.Name = "grdComentarioDev"
        Me.grdComentarioDev.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdComentarioDev.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdComentarioDev.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdComentarioDev.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdComentarioDev.Size = New System.Drawing.Size(1174, 202)
        Me.grdComentarioDev.TabIndex = 0
        Me.grdComentarioDev.TabStop = False
        Me.grdComentarioDev.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdComentarioDev.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grpControl2
        '
        Me.grpControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpControl2.BackColor = System.Drawing.Color.Transparent
        Me.grpControl2.Controls.Add(Me.btnSair2)
        Me.grpControl2.Controls.Add(Me.btnVoltar)
        Me.grpControl2.Controls.Add(Me.btnSalvar)
        Me.grpControl2.Controls.Add(Me.btnNovo)
        Me.grpControl2.Controls.Add(Me.btnInserirComentarioDev)
        Me.grpControl2.Controls.Add(Me.btnInserirComentario)
        Me.grpControl2.Controls.Add(Me.btnFinalizarChamado)
        Me.grpControl2.Controls.Add(Me.btnIniciar)
        Me.grpControl2.Location = New System.Drawing.Point(7, 486)
        Me.grpControl2.Name = "grpControl2"
        Me.grpControl2.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControl2.Size = New System.Drawing.Size(1182, 51)
        Me.grpControl2.TabIndex = 2
        Me.grpControl2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnIniciar
        '
        Me.btnIniciar.Image = Global.Documentacti.My.Resources.Resources.run
        Me.btnIniciar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnIniciar.Location = New System.Drawing.Point(9, 17)
        Me.btnIniciar.Name = "btnIniciar"
        Me.btnIniciar.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnIniciar.Size = New System.Drawing.Size(91, 23)
        Me.btnIniciar.TabIndex = 1
        Me.btnIniciar.Text = "Iniciar"
        Me.btnIniciar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnFinalizarChamado
        '
        Me.btnFinalizarChamado.Image = Global.Documentacti.My.Resources.Resources.aprovar
        Me.btnFinalizarChamado.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnFinalizarChamado.Location = New System.Drawing.Point(106, 17)
        Me.btnFinalizarChamado.Name = "btnFinalizarChamado"
        Me.btnFinalizarChamado.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnFinalizarChamado.Size = New System.Drawing.Size(91, 23)
        Me.btnFinalizarChamado.TabIndex = 2
        Me.btnFinalizarChamado.Text = "Concluir"
        Me.btnFinalizarChamado.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnInserirComentario
        '
        Me.btnInserirComentario.Image = CType(resources.GetObject("btnInserirComentario.Image"), System.Drawing.Image)
        Me.btnInserirComentario.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnInserirComentario.Location = New System.Drawing.Point(203, 17)
        Me.btnInserirComentario.Name = "btnInserirComentario"
        Me.btnInserirComentario.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnInserirComentario.Size = New System.Drawing.Size(120, 23)
        Me.btnInserirComentario.TabIndex = 3
        Me.btnInserirComentario.Text = "Comentário"
        Me.btnInserirComentario.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnInserirComentarioDev
        '
        Me.btnInserirComentarioDev.Image = CType(resources.GetObject("btnInserirComentarioDev.Image"), System.Drawing.Image)
        Me.btnInserirComentarioDev.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnInserirComentarioDev.Location = New System.Drawing.Point(329, 17)
        Me.btnInserirComentarioDev.Name = "btnInserirComentarioDev"
        Me.btnInserirComentarioDev.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnInserirComentarioDev.Size = New System.Drawing.Size(140, 23)
        Me.btnInserirComentarioDev.TabIndex = 4
        Me.btnInserirComentarioDev.Text = "Comentário DEV"
        Me.btnInserirComentarioDev.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnNovo
        '
        Me.btnNovo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNovo.Image = Global.Documentacti.My.Resources.Resources.Novo
        Me.btnNovo.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnNovo.ImageSize = New System.Drawing.Size(13, 16)
        Me.btnNovo.Location = New System.Drawing.Point(790, 17)
        Me.btnNovo.Name = "btnNovo"
        Me.btnNovo.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnNovo.Size = New System.Drawing.Size(91, 23)
        Me.btnNovo.TabIndex = 5
        Me.btnNovo.Text = "Novo"
        Me.btnNovo.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
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
        Me.btnSalvar.TabIndex = 6
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
        Me.btnVoltar.TabIndex = 7
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
        Me.btnSair2.TabIndex = 8
        Me.btnSair2.Text = "Sair"
        Me.btnSair2.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'grpApontamento
        '
        Me.grpApontamento.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpApontamento.BackColor = System.Drawing.Color.Transparent
        Me.grpApontamento.Controls.Add(Me.btnEncerrarApontamento)
        Me.grpApontamento.Controls.Add(Me.btnIniciarApontamento)
        Me.grpApontamento.Controls.Add(Me.cboChamadoApontamento)
        Me.grpApontamento.Controls.Add(Me.lblChamadoApontamento)
        Me.grpApontamento.Controls.Add(Me.cboEmpresaApontamento)
        Me.grpApontamento.Controls.Add(Me.lblEmpresaApontamento)
        Me.grpApontamento.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpApontamento.Location = New System.Drawing.Point(7, 6)
        Me.grpApontamento.Name = "grpApontamento"
        Me.grpApontamento.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpApontamento.Size = New System.Drawing.Size(1182, 62)
        Me.grpApontamento.TabIndex = 0
        Me.grpApontamento.Text = "Novo Apontamento"
        Me.grpApontamento.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblEmpresaApontamento
        '
        Me.lblEmpresaApontamento.AutoSize = True
        Me.lblEmpresaApontamento.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpresaApontamento.Location = New System.Drawing.Point(6, 15)
        Me.lblEmpresaApontamento.Name = "lblEmpresaApontamento"
        Me.lblEmpresaApontamento.TabIndex = 0
        Me.lblEmpresaApontamento.Text = "Empresa:"
        '
        'cboEmpresaApontamento
        '
        Me.cboEmpresaApontamento.AutoSize = False
        Me.cboEmpresaApontamento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboEmpresaApontamento.Location = New System.Drawing.Point(9, 32)
        Me.cboEmpresaApontamento.Name = "cboEmpresaApontamento"
        Me.cboEmpresaApontamento.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboEmpresaApontamento.Size = New System.Drawing.Size(250, 20)
        Me.cboEmpresaApontamento.TabIndex = 1
        '
        'lblChamadoApontamento
        '
        Me.lblChamadoApontamento.AutoSize = True
        Me.lblChamadoApontamento.BackColor = System.Drawing.Color.Transparent
        Me.lblChamadoApontamento.Location = New System.Drawing.Point(262, 15)
        Me.lblChamadoApontamento.Name = "lblChamadoApontamento"
        Me.lblChamadoApontamento.TabIndex = 2
        Me.lblChamadoApontamento.Text = "Chamado:"
        '
        'cboChamadoApontamento
        '
        Me.cboChamadoApontamento.AutoSize = False
        Me.cboChamadoApontamento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboChamadoApontamento.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboChamadoApontamento.Location = New System.Drawing.Point(265, 32)
        Me.cboChamadoApontamento.Name = "cboChamadoApontamento"
        Me.cboChamadoApontamento.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboChamadoApontamento.Size = New System.Drawing.Size(560, 20)
        Me.cboChamadoApontamento.TabIndex = 3
        '
        'btnIniciarApontamento
        '
        Me.btnIniciarApontamento.Image = Global.Documentacti.My.Resources.Resources.run
        Me.btnIniciarApontamento.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnIniciarApontamento.Location = New System.Drawing.Point(831, 30)
        Me.btnIniciarApontamento.Name = "btnIniciarApontamento"
        Me.btnIniciarApontamento.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnIniciarApontamento.Size = New System.Drawing.Size(165, 23)
        Me.btnIniciarApontamento.TabIndex = 4
        Me.btnIniciarApontamento.Text = "Iniciar Apontamento"
        Me.btnIniciarApontamento.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnEncerrarApontamento
        '
        Me.btnEncerrarApontamento.Image = Global.Documentacti.My.Resources.Resources.cancelar
        Me.btnEncerrarApontamento.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnEncerrarApontamento.Location = New System.Drawing.Point(1002, 30)
        Me.btnEncerrarApontamento.Name = "btnEncerrarApontamento"
        Me.btnEncerrarApontamento.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnEncerrarApontamento.Size = New System.Drawing.Size(171, 23)
        Me.btnEncerrarApontamento.TabIndex = 5
        Me.btnEncerrarApontamento.Text = "Encerrar Apontamento"
        Me.btnEncerrarApontamento.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'grpFiltroApontamento
        '
        Me.grpFiltroApontamento.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFiltroApontamento.BackColor = System.Drawing.Color.Transparent
        Me.grpFiltroApontamento.Controls.Add(Me.btnFiltrarApontamento)
        Me.grpFiltroApontamento.Controls.Add(Me.lblTotalApontamento)
        Me.grpFiltroApontamento.Controls.Add(Me.cboResponsavelApontamento)
        Me.grpFiltroApontamento.Controls.Add(Me.lblResponsavelApontamento)
        Me.grpFiltroApontamento.Controls.Add(Me.dtpTerminoApontamento)
        Me.grpFiltroApontamento.Controls.Add(Me.dtpInicioApontamento)
        Me.grpFiltroApontamento.Controls.Add(Me.lblPeriodoApontamento)
        Me.grpFiltroApontamento.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpFiltroApontamento.Location = New System.Drawing.Point(7, 72)
        Me.grpFiltroApontamento.Name = "grpFiltroApontamento"
        Me.grpFiltroApontamento.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpFiltroApontamento.Size = New System.Drawing.Size(1182, 62)
        Me.grpFiltroApontamento.TabIndex = 1
        Me.grpFiltroApontamento.Text = "Histórico"
        Me.grpFiltroApontamento.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblPeriodoApontamento
        '
        Me.lblPeriodoApontamento.AutoSize = True
        Me.lblPeriodoApontamento.BackColor = System.Drawing.Color.Transparent
        Me.lblPeriodoApontamento.Location = New System.Drawing.Point(6, 15)
        Me.lblPeriodoApontamento.Name = "lblPeriodoApontamento"
        Me.lblPeriodoApontamento.TabIndex = 0
        Me.lblPeriodoApontamento.Text = "Período:"
        '
        'dtpInicioApontamento
        '
        '
        '
        '
        Me.dtpInicioApontamento.DropDownCalendar.Name = ""
        Me.dtpInicioApontamento.Location = New System.Drawing.Point(9, 32)
        Me.dtpInicioApontamento.Name = "dtpInicioApontamento"
        Me.dtpInicioApontamento.ShowCheckBox = True
        Me.dtpInicioApontamento.Size = New System.Drawing.Size(108, 20)
        Me.dtpInicioApontamento.TabIndex = 1
        Me.dtpInicioApontamento.Value = New Date(2026, 1, 1, 0, 0, 0, 0)
        '
        'dtpTerminoApontamento
        '
        '
        '
        '
        Me.dtpTerminoApontamento.DropDownCalendar.Name = ""
        Me.dtpTerminoApontamento.Location = New System.Drawing.Point(123, 32)
        Me.dtpTerminoApontamento.Name = "dtpTerminoApontamento"
        Me.dtpTerminoApontamento.ShowCheckBox = True
        Me.dtpTerminoApontamento.Size = New System.Drawing.Size(108, 20)
        Me.dtpTerminoApontamento.TabIndex = 2
        Me.dtpTerminoApontamento.Value = New Date(2026, 1, 1, 0, 0, 0, 0)
        '
        'lblResponsavelApontamento
        '
        Me.lblResponsavelApontamento.AutoSize = True
        Me.lblResponsavelApontamento.BackColor = System.Drawing.Color.Transparent
        Me.lblResponsavelApontamento.Location = New System.Drawing.Point(240, 15)
        Me.lblResponsavelApontamento.Name = "lblResponsavelApontamento"
        Me.lblResponsavelApontamento.TabIndex = 3
        Me.lblResponsavelApontamento.Text = "Responsável:"
        '
        'cboResponsavelApontamento
        '
        Me.cboResponsavelApontamento.AutoSize = False
        Me.cboResponsavelApontamento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboResponsavelApontamento.Location = New System.Drawing.Point(243, 32)
        Me.cboResponsavelApontamento.Name = "cboResponsavelApontamento"
        Me.cboResponsavelApontamento.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboResponsavelApontamento.Size = New System.Drawing.Size(250, 20)
        Me.cboResponsavelApontamento.TabIndex = 4
        '
        'lblTotalApontamento
        '
        Me.lblTotalApontamento.AutoSize = True
        Me.lblTotalApontamento.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalApontamento.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalApontamento.ForeColor = System.Drawing.Color.Blue
        Me.lblTotalApontamento.Location = New System.Drawing.Point(510, 35)
        Me.lblTotalApontamento.Name = "lblTotalApontamento"
        Me.lblTotalApontamento.TabIndex = 5
        Me.lblTotalApontamento.Text = "Total no período: 00:00:00"
        '
        'btnFiltrarApontamento
        '
        Me.btnFiltrarApontamento.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFiltrarApontamento.Image = Global.Documentacti.My.Resources.Resources.procurar
        Me.btnFiltrarApontamento.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnFiltrarApontamento.Location = New System.Drawing.Point(1082, 30)
        Me.btnFiltrarApontamento.Name = "btnFiltrarApontamento"
        Me.btnFiltrarApontamento.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnFiltrarApontamento.Size = New System.Drawing.Size(91, 23)
        Me.btnFiltrarApontamento.TabIndex = 6
        Me.btnFiltrarApontamento.Text = "Filtrar"
        Me.btnFiltrarApontamento.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'tlpApontamento
        '
        Me.tlpApontamento.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpApontamento.ColumnCount = 2
        Me.tlpApontamento.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.0!))
        Me.tlpApontamento.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.0!))
        Me.tlpApontamento.Controls.Add(Me.grpApontamentoHistorico, 0, 0)
        Me.tlpApontamento.Controls.Add(Me.grpApontamentoAndamento, 1, 0)
        Me.tlpApontamento.Location = New System.Drawing.Point(7, 140)
        Me.tlpApontamento.Name = "tlpApontamento"
        Me.tlpApontamento.RowCount = 1
        Me.tlpApontamento.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpApontamento.Size = New System.Drawing.Size(1182, 340)
        Me.tlpApontamento.TabIndex = 2
        '
        'grpApontamentoHistorico
        '
        Me.grpApontamentoHistorico.BackColor = System.Drawing.Color.Transparent
        Me.grpApontamentoHistorico.Controls.Add(Me.grdApontamentoHistorico)
        Me.grpApontamentoHistorico.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpApontamentoHistorico.Padding = New System.Windows.Forms.Padding(8, 22, 8, 8)
        Me.grpApontamentoHistorico.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpApontamentoHistorico.Name = "grpApontamentoHistorico"
        Me.grpApontamentoHistorico.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpApontamentoHistorico.TabIndex = 0
        Me.grpApontamentoHistorico.Text = "Histórico de Apontamentos (edite Início, Término e Observação na própria célula)"
        Me.grpApontamentoHistorico.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'grdApontamentoHistorico
        '
        Me.grdApontamentoHistorico.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdApontamentoHistorico.AlternatingColors = True
        Me.grdApontamentoHistorico.Dock = System.Windows.Forms.DockStyle.Fill
        grdApontamentoHistorico_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("grdApontamentoHistorico_DesignTimeLayout_Reference_0.Instance"), Object)
        grdApontamentoHistorico_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {grdApontamentoHistorico_DesignTimeLayout_Reference_0})
        grdApontamentoHistorico_DesignTimeLayout.LayoutString = resources.GetString("grdApontamentoHistorico_DesignTimeLayout.LayoutString")
        Me.grdApontamentoHistorico.DesignTimeLayout = grdApontamentoHistorico_DesignTimeLayout
        Me.grdApontamentoHistorico.DynamicFiltering = True
        Me.grdApontamentoHistorico.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdApontamentoHistorico.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdApontamentoHistorico.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdApontamentoHistorico.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid
        Me.grdApontamentoHistorico.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdApontamentoHistorico.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdApontamentoHistorico.GroupByBoxVisible = False
        Me.grdApontamentoHistorico.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdApontamentoHistorico.Location = New System.Drawing.Point(8, 22)
        Me.grdApontamentoHistorico.Name = "grdApontamentoHistorico"
        Me.grdApontamentoHistorico.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdApontamentoHistorico.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdApontamentoHistorico.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdApontamentoHistorico.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdApontamentoHistorico.Size = New System.Drawing.Size(680, 300)
        Me.grdApontamentoHistorico.TabIndex = 0
        Me.grdApontamentoHistorico.TabStop = True
        Me.grdApontamentoHistorico.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdApontamentoHistorico.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grpApontamentoAndamento
        '
        Me.grpApontamentoAndamento.BackColor = System.Drawing.Color.Transparent
        Me.grpApontamentoAndamento.Controls.Add(Me.grdApontamentoAndamento)
        Me.grpApontamentoAndamento.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpApontamentoAndamento.Padding = New System.Windows.Forms.Padding(8, 22, 8, 8)
        Me.grpApontamentoAndamento.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpApontamentoAndamento.Name = "grpApontamentoAndamento"
        Me.grpApontamentoAndamento.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpApontamentoAndamento.TabIndex = 0
        Me.grpApontamentoAndamento.Text = "Apontamentos em Andamento (contando horas)"
        Me.grpApontamentoAndamento.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'grdApontamentoAndamento
        '
        Me.grdApontamentoAndamento.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdApontamentoAndamento.AlternatingColors = True
        Me.grdApontamentoAndamento.Dock = System.Windows.Forms.DockStyle.Fill
        grdApontamentoAndamento_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("grdApontamentoAndamento_DesignTimeLayout_Reference_0.Instance"), Object)
        grdApontamentoAndamento_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {grdApontamentoAndamento_DesignTimeLayout_Reference_0})
        grdApontamentoAndamento_DesignTimeLayout.LayoutString = resources.GetString("grdApontamentoAndamento_DesignTimeLayout.LayoutString")
        Me.grdApontamentoAndamento.DesignTimeLayout = grdApontamentoAndamento_DesignTimeLayout
        Me.grdApontamentoAndamento.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdApontamentoAndamento.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdApontamentoAndamento.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdApontamentoAndamento.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdApontamentoAndamento.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdApontamentoAndamento.GroupByBoxVisible = False
        Me.grdApontamentoAndamento.Location = New System.Drawing.Point(8, 22)
        Me.grdApontamentoAndamento.Name = "grdApontamentoAndamento"
        Me.grdApontamentoAndamento.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdApontamentoAndamento.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdApontamentoAndamento.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdApontamentoAndamento.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdApontamentoAndamento.Size = New System.Drawing.Size(460, 300)
        Me.grdApontamentoAndamento.TabIndex = 0
        Me.grdApontamentoAndamento.TabStop = False
        Me.grdApontamentoAndamento.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdApontamentoAndamento.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grpControl3
        '
        Me.grpControl3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpControl3.BackColor = System.Drawing.Color.Transparent
        Me.grpControl3.Controls.Add(Me.btnSair3)
        Me.grpControl3.Controls.Add(Me.btnExcelApontamento)
        Me.grpControl3.Location = New System.Drawing.Point(7, 486)
        Me.grpControl3.Name = "grpControl3"
        Me.grpControl3.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControl3.Size = New System.Drawing.Size(1182, 51)
        Me.grpControl3.TabIndex = 3
        Me.grpControl3.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnExcelApontamento
        '
        Me.btnExcelApontamento.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcelApontamento.Image = Global.Documentacti.My.Resources.Resources.excel
        Me.btnExcelApontamento.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnExcelApontamento.Location = New System.Drawing.Point(984, 17)
        Me.btnExcelApontamento.Name = "btnExcelApontamento"
        Me.btnExcelApontamento.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnExcelApontamento.Size = New System.Drawing.Size(91, 23)
        Me.btnExcelApontamento.TabIndex = 1
        Me.btnExcelApontamento.Text = "Excel"
        Me.btnExcelApontamento.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSair3
        '
        Me.btnSair3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSair3.Image = CType(resources.GetObject("btnSair3.Image"), System.Drawing.Image)
        Me.btnSair3.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSair3.ImageSize = New System.Drawing.Size(19, 16)
        Me.btnSair3.Location = New System.Drawing.Point(1081, 17)
        Me.btnSair3.Name = "btnSair3"
        Me.btnSair3.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnSair3.Size = New System.Drawing.Size(91, 23)
        Me.btnSair3.TabIndex = 2
        Me.btnSair3.Text = "Sair"
        Me.btnSair3.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'grpFiltroRelatorio
        '
        Me.grpFiltroRelatorio.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFiltroRelatorio.BackColor = System.Drawing.Color.Transparent
        Me.grpFiltroRelatorio.Controls.Add(Me.btnGerarRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.lblTotalRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.cboGraficoRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.lblGraficoRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.cboAgruparRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.lblAgruparRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.cboResponsavelRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.lblResponsavelRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.cboTipoAtendimentoRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.lblTipoAtendimentoRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.cboTipoAtividadeRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.lblTipoAtividadeRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.cboEmpresaRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.lblEmpresaRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.dtpTerminoRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.dtpInicioRelatorio)
        Me.grpFiltroRelatorio.Controls.Add(Me.lblPeriodoRelatorio)
        Me.grpFiltroRelatorio.Location = New System.Drawing.Point(7, 6)
        Me.grpFiltroRelatorio.Name = "grpFiltroRelatorio"
        Me.grpFiltroRelatorio.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpFiltroRelatorio.Size = New System.Drawing.Size(1182, 104)
        Me.grpFiltroRelatorio.TabIndex = 0
        Me.grpFiltroRelatorio.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblPeriodoRelatorio
        '
        Me.lblPeriodoRelatorio.AutoSize = True
        Me.lblPeriodoRelatorio.BackColor = System.Drawing.Color.Transparent
        Me.lblPeriodoRelatorio.Location = New System.Drawing.Point(6, 17)
        Me.lblPeriodoRelatorio.Name = "lblPeriodoRelatorio"
        Me.lblPeriodoRelatorio.TabIndex = 0
        Me.lblPeriodoRelatorio.Text = "Período:"
        '
        'dtpInicioRelatorio
        '
        '
        '
        '
        Me.dtpInicioRelatorio.DropDownCalendar.Name = ""
        Me.dtpInicioRelatorio.Location = New System.Drawing.Point(9, 34)
        Me.dtpInicioRelatorio.Name = "dtpInicioRelatorio"
        Me.dtpInicioRelatorio.ShowCheckBox = True
        Me.dtpInicioRelatorio.Size = New System.Drawing.Size(108, 20)
        Me.dtpInicioRelatorio.TabIndex = 1
        Me.dtpInicioRelatorio.Value = New Date(2026, 1, 1, 0, 0, 0, 0)
        '
        'dtpTerminoRelatorio
        '
        '
        '
        '
        Me.dtpTerminoRelatorio.DropDownCalendar.Name = ""
        Me.dtpTerminoRelatorio.Location = New System.Drawing.Point(123, 34)
        Me.dtpTerminoRelatorio.Name = "dtpTerminoRelatorio"
        Me.dtpTerminoRelatorio.ShowCheckBox = True
        Me.dtpTerminoRelatorio.Size = New System.Drawing.Size(108, 20)
        Me.dtpTerminoRelatorio.TabIndex = 2
        Me.dtpTerminoRelatorio.Value = New Date(2026, 1, 1, 0, 0, 0, 0)
        '
        'lblEmpresaRelatorio
        '
        Me.lblEmpresaRelatorio.AutoSize = True
        Me.lblEmpresaRelatorio.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpresaRelatorio.Location = New System.Drawing.Point(234, 17)
        Me.lblEmpresaRelatorio.Name = "lblEmpresaRelatorio"
        Me.lblEmpresaRelatorio.TabIndex = 3
        Me.lblEmpresaRelatorio.Text = "Empresa:"
        '
        'cboEmpresaRelatorio
        '
        Me.cboEmpresaRelatorio.AutoSize = False
        Me.cboEmpresaRelatorio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboEmpresaRelatorio.Location = New System.Drawing.Point(237, 34)
        Me.cboEmpresaRelatorio.Name = "cboEmpresaRelatorio"
        Me.cboEmpresaRelatorio.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboEmpresaRelatorio.Size = New System.Drawing.Size(300, 20)
        Me.cboEmpresaRelatorio.TabIndex = 4
        '
        'lblTipoAtividadeRelatorio
        '
        Me.lblTipoAtividadeRelatorio.AutoSize = True
        Me.lblTipoAtividadeRelatorio.BackColor = System.Drawing.Color.Transparent
        Me.lblTipoAtividadeRelatorio.Location = New System.Drawing.Point(540, 17)
        Me.lblTipoAtividadeRelatorio.Name = "lblTipoAtividadeRelatorio"
        Me.lblTipoAtividadeRelatorio.TabIndex = 5
        Me.lblTipoAtividadeRelatorio.Text = "Tipo de Atividade:"
        '
        'cboTipoAtividadeRelatorio
        '
        Me.cboTipoAtividadeRelatorio.AutoSize = False
        Me.cboTipoAtividadeRelatorio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboTipoAtividadeRelatorio.Location = New System.Drawing.Point(543, 34)
        Me.cboTipoAtividadeRelatorio.Name = "cboTipoAtividadeRelatorio"
        Me.cboTipoAtividadeRelatorio.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboTipoAtividadeRelatorio.Size = New System.Drawing.Size(200, 20)
        Me.cboTipoAtividadeRelatorio.TabIndex = 6
        '
        'lblTipoAtendimentoRelatorio
        '
        Me.lblTipoAtendimentoRelatorio.AutoSize = True
        Me.lblTipoAtendimentoRelatorio.BackColor = System.Drawing.Color.Transparent
        Me.lblTipoAtendimentoRelatorio.Location = New System.Drawing.Point(746, 17)
        Me.lblTipoAtendimentoRelatorio.Name = "lblTipoAtendimentoRelatorio"
        Me.lblTipoAtendimentoRelatorio.TabIndex = 7
        Me.lblTipoAtendimentoRelatorio.Text = "Tipo de Atendimento:"
        '
        'cboTipoAtendimentoRelatorio
        '
        Me.cboTipoAtendimentoRelatorio.AutoSize = False
        Me.cboTipoAtendimentoRelatorio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboTipoAtendimentoRelatorio.Location = New System.Drawing.Point(749, 34)
        Me.cboTipoAtendimentoRelatorio.Name = "cboTipoAtendimentoRelatorio"
        Me.cboTipoAtendimentoRelatorio.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboTipoAtendimentoRelatorio.Size = New System.Drawing.Size(220, 20)
        Me.cboTipoAtendimentoRelatorio.TabIndex = 8
        '
        'lblResponsavelRelatorio
        '
        Me.lblResponsavelRelatorio.AutoSize = True
        Me.lblResponsavelRelatorio.BackColor = System.Drawing.Color.Transparent
        Me.lblResponsavelRelatorio.Location = New System.Drawing.Point(972, 17)
        Me.lblResponsavelRelatorio.Name = "lblResponsavelRelatorio"
        Me.lblResponsavelRelatorio.TabIndex = 9
        Me.lblResponsavelRelatorio.Text = "Responsável:"
        '
        'cboResponsavelRelatorio
        '
        Me.cboResponsavelRelatorio.AutoSize = False
        Me.cboResponsavelRelatorio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboResponsavelRelatorio.Location = New System.Drawing.Point(975, 34)
        Me.cboResponsavelRelatorio.Name = "cboResponsavelRelatorio"
        Me.cboResponsavelRelatorio.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboResponsavelRelatorio.Size = New System.Drawing.Size(198, 20)
        Me.cboResponsavelRelatorio.TabIndex = 10
        '
        'lblAgruparRelatorio
        '
        Me.lblAgruparRelatorio.AutoSize = True
        Me.lblAgruparRelatorio.BackColor = System.Drawing.Color.Transparent
        Me.lblAgruparRelatorio.Location = New System.Drawing.Point(6, 57)
        Me.lblAgruparRelatorio.Name = "lblAgruparRelatorio"
        Me.lblAgruparRelatorio.TabIndex = 11
        Me.lblAgruparRelatorio.Text = "Agrupar por:"
        '
        'cboAgruparRelatorio
        '
        Me.cboAgruparRelatorio.AutoSize = False
        Me.cboAgruparRelatorio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboAgruparRelatorio.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboAgruparRelatorio.Location = New System.Drawing.Point(9, 74)
        Me.cboAgruparRelatorio.Name = "cboAgruparRelatorio"
        Me.cboAgruparRelatorio.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboAgruparRelatorio.Size = New System.Drawing.Size(222, 20)
        Me.cboAgruparRelatorio.TabIndex = 12
        '
        'lblGraficoRelatorio
        '
        Me.lblGraficoRelatorio.AutoSize = True
        Me.lblGraficoRelatorio.BackColor = System.Drawing.Color.Transparent
        Me.lblGraficoRelatorio.Location = New System.Drawing.Point(234, 57)
        Me.lblGraficoRelatorio.Name = "lblGraficoRelatorio"
        Me.lblGraficoRelatorio.TabIndex = 13
        Me.lblGraficoRelatorio.Text = "Gráfico:"
        '
        'cboGraficoRelatorio
        '
        Me.cboGraficoRelatorio.AutoSize = False
        Me.cboGraficoRelatorio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboGraficoRelatorio.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList
        Me.cboGraficoRelatorio.Location = New System.Drawing.Point(237, 74)
        Me.cboGraficoRelatorio.Name = "cboGraficoRelatorio"
        Me.cboGraficoRelatorio.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboGraficoRelatorio.Size = New System.Drawing.Size(300, 20)
        Me.cboGraficoRelatorio.TabIndex = 14
        '
        'lblTotalRelatorio
        '
        Me.lblTotalRelatorio.AutoSize = True
        Me.lblTotalRelatorio.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalRelatorio.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalRelatorio.ForeColor = System.Drawing.Color.Blue
        Me.lblTotalRelatorio.Location = New System.Drawing.Point(543, 77)
        Me.lblTotalRelatorio.Name = "lblTotalRelatorio"
        Me.lblTotalRelatorio.TabIndex = 15
        Me.lblTotalRelatorio.Text = "Total: 00:00:00"
        '
        'btnGerarRelatorio
        '
        Me.btnGerarRelatorio.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGerarRelatorio.Image = Global.Documentacti.My.Resources.Resources.procurar
        Me.btnGerarRelatorio.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnGerarRelatorio.Location = New System.Drawing.Point(1082, 71)
        Me.btnGerarRelatorio.Name = "btnGerarRelatorio"
        Me.btnGerarRelatorio.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnGerarRelatorio.Size = New System.Drawing.Size(91, 23)
        Me.btnGerarRelatorio.TabIndex = 16
        Me.btnGerarRelatorio.Text = "Gerar"
        Me.btnGerarRelatorio.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'tlpRelatorio
        '
        Me.tlpRelatorio.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpRelatorio.ColumnCount = 2
        Me.tlpRelatorio.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.0!))
        Me.tlpRelatorio.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.0!))
        Me.tlpRelatorio.Controls.Add(Me.grpGraficoRelatorio, 0, 0)
        Me.tlpRelatorio.Controls.Add(Me.grpResumoRelatorio, 1, 0)
        Me.tlpRelatorio.Location = New System.Drawing.Point(7, 116)
        Me.tlpRelatorio.Name = "tlpRelatorio"
        Me.tlpRelatorio.RowCount = 1
        Me.tlpRelatorio.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpRelatorio.Size = New System.Drawing.Size(1182, 364)
        Me.tlpRelatorio.TabIndex = 1
        '
        'grpGraficoRelatorio
        '
        Me.grpGraficoRelatorio.BackColor = System.Drawing.Color.Transparent
        Me.grpGraficoRelatorio.Controls.Add(Me.chrRelatorio)
        Me.grpGraficoRelatorio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpGraficoRelatorio.Padding = New System.Windows.Forms.Padding(8, 22, 8, 8)
        Me.grpGraficoRelatorio.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpGraficoRelatorio.Name = "grpGraficoRelatorio"
        Me.grpGraficoRelatorio.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpGraficoRelatorio.TabIndex = 0
        Me.grpGraficoRelatorio.Text = "Gráfico"
        Me.grpGraficoRelatorio.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'chrRelatorio
        '
        Me.chrRelatorio.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chrRelatorio.BackGradientEndColor = System.Drawing.Color.White
        Me.chrRelatorio.BackGradientType = Dundas.Charting.WinControl.GradientType.DiagonalLeft
        Me.chrRelatorio.BorderLineColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.chrRelatorio.BorderLineStyle = Dundas.Charting.WinControl.ChartDashStyle.Solid
        ChartArea1.BackColor = System.Drawing.Color.White
        ChartArea1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(105, Byte), Integer))
        ChartArea1.BorderStyle = Dundas.Charting.WinControl.ChartDashStyle.Solid
        ChartArea1.Name = "Default"
        ChartArea1.ShadowOffset = 2
        Me.chrRelatorio.ChartAreas.Add(ChartArea1)
        Me.chrRelatorio.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Alignment = System.Drawing.StringAlignment.Center
        Legend1.AutoFitText = False
        Legend1.BorderColor = System.Drawing.Color.Black
        Legend1.Docking = Dundas.Charting.WinControl.LegendDocking.Bottom
        Legend1.Font = New System.Drawing.Font("Arial", 6.75!)
        Legend1.LegendStyle = Dundas.Charting.WinControl.LegendStyle.Row
        Legend1.Name = "Default"
        Me.chrRelatorio.Legends.Add(Legend1)
        Me.chrRelatorio.Location = New System.Drawing.Point(8, 22)
        Me.chrRelatorio.Name = "chrRelatorio"
        Me.chrRelatorio.Palette = Dundas.Charting.WinControl.ChartColorPalette.Dundas
        Me.chrRelatorio.Size = New System.Drawing.Size(640, 330)
        Me.chrRelatorio.TabIndex = 0
        Me.chrRelatorio.UI.Toolbar.BorderColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.chrRelatorio.UI.Toolbar.BorderSkin.PageColor = System.Drawing.Color.Transparent
        Me.chrRelatorio.UI.Toolbar.Enabled = True
        CommandUIItem1.CommandName = "SaveImage"
        CommandUIItem2.CommandName = "Copy"
        CommandUIItem3.CommandName = "Print"
        CommandUIItem4.CommandName = "PrintPreview"
        Me.chrRelatorio.UI.Toolbar.Items.Add(CommandUIItem1)
        Me.chrRelatorio.UI.Toolbar.Items.Add(CommandUIItem2)
        Me.chrRelatorio.UI.Toolbar.Items.Add(CommandUIItem3)
        Me.chrRelatorio.UI.Toolbar.Items.Add(CommandUIItem4)
        '
        'grpResumoRelatorio
        '
        Me.grpResumoRelatorio.BackColor = System.Drawing.Color.Transparent
        Me.grpResumoRelatorio.Controls.Add(Me.grdRelatorio)
        Me.grpResumoRelatorio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpResumoRelatorio.Padding = New System.Windows.Forms.Padding(8, 22, 8, 8)
        Me.grpResumoRelatorio.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpResumoRelatorio.Name = "grpResumoRelatorio"
        Me.grpResumoRelatorio.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpResumoRelatorio.TabIndex = 0
        Me.grpResumoRelatorio.Text = "Horas apontadas (apontamentos encerrados)"
        Me.grpResumoRelatorio.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'grdRelatorio
        '
        Me.grdRelatorio.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdRelatorio.AlternatingColors = True
        Me.grdRelatorio.Dock = System.Windows.Forms.DockStyle.Fill
        grdRelatorio_DesignTimeLayout.LayoutString = resources.GetString("grdRelatorio_DesignTimeLayout.LayoutString")
        Me.grdRelatorio.DesignTimeLayout = grdRelatorio_DesignTimeLayout
        Me.grdRelatorio.DynamicFiltering = True
        Me.grdRelatorio.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdRelatorio.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdRelatorio.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdRelatorio.FocusStyle = Janus.Windows.GridEX.FocusStyle.None
        Me.grdRelatorio.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdRelatorio.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdRelatorio.GroupByBoxVisible = False
        Me.grdRelatorio.Location = New System.Drawing.Point(8, 22)
        Me.grdRelatorio.Name = "grdRelatorio"
        Me.grdRelatorio.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Silver
        Me.grdRelatorio.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdRelatorio.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.grdRelatorio.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty
        Me.grdRelatorio.Size = New System.Drawing.Size(460, 330)
        Me.grdRelatorio.TabIndex = 0
        Me.grdRelatorio.TabStop = False
        Me.grdRelatorio.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdRelatorio.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grpControl4
        '
        Me.grpControl4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpControl4.BackColor = System.Drawing.Color.Transparent
        Me.grpControl4.Controls.Add(Me.btnSair4)
        Me.grpControl4.Controls.Add(Me.btnExcelRelatorio)
        Me.grpControl4.Location = New System.Drawing.Point(7, 486)
        Me.grpControl4.Name = "grpControl4"
        Me.grpControl4.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpControl4.Size = New System.Drawing.Size(1182, 51)
        Me.grpControl4.TabIndex = 2
        Me.grpControl4.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnExcelRelatorio
        '
        Me.btnExcelRelatorio.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcelRelatorio.Image = Global.Documentacti.My.Resources.Resources.excel
        Me.btnExcelRelatorio.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnExcelRelatorio.Location = New System.Drawing.Point(984, 17)
        Me.btnExcelRelatorio.Name = "btnExcelRelatorio"
        Me.btnExcelRelatorio.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnExcelRelatorio.Size = New System.Drawing.Size(91, 23)
        Me.btnExcelRelatorio.TabIndex = 1
        Me.btnExcelRelatorio.Text = "Excel"
        Me.btnExcelRelatorio.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnSair4
        '
        Me.btnSair4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSair4.Image = CType(resources.GetObject("btnSair4.Image"), System.Drawing.Image)
        Me.btnSair4.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnSair4.ImageSize = New System.Drawing.Size(19, 16)
        Me.btnSair4.Location = New System.Drawing.Point(1081, 17)
        Me.btnSair4.Name = "btnSair4"
        Me.btnSair4.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnSair4.Size = New System.Drawing.Size(91, 23)
        Me.btnSair4.TabIndex = 2
        Me.btnSair4.Text = "Sair"
        Me.btnSair4.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
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
        'tmrApontamento
        '
        Me.tmrApontamento.Interval = 1000
        '
        'usrAdmChamado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.tabMain)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "usrAdmChamado"
        Me.Size = New System.Drawing.Size(1200, 565)
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMain.ResumeLayout(False)
        Me.pagLista.ResumeLayout(False)
        Me.pagLista.PerformLayout()
        Me.pagDados.ResumeLayout(False)
        Me.pagDados.PerformLayout()
        Me.pagApontamento.ResumeLayout(False)
        Me.pagApontamento.PerformLayout()
        Me.pagRelatorio.ResumeLayout(False)
        Me.pagRelatorio.PerformLayout()
        CType(Me.grpFiltro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFiltro.ResumeLayout(False)
        Me.grpFiltro.PerformLayout()
        CType(Me.grdListagem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpLegenda, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLegenda.ResumeLayout(False)
        Me.grpLegenda.PerformLayout()
        CType(Me.picLegendaAberto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLegendaIniciado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLegendaConcluido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLegendaCancelado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLegendaAgCliente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpControl.ResumeLayout(False)
        Me.grpControl.PerformLayout()
        CType(Me.grpDados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDados.ResumeLayout(False)
        Me.grpDados.PerformLayout()
        CType(Me.tabDadosChamado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabDadosChamado.ResumeLayout(False)
        Me.pagArquivos.ResumeLayout(False)
        Me.pagArquivos.PerformLayout()
        Me.pagComentario.ResumeLayout(False)
        Me.pagComentario.PerformLayout()
        Me.pagComentarioDev.ResumeLayout(False)
        Me.pagComentarioDev.PerformLayout()
        CType(Me.grdArquivos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdComentario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdComentarioDev, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpControl2.ResumeLayout(False)
        Me.grpControl2.PerformLayout()
        CType(Me.grpApontamento, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpApontamento.ResumeLayout(False)
        Me.grpApontamento.PerformLayout()
        CType(Me.grpFiltroApontamento, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFiltroApontamento.ResumeLayout(False)
        Me.grpFiltroApontamento.PerformLayout()
        Me.tlpApontamento.ResumeLayout(False)
        Me.tlpApontamento.PerformLayout()
        CType(Me.grpApontamentoHistorico, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpApontamentoHistorico.ResumeLayout(False)
        Me.grpApontamentoHistorico.PerformLayout()
        CType(Me.grdApontamentoHistorico, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpApontamentoAndamento, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpApontamentoAndamento.ResumeLayout(False)
        Me.grpApontamentoAndamento.PerformLayout()
        CType(Me.grdApontamentoAndamento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpControl3.ResumeLayout(False)
        Me.grpControl3.PerformLayout()
        CType(Me.grpFiltroRelatorio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFiltroRelatorio.ResumeLayout(False)
        Me.grpFiltroRelatorio.PerformLayout()
        Me.tlpRelatorio.ResumeLayout(False)
        Me.tlpRelatorio.PerformLayout()
        CType(Me.grpGraficoRelatorio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGraficoRelatorio.ResumeLayout(False)
        Me.grpGraficoRelatorio.PerformLayout()
        CType(Me.chrRelatorio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpResumoRelatorio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpResumoRelatorio.ResumeLayout(False)
        Me.grpResumoRelatorio.PerformLayout()
        CType(Me.grdRelatorio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpControl4.ResumeLayout(False)
        Me.grpControl4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabMain As Janus.Windows.UI.Tab.UITab
    Friend WithEvents pagLista As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents pagDados As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents pagApontamento As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents pagRelatorio As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents grpFiltro As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblDataFiltro As System.Windows.Forms.Label
    Friend WithEvents dtpDataInicioFiltro As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtpDataTerminoFiltro As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents lblCodigoFiltro As System.Windows.Forms.Label
    Friend WithEvents txtCodigoFiltro As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblEmpresaFiltro As System.Windows.Forms.Label
    Friend WithEvents cboEmpresaFiltro As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblStatusFiltro As System.Windows.Forms.Label
    Friend WithEvents cboStatusFiltro As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents lblTituloFiltro As System.Windows.Forms.Label
    Friend WithEvents txtTituloFiltro As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblTipoAtendimentoFiltro As System.Windows.Forms.Label
    Friend WithEvents cboTipoAtendimentoFiltro As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblResponsavelFiltro As System.Windows.Forms.Label
    Friend WithEvents cboResponsavelFiltro As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents btnFiltrar As Janus.Windows.EditControls.UIButton
    Friend WithEvents grdListagem As Janus.Windows.GridEX.GridEX
    Friend WithEvents grpLegenda As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents picLegendaAberto As System.Windows.Forms.PictureBox
    Friend WithEvents lblAberto As System.Windows.Forms.Label
    Friend WithEvents picLegendaIniciado As System.Windows.Forms.PictureBox
    Friend WithEvents lblIniciado As System.Windows.Forms.Label
    Friend WithEvents picLegendaConcluido As System.Windows.Forms.PictureBox
    Friend WithEvents lblConcluido As System.Windows.Forms.Label
    Friend WithEvents picLegendaCancelado As System.Windows.Forms.PictureBox
    Friend WithEvents lblCancelado As System.Windows.Forms.Label
    Friend WithEvents picLegendaAgCliente As System.Windows.Forms.PictureBox
    Friend WithEvents lblAgCliente As System.Windows.Forms.Label
    Friend WithEvents grpControl As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnExcel As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnNovo1 As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair As Janus.Windows.EditControls.UIButton
    Friend WithEvents grpDados As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents txtCodigo As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblDataChamado As System.Windows.Forms.Label
    Friend WithEvents dtpDataChamado As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtStatus As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblSolicitante As System.Windows.Forms.Label
    Friend WithEvents txtSolicitante As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblEmpresa As System.Windows.Forms.Label
    Friend WithEvents cboEmpresa As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblPrioridade As System.Windows.Forms.Label
    Friend WithEvents cboPrioridade As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents txtTitulo As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblTipoChamado As System.Windows.Forms.Label
    Friend WithEvents cboTipoAtividade As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblResponsavel As System.Windows.Forms.Label
    Friend WithEvents cboResponsavel As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents lblTipoAtendimento As System.Windows.Forms.Label
    Friend WithEvents cboTipoAtendimento As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents lblModulo As System.Windows.Forms.Label
    Friend WithEvents txtModulo As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblTela As System.Windows.Forms.Label
    Friend WithEvents txtTela As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblNumeroDocumento As System.Windows.Forms.Label
    Friend WithEvents txtNumeroDocumento As Janus.Windows.GridEX.EditControls.MaskedEditBox
    Friend WithEvents lblDefinicao As System.Windows.Forms.Label
    Friend WithEvents txtDescricao As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents chkExibirUsuario As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents tabDadosChamado As Janus.Windows.UI.Tab.UITab
    Friend WithEvents pagArquivos As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents pagComentario As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents pagComentarioDev As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents btnArquivo As Janus.Windows.EditControls.UIButton
    Friend WithEvents grdArquivos As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdComentario As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdComentarioDev As Janus.Windows.GridEX.GridEX
    Friend WithEvents grpControl2 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnIniciar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnFinalizarChamado As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnInserirComentario As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnInserirComentarioDev As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnNovo As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSalvar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnVoltar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair2 As Janus.Windows.EditControls.UIButton
    Friend WithEvents grpApontamento As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblEmpresaApontamento As System.Windows.Forms.Label
    Friend WithEvents cboEmpresaApontamento As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblChamadoApontamento As System.Windows.Forms.Label
    Friend WithEvents cboChamadoApontamento As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents btnIniciarApontamento As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnEncerrarApontamento As Janus.Windows.EditControls.UIButton
    Friend WithEvents grpFiltroApontamento As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblPeriodoApontamento As System.Windows.Forms.Label
    Friend WithEvents dtpInicioApontamento As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtpTerminoApontamento As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents lblResponsavelApontamento As System.Windows.Forms.Label
    Friend WithEvents cboResponsavelApontamento As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblTotalApontamento As System.Windows.Forms.Label
    Friend WithEvents btnFiltrarApontamento As Janus.Windows.EditControls.UIButton
    Friend WithEvents tlpApontamento As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grpApontamentoHistorico As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents grdApontamentoHistorico As Janus.Windows.GridEX.GridEX
    Friend WithEvents grpApontamentoAndamento As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents grdApontamentoAndamento As Janus.Windows.GridEX.GridEX
    Friend WithEvents grpControl3 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnExcelApontamento As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair3 As Janus.Windows.EditControls.UIButton
    Friend WithEvents grpFiltroRelatorio As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblPeriodoRelatorio As System.Windows.Forms.Label
    Friend WithEvents dtpInicioRelatorio As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtpTerminoRelatorio As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents lblEmpresaRelatorio As System.Windows.Forms.Label
    Friend WithEvents cboEmpresaRelatorio As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblTipoAtividadeRelatorio As System.Windows.Forms.Label
    Friend WithEvents cboTipoAtividadeRelatorio As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblTipoAtendimentoRelatorio As System.Windows.Forms.Label
    Friend WithEvents cboTipoAtendimentoRelatorio As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblResponsavelRelatorio As System.Windows.Forms.Label
    Friend WithEvents cboResponsavelRelatorio As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblAgruparRelatorio As System.Windows.Forms.Label
    Friend WithEvents cboAgruparRelatorio As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblGraficoRelatorio As System.Windows.Forms.Label
    Friend WithEvents cboGraficoRelatorio As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblTotalRelatorio As System.Windows.Forms.Label
    Friend WithEvents btnGerarRelatorio As Janus.Windows.EditControls.UIButton
    Friend WithEvents tlpRelatorio As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grpGraficoRelatorio As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents chrRelatorio As Dundas.Charting.WinControl.Chart
    Friend WithEvents grpResumoRelatorio As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents grdRelatorio As Janus.Windows.GridEX.GridEX
    Friend WithEvents grpControl4 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnExcelRelatorio As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSair4 As Janus.Windows.EditControls.UIButton
    Friend WithEvents vsmMain As Janus.Windows.Common.VisualStyleManager
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents tmrApontamento As System.Windows.Forms.Timer

End Class
