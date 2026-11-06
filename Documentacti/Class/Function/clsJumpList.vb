'===============================================================================================
'Classe:        clsJumpList
'Objetivo:      Itens do clique direito no ícone da barra de tarefas (Jump List / "Tarefas" do Windows).
'               Cada item abre o próprio exe com um parâmetro (ex.: /novochamado); como o sistema é de
'               instância única, o exe novo repassa o parâmetro para o que já está aberto e fecha
'               (ApplicationEvents.StartupNextInstance -> frmMain.ExecutarComandoJumpList).
'===============================================================================================
Imports System.Runtime.InteropServices

Public Class clsJumpList

#Region "::: VARIAVEIS :::"

    'Parâmetros aceitos pelo exe
    Public Const gcComandoNovoChamado As String = "/novochamado"
    Public Const gcComandoAbrirChamado As String = "/abrirchamado"

#End Region

#Region "::: INTEROP :::"

    'Ordem dos métodos = ordem da vtable de cada interface: não alterar
    <ComImport(), Guid("6332debf-87b5-4670-90c0-5e57b408a49e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
    Private Interface ICustomDestinationList
        Sub SetAppID(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszAppID As String)
        Sub BeginList(ByRef pcMinSlots As UInteger, ByRef riid As Guid, <MarshalAs(UnmanagedType.Interface)> ByRef ppv As Object)
        Sub AppendCategory(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszCategory As String, <MarshalAs(UnmanagedType.Interface)> ByVal poa As IObjectArray)
        Sub AppendKnownCategory(ByVal category As Integer)
        Sub AddUserTasks(<MarshalAs(UnmanagedType.Interface)> ByVal poa As IObjectArray)
        Sub CommitList()
        Sub GetRemovedDestinations(ByRef riid As Guid, <MarshalAs(UnmanagedType.Interface)> ByRef ppv As Object)
        Sub DeleteList(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszAppID As String)
        Sub AbortList()
    End Interface

    <ComImport(), Guid("92CA9DCD-5622-4bba-A805-5E9F541BD8C9"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
    Private Interface IObjectArray
        Sub GetCount(ByRef pcObjects As UInteger)
        Sub GetAt(ByVal uiIndex As UInteger, ByRef riid As Guid, <MarshalAs(UnmanagedType.Interface)> ByRef ppv As Object)
    End Interface

    <ComImport(), Guid("5632b1a4-e38a-400a-928a-d4cd63230295"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
    Private Interface IObjectCollection
        'IObjectArray
        Sub GetCount(ByRef pcObjects As UInteger)
        Sub GetAt(ByVal uiIndex As UInteger, ByRef riid As Guid, <MarshalAs(UnmanagedType.Interface)> ByRef ppv As Object)
        'IObjectCollection
        Sub AddObject(<MarshalAs(UnmanagedType.Interface)> ByVal punk As Object)
        Sub AddFromArray(<MarshalAs(UnmanagedType.Interface)> ByVal poaSource As IObjectArray)
        Sub RemoveObjectAt(ByVal uiIndex As UInteger)
        Sub Clear()
    End Interface

    <ComImport(), Guid("000214F9-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
    Private Interface IShellLinkW
        Sub GetPath(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszFile As System.Text.StringBuilder, ByVal cch As Integer, ByVal pfd As IntPtr, ByVal fFlags As UInteger)
        Sub GetIDList(ByRef ppidl As IntPtr)
        Sub SetIDList(ByVal pidl As IntPtr)
        Sub GetDescription(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszName As System.Text.StringBuilder, ByVal cch As Integer)
        Sub SetDescription(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszName As String)
        Sub GetWorkingDirectory(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszDir As System.Text.StringBuilder, ByVal cch As Integer)
        Sub SetWorkingDirectory(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszDir As String)
        Sub GetArguments(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszArgs As System.Text.StringBuilder, ByVal cch As Integer)
        Sub SetArguments(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszArgs As String)
        Sub GetHotkey(ByRef pwHotkey As Short)
        Sub SetHotkey(ByVal wHotkey As Short)
        Sub GetShowCmd(ByRef piShowCmd As Integer)
        Sub SetShowCmd(ByVal iShowCmd As Integer)
        Sub GetIconLocation(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszIconPath As System.Text.StringBuilder, ByVal cch As Integer, ByRef piIcon As Integer)
        Sub SetIconLocation(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszIconPath As String, ByVal iIcon As Integer)
        Sub SetRelativePath(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszPathRel As String, ByVal dwReserved As UInteger)
        Sub Resolve(ByVal hwnd As IntPtr, ByVal fFlags As UInteger)
        Sub SetPath(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszFile As String)
    End Interface

    <ComImport(), Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
    Private Interface IPropertyStore
        Sub GetCount(ByRef cProps As UInteger)
        Sub GetAt(ByVal iProp As UInteger, ByRef pkey As PropertyKey)
        Sub GetValue(ByRef key As PropertyKey, ByRef pv As PropVariant)
        Sub SetValue(ByRef key As PropertyKey, ByRef propvar As PropVariant)
        Sub Commit()
    End Interface

    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Private Structure PropertyKey
        Public fmtid As Guid
        Public pid As UInteger
    End Structure

    'PROPVARIANT só com texto (VT_LPWSTR); 16 bytes no x86
    <StructLayout(LayoutKind.Explicit, Size:=16)> _
    Private Structure PropVariant
        <FieldOffset(0)> Public vt As UShort
        <FieldOffset(8)> Public pValor As IntPtr
    End Structure

    <ComImport(), Guid("77f10cf0-3db5-4966-b520-b7c54fd35ed6"), ClassInterface(ClassInterfaceType.None)> _
    Private Class DestinationList
    End Class

    <ComImport(), Guid("2d3468c1-36a7-43b6-ac24-d3f02fd9607a"), ClassInterface(ClassInterfaceType.None)> _
    Private Class EnumerableObjectCollection
    End Class

    <ComImport(), Guid("00021401-0000-0000-C000-000000000046"), ClassInterface(ClassInterfaceType.None)> _
    Private Class ShellLink
    End Class

    <DllImport("ole32.dll")> _
    Private Shared Function PropVariantClear(ByRef pvar As PropVariant) As Integer
    End Function

    Private Const VT_LPWSTR As UShort = 31

    'PKEY_Title: texto mostrado no item da Jump List
    Private Shared ReadOnly PKEY_Title As New PropertyKey With {.fmtid = New Guid("F29F85E0-4FF9-1068-AB91-08002B27B3D9"), .pid = 2}

#End Region

#Region "::: FUNCTION / SUB :::"

    Public Shared Sub Registrar()

        Try

            'Windows 7 ou superior
            If Environment.OSVersion.Version < New Version(6, 1) Then Exit Sub

            Dim sExe As String = Application.ExecutablePath

            Dim oLista As ICustomDestinationList = CType(New DestinationList, ICustomDestinationList)
            Dim iSlots As UInteger = 0
            Dim oRemovidos As Object = Nothing
            Dim oGuidArray As Guid = GetType(IObjectArray).GUID

            oLista.BeginList(iSlots, oGuidArray, oRemovidos)

            Dim oTarefas As IObjectCollection = CType(New EnumerableObjectCollection, IObjectCollection)
            oTarefas.AddObject(CriarAtalho(sExe, gcComandoNovoChamado, "Novo chamado", "Abre o Documentacti em um chamado novo"))
            oTarefas.AddObject(CriarAtalho(sExe, gcComandoAbrirChamado, "Abrir chamado...", "Informe o número e abra o chamado direto"))

            oLista.AddUserTasks(CType(oTarefas, IObjectArray))
            oLista.CommitList()

        Catch
            'Jump List é só atalho: sem ela o sistema funciona normalmente
        End Try

    End Sub

    Private Shared Function CriarAtalho(ByVal sExe As String, ByVal sArgumento As String, ByVal sTitulo As String, ByVal sDescricao As String) As IShellLinkW

        Dim oAtalho As IShellLinkW = CType(New ShellLink, IShellLinkW)

        oAtalho.SetPath(sExe)
        oAtalho.SetArguments(sArgumento)
        oAtalho.SetWorkingDirectory(IO.Path.GetDirectoryName(sExe))
        oAtalho.SetIconLocation(sExe, 0)
        oAtalho.SetDescription(sDescricao)

        'Título do item
        Dim oPropriedades As IPropertyStore = CType(oAtalho, IPropertyStore)
        Dim oValor As New PropVariant
        oValor.vt = VT_LPWSTR
        oValor.pValor = Marshal.StringToCoTaskMemUni(sTitulo)

        Try
            Dim oChave As PropertyKey = PKEY_Title
            oPropriedades.SetValue(oChave, oValor)
            oPropriedades.Commit()
        Finally
            'Libera o texto alocado para o PROPVARIANT
            PropVariantClear(oValor)
        End Try

        Return oAtalho

    End Function

    Public Shared Function Comando(ByVal oArgumentos As IEnumerable(Of String)) As String

        'Primeiro parâmetro reconhecido da linha de comando ("" quando não houver)
        If IsNothing(oArgumentos) Then Return ""

        For Each sArgumento As String In oArgumentos
            Select Case sArgumento.Trim.ToLower
                Case gcComandoNovoChamado, gcComandoAbrirChamado
                    Return sArgumento.Trim.ToLower
            End Select
        Next

        Return ""

    End Function

#End Region

End Class
