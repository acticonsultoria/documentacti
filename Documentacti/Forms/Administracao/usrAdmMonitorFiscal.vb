Imports Janus.Windows.GridEX
Imports System.Threading.Tasks

Public Class usrAdmMonitorFiscal

#Region "::: VARIAVEIS :::"

    Private oClsUsrAdmMonitorFiscal As New clsUsrAdmMonitorFiscal

    'Notas técnicas gravadas no banco (todas) e separadas por documento (uma grid por aba)
    Private oTabelaNFe As DataTable
    Private oTabelaNFSe As DataTable

    'Portais dos autorizadores (teste de disponibilidade) e o quadro das UFs
    Private oPortais As List(Of clsMonitorFiscal.Portal)
    Private oTabelaAutorizador As DataTable
    Private oTabelaUF As DataTable

    Private bColetando As Boolean = False
    Private bVerificando As Boolean = False

    'Nota escolhida no sino antes da lista carregar
    Private lNotaSelecionar As Long = 0

#End Region

#Region "::: CONTROLES :::"

#Region "::: GERAL :::"

    Private Sub usrAdmMonitorFiscal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Call ConfigurarUserControl()

    End Sub

    Private Sub usrAdmMonitorFiscal_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        Try

            Select Case e.KeyCode

                Case Keys.Enter
                    If TypeOf sender Is GridEX Then
                        Call AbrirLinhaGrid(CType(sender, GridEX))
                    Else
                        Control_Enter(sender)
                    End If

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub tmrPortais_Tick(sender As Object, e As EventArgs) Handles tmrPortais.Tick

        'Reteste automático a cada 5 minutos enquanto a tela está aberta
        Call VerificarPortais()

    End Sub

#End Region

#Region "::: NOTAS TÉCNICAS :::"

    Private Sub grdNFe_RowDoubleClick(ByVal sender As Object, ByVal e As RowActionEventArgs) Handles grdNFe.RowDoubleClick, grdNFSe.RowDoubleClick

        Try

            Call AbrirLinhaGrid(CType(sender, GridEX))

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub lnkNFe_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkNFe.LinkClicked, lnkNFSe.LinkClicked

        Try

            Call AbrirLink(CType(sender, LinkLabel).Tag)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnAtualizar_Click(sender As Object, e As EventArgs) Handles btnAtualizar.Click

        'Coleta na hora, sem esperar a janela de 60 minutos
        Call AtualizarNotas(True)

    End Sub

    Private Sub btnFontes_Click(sender As Object, e As EventArgs) Handles btnFontes.Click

        Try

            mnuFontes.Show(btnFontes, 0, btnFontes.Height)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub mnuFonte_Click(sender As Object, e As EventArgs) Handles mnuFonteNFe.Click, mnuFonteNFSe.Click, mnuFonteNoticias.Click, mnuFonteOrientacoes.Click, mnuFonteDisponibilidade.Click

        Try

            Select Case True
                Case sender Is mnuFonteNFe : Call AbrirLink(clsMonitorFiscal.gcUrlNFeLista)
                Case sender Is mnuFonteNFSe : Call AbrirLink(clsMonitorFiscal.gcUrlNFSeRtc)
                Case sender Is mnuFonteNoticias : Call AbrirLink(clsMonitorFiscal.gcUrlNFSeNoticias)
                Case sender Is mnuFonteOrientacoes : Call AbrirLink(clsMonitorFiscal.gcUrlOrientacoesRtc)
                Case sender Is mnuFonteDisponibilidade : Call AbrirLink(clsMonitorFiscal.gcUrlDisponibilidadeNFe)
            End Select

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: PORTAIS :::"

    Private Sub btnVerificar_Click(sender As Object, e As EventArgs) Handles btnVerificar.Click

        Call VerificarPortais()

    End Sub

    Private Sub grdAutorizador_RowDoubleClick(ByVal sender As Object, ByVal e As RowActionEventArgs) Handles grdAutorizador.RowDoubleClick

        Try

            Call AbrirLinhaGrid(grdAutorizador)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#End Region

#Region "::: FUNCTION / SUB :::"

