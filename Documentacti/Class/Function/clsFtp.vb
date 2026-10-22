'===============================================================================================
'Classe:        clsFtp
'Objetivo:      Acesso ao servidor FTP dos anexos de chamado (FTP SSL/TLS)
'               Endereço, usuário e senha vêm do XmlConfig\Config.xml (nunca do código-fonte)
'===============================================================================================
Imports System.IO
Imports System.Net
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates

Public Class clsFtp

#Region "::: FUNCTION / SUB :::"

    Public Function Configurado() As Boolean

        Return goFtp.sCaminho <> "" AndAlso goFtp.sUsuario <> ""

    End Function

    Public Sub Upload(ByVal sRelativo As String, ByVal sArquivoLocal As String)

        Try

            'Cria as pastas do caminho (ex.: chamados/7001), caso ainda não existam
            Call CriarPastas(sRelativo)

            Dim oRequest As FtpWebRequest = CriarRequest(sRelativo, WebRequestMethods.Ftp.UploadFile)

            Using oArquivo As FileStream = File.OpenRead(sArquivoLocal), _
                  oStream As Stream = oRequest.GetRequestStream()
                oArquivo.CopyTo(oStream)
            End Using

            Using oResponse As FtpWebResponse = DirectCast(oRequest.GetResponse(), FtpWebResponse)
                'Resposta lida para confirmar o término da transferência
            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub Download(ByVal sRelativo As String, ByVal sDestino As String)

        Try

            Dim oRequest As FtpWebRequest = CriarRequest(sRelativo, WebRequestMethods.Ftp.DownloadFile)

            Using oResponse As FtpWebResponse = DirectCast(oRequest.GetResponse(), FtpWebResponse), _
                  oFtpStream As Stream = oResponse.GetResponseStream(), _
                  oArquivo As FileStream = File.Create(sDestino)
                oFtpStream.CopyTo(oArquivo)
            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function Existe(ByVal sRelativo As String) As Boolean

        'Verifica se já existe um arquivo com este nome no servidor (evita sobrescrever anexos de outros chamados)
        Try

            Dim oRequest As FtpWebRequest = CriarRequest(sRelativo, WebRequestMethods.Ftp.GetFileSize)

            Using oResponse As FtpWebResponse = DirectCast(oRequest.GetResponse(), FtpWebResponse)
                Return True
            End Using

        Catch exWeb As WebException
            If ArquivoNaoEncontrado(exWeb) Then Return False
            Throw exWeb
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Delete(ByVal sRelativo As String) As Boolean

        'Retorna False quando o arquivo não existe mais no servidor
        Try

            Dim oRequest As FtpWebRequest = CriarRequest(sRelativo, WebRequestMethods.Ftp.DeleteFile)

            Using oResponse As FtpWebResponse = DirectCast(oRequest.GetResponse(), FtpWebResponse)
                Return True
            End Using

        Catch exWeb As WebException
            If ArquivoNaoEncontrado(exWeb) Then Return False
            Throw exWeb
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub CriarPastas(ByVal sRelativo As String)

        Try

            Dim sPartes() As String = sRelativo.Split("/"c)
            Dim sPasta As String = ""

            'Todas as partes, exceto o nome do arquivo
            For i As Integer = 0 To sPartes.Length - 2

                sPasta &= IIf(sPasta = "", "", "/") & sPartes(i)

                Try
                    Dim oRequest As FtpWebRequest = CriarRequest(sPasta, WebRequestMethods.Ftp.MakeDirectory)
                    Using oResponse As FtpWebResponse = DirectCast(oRequest.GetResponse(), FtpWebResponse)
                    End Using
                Catch exWeb As WebException
                    'Pasta já existe: segue
                    If ArquivoNaoEncontrado(exWeb) = False Then Throw exWeb
                End Try

            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function CriarRequest(ByVal sRelativo As String, ByVal sMetodo As String) As FtpWebRequest

        Try

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidaCertificado

            Dim oRequest As FtpWebRequest = DirectCast(WebRequest.Create(New Uri(goFtp.sCaminho & sRelativo)), FtpWebRequest)

            oRequest.Credentials = New NetworkCredential(goFtp.sUsuario, goFtp.sSenha)
            oRequest.Method = sMetodo
            oRequest.EnableSsl = True
            oRequest.UsePassive = True
            oRequest.UseBinary = True
            oRequest.KeepAlive = False
            oRequest.Timeout = 30000
            oRequest.ReadWriteTimeout = 120000

            Return oRequest

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared Function ValidaCertificado(ByVal sender As Object, _
                                              ByVal oCertificado As X509Certificate, _
                                              ByVal oCadeia As X509Chain, _
                                              ByVal oErros As SslPolicyErrors) As Boolean

        'Certificado válido: aceita. Inválido (ex.: certificado de hospedagem compartilhada):
        'aceita somente para o servidor FTP configurado, nunca para outros hosts
        If oErros = SslPolicyErrors.None Then Return True

        Dim oRequest As FtpWebRequest = TryCast(sender, FtpWebRequest)

        If IsNothing(oRequest) OrElse goFtp.sCaminho = "" Then Return False

        Return String.Equals(oRequest.RequestUri.Host, New Uri(goFtp.sCaminho).Host, StringComparison.OrdinalIgnoreCase)

    End Function

    Private Function ArquivoNaoEncontrado(ByVal oErro As WebException) As Boolean

        Dim oResponse As FtpWebResponse = TryCast(oErro.Response, FtpWebResponse)

        Return Not IsNothing(oResponse) AndAlso oResponse.StatusCode = FtpStatusCode.ActionNotTakenFileUnavailable

    End Function

#End Region

End Class
