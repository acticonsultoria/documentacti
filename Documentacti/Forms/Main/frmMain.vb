Imports Janus.Windows.UI.Tab
Imports ToastUI

Public Class frmMain

#Region "::: VARIAVEIS :::"

    Private oClsConfigXML As New clsConfigXML
    Private WithEvents tmrInformacao As New System.Windows.Forms.Timer

    'Notificações (ToastUI) das mensagens novas do chat dos chamados
    Private WithEvents tmrNotificacao As New System.Windows.Forms.Timer
    Private oToastManager As New ToastManager
    Private oMensagensNotificadas As New HashSet(Of String)
    Private oToastsChat As New Dictionary(Of String, Toast)
    Private bNotificacaoIniciada As Boolean = False
    Private bSilenciarNotificacao As Boolean = False
    Private Const gcIntervaloNotificacao As Integer = 30000 'milissegundos

    'Chamados criados sem responsável para o time do usuário (notificados uma vez; marcados no banco)
    Private oChamadosNotificados As New HashSet(Of Long)
    Private bNotificacaoChamadoIndisponivel As Boolean = False
    'Avisos do calendário já exibidos nesta sessão (evento | tipo | data do aviso)
    Private oEventosNotificados As New HashSet(Of String)
    Private bNotificacaoEventoIndisponivel As Boolean = False

    'Sino da barra de status: avisos guardados no banco até serem lidos (inclusive os que chegaram com o sistema fechado)
    'mais os chamados com mensagens não lidas no chat (última consulta das notificações)
    Private oClsFrmNotificacao As New clsFrmNotificacao
    Private oTabelaChatPendente As DataTable
    Private iQuantidadeSino As Integer = -1
    Private bSinoIndisponivel As Boolean = False
    'Avisos do banco já mostrados em toast nesta sessão e a última quantidade lida (aumento = aviso novo de fora, ex.: nota técnica)
    Private oNotificacoesSinoExibidas As New HashSet(Of Long)
    Private iQuantidadeBancoSino As Integer = -1
    Private bAbrirSinoAoMostrar As Boolean = False
    Private oFormNotificacao As frmNotificacao

    'Coleta das notas técnicas (Monitor Fiscal) em segundo plano: no máximo uma tentativa por hora nesta máquina;
    'a janela de 60 minutos entre coletas também vale entre máquinas (data da última coleta no banco)
    Private dProximaColetaNotas As Date = Date.MinValue
    Private bColetandoNotas As Boolean = False

    'Logo de fundo da aba principal (carregada uma vez; o My.Resources cria uma cópia a cada leitura)
    Private oLogoFundo As Image = My.Resources.logo_documentacti_fundo

    'Bolinha verde com check sobre o ícone da barra de tarefas (igual ao status do Teams) com apontamento em andamento
    Private oBarraTarefas As clsBarraTarefas
    Private oIconeOverlayApontamento As Icon
    Private bComApontamento As Boolean = False
    'Conferência periódica (apontamento encerrado pela web, chamado fechado, tela de chamados fechada etc.)
    Private WithEvents tmrIconeApontamento As New System.Windows.Forms.Timer
    Private Const gcIntervaloIconeApontamento As Integer = 30000 'milissegundos

    'Comando da Jump List recebido antes do login (ou na abertura): executado depois que o usuário entrar
    Private sComandoPendente As String = ""

#End Region

