'===============================================================================================
'Módulo:        modFunction
'Objetivo:      Funções compartilhadas do Documentacti (subconjunto do modFunction do INTERACTI,
'               adaptado para MySQL)
'===============================================================================================
Imports System.IO
Imports Janus.Windows.GridEX
Imports Janus.Windows.GridEX.EditControls
Imports Janus.Windows.EditControls
Imports Janus.Windows.UI.Tab
Imports Janus.Windows.CalendarCombo
Imports MySqlConnector
Imports Documentacti.MySQLHelper

Public Module modFunction

#Region "::: GERAL :::"

    Public Function ApplicationPath() As String

        Return System.AppDomain.CurrentDomain.BaseDirectory()

    End Function

    Public Function Cripitografar(ByVal sTexto As String) As String

        Try

            'Variaveis Locais
            Dim sLetra As String
            Dim sTextoCriptografada As String = ""
            Dim i As Integer

            sTexto = Trim(sTexto)

            For i = 1 To Len(sTexto)
                sLetra = Asc(Mid(sTexto, i, 1))
                sLetra = Chr(Trim(Str(Val(sLetra) Xor (i * 2))))
                sTextoCriptografada = sTextoCriptografada & sLetra
            Next i

            Return sTextoCriptografada

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function NomeArquivoSeguro(ByVal sNome As String) As String

        'Remove acentos e troca caracteres inválidos por "_" (nome seguro para FTP)
        Try

            Dim sNormal As String = sNome.Normalize(System.Text.NormalizationForm.FormD)
            Dim oBuilder As New System.Text.StringBuilder

            For Each cLetra As Char In sNormal
                If System.Globalization.CharUnicodeInfo.GetUnicodeCategory(cLetra) <> System.Globalization.UnicodeCategory.NonSpacingMark Then
                    oBuilder.Append(cLetra)
                End If
            Next

            Return System.Text.RegularExpressions.Regex.Replace(oBuilder.ToString.Normalize(System.Text.NormalizationForm.FormC), "[^A-Za-z0-9._-]", "_")

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function FormatarDuracao(ByVal lSegundos As Long) As String

        Try

            If lSegundos < 0 Then lSegundos = 0

            Return Format(Math.Floor(lSegundos / 3600), "00") & ":" & _
                   Format(Math.Floor((lSegundos Mod 3600) / 60), "00") & ":" & _
                   Format(lSegundos Mod 60, "00")

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub TratamentoErro(ByVal oErro As Exception, _
                              ByVal sFormulario As String)

        Call TratamentoErro(oErro.Message, sFormulario, oErro.ToString)

    End Sub

    Public Sub TratamentoErro(ByVal sErro As String, _
                              ByVal sFormulario As String, _
                              Optional ByVal sPilha As String = "")

        Try

            'Grava log local do erro
            Dim sDiretorio As String = ApplicationPath() & "Log\"
            If Directory.Exists(sDiretorio) = False Then Directory.CreateDirectory(sDiretorio)

            File.AppendAllText(sDiretorio & "erro_" & Format(Now, "yyyyMMdd") & ".log", _
                               Format(Now, "dd/MM/yyyy HH:mm:ss") & " | " & goUsuario.sUsuario & " | " & sFormulario & " | " & sErro & vbCrLf & _
                               IIf(sPilha = "", "", sPilha & vbCrLf))

        Catch
            'Falha ao gravar o log não deve esconder o erro original
        End Try

        'Informa o Usuário sobre o Erro Ocorrido
        frmMain.Informacao(Mensagem.Erro, sErro)

        MsgBox(sErro, MsgBoxStyle.Exclamation, gAplicacao)

        'Seta Cursor do Mouse
        Cursor.Current = Cursors.Default

    End Sub

#End Region

