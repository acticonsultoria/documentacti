Imports Janus.Windows.GridEX
Imports Janus.Windows.EditControls
Imports MySql.Data.MySqlClient

Public Class usrCadBaseConhecimento

#Region "::: VARIAVEIS :::"

    Private oClsUsrCadBaseConhecimento As New clsUsrCadBaseConhecimento
    Private oFtp As New clsFtp

    'Código do artigo em edição (0 = novo; a PK não aparece na tela)
    Private lCodigoArtigo As Long = 0

    'Empresas marcadas antes da última alteração no combo (controle da opção TODOS)
    Private sEmpresasAnteriores As String = ""
    Private bAjustandoEmpresas As Boolean = False

    'Código padrão da opção TODOS
    Private Const gcEmpresaTodos As String = "-1"

    'Anexos da base de conhecimento ficam na pasta da plataforma web (relativa à pasta de anexos de chamado do Config.xml)
    Private Const gcPastaFtpAnexoBaseConhecimento As String = "../../../base_conhecimento/uploads/"
    Private ReadOnly gcExtensoesAnexoBaseConhecimento() As String = {"txt", "xls", "xlsx", "pdf", "doc", "docx", "jpg", "png", "jpeg", "xml", "csv", "sql", "rar", "zip", "pfx", "ovpn"}

    Private Const gcTamanhoMaximoDescricao As Integer = 4000

#End Region

#Region "::: CONTROLES :::"

