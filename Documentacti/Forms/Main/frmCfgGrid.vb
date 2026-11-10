Imports Janus.Windows.GridEX

Public Class frmCfgGrid

#Region "::: VARIAVEIS :::"

    'Grid configurada (a tela altera as colunas dela na hora de salvar)
    Private oGrid As GridEX

#End Region

#Region "::: PROPRIEDADES :::"

    Public WriteOnly Property Grid() As GridEX
        Set(ByVal value As GridEX)
            oGrid = value
        End Set
    End Property

#End Region

#Region "::: CONTROLES :::"

    Private Sub frmCfgGrid_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            Call AddGotFocus(Me, New DelegateGotFocus(AddressOf Control_GotFocus))
            Call AddLostFocus(Me, New DelegateLostFocus(AddressOf Control_LostFocus))

            Me.Icon = frmMain.Icon

            Call ConfiguraGrid(grdColunas, False)

            Call LoadGrid()

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub frmCfgGrid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        'Esc volta
        If e.KeyCode = Keys.Escape Then
            e.SuppressKeyPress = True
            Call btnVoltar_Click(btnVoltar, System.EventArgs.Empty)
        End If

    End Sub

    Private Sub grdColunas_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdColunas.CellValueChanged

        Try

            'Check confirmado na hora (sem precisar sair da célula)
            If e.Column.Key = "exibir" Then grdColunas.UpdateData()

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalvar.Click

        Try

            If Salvar() = True Then Me.DialogResult = DialogResult.OK

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnRestaurar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestaurar.Click

        Try

            If MsgBox("Deseja restaurar as colunas desta grid para o padrão?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Cursor.Current = Cursors.WaitCursor

            Call RestaurarPadraoGrid(oGrid)

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Colunas restauradas para o padrão.", Color.Blue)

            Me.DialogResult = DialogResult.OK

        Catch ex As Exception
            Call TratamentoErro(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnVoltar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoltar.Click

        Me.DialogResult = DialogResult.Cancel

    End Sub

#End Region

#Region "::: FUNCTION / SUB :::"

    Private Sub LoadGrid()

        Try

            'Somente as colunas que o usuário pode ocultar (Tag "S"), na ordem atual da grid
            Dim oTabela As New DataTable
            oTabela.Columns.Add("campo", GetType(String))
            oTabela.Columns.Add("exibir", GetType(Boolean))
            oTabela.Columns.Add("key", GetType(String))
            oTabela.Columns.Add("posicao", GetType(Integer))

            For Each oColumn As GridEXColumn In oGrid.RootTable.Columns
                If ColunaConfiguravel(oColumn) Then
                    oTabela.Rows.Add(IIf(oColumn.Caption.Trim = "", oColumn.Key, oColumn.Caption.Replace(vbCrLf, " ")), _
                                     oColumn.Visible, _
                                     oColumn.Key, _
                                     oColumn.Position)
                End If
            Next

            oTabela.DefaultView.Sort = "posicao"

            grdColunas.DataSource = oTabela.DefaultView.ToTable

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function Salvar() As Boolean

        Try

            Salvar = False

            grdColunas.UpdateData()

            Dim oTabela As DataTable = CType(grdColunas.DataSource, DataTable)
            Dim iVisiveis As Integer = 0

            For Each oRow As DataRow In oTabela.Rows
                If CBool(oRow.Item("exibir")) Then iVisiveis += 1
            Next

            If iVisiveis = 0 Then
                MsgBox("Deixe ao menos uma coluna visível.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Function
            End If

            Cursor.Current = Cursors.WaitCursor

            'Aplica na grid da tela e grava a lista inteira de uma vez
            For Each oRow As DataRow In oTabela.Rows
                If oGrid.RootTable.Columns.Contains(oRow.Item("key").ToString) Then
                    oGrid.RootTable.Columns(oRow.Item("key").ToString).Visible = CBool(oRow.Item("exibir"))
                End If
            Next

            Call SalvarConfiguracaoGrid(oGrid, clsFrmCfgGrid.gcTipoVisivel)

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Configuração das colunas salva com sucesso!", Color.Blue)

            Salvar = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

End Class
