Imports Janus.Windows.GridEX

Public Class frmNotificacao

#Region "::: VARIAVEIS :::"

    Private oClsFrmNotificacao As New clsFrmNotificacao

    'Notificações do sino (tabela do banco + chamados com mensagens não lidas no chat), montada pelo frmMain
    Public oTabela As DataTable

    'Fecha ao perder o foco (igual a um menu suspenso); desligado enquanto abre a notificação escolhida ou pergunta algo
    Private bFecharAoSair As Boolean = True

    'Carência logo após abrir: na abertura do sistema a tela principal ainda pode pegar o foco por um instante
    Private oAberto As Stopwatch = Stopwatch.StartNew()
    Private Const gcCarenciaFechar As Integer = 1500 'milissegundos

#End Region

#Region "::: CONTROLES :::"

    Private Sub frmNotificacao_Load(sender As Object, e As EventArgs) Handles Me.Load

        Call ConfigurarFormulario()

    End Sub

    Private Sub frmNotificacao_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate

        If bFecharAoSair = False Then Exit Sub

        If oAberto.ElapsedMilliseconds < gcCarenciaFechar Then
            BeginInvoke(New MethodInvoker(Sub() If Not IsDisposed Then Activate()))
            Exit Sub
        End If

        Me.Close()

    End Sub

    Private Sub frmNotificacao_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Try

            Select Case e.KeyCode
                Case Keys.Escape
                    Me.Close()
                Case Keys.Enter
                    If grdNotificacao.Focused AndAlso grdNotificacao.CurrentRow IsNot Nothing AndAlso grdNotificacao.CurrentRow.RowType = RowType.Record Then
                        e.Handled = True
                        Call AbrirNotificacao(CType(grdNotificacao.CurrentRow.DataRow, DataRowView).Row)
                    End If
            End Select

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub grdNotificacao_RowDoubleClick(ByVal sender As Object, ByVal e As RowActionEventArgs) Handles grdNotificacao.RowDoubleClick

        'Duplo clique: check = marcar como lida / X = excluir / demais colunas = abrir a notificação.
        'A coluna vem da posição do mouse (CurrentColumn nem sempre acompanha o clique na grid sem foco de célula)
        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            Dim oRow As DataRow = CType(e.Row.DataRow, DataRowView).Row
            Dim oColuna As GridEXColumn = grdNotificacao.ColumnFromPoint()

            Select Case IIf(IsNothing(oColuna), "", oColuna.Key).ToString
                Case "marcar_lida" : Call MarcarLida(oRow)
                Case "remover" : Call Remover(oRow)
                Case Else : Call AbrirNotificacao(oRow)
            End Select

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnMarcarLidas_Click(sender As Object, e As EventArgs) Handles btnMarcarLidas.Click

        Try

            Cursor.Current = Cursors.WaitCursor

            oClsFrmNotificacao.UpdateLida(0)

            'Chat: marca a conversa como lida até a última mensagem
            For Each oRow As DataRow In oTabela.Select("tipo = 'CHAT'")
                Call MarcarChatLido(oRow)
            Next

            For Each oRow As DataRow In oTabela.Rows
                oRow.Item("lida") = 1
                oRow.Item("situacao") = "LIDA"
            Next

            Call AtualizarTela()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Cursor.Current = Cursors.Default
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnExcluirTodas_Click(sender As Object, e As EventArgs) Handles btnExcluirTodas.Click

        Try

            If oTabela.Rows.Count = 0 Then Exit Sub

            'A pergunta tira o foco do popup: não pode fechar por isso
            bFecharAoSair = False
            Dim oResposta As MsgBoxResult = MsgBox("Excluir todas as notificações do sino?" & vbCrLf & _
                                                   "As conversas do chat com mensagens não lidas serão marcadas como lidas.", _
                                                   MsgBoxStyle.Question + MsgBoxStyle.YesNo, gAplicacao)
            bFecharAoSair = True
            Me.Activate()

            If oResposta = MsgBoxResult.No Then Exit Sub

            Cursor.Current = Cursors.WaitCursor

            oClsFrmNotificacao.Delete(0)

            For Each oRow As DataRow In oTabela.Select("tipo = 'CHAT'")
                Call MarcarChatLido(oRow)
            Next

            oTabela.Rows.Clear()

            Call AtualizarTela()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            bFecharAoSair = True
            Cursor.Current = Cursors.Default
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnSair_Click(sender As Object, e As EventArgs) Handles btnSair.Click

        Me.Close()

    End Sub

#End Region

#Region "::: FUNCTION / SUB :::"

    Private Sub ConfigurarFormulario()

        Try

            Call ConfiguraGrid(grdNotificacao, False)

            Call ConfigurarDataMemberGrid(grdNotificacao)
            grdNotificacao.DataSource = oTabela

            Call AtualizarTitulo()

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub AtualizarTitulo()

        grpNotificacao.Text = "Notificações (" & oTabela.Select("situacao = 'NÃO LIDA'").Length & " não lida(s) de " & oTabela.Rows.Count & ")"

    End Sub

    Private Sub AtualizarTela()

        grdNotificacao.Refetch()
        Call AtualizarTitulo()
        frmMain.AtualizarSino()

    End Sub

    Private Sub MarcarChatLido(ByVal oRow As DataRow)

        'Chat não fica na tabela do sino: "ler" é marcar a conversa como lida até a última mensagem avisada
        If IsDBNull(oRow.Item("codigo_mensagem")) Then Exit Sub

        frmMain.MarcarChatLido(CLng(oRow.Item("codigo_referencia")), _
                               CInt(IIf(IsDBNull(oRow.Item("canal")), 1, oRow.Item("canal"))), _
                               CInt(oRow.Item("codigo_mensagem")))

    End Sub

    Private Sub MarcarLida(ByVal oRow As DataRow)

        Try

            If CInt(oRow.Item("lida")) = 1 Then Exit Sub

            If oRow.Item("tipo").ToString = "CHAT" Then
                Call MarcarChatLido(oRow)
            Else
                oClsFrmNotificacao.UpdateLida(CLng(oRow.Item("codigo")))
            End If

            oRow.Item("lida") = 1
            oRow.Item("situacao") = "LIDA"

            Call AtualizarTela()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Remover(ByVal oRow As DataRow)

        Try

            'Chat não tem registro no sino: excluir = conversa lida (sai da lista)
            If oRow.Item("tipo").ToString = "CHAT" Then
                Call MarcarChatLido(oRow)
            Else
                oClsFrmNotificacao.Delete(CLng(oRow.Item("codigo")))
            End If

            oTabela.Rows.Remove(oRow)

            Call AtualizarTela()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AbrirNotificacao(ByVal oRow As DataRow)

        Try

            Dim sTipo As String = oRow.Item("tipo").ToString
            Dim lCodigo As Long = CLng(oRow.Item("codigo"))
            Dim lReferencia As Long = CLng(oRow.Item("codigo_referencia"))
            Dim oDataReferencia As Object = oRow.Item("data_referencia")
            Dim iCanal As Integer = IIf(IsDBNull(oRow.Item("canal")), 1, oRow.Item("canal"))

            'Lida ao abrir (o chat sai do sino quando a conversa é lida na tela do chamado)
            If sTipo <> "CHAT" AndAlso CInt(oRow.Item("lida")) = 0 Then oClsFrmNotificacao.UpdateLida(lCodigo)

            bFecharAoSair = False
            Me.Close()

            frmMain.AbrirNotificacao(sTipo, lReferencia, oDataReferencia, iCanal)
            frmMain.AtualizarSino()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class