#Region "::: GERAL :::"

    Private Sub ConfigurarUserControl()

        Try

            Cursor.Current = Cursors.WaitCursor

            Call AddGotFocus(Me, New DelegateGotFocus(AddressOf Control_GotFocus))
            Call AddKeyDown(Me, New DelegateKeyDown(AddressOf usrAdmMonitorFiscal_KeyDown))
            Call AddLostFocus(Me, New DelegateLostFocus(AddressOf Control_LostFocus))

            Call ConfiguraGrid(grdNFe)
            Call ConfiguraGrid(grdNFSe)
            Call ConfiguraGrid(grdAgenda)
            Call ConfiguraGrid(grdAutorizador)
            Call ConfiguraGrid(grdUF)

            Call LoadNotas()
            Call LoadAgenda()
            Call MontarPortais()

            Cursor.Current = Cursors.Default

            'Coleta nos portais só se a última tiver mais de 60 minutos (em segundo plano: a tela já abre com a lista do banco)
            Call AtualizarNotas(False)
            Call VerificarPortais()
            tmrPortais.Start()

        Catch ex As Exception
            Cursor.Current = Cursors.Default
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub TratarErro(ByVal ex As Exception)

        Call TratamentoErro(ex, "Monitor Fiscal")

    End Sub

    Private Sub AbrirLink(ByVal oUrl As Object)

        Try

            'Somente links dos portais (http/https) - nada é baixado pelo sistema
            Dim sUrl As String = IIf(IsNothing(oUrl), "", oUrl).ToString.Trim
            If sUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) = False AndAlso _
               sUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase) = False Then Exit Sub

            Process.Start(sUrl)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AbrirLinhaGrid(ByVal oGrid As GridEX)

        Try

            If IsNothing(oGrid.CurrentRow) OrElse oGrid.CurrentRow.RowType <> RowType.Record Then Exit Sub

            Dim oRow As DataRow = CType(oGrid.CurrentRow.DataRow, DataRowView).Row

            If oRow.Table.Columns.Contains("url") Then
                Call AbrirLink(oRow.Item("url"))
            ElseIf oRow.Table.Columns.Contains("url_status") Then
                Call AbrirLink(oRow.Item("url_status"))
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub SelecionarNotaTecnica(ByVal lCodigo As Long)

        'Clique na notificação de nota técnica (sino): seleciona a nota na aba do documento dela
        Try

            tabMain.SelectedTab = pagNotas

            If IsNothing(oTabelaNFe) OrElse IsNothing(oTabelaNFSe) Then
                lNotaSelecionar = lCodigo
                Exit Sub
            End If

            For Each oItem As Object() In New Object() {New Object() {grdNFe, pagNFe}, New Object() {grdNFSe, pagNFSe}}

                Dim oGrid As GridEX = CType(oItem(0), GridEX)

                For Each oRow As GridEXRow In oGrid.GetRows()
                    If oRow.RowType = RowType.Record AndAlso CLng(oRow.Cells("codigo").Value) = lCodigo Then
                        tabNotas.SelectedTab = CType(oItem(1), Janus.Windows.UI.Tab.UITabPage)
                        oGrid.MoveTo(oRow)
                        oGrid.Focus()
                        lNotaSelecionar = 0
                        Exit Sub
                    End If
                Next

            Next

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

#End Region

