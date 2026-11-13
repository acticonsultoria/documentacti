'===============================================================================================
'Classe:        clsMonitorFiscal
'Objetivo:      Coleta das notas técnicas da NF-e / NFS-e nos portais oficiais e teste de
'               disponibilidade dos portais dos autorizadores (mesma lógica do Monitor Fiscal ACTI)
'
'               Somente páginas públicas: nenhum certificado digital, nenhuma consulta autenticada
'               e nenhum evento fiscal. Só o índice das notas (número, versão, data, resumo e link)
'               é lido - nenhum PDF é baixado.
'===============================================================================================
Imports System.Net
Imports System.Text.RegularExpressions

Public Class clsMonitorFiscal

#Region "::: VARIAVEIS :::"

    Public Class NotaTecnica
        Public sDocumento As String = ""
        Public sNumero As String = ""
        Public sVersao As String = ""
        Public dPublicacao As Date? = Nothing
        Public sResumo As String = ""
        Public sUrl As String = ""
    End Class

    Public Class Portal
        Public sCodigo As String = ""
        Public sNome As String = ""
        Public sAtende As String = ""
        Public sUrlTeste As String = ""
        Public sUrlStatus As String = ""
        Public sStatus As String = "NÃO TESTADO"
        Public iTempo As Integer = -1
    End Class

    Private Const gcUserAgent As String = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0 Safari/537.36"

    Public Const gcUrlNFeLista As String = "https://www.nfe.fazenda.gov.br/portal/listaConteudo.aspx?tipoConteudo=04BIflQt1aY%3D"
    Private Const gcUrlNFeBase As String = "https://www.nfe.fazenda.gov.br/portal/"
    Public Const gcUrlNFSeRtc As String = "https://www.gov.br/nfse/pt-br/biblioteca/documentacao-tecnica/rtc"
    Public Const gcUrlNFSeNoticias As String = "https://www.gov.br/nfse/pt-br/noticias"
    Public Const gcUrlOrientacoesRtc As String = "https://www.gov.br/receitafederal/pt-br/acesso-a-informacao/acoes-e-programas/programas-e-atividades/reforma-consumo/orientacoes-2026"
    Public Const gcUrlDisponibilidadeNFe As String = "https://www.nfe.fazenda.gov.br/portal/disponibilidade.aspx"

    'Quantas notas técnicas manter por documento em cada coleta
    Private Const gcMaximoPorDocumento As Integer = 12

    'Janela entre coletas (várias máquinas abrindo o sistema não ficam lendo os portais em sequência)
    Public Const gcIntervaloColetaMinutos As Integer = 60

    'Tempo de resposta dos portais (milissegundos)
    Private Const gcTimeoutColeta As Integer = 30000
    Private Const gcTimeoutPortal As Integer = 8000
    Public Const gcTempoLento As Integer = 1200

    Public Const gcStatusNoAr As String = "NO AR"
    Public Const gcStatusLento As String = "LENTO"
    Public Const gcStatusSemResposta As String = "SEM RESPOSTA"

#End Region

