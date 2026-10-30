'===============================================================================================
'Classe:        clsFrmMain
'Objetivo:      Atualização automática do executável (mesmo esquema do INTERACTI):
'               compara a versão do exe com a publicada em tb_adm_aplicativo e, se for diferente,
'               o próprio exe se substitui pela versão da pasta de rede, onde quer que esteja
'===============================================================================================
Imports System.Data
Imports System.IO
Imports MySqlConnector
Imports Documentacti.MySQLHelper

Public Class clsFrmMain

#Region "::: VARIAVEIS :::"

    Private Const gcAplicativo As String = "DOCUMENTACTI"

#End Region

#Region "::: FUNCTION / SUB :::"

    Public Function LoadVersaoAplicativo() As DataTable

        'Variaveis Locais
        Dim oMySqlParameter(0) As MySqlParameter
        Dim i As Integer = 0

        Try

            'Seta Parametros - Código Empresa
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_codigo_empresa"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.Int32
            oMySqlParameter(i).Value = goUsuario.iEmpresa : i += 1 : ReDim Preserve oMySqlParameter(i)

            'Seta Parametros - Aplicativo
            oMySqlParameter(i) = New MySqlParameter
            oMySqlParameter(i).ParameterName = "@p_aplicativo"
            oMySqlParameter(i).Direction = ParameterDirection.Input
            oMySqlParameter(i).MySqlDbType = MySqlDbType.VarChar
            oMySqlParameter(i).Value = gcAplicativo

            'Executa Query
            Return ExecuteDataset(goDatabase.sConnection, CommandType.StoredProcedure, "sp_select_administracao_aplicativo", oMySqlParameter).Tables(0)

        Catch MySqlEx As MySqlException
            Throw MySqlEx
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function VerificaVersaoAplicativo() As Boolean

        'Retorna True quando a atualização foi disparada e o sistema precisa fechar
        Try

            VerificaVersaoAplicativo = False

            'Executando pelo Visual Studio ou de uma pasta bin do projeto: não troca o exe do desenvolvedor
            If Debugger.IsAttached Then Exit Function
            If Application.ExecutablePath.ToUpper.Contains("\BIN\DEBUG\") OrElse Application.ExecutablePath.ToUpper.Contains("\BIN\RELEASE\") Then Exit Function

            Dim oTabela As DataTable

            Try
                oTabela = LoadVersaoAplicativo()
            Catch
                'Base sem a tabela/proc de versão: segue sem verificar
                Exit Function
            End Try

            If oTabela.Rows.Count = 0 Then Exit Function

            Dim sVersaoBanco As String = oTabela.Rows(0).Item("versao").ToString.Trim
            Dim sCaminho As String = oTabela.Rows(0).Item("caminho").ToString.Trim

            If sVersaoBanco = "" OrElse sVersaoBanco = Application.ProductVersion.ToString Then Exit Function

            If File.Exists(sCaminho) = False Then
                frmMain.Informacao("Versão " & sVersaoBanco & " disponível, mas o executável não foi encontrado ou seu login não tem acesso a " & sCaminho, gColorInformacaoErro)
                Exit Function
            End If

            'O exe da pasta precisa ser da versão publicada (evita trocar por um exe errado)
            Dim sVersaoArquivo As String = FileVersionInfo.GetVersionInfo(sCaminho).ProductVersion.Trim
            If sVersaoArquivo <> sVersaoBanco Then
                frmMain.Informacao("Informar ao suporte: o executável da pasta de atualização está na versão " & sVersaoArquivo & " e a versão publicada é " & sVersaoBanco & ".", gColorInformacaoErro)
                Exit Function
            End If

            'Onde este exe está (área de trabalho, C:\, etc.): a versão nova é copiada para o lado dele
            Dim sAtual As String = Application.ExecutablePath
            Dim sNovo As String = sAtual & ".novo"

            Cursor.Current = Cursors.WaitCursor
            File.Copy(sCaminho, sNovo, True)
            Cursor.Current = Cursors.Default

            MsgBox("O " & gAplicacao & " será atualizado para a versão " & sVersaoBanco & " e reaberto.", MsgBoxStyle.Information, "Atualização de Versão")

            'Um processo oculto espera este exe fechar, coloca o novo no lugar (mesmo nome) e reabre
            Call SubstituirExecutavel(sAtual, sNovo)

            VerificaVersaoAplicativo = True

        Catch ex As Exception
            Cursor.Current = Cursors.Default
            'Falha na atualização não impede o uso da versão atual
            frmMain.Informacao("Não foi possível atualizar a versão: " & ex.Message, gColorInformacaoErro)
            Return False
        End Try

    End Function

    Private Sub SubstituirExecutavel(ByVal sAtual As String, ByVal sNovo As String)

        Try

            'Aspas simples no caminho precisam ser dobradas dentro do texto do PowerShell
            Dim sComando As String = "Wait-Process -Id " & Process.GetCurrentProcess.Id & " -ErrorAction SilentlyContinue; " & _
                                     "Move-Item -LiteralPath '" & sNovo.Replace("'", "''") & "' -Destination '" & sAtual.Replace("'", "''") & "' -Force; " & _
                                     "Start-Process -FilePath '" & sAtual.Replace("'", "''") & "'"

            Dim oProcesso As New ProcessStartInfo("powershell.exe", "-NoProfile -WindowStyle Hidden -Command """ & sComando & """")
            oProcesso.CreateNoWindow = True
            oProcesso.UseShellExecute = False
            oProcesso.WindowStyle = ProcessWindowStyle.Hidden

            Process.Start(oProcesso)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class