#Region "::: NOTAS TÉCNICAS :::"

    Private Sub LoadNotas()

        Try

            Dim oTabela As DataTable = oClsUsrAdmMonitorFiscal.LoadNotaTecnica()

            oTabelaNFe = FiltrarDocumento(oTabela, "NF-e")
            oTabelaNFSe = FiltrarDocumento(oTabela, "NFS-e")

            Call ConfigurarDataMemberGrid(grdNFe)
            grdNFe.DataSource = oTabelaNFe
            Call ConfigurarDataMemberGrid(grdNFSe)
            grdNFSe.DataSource = oTabelaNFSe

            pagNFe.Text = "NF-e / NFC-e (" & oTabelaNFe.Rows.Count & ")"
            pagNFSe.Text = "NFS-e (" & oTabelaNFSe.Rows.Count & ")"

            'Destaques: a nota mais recente de cada documento (a proc já ordena da mais nova para a mais antiga)
            Call MostrarDestaque(oTabelaNFe, lblNFeNumero, lblNFeData, lblNFeResumo, lnkNFe)
            Call MostrarDestaque(oTabelaNFSe, lblNFSeNumero, lblNFSeData, lblNFSeResumo, lnkNFSe)

            If bColetando = False Then Call MostrarFonte(oTabela)

            If lNotaSelecionar > 0 Then Call SelecionarNotaTecnica(lNotaSelecionar)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function FiltrarDocumento(ByVal oTabela As DataTable, ByVal sDocumento As String) As DataTable

        Dim oView As New DataView(oTabela)

        oView.RowFilter = "documento = '" & sDocumento & "'"

        Return oView.ToTable()

    End Function

    Private Sub MostrarDestaque(ByVal oTabela As DataTable, _
                                ByVal lblNumero As Label, _
                                ByVal lblData As Label, _
                                ByVal lblResumo As Label, _
                                ByVal lnkAbrir As LinkLabel)

        If oTabela.Rows.Count = 0 Then
            lblNumero.Text = "—"
            lblData.Text = "Nenhuma nota técnica coletada ainda."
            lblResumo.Text = ""
            lnkAbrir.Visible = False
            Exit Sub
        End If

        Dim oRow As DataRow = oTabela.Rows(0)

        lblNumero.Text = (oRow.Item("numero").ToString & "  " & oRow.Item("versao").ToString).Trim
        lblData.Text = IIf(IsDBNull(oRow.Item("data_publicacao")), "Data não informada no portal", "Publicada em " & oRow.Item("data_exibicao").ToString).ToString
        lblResumo.Text = oRow.Item("resumo").ToString
        lnkAbrir.Tag = oRow.Item("url").ToString
        lnkAbrir.Visible = True

    End Sub

    Private Sub MostrarFonte(ByVal oTabela As DataTable)

        If oTabela.Rows.Count = 0 Then
            lblFonte.ForeColor = Color.Gray
            lblFonte.Text = "Nenhuma nota técnica coletada ainda. Clique em Atualizar Notas para ler as fontes oficiais."
            Exit Sub
        End If

        Dim dColeta As Date = CDate(oTabela.Compute("MAX(data_verificacao)", ""))

        lblFonte.ForeColor = Color.Gray
        lblFonte.Text = "Notas lidas das fontes oficiais em " & Format(dColeta, "dd/MM/yyyy HH:mm") & _
                        " (atualização automática a cada " & clsMonitorFiscal.gcIntervaloColetaMinutos & " min). " & _
                        "Duplo clique abre a nota no portal oficial; confirme sempre no portal antes de aplicar."

    End Sub

    Private Async Sub AtualizarNotas(ByVal bForcar As Boolean)

        If bColetando = True Then Exit Sub

        Try

            bColetando = True
            btnAtualizar.Enabled = False
            If bForcar = True Then
                lblFonte.ForeColor = Color.SteelBlue
                lblFonte.Text = "Atualizando as notas técnicas nas fontes oficiais..."
            End If

            'Leitura dos portais em segundo plano (pode levar alguns segundos)
            Dim bColetou As Boolean = Await Task.Run(Function() oClsUsrAdmMonitorFiscal.AtualizarNotasTecnicas(bForcar))

            If Me.IsDisposed Then Exit Sub

            bColetando = False
            Call LoadNotas()

            If bColetou = True Then frmMain.AtualizarSino()
            If bForcar = True Then frmMain.Informacao("Notas técnicas atualizadas.", gColorInformacaoOK)

        Catch ex As Exception

            If Me.IsDisposed Then Exit Sub

            'Portal fora do ar / sem internet: a tela continua com a última lista gravada
            lblFonte.ForeColor = gColorInformacaoErro
            lblFonte.Text = "Não foi possível atualizar agora (" & ex.Message & "). Exibindo a última lista gravada."
            frmMain.Informacao("Monitor Fiscal: não foi possível atualizar as notas técnicas.", gColorInformacaoErro)

        Finally
            bColetando = False
            If Me.IsDisposed = False Then btnAtualizar.Enabled = True
        End Try

    End Sub

#End Region