#Region "::: NOTAS TÉCNICAS :::"

    Public Shared Function ColetarNotasTecnicas() As List(Of NotaTecnica)

        'NF-e e NFS-e; se as duas falharem a coleta inteira falha (a lista anterior do banco é mantida)
        Dim oNotas As New List(Of NotaTecnica)
        Dim sErro As String = ""

        Call HabilitarTls()

        Try
            oNotas.AddRange(ColetarNFe())
        Catch ex As Exception
            sErro = "NF-e: " & ex.Message
        End Try

        Try
            oNotas.AddRange(ColetarNFSe())
        Catch ex As Exception
            sErro &= IIf(sErro = "", "", " / ") & "NFS-e: " & ex.Message
        End Try

        If oNotas.Count = 0 Then
            Throw New Exception("Nenhuma nota técnica foi lida dos portais. " & sErro)
        End If

        Return oNotas

    End Function

    Private Shared Function ColetarNFe() As List(Of NotaTecnica)

        Dim sHtml As String = BaixarPagina(gcUrlNFeLista, gcTimeoutColeta)

        Dim sPadrao As String = "(?is)<a[^>]*href=""(exibirArquivo\.aspx\?conteudo=[^""]+)""[^>]*>\s*" & _
                                "<span class=""tituloConteudo"">\s*Nota\s+T[eé]cnica\s+([\d.]+)\s+v\.?\s*([^\s<-]+)\s*-\s*" & _
                                "Publicada\s+em\s*(\d{2}/\d{2}/\d{4})\s*</span>\s*</a>\s*<br\s*/?>(.*?)<br"

        Dim oItens As New List(Of NotaTecnica)

        For Each oMatch As Match In Regex.Matches(sHtml, sPadrao)

            Dim sHref As String = oMatch.Groups(1).Value.Replace("&amp;", "&")

            oItens.Add(New NotaTecnica With {.sDocumento = "NF-e", _
                                             .sNumero = "NT " & oMatch.Groups(2).Value, _
                                             .sVersao = "v" & oMatch.Groups(3).Value, _
                                             .dPublicacao = DataBrasil(oMatch.Groups(4).Value), _
                                             .sResumo = LimparTextoNota(oMatch.Groups(5).Value), _
                                             .sUrl = gcUrlNFeBase & CorrigirUrlPortal(sHref)})

        Next

        'O portal lista todas as versões de cada NT: fica só a mais recente
        Return oItens.GroupBy(Function(n) n.sNumero) _
                     .Select(Function(g) g.OrderByDescending(Function(n) n.dPublicacao.GetValueOrDefault()).First()) _
                     .OrderByDescending(Function(n) n.dPublicacao.GetValueOrDefault()) _
                     .Take(gcMaximoPorDocumento) _
                     .ToList()

    End Function

    Private Shared Function ColetarNFSe() As List(Of NotaTecnica)

        Dim sHtml As String = BaixarPagina(gcUrlNFSeRtc, gcTimeoutColeta)
        Dim oNoticias As Dictionary(Of String, String()) = ColetarNoticiasNFSe()
        Dim oItens As New List(Of NotaTecnica)

        For Each oMatch As Match In Regex.Matches(sHtml, "(?is)<a[^>]+href=""([^""]+\.pdf[^""]*)""[^>]*>(.*?)</a>")

            Dim sRotulo As String = LimparTexto(oMatch.Groups(2).Value)
            If Regex.IsMatch(sRotulo, "(?i)nota\s+t[eé]cnica") = False Then Continue For

            Dim oNumero As Match = Regex.Match(sRotulo, "(?i)n[º°o]?\s*0*(\d{1,3})\s*$")
            If oNumero.Success = False Then Continue For

            Dim sChave As String = CInt(oNumero.Groups(1).Value).ToString("000")
            Dim sArquivo As String = oMatch.Groups(1).Value
            Dim oNota As New NotaTecnica With {.sDocumento = "NFS-e", _
                                               .sNumero = "NT " & sChave, _
                                               .sResumo = "Nota técnica do padrão nacional da NFS-e. Abra o arquivo para o conteúdo completo.", _
                                               .sUrl = sArquivo.Replace("&amp;", "&")}

            'A data e a versão costumam estar no nome do arquivo oficial (ex.: nt-008-se-cgnfse-danfse-20260714-v1-02.pdf)
            Dim oDataArquivo As Match = Regex.Match(sArquivo, "(20\d{2})(\d{2})(\d{2})")
            If oDataArquivo.Success Then
                oNota.dPublicacao = DataIso(oDataArquivo.Groups(1).Value, oDataArquivo.Groups(2).Value, oDataArquivo.Groups(3).Value)
            End If

            'Tira a data antes de procurar a versão, senão os dígitos dela entram no número
            Dim oVersao As Match = Regex.Match(Regex.Replace(sArquivo, "20\d{6}", ""), "(?i)-v(\d{1,2})-(\d{1,2})(?:-(\d{1,2}))?")
            If oVersao.Success Then
                oNota.sVersao = "v" & oVersao.Groups(1).Value & "." & oVersao.Groups(2).Value
                If oVersao.Groups(3).Success Then oNota.sVersao &= "." & oVersao.Groups(3).Value
            End If

            'A notícia do portal é a melhor fonte de data e de resumo, quando existe
            If oNoticias.ContainsKey(sChave) Then
                If oNoticias(sChave)(0) <> "" Then oNota.dPublicacao = DataBrasil(oNoticias(sChave)(0))
                If oNoticias(sChave)(1) <> "" Then oNota.sResumo = oNoticias(sChave)(1)
            End If

            oItens.Add(oNota)

        Next

        'A numeração da NFS-e é cronológica: ordena pelo número
        Return oItens.GroupBy(Function(n) n.sNumero) _
                     .Select(Function(g) g.First()) _
                     .OrderByDescending(Function(n) n.sNumero) _
                     .Take(gcMaximoPorDocumento) _
                     .ToList()

    End Function

    Private Shared Function ColetarNoticiasNFSe() As Dictionary(Of String, String())

        'Número da NT (ex.: "009") -> {data dd/MM/yyyy, título da notícia}
        Dim oMapa As New Dictionary(Of String, String())
        Dim sHtml As String

        Try
            sHtml = BaixarPagina(gcUrlNFSeNoticias, gcTimeoutColeta)
        Catch
            'Sem as notícias as notas ficam com a data do nome do arquivo
            Return oMapa
        End Try

        For Each oLink As Match In Regex.Matches(sHtml, "(?is)<h2 class=""titulo"">\s*<a[^>]+href=""([^""]+)""[^>]*>(.*?)</a>")

            Dim sTitulo As String = LimparTexto(oLink.Groups(2).Value)
            If Regex.IsMatch(sTitulo, "(?i)nota\s+t[eé]cnica") = False Then Continue For

            Dim oNumero As Match = Regex.Match(sTitulo, "(?i)n[º°o]?\s*0*(\d{1,3})")
            If oNumero.Success = False Then Continue For

            Dim sChave As String = CInt(oNumero.Groups(1).Value).ToString("000")
            If oMapa.ContainsKey(sChave) Then Continue For

            Dim sPagina As String
            Try
                sPagina = BaixarPagina(oLink.Groups(1).Value.Replace("&amp;", "&"), gcTimeoutColeta)
            Catch
                Continue For
            End Try

            Dim oData As Match = Regex.Match(sPagina, "(?is)plone-document-byline[^>]*>\s*Publicado\s+em\s*(\d{2}/\d{2}/\d{4})")

            oMapa(sChave) = New String() {IIf(oData.Success, oData.Groups(1).Value, "").ToString, sTitulo}

        Next

        Return oMapa

    End Function

    Private Shared Function LimparTexto(ByVal sHtml As String) As String

        Dim sTexto As String = Regex.Replace(sHtml, "<[^>]+>", " ")

        sTexto = Regex.Replace(sTexto, "(?i)&nbsp;", " ")
        sTexto = Regex.Replace(sTexto, "(?i)&amp;", "&")
        sTexto = Regex.Replace(sTexto, "(?i)&quot;", """")
        sTexto = sTexto.Replace("&#39;", "'")
        sTexto = Regex.Replace(sTexto, "(?i)&ndash;|&mdash;", "-")
        sTexto = WebUtility.HtmlDecode(sTexto)
        sTexto = Regex.Replace(sTexto, "\s+", " ")

        Return sTexto.Trim

    End Function

    Private Shared Function LimparTextoNota(ByVal sHtml As String) As String

        Dim sTexto As String = LimparTexto(sHtml)

        'Em alguns itens o portal esquece o <br /> final e a descrição emenda no título da nota seguinte
        Dim oCorte As Match = Regex.Match(sTexto, "(?i)\s*Nota\s+T[eé]cnica\s+\d{4}\.\d{3}")
        If oCorte.Success Then sTexto = sTexto.Substring(0, oCorte.Index)

        Return sTexto.Trim

    End Function

    Private Shared Function CorrigirUrlPortal(ByVal sUrl As String) As String

        'O portal da NF-e imprime o "+" do base64 como espaço no HTML; sem o "+" o link abre um arquivo inexistente
        Return sUrl.Replace(" ", "+")

    End Function

    Private Shared Function DataBrasil(ByVal sData As String) As Date?

        Dim dData As Date

        If Date.TryParseExact(sData, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dData) Then Return dData

        Return Nothing

    End Function

    Private Shared Function DataIso(ByVal sAno As String, ByVal sMes As String, ByVal sDia As String) As Date?

        Dim dData As Date

        If Date.TryParseExact(sAno & sMes & sDia, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dData) Then Return dData

        Return Nothing

    End Function

#End Region

#Region "::: DISPONIBILIDADE DOS PORTAIS :::"

    Public Shared Function Autorizadores() As List(Of Portal)

        'Portal de cada autorizador da NF-e + Emissor Nacional da NFS-e
        Dim oLista As New List(Of Portal)

        oLista.Add(NovoPortal("SP", "Sefaz São Paulo", "SP · autorizador próprio", "https://www.nfe.fazenda.sp.gov.br/", gcUrlDisponibilidadeNFe))
        oLista.Add(NovoPortal("SVRS", "Sefaz Virtual do RS", "RS + 16 UFs · SVRS", "https://dfe-portal.svrs.rs.gov.br/Nfe", "https://dfe-portal.svrs.rs.gov.br/Nfe/Servicos"))
        oLista.Add(NovoPortal("MT", "Sefaz Mato Grosso", "MT · autorizador próprio", "https://www.sefaz.mt.gov.br/", gcUrlDisponibilidadeNFe))
        oLista.Add(NovoPortal("AN", "Ambiente Nacional", "SVAN · SVC-AN", gcUrlDisponibilidadeNFe, gcUrlDisponibilidadeNFe))
        oLista.Add(NovoPortal("MG", "Sefaz Minas Gerais", "MG · autorizador próprio", "https://www.fazenda.mg.gov.br/", gcUrlDisponibilidadeNFe))
        oLista.Add(NovoPortal("PR", "Sefaz Paraná", "PR · autorizador próprio", "https://www.fazenda.pr.gov.br/", gcUrlDisponibilidadeNFe))
        oLista.Add(NovoPortal("BA", "Sefaz Bahia", "BA · autorizador próprio", "https://www.sefaz.ba.gov.br/", gcUrlDisponibilidadeNFe))
        oLista.Add(NovoPortal("PE", "Sefaz Pernambuco", "PE · autorizador próprio", "https://www.sefaz.pe.gov.br/", gcUrlDisponibilidadeNFe))
        oLista.Add(NovoPortal("GO", "Sefaz Goiás", "GO · autorizador próprio", "https://www.sefaz.go.gov.br/", gcUrlDisponibilidadeNFe))
        oLista.Add(NovoPortal("MS", "Sefaz Mato Grosso do Sul", "MS · autorizador próprio", "https://www.efazenda.ms.gov.br/", gcUrlDisponibilidadeNFe))
        oLista.Add(NovoPortal("AM", "Sefaz Amazonas", "AM · autorizador próprio", "https://www.sefaz.am.gov.br/", gcUrlDisponibilidadeNFe))
        oLista.Add(NovoPortal("NFSE", "NFS-e Nacional", "Emissor Nacional", "https://www.nfse.gov.br/EmissorNacional/Login", "https://www.gov.br/nfse/pt-br"))

        Return oLista

    End Function

    Private Shared Function NovoPortal(ByVal sCodigo As String, ByVal sNome As String, ByVal sAtende As String, ByVal sUrlTeste As String, ByVal sUrlStatus As String) As Portal

        Return New Portal With {.sCodigo = sCodigo, .sNome = sNome, .sAtende = sAtende, .sUrlTeste = sUrlTeste, .sUrlStatus = sUrlStatus}

    End Function

    Public Shared Function UFs() As DataTable

        'UF -> autorizador em produção e contingência (Portal Nacional da NF-e / SVRS)
        Dim oTabela As New DataTable

        oTabela.Columns.Add("uf", GetType(String))
        oTabela.Columns.Add("codigo_autorizador", GetType(String))
        oTabela.Columns.Add("autorizador", GetType(String))
        oTabela.Columns.Add("contingencia", GetType(String))
        oTabela.Columns.Add("status", GetType(String))

        For Each sLinha As String In New String() {"AC|SVRS|SVRS|SVC-AN", "AL|SVRS|SVRS|SVC-AN", "AM|AM|PRÓPRIA|SVC-RS", "AP|SVRS|SVRS|SVC-AN", _
                                                   "BA|BA|PRÓPRIA|SVC-RS", "CE|SVRS|SVRS|SVC-AN", "DF|SVRS|SVRS|SVC-AN", "ES|SVRS|SVRS|SVC-AN", _
                                                   "GO|GO|PRÓPRIA|SVC-RS", "MA|AN|SVAN|SVC-RS", "MG|MG|PRÓPRIA|SVC-AN", "MS|MS|PRÓPRIA|SVC-RS", _
                                                   "MT|MT|PRÓPRIA|SVC-RS", "PA|SVRS|SVRS|SVC-AN", "PB|SVRS|SVRS|SVC-AN", "PE|PE|PRÓPRIA|SVC-RS", _
                                                   "PI|SVRS|SVRS|SVC-AN", "PR|PR|PRÓPRIA|SVC-RS", "RJ|SVRS|SVRS|SVC-AN", "RN|SVRS|SVRS|SVC-AN", _
                                                   "RO|SVRS|SVRS|SVC-AN", "RR|SVRS|SVRS|SVC-AN", "RS|SVRS|SVRS|SVC-AN", "SC|SVRS|SVRS|SVC-AN", _
                                                   "SE|SVRS|SVRS|SVC-AN", "SP|SP|PRÓPRIA|SVC-AN", "TO|SVRS|SVRS|SVC-AN"}
            Dim sCampo() As String = sLinha.Split("|"c)
            oTabela.Rows.Add(sCampo(0), sCampo(1), sCampo(2), sCampo(3), "NÃO TESTADO")
        Next

        Return oTabela

    End Function

    Public Shared Sub TestarPortal(ByVal oPortal As Portal)

        'Mede o tempo até o portal responder (qualquer resposta HTTP conta: o portal está no ar).
        'Não é a consulta oficial de status do serviço (nfeStatusServicoNF), que exige certificado digital.
        Dim oCronometro As Stopwatch = Stopwatch.StartNew()

        Call HabilitarTls()

        Try

            Dim oRequest As HttpWebRequest = CType(WebRequest.Create(oPortal.sUrlTeste), HttpWebRequest)
            oRequest.Method = "GET"
            oRequest.UserAgent = gcUserAgent
            oRequest.Timeout = gcTimeoutPortal
            oRequest.ReadWriteTimeout = gcTimeoutPortal
            oRequest.AllowAutoRedirect = True

            Using oResponse As HttpWebResponse = CType(oRequest.GetResponse(), HttpWebResponse)
            End Using

            Call DefinirStatusPortal(oPortal, oCronometro)

        Catch ex As WebException

            'Erro HTTP (403, 500 etc.) também é resposta do portal; sem resposta = fora do ar / bloqueado
            If IsNothing(ex.Response) = False Then
                ex.Response.Close()
                Call DefinirStatusPortal(oPortal, oCronometro)
            Else
                oPortal.iTempo = -1
                oPortal.sStatus = gcStatusSemResposta
            End If

        Catch
            oPortal.iTempo = -1
            oPortal.sStatus = gcStatusSemResposta
        End Try

    End Sub

    Private Shared Sub DefinirStatusPortal(ByVal oPortal As Portal, ByVal oCronometro As Stopwatch)

        oPortal.iTempo = CInt(oCronometro.ElapsedMilliseconds)
        oPortal.sStatus = IIf(oPortal.iTempo <= gcTempoLento, gcStatusNoAr, gcStatusLento).ToString

    End Sub

#End Region

#Region "::: FUNCTION / SUB :::"

    Private Shared Sub HabilitarTls()

        'Portais do governo exigem TLS 1.2 (o .NET 4.5 não liga por padrão)
        ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol Or SecurityProtocolType.Tls12

    End Sub

    Private Shared Function BaixarPagina(ByVal sUrl As String, ByVal iTimeout As Integer) As String

        Dim oRequest As HttpWebRequest = CType(WebRequest.Create(sUrl), HttpWebRequest)

        oRequest.Method = "GET"
        oRequest.UserAgent = gcUserAgent
        oRequest.Timeout = iTimeout
        oRequest.ReadWriteTimeout = iTimeout
        oRequest.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
        'O portal da NF-e redireciona para detectar suporte a cookie (AspxAutoDetectCookieSupport): sem cookie fica em loop
        oRequest.CookieContainer = New CookieContainer

        Using oResponse As HttpWebResponse = CType(oRequest.GetResponse(), HttpWebResponse)
            Using oStream As New IO.MemoryStream

                oResponse.GetResponseStream().CopyTo(oStream)

                'Charset do cabeçalho (padrão UTF-8)
                Dim oEncoding As System.Text.Encoding = System.Text.Encoding.UTF8
                Try
                    If oResponse.CharacterSet <> "" Then oEncoding = System.Text.Encoding.GetEncoding(oResponse.CharacterSet)
                Catch
                    oEncoding = System.Text.Encoding.UTF8
                End Try

                Return oEncoding.GetString(oStream.ToArray())

            End Using
        End Using

    End Function

#End Region

End Class