#Region "::: CONTROLES :::"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load

        Call ConfigurarFormulario()

    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            tmrNotificacao.Stop()
            tmrIconeApontamento.Stop()
            'Dispose fecha as notificações na hora (o CloseAll só inicia a animação de saída e mantém as janelas abertas)
            oToastManager.Dispose()

            If goUsuario.sUsuario <> "" Then
                Dim oClsFrmLogin As New clsFrmLogin
                oClsFrmLogin.InsertLog("logout")
            End If

        Catch
            'Falha ao registrar o logout não impede o fechamento
        End Try

    End Sub

    Private Sub pagPrincipal_Paint(sender As Object, e As PaintEventArgs) Handles pagPrincipal.Paint

        'Logo do Documentacti ao fundo da aba principal (o que aparece quando todas as telas estão fechadas)
        Try

            Dim oLogo As Image = oLogoFundo
            Dim oArea As Rectangle = pagPrincipal.ClientRectangle

            'Margem proporcional ao tamanho da janela: 14% nas laterais e 20% em cima/embaixo
            oArea.Inflate(-CInt(oArea.Width * 0.14), -CInt(oArea.Height * 0.2))
            If oArea.Width <= 0 OrElse oArea.Height <= 0 Then Exit Sub

            Dim dEscala As Double = Math.Min(oArea.Width / oLogo.Width, oArea.Height / oLogo.Height)
            Dim iLargura As Integer = CInt(oLogo.Width * dEscala)
            Dim iAltura As Integer = CInt(oLogo.Height * dEscala)

            e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            e.Graphics.DrawImage(oLogo, oArea.X + (oArea.Width - iLargura) \ 2, oArea.Y + (oArea.Height - iAltura) \ 2, iLargura, iAltura)

        Catch
            'Falha ao desenhar a logo não afeta o sistema
        End Try

    End Sub

    Private Sub pagPrincipal_Resize(sender As Object, e As EventArgs) Handles pagPrincipal.Resize

        pagPrincipal.Invalidate()

    End Sub

    Private Sub tabMain_TabClosed(ByVal sender As Object, ByVal e As Janus.Windows.UI.Tab.TabEventArgs) Handles tabMain.TabClosed

        Try

            tabMain.TabPages.Remove(e.Page)
            e.Page.Dispose()

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub tmrHora_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrHora.Tick

        stbMain.Panels("pnlDataHora").Text = Format(Now, "dd/MM/yyyy HH:mm:ss")

    End Sub

    Private Sub tmrNotificacao_Tick(sender As Object, e As EventArgs) Handles tmrNotificacao.Tick

        Try

            Call VerificarNotificacoes()
            Call AtualizarSino()
            Call ColetarNotasTecnicas()

        Catch ex As Exception
            'Falha de conexão: interrompe as notificações e avisa na barra de status (voltam ao trocar de usuário)
            tmrNotificacao.Stop()
            Informacao("Notificações do chat interrompidas: " & ex.Message, gColorInformacaoErro)
        End Try

    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        'Sistema aberto por um item da Jump List
        Call ExecutarComandoPendente()

        'Notificações não lidas no login: abre a lista do sino depois que a tela aparecer
        If bAbrirSinoAoMostrar Then
            bAbrirSinoAoMostrar = False
            BeginInvoke(New MethodInvoker(AddressOf AbrirSino))
        End If

    End Sub

    Private Sub tmrIconeApontamento_Tick(sender As Object, e As EventArgs) Handles tmrIconeApontamento.Tick

        Call VerificarIconeApontamento()

    End Sub

    Private Sub tmrInformacao_Tick(sender As Object, e As EventArgs) Handles tmrInformacao.Tick

        tmrInformacao.Stop()
        stbMain.Panels("pnlInfo").Text = ""

    End Sub

    Private Sub mnuChamados_Click(sender As Object, e As EventArgs) Handles mnuChamados.Click

        Try

            Call AbrirTela(New usrAdmChamado, "Chamados")

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub mnuEventos_Click(sender As Object, e As EventArgs) Handles mnuEventos.Click

        Try

            Call AbrirTela(New usrAdmEvento, "Calendário de Eventos")

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub mnuMonitorFiscal_Click(sender As Object, e As EventArgs) Handles mnuMonitorFiscal.Click

        Try

            Call AbrirTela(New usrAdmMonitorFiscal, "Monitor Fiscal")

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub stbMain_PanelClick(sender As Object, e As Janus.Windows.UI.StatusBar.StatusBarEventArgs) Handles stbMain.PanelClick

        Try

            If e.Panel.Key = "pnlNotificacao" Then Call AbrirSino()

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub mnuEmpresas_Click(sender As Object, e As EventArgs) Handles mnuEmpresas.Click

        Try

            Call AbrirTela(New usrCadEmpresa, "Empresas")

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub mnuUsuarios_Click(sender As Object, e As EventArgs) Handles mnuUsuarios.Click

        Try

            Call AbrirTela(New usrCadUsuario, "Usuários")

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub mnuAvisos_Click(sender As Object, e As EventArgs) Handles mnuAvisos.Click

        Try

            Call AbrirTela(New usrCadAviso, "Avisos")

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub mnuBaseConhecimento_Click(sender As Object, e As EventArgs) Handles mnuBaseConhecimento.Click

        Try

            Call AbrirTela(New usrCadBaseConhecimento, "Base de Conhecimento")

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub mnuRelApontamentos_Click(sender As Object, e As EventArgs) Handles mnuRelApontamentos.Click

        Try

            Call AbrirTela(New usrRelApontamento, "Relatório - Apontamentos")

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub mnuTrocarUsuario_Click(sender As Object, e As EventArgs) Handles mnuTrocarUsuario.Click

        Try

            If MsgBox("Deseja trocar de usuário? As telas abertas serão fechadas.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gAplicacao) = MsgBoxResult.No Then Exit Sub

            'Fecha telas abertas (exceto a principal)
            Call ClosePageButOne("pagPrincipal")

            tmrNotificacao.Stop()
            oToastManager.CloseAll()
            If IsNothing(oFormNotificacao) = False AndAlso oFormNotificacao.IsDisposed = False Then oFormNotificacao.Close()

            Dim oClsFrmLogin As New clsFrmLogin
            oClsFrmLogin.InsertLog("logout")

            goUsuario = Nothing

            tmrIconeApontamento.Stop()
            Call AtualizarIconeApontamento(False)

            If Login() = False Then
                Me.Close()
            Else
                Call IniciarNotificacoes()
                Call VerificarIconeApontamento()
                tmrIconeApontamento.Start()
                Call ExecutarComandoPendente()
            End If

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub mnuConfiguracao_Click(sender As Object, e As EventArgs) Handles mnuConfiguracao.Click

        Try

            Dim oForm As New frmConfiguracao
            oForm.ShowDialog(Me)

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub mnuAlterarSenha_Click(sender As Object, e As EventArgs) Handles mnuAlterarSenha.Click

        Try

            Dim oForm As New frmAlterarSenha
            oForm.ShowDialog(Me)

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub mnuVerificarVersao_Click(sender As Object, e As EventArgs) Handles mnuVerificarVersao.Click

        Try

            'Mesma verificação da abertura do sistema: se a versão do banco for diferente, troca o exe e reabre
            Dim oClsFrmMain As New clsFrmMain
            If oClsFrmMain.VerificaVersaoAplicativo(True) = True Then
                'Fecha pelo FormClosing (registra o logout); o processo oculto espera o fechamento para trocar o exe
                Me.Close()
            End If

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub mnuSair_Click(sender As Object, e As EventArgs) Handles mnuSair.Click

        Me.Close()

    End Sub

#End Region

#Region "::: FUNCTION / SUB :::"

    Public Sub ConfigurarFormulario()

        Try

            'Carrega configuração (XmlConfig\Config.xml)
            If oClsConfigXML.LoadXML() = False Then
                End
            End If

            'Testa conexão com o MySQL
            If oClsConfigXML.TesteConexao() = False Then
                End
            End If

            tmrInformacao.Interval = gcTempoMensagem
            tmrHora.Enabled = True

            'Autentica Usuário
            If Login() = False Then
                End
            End If

            'Verifica Versão do Aplicativo (se atualizou, o exe novo já foi aberto)
            Dim oClsFrmMain As New clsFrmMain
            If oClsFrmMain.VerificaVersaoAplicativo() = True Then
                End
            End If

            'Ícone da barra de tarefas conforme o apontamento em andamento do usuário
            Call ConfigurarIconeApontamento()

            'Abre a tela de chamados
            Call AbrirTela(New usrAdmChamado, "Chamados")

            Call IniciarNotificacoes()

            'Clique direito no ícone da barra de tarefas: Novo chamado / Abrir chamado...
            Call clsJumpList.Registrar()

            'Sistema aberto por um item da Jump List: executa quando a tela aparecer (frmMain_Shown)
            If sComandoPendente = "" Then sComandoPendente = clsJumpList.Comando(My.Application.CommandLineArgs)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gAplicacao)
            End
        End Try

    End Sub

    Private Function Login() As Boolean

        Try

            'Seta Retorno da Função
            Login = False

            Dim oForm As New frmLogin
            oForm.ShowDialog(Me)

            'Verifica se o Usuário autenticou no sistema
            If IsNothing(goUsuario.sUsuario) OrElse goUsuario.sUsuario = "" Then Exit Function

            stbMain.Panels("pnlUsuario").Text = goUsuario.sNome
            stbMain.Panels("pnlEmpresa").Text = goUsuario.sEmpresa & " | " & goDatabase.sServer & " / " & goDatabase.sDatabase
            Me.Text = gAplicacao & " - " & goUsuario.sNome & " - Versão: " & Application.ProductVersion.ToString

            'Relatórios: somente super_adm (coluna marcada manualmente no banco)
            mnuRelatorios.Visible = goUsuario.bSuperAdm

            'Seta Retorno da Função
            Login = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub AbrirTela(ByVal oUsrControl As UserControl, ByVal sTexto As String)

        Try

            Cursor.Current = Cursors.WaitCursor

            oUsrControl.Dock = DockStyle.Fill

            Call LoadPage(oUsrControl.Name, sTexto, oUsrControl)

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#Region "::: ÍCONE DE APONTAMENTO :::"

    Protected Overrides Sub WndProc(ByRef m As Message)

        MyBase.WndProc(m)

        'Botão da barra de tarefas criado/recriado (abertura da tela ou reinício do Explorer): reaplica a bolinha
        If m.Msg = clsBarraTarefas.iMensagemBotaoCriado AndAlso IsNothing(oBarraTarefas) = False Then
            Call AplicarOverlayApontamento()
        End If

    End Sub

    Private Sub ConfigurarIconeApontamento()

        Try

            oBarraTarefas = New clsBarraTarefas

            'Bolinha verde com check embutida no exe (Icon\documentacti_apontamento_overlay.ico), no tamanho de ícone pequeno do Windows
            Using oRecurso As IO.Stream = Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Documentacti.Icon.documentacti_apontamento_overlay.ico")
                If IsNothing(oRecurso) = False Then oIconeOverlayApontamento = New Icon(oRecurso, SystemInformation.SmallIconSize)
            End Using

            Call VerificarIconeApontamento()

            tmrIconeApontamento.Interval = gcIntervaloIconeApontamento
            tmrIconeApontamento.Start()

        Catch ex As Exception
            'Sem a bolinha o sistema segue com o ícone normal
            oIconeOverlayApontamento = Nothing
        End Try

    End Sub

    Private Sub VerificarIconeApontamento()

        Try

            If IsNothing(goUsuario) OrElse IsNothing(goUsuario.sUsuario) OrElse goUsuario.sUsuario = "" Then Exit Sub

            Dim oClsFrmMain As New clsFrmMain
            Call AtualizarIconeApontamento(oClsFrmMain.ExisteApontamentoAndamento())

        Catch
            'Falha de conexão: mantém o ícone atual e tenta de novo no próximo ciclo
        End Try

    End Sub

    Public Sub AtualizarIconeApontamento(ByVal bAndamento As Boolean)

        Try

            'Só troca quando muda (evita piscar a barra de tarefas)
            If bAndamento = bComApontamento Then Exit Sub

            bComApontamento = bAndamento
            Call AplicarOverlayApontamento()

        Catch
            'Bolinha é só visual: não interrompe o uso
        End Try

    End Sub

    Private Sub AplicarOverlayApontamento()

        Try

            If IsNothing(oBarraTarefas) OrElse IsNothing(oIconeOverlayApontamento) Then Exit Sub

            If bComApontamento = True Then
                oBarraTarefas.DefinirOverlay(Me.Handle, oIconeOverlayApontamento, "Apontamento em andamento")
            Else
                oBarraTarefas.DefinirOverlay(Me.Handle, Nothing, "")
            End If

        Catch
            'Bolinha é só visual: não interrompe o uso
        End Try

    End Sub

