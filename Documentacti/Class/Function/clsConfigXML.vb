'===============================================================================================
'Classe:        clsConfigXML
'Objetivo:      Acesso ao banco e ao FTP (constantes do modConexao, compiladas no executável)
'               Em Debug, o XmlConfig\Config.xml (se existir) tem prioridade, para testes em base local.
'               Em Release o Config.xml não é lido.
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

            'Acesso compilado no executável (modConexao)
            Dim sConexao As String = gcConexaoBanco
            Dim sCaminhoFtp As String = gcFtpCaminho
            Dim sUsuarioFtp As String = gcFtpUsuario
            Dim sSenhaFtp As String = gcFtpSenha

#If DEBUG Then
            'Debug: o Config.xml, se existir, aponta para uma base/FTP de testes
            If File.Exists(ArquivoConfiguracao) Then

                Dim oXmlDocument As New XmlDocument
                oXmlDocument.Load(ArquivoConfiguracao)

                If LerNo(oXmlDocument, "Documentacti/ConfigDatabase/StringConnection") <> "" Then sConexao = LerNo(oXmlDocument, "Documentacti/ConfigDatabase/StringConnection")

                If LerNo(oXmlDocument, "Documentacti/Ftp/Caminho") <> "" Then
                    sCaminhoFtp = LerNo(oXmlDocument, "Documentacti/Ftp/Caminho")
                    sUsuarioFtp = LerNo(oXmlDocument, "Documentacti/Ftp/Usuario")
                    sSenhaFtp = LerNo(oXmlDocument, "Documentacti/Ftp/Senha")
                End If

            End If
#End If

            If sConexao.Trim = "" Then
                MsgBox("Esta versão do " & gAplicacao & " foi compilada sem a conexão com o banco de dados." & vbCrLf & _
                       "Procure o suporte da ACTi.", MsgBoxStyle.Critical, gAplicacao)
                Exit Function
            End If

            goDatabase.sConnection = sConexao.Trim

            'Dados exibidos na barra de status (sem senha)
            Dim oBuilder As New MySqlConnector.MySqlConnectionStringBuilder(goDatabase.sConnection)
            goDatabase.sServer = oBuilder.Server & ":" & oBuilder.Port
            goDatabase.sDatabase = oBuilder.Database

            'FTP dos anexos (opcional: sem ele a aba Arquivos fica indisponível para upload/download)
            If sCaminhoFtp.Trim <> "" Then
                goFtp.sCaminho = sCaminhoFtp.Trim & IIf(sCaminhoFtp.Trim.EndsWith("/"), "", "/")
                goFtp.sUsuario = sUsuarioFtp
                goFtp.sSenha = sSenhaFtp
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

            Using oConnection As New MySqlConnector.MySqlConnection(goDatabase.sConnection)
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
