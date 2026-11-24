Imports Janus.Windows.GridEX
Imports Janus.Windows.EditControls
Imports MySqlConnector

Public Class usrCadEmpresa

#Region "::: VARIAVEIS :::"

    Private oClsUsrCadEmpresa As New clsUsrCadEmpresa
    Private oFtp As New clsFtp

    'Código da empresa em edição (a PK não aparece na tela)
    Private lCodigoEmpresa As Long = 0

    'Campos que a tela não altera, mas a sp_update_cadastro_empresa regrava
    Private sArquivoAtual As String = ""
    Private sModulosOriginais As String = ""

    'Responsável BKP gravado (mantido ao recarregar o combo pela troca do time)
    Private lResponsavelBkpAtual As Long = 0

    'Evita recarregar combos dependentes durante o preenchimento da tela
    Private bCarregando As Boolean = False

    'Anexos da empresa ficam na pasta da plataforma web (relativa à pasta de anexos de chamado do Config.xml)
    Private Const gcPastaFtpAnexoEmpresa As String = "../../../ajuda/listagem/uploads/"

#End Region

#Region "::: CONTROLES :::"

#Region "::: GERAL :::"

    Private Sub usrCadEmpresa_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Call ConfigurarUserControl()

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Try

            Call ExportExcel(grdListagem)

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnAgruparListagem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgruparListagem.Click

        Try

            Call AlternarAgrupamentoGrid(grdListagem)

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
            Call TratamentoErro(ex.Message, "Empresas")
        End Try

    End Sub

    Private Sub btnVoltar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoltar.Click

        Try

            frmMain.errInfo.Clear()

            Call LoadGrid()

            Call MostrarLista()

            txtCnpjCpfFiltro.Focus()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdListagem_RowDoubleClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdListagem.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            If IsNothing(grdListagem.CurrentColumn) Then Exit Sub
            If grdListagem.CurrentColumn.Key = "editar" Then Call Editar()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdArquivos_RowDoubleClick(sender As Object, e As RowActionEventArgs) Handles grdArquivos.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            If IsNothing(grdArquivos.CurrentColumn) Then Exit Sub
            If grdArquivos.CurrentColumn.Key = "visualizar" Then Call VisualizarArquivo()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub cboTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTime.SelectedIndexChanged

        Try

            If bCarregando = True Then Exit Sub

            'Responsável BKP: somente usuários do time da empresa
            Call LoadComboResponsavelBkp()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub cboTipoAcesso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTipoAcesso.SelectedIndexChanged, cboTipoAcesso.Leave

        Try

            If bCarregando = True Then Exit Sub

            'Sugestões de subtipo já usadas para o tipo informado
            Call LoadComboSubtipoAcesso()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub btnAdicionarAcesso_Click(sender As Object, e As EventArgs) Handles btnAdicionarAcesso.Click

        Try

            Call AdicionarAcesso()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub txtValorAcesso_KeyDown(sender As Object, e As KeyEventArgs) Handles txtValorAcesso.KeyDown

        Try

            'Enter no valor inclui a informação (mesmo comportamento do botão Adicionar)
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                Call AdicionarAcesso()
            End If

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub grdAcesso_CellUpdated(sender As Object, e As ColumnActionEventArgs) Handles grdAcesso.CellUpdated

        Try

            Select Case e.Column.Key

                Case "tipo", "subtipo", "valor"
                    Call AtualizarAcesso(grdAcesso.CurrentRow)

            End Select

        Catch ex As Exception
            Call TratarErro(ex)
            'Recarrega para descartar o valor não gravado
            Me.BeginInvoke(New MethodInvoker(AddressOf RecarregarAcesso))
        End Try

    End Sub

    Private Sub grdAcesso_RowDoubleClick(sender As Object, e As RowActionEventArgs) Handles grdAcesso.RowDoubleClick

        Try

            If IsNothing(e.Row) OrElse e.Row.RowType <> RowType.Record Then Exit Sub

            If IsNothing(grdAcesso.CurrentColumn) Then Exit Sub
            If grdAcesso.CurrentColumn.Key = "excluir" Then Call ExcluirAcesso()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub usrCadEmpresa_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

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
            Call AddKeyDown(Me, New DelegateKeyDown(AddressOf usrCadEmpresa_KeyDown))
            Call AddLostFocus(Me, New DelegateLostFocus(AddressOf Control_LostFocus))
            'Tipo e Subtipo de acesso ficam fora: são texto livre com sugestões (não voltam a seleção ao sair)
            Call AddComboAutoComplete(New UIComboBox() {cboAtivoFiltro, cboTimeFiltro, cboAtivo, cboTime, cboResponsavelBkp})

            'Combos - Filtro
            'Padrão: somente empresas ativas, do mesmo time do usuário (TODOS quando o usuário não tem time)
            Call LoadComboLista(cboAtivoFiltro, "-1=TODOS;1=SIM;0=NÃO")
            cboAtivoFiltro.SelectedValue = "1"
            Call LoadCombo(cboTimeFiltro, "sp_select_combo_tipo_time_empresa")
            Dim oTimes As DataTable = CType(cboTimeFiltro.DataSource, DataTable)
            oTimes.Rows.InsertAt(oTimes.NewRow, 0)
            oTimes.Rows(0).Item("codigo") = -1
            oTimes.Rows(0).Item("descricao") = "TODOS"
            'Seleção pela posição: o código vem do banco como Int16 e não casa com um Integer no SelectedValue
            Dim oTimeUsuario() As DataRow = oTimes.Select("codigo = " & goUsuario.iTime)
            cboTimeFiltro.SelectedIndex = If(goUsuario.iTime > 0 AndAlso oTimeUsuario.Length > 0, oTimes.Rows.IndexOf(oTimeUsuario(0)), 0)

            'Combos - Dados
            Call LoadComboLista(cboAtivo, "1=SIM;0=NÃO")
            Call LoadCombo(cboTime, "sp_select_combo_tipo_time_empresa")
            Call LoadCombo(cboModulo, oClsUsrCadEmpresa.LoadModulos())

            tabMain.TabPages.Remove(pagDados)

            Call ConfiguraGrid(grdListagem)
            Call ConfiguraGrid(grdArquivos)
            Call ConfiguraGrid(grdHistorico)
            Call ConfiguraGrid(grdAcesso)
            grdAcesso.RootTable.Columns("excluir").CellToolTipText = "Duplo clique para excluir a informação de acesso."

            Call LoadGrid()

            txtCnpjCpfFiltro.Focus()

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
            Call TratamentoErro(ex, "Empresas")
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

            Call oClsUsrCadEmpresa.LoadGrid(grdListagem, _
                                            txtCnpjCpfFiltro.Text.Trim, _
                                            txtRazaoSocialFiltro.Text.Trim, _
                                            CInt(cboAtivoFiltro.SelectedValue), _
                                            CInt(cboTimeFiltro.SelectedValue))

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Editar()

        Try

            Cursor.Current = Cursors.WaitCursor

            Dim lCodigo As Long = CLng(grdListagem.CurrentRow.Cells.Item("codigo").Value)
            Dim oTabela As DataTable = oClsUsrCadEmpresa.LoadDados(lCodigo)

            'A proc da web só devolve a empresa quando ela tem ao menos um módulo ativo vinculado
            If oTabela.Rows.Count = 0 Then
                Cursor.Current = Cursors.Default
                MsgBox("Não foi possível carregar os dados da empresa " & lCodigo & "." & vbCrLf & _
                       "Verifique se ela possui módulos ativos vinculados.", MsgBoxStyle.Exclamation, gAplicacao)
                Exit Sub
            End If

            bCarregando = True

            With oTabela.Rows(0)

                lCodigoEmpresa = CLng(.Item("codigo"))
                txtCnpjCpf.Text = .Item("cnpj_cpf").ToString
                txtRazaoSocial.Text = .Item("razao_social").ToString
                txtNomeFantasia.Text = .Item("nome_fantasia").ToString
                txtTelefone.Text = .Item("telefone").ToString
                'ativo é TINYINT(1): o conector devolve Boolean
                cboAtivo.SelectedValue = IIf(IsDBNull(.Item("ativo")) OrElse CBool(.Item("ativo")) = False, "0", "1")
                If IsDBNull(.Item("tipo_time")) Then cboTime.SelectedIndex = -1 Else cboTime.SelectedValue = .Item("tipo_time")
                lResponsavelBkpAtual = IIf(IsDBNull(.Item("codigo_usuario_bkp")), 0, .Item("codigo_usuario_bkp"))
                sModulosOriginais = .Item("modulo_id").ToString
                Call SetCheckedValues(cboModulo, sModulosOriginais)
                txtFocalPoint.Text = .Item("focal_point").ToString
                txtCelular.Text = .Item("celular").ToString
                txtEmail.Text = .Item("email").ToString
                'A web grava quebra de linha só com LF; a caixa de texto do Windows precisa de CRLF
                txtObservacao.Text = .Item("observacao").ToString.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
                sArquivoAtual = .Item("arquivo").ToString

            End With

            bCarregando = False

            'Responsável BKP depende do time já selecionado (limpa a seleção da empresa editada antes)
            cboResponsavelBkp.SelectedIndex = -1
            Call LoadComboResponsavelBkp()

            tabDadosEmpresa.SelectedTab = pagAcesso

            Call LoadComboTipoAcesso()
            Call LimparAcesso()
            Call LoadGridAcesso()
            Call oClsUsrCadEmpresa.LoadGridArquivo(grdArquivos, lCodigo)
            Call oClsUsrCadEmpresa.LoadGridHistorico(grdHistorico, lCodigo)

            Call MostrarDados()

            txtCnpjCpf.Focus()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Salvar()

        Try

            If MsgBox("Deseja salvar as alterações da empresa " & txtNomeFantasia.Text.Trim & "?", _
                      MsgBoxStyle.Question + MsgBoxStyle.YesNo, gAplicacao) = MsgBoxResult.No Then Exit Sub

            Cursor.Current = Cursors.WaitCursor

            Dim oDados As New Dictionary(Of String, Object)

            oDados("codigo_empresa") = CInt(lCodigoEmpresa)
            oDados("cnpj_cpf") = txtCnpjCpf.Text.Trim
            oDados("razao_social") = txtRazaoSocial.Text.Trim
            oDados("nome_fantasia") = txtNomeFantasia.Text.Trim
            oDados("ativo") = CInt(cboAtivo.SelectedValue)
            oDados("telefone") = txtTelefone.Text.Trim
            oDados("arquivo") = IIf(sArquivoAtual = "", Nothing, sArquivoAtual)
            oDados("modulo_empresa") = ModulosSelecionados()
            oDados("tipo_time") = CInt(cboTime.SelectedValue)
            oDados("codigo_usuario_bkp") = IIf(cboResponsavelBkp.SelectedIndex = -1, Nothing, cboResponsavelBkp.SelectedValue)
            oDados("focal_point") = txtFocalPoint.Text.Trim
            oDados("celular") = txtCelular.Text.Trim
            oDados("email") = txtEmail.Text.Trim
            oDados("observacao") = txtObservacao.Text.Trim.Replace(vbCrLf, vbLf) 'Mantém o formato da web (LF)

            'As informações de acesso são gravadas linha a linha na grid (não dependem do Salvar)
            Call oClsUsrCadEmpresa.Update(oDados)

            sModulosOriginais = oDados("modulo_empresa").ToString
            lResponsavelBkpAtual = IIf(IsNothing(oDados("codigo_usuario_bkp")), 0, oDados("codigo_usuario_bkp"))

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Registro alterado com sucesso!", Color.Blue)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function ModulosSelecionados() As String

        'Módulos marcados + códigos já gravados que não aparecem no combo (não se perdem ao salvar pelo Documentacti)
        Try

            Dim oCodigos As New List(Of String)(GetCheckedValues(cboModulo).Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries))
            Dim oLista As DataTable = CType(cboModulo.DropDownDataSource, DataTable)

            For Each sCodigo As String In sModulosOriginais.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
                If IsNumeric(sCodigo) AndAlso oLista.Select("codigo = " & CInt(sCodigo)).Length = 0 AndAlso oCodigos.Contains(sCodigo.Trim) = False Then
                    oCodigos.Add(sCodigo.Trim)
                End If
            Next

            Return String.Join(",", oCodigos)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function Validacao() As Boolean

        Try

            Validacao = False

            If ValidaCampo(txtCnpjCpf, lblCnpjCpf, TipoCampo.texto) = False Then Exit Function
            If ValidaCampo(txtRazaoSocial, lblRazaoSocial, TipoCampo.texto) = False Then Exit Function
            If ValidaCampo(txtNomeFantasia, lblNomeFantasia, TipoCampo.texto) = False Then Exit Function
            If ValidaCampo(cboAtivo, lblAtivo) = False Then Exit Function
            If ValidaCampo(cboTime, lblTime) = False Then Exit Function
            If ValidaCampo(cboModulo, lblModulo) = False Then Exit Function

            Validacao = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub LoadComboResponsavelBkp()

        Try

            'Mantém a seleção atual (ou o responsável gravado) se ele continuar na lista do novo time
            Dim lSelecionado As Long = IIf(cboResponsavelBkp.SelectedIndex = -1, lResponsavelBkpAtual, CLng(cboResponsavelBkp.SelectedValue))
            Dim iTime As Integer = IIf(cboTime.SelectedIndex = -1, 0, CInt(cboTime.SelectedValue))

            Call LoadCombo(cboResponsavelBkp, "sp_select_combo_cadastro_empresa_usuario_bkp", oClsUsrCadEmpresa.ParametrosResponsavelBkp(iTime, lCodigoEmpresa))

            'Seleção pela posição: o código vem do banco como Int64 e não casa com outro tipo no SelectedValue
            Dim oLista As DataTable = CType(cboResponsavelBkp.DataSource, DataTable)
            Dim oLinha() As DataRow = oLista.Select("codigo = " & lSelecionado)
            cboResponsavelBkp.SelectedIndex = If(lSelecionado > 0 AndAlso oLinha.Length > 0, oLista.Rows.IndexOf(oLinha(0)), -1)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "::: ACESSO :::"

    Private Sub LoadGridAcesso()

        Try

            Call oClsUsrCadEmpresa.LoadGridAcesso(grdAcesso, lCodigoEmpresa)

            Call AtualizarSugestoesGridAcesso()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub RecarregarAcesso()

        Try

            Call LoadGridAcesso()

        Catch ex As Exception
            Call TratarErro(ex)
        End Try

    End Sub

    Private Sub LoadComboTipoAcesso()

        Try

            Dim sTexto As String = cboTipoAcesso.Text

            bCarregando = True
            Call LoadCombo(cboTipoAcesso, "sp_select_combo_cadastro_empresa_acesso_tipo")
            cboTipoAcesso.Text = sTexto
            bCarregando = False

            Call LoadComboSubtipoAcesso()

        Catch ex As Exception
            bCarregando = False
            Throw ex
        End Try

    End Sub

    Private Sub LoadComboSubtipoAcesso()

        Try

            'Mantém o que já foi digitado: o combo é texto livre com sugestões
            Dim sTexto As String = cboSubtipoAcesso.Text

            Call LoadCombo(cboSubtipoAcesso, "sp_select_combo_cadastro_empresa_acesso_subtipo", oClsUsrCadEmpresa.ParametrosSubtipoAcesso(cboTipoAcesso.Text.Trim))
            cboSubtipoAcesso.Text = sTexto

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AtualizarSugestoesGridAcesso()

        Try

            'Combos das colunas Tipo/Subtipo: sugestões (LimitToList = False permite digitar valor novo)
            For Each sColuna As String In {"tipo", "subtipo"}

                Dim oColuna As GridEXColumn = grdAcesso.RootTable.Columns(sColuna)
                Dim oValores As New List(Of String)

                Dim oSugestoes As DataTable = If(sColuna = "tipo", CType(cboTipoAcesso.DataSource, DataTable), oClsUsrCadEmpresa.LoadSugestaoSubtipoAcesso())

                For Each oDataRow As DataRow In oSugestoes.Rows
                    If oValores.Contains(oDataRow.Item("descricao").ToString) = False Then oValores.Add(oDataRow.Item("descricao").ToString)
                Next

                oColuna.HasValueList = True
                oColuna.ValueList.Clear()
                For Each sValor As String In oValores
                    oColuna.ValueList.Add(sValor, sValor)
                Next

            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub LimparAcesso()

        cboTipoAcesso.SelectedIndex = -1 : cboTipoAcesso.Text = ""
        cboSubtipoAcesso.SelectedIndex = -1 : cboSubtipoAcesso.Text = ""
        txtValorAcesso.Text = ""

    End Sub

    Private Sub AdicionarAcesso()

        Try

            If lCodigoEmpresa = 0 Then Exit Sub

            frmMain.errInfo.Clear()

            If cboTipoAcesso.Text.Trim = "" Then
                frmMain.errInfo.SetError(cboTipoAcesso, "Informe o tipo (ex.: MSTSC, VPN, SQL SERVER).")
                cboTipoAcesso.Focus()
                Exit Sub
            End If

            If cboSubtipoAcesso.Text.Trim = "" Then
                frmMain.errInfo.SetError(cboSubtipoAcesso, "Informe o subtipo (ex.: IP, USUÁRIO, SENHA).")
                cboSubtipoAcesso.Focus()
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor

            Call oClsUsrCadEmpresa.InsertAcesso(lCodigoEmpresa, cboTipoAcesso.Text.Trim, cboSubtipoAcesso.Text.Trim, txtValorAcesso.Text.Trim)

            'Mantém o tipo para incluir o próximo subtipo do mesmo acesso (ex.: IP, depois usuário e senha)
            Dim sTipo As String = cboTipoAcesso.Text.Trim
            Call LoadComboTipoAcesso()
            Call LimparAcesso()
            cboTipoAcesso.Text = sTipo
            Call LoadComboSubtipoAcesso()

            Call LoadGridAcesso()

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Informação de acesso incluída com sucesso!", Color.Blue)

            cboSubtipoAcesso.Focus()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AtualizarAcesso(ByVal oRow As GridEXRow)

        Try

            If IsNothing(oRow) OrElse oRow.RowType <> RowType.Record Then Exit Sub

            Cursor.Current = Cursors.WaitCursor

            Call oClsUsrCadEmpresa.UpdateAcesso(CLng(oRow.Cells("codigo").Value), _
                                                oRow.Cells("tipo").Value.ToString.Trim, _
                                                oRow.Cells("subtipo").Value.ToString.Trim, _
                                                oRow.Cells("valor").Value.ToString.Trim)

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Informação de acesso alterada com sucesso!", Color.Blue)

            'Recarrega após o evento (maiúsculas, usuário/data de alteração e novas sugestões)
            Call LoadComboTipoAcesso()
            Me.BeginInvoke(New MethodInvoker(AddressOf RecarregarAcesso))

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub ExcluirAcesso()

        Try

            With grdAcesso.CurrentRow

                If MsgBox("Deseja excluir a informação de acesso " & .Cells("tipo").Value.ToString & " / " & .Cells("subtipo").Value.ToString & "?", _
                          MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gAplicacao) = MsgBoxResult.No Then Exit Sub

                Cursor.Current = Cursors.WaitCursor

                Call oClsUsrCadEmpresa.DeleteAcesso(CLng(.Cells("codigo").Value))

            End With

            Call LoadGridAcesso()

            Cursor.Current = Cursors.Default

            frmMain.Informacao("Informação de acesso excluída com sucesso!", Color.Blue)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "::: ARQUIVOS :::"

    Private Sub VisualizarArquivo()

        Try

            'O FTP é configurado no XmlConfig\Config.xml (nó Documentacti/Ftp)
            If oFtp.Configurado() = False Then
                frmMain.Informacao("FTP dos anexos não configurado no Config.xml.", gColorInformacaoErro)
                Exit Sub
            End If

            Dim sArquivo As String = grdArquivos.CurrentRow.Cells.Item("arquivo").Value.ToString

            Cursor.Current = Cursors.WaitCursor

            Dim sDestino As String = System.IO.Path.Combine(PastaArquivosTemporarios(), System.IO.Path.GetFileName(sArquivo))

            oFtp.Download(gcPastaFtpAnexoEmpresa & sArquivo, sDestino)

            Cursor.Current = Cursors.Default

            Call AbrirArquivoLocal(sDestino)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#End Region

End Class
