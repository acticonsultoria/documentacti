Imports Janus.Windows.GridEX
Imports Janus.Windows.EditControls
Imports MySql.Data.MySqlClient

Public Class usrCadAviso

#Region "::: VARIAVEIS :::"

    Private oClsUsrCadAviso As New clsUsrCadAviso

    'Código do aviso em edição (0 = novo; a PK não aparece na tela)
    Private lCodigoAviso As Long = 0

    'Empresas marcadas ao abrir/salvar: usadas para avisar sobre alterações não salvas
    Private sEmpresasGravadas As String = ""

#End Region

#Region "::: CONTROLES :::"

#Region "::: GERAL :::"

    Private Sub usrCadAviso_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Call ConfigurarUserControl()

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Try

            Call ExportExcel(grdListagem)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnFiltrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFiltrar.Click

        Try

            Call LoadGrid()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnNovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNovo.Click, btnNovo1.Click

        Try

            If DescartarAlteracoes() = False Then Exit Sub

            Call Novo()

            Call MostrarDados()

            txtTitulo.Focus()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalvar.Click

        Try

            frmMain.errInfo.Clear()

            'Confirma a célula em edição antes de ler as marcações
            grdEmpresas.UpdateData()

            If Validacao() = True Then

                Call Salvar()

            End If

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSair.Click, btnSair2.Click

        Try

            If sender Is btnSair2 AndAlso DescartarAlteracoes() = False Then Exit Sub

            If TypeOf Me.Parent Is Form Then Me.Parent.Dispose() Else frmMain.tabMain.TabPages.Remove(Me.Parent)
            Me.Dispose()

        Catch ex As Exception
            Call TratamentoErro(ex.Message, "Avisos")
        End Try

    End Sub

    Private Sub btnVoltar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoltar.Click

        Try

            If DescartarAlteracoes() = False Then Exit Sub

            frmMain.errInfo.Clear()

            Call LoadGrid()

            Call MostrarLista()

            txtTituloFiltro.Focus()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdListagem_RowDoubleClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdListagem.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            If grdListagem.CurrentColumn.Key = "editar" Then Call Editar()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdEmpresas_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdEmpresas.CellValueChanged

        Try

            'A marcação só altera a grid; o banco é atualizado somente no Salvar
            If e.Column.Key = "selecionado" Then
                grdEmpresas.UpdateData()
                Call AtualizarContador()
            End If

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnMarcarTodas_Click(sender As Object, e As EventArgs) Handles btnMarcarTodas.Click, btnDesmarcarTodas.Click

        Try

            'Considera somente as linhas visíveis (respeita o filtro da grid)
            Call MarcarEmpresas(sender Is btnMarcarTodas)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub usrCadAviso_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        Try

            Select Case e.KeyCode

                Case Keys.Enter
                    If InStr(sender.name, "Filtro", CompareMethod.Text) > 0 Then
                        Call LoadGrid()
                    End If

            End Select

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
            Call AddKeyDown(Me, New DelegateKeyDown(AddressOf usrCadAviso_KeyDown))
            Call AddLostFocus(Me, New DelegateLostFocus(AddressOf Control_LostFocus))

            Call LoadComboLista(cboAtivoFiltro, "-1=TODOS;1=SIM;0=NÃO")

            tabMain.TabPages.Remove(pagDados)

            Call ConfiguraGrid(grdListagem)
            Call ConfiguraGrid(grdEmpresas)

            Call LoadGrid()

            txtTituloFiltro.Focus()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub TratarErro(ByVal ex As Exception)

        'Erro de regra de negócio (SIGNAL nas procs) x erro de sistema
        If TypeOf ex Is MySqlException AndAlso CType(ex, MySqlException).Number = 1644 Then
            Cursor.Current = Cursors.Default
            frmMain.Informacao(ex.Message, gColorInformacaoErro)
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, gAplicacao)
        Else
            Call TratamentoErro(ex, "Avisos")
        End If

    End Sub

    Private Sub MostrarDados()

        tabMain.TabPages.Remove(pagLista)
        If tabMain.TabPages.Contains(pagDados) = False Then tabMain.TabPages.Insert(0, pagDados)
        tabMain.SelectedTab = pagDados

    End Sub

    Private Sub MostrarLista()

        tabMain.TabPages.Remove(pagDados)
        If tabMain.TabPages.Contains(pagLista) = False Then tabMain.TabPages.Insert(0, pagLista)
        tabMain.SelectedTab = pagLista

    End Sub

    Private Sub LoadGrid()

        Try

            Cursor.Current = Cursors.WaitCursor

            Call oClsUsrCadAviso.LoadGrid(grdListagem, _
                                          txtTituloFiltro.Text.Trim, _
                                          CInt(cboAtivoFiltro.SelectedValue))

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Novo()

        Try

            frmMain.errInfo.Clear()

            lCodigoAviso = 0
            txtTitulo.Text = ""
            txtDescricao.Text = ""
            chkAtivo.Checked = True

            'Aviso novo: nenhuma empresa marcada
            Call LoadGridEmpresas("")

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Editar()

        Try

            Cursor.Current = Cursors.WaitCursor

            Dim oTabela As DataTable = oClsUsrCadAviso.LoadDados(CLng(grdListagem.CurrentRow.Cells.Item("codigo").Value))

            If oTabela.Rows.Count = 0 Then
                Cursor.Current = Cursors.Default
                MsgBox("Aviso não encontrado.", MsgBoxStyle.Exclamation, gAplicacao)
                Exit Sub
            End If

            frmMain.errInfo.Clear()

            With oTabela.Rows(0)

                lCodigoAviso = CLng(.Item("codigo"))
                txtTitulo.Text = .Item("titulo").ToString
                'Quebras de linha gravadas só com LF (web) aparecem corretamente na caixa de texto
                txtDescricao.Text = .Item("descricao").ToString.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
                chkAtivo.Checked = (IsDBNull(.Item("ativo")) = False AndAlso CInt(.Item("ativo")) = 1)

                Call LoadGridEmpresas(.Item("empresa_id").ToString)

            End With

            Call MostrarDados()

            txtTitulo.Focus()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Salvar()

        Try

            Cursor.Current = Cursors.WaitCursor

            Dim sEmpresas As String = EmpresasMarcadas()
            Dim sDescricao As String = txtDescricao.Text.Trim.Replace(vbCrLf, vbLf)

            If lCodigoAviso > 0 Then
                oClsUsrCadAviso.Update(lCodigoAviso, txtTitulo.Text.Trim, sDescricao, sEmpresas, chkAtivo.Checked)
            Else
                lCodigoAviso = oClsUsrCadAviso.Insert(txtTitulo.Text.Trim, sDescricao, sEmpresas, chkAtivo.Checked)
            End If

            sEmpresasGravadas = sEmpresas

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Registro salvo com sucesso! Aviso vinculado a " & ContarEmpresas(sEmpresas) & " empresa(s).", Color.Blue)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function Validacao() As Boolean

        Try

            Validacao = False

            If ValidaCampo(txtTitulo, lblTitulo, TipoCampo.texto) = False Then Exit Function
            If ValidaCampo(txtDescricao, lblDescricao, TipoCampo.texto) = False Then Exit Function

            'Aviso ativo sem empresa não aparece para ninguém: confirma
            If chkAtivo.Checked AndAlso EmpresasMarcadas() = "" Then
                If MsgBox("Nenhuma empresa foi marcada para receber o aviso." & vbCrLf & "Deseja salvar assim mesmo?", _
                          MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gAplicacao) = MsgBoxResult.No Then Exit Function
            End If

            Validacao = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "::: EMPRESAS :::"

    Private Sub LoadGridEmpresas(ByVal sEmpresasAviso As String)

        Try

            Dim oTabela As DataTable = oClsUsrCadAviso.LoadEmpresas()
            Dim oVinculadas As New List(Of String)(sEmpresasAviso.Split(New Char() {";"c, ","c}, StringSplitOptions.RemoveEmptyEntries))

            For Each oRow As DataRow In oTabela.Rows
                oRow.Item("selecionado") = oVinculadas.Contains(oRow.Item("codigo").ToString)
            Next

            oTabela.AcceptChanges()

            'Empresas que recebem o aviso aparecem primeiro
            oTabela.DefaultView.Sort = "selecionado DESC, nome_fantasia ASC"

            Call ConfigurarDataMemberGrid(grdEmpresas)
            grdEmpresas.DataSource = oTabela.DefaultView

            sEmpresasGravadas = EmpresasMarcadas()

            Call AtualizarContador()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub MarcarEmpresas(ByVal bMarcar As Boolean)

        Try

            For Each oRow As GridEXRow In grdEmpresas.GetRows()
                If oRow.RowType = RowType.Record Then
                    CType(oRow.DataRow, DataRowView).Item("selecionado") = bMarcar
                End If
            Next

            grdEmpresas.Refetch()

            Call AtualizarContador()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function EmpresasMarcadas() As String

        'Códigos separados por ";" (formato das procs de aviso)
        Try

            Dim oTabela As DataTable = TabelaEmpresas()
            Dim oCodigos As New List(Of String)

            If IsNothing(oTabela) Then Return ""

            For Each oRow As DataRow In oTabela.Rows
                If IsDBNull(oRow.Item("selecionado")) = False AndAlso CBool(oRow.Item("selecionado")) Then
                    oCodigos.Add(oRow.Item("codigo").ToString)
                End If
            Next

            oCodigos.Sort(Function(a, b) CLng(a).CompareTo(CLng(b)))

            Return String.Join(";", oCodigos)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function TabelaEmpresas() As DataTable

        Dim oView As DataView = TryCast(grdEmpresas.DataSource, DataView)

        Return IIf(IsNothing(oView), Nothing, oView.Table)

    End Function

    Private Function ContarEmpresas(ByVal sEmpresas As String) As Integer

        Return sEmpresas.Split(New Char() {";"c}, StringSplitOptions.RemoveEmptyEntries).Length

    End Function

    Private Sub AtualizarContador()

        lblEmpresasMarcadas.Text = ContarEmpresas(EmpresasMarcadas()) & " empresa(s) marcada(s)" & _
                                   IIf(EmpresasMarcadas() <> sEmpresasGravadas, " - alterações não salvas", "")

    End Sub

    Private Function DescartarAlteracoes() As Boolean

        'Marcações na grid ainda não salvas: confirma antes de sair da edição
        Try

            If tabMain.TabPages.Contains(pagDados) = False OrElse IsNothing(TabelaEmpresas()) Then Return True

            grdEmpresas.UpdateData()

            If EmpresasMarcadas() = sEmpresasGravadas Then Return True

            Return MsgBox("As empresas marcadas na grid ainda não foram salvas." & vbCrLf & "Deseja descartar as alterações?", _
                          MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gAplicacao) = MsgBoxResult.Yes

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#End Region

End Class
