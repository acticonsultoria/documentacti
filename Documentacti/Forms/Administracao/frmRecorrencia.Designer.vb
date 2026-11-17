<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRecorrencia
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRecorrencia))
        Me.grpPadrao = New Janus.Windows.EditControls.UIGroupBox()
        Me.optDiario = New Janus.Windows.EditControls.UIRadioButton()
        Me.optSemanal = New Janus.Windows.EditControls.UIRadioButton()
        Me.optMensal = New Janus.Windows.EditControls.UIRadioButton()
        Me.optAnual = New Janus.Windows.EditControls.UIRadioButton()
        Me.pnlDiario = New System.Windows.Forms.Panel()
        Me.pnlSemanal = New System.Windows.Forms.Panel()
        Me.pnlMensal = New System.Windows.Forms.Panel()
        Me.pnlAnual = New System.Windows.Forms.Panel()
        Me.optDiarioIntervalo = New Janus.Windows.EditControls.UIRadioButton()
        Me.numDiarioIntervalo = New Janus.Windows.GridEX.EditControls.IntegerUpDown()
        Me.lblDiarioDias = New System.Windows.Forms.Label()
        Me.optDiarioUteis = New Janus.Windows.EditControls.UIRadioButton()
        Me.lblSemanalCada = New System.Windows.Forms.Label()
        Me.numSemanalIntervalo = New Janus.Windows.GridEX.EditControls.IntegerUpDown()
        Me.lblSemanalSemanas = New System.Windows.Forms.Label()
        Me.chkDomingo = New Janus.Windows.EditControls.UICheckBox()
        Me.chkSegunda = New Janus.Windows.EditControls.UICheckBox()
        Me.chkTerca = New Janus.Windows.EditControls.UICheckBox()
        Me.chkQuarta = New Janus.Windows.EditControls.UICheckBox()
        Me.chkQuinta = New Janus.Windows.EditControls.UICheckBox()
        Me.chkSexta = New Janus.Windows.EditControls.UICheckBox()
        Me.chkSabado = New Janus.Windows.EditControls.UICheckBox()
        Me.lblMensalCada = New System.Windows.Forms.Label()
        Me.numMensalIntervalo = New Janus.Windows.GridEX.EditControls.IntegerUpDown()
        Me.lblMensalMeses = New System.Windows.Forms.Label()
        Me.optMensalDia = New Janus.Windows.EditControls.UIRadioButton()
        Me.numMensalDia = New Janus.Windows.GridEX.EditControls.IntegerUpDown()
        Me.optMensalOrdinal = New Janus.Windows.EditControls.UIRadioButton()
        Me.cboMensalOrdinal = New Janus.Windows.EditControls.UIComboBox()
        Me.cboMensalDiaSemana = New Janus.Windows.EditControls.UIComboBox()
        Me.lblAnualCada = New System.Windows.Forms.Label()
        Me.numAnualIntervalo = New Janus.Windows.GridEX.EditControls.IntegerUpDown()
        Me.lblAnualAnos = New System.Windows.Forms.Label()
        Me.optAnualData = New Janus.Windows.EditControls.UIRadioButton()
        Me.numAnualDia = New Janus.Windows.GridEX.EditControls.IntegerUpDown()
        Me.lblAnualDeData = New System.Windows.Forms.Label()
        Me.cboAnualMesData = New Janus.Windows.EditControls.UIComboBox()
        Me.optAnualOrdinal = New Janus.Windows.EditControls.UIRadioButton()
        Me.cboAnualOrdinal = New Janus.Windows.EditControls.UIComboBox()
        Me.cboAnualDiaSemana = New Janus.Windows.EditControls.UIComboBox()
        Me.lblAnualDeOrdinal = New System.Windows.Forms.Label()
        Me.cboAnualMesOrdinal = New Janus.Windows.EditControls.UIComboBox()
        Me.grpIntervalo = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblComecaEm = New System.Windows.Forms.Label()
        Me.dtpInicio = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.optTerminaEm = New Janus.Windows.EditControls.UIRadioButton()
        Me.dtpTermino = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.optTerminaApos = New Janus.Windows.EditControls.UIRadioButton()
        Me.numOcorrencias = New Janus.Windows.GridEX.EditControls.IntegerUpDown()
        Me.lblOcorrencias = New System.Windows.Forms.Label()
        Me.optSemTermino = New Janus.Windows.EditControls.UIRadioButton()
        Me.lblResumo = New System.Windows.Forms.Label()
        Me.btnOk = New Janus.Windows.EditControls.UIButton()
        Me.btnCancelar = New Janus.Windows.EditControls.UIButton()
        Me.btnRemover = New Janus.Windows.EditControls.UIButton()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.grpPadrao, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpIntervalo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPadrao.SuspendLayout()
        Me.pnlDiario.SuspendLayout()
        Me.pnlSemanal.SuspendLayout()
        Me.pnlMensal.SuspendLayout()
        Me.pnlAnual.SuspendLayout()
        Me.grpIntervalo.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpPadrao
        '
        Me.grpPadrao.BackColor = System.Drawing.Color.Transparent
        Me.grpPadrao.Controls.Add(Me.pnlAnual)
        Me.grpPadrao.Controls.Add(Me.pnlMensal)
        Me.grpPadrao.Controls.Add(Me.pnlSemanal)
        Me.grpPadrao.Controls.Add(Me.pnlDiario)
        Me.grpPadrao.Controls.Add(Me.optAnual)
        Me.grpPadrao.Controls.Add(Me.optMensal)
        Me.grpPadrao.Controls.Add(Me.optSemanal)
        Me.grpPadrao.Controls.Add(Me.optDiario)
        Me.grpPadrao.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpPadrao.Location = New System.Drawing.Point(8, 8)
        Me.grpPadrao.Name = "grpPadrao"
        Me.grpPadrao.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpPadrao.Size = New System.Drawing.Size(544, 168)
        Me.grpPadrao.TabIndex = 0
        Me.grpPadrao.Text = "Padrão de recorrência"
        Me.grpPadrao.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'optDiario
        '
        Me.optDiario.BackColor = System.Drawing.Color.Transparent
        Me.optDiario.Location = New System.Drawing.Point(10, 24)
        Me.optDiario.Name = "optDiario"
        Me.optDiario.Size = New System.Drawing.Size(80, 20)
        Me.optDiario.TabIndex = 0
        Me.optDiario.Text = "Diário"
        Me.optDiario.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'optSemanal
        '
        Me.optSemanal.BackColor = System.Drawing.Color.Transparent
        Me.optSemanal.Location = New System.Drawing.Point(10, 52)
        Me.optSemanal.Name = "optSemanal"
        Me.optSemanal.Size = New System.Drawing.Size(80, 20)
        Me.optSemanal.TabIndex = 1
        Me.optSemanal.Text = "Semanal"
        Me.optSemanal.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'optMensal
        '
        Me.optMensal.BackColor = System.Drawing.Color.Transparent
        Me.optMensal.Location = New System.Drawing.Point(10, 80)
        Me.optMensal.Name = "optMensal"
        Me.optMensal.Size = New System.Drawing.Size(80, 20)
        Me.optMensal.TabIndex = 2
        Me.optMensal.Text = "Mensal"
        Me.optMensal.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'optAnual
        '
        Me.optAnual.BackColor = System.Drawing.Color.Transparent
        Me.optAnual.Location = New System.Drawing.Point(10, 108)
        Me.optAnual.Name = "optAnual"
        Me.optAnual.Size = New System.Drawing.Size(80, 20)
        Me.optAnual.TabIndex = 3
        Me.optAnual.Text = "Anual"
        Me.optAnual.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'pnlDiario
        '
        Me.pnlDiario.BackColor = System.Drawing.Color.Transparent
        Me.pnlDiario.Controls.Add(Me.optDiarioUteis)
        Me.pnlDiario.Controls.Add(Me.lblDiarioDias)
        Me.pnlDiario.Controls.Add(Me.numDiarioIntervalo)
        Me.pnlDiario.Controls.Add(Me.optDiarioIntervalo)
        Me.pnlDiario.Location = New System.Drawing.Point(100, 18)
        Me.pnlDiario.Name = "pnlDiario"
        Me.pnlDiario.Size = New System.Drawing.Size(436, 142)
        Me.pnlDiario.TabIndex = 4
        '
        'pnlSemanal
        '
        Me.pnlSemanal.BackColor = System.Drawing.Color.Transparent
        Me.pnlSemanal.Controls.Add(Me.chkSabado)
        Me.pnlSemanal.Controls.Add(Me.chkSexta)
        Me.pnlSemanal.Controls.Add(Me.chkQuinta)
        Me.pnlSemanal.Controls.Add(Me.chkQuarta)
        Me.pnlSemanal.Controls.Add(Me.chkTerca)
        Me.pnlSemanal.Controls.Add(Me.chkSegunda)
        Me.pnlSemanal.Controls.Add(Me.chkDomingo)
        Me.pnlSemanal.Controls.Add(Me.lblSemanalSemanas)
        Me.pnlSemanal.Controls.Add(Me.numSemanalIntervalo)
        Me.pnlSemanal.Controls.Add(Me.lblSemanalCada)
        Me.pnlSemanal.Location = New System.Drawing.Point(100, 18)
        Me.pnlSemanal.Name = "pnlSemanal"
        Me.pnlSemanal.Size = New System.Drawing.Size(436, 142)
        Me.pnlSemanal.TabIndex = 4
        '
        'pnlMensal
        '
        Me.pnlMensal.BackColor = System.Drawing.Color.Transparent
        Me.pnlMensal.Controls.Add(Me.cboMensalDiaSemana)
        Me.pnlMensal.Controls.Add(Me.cboMensalOrdinal)
        Me.pnlMensal.Controls.Add(Me.optMensalOrdinal)
        Me.pnlMensal.Controls.Add(Me.numMensalDia)
        Me.pnlMensal.Controls.Add(Me.optMensalDia)
        Me.pnlMensal.Controls.Add(Me.lblMensalMeses)
        Me.pnlMensal.Controls.Add(Me.numMensalIntervalo)
        Me.pnlMensal.Controls.Add(Me.lblMensalCada)
        Me.pnlMensal.Location = New System.Drawing.Point(100, 18)
        Me.pnlMensal.Name = "pnlMensal"
        Me.pnlMensal.Size = New System.Drawing.Size(436, 142)
        Me.pnlMensal.TabIndex = 4
        '
        'pnlAnual
        '
        Me.pnlAnual.BackColor = System.Drawing.Color.Transparent
        Me.pnlAnual.Controls.Add(Me.cboAnualMesOrdinal)
        Me.pnlAnual.Controls.Add(Me.lblAnualDeOrdinal)
        Me.pnlAnual.Controls.Add(Me.cboAnualDiaSemana)
        Me.pnlAnual.Controls.Add(Me.cboAnualOrdinal)
        Me.pnlAnual.Controls.Add(Me.optAnualOrdinal)
        Me.pnlAnual.Controls.Add(Me.cboAnualMesData)
        Me.pnlAnual.Controls.Add(Me.lblAnualDeData)
        Me.pnlAnual.Controls.Add(Me.numAnualDia)
        Me.pnlAnual.Controls.Add(Me.optAnualData)
        Me.pnlAnual.Controls.Add(Me.lblAnualAnos)
        Me.pnlAnual.Controls.Add(Me.numAnualIntervalo)
        Me.pnlAnual.Controls.Add(Me.lblAnualCada)
        Me.pnlAnual.Location = New System.Drawing.Point(100, 18)
        Me.pnlAnual.Name = "pnlAnual"
        Me.pnlAnual.Size = New System.Drawing.Size(436, 142)
        Me.pnlAnual.TabIndex = 4
        '
        'optDiarioIntervalo
        '
        Me.optDiarioIntervalo.BackColor = System.Drawing.Color.Transparent
        Me.optDiarioIntervalo.Location = New System.Drawing.Point(0, 6)
        Me.optDiarioIntervalo.Name = "optDiarioIntervalo"
        Me.optDiarioIntervalo.Size = New System.Drawing.Size(70, 20)
        Me.optDiarioIntervalo.TabIndex = 0
        Me.optDiarioIntervalo.Text = "A cada"
        Me.optDiarioIntervalo.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'numDiarioIntervalo
        '
        Me.numDiarioIntervalo.Location = New System.Drawing.Point(74, 6)
        Me.numDiarioIntervalo.Maximum = 999
        Me.numDiarioIntervalo.Minimum = 1
        Me.numDiarioIntervalo.Name = "numDiarioIntervalo"
        Me.numDiarioIntervalo.Size = New System.Drawing.Size(50, 20)
        Me.numDiarioIntervalo.TabIndex = 1
        Me.numDiarioIntervalo.Value = 1
        Me.numDiarioIntervalo.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblDiarioDias
        '
        Me.lblDiarioDias.AutoSize = True
        Me.lblDiarioDias.BackColor = System.Drawing.Color.Transparent
        Me.lblDiarioDias.Location = New System.Drawing.Point(130, 9)
        Me.lblDiarioDias.Name = "lblDiarioDias"
        Me.lblDiarioDias.TabIndex = 2
        Me.lblDiarioDias.Text = "dia(s)"
        '
        'optDiarioUteis
        '
        Me.optDiarioUteis.BackColor = System.Drawing.Color.Transparent
        Me.optDiarioUteis.Location = New System.Drawing.Point(0, 34)
        Me.optDiarioUteis.Name = "optDiarioUteis"
        Me.optDiarioUteis.Size = New System.Drawing.Size(200, 20)
        Me.optDiarioUteis.TabIndex = 3
        Me.optDiarioUteis.Text = "Todos os dias úteis"
        Me.optDiarioUteis.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'lblSemanalCada
        '
        Me.lblSemanalCada.AutoSize = True
        Me.lblSemanalCada.BackColor = System.Drawing.Color.Transparent
        Me.lblSemanalCada.Location = New System.Drawing.Point(0, 9)
        Me.lblSemanalCada.Name = "lblSemanalCada"
        Me.lblSemanalCada.TabIndex = 0
        Me.lblSemanalCada.Text = "A cada"
        '
        'numSemanalIntervalo
        '
        Me.numSemanalIntervalo.Location = New System.Drawing.Point(44, 6)
        Me.numSemanalIntervalo.Maximum = 999
        Me.numSemanalIntervalo.Minimum = 1
        Me.numSemanalIntervalo.Name = "numSemanalIntervalo"
        Me.numSemanalIntervalo.Size = New System.Drawing.Size(50, 20)
        Me.numSemanalIntervalo.TabIndex = 1
        Me.numSemanalIntervalo.Value = 1
        Me.numSemanalIntervalo.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblSemanalSemanas
        '
        Me.lblSemanalSemanas.AutoSize = True
        Me.lblSemanalSemanas.BackColor = System.Drawing.Color.Transparent
        Me.lblSemanalSemanas.Location = New System.Drawing.Point(100, 9)
        Me.lblSemanalSemanas.Name = "lblSemanalSemanas"
        Me.lblSemanalSemanas.TabIndex = 2
        Me.lblSemanalSemanas.Text = "semana(s) no(a):"
        '
        'chkDomingo
        '
        Me.chkDomingo.BackColor = System.Drawing.Color.Transparent
        Me.chkDomingo.Location = New System.Drawing.Point(0, 36)
        Me.chkDomingo.Name = "chkDomingo"
        Me.chkDomingo.Size = New System.Drawing.Size(100, 20)
        Me.chkDomingo.TabIndex = 3
        Me.chkDomingo.Text = "domingo"
        Me.chkDomingo.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'chkSegunda
        '
        Me.chkSegunda.BackColor = System.Drawing.Color.Transparent
        Me.chkSegunda.Location = New System.Drawing.Point(105, 36)
        Me.chkSegunda.Name = "chkSegunda"
        Me.chkSegunda.Size = New System.Drawing.Size(100, 20)
        Me.chkSegunda.TabIndex = 4
        Me.chkSegunda.Text = "segunda-feira"
        Me.chkSegunda.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'chkTerca
        '
        Me.chkTerca.BackColor = System.Drawing.Color.Transparent
        Me.chkTerca.Location = New System.Drawing.Point(210, 36)
        Me.chkTerca.Name = "chkTerca"
        Me.chkTerca.Size = New System.Drawing.Size(100, 20)
        Me.chkTerca.TabIndex = 5
        Me.chkTerca.Text = "terça-feira"
        Me.chkTerca.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'chkQuarta
        '
        Me.chkQuarta.BackColor = System.Drawing.Color.Transparent
        Me.chkQuarta.Location = New System.Drawing.Point(315, 36)
        Me.chkQuarta.Name = "chkQuarta"
        Me.chkQuarta.Size = New System.Drawing.Size(100, 20)
        Me.chkQuarta.TabIndex = 6
        Me.chkQuarta.Text = "quarta-feira"
        Me.chkQuarta.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'chkQuinta
        '
        Me.chkQuinta.BackColor = System.Drawing.Color.Transparent
        Me.chkQuinta.Location = New System.Drawing.Point(0, 64)
        Me.chkQuinta.Name = "chkQuinta"
        Me.chkQuinta.Size = New System.Drawing.Size(100, 20)
        Me.chkQuinta.TabIndex = 7
        Me.chkQuinta.Text = "quinta-feira"
        Me.chkQuinta.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'chkSexta
        '
        Me.chkSexta.BackColor = System.Drawing.Color.Transparent
        Me.chkSexta.Location = New System.Drawing.Point(105, 64)
        Me.chkSexta.Name = "chkSexta"
        Me.chkSexta.Size = New System.Drawing.Size(100, 20)
        Me.chkSexta.TabIndex = 8
        Me.chkSexta.Text = "sexta-feira"
        Me.chkSexta.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'chkSabado
        '
        Me.chkSabado.BackColor = System.Drawing.Color.Transparent
        Me.chkSabado.Location = New System.Drawing.Point(210, 64)
        Me.chkSabado.Name = "chkSabado"
        Me.chkSabado.Size = New System.Drawing.Size(100, 20)
        Me.chkSabado.TabIndex = 9
        Me.chkSabado.Text = "sábado"
        Me.chkSabado.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'lblMensalCada
        '
        Me.lblMensalCada.AutoSize = True
        Me.lblMensalCada.BackColor = System.Drawing.Color.Transparent
        Me.lblMensalCada.Location = New System.Drawing.Point(0, 9)
        Me.lblMensalCada.Name = "lblMensalCada"
        Me.lblMensalCada.TabIndex = 0
        Me.lblMensalCada.Text = "A cada"
        '
        'numMensalIntervalo
        '
        Me.numMensalIntervalo.Location = New System.Drawing.Point(44, 6)
        Me.numMensalIntervalo.Maximum = 999
        Me.numMensalIntervalo.Minimum = 1
        Me.numMensalIntervalo.Name = "numMensalIntervalo"
        Me.numMensalIntervalo.Size = New System.Drawing.Size(50, 20)
        Me.numMensalIntervalo.TabIndex = 1
        Me.numMensalIntervalo.Value = 1
        Me.numMensalIntervalo.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblMensalMeses
        '
        Me.lblMensalMeses.AutoSize = True
        Me.lblMensalMeses.BackColor = System.Drawing.Color.Transparent
        Me.lblMensalMeses.Location = New System.Drawing.Point(100, 9)
        Me.lblMensalMeses.Name = "lblMensalMeses"
        Me.lblMensalMeses.TabIndex = 2
        Me.lblMensalMeses.Text = "mês(es), no:"
        '
        'optMensalDia
        '
        Me.optMensalDia.BackColor = System.Drawing.Color.Transparent
        Me.optMensalDia.Location = New System.Drawing.Point(0, 36)
        Me.optMensalDia.Name = "optMensalDia"
        Me.optMensalDia.Size = New System.Drawing.Size(50, 20)
        Me.optMensalDia.TabIndex = 3
        Me.optMensalDia.Text = "Dia"
        Me.optMensalDia.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'numMensalDia
        '
        Me.numMensalDia.Location = New System.Drawing.Point(54, 36)
        Me.numMensalDia.Maximum = 999
        Me.numMensalDia.Minimum = 1
        Me.numMensalDia.Name = "numMensalDia"
        Me.numMensalDia.Size = New System.Drawing.Size(50, 20)
        Me.numMensalDia.TabIndex = 4
        Me.numMensalDia.Value = 1
        Me.numMensalDia.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'optMensalOrdinal
        '
        Me.optMensalOrdinal.BackColor = System.Drawing.Color.Transparent
        Me.optMensalOrdinal.Location = New System.Drawing.Point(0, 64)
        Me.optMensalOrdinal.Name = "optMensalOrdinal"
        Me.optMensalOrdinal.Size = New System.Drawing.Size(50, 20)
        Me.optMensalOrdinal.TabIndex = 5
        Me.optMensalOrdinal.Text = "O(a)"
        Me.optMensalOrdinal.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'cboMensalOrdinal
        '
        Me.cboMensalOrdinal.Location = New System.Drawing.Point(54, 64)
        Me.cboMensalOrdinal.Name = "cboMensalOrdinal"
        Me.cboMensalOrdinal.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboMensalOrdinal.Size = New System.Drawing.Size(90, 20)
        Me.cboMensalOrdinal.TabIndex = 6
        '
        'cboMensalDiaSemana
        '
        Me.cboMensalDiaSemana.Location = New System.Drawing.Point(148, 64)
        Me.cboMensalDiaSemana.Name = "cboMensalDiaSemana"
        Me.cboMensalDiaSemana.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboMensalDiaSemana.Size = New System.Drawing.Size(110, 20)
        Me.cboMensalDiaSemana.TabIndex = 7
        '
        'lblAnualCada
        '
        Me.lblAnualCada.AutoSize = True
        Me.lblAnualCada.BackColor = System.Drawing.Color.Transparent
        Me.lblAnualCada.Location = New System.Drawing.Point(0, 9)
        Me.lblAnualCada.Name = "lblAnualCada"
        Me.lblAnualCada.TabIndex = 0
        Me.lblAnualCada.Text = "A cada"
        '
        'numAnualIntervalo
        '
        Me.numAnualIntervalo.Location = New System.Drawing.Point(44, 6)
        Me.numAnualIntervalo.Maximum = 999
        Me.numAnualIntervalo.Minimum = 1
        Me.numAnualIntervalo.Name = "numAnualIntervalo"
        Me.numAnualIntervalo.Size = New System.Drawing.Size(50, 20)
        Me.numAnualIntervalo.TabIndex = 1
        Me.numAnualIntervalo.Value = 1
        Me.numAnualIntervalo.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblAnualAnos
        '
        Me.lblAnualAnos.AutoSize = True
        Me.lblAnualAnos.BackColor = System.Drawing.Color.Transparent
        Me.lblAnualAnos.Location = New System.Drawing.Point(100, 9)
        Me.lblAnualAnos.Name = "lblAnualAnos"
        Me.lblAnualAnos.TabIndex = 2
        Me.lblAnualAnos.Text = "ano(s), em:"
        '
        'optAnualData
        '
        Me.optAnualData.BackColor = System.Drawing.Color.Transparent
        Me.optAnualData.Location = New System.Drawing.Point(0, 36)
        Me.optAnualData.Name = "optAnualData"
        Me.optAnualData.Size = New System.Drawing.Size(50, 20)
        Me.optAnualData.TabIndex = 3
        Me.optAnualData.Text = "Dia"
        Me.optAnualData.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'numAnualDia
        '
        Me.numAnualDia.Location = New System.Drawing.Point(54, 36)
        Me.numAnualDia.Maximum = 999
        Me.numAnualDia.Minimum = 1
        Me.numAnualDia.Name = "numAnualDia"
        Me.numAnualDia.Size = New System.Drawing.Size(50, 20)
        Me.numAnualDia.TabIndex = 4
        Me.numAnualDia.Value = 1
        Me.numAnualDia.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblAnualDeData
        '
        Me.lblAnualDeData.AutoSize = True
        Me.lblAnualDeData.BackColor = System.Drawing.Color.Transparent
        Me.lblAnualDeData.Location = New System.Drawing.Point(108, 39)
        Me.lblAnualDeData.Name = "lblAnualDeData"
        Me.lblAnualDeData.TabIndex = 5
        Me.lblAnualDeData.Text = "de"
        '
        'cboAnualMesData
        '
        Me.cboAnualMesData.Location = New System.Drawing.Point(130, 36)
        Me.cboAnualMesData.Name = "cboAnualMesData"
        Me.cboAnualMesData.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboAnualMesData.Size = New System.Drawing.Size(110, 20)
        Me.cboAnualMesData.TabIndex = 6
        '
        'optAnualOrdinal
        '
        Me.optAnualOrdinal.BackColor = System.Drawing.Color.Transparent
        Me.optAnualOrdinal.Location = New System.Drawing.Point(0, 64)
        Me.optAnualOrdinal.Name = "optAnualOrdinal"
        Me.optAnualOrdinal.Size = New System.Drawing.Size(50, 20)
        Me.optAnualOrdinal.TabIndex = 7
        Me.optAnualOrdinal.Text = "O(a)"
        Me.optAnualOrdinal.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'cboAnualOrdinal
        '
        Me.cboAnualOrdinal.Location = New System.Drawing.Point(54, 64)
        Me.cboAnualOrdinal.Name = "cboAnualOrdinal"
        Me.cboAnualOrdinal.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboAnualOrdinal.Size = New System.Drawing.Size(90, 20)
        Me.cboAnualOrdinal.TabIndex = 8
        '
        'cboAnualDiaSemana
        '
        Me.cboAnualDiaSemana.Location = New System.Drawing.Point(148, 64)
        Me.cboAnualDiaSemana.Name = "cboAnualDiaSemana"
        Me.cboAnualDiaSemana.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboAnualDiaSemana.Size = New System.Drawing.Size(110, 20)
        Me.cboAnualDiaSemana.TabIndex = 9
        '
        'lblAnualDeOrdinal
        '
        Me.lblAnualDeOrdinal.AutoSize = True
        Me.lblAnualDeOrdinal.BackColor = System.Drawing.Color.Transparent
        Me.lblAnualDeOrdinal.Location = New System.Drawing.Point(262, 67)
        Me.lblAnualDeOrdinal.Name = "lblAnualDeOrdinal"
        Me.lblAnualDeOrdinal.TabIndex = 10
        Me.lblAnualDeOrdinal.Text = "de"
        '
        'cboAnualMesOrdinal
        '
        Me.cboAnualMesOrdinal.Location = New System.Drawing.Point(284, 64)
        Me.cboAnualMesOrdinal.Name = "cboAnualMesOrdinal"
        Me.cboAnualMesOrdinal.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.cboAnualMesOrdinal.Size = New System.Drawing.Size(110, 20)
        Me.cboAnualMesOrdinal.TabIndex = 11
        '
        'grpIntervalo
        '
        Me.grpIntervalo.BackColor = System.Drawing.Color.Transparent
        Me.grpIntervalo.Controls.Add(Me.optSemTermino)
        Me.grpIntervalo.Controls.Add(Me.lblOcorrencias)
        Me.grpIntervalo.Controls.Add(Me.numOcorrencias)
        Me.grpIntervalo.Controls.Add(Me.optTerminaApos)
        Me.grpIntervalo.Controls.Add(Me.dtpTermino)
        Me.grpIntervalo.Controls.Add(Me.optTerminaEm)
        Me.grpIntervalo.Controls.Add(Me.dtpInicio)
        Me.grpIntervalo.Controls.Add(Me.lblComecaEm)
        Me.grpIntervalo.FormatStyle.ForeColor = System.Drawing.Color.Blue
        Me.grpIntervalo.Location = New System.Drawing.Point(8, 182)
        Me.grpIntervalo.Name = "grpIntervalo"
        Me.grpIntervalo.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.grpIntervalo.Size = New System.Drawing.Size(544, 108)
        Me.grpIntervalo.TabIndex = 1
        Me.grpIntervalo.Text = "Intervalo de recorrência"
        Me.grpIntervalo.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblComecaEm
        '
        Me.lblComecaEm.AutoSize = True
        Me.lblComecaEm.BackColor = System.Drawing.Color.Transparent
        Me.lblComecaEm.Location = New System.Drawing.Point(10, 24)
        Me.lblComecaEm.Name = "lblComecaEm"
        Me.lblComecaEm.TabIndex = 0
        Me.lblComecaEm.Text = "Começa em:"
        '
        'dtpInicio
        '
        Me.dtpInicio.DropDownCalendar.Name = ""
        Me.dtpInicio.Location = New System.Drawing.Point(80, 21)
        Me.dtpInicio.Name = "dtpInicio"
        Me.dtpInicio.Size = New System.Drawing.Size(110, 20)
        Me.dtpInicio.TabIndex = 1
        '
        'optTerminaEm
        '
        Me.optTerminaEm.BackColor = System.Drawing.Color.Transparent
        Me.optTerminaEm.Location = New System.Drawing.Point(250, 21)
        Me.optTerminaEm.Name = "optTerminaEm"
        Me.optTerminaEm.Size = New System.Drawing.Size(105, 20)
        Me.optTerminaEm.TabIndex = 2
        Me.optTerminaEm.Text = "Termina em:"
        Me.optTerminaEm.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'dtpTermino
        '
        Me.dtpTermino.DropDownCalendar.Name = ""
        Me.dtpTermino.Location = New System.Drawing.Point(360, 21)
        Me.dtpTermino.Name = "dtpTermino"
        Me.dtpTermino.Size = New System.Drawing.Size(110, 20)
        Me.dtpTermino.TabIndex = 3
        '
        'optTerminaApos
        '
        Me.optTerminaApos.BackColor = System.Drawing.Color.Transparent
        Me.optTerminaApos.Location = New System.Drawing.Point(250, 49)
        Me.optTerminaApos.Name = "optTerminaApos"
        Me.optTerminaApos.Size = New System.Drawing.Size(105, 20)
        Me.optTerminaApos.TabIndex = 4
        Me.optTerminaApos.Text = "Termina após:"
        Me.optTerminaApos.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'numOcorrencias
        '
        Me.numOcorrencias.Location = New System.Drawing.Point(360, 49)
        Me.numOcorrencias.Maximum = 999
        Me.numOcorrencias.Minimum = 1
        Me.numOcorrencias.Name = "numOcorrencias"
        Me.numOcorrencias.Size = New System.Drawing.Size(50, 20)
        Me.numOcorrencias.TabIndex = 5
        Me.numOcorrencias.Value = 1
        Me.numOcorrencias.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblOcorrencias
        '
        Me.lblOcorrencias.AutoSize = True
        Me.lblOcorrencias.BackColor = System.Drawing.Color.Transparent
        Me.lblOcorrencias.Location = New System.Drawing.Point(415, 52)
        Me.lblOcorrencias.Name = "lblOcorrencias"
        Me.lblOcorrencias.TabIndex = 6
        Me.lblOcorrencias.Text = "ocorrências"
        '
        'optSemTermino
        '
        Me.optSemTermino.BackColor = System.Drawing.Color.Transparent
        Me.optSemTermino.Location = New System.Drawing.Point(250, 77)
        Me.optSemTermino.Name = "optSemTermino"
        Me.optSemTermino.Size = New System.Drawing.Size(180, 20)
        Me.optSemTermino.TabIndex = 7
        Me.optSemTermino.Text = "Sem data de término"
        Me.optSemTermino.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'lblResumo
        '
        Me.lblResumo.AutoSize = True
        Me.lblResumo.BackColor = System.Drawing.Color.Transparent
        Me.lblResumo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResumo.ForeColor = System.Drawing.Color.Blue
        Me.lblResumo.Location = New System.Drawing.Point(8, 298)
        Me.lblResumo.Name = "lblResumo"
        Me.lblResumo.TabIndex = 2
        Me.lblResumo.Text = ""
        '
        'btnOk
        '
        Me.btnOk.Image = Global.Documentacti.My.Resources.Resources.aprovar
        Me.btnOk.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnOk.Location = New System.Drawing.Point(166, 330)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnOk.Size = New System.Drawing.Size(91, 23)
        Me.btnOk.TabIndex = 3
        Me.btnOk.Text = "OK"
        Me.btnOk.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = Global.Documentacti.My.Resources.Resources.cancelar
        Me.btnCancelar.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnCancelar.Location = New System.Drawing.Point(263, 330)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnCancelar.Size = New System.Drawing.Size(91, 23)
        Me.btnCancelar.TabIndex = 4
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'btnRemover
        '
        Me.btnRemover.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.btnRemover.Location = New System.Drawing.Point(360, 330)
        Me.btnRemover.Name = "btnRemover"
        Me.btnRemover.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnRemover.Size = New System.Drawing.Size(160, 23)
        Me.btnRemover.TabIndex = 5
        Me.btnRemover.Text = "Remover recorrência"
        Me.btnRemover.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
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
        'frmRecorrencia
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(560, 362)
        Me.Controls.Add(Me.grpPadrao)
        Me.Controls.Add(Me.grpIntervalo)
        Me.Controls.Add(Me.lblResumo)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnRemover)
        Me.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRecorrencia"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Chamado recorrente"
        CType(Me.grpPadrao, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpIntervalo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPadrao.ResumeLayout(False)
        Me.grpPadrao.PerformLayout()
        Me.pnlDiario.ResumeLayout(False)
        Me.pnlDiario.PerformLayout()
        Me.pnlSemanal.ResumeLayout(False)
        Me.pnlSemanal.PerformLayout()
        Me.pnlMensal.ResumeLayout(False)
        Me.pnlMensal.PerformLayout()
        Me.pnlAnual.ResumeLayout(False)
        Me.pnlAnual.PerformLayout()
        Me.grpIntervalo.ResumeLayout(False)
        Me.grpIntervalo.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpPadrao As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents optDiario As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents optSemanal As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents optMensal As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents optAnual As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents pnlDiario As System.Windows.Forms.Panel
    Friend WithEvents pnlSemanal As System.Windows.Forms.Panel
    Friend WithEvents pnlMensal As System.Windows.Forms.Panel
    Friend WithEvents pnlAnual As System.Windows.Forms.Panel
    Friend WithEvents optDiarioIntervalo As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents numDiarioIntervalo As Janus.Windows.GridEX.EditControls.IntegerUpDown
    Friend WithEvents lblDiarioDias As System.Windows.Forms.Label
    Friend WithEvents optDiarioUteis As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents lblSemanalCada As System.Windows.Forms.Label
    Friend WithEvents numSemanalIntervalo As Janus.Windows.GridEX.EditControls.IntegerUpDown
    Friend WithEvents lblSemanalSemanas As System.Windows.Forms.Label
    Friend WithEvents chkDomingo As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents chkSegunda As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents chkTerca As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents chkQuarta As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents chkQuinta As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents chkSexta As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents chkSabado As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents lblMensalCada As System.Windows.Forms.Label
    Friend WithEvents numMensalIntervalo As Janus.Windows.GridEX.EditControls.IntegerUpDown
    Friend WithEvents lblMensalMeses As System.Windows.Forms.Label
    Friend WithEvents optMensalDia As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents numMensalDia As Janus.Windows.GridEX.EditControls.IntegerUpDown
    Friend WithEvents optMensalOrdinal As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents cboMensalOrdinal As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents cboMensalDiaSemana As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblAnualCada As System.Windows.Forms.Label
    Friend WithEvents numAnualIntervalo As Janus.Windows.GridEX.EditControls.IntegerUpDown
    Friend WithEvents lblAnualAnos As System.Windows.Forms.Label
    Friend WithEvents optAnualData As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents numAnualDia As Janus.Windows.GridEX.EditControls.IntegerUpDown
    Friend WithEvents lblAnualDeData As System.Windows.Forms.Label
    Friend WithEvents cboAnualMesData As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents optAnualOrdinal As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents cboAnualOrdinal As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents cboAnualDiaSemana As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents lblAnualDeOrdinal As System.Windows.Forms.Label
    Friend WithEvents cboAnualMesOrdinal As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents grpIntervalo As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblComecaEm As System.Windows.Forms.Label
    Friend WithEvents dtpInicio As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents optTerminaEm As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents dtpTermino As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents optTerminaApos As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents numOcorrencias As Janus.Windows.GridEX.EditControls.IntegerUpDown
    Friend WithEvents lblOcorrencias As System.Windows.Forms.Label
    Friend WithEvents optSemTermino As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents lblResumo As System.Windows.Forms.Label
    Friend WithEvents btnOk As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnCancelar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnRemover As Janus.Windows.EditControls.UIButton
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip

End Class