#Region "::: CONTROLES :::"

    Public Sub Control_GotFocus(ByVal sender As Object, ByVal e As EventArgs)

        Try

            'Verifica o Tipo de Controle
            If Not TypeOf (sender) Is UIButton And _
                Not TypeOf (sender) Is Button And _
                Not TypeOf (sender) Is GridEX And _
                Not TypeOf (sender) Is UserControl And _
                Not TypeOf (sender) Is UITab And _
                Not TypeOf (sender) Is UICheckBox And _
                Not TypeOf (sender) Is Label And _
                sender.BackColor = System.Drawing.SystemColors.Window Then

                'Seta Cor do Controle
                sender.BackColor = System.Drawing.SystemColors.Info

                'Limpa Controle
                sControlValue = ""

                If TypeOf (sender) Is MaskedEditBox Then
                    sControlValue = CType(sender, MaskedEditBox).Text
                ElseIf TypeOf (sender) Is EditBox Then
                    sControlValue = CType(sender, EditBox).Text
                ElseIf TypeOf (sender) Is CalendarCombo Then
                    sControlValue = CType(sender, CalendarCombo).Value
                End If

            ElseIf TypeOf (sender) Is UITab Then

                'Desabilita propriedades - Tab
                Dim oUITab As UITab = sender
                oUITab.TabStop = False
                oUITab.ShowFocusRectangle = False

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub Control_LostFocus(ByVal sender As Object, ByVal e As EventArgs)

        Try

            'Verifica o Tipo de Controle
            If Not TypeOf (sender) Is UIButton And _
                Not TypeOf (sender) Is Button And _
                Not TypeOf (sender) Is GridEX And _
                Not TypeOf (sender) Is UserControl And _
                Not TypeOf (sender) Is UITab And _
                Not TypeOf (sender) Is UICheckBox And _
                Not TypeOf (sender) Is Label And _
                sender.BackColor = System.Drawing.SystemColors.Info Then

                'Seta Cor do Controle
                sender.BackColor = System.Drawing.SystemColors.Window

                If TypeOf (sender) Is MaskedEditBox Then
                    bChangeControl = IIf(sControlValue = CType(sender, MaskedEditBox).Text And bChangeControl, False, True)
                ElseIf TypeOf (sender) Is EditBox Then
                    bChangeControl = IIf(sControlValue = CType(sender, EditBox).Text And bChangeControl, False, True)
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub Control_Enter(ByVal sender As Object)

        Try

            'Verifica o Tipo de Controle
            If Not TypeOf (sender) Is UIButton And _
                Not TypeOf (sender) Is GridEX And _
                Not TypeOf (sender) Is UserControl And _
                Not TypeOf (sender) Is UITab And _
                Not (TypeOf (sender) Is EditBox AndAlso CType(sender, EditBox).Multiline = True) And _
                sender.name <> "" Then

                'Muda para o próximo Controle
                System.Windows.Forms.SendKeys.Send("{TAB}")

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub AddKeyDown(ByVal oControlParent As Control, _
                          ByRef oDelegate As DelegateKeyDown)

        Try

            'Váriavel - Controle
            Dim oControl As Control

            For Each oControl In oControlParent.Controls

                If Not TypeOf (oControl) Is UIGroupBox And _
                    Not TypeOf (oControl) Is Button And _
                    Not TypeOf (oControl) Is UIButton Then

                    'Adiciona Evento
                    AddHandler oControl.KeyDown, AddressOf oDelegate.Invoke

                End If

                'Verifica se existe Controles Internos
                If oControl.Controls.Count > 0 Then
                    AddKeyDown(oControl, oDelegate)
                End If

            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub AddGotFocus(ByVal oControlParent As Control, _
                           ByRef oDelegate As DelegateGotFocus)

        Try

            'Váriavel - Controle
            Dim oControl As Control

            For Each oControl In oControlParent.Controls

                'Adiciona Evento ao Controle
                AddHandler oControl.GotFocus, AddressOf oDelegate.Invoke

                'Verifica se existe Controles Internos
                If oControl.Controls.Count > 0 Then
                    AddGotFocus(oControl, oDelegate)
                End If

            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub AddLostFocus(ByVal oControlParent As Control, _
                            ByRef oDelegate As DelegateLostFocus)

        Try

            'Váriavel - Controle
            Dim oControl As Control

            For Each oControl In oControlParent.Controls

                'Adiciona Evento ao Controle
                AddHandler oControl.LostFocus, AddressOf oDelegate.Invoke

                'Verifica se existe Controles Internos
                If oControl.Controls.Count > 0 Then
                    AddLostFocus(oControl, oDelegate)
                End If

            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "::: VALIDAÇÃO :::"

    Public Function ValidaCampo(ByVal oTextBox As EditBox, _
                                ByVal oControl As Control, _
                                ByVal eTipo As TipoCampo) As Boolean

        Try

            'Váriaveis
            Dim sCampo As String = Left(oControl.Text, oControl.Text.Length - 1).ToUpper

            'Seta Retorno da Função
            ValidaCampo = False

            If oTextBox.Text.Trim.Length = 0 Then
                frmMain.errInfo.SetError(oControl, "O Campo [" & sCampo & "] é obrigatório. Digite um valor para ele.")
                oTextBox.Focus()
                Exit Function
            End If

            Select Case eTipo
                Case TipoCampo.numero
                    If IsNumeric(oTextBox.Text) = False Then
                        frmMain.errInfo.SetError(oControl, "O Campo [" & sCampo & "] é numérico. Digite um valor válido para ele.")
                        oTextBox.Focus()
                        Exit Function
                    End If
                Case TipoCampo.data, TipoCampo.hora
                    If IsDate(oTextBox.Text) = False Then
                        frmMain.errInfo.SetError(oControl, "O Campo [" & sCampo & "] é do tipo Data. Digite um valor válido para ele.")
                        oTextBox.Focus()
                        Exit Function
                    End If
            End Select

            'Seta Retorno da Função
            ValidaCampo = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ValidaCampo(ByVal oMaskTextBox As MaskedEditBox, _
                                ByVal oControl As Control, _
                                ByVal eTipo As TipoCampo) As Boolean

        Try

            'Váriaveis
            Dim sCampo As String = Left(oControl.Text, oControl.Text.Length - 1).ToUpper

            'Seta Retorno da Função
            ValidaCampo = False

            If oMaskTextBox.Text.Trim.Length = 0 Then
                frmMain.errInfo.SetError(oControl, "O Campo [" & sCampo & "] é obrigatório. Digite um valor para ele.")
                oMaskTextBox.Focus()
                Exit Function
            End If

            Select Case eTipo
                Case TipoCampo.numero
                    If IsNumeric(oMaskTextBox.Text) = False Then
                        frmMain.errInfo.SetError(oControl, "O Campo [" & sCampo & "] é numérico. Digite um valor válido para ele.")
                        oMaskTextBox.Focus()
                        Exit Function
                    End If
                Case TipoCampo.data, TipoCampo.hora
                    If IsDate(oMaskTextBox.Text) = False Then
                        frmMain.errInfo.SetError(oControl, "O Campo [" & sCampo & "] é do tipo Data. Digite um valor válido para ele.")
                        oMaskTextBox.Focus()
                        Exit Function
                    End If
            End Select

            'Seta Retorno da Função
            ValidaCampo = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ValidaCampo(ByVal oComboBox As UIComboBox, _
                                ByVal oControl As Control) As Boolean

        Try

            'Seta Retorno da Função
            ValidaCampo = False

            'Váriaveis
            Dim sCampo As String = Left(oControl.Text, oControl.Text.Length - 1).ToUpper

            'Verifica se foi Selecionado algum valor
            If oComboBox.SelectedIndex = -1 Then
                frmMain.errInfo.SetError(oControl, "O Campo [" & sCampo & "] é obrigatório. Selecione um valor para ele.")
                oComboBox.Focus()
                Exit Function
            End If

            'Seta Retorno da Função
            ValidaCampo = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ValidaCampo(ByVal oComboBox As CheckedComboBox, _
                                ByVal oControl As Control) As Boolean

        Try

            'Seta Retorno da Função
            ValidaCampo = False

            'Váriaveis
            Dim sCampo As String = Left(oControl.Text, oControl.Text.Length - 1).ToUpper

            'Verifica se foi Selecionado algum valor
            If GetCheckedValues(oComboBox) = "" Then
                frmMain.errInfo.SetError(oControl, "O Campo [" & sCampo & "] é obrigatório. Selecione ao menos um valor para ele.")
                oComboBox.Focus()
                Exit Function
            End If

            'Seta Retorno da Função
            ValidaCampo = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "::: GRID / COMBO :::"

    Public Sub ConfiguraGrid(ByVal oGrid As GridEX)

        'Variaveis Locais
        Dim oColumn As GridEXColumn

        Try

            With oGrid

                .SelectedFormatStyle.BackColor = System.Drawing.Color.FromArgb(192, 255, 192)

                .GroupTotals = GroupTotals.Always

                'Seta Forma de Agrupar
                .GroupRowVisualStyle = GroupRowVisualStyle.Outlook2003
                .GroupRowFormatStyle.ForeColor = System.Drawing.Color.SteelBlue
                .GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
                .RootTable.CellToolTip = CellToolTip.TruncatedText
                .RootTable.TableHeaderFormatStyle.ForeColor = System.Drawing.Color.Blue
                .RootTable.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
                .RootTable.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .FocusCellFormatStyle.BackColor = System.Drawing.Color.Empty

                For Each oColumn In oGrid.RootTable.Columns

                    'Colunas com Tag "A" somente para administrador
                    If IsNothing(oColumn.Tag) = False AndAlso oColumn.Tag.ToString = "A" AndAlso goUsuario.bAdmin = False Then
                        oColumn.Visible = False
                    End If

                    'Seta Propriedade do Controle
                    oColumn.HideWhenGrouped = InheritableBoolean.True

                    'Botões da grid (colunas de imagem): tooltip e largura
                    Select Case oColumn.Key
                        Case "editar" : oColumn.CellToolTipText = "Duplo clique para editar." : oColumn.CellToolTip = CellToolTip.UseCellToolTipText : oColumn.Width = 20
                        Case "cancelar" : oColumn.CellToolTipText = "Duplo clique para cancelar." : oColumn.CellToolTip = CellToolTip.UseCellToolTipText : oColumn.Width = 20
                        Case "visualizar" : oColumn.CellToolTipText = "Duplo clique para visualizar." : oColumn.CellToolTip = CellToolTip.UseCellToolTipText : oColumn.Width = 20
                        Case "reapontar" : oColumn.CellToolTipText = "Duplo clique para reapontar este chamado." : oColumn.CellToolTip = CellToolTip.UseCellToolTipText : oColumn.Width = 20
                        Case "excluir" : oColumn.CellToolTipText = "Duplo clique para excluir o anexo." : oColumn.CellToolTip = CellToolTip.UseCellToolTipText : oColumn.Width = 20
                        Case "encerrar" : oColumn.CellToolTipText = "Duplo clique para encerrar o apontamento." : oColumn.CellToolTip = CellToolTip.UseCellToolTipText : oColumn.Width = 20
                    End Select

                Next

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ConfigurarDataMemberGrid(ByVal oGrid As GridEX)

        Try

            'Percorrendo as colunas
            For Each oCol As GridEXColumn In oGrid.RootTable.Columns
                'Não atribuir o data member para a coluna de seleção
                If oCol.Key <> "x" Then
                    'Atribuindo o datamember com o mesmo nome da coluna
                    oGrid.RootTable.Columns.Item(oCol.Key).DataMember = oCol.Key
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ConfigurarDataCheckedComboBox(ByVal oCheckedComboBox As CheckedComboBox)

        Try

            'Percorrendo as colunas
            For Each oCol As GridEXColumn In oCheckedComboBox.DropDownList.Columns
                'Não atribuir o data member para a coluna de seleção
                If oCol.Key <> "x" Then
                    'Atribuindo o datamember com o mesmo nome da coluna
                    oCheckedComboBox.DropDownList.Columns.Item(oCol.Key).DataMember = oCol.Key
                End If
            Next

            oCheckedComboBox.DropDownDisplayMember = "descricao"
            oCheckedComboBox.DropDownValueMember = "codigo"
            oCheckedComboBox.DropDownList.GridLineStyle = GridLineStyle.Solid
            oCheckedComboBox.DropDownList.SelectedFormatStyle.BackColor = Nothing
            oCheckedComboBox.DropDownList.SelectedFormatStyle.ForeColor = Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function ParametroEmpresa() As MySqlParameter()

        'Parâmetro padrão das procs de combo (p_codigo_empresa)
        Dim oMySqlParameter(0) As MySqlParameter

        oMySqlParameter(0) = New MySqlParameter
        oMySqlParameter(0).ParameterName = "@p_codigo_empresa"
        oMySqlParameter(0).Direction = ParameterDirection.Input
        oMySqlParameter(0).MySqlDbType = MySqlDbType.Int32
        oMySqlParameter(0).Value = goUsuario.iEmpresa

        Return oMySqlParameter

    End Function

    Public Sub LoadCombo(ByVal oComboBox As UIComboBox, _
                         ByVal sProcedure As String, _
                         Optional ByVal oMySqlParameter() As MySqlParameter = Nothing)

        'Variaveis Locais
        Dim oDataSet As DataSet

        Try

            If IsNothing(oMySqlParameter) Then oMySqlParameter = ParametroEmpresa()

            'Obtem o DataSet
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, sProcedure, oMySqlParameter)

            'Carrega Combo
            With oComboBox

                .DataSource = oDataSet.Tables(0)
                .ValueMember = "codigo"
                .DisplayMember = "descricao"
                .SelectedIndex = -1

            End With

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub LoadCombo(ByVal oComboBox As CheckedComboBox, _
                         ByVal oTabela As DataTable)

        'Carrega a partir de uma tabela já montada (colunas codigo / descricao)
        Try

            With oComboBox

                .DropDownDataSource = Nothing
                Call ConfigurarDataCheckedComboBox(oComboBox)
                .DropDownDataSource = oTabela
                .DropDownDataMember = ""

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub LoadComboLista(ByVal oComboBox As UIComboBox, ByVal sLista As String)

        Try

            'Lista fixa no formato "CODIGO=Descrição;CODIGO=Descrição"
            Dim oTabela As New DataTable
            oTabela.Columns.Add("codigo", GetType(String))
            oTabela.Columns.Add("descricao", GetType(String))

            For Each sItem As String In sLista.Split(";"c)
                oTabela.Rows.Add(sItem.Split("="c)(0), sItem.Split("="c)(1))
            Next

            With oComboBox
                .DataSource = oTabela
                .ValueMember = "codigo"
                .DisplayMember = "descricao"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub LoadCombo(ByVal oComboBox As CheckedComboBox, _
                         ByVal sProcedure As String)

        'Variaveis Locais
        Dim oDataSet As DataSet

        Try

            'Obtem o DataSet
            oDataSet = ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, sProcedure, ParametroEmpresa())

            'Carrega Combo
            With oComboBox

                .DropDownDataSource = Nothing
                Call ConfigurarDataCheckedComboBox(oComboBox)
                .DropDownDataSource = oDataSet.Tables(0)
                .DropDownDataMember = ""

            End With

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function GetCheckedValues(ByVal oComboBox As CheckedComboBox) As String

        'Retorna os valores marcados em formato CSV (ex.: "1,3")
        Dim sRetorno As String = ""

        Try

            If IsNothing(oComboBox.CheckedValues) = False Then
                For Each oValor As Object In oComboBox.CheckedValues
                    If IsNothing(oValor) = False AndAlso IsDBNull(oValor) = False Then
                        sRetorno &= IIf(sRetorno = "", "", ",") & oValor.ToString
                    End If
                Next
            End If

            Return sRetorno

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub SetCheckedValues(ByVal oComboBox As CheckedComboBox, ByVal sValores As String)

        'Marca os valores informados em formato CSV
        Try

            Dim oValores As New List(Of Object)

            For Each sValor As String In IIf(IsDBNull(sValores) Or IsNothing(sValores), "", sValores).ToString.Split(",")
                If sValor.Trim <> "" AndAlso IsNumeric(sValor) Then
                    oValores.Add(CLng(sValor.Trim))
                End If
            Next

            oComboBox.UncheckAll()

            If oValores.Count > 0 Then
                'Converte para o mesmo tipo da coluna de valor do DataSource
                Dim oTable As DataTable = TryCast(oComboBox.DropDownDataSource, DataTable)
                If IsNothing(oTable) = False Then
                    Dim oTipo As Type = oTable.Columns("codigo").DataType
                    oComboBox.CheckedValues = oValores.Select(Function(v) Convert.ChangeType(v, oTipo)).ToArray
                Else
                    oComboBox.CheckedValues = oValores.ToArray
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function PastaArquivosTemporarios() As String

        'Pasta local onde os arquivos baixados do FTP são gravados antes de abrir
        Dim sPasta As String = Path.Combine(Path.GetTempPath, "Documentacti")

        Directory.CreateDirectory(sPasta)

        Return sPasta

    End Function

    Public Sub AbrirArquivoLocal(ByVal sCaminho As String)

        Try

            'Arquivos executáveis/scripts não são abertos automaticamente
            Dim sExtensoesBloqueadas As String = ".exe.bat.cmd.com.msi.scr.js.jse.vbs.vbe.ps1.lnk.jar.hta.reg.dll.wsf"

            If sExtensoesBloqueadas.Contains(Path.GetExtension(sCaminho).ToLower) AndAlso Path.GetExtension(sCaminho) <> "" Then
                MsgBox("Por segurança, este tipo de arquivo não é aberto automaticamente." & vbCrLf & "O arquivo foi salvo em:" & vbCrLf & sCaminho, MsgBoxStyle.Information, gAplicacao)
                Process.Start("explorer.exe", "/select," & Chr(34) & sCaminho & Chr(34))
            Else
                Process.Start(sCaminho)
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ExportExcel(ByVal oGrid As GridEX)

        Try

            'Váriavel - Dialog
            Dim oFileDialog As New SaveFileDialog
            oFileDialog.Filter = "Excel|*.xls"

            'Abrindo um save dialog para salvar o arquivo
            If oFileDialog.ShowDialog() <> DialogResult.OK Then
                Exit Sub
            End If

            'Seta Cursor do Mouse
            Cursor.Current = Cursors.WaitCursor

            'Instanciando a classe para exportar para o Excel
            Dim oGridEXExporter As New Janus.Windows.GridEX.Export.GridEXExporter()
            oGridEXExporter.GridEX = oGrid

            'Exportando
            Using oStream As New System.IO.FileStream(oFileDialog.FileName, System.IO.FileMode.Create)
                oGridEXExporter.Export(oStream)
            End Using

            'Seta Cursor do Mouse
            Cursor.Current = Cursors.Default

            'Iniciando o arquivo
            Process.Start(oFileDialog.FileName)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Module