#Region "::: AGENDA :::"

    Private Sub LoadAgenda()

        Try

            Dim oTabela As DataTable = oClsUsrAdmMonitorFiscal.LoadAgenda()

            Call ConfigurarDataMemberGrid(grdAgenda)
            grdAgenda.DataSource = oTabela

            Dim oProximo() As DataRow = oTabela.Select("situacao = 'PROXIMO'")

            If oProximo.Length = 0 Then
                lblProximoData.Text = "—"
                lblProximoMarco.Text = "Sem marcos futuros cadastrados."
                Exit Sub
            End If

            Dim iDias As Integer = CInt(oProximo(0).Item("dias"))

            lblProximoData.Text = Format(CDate(oProximo(0).Item("data")), "dd/MM") & "  ·  " & _
                                  IIf(iDias = 0, "hoje", IIf(iDias = 1, "1 dia", iDias & " dias")).ToString
            lblProximoMarco.Text = oProximo(0).Item("marco").ToString

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "::: PORTAIS :::"

    Private Sub MontarPortais()

        Try

            oPortais = clsMonitorFiscal.Autorizadores()

            oTabelaAutorizador = New DataTable
            oTabelaAutorizador.Columns.Add("status", GetType(String))
            oTabelaAutorizador.Columns.Add("nome", GetType(String))
            oTabelaAutorizador.Columns.Add("atende", GetType(String))
            oTabelaAutorizador.Columns.Add("tempo", GetType(String))
            oTabelaAutorizador.Columns.Add("codigo", GetType(String))
            oTabelaAutorizador.Columns.Add("url_status", GetType(String))

            For Each oPortal As clsMonitorFiscal.Portal In oPortais
                oTabelaAutorizador.Rows.Add(oPortal.sStatus, oPortal.sNome, oPortal.sAtende, "—", oPortal.sCodigo, oPortal.sUrlStatus)
            Next

            Call ConfigurarDataMemberGrid(grdAutorizador)
            grdAutorizador.DataSource = oTabelaAutorizador

            oTabelaUF = clsMonitorFiscal.UFs()
            Call ConfigurarDataMemberGrid(grdUF)
            grdUF.DataSource = oTabelaUF

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Async Sub VerificarPortais()

        If bVerificando = True OrElse IsNothing(oPortais) Then Exit Sub

        Try

            bVerificando = True
            btnVerificar.Enabled = False
            lblResumo.Text = "Verificando os portais..."

            For Each oRow As DataRow In oTabelaAutorizador.Rows
                oRow.Item("status") = "VERIFICANDO"
                oRow.Item("tempo") = "—"
            Next
            Call PintarUFs()

            'Todos os portais ao mesmo tempo, fora da thread da tela (timeout de 8 s por portal)
            Await Task.Run(Sub() Parallel.ForEach(oPortais, Sub(oPortal) clsMonitorFiscal.TestarPortal(oPortal)))

            If Me.IsDisposed Then Exit Sub

            Dim iNoAr As Integer = 0
            Dim iLento As Integer = 0
            Dim iSemResposta As Integer = 0

            For i As Integer = 0 To oPortais.Count - 1
                oTabelaAutorizador.Rows(i).Item("status") = oPortais(i).sStatus
                oTabelaAutorizador.Rows(i).Item("tempo") = IIf(oPortais(i).iTempo < 0, "—", oPortais(i).iTempo.ToString("N0")).ToString
                Select Case oPortais(i).sStatus
                    Case clsMonitorFiscal.gcStatusNoAr : iNoAr += 1
                    Case clsMonitorFiscal.gcStatusLento : iLento += 1
                    Case Else : iSemResposta += 1
                End Select
            Next

            If iSemResposta = oPortais.Count Then
                'Nenhum respondeu: quase sempre é a rede deste computador (proxy / firewall), não a SEFAZ
                For Each oRow As DataRow In oTabelaAutorizador.Rows
                    oRow.Item("status") = "NÃO TESTADO"
                Next
                lblResumo.ForeColor = gColorInformacaoErro
                lblResumo.Text = "Teste de rede indisponível: este computador não alcançou nenhum portal (proxy ou firewall). Use o status oficial (duplo clique)."
            Else
                lblResumo.ForeColor = Color.Black
                lblResumo.Text = (iNoAr + iLento) & " de " & oPortais.Count & " portais respondendo" & _
                                 IIf(iLento > 0, "  ·  " & iLento & " com resposta lenta", "").ToString & _
                                 IIf(iSemResposta > 0, "  ·  " & iSemResposta & " sem resposta", "").ToString
            End If

            Call PintarUFs()
            grdAutorizador.Refetch()

            lblMedido.Text = "medido às " & Format(Now, "HH:mm:ss")

        Catch ex As Exception
            If Me.IsDisposed = False Then lblResumo.Text = "Falha ao verificar os portais: " & ex.Message
        Finally
            bVerificando = False
            If Me.IsDisposed = False Then btnVerificar.Enabled = True
        End Try

    End Sub

    Private Sub PintarUFs()

        'Cada UF herda o estado do autorizador que a atende
        For Each oRow As DataRow In oTabelaUF.Rows
            Dim oAutorizador() As DataRow = oTabelaAutorizador.Select("codigo = '" & oRow.Item("codigo_autorizador").ToString & "'")
            oRow.Item("status") = IIf(oAutorizador.Length = 0, "NÃO TESTADO", oAutorizador(0).Item("status")).ToString
        Next

        grdUF.Refetch()

    End Sub

#End Region

#End Region

End Class