#End Region

#Region "::: JUMP LIST :::"

    Public Sub ExecutarComandoJumpList(ByVal oArgumentos As IEnumerable(Of String))

        'Chamado pelo ApplicationEvents (StartupNextInstance) quando um item da Jump List é clicado com o sistema aberto
        Try

            Dim sComando As String = clsJumpList.Comando(oArgumentos)
            If sComando = "" Then Exit Sub

            sComandoPendente = sComando

            'Ainda no login (abertura ou Trocar Usuário): executa depois que o usuário entrar
            If IsNothing(goUsuario) OrElse IsNothing(goUsuario.sUsuario) OrElse goUsuario.sUsuario = "" Then Exit Sub

            Call ExecutarComandoPendente()

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub ExecutarComandoPendente()

        Try

            If sComandoPendente = "" Then Exit Sub
            If IsNothing(goUsuario) OrElse IsNothing(goUsuario.sUsuario) OrElse goUsuario.sUsuario = "" Then Exit Sub

            Dim sComando As String = sComandoPendente
            sComandoPendente = ""

            'Traz o sistema para frente
            If WindowState = FormWindowState.Minimized Then WindowState = FormWindowState.Maximized
            Activate()

            'Outra janela aberta por cima (ex.: Configurações): não abre uma segunda
            If Me.CanFocus = False Then
                Informacao("Feche a janela aberta para usar o atalho da barra de tarefas.", gColorInformacaoErro)
                Exit Sub
            End If

            Select Case sComando

                Case clsJumpList.gcComandoNovoChamado

                    Call AbrirTela(New usrAdmChamado, "Chamados")

                    Dim oTela As usrAdmChamado = TelaChamados()
                    If Not IsNothing(oTela) Then oTela.NovoChamado()

                Case clsJumpList.gcComandoAbrirChamado

                    Dim oForm As New frmAbrirChamado
                    If oForm.ShowDialog(Me) = DialogResult.OK AndAlso oForm.lChamado > 0 Then
                        Call AbrirChamado(oForm.lChamado)
                    End If
                    oForm.Dispose()

            End Select

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

#End Region

