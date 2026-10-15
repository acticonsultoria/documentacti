'===============================================================================================
'Classe:        clsConfigXML
'Objetivo:      Leitura do arquivo de configuração XmlConfig\Config.xml (padrão do INTERACTI)
'               O Config.xml fica somente na pasta do executável e não é versionado.
'===============================================================================================
Imports System.Xml
Imports System.IO

Public Class clsConfigXML

#Region "::: FUNCTION / SUB :::"

    Public Function ArquivoConfiguracao() As String

        Return ApplicationPath() & "XmlConfig\Config.xml"

    End Function

    Public Function LoadXML() As Boolean

        Try

            'Seta Retorno da Função
            LoadXML = False

            'Verifica se existe o arquivo de configuração
            If File.Exists(ArquivoConfiguracao) = False Then
                MsgBox("Arquivo de configuração não encontrado: " & ArquivoConfiguracao() & vbCrLf & vbCrLf & _
                       "Copie o arquivo XmlConfig\Config.exemplo.xml para XmlConfig\Config.xml e informe a conexão com o MySQL.", _
                       MsgBoxStyle.Exclamation, gAplicacao)
                Exit Function
            End If

            Dim oXmlDocument As New XmlDocument
            oXmlDocument.Load(ArquivoConfiguracao)

            Dim oNode As XmlNode = oXmlDocument.SelectSingleNode("Documentacti/ConfigDatabase/StringConnection")

            If IsNothing(oNode) OrElse oNode.InnerText.Trim = "" Then
                MsgBox("A tag Documentacti/ConfigDatabase/StringConnection não foi informada no arquivo de configuração.", MsgBoxStyle.Exclamation, gAplicacao)
                Exit Function
            End If

            goDatabase.sConnection = oNode.InnerText.Trim

            'Dados exibidos na barra de status (sem senha)
            Dim oBuilder As New MySql.Data.MySqlClient.MySqlConnectionStringBuilder(goDatabase.sConnection)
            goDatabase.sServer = oBuilder.Server & ":" & oBuilder.Port
            goDatabase.sDatabase = oBuilder.Database

            'FTP dos anexos (opcional: sem ele a aba Arquivos fica indisponível para upload/download)
            Dim sCaminhoFtp As String = LerNo(oXmlDocument, "Documentacti/Ftp/Caminho")

            If sCaminhoFtp <> "" Then
                goFtp.sCaminho = sCaminhoFtp & IIf(sCaminhoFtp.EndsWith("/"), "", "/")
                goFtp.sUsuario = LerNo(oXmlDocument, "Documentacti/Ftp/Usuario")
                goFtp.sSenha = LerNo(oXmlDocument, "Documentacti/Ftp/Senha")
            End If

            'Seta Retorno da Função
            LoadXML = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function LerNo(ByVal oXmlDocument As XmlDocument, ByVal sCaminho As String) As String

        Dim oNode As XmlNode = oXmlDocument.SelectSingleNode(sCaminho)

        Return IIf(IsNothing(oNode), "", oNode.InnerText.Trim)

    End Function

    Public Function TesteConexao() As Boolean

        Try

            Using oConnection As New MySql.Data.MySqlClient.MySqlConnection(goDatabase.sConnection)
                oConnection.Open()
            End Using

            Return True

        Catch ex As Exception
            'Não exibe a string de conexão para não expor credenciais
            MsgBox("Não foi possível conectar ao banco de dados MySQL (" & goDatabase.sServer & " / " & goDatabase.sDatabase & ")." & vbCrLf & vbCrLf & ex.Message, _
                   MsgBoxStyle.Critical, gAplicacao)
            Return False
        End Try

    End Function

#End Region

End Class
