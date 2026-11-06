'===============================================================================================
'Classe:        clsBarraTarefas
'Objetivo:      Ícone sobreposto (overlay) no botão da barra de tarefas do Windows, igual à bolinha
'               de status do Teams (ITaskbarList3.SetOverlayIcon). Funciona com o atalho fixado ou não.
'               Obs.: o Windows não mostra overlay com "Usar botões pequenos da barra de tarefas".
'===============================================================================================
Imports System.Runtime.InteropServices

Public Class clsBarraTarefas

#Region "::: INTEROP :::"

    'Ordem dos métodos = ordem da vtable (ITaskbarList, ITaskbarList2, ITaskbarList3): não alterar
    <ComImport(), Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
    Private Interface ITaskbarList3
        'ITaskbarList
        Sub HrInit()
        Sub AddTab(ByVal hWnd As IntPtr)
        Sub DeleteTab(ByVal hWnd As IntPtr)
        Sub ActivateTab(ByVal hWnd As IntPtr)
        Sub SetActiveAlt(ByVal hWnd As IntPtr)
        'ITaskbarList2
        Sub MarkFullscreenWindow(ByVal hWnd As IntPtr, <MarshalAs(UnmanagedType.Bool)> ByVal fFullscreen As Boolean)
        'ITaskbarList3
        Sub SetProgressValue(ByVal hWnd As IntPtr, ByVal ullCompleted As UInt64, ByVal ullTotal As UInt64)
        Sub SetProgressState(ByVal hWnd As IntPtr, ByVal tbpFlags As Integer)
        Sub RegisterTab(ByVal hWndTab As IntPtr, ByVal hWndMDI As IntPtr)
        Sub UnregisterTab(ByVal hWndTab As IntPtr)
        Sub SetTabOrder(ByVal hWndTab As IntPtr, ByVal hWndInsertBefore As IntPtr)
        Sub SetTabActive(ByVal hWndTab As IntPtr, ByVal hWndMDI As IntPtr, ByVal dwReserved As Integer)
        Sub ThumbBarAddButtons(ByVal hWnd As IntPtr, ByVal cButtons As UInteger, ByVal pButton As IntPtr)
        Sub ThumbBarUpdateButtons(ByVal hWnd As IntPtr, ByVal cButtons As UInteger, ByVal pButton As IntPtr)
        Sub ThumbBarSetImageList(ByVal hWnd As IntPtr, ByVal himl As IntPtr)
        Sub SetOverlayIcon(ByVal hWnd As IntPtr, ByVal hIcon As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByVal pszDescription As String)
        Sub SetThumbnailTooltip(ByVal hWnd As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByVal pszTip As String)
        Sub SetThumbnailClip(ByVal hWnd As IntPtr, ByVal prcClip As IntPtr)
    End Interface

    <ComImport(), Guid("56FDF344-FD6D-11d0-958A-006097C9A090"), ClassInterface(ClassInterfaceType.None)> _
    Private Class TaskbarList
    End Class

    <DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
    Private Shared Function RegisterWindowMessage(ByVal lpString As String) As Integer
    End Function

#End Region

#Region "::: VARIAVEIS :::"

    Private oTaskbar As ITaskbarList3

    'Mensagem enviada pelo Windows quando o botão da barra é criado (inclusive após reiniciar o Explorer):
    'overlay definido antes disso é perdido e precisa ser reaplicado
    Public Shared ReadOnly iMensagemBotaoCriado As Integer = RegisterWindowMessage("TaskbarButtonCreated")

#End Region

#Region "::: FUNCTION / SUB :::"

    Public Sub New()

        Try

            'Windows 7 ou superior
            If Environment.OSVersion.Version < New Version(6, 1) Then Exit Sub

            oTaskbar = CType(New TaskbarList, ITaskbarList3)
            oTaskbar.HrInit()

        Catch
            'Sem suporte a overlay: segue sem a bolinha
            oTaskbar = Nothing
        End Try

    End Sub

    Public Sub DefinirOverlay(ByVal hWnd As IntPtr, ByVal oIcone As Icon, ByVal sDescricao As String)

        Try

            If IsNothing(oTaskbar) OrElse hWnd = IntPtr.Zero Then Exit Sub

            'Icone Nothing remove a bolinha
            oTaskbar.SetOverlayIcon(hWnd, If(IsNothing(oIcone), IntPtr.Zero, oIcone.Handle), sDescricao)

        Catch
            'Overlay é só visual: não interrompe o uso
        End Try

    End Sub

#End Region

End Class