#Region "::: NOTIFICAÇÕES :::"

    Private Sub IniciarNotificacoes()

        Try

            oMensagensNotificadas.Clear()
            oToastsChat.Clear()
            oChamadosNotificados.Clear()
            bNotificacaoChamadoIndisponivel = False
            oEventosNotificados.Clear()
            bNotificacaoEventoIndisponivel = False
            bNotificacaoIniciada = False
            oTabelaChatPendente = Nothing
            iQuantidadeSino = -1
            bSinoIndisponivel = False
            oNotificacoesSinoExibidas.Clear()
            iQuantidadeBancoSino = -1

            oToastManager.AppCaption = "Documentacti"
            oToastManager.MaxVisible = 4

            'Silenciar (Configurações) vale só até sair do sistema: a cada login as notificações são religadas,
            'para não ficarem desligadas por esquecimento
            Dim oClsConfiguracao As New clsFrmConfiguracao
            oClsConfiguracao.Update(False)
            bSilenciarNotificacao = False

            Call VerificarNotificacoes()
            Call AtualizarSino()

            'Já há notificações não lidas (sistema fechado / máquina desligada / sessão anterior): abre o sistema com a lista do sino aberta.
            'Na abertura do sistema a tela ainda não apareceu: abre no frmMain_Shown
            If iQuantidadeSino > 0 Then
                If Me.Visible Then
                    BeginInvoke(New MethodInvoker(AddressOf AbrirSino))
                Else
                    bAbrirSinoAoMostrar = True
                End If
            End If

            tmrNotificacao.Interval = gcIntervaloNotificacao
            tmrNotificacao.Start()

            'Notas técnicas: confere os portais oficiais logo após o login (em segundo plano)
            Call ColetarNotasTecnicas()

        Catch ex As Exception
            Informacao("Notificações do chat indisponíveis: " & ex.Message, gColorInformacaoErro)
        End Try

    End Sub

    Private Sub VerificarNotificacoes()

        Try

            Call VerificarChamadosSemResponsavel()
            Call VerificarEventosDesignados()

            Dim oClsUsrAdmChamado As New clsUsrAdmChamado
            Dim oTabela As DataTable = oClsUsrAdmChamado.LoadChatNotificacao()
            Dim oNovas As New List(Of DataRow)

            'Somente o chat do chamado (canal 1); o chat interno (DEV) não é mais usado
            For Each oRow As DataRow In oTabela.Select("canal <> 1")
                oTabela.Rows.Remove(oRow)
            Next

            'Sino: chamados com mensagens não lidas (saem sozinhos quando a conversa é lida)
            oTabelaChatPendente = oTabela.Copy()

            For Each oRow As DataRow In oTabela.Rows
                Dim sChave As String = oRow.Item("codigo_atividade") & "|" & oRow.Item("canal") & "|" & oRow.Item("codigo_mensagem")
                If oMensagensNotificadas.Add(sChave) AndAlso bNotificacaoIniciada Then oNovas.Add(oRow)
            Next

            'Primeira consulta após o login: somente um resumo do que já estava pendente
            If bNotificacaoIniciada = False Then

                bNotificacaoIniciada = True

                If oTabela.Rows.Count > 0 AndAlso bSilenciarNotificacao = False Then
                    Dim iChamados As Integer = oTabela.DefaultView.ToTable(True, "codigo_atividade").Rows.Count
                    Dim oToast As New Toast("Mensagens não lidas", _
                                            oTabela.Rows.Count & " mensagem(ns) não lida(s) no chat de " & iChamados & " chamado(s).", _
                                            ToastType.Info) With {.Caption = "Chat dos chamados", .Duration = 8000}
                    If iChamados = 1 Then
                        Dim lChamado As Long = CLng(oTabela.Rows(0).Item("codigo_atividade"))
                        Dim iCanal As Integer = CInt(oTabela.Rows(0).Item("canal"))
                        AddHandler oToast.Click, Sub(s, a) AbrirChatChamado(lChamado, iCanal)
                    End If
                    oToastManager.Show(oToast)
                End If

                Exit Sub

            End If

            'Silenciado: as mensagens continuam sendo registradas como avisadas (não acumulam para depois)
            If bSilenciarNotificacao Then Exit Sub

            For Each oRow As DataRow In oNovas
                Call NotificarMensagem(oRow)
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub VerificarChamadosSemResponsavel()

        'Falha aqui (ex.: banco ainda sem a proc) não pode derrubar as notificações do chat
        If bNotificacaoChamadoIndisponivel Then Exit Sub

        Try

            Dim oClsUsrAdmChamado As New clsUsrAdmChamado
            Dim oTabela As DataTable = oClsUsrAdmChamado.LoadChamadoNotificacao()
            Dim oNovos As New List(Of DataRow)

            For Each oRow As DataRow In oTabela.Rows
                If oChamadosNotificados.Add(CLng(oRow.Item("codigo_atividade"))) Then oNovos.Add(oRow)
            Next

            If oNovos.Count = 0 Then Exit Sub

            'Exibidas: não voltam a aparecer (nem no próximo login) - ficam no sino até serem lidas
            Dim oCodigosSino As New List(Of Long)
            For Each oRow As DataRow In oNovos
                oCodigosSino.Add(RegistrarSino("CHAMADO", CLng(oRow.Item("codigo_atividade")), _
                                               "Novo chamado sem responsável #" & oRow.Item("codigo_atividade").ToString, _
                                               oRow.Item("titulo").ToString & " - " & oRow.Item("empresa").ToString & _
                                               IIf(oRow.Item("criador").ToString = "", "", " - por " & oRow.Item("criador").ToString).ToString, _
                                               Nothing))
                oClsUsrAdmChamado.UpdateChamadoNotificacao(CLng(oRow.Item("codigo_atividade")))
            Next

            'Muitos pendentes (ex.: ao entrar no sistema): um resumo em vez de uma notificação por chamado
            If oNovos.Count > 3 Then
                Dim oToast As New Toast("Chamados sem responsável", _
                                        oNovos.Count & " chamado(s) do seu time aguardando responsável." & vbCrLf & "OK marca todos como lidos.", _
                                        ToastType.Warning) With {.Caption = "Chamados", .Duration = 10000}
                Dim lPrimeiro As Long = CLng(oNovos(0).Item("codigo_atividade"))
                AddHandler oToast.Click, Sub(s, a) AbrirChamado(lPrimeiro)
                Call ConfigurarOkSino(oToast, oCodigosSino, False)
                oToastManager.Show(oToast)
            Else
                For i As Integer = 0 To oNovos.Count - 1
                    Dim oRow As DataRow = oNovos(i)
                    Dim lChamado As Long = CLng(oRow.Item("codigo_atividade"))
                    Dim oToast As New Toast("Novo chamado sem responsável", _
                                            "#" & lChamado & " - " & oRow.Item("titulo").ToString & vbCrLf & _
                                            oRow.Item("empresa").ToString & IIf(oRow.Item("criador").ToString = "", "", "  ·  por " & oRow.Item("criador").ToString), _
                                            ToastType.Warning) With {.Caption = "Chamado #" & lChamado, .Duration = 10000}
                    AddHandler oToast.Click, Sub(s, a) AbrirChamado(lChamado)
                    Call ConfigurarOkSino(oToast, New List(Of Long) From {oCodigosSino(i)}, True)
                    oToastManager.Show(oToast)
                Next
            End If

        Catch ex As Exception
            bNotificacaoChamadoIndisponivel = True
            Informacao("Notificações de chamados sem responsável indisponíveis: " & ex.Message, gColorInformacaoErro)
        End Try

    End Sub

    Private Sub VerificarEventosDesignados()

        'Falha aqui (ex.: banco ainda sem a proc) não pode derrubar as notificações do chat
        If bNotificacaoEventoIndisponivel Then Exit Sub

        Try

            Dim oClsUsrAdmEvento As New clsUsrAdmEvento
            Dim oTabela As DataTable = oClsUsrAdmEvento.LoadNotificacao()
            Dim oNovos As New List(Of DataRow)

            'Um mesmo evento pode avisar mais de uma vez (inclusão, depois alteração, depois exclusão)
            For Each oRow As DataRow In oTabela.Rows
                Dim sChave As String = oRow.Item("codigo").ToString & "|" & oRow.Item("tipo_notificacao").ToString & "|" & oRow.Item("data_notificacao").ToString
                If oEventosNotificados.Add(sChave) Then oNovos.Add(oRow)
            Next

            If oNovos.Count = 0 Then Exit Sub

            'Avisados (ou silenciados): não voltam a aparecer (nem no próximo login) - ficam no sino até serem lidos
            Dim oCodigosSino As New List(Of Long)
            For Each oRow As DataRow In oNovos
                Dim sTitulo As String
                Select Case oRow.Item("tipo_notificacao").ToString
                    Case "ALTERACAO" : sTitulo = "Evento alterado: "
                    Case "EXCLUSAO" : sTitulo = "Evento excluído: "
                    Case Else : sTitulo = "Você foi incluído no evento: "
                End Select
                oCodigosSino.Add(RegistrarSino("EVENTO", CLng(oRow.Item("codigo")), sTitulo & oRow.Item("titulo").ToString, _
                                               IIf(oRow.Item("detalhe_notificacao").ToString = "", _
                                                   oRow.Item("tipo_evento").ToString & " - " & Format(CDate(oRow.Item("data_inicio")), "dd/MM/yyyy HH:mm"), _
                                                   oRow.Item("detalhe_notificacao").ToString).ToString & _
                                               IIf(oRow.Item("criador").ToString = "", "", " - por " & oRow.Item("criador").ToString).ToString, _
                                               CDate(oRow.Item("data_inicio"))))
                oClsUsrAdmEvento.UpdateNotificacao(CLng(oRow.Item("codigo")))
            Next

            If bSilenciarNotificacao = False Then

                'Muitos pendentes (ex.: ao entrar no sistema): um resumo em vez de uma notificação por aviso
                If oNovos.Count > 3 Then
                    Dim oToast As New Toast("Calendário de eventos", _
                                            "Você tem " & oNovos.Count & " aviso(s) de eventos (inclusões, alterações ou exclusões)." & vbCrLf & "OK marca todos como lidos.", _
                                            ToastType.Info) With {.Caption = "Calendário de Eventos", .Duration = 10000}
                    Dim dPrimeiro As Date = CDate(oNovos(0).Item("data_inicio"))
                    AddHandler oToast.Click, Sub(s, a) AbrirEvento(0, dPrimeiro, False)
                    Call ConfigurarOkSino(oToast, oCodigosSino, False)
                    oToastManager.Show(oToast)
                Else
                    For i As Integer = 0 To oNovos.Count - 1
                        Call NotificarEvento(oNovos(i), oCodigosSino(i))
                    Next
                End If

            End If

        Catch ex As Exception
            bNotificacaoEventoIndisponivel = True
            Informacao("Notificações do calendário de eventos indisponíveis: " & ex.Message, gColorInformacaoErro)
        End Try

    End Sub

    Private Sub NotificarEvento(ByVal oRow As DataRow, ByVal lCodigoSino As Long)

        Try

            Dim lEvento As Long = CLng(oRow.Item("codigo"))
            Dim dData As Date = CDate(oRow.Item("data_inicio"))
            Dim sQuando As String = Format(dData, IIf(CInt(oRow.Item("dia_inteiro")) = 1, "dd/MM/yyyy", "dd/MM/yyyy HH:mm"))
            Dim sPor As String = IIf(oRow.Item("criador").ToString = "", "", "  ·  por " & oRow.Item("criador").ToString)
            Dim sDetalhe As String = oRow.Item("detalhe_notificacao").ToString.Replace(" | ", vbCrLf)
            Dim oToast As Toast

            Select Case oRow.Item("tipo_notificacao").ToString

                Case "ALTERACAO"
                    'Título / data / horário mudou: o detalhe mostra de -> para
                    oToast = New Toast("Evento alterado", _
                                       oRow.Item("titulo").ToString & vbCrLf & sDetalhe & sPor, _
                                       ToastType.Warning) With {.Caption = "Calendário de Eventos", .Duration = 12000}
                    AddHandler oToast.Click, Sub(s, a) AbrirEvento(lEvento, dData, True)

                Case "EXCLUSAO"
                    'Evento excluído: o clique só abre o calendário no mês em que ele estava
                    oToast = New Toast("Evento excluído", _
                                       oRow.Item("titulo").ToString & vbCrLf & sDetalhe & sPor, _
                                       ToastType.Error) With {.Caption = "Calendário de Eventos", .Duration = 12000}
                    AddHandler oToast.Click, Sub(s, a) AbrirEvento(lEvento, dData, False)

                Case Else
                    oToast = New Toast("Você foi incluído em um evento", _
                                       oRow.Item("titulo").ToString & vbCrLf & _
                                       oRow.Item("tipo_evento").ToString & "  ·  " & sQuando & sPor, _
                                       ToastType.Info) With {.Caption = "Calendário de Eventos", .Duration = 10000}
                    AddHandler oToast.Click, Sub(s, a) AbrirEvento(lEvento, dData, True)

            End Select

            Call ConfigurarOkSino(oToast, New List(Of Long) From {lCodigoSino}, True)
            oToastManager.Show(oToast)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AbrirEvento(ByVal lEvento As Long, ByVal dData As Date, ByVal bAbrirCadastro As Boolean)

        'Clique na notificação do calendário: traz o sistema para frente e abre o mês (e o evento, quando ainda existe)
        Try

            If WindowState = FormWindowState.Minimized Then WindowState = FormWindowState.Maximized
            Activate()

            Call AbrirTela(New usrAdmEvento, "Calendário de Eventos")

            If tabMain.TabPages.Contains("usrAdmEvento") = False Then Exit Sub

            For Each oControl As Control In tabMain.TabPages("usrAdmEvento").Controls
                If TypeOf oControl Is usrAdmEvento Then CType(oControl, usrAdmEvento).AbrirEvento(lEvento, dData, bAbrirCadastro)
            Next

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub NotificarMensagem(ByVal oRow As DataRow)

        Try

            Dim lChamado As Long = CLng(oRow.Item("codigo_atividade"))
            Dim iCanal As Integer = CInt(oRow.Item("canal"))

            'Conversa já está na tela: não notifica
            If ChatVisivel(lChamado, iCanal) Then Exit Sub

            Dim sChave As String = lChamado & "|" & iCanal
            Dim sTexto As String = oRow.Item("mensagem").ToString
            If sTexto.Trim = "" Then sTexto = "Arquivo: " & oRow.Item("nome_arquivo").ToString
            Dim sCaption As String = "Chamado #" & lChamado & "  ·  Chat"

            'Uma notificação por conversa: mensagens seguidas atualizam a mesma
            'Tag = última mensagem avisada (o OK marca a conversa como lida até ela)
            Dim oToast As Toast = Nothing
            If oToastsChat.TryGetValue(sChave, oToast) AndAlso oToast.IsOpen Then
                oToast.Title = oRow.Item("nome_usuario").ToString
                oToast.Message = sTexto
                oToast.Caption = sCaption
                oToast.Tag = CInt(oRow.Item("codigo_mensagem"))
                oToast.RestartTimer()
                Exit Sub
            End If

            oToast = New Toast(oRow.Item("nome_usuario").ToString, sTexto, ToastType.Message) With {.Caption = sCaption, .Duration = 8000, .ShowOkButton = True}
            oToast.Tag = CInt(oRow.Item("codigo_mensagem"))
            AddHandler oToast.Click, Sub(s, a) AbrirChatChamado(lChamado, iCanal)
            AddHandler oToast.OkClick, Sub(s, a) MarcarChatLido(lChamado, iCanal, CInt(a.Toast.Tag))

            oToastsChat(sChave) = oToast
            oToastManager.Show(oToast)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub DefinirSilenciarNotificacao(ByVal bSilenciar As Boolean)

        'Chamado pela tela de Configurações ao salvar
        bSilenciarNotificacao = bSilenciar

        If bSilenciar Then oToastManager.CloseAll()

    End Sub

    Private Function TelaChamados() As usrAdmChamado

        If tabMain.TabPages.Contains("usrAdmChamado") = False Then Return Nothing

        For Each oControl As Control In tabMain.TabPages("usrAdmChamado").Controls
            If TypeOf oControl Is usrAdmChamado Then Return CType(oControl, usrAdmChamado)
        Next

        Return Nothing

    End Function

    Private Function ChatVisivel(ByVal lChamado As Long, ByVal iCanal As Integer) As Boolean

        Dim oTela As usrAdmChamado = TelaChamados()

        Return Not IsNothing(oTela) AndAlso Form.ActiveForm Is Me AndAlso WindowState <> FormWindowState.Minimized AndAlso _
               tabMain.SelectedTab Is tabMain.TabPages("usrAdmChamado") AndAlso oTela.ChatVisivel(lChamado, iCanal)

    End Function

    Private Sub AbrirChatChamado(ByVal lChamado As Long, ByVal iCanal As Integer)

        'Clique na notificação: traz o sistema para frente e abre o chamado na aba do chat
        Try

            If WindowState = FormWindowState.Minimized Then WindowState = FormWindowState.Maximized
            Activate()

            Call AbrirTela(New usrAdmChamado, "Chamados")

            Dim oTela As usrAdmChamado = TelaChamados()
            If Not IsNothing(oTela) Then oTela.AbrirChat(lChamado, iCanal)

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub AbrirChamado(ByVal lChamado As Long)

        'Clique na notificação de chamado sem responsável: traz o sistema para frente e abre o chamado
        Try

            If WindowState = FormWindowState.Minimized Then WindowState = FormWindowState.Maximized
            Activate()

            Call AbrirTela(New usrAdmChamado, "Chamados")

            Dim oTela As usrAdmChamado = TelaChamados()
            If Not IsNothing(oTela) Then oTela.AbrirChamado(lChamado)

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

