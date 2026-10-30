'===============================================================================================
'Módulo:        modDeclaration
'Objetivo:      Declarações globais do Documentacti (baseado no modDeclaration do INTERACTI)
'===============================================================================================
Imports Janus.Windows.GridEX

Public Module modDeclaration

#Region "::: VARIAVEIS GLOBAIS :::"

    'Banco de Dados (MySQL)
    Public goDatabase As Database

    'Usuário autenticado
    Public goUsuario As Usuario

    'FTP dos anexos (lido do XmlConfig\Config.xml)
    Public goFtp As FtpConfig

    'Controle de alteração dos campos (Control_GotFocus / Control_LostFocus)
    Public bChangeControl As Boolean
    Public sControlValue As String

    'Cores de Mensagem
    Public gColorInformacaoErro As Color = Color.Purple
    Public gColorInformacaoOK As Color = Color.Blue
    Public gColorSistemaErro As Color = Color.Red
    Public gAplicacao As String = "Documentacti"

#End Region

#Region "::: CONSTANTES :::"

    Public Const gSistema As String = "DOCUMENTACTI"

    'Auxiliares
    Public Const gcTempoMensagem As Integer = 5000

    'Tamanho máximo de cada anexo (bytes)
    Public Const gcTamanhoMaximoAnexo As Long = 31457280

    'Status do Chamado (tb_stc_status_atividade)
    Public Const gcStatusChamadoIniciado As Integer = 3
    Public Const gcStatusChamadoAgCliente As Integer = 9
    Public Const gcStatusChamadoConcluido As Integer = 6
    Public Const gcStatusChamadoCancelado As Integer = 14

#End Region

#Region "::: ESTRUTURAS :::"

    'Usuário
    Public Structure Usuario
        Public sUsuario As String
        Public sNome As String
        Public lUsuario As Long
        Public iEmpresa As Integer
        Public sEmpresa As String
        Public sEmail As String
        Public bAdmin As Boolean
        Public bInterno As Boolean
        Public iTime As Integer 'time do usuário (tb_stc_tipo_time_empresa); 0 = sem time
        Public bSuperAdm As Boolean 'tb_cad_usuario.super_adm (marcado manualmente no banco): acesso aos relatórios
    End Structure

    'FTP
    Public Structure FtpConfig
        Dim sCaminho As String
        Dim sUsuario As String
        Dim sSenha As String
    End Structure

    'Banco de Dados
    Public Structure Database
        Dim sConnection As String
        Dim sServer As String
        Dim sDatabase As String
    End Structure

#End Region

#Region "::: DELEGATE :::"

    Public Delegate Sub DelegateKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Public Delegate Sub DelegateGotFocus(ByVal sender As Object, ByVal e As EventArgs)
    Public Delegate Sub DelegateLostFocus(ByVal sender As Object, ByVal e As EventArgs)

#End Region

#Region "::: ENUMS GLOBAIS :::"

    'Tipo Campo
    Public Enum TipoCampo
        numero = 1
        texto = 2
        data = 3
        hora = 4
    End Enum

#End Region

#Region " - MENSAGENS - "

    Public Enum Mensagem
        RegistroExcluido = 1
        RegistroAlterado = 2
        RegistroInserido = 3
        DireitoAlteracao = 4
        ValidacaoSelecionar = 10
        OperacaoRealizadaSucesso = 11
        Erro = 19
    End Enum

#End Region

End Module