#Region "::: GERAL :::"

    Private Sub usrCadBaseConhecimento_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

            If Validacao() = True Then

                Call Salvar()

            End If

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSair.Click, btnSair2.Click

        Try

            If TypeOf Me.Parent Is Form Then Me.Parent.Dispose() Else frmMain.tabMain.TabPages.Remove(Me.Parent)
            Me.Dispose()

        Catch ex As Exception
            Call TratamentoErro(ex.Message, "Base de Conhecimento")
        End Try

    End Sub

    Private Sub btnVoltar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoltar.Click

        Try

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

    Private Sub btnArquivo_Click(sender As Object, e As EventArgs) Handles btnArquivo.Click

        Try

            frmMain.errInfo.Clear()

            If FtpDisponivel() = False Then Exit Sub

            If lCodigoArtigo = 0 Then
                frmMain.Informacao("Salve o artigo antes de anexar arquivos.", gColorInformacaoErro)
                Exit Sub
            End If

            Call InserirArquivos()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdArquivos_RowDoubleClick(sender As Object, e As RowActionEventArgs) Handles grdArquivos.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            If FtpDisponivel() = False Then Exit Sub

            Select Case grdArquivos.CurrentColumn.Key

                Case "visualizar" : Call VisualizarArquivo()
                Case "excluir" : Call ExcluirArquivo()

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub cboEmpresa_CheckedValuesChanged(sender As Object, e As EventArgs) Handles cboEmpresa.CheckedValuesChanged

        Try

            If bAjustandoEmpresas Then Exit Sub

            Call AjustarOpcaoTodos()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub tabConteudo_SelectedTabChanged(sender As Object, e As Janus.Windows.UI.Tab.TabEventArgs) Handles tabConteudo.SelectedTabChanged

        Try

            'A visualização reflete o HTML editado (ainda não salvo)
            If e.Page Is pagVisualizar Then Call AtualizarVisualizacao()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub txtDescricao_TextChanged(sender As Object, e As EventArgs) Handles txtDescricao.TextChanged

        Try

            Call AtualizarContador()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub webDescricao_Navigating(sender As Object, e As WebBrowserNavigatingEventArgs) Handles webDescricao.Navigating

        'Links do artigo abrem no navegador padrão, não dentro da tela
        If e.Url.ToString <> "about:blank" Then
            e.Cancel = True
            If e.Url.Scheme = Uri.UriSchemeHttp OrElse e.Url.Scheme = Uri.UriSchemeHttps Then Process.Start(e.Url.ToString)
        End If

    End Sub

    Private Sub usrCadBaseConhecimento_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

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
            Call AddKeyDown(Me, New DelegateKeyDown(AddressOf usrCadBaseConhecimento_KeyDown))
            Call AddLostFocus(Me, New DelegateLostFocus(AddressOf Control_LostFocus))

            'Combos - Filtro
            Call LoadCombo(cboModuloFiltro, "sp_select_combo_cadastro_basico_modulo")
            Call InserirOpcao(cboModuloFiltro, -1, "TODOS")
            Call LoadCombo(cboEmpresaFiltro, "sp_select_combo_administracao_chamado_empresa")
            Call InserirOpcao(cboEmpresaFiltro, -1, "SOMENTE ARTIGOS PARA TODOS")
            Call InserirOpcao(cboEmpresaFiltro, -2, "TODAS")

            'Combos - Dados
            Call LoadCombo(cboModulo, "sp_select_combo_cadastro_basico_modulo")
            Call LoadCombo(cboEmpresa, oClsUsrCadBaseConhecimento.LoadEmpresas())
            Call LoadCombo(cboMarcador, oClsUsrCadBaseConhecimento.LoadMarcadores())

            tabMain.TabPages.Remove(pagDados)

            Call ConfiguraGrid(grdListagem)
            Call ConfiguraGrid(grdArquivos)

            Call LoadGrid()

            txtTituloFiltro.Focus()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub InserirOpcao(ByVal oComboBox As UIComboBox, ByVal iCodigo As Integer, ByVal sDescricao As String)

        'Opção fixa no topo do combo, já selecionada
        Dim oTabela As DataTable = CType(oComboBox.DataSource, DataTable)
        Dim oRow As DataRow = oTabela.NewRow

        oRow.Item("codigo") = iCodigo
        oRow.Item("descricao") = sDescricao
        oTabela.Rows.InsertAt(oRow, 0)

        oComboBox.SelectedIndex = 0

    End Sub

    Private Sub TratarErro(ByVal ex As Exception)

        'Erro de regra de negócio (SIGNAL nas procs) x erro de sistema
        If TypeOf ex Is MySqlException AndAlso CType(ex, MySqlException).Number = 1644 Then
            Cursor.Current = Cursors.Default
            frmMain.Informacao(ex.Message, gColorInformacaoErro)
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, gAplicacao)
        Else
            Call TratamentoErro(ex, "Base de Conhecimento")
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

            Call oClsUsrCadBaseConhecimento.LoadGrid(grdListagem, _
                                                     txtTituloFiltro.Text.Trim, _
                                                     txtMarcadorFiltro.Text.Trim, _
                                                     CInt(cboModuloFiltro.SelectedValue), _
                                                     CInt(cboEmpresaFiltro.SelectedValue))

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Novo()

        Try

            frmMain.errInfo.Clear()

            lCodigoArtigo = 0
            txtTitulo.Text = ""
            cboModulo.SelectedIndex = -1
            Call DefinirEmpresas("")
            cboMarcador.UncheckAll()
            txtDescricao.Text = ""
            grdArquivos.DataSource = Nothing

            'Artigo novo começa na edição do HTML
            tabConteudo.SelectedTab = pagEditar

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Editar()

        Try

            Cursor.Current = Cursors.WaitCursor

            Dim oTabela As DataTable = oClsUsrCadBaseConhecimento.LoadDados(CLng(grdListagem.CurrentRow.Cells.Item("codigo").Value))

            If oTabela.Rows.Count = 0 Then
                Cursor.Current = Cursors.Default
                MsgBox("Artigo não encontrado.", MsgBoxStyle.Exclamation, gAplicacao)
                Exit Sub
            End If

            Call Novo()

            With oTabela.Rows(0)

                lCodigoArtigo = CLng(.Item("codigo"))
                txtTitulo.Text = .Item("titulo").ToString
                If IsDBNull(.Item("codigo_modulo")) Then cboModulo.SelectedIndex = -1 Else cboModulo.SelectedValue = .Item("codigo_modulo")
                'Sem vínculo = não preenchido (combo vazio)
                Call DefinirEmpresas(.Item("codigos_empresa").ToString)
                Call SetCheckedValues(cboMarcador, .Item("marcadores").ToString.Replace(";", ","))
                'Quebras de linha gravadas só com LF aparecem corretamente na caixa de texto
                txtDescricao.Text = .Item("descricao").ToString.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)

            End With

            Call oClsUsrCadBaseConhecimento.LoadGridArquivo(grdArquivos, lCodigoArtigo)

            Call MostrarDados()

            tabConteudo.SelectedTab = pagVisualizar
            Call AtualizarVisualizacao()

            txtTitulo.Focus()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Salvar()

        Try

            Cursor.Current = Cursors.WaitCursor

            Dim bNovo As Boolean = (lCodigoArtigo = 0)

            lCodigoArtigo = oClsUsrCadBaseConhecimento.Gravar(lCodigoArtigo, _
                                                              txtTitulo.Text.Trim, _
                                                              DescricaoGravacao(), _
                                                              CInt(cboModulo.SelectedValue), _
                                                              GetCheckedValues(cboMarcador).Replace(",", ";"), _
                                                              GetCheckedValues(cboEmpresa))

            Cursor.Current = Cursors.Default

            frmMain.Informacao(IIf(bNovo, "Registro inserido com sucesso!", "Registro alterado com sucesso!"), Color.Blue)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function Validacao() As Boolean

        Try

            Validacao = False

            If ValidaCampo(txtTitulo, lblTitulo, TipoCampo.texto) = False Then Exit Function
            If ValidaCampo(cboModulo, lblModulo) = False Then Exit Function

            If DescricaoGravacao().Trim = "" Then
                tabConteudo.SelectedTab = pagEditar
                frmMain.errInfo.SetError(lblCaracteres, "O Campo [DESCRIÇÃO] é obrigatório. Digite um valor para ele.")
                txtDescricao.Focus()
                Exit Function
            End If

            If DescricaoGravacao().Length > gcTamanhoMaximoDescricao Then
                tabConteudo.SelectedTab = pagEditar
                frmMain.errInfo.SetError(lblCaracteres, "A descrição ultrapassa o limite de " & gcTamanhoMaximoDescricao & " caracteres.")
                txtDescricao.Focus()
                Exit Function
            End If

            Validacao = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "::: EMPRESAS :::"

    Private Sub DefinirEmpresas(ByVal sEmpresas As String)

        'Posiciona o combo sem disparar o ajuste da opção TODOS
        bAjustandoEmpresas = True

        Try
            Call SetCheckedValues(cboEmpresa, sEmpresas)
            sEmpresasAnteriores = GetCheckedValues(cboEmpresa)
        Finally
            bAjustandoEmpresas = False
        End Try

    End Sub

    Private Sub AjustarOpcaoTodos()

        'TODOS é exclusivo: marcar TODOS desmarca as empresas; marcar uma empresa desmarca TODOS
        Dim oAtuais As New List(Of String)(GetCheckedValues(cboEmpresa).Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries))
        Dim oAnteriores As New List(Of String)(sEmpresasAnteriores.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries))

        If oAtuais.Contains(gcEmpresaTodos) AndAlso oAtuais.Count > 1 Then
            If oAnteriores.Contains(gcEmpresaTodos) Then
                oAtuais.Remove(gcEmpresaTodos)
            Else
                oAtuais = New List(Of String)({gcEmpresaTodos})
            End If
            Call DefinirEmpresas(String.Join(",", oAtuais))
        Else
            sEmpresasAnteriores = String.Join(",", oAtuais)
        End If

    End Sub

