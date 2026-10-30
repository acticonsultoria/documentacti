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

#End Region

#Region "::: CONTROLES :::"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load

        Call ConfigurarFormulario()

    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            tmrNotificacao.Stop()
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

        Catch ex As Exception
            'Falha de conexão: interrompe as notificações e avisa na barra de status (voltam ao trocar de usuário)
            tmrNotificacao.Stop()
            Informacao("Notificações do chat interrompidas: " & ex.Message, gColorInformacaoErro)
        End Try

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

    Private Sub mnuTrocarUsuario_Click(sender As Object, e As EventArgs) Handles mnuTrocarUsuario.Click

        Try

            If MsgBox("Deseja trocar de usuário? As telas abertas serão fechadas.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gAplicacao) = MsgBoxResult.No Then Exit Sub

            'Fecha telas abertas (exceto a principal)
            Call ClosePageButOne("pagPrincipal")

            tmrNotificacao.Stop()
            oToastManager.CloseAll()

            Dim oClsFrmLogin As New clsFrmLogin
            oClsFrmLogin.InsertLog("logout")

            goUsuario = Nothing

            If Login() = False Then
                Me.Close()
            Else
                Call IniciarNotificacoes()
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

            'Abre a tela de chamados
            Call AbrirTela(New usrAdmChamado, "Chamados")

            Call IniciarNotificacoes()

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

#Region "::: NOTIFICAÇÕES :::"

    Private Sub IniciarNotificacoes()

        Try

            oMensagensNotificadas.Clear()
            oToastsChat.Clear()
            bNotificacaoIniciada = False

            oToastManager.AppCaption = "Documentacti"
            oToastManager.MaxVisible = 4

            'Silenciar (Configurações) vale só até sair do sistema: a cada login as notificações são religadas,
            'para não ficarem desligadas por esquecimento
            Dim oClsConfiguracao As New clsFrmConfiguracao
            oClsConfiguracao.Update(False)
            bSilenciarNotificacao = False

            Call VerificarNotificacoes()

            tmrNotificacao.Interval = gcIntervaloNotificacao
            tmrNotificacao.Start()

        Catch ex As Exception
            Informacao("Notificações do chat indisponíveis: " & ex.Message, gColorInformacaoErro)
        End Try

    End Sub

    Private Sub VerificarNotificacoes()

        Try

            Dim oClsUsrAdmChamado As New clsUsrAdmChamado
            Dim oTabela As DataTable = oClsUsrAdmChamado.LoadChatNotificacao()
            Dim oNovas As New List(Of DataRow)

            'Somente o chat do chamado (canal 1); o chat interno (DEV) não é mais usado
            For Each oRow As DataRow In oTabela.Select("canal <> 1")
                oTabela.Rows.Remove(oRow)
            Next

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
            Dim oToast As Toast = Nothing
            If oToastsChat.TryGetValue(sChave, oToast) AndAlso oToast.IsOpen Then
                oToast.Title = oRow.Item("nome_usuario").ToString
                oToast.Message = sTexto
                oToast.Caption = sCaption
                oToast.RestartTimer()
                Exit Sub
            End If

            oToast = New Toast(oRow.Item("nome_usuario").ToString, sTexto, ToastType.Message) With {.Caption = sCaption, .Duration = 8000}
            AddHandler oToast.Click, Sub(s, a) AbrirChatChamado(lChamado, iCanal)

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
