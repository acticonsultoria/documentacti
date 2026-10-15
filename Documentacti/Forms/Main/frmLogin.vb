Imports System.IO

Public Class frmLogin

#Region "::: VARIAVEIS :::"

    'Variaveis do Módulo
    Private oClsLogin As New clsFrmLogin

#End Region

#Region "::: CONTROLES :::"

    Private Sub frmLogin_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        'Verifica se foi pressionado a tecla enter
        If e.KeyCode = Keys.Enter Then
            'Executa Clique do Botão - Logar
            Call btnLogar_Click(btnLogar, System.EventArgs.Empty)
        End If

    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Configura Formulário
        Call ConfigureFormulario()

    End Sub

    Private Sub btnSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSair.Click

        Try

            Me.Dispose()

        Catch ex As Exception
            'Trata Erro
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnLogar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogar.Click

        Try

            frmMain.errInfo.Clear()

            'Valida Dados do Formulário
            If Validacao() = True Then

                'Autentica Usuário
                If AutenticaUsuario() = True Then

                    'Fecha o Formulário
                    Me.Dispose()

                End If

            End If

        Catch ex As Exception
            'Trata Erro
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

#End Region

#Region "::: FUNCTION / SUB :::"

    Private Sub ConfigureFormulario()

        Try

            'Adiciona GotFocus / LostFocus
            Call AddGotFocus(Me, New DelegateGotFocus(AddressOf Control_GotFocus))
            Call AddLostFocus(Me, New DelegateLostFocus(AddressOf Control_LostFocus))

            'Seta Ícone
            Me.Icon = frmMain.Icon

            'Seta Label
            Dim oFileInfo As FileInfo = New FileInfo(ApplicationPath() & My.Application.Info.AssemblyName.ToString & ".exe")
            lblSistema.Text = "Bem Vindo ao " & gAplicacao & " - Versão: " & Application.ProductVersion.ToString & " - Última Atualização: " & Format(oFileInfo.LastWriteTime, "dd/MM/yyyy")

            txtUsuario.Focus()

            'Seta Cursor do Mouse
            Cursor.Current = Cursors.Default

        Catch ex As Exception
            'Trata Erro
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Function AutenticaUsuario() As Boolean

        Try

            'Seta Retorno da Função
            AutenticaUsuario = False

            'Seta Cursor do Mouse
            Cursor.Current = Cursors.WaitCursor

            'Seta Parametros
            oClsLogin.Usuario = txtUsuario.Text
            oClsLogin.Senha = txtSenha.Text

            'Verifica se o Usuário é Válido
            If oClsLogin.Login = False Then
                'Informa o Usuário que a Senha está errada
                Cursor.Current = Cursors.Default
                frmMain.errInfo.SetError(lblUsuario, "Usuário ou Senha informado são inválidos, ou o usuário não pertence à equipe interna.")
                txtSenha.Text = ""
                txtUsuario.Focus()
                Exit Function
            End If

            'Seta Retorno da Função
            AutenticaUsuario = True

            'Seta Cursor do Mouse
            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function Validacao() As Boolean

        Try

            'Seta Retorno da Função
            Validacao = False

            'Verifica se foi Preenchido o Campo - Usuário
            If ValidaCampo(txtUsuario, lblUsuario, TipoCampo.texto) = False Then
                Exit Function
            End If

            'Verifica se foi Preenchido o Campo - Senha
            If ValidaCampo(txtSenha, lblSenha, TipoCampo.texto) = False Then
                Exit Function
            End If

            'Seta Retorno da Função
            Validacao = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

End Class
