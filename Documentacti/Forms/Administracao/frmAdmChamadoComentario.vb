Imports Janus.Windows.GridEX
Imports System.IO

Public Class frmAdmChamadoComentario

#Region "::: VARIAVEIS :::"

    Private oClsUsrAdmChamado As New clsUsrAdmChamado
    Private bComentarioDev As Boolean = False
    Private bVisualizacao As Boolean = False

#End Region

#Region "::: PROPRIEDADES :::"

    'True = Comentário interno de desenvolvimento (não visível ao cliente)
    Public Property ComentarioDev() As Boolean
        Get
            Return bComentarioDev
        End Get
        Set(ByVal value As Boolean)
            bComentarioDev = value
        End Set
    End Property

    'True = somente visualização do comentário
    Public Property Visualizacao() As Boolean
        Get
            Return bVisualizacao
        End Get
        Set(ByVal value As Boolean)
            bVisualizacao = value
        End Set
    End Property

#End Region

#Region "::: CONTROLES :::"

    Private Sub frmAdmChamadoComentario_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Call ConfigurarForm()

    End Sub

    Private Sub btnInserir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInserir.Click

        Try

            frmMain.errInfo.Clear()

            If Validacao() = True Then

                'Só fecha o formulário após confirmar a gravação
                If Salvar() = True Then Me.Dispose()

            End If

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSair.Click

        Me.Dispose()

    End Sub

#End Region

#Region "::: FUNCTION / SUB :::"

    Private Sub ConfigurarForm()

        Try

            Me.Icon = frmMain.Icon

            If bComentarioDev = True Then
                Me.Text = "Chamado - Comentário de DEV (interno)"
                pagTabela.Text = "Comentário de DEV"
            End If

            If bVisualizacao = True Then
                txtComentario.ReadOnly = True
                btnInserir.Enabled = False
            End If

            txtComentario.Focus()

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Function Salvar() As Boolean

        Try

            Salvar = False

            Cursor.Current = Cursors.WaitCursor

            If bComentarioDev = True Then
                Call oClsUsrAdmChamado.InsertComentarioDev(txtCodigoChamado.Text, _
                                                           txtComentario.Text)
            Else
                Call oClsUsrAdmChamado.InsertComentario(txtCodigoChamado.Text, _
                                                        txtComentario.Text)
            End If

            frmMain.Informacao("Comentário incluído com sucesso!", Color.Blue)

            Cursor.Current = Cursors.Default

            Salvar = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function Validacao() As Boolean

        Try

            Validacao = False

            If ValidaCampo(txtComentario, lblHistorico, TipoCampo.texto) = False Then Exit Function

            Validacao = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

End Class
