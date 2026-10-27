<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Dim JanusColorScheme2 As Janus.Windows.Common.JanusColorScheme = New Janus.Windows.Common.JanusColorScheme()
        Dim UiStatusBarPanel7 As Janus.Windows.UI.StatusBar.UIStatusBarPanel = New Janus.Windows.UI.StatusBar.UIStatusBarPanel()
        Dim UiStatusBarPanel8 As Janus.Windows.UI.StatusBar.UIStatusBarPanel = New Janus.Windows.UI.StatusBar.UIStatusBarPanel()
        Dim UiStatusBarPanel9 As Janus.Windows.UI.StatusBar.UIStatusBarPanel = New Janus.Windows.UI.StatusBar.UIStatusBarPanel()
        Dim UiStatusBarPanel10 As Janus.Windows.UI.StatusBar.UIStatusBarPanel = New Janus.Windows.UI.StatusBar.UIStatusBarPanel()
        Dim UiStatusBarPanel11 As Janus.Windows.UI.StatusBar.UIStatusBarPanel = New Janus.Windows.UI.StatusBar.UIStatusBarPanel()
        Dim UiStatusBarPanel12 As Janus.Windows.UI.StatusBar.UIStatusBarPanel = New Janus.Windows.UI.StatusBar.UIStatusBarPanel()
        Me.errInfo = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.tmrHora = New System.Windows.Forms.Timer(Me.components)
        Me.errMsg = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.vsmMain = New Janus.Windows.Common.VisualStyleManager(Me.components)
        Me.stbMain = New Janus.Windows.UI.StatusBar.UIStatusBar()
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.tabMain = New Janus.Windows.UI.Tab.UITab()
        Me.pagPrincipal = New Janus.Windows.UI.Tab.UITabPage()
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.mnuChamados = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCadastros = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEmpresas = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUsuarios = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAvisos = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBaseConhecimento = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTrocarUsuario = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAlterarSenha = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSair = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.errInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.errMsg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpMain.SuspendLayout()
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMain.SuspendLayout()
        Me.pagPrincipal.SuspendLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'errInfo
        '
        Me.errInfo.ContainerControl = Me
        '
        'tmrHora
        '
        Me.tmrHora.Interval = 1000
        '
        'errMsg
        '
        Me.errMsg.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.errMsg.ContainerControl = Me
        Me.errMsg.Icon = CType(resources.GetObject("errMsg.Icon"), System.Drawing.Icon)
        '
        'vsmMain
        '
        JanusColorScheme2.HighlightTextColor = System.Drawing.SystemColors.HighlightText
        JanusColorScheme2.MenuColor = System.Drawing.Color.WhiteSmoke
        JanusColorScheme2.Name = "Scheme"
        JanusColorScheme2.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.Silver
        JanusColorScheme2.Office2007CustomColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        JanusColorScheme2.VisualStyle = Janus.Windows.Common.VisualStyle.Office2007
        Me.vsmMain.ColorSchemes.Add(JanusColorScheme2)
        Me.vsmMain.DefaultColorScheme = Nothing
        '
        'stbMain
        '
        Me.stbMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.stbMain.Location = New System.Drawing.Point(0, 454)
        Me.stbMain.Name = "stbMain"
        Me.stbMain.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        UiStatusBarPanel7.BorderColor = System.Drawing.Color.Empty
        UiStatusBarPanel7.DrawBorder = False
        UiStatusBarPanel7.Key = "pnlProgressBar"
        UiStatusBarPanel7.PanelType = Janus.Windows.UI.StatusBar.StatusBarPanelType.ProgressBar
        UiStatusBarPanel7.ProgressBarValue = 0
        UiStatusBarPanel7.Width = 200
        UiStatusBarPanel8.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        UiStatusBarPanel8.BorderColor = System.Drawing.Color.Empty
        UiStatusBarPanel8.DrawBorder = False
        UiStatusBarPanel8.Key = "pnlInfo"
        UiStatusBarPanel8.ProgressBarValue = 0
        UiStatusBarPanel8.Width = 502
        UiStatusBarPanel9.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        UiStatusBarPanel9.BorderColor = System.Drawing.Color.Empty
        UiStatusBarPanel9.Key = "pnlEmpresa"
        UiStatusBarPanel9.ProgressBarValue = 0
        UiStatusBarPanel9.Width = 10
        UiStatusBarPanel10.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        UiStatusBarPanel10.BorderColor = System.Drawing.Color.Empty
        UiStatusBarPanel10.Key = "pnlUsuario"
        UiStatusBarPanel10.ProgressBarValue = 0
        UiStatusBarPanel10.Width = 10
        UiStatusBarPanel11.BorderColor = System.Drawing.Color.Empty
        UiStatusBarPanel11.Image = CType(resources.GetObject("UiStatusBarPanel11.Image"), System.Drawing.Image)
        UiStatusBarPanel11.Key = "pnlFuncaoCalculadora"
        UiStatusBarPanel11.ProgressBarValue = 0
        UiStatusBarPanel11.Visible = False
        UiStatusBarPanel11.Width = 23
        UiStatusBarPanel12.BorderColor = System.Drawing.Color.Empty
        UiStatusBarPanel12.Key = "pnlDataHora"
        UiStatusBarPanel12.ProgressBarValue = 0
        UiStatusBarPanel12.Width = 110
        Me.stbMain.Panels.AddRange(New Janus.Windows.UI.StatusBar.UIStatusBarPanel() {UiStatusBarPanel7, UiStatusBarPanel8, UiStatusBarPanel9, UiStatusBarPanel10, UiStatusBarPanel11, UiStatusBarPanel12})
        Me.stbMain.PanelsBorderColor = System.Drawing.SystemColors.ControlDark
        Me.stbMain.Size = New System.Drawing.Size(725, 25)
        Me.stbMain.TabIndex = 16
        Me.stbMain.TabStop = False
        Me.stbMain.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'tlpMain
        '
        Me.tlpMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpMain.AutoScroll = True
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.tabMain, 0, 1)
        Me.tlpMain.Controls.Add(Me.mnuMain, 0, 0)
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.Padding = New System.Windows.Forms.Padding(7, 8, 7, 8)
        Me.tlpMain.RowCount = 2
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(725, 454)
        Me.tlpMain.TabIndex = 17
        '
        'tabMain
        '
        Me.tabMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabMain.BackColor = System.Drawing.Color.Transparent
        Me.tabMain.Location = New System.Drawing.Point(10, 15)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.tabMain.ShowCloseButton = True
        Me.tabMain.ShowFocusRectangle = False
        Me.tabMain.Size = New System.Drawing.Size(705, 428)
        Me.tabMain.TabIndex = 15
        Me.tabMain.TabPages.AddRange(New Janus.Windows.UI.Tab.UITabPage() {Me.pagPrincipal})
        Me.tabMain.TabStop = False
        Me.tabMain.VisualStyle = Janus.Windows.UI.Tab.TabVisualStyle.Office2007
        '
        'pagPrincipal
        '
        Me.pagPrincipal.AllowClose = False
        Me.pagPrincipal.Controls.Add(Me.picImage)
        Me.pagPrincipal.Icon = CType(resources.GetObject("pagPrincipal.Icon"), System.Drawing.Icon)
        Me.pagPrincipal.Location = New System.Drawing.Point(1, 23)
        Me.pagPrincipal.Name = "pagPrincipal"
        Me.pagPrincipal.Padding = New System.Windows.Forms.Padding(8, 9, 8, 9)
        Me.pagPrincipal.Size = New System.Drawing.Size(703, 404)
        Me.pagPrincipal.TabStop = True
        '
        'picImage
        '
        Me.picImage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picImage.BackColor = System.Drawing.SystemColors.Window
        Me.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picImage.Location = New System.Drawing.Point(8, 9)
        Me.picImage.Name = "picImage"
        Me.picImage.Padding = New System.Windows.Forms.Padding(8, 9, 8, 9)
        Me.picImage.Size = New System.Drawing.Size(687, 386)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picImage.TabIndex = 0
        Me.picImage.TabStop = False
        Me.picImage.Visible = False
        '
        'mnuMain
        '
        Me.mnuMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.mnuMain.BackColor = System.Drawing.Color.Transparent
        Me.mnuMain.Dock = System.Windows.Forms.DockStyle.None
        Me.mnuMain.GripMargin = New System.Windows.Forms.Padding(6, 2, 0, 6)
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuChamados, Me.mnuCadastros, Me.mnuTrocarUsuario, Me.mnuAlterarSenha, Me.mnuSair})
        Me.mnuMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.mnuMain.Location = New System.Drawing.Point(7, 8)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(711, 24)
        Me.mnuMain.TabIndex = 1
        Me.mnuMain.Text = "MenuStrip1"
        '
        'mnuChamados
        '
        Me.mnuChamados.Name = "mnuChamados"
        Me.mnuChamados.Size = New System.Drawing.Size(150, 20)
        Me.mnuChamados.Tag = "usrAdmChamado"
        Me.mnuChamados.Text = "Chamados / Apontamentos"
        '
        'mnuCadastros
        '
        Me.mnuCadastros.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEmpresas, Me.mnuUsuarios, Me.mnuAvisos, Me.mnuBaseConhecimento})
        Me.mnuCadastros.Name = "mnuCadastros"
        Me.mnuCadastros.Size = New System.Drawing.Size(71, 20)
        Me.mnuCadastros.Text = "Cadastros"
        '
        'mnuEmpresas
        '
        Me.mnuEmpresas.Name = "mnuEmpresas"
        Me.mnuEmpresas.Size = New System.Drawing.Size(180, 22)
        Me.mnuEmpresas.Tag = "usrCadEmpresa"
        Me.mnuEmpresas.Text = "Empresas"
        '
        'mnuUsuarios
        '
        Me.mnuUsuarios.Name = "mnuUsuarios"
        Me.mnuUsuarios.Size = New System.Drawing.Size(180, 22)
        Me.mnuUsuarios.Tag = "usrCadUsuario"
        Me.mnuUsuarios.Text = "Usuários"
        '
        'mnuAvisos
        '
        Me.mnuAvisos.Name = "mnuAvisos"
        Me.mnuAvisos.Size = New System.Drawing.Size(180, 22)
        Me.mnuAvisos.Tag = "usrCadAviso"
        Me.mnuAvisos.Text = "Avisos"
        '
        'mnuBaseConhecimento
        '
        Me.mnuBaseConhecimento.Name = "mnuBaseConhecimento"
        Me.mnuBaseConhecimento.Size = New System.Drawing.Size(180, 22)
        Me.mnuBaseConhecimento.Tag = "usrCadBaseConhecimento"
        Me.mnuBaseConhecimento.Text = "Base de Conhecimento"
        '
        'mnuTrocarUsuario
        '
        Me.mnuTrocarUsuario.Name = "mnuTrocarUsuario"
        Me.mnuTrocarUsuario.Size = New System.Drawing.Size(100, 20)
        Me.mnuTrocarUsuario.Text = "Trocar Usuário"
        '
        'mnuAlterarSenha
        '
        Me.mnuAlterarSenha.Name = "mnuAlterarSenha"
        Me.mnuAlterarSenha.Size = New System.Drawing.Size(100, 20)
        Me.mnuAlterarSenha.Text = "Alterar Senha"
        '
        'mnuSair
        '
        Me.mnuSair.Name = "mnuSair"
        Me.mnuSair.Size = New System.Drawing.Size(38, 20)
        Me.mnuSair.Text = "Sair"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(725, 479)
        Me.Controls.Add(Me.tlpMain)
        Me.Controls.Add(Me.stbMain)
        Me.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "frmMain"
        Me.Text = "Documentacti"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.errInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.errMsg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMain.ResumeLayout(False)
        Me.pagPrincipal.ResumeLayout(False)
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents errInfo As System.Windows.Forms.ErrorProvider
    Friend WithEvents tmrHora As System.Windows.Forms.Timer
    Friend WithEvents errMsg As System.Windows.Forms.ErrorProvider
    Friend WithEvents vsmMain As Janus.Windows.Common.VisualStyleManager
    Friend WithEvents stbMain As Janus.Windows.UI.StatusBar.UIStatusBar
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tabMain As Janus.Windows.UI.Tab.UITab
    Friend WithEvents pagPrincipal As Janus.Windows.UI.Tab.UITabPage
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuChamados As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCadastros As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEmpresas As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUsuarios As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAvisos As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBaseConhecimento As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTrocarUsuario As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAlterarSenha As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSair As System.Windows.Forms.ToolStripMenuItem

End Class