#End Region

#Region "::: DESCRIÇÃO / ARQUIVOS :::"

    Private Function DescricaoGravacao() As String

        'Mantém o formato da web (quebra de linha LF)
        Return txtDescricao.Text.Replace(vbCrLf, vbLf)

    End Function

    Private Sub AtualizarContador()

        lblCaracteres.Text = DescricaoGravacao().Length & " de " & gcTamanhoMaximoDescricao & " caracteres"
        lblCaracteres.ForeColor = IIf(DescricaoGravacao().Length > gcTamanhoMaximoDescricao, Color.Red, Color.Blue)

    End Sub

    Private Sub AtualizarVisualizacao()

        Try

            webDescricao.DocumentText = HtmlVisualizacao(txtDescricao.Text)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function HtmlVisualizacao(ByVal sDescricao As String) As String

        'Somente leitura: remove scripts e eventos (on...) antes de exibir
        Dim sCorpo As String = System.Text.RegularExpressions.Regex.Replace(sDescricao, "<script[\s\S]*?</script>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        sCorpo = System.Text.RegularExpressions.Regex.Replace(sCorpo, "\son\w+\s*=\s*(""[^""]*""|'[^']*'|[^\s>]+)", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        'Texto sem tags HTML (artigos antigos): preserva as quebras de linha
        If System.Text.RegularExpressions.Regex.IsMatch(sCorpo, "<[a-zA-Z][^>]*>") = False Then
            sCorpo = System.Net.WebUtility.HtmlEncode(sCorpo).Replace(vbCrLf, vbLf).Replace(vbLf, "<br>")
        End If

        Return "<!DOCTYPE html><html><head><meta charset=""utf-8""><meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">" & _
               "<style>body{font-family:Segoe UI,Arial;font-size:10pt;margin:10px;} img{max-width:100%;}</style></head><body>" & _
               sCorpo & "</body></html>"

    End Function

    Private Function FtpDisponivel() As Boolean

        'O FTP fica compilado no executável (modConexao); em Debug o Config.xml pode sobrepor
        If oFtp.Configurado() = False Then
            frmMain.Informacao("FTP dos anexos não configurado.", gColorInformacaoErro)
            Return False
        End If

        Return True

    End Function

    Private Sub InserirArquivos()

        Try

            Dim oDialog As New OpenFileDialog
            oDialog.Multiselect = True
            oDialog.Title = "Selecione os arquivos do artigo " & lCodigoArtigo
            oDialog.Filter = "Arquivos permitidos|" & String.Join(";", Array.ConvertAll(gcExtensoesAnexoBaseConhecimento, Function(s) "*." & s))

            If oDialog.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim iEnviados As Integer = 0

            Cursor.Current = Cursors.WaitCursor

            Try

                For Each sCaminho As String In oDialog.FileNames

                    Dim oInfo As New System.IO.FileInfo(sCaminho)

                    'Mesmas extensões aceitas pela web (base_conhecimento/upload.php)
                    If Array.IndexOf(gcExtensoesAnexoBaseConhecimento, oInfo.Extension.TrimStart("."c).ToLower) < 0 Then
                        Throw New ArgumentException("O arquivo '" & oInfo.Name & "' não é de um tipo permitido (" & String.Join(", ", gcExtensoesAnexoBaseConhecimento) & ").")
                    End If

                    If oInfo.Length > gcTamanhoMaximoAnexo Then
                        Throw New ArgumentException("O arquivo '" & oInfo.Name & "' excede o limite de " & (gcTamanhoMaximoAnexo / 1048576) & " MB.")
                    End If

                    'Somente o nome do arquivo (sem acento) na pasta da web. Se já existir um arquivo
                    'com o mesmo nome (de outro artigo), acrescenta um sufixo para não sobrescrevê-lo
                    Dim sNome As String = NomeArquivoSeguro(oInfo.Name)
                    Dim iSufixo As Integer = 1

                    Do While oFtp.Existe(gcPastaFtpAnexoBaseConhecimento & sNome)
                        sNome = System.IO.Path.GetFileNameWithoutExtension(NomeArquivoSeguro(oInfo.Name)) & "_" & iSufixo & System.IO.Path.GetExtension(NomeArquivoSeguro(oInfo.Name))
                        iSufixo += 1
                    Loop

                    oFtp.Upload(gcPastaFtpAnexoBaseConhecimento & sNome, sCaminho)

                    Try
                        oClsUsrCadBaseConhecimento.InsertArquivo(lCodigoArtigo, sNome)
                    Catch
                        'Falha ao registrar no banco: remove somente o arquivo que acabou de ser enviado
                        Try
                            oFtp.Delete(gcPastaFtpAnexoBaseConhecimento & sNome)
                        Catch
                        End Try
                        Throw
                    End Try

                    iEnviados += 1

                Next

            Finally

                Call oClsUsrCadBaseConhecimento.LoadGridArquivo(grdArquivos, lCodigoArtigo)

            End Try

            Cursor.Current = Cursors.Default

            frmMain.Informacao(iEnviados & " arquivo(s) anexado(s) com sucesso!", Color.Blue)

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub ExcluirArquivo()

        Try

            With grdArquivos.CurrentRow.Cells

                If MsgBox("Deseja excluir o anexo '" & .Item("arquivo").Value.ToString & "'?", _
                          MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gAplicacao) = MsgBoxResult.No Then Exit Sub

                Cursor.Current = Cursors.WaitCursor

                'Igual à web: remove o vínculo; o arquivo fica no FTP (outros artigos podem usar o mesmo nome)
                oClsUsrCadBaseConhecimento.DeleteArquivo(CLng(.Item("codigo").Value))

            End With

            Call oClsUsrCadBaseConhecimento.LoadGridArquivo(grdArquivos, lCodigoArtigo)

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Anexo excluído com sucesso!", Color.Blue)

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub VisualizarArquivo()

        Try

            Dim sArquivo As String = grdArquivos.CurrentRow.Cells.Item("arquivo").Value.ToString

            Cursor.Current = Cursors.WaitCursor

            Dim sDestino As String = System.IO.Path.Combine(PastaArquivosTemporarios(), System.IO.Path.GetFileName(sArquivo))

            oFtp.Download(gcPastaFtpAnexoBaseConhecimento & sArquivo, sDestino)

            Cursor.Current = Cursors.Default

            Call AbrirArquivoLocal(sDestino)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#End Region

End Class