#End Region

#Region "::: SINO / MONITOR FISCAL :::"

    Private Function RegistrarSino(ByVal sTipo As String, _
                                   ByVal lCodigoReferencia As Long, _
                                   ByVal sTitulo As String, _
                                   ByVal sMensagem As String, _
                                   ByVal oDataReferencia As Object) As Long

        'Guarda o aviso no sino (fica até o usuário ler) e retorna o código gravado (0 = sem sino).
        'Banco ainda sem a tabela: segue só com os toasts
        If bSinoIndisponivel Then Return 0

        Try

            Return oClsFrmNotificacao.Insert(sTipo, lCodigoReferencia, sTitulo, sMensagem, oDataReferencia)

        Catch ex As Exception
            bSinoIndisponivel = True
            Informacao("Sino de notificações indisponível: " & ex.Message, gColorInformacaoErro)
            Return 0
        End Try

    End Function

    Private Sub ConfigurarOkSino(ByVal oToast As Toast, ByVal oCodigos As List(Of Long), ByVal bMarcarAoClicar As Boolean)

        'Botão OK (check) do toast: marca como lido no sino. Clique no corpo (abre o item) também marca, quando é um item só
        Dim oValidos As List(Of Long) = oCodigos.Where(Function(l) l > 0).ToList()
        If oValidos.Count = 0 Then Exit Sub

        For Each lCodigo As Long In oValidos
            oNotificacoesSinoExibidas.Add(lCodigo)
        Next

        oToast.ShowOkButton = True
        AddHandler oToast.OkClick, Sub(s, a) MarcarSinoLido(oValidos)
        If bMarcarAoClicar Then AddHandler oToast.Click, Sub(s, a) MarcarSinoLido(oValidos)

    End Sub

    Private Sub MarcarSinoLido(ByVal oCodigos As List(Of Long))

        Try

            For Each lCodigo As Long In oCodigos
                oClsFrmNotificacao.UpdateLida(lCodigo)
            Next

            Call AtualizarSino()

        Catch ex As Exception
            Informacao("Não foi possível marcar a notificação como lida: " & ex.Message, gColorInformacaoErro)
        End Try

    End Sub

    Public Sub MarcarChatLido(ByVal lChamado As Long, ByVal iCanal As Integer, ByVal iMensagem As Integer)

        'OK no toast do chat: conversa lida até a última mensagem avisada (sai do sino na hora)
        Try

            Dim oClsUsrAdmChamado As New clsUsrAdmChamado
            oClsUsrAdmChamado.UpdateChatLeitura(lChamado, iCanal, iMensagem)

            If IsNothing(oTabelaChatPendente) = False Then
                For Each oRow As DataRow In oTabelaChatPendente.Select("codigo_atividade = " & lChamado & " AND canal = " & iCanal & " AND codigo_mensagem <= " & iMensagem)
                    oTabelaChatPendente.Rows.Remove(oRow)
                Next
            End If

            Call AtualizarSino()

        Catch ex As Exception
            Informacao("Não foi possível marcar o chat como lido: " & ex.Message, gColorInformacaoErro)
        End Try

    End Sub

    Private Sub NotificarNovasDoSino()

        'Avisos gravados direto no banco por outra máquina (nota técnica nova do Monitor Fiscal): também viram toast com OK
        Try

            Dim oTabela As DataTable = oClsFrmNotificacao.LoadGrid()
            Dim oNovas As New List(Of DataRow)

            For Each oRow As DataRow In oTabela.Select("lida = 0 AND tipo = 'NOTA_TECNICA'")
                If oNotificacoesSinoExibidas.Add(CLng(oRow.Item("codigo"))) Then oNovas.Add(oRow)
            Next

            If oNovas.Count = 0 OrElse bSilenciarNotificacao Then Exit Sub

            If oNovas.Count > 3 Then
                Dim oCodigos As List(Of Long) = oNovas.Select(Function(r) CLng(r.Item("codigo"))).ToList()
                Dim oToast As New Toast("Notas técnicas", _
                                        oNovas.Count & " nota(s) técnica(s) nova(s) ou atualizada(s) nos portais oficiais." & vbCrLf & "OK marca todas como lidas.", _
                                        ToastType.Info) With {.Caption = "Monitor Fiscal", .Duration = 12000}
                AddHandler oToast.Click, Sub(s, a) AbrirMonitorFiscal(0)
                Call ConfigurarOkSino(oToast, oCodigos, False)
                oToastManager.Show(oToast)
            Else
                For Each oRow As DataRow In oNovas
                    Dim lNota As Long = CLng(oRow.Item("codigo_referencia"))
                    Dim oToast As New Toast(oRow.Item("titulo").ToString, oRow.Item("mensagem").ToString, ToastType.Info) _
                                            With {.Caption = "Monitor Fiscal", .Duration = 12000}
                    AddHandler oToast.Click, Sub(s, a) AbrirMonitorFiscal(lNota)
                    Call ConfigurarOkSino(oToast, New List(Of Long) From {CLng(oRow.Item("codigo"))}, True)
                    oToastManager.Show(oToast)
                Next
            End If

        Catch
            'Toast é complemento: o aviso continua no sino
        End Try

    End Sub

    Private Function QuantidadeSinoBanco() As Integer

        If bSinoIndisponivel Then Return 0

        Try

            Return oClsFrmNotificacao.Quantidade()

        Catch ex As Exception
            bSinoIndisponivel = True
            Informacao("Sino de notificações indisponível: " & ex.Message, gColorInformacaoErro)
            Return 0
        End Try

    End Function

    Private Function QuantidadeChatPendente() As Integer

        'Um item por chamado (não por mensagem)
        If IsNothing(oTabelaChatPendente) OrElse oTabelaChatPendente.Rows.Count = 0 Then Return 0

        Return oTabelaChatPendente.DefaultView.ToTable(True, "codigo_atividade").Rows.Count

    End Function

    Public Sub AtualizarSino()

        Try

            If IsNothing(goUsuario) OrElse IsNothing(goUsuario.sUsuario) OrElse goUsuario.sUsuario = "" Then Exit Sub

            Dim iQuantidadeBanco As Integer = QuantidadeSinoBanco()

            'Primeira leitura após o login: o que já estava pendente fica só no sino (o toast de pendências avisa);
            'depois, aumento no banco = aviso novo vindo de fora (nota técnica) e vira toast
            If iQuantidadeBancoSino < 0 Then
                If iQuantidadeBanco > 0 Then
                    For Each oRow As DataRow In oClsFrmNotificacao.LoadGrid().Select("lida = 0")
                        oNotificacoesSinoExibidas.Add(CLng(oRow.Item("codigo")))
                    Next
                End If
            ElseIf iQuantidadeBanco > iQuantidadeBancoSino Then
                Call NotificarNovasDoSino()
            End If
            iQuantidadeBancoSino = iQuantidadeBanco

            Dim iQuantidade As Integer = iQuantidadeBanco + QuantidadeChatPendente()

            'Só redesenha quando muda
            If iQuantidade = iQuantidadeSino Then Exit Sub
            iQuantidadeSino = iQuantidade

            Dim oPanel As Janus.Windows.UI.StatusBar.UIStatusBarPanel = stbMain.Panels("pnlNotificacao")
            Dim oImagemAnterior As Image = oPanel.Image

            oPanel.Image = DesenharSino(iQuantidade)

            'Contador ao lado do sino, em texto (o painel cresce conforme o número)
            oPanel.Text = IIf(iQuantidade = 0, "", IIf(iQuantidade > 99, "99+", iQuantidade.ToString)).ToString
            oPanel.FormatStyle.ForeColor = Color.FromArgb(200, 30, 30)
            oPanel.FormatStyle.FontBold = Janus.Windows.UI.TriState.True

            oPanel.ToolTipText = IIf(iQuantidade = 0, "Notificações: nenhuma pendente", "Notificações: " & iQuantidade & " não lida(s) - clique para ver").ToString

            If IsNothing(oImagemAnterior) = False Then oImagemAnterior.Dispose()

        Catch
            'Sino é só visual: não interrompe as notificações
        End Try

    End Sub

    Private Function DesenharSino(ByVal iQuantidade As Integer) As Image

        'Ícone do sino desenhado na hora em 16x16: a barra de status do Janus redimensiona qualquer imagem para esse
        'tamanho (ImageSize), por isso o contador vai como texto do painel, ao lado; aqui só o pontinho vermelho de pendência
        Dim oImagem As New Bitmap(16, 16)

        Using oGraphics As Graphics = Graphics.FromImage(oImagem)

            oGraphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            oGraphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
            oGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit

            'Mesmo cinza do texto da barra; mais claro quando não há nada pendente
            Dim oCor As Color = IIf(iQuantidade > 0, Color.FromArgb(70, 70, 70), Color.FromArgb(125, 125, 125))

            Using oBrush As New SolidBrush(oCor)

                'Corpo: ombros arredondados, lados quase retos e a boca abrindo na base
                Using oPath As New Drawing2D.GraphicsPath
                    oPath.AddBezier(8.0!, 1.8!, 4.6!, 1.8!, 3.4!, 4.4!, 3.4!, 7.4!)
                    oPath.AddLine(3.4!, 7.4!, 3.4!, 9.8!)
                    oPath.AddBezier(3.4!, 9.8!, 3.4!, 11.0!, 2.2!, 11.5!, 1.6!, 12.4!)
                    oPath.AddLine(1.6!, 12.4!, 14.4!, 12.4!)
                    oPath.AddBezier(14.4!, 12.4!, 13.8!, 11.5!, 12.6!, 11.0!, 12.6!, 9.8!)
                    oPath.AddLine(12.6!, 9.8!, 12.6!, 7.4!)
                    oPath.AddBezier(12.6!, 7.4!, 12.6!, 4.4!, 11.4!, 1.8!, 8.0!, 1.8!)
                    oPath.CloseFigure()
                    oGraphics.FillPath(oBrush, oPath)
                End Using

                'Pino no topo e badalo embaixo
                oGraphics.FillEllipse(oBrush, 6.8!, 0.4!, 2.4!, 2.4!)
                oGraphics.FillEllipse(oBrush, 6.3!, 12.6!, 3.4!, 3.0!)

            End Using

            If iQuantidade > 0 Then

                'Pontinho vermelho no canto superior direito, com contorno branco separando do sino
                Using oBorda As New SolidBrush(Color.White)
                    oGraphics.FillEllipse(oBorda, 9.5!, 0.0!, 6.5!, 6.5!)
                End Using
                Using oBrush As New SolidBrush(Color.FromArgb(214, 40, 40))
                    oGraphics.FillEllipse(oBrush, 10.5!, 1.0!, 4.5!, 4.5!)
                End Using

            End If

        End Using

        Return oImagem

    End Function

    Public Sub AbrirSino()

        'Lista do sino logo acima do ícone: avisos do banco + chamados com mensagens não lidas no chat
        Try

            If IsNothing(goUsuario) OrElse IsNothing(goUsuario.sUsuario) OrElse goUsuario.sUsuario = "" Then Exit Sub

            If WindowState = FormWindowState.Minimized Then WindowState = FormWindowState.Maximized
            Activate()

            If IsNothing(oFormNotificacao) = False AndAlso oFormNotificacao.IsDisposed = False Then oFormNotificacao.Close()

            Cursor.Current = Cursors.WaitCursor

            Dim oTabela As DataTable

            If bSinoIndisponivel Then
                oTabela = New DataTable
                For Each sColuna As String In New String() {"codigo", "tipo", "origem", "codigo_referencia", "titulo", "mensagem", "data_referencia", "data_notificacao", "lida", "situacao", "data_leitura"}
                    oTabela.Columns.Add(sColuna)
                Next
            Else
                oTabela = oClsFrmNotificacao.LoadGrid()
            End If

            If oTabela.Columns.Contains("canal") = False Then oTabela.Columns.Add("canal", GetType(Integer))
            If oTabela.Columns.Contains("codigo_mensagem") = False Then oTabela.Columns.Add("codigo_mensagem", GetType(Integer))

            Call AdicionarChatPendente(oTabela)

            oFormNotificacao = New frmNotificacao
            oFormNotificacao.oTabela = oTabela

            'Canto inferior esquerdo, logo acima do sino (dentro da área de trabalho da tela)
            Dim oPanel As Janus.Windows.UI.StatusBar.UIStatusBarPanel = stbMain.Panels("pnlNotificacao")
            Dim oPonto As Point = stbMain.PointToScreen(New Point(oPanel.Bounds.Left, 0))
            Dim oArea As Rectangle = Screen.FromControl(Me).WorkingArea

            oFormNotificacao.Location = New Point(Math.Max(oArea.Left, Math.Min(oPonto.X, oArea.Right - oFormNotificacao.Width)), _
                                                  Math.Max(oArea.Top, oPonto.Y - oFormNotificacao.Height))

            Cursor.Current = Cursors.Default

            oFormNotificacao.Show(Me)

        Catch ex As Exception
            Cursor.Current = Cursors.Default
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub AdicionarChatPendente(ByVal oTabela As DataTable)

        'Uma linha por chamado: quantidade de mensagens não lidas e a última delas
        If IsNothing(oTabelaChatPendente) OrElse oTabelaChatPendente.Rows.Count = 0 Then Exit Sub

        Dim iPosicao As Integer = 0

        For Each oChamado As DataRow In oTabelaChatPendente.DefaultView.ToTable(True, "codigo_atividade", "canal").Rows

            Dim oMensagens() As DataRow = oTabelaChatPendente.Select("codigo_atividade = " & oChamado.Item("codigo_atividade") & " AND canal = " & oChamado.Item("canal"), "codigo_mensagem DESC")
            Dim oUltima As DataRow = oMensagens(0)
            Dim sTexto As String = oUltima.Item("mensagem").ToString
            If sTexto.Trim = "" Then sTexto = "Arquivo: " & oUltima.Item("nome_arquivo").ToString

            Dim oNova As DataRow = oTabela.NewRow()
            oNova.Item("codigo") = 0
            oNova.Item("tipo") = "CHAT"
            oNova.Item("origem") = "CHAT"
            oNova.Item("codigo_referencia") = oChamado.Item("codigo_atividade")
            oNova.Item("titulo") = "Chamado #" & oChamado.Item("codigo_atividade").ToString & " - " & oMensagens.Length & " mensagem(ns) não lida(s)"
            oNova.Item("mensagem") = oUltima.Item("nome_usuario").ToString & ": " & sTexto
            oNova.Item("data_notificacao") = oUltima.Item("data_hora")
            oNova.Item("lida") = 0
            oNova.Item("situacao") = "NÃO LIDA"
            oNova.Item("canal") = oChamado.Item("canal")
            oNova.Item("codigo_mensagem") = oUltima.Item("codigo_mensagem")

            'Chat no topo da lista
            oTabela.Rows.InsertAt(oNova, iPosicao)
            iPosicao += 1

        Next

    End Sub

    Public Sub AbrirNotificacao(ByVal sTipo As String, _
                                ByVal lCodigoReferencia As Long, _
                                ByVal oDataReferencia As Object, _
                                ByVal iCanal As Integer)

        'Duplo clique numa notificação do sino
        Try

            Select Case sTipo

                Case "CHAT"
                    Call AbrirChatChamado(lCodigoReferencia, iCanal)

                Case "CHAMADO"
                    Call AbrirChamado(lCodigoReferencia)

                Case "EVENTO"
                    'Evento excluído: o calendário abre no mês e avisa que o evento não foi encontrado
                    Call AbrirEvento(lCodigoReferencia, IIf(IsDBNull(oDataReferencia) OrElse IsNothing(oDataReferencia), Today, oDataReferencia), True)

                Case "NOTA_TECNICA"
                    Call AbrirMonitorFiscal(lCodigoReferencia)

            End Select

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub AbrirMonitorFiscal(ByVal lNotaTecnica As Long)

        Try

            If WindowState = FormWindowState.Minimized Then WindowState = FormWindowState.Maximized
            Activate()

            Call AbrirTela(New usrAdmMonitorFiscal, "Monitor Fiscal")

            If tabMain.TabPages.Contains("usrAdmMonitorFiscal") = False Then Exit Sub

            For Each oControl As Control In tabMain.TabPages("usrAdmMonitorFiscal").Controls
                If TypeOf oControl Is usrAdmMonitorFiscal Then CType(oControl, usrAdmMonitorFiscal).SelecionarNotaTecnica(lNotaTecnica)
            Next

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Async Sub ColetarNotasTecnicas()

        'Notas técnicas novas viram aviso no sino de todos, mesmo sem ninguém abrir o Monitor Fiscal.
        'Uma tentativa por hora nesta máquina; falha (portal fora, sem internet, banco sem a proc) só espera a próxima
        If bColetandoNotas OrElse Now < dProximaColetaNotas Then Exit Sub
        If IsNothing(goUsuario) OrElse IsNothing(goUsuario.sUsuario) OrElse goUsuario.sUsuario = "" Then Exit Sub

        bColetandoNotas = True
        dProximaColetaNotas = Now.AddMinutes(clsMonitorFiscal.gcIntervaloColetaMinutos)

        Try

            Dim oClsUsrAdmMonitorFiscal As New clsUsrAdmMonitorFiscal
            Dim bColetou As Boolean = Await System.Threading.Tasks.Task.Run(Function() oClsUsrAdmMonitorFiscal.AtualizarNotasTecnicas(False))

            If bColetou Then Call AtualizarSino()

        Catch
            'Sem aviso na tela: a coleta é de fundo e a tela do Monitor Fiscal mostra o erro quando aberta
        Finally
            bColetandoNotas = False
        End Try

    End Sub

