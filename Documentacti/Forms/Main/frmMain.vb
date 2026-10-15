Imports Janus.Windows.UI.Tab

Public Class frmMain

#Region "::: VARIAVEIS :::"

    Private oClsConfigXML As New clsConfigXML
    Private WithEvents tmrInformacao As New System.Windows.Forms.Timer

#End Region

#Region "::: CONTROLES :::"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load

        Call ConfigurarFormulario()

    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

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

    Private Sub mnuTrocarUsuario_Click(sender As Object, e As EventArgs) Handles mnuTrocarUsuario.Click

        Try

            If MsgBox("Deseja trocar de usuário? As telas abertas serão fechadas.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gAplicacao) = MsgBoxResult.No Then Exit Sub

            'Fecha telas abertas (exceto a principal)
            Call ClosePageButOne("pagPrincipal")

            Dim oClsFrmLogin As New clsFrmLogin
            oClsFrmLogin.InsertLog("logout")

            goUsuario = Nothing

            If Login() = False Then Me.Close()

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

            'Abre a tela de chamados
            Call AbrirTela(New usrAdmChamado, "Chamados")

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