#End Region

    Public Sub LoadPage(ByVal pStrName As String, ByVal pStrText As String, ByVal pUsrControl As UserControl)

        Try

            'Suspende Layout
            tabMain.SuspendLayout()

            'Verifica se a página já está aberta
            If tabMain.TabPages.Contains(pStrName) = True Then

                'Seleciona a página
                tabMain.SelectedTab = tabMain.TabPages(pStrName)
                pUsrControl.Dispose()

            Else

                'Cria nova página
                Dim oTabPage As New UITabPage(pStrText)
                oTabPage.Name = pStrName
                oTabPage.Key = pStrName
                oTabPage.Tag = pUsrControl.Tag
                oTabPage.Padding = New Padding(8)
                oTabPage.Controls.Add(pUsrControl)

                'Adiciona página
                tabMain.TabPages.Add(oTabPage)
                tabMain.SelectedTab = oTabPage

            End If

            'Retoma Layout
            tabMain.ResumeLayout()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ClosePageButOne(ByVal pStrName As String)

        Dim i As Integer = 0

        Try

            While i < tabMain.TabPages.Count
                If tabMain.TabPages(i).Name <> pStrName Then
                    'Remove Página
                    Dim oPage As UITabPage = tabMain.TabPages(i)
                    tabMain.TabPages.Remove(oPage)
                    oPage.Dispose()
                Else
                    i += 1
                End If
            End While

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub Informacao(ByVal iMensagem As Mensagem, _
                          Optional ByVal sCampo As String = "")

        'Váriaveis Locais
        Dim sMensagem As String = ""

        Try

            Select Case iMensagem
                Case Mensagem.RegistroExcluido : sMensagem = "Registro excluído com sucesso!"
                Case Mensagem.RegistroAlterado : sMensagem = "Registro alterado com sucesso!"
                Case Mensagem.RegistroInserido : sMensagem = "Registro inserido com sucesso!"
                Case Mensagem.DireitoAlteracao : sMensagem = "Usuário sem direito para alterar este registro."
                Case Mensagem.ValidacaoSelecionar : sMensagem = "Selecione um registro. " & sCampo
                Case Mensagem.OperacaoRealizadaSucesso : sMensagem = "Operação realizada com sucesso!"
                Case Mensagem.Erro : sMensagem = "Erro: " & sCampo
            End Select

            Call Informacao(sMensagem, IIf(iMensagem = Mensagem.Erro Or iMensagem = Mensagem.DireitoAlteracao, gColorInformacaoErro, gColorInformacaoOK))

        Catch
            'Não propaga erro de exibição de mensagem
        End Try

    End Sub

    Public Sub Informacao(ByVal sMensagem As String, ByVal oColor As System.Drawing.Color)

        Try

            stbMain.Panels("pnlInfo").Text = sMensagem
            stbMain.Panels("pnlInfo").FormatStyle.ForeColor = oColor

            'Limpa a mensagem após gcTempoMensagem (Timer na thread da interface)
            tmrInformacao.Stop()
            tmrInformacao.Start()

        Catch
            'Não propaga erro de exibição de mensagem
        End Try

    End Sub

#End Region

End Class
