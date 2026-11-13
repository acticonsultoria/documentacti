Imports System.Runtime.InteropServices

''' <summary>APIs do Windows usadas para a janela com transparência por pixel (layered window).</summary>
Friend NotInheritable Class NativeMethods
    Private Sub New()
    End Sub

    Public Const WS_EX_TOPMOST As Integer = &H8
    Public Const WS_EX_TOOLWINDOW As Integer = &H80
    Public Const WS_EX_LAYERED As Integer = &H80000
    Public Const WS_EX_NOACTIVATE As Integer = &H8000000
    Public Const WM_MOUSEACTIVATE As Integer = &H21
    Public Const MA_NOACTIVATE As Integer = 3
    Public Const ULW_ALPHA As Integer = 2
    Public Const AC_SRC_OVER As Byte = 0
    Public Const AC_SRC_ALPHA As Byte = 1
    Public Const SWP_NOSIZE As Integer = &H1
    Public Const SWP_NOMOVE As Integer = &H2
    Public Const SWP_NOACTIVATE As Integer = &H10
    Public Shared ReadOnly HWND_TOPMOST As New IntPtr(-1)

    <StructLayout(LayoutKind.Sequential)>
    Public Structure POINTAPI
        Public X As Integer
        Public Y As Integer
        Public Sub New(x As Integer, y As Integer)
            Me.X = x
            Me.Y = y
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure SIZEAPI
        Public CX As Integer
        Public CY As Integer
        Public Sub New(cx As Integer, cy As Integer)
            Me.CX = cx
            Me.CY = cy
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Structure BLENDFUNCTION
        Public BlendOp As Byte
        Public BlendFlags As Byte
        Public SourceConstantAlpha As Byte
        Public AlphaFormat As Byte
    End Structure

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function UpdateLayeredWindow(hwnd As IntPtr, hdcDst As IntPtr, ByRef pptDst As POINTAPI, ByRef psize As SIZEAPI,
                                               hdcSrc As IntPtr, ByRef pptSrc As POINTAPI, crKey As Integer,
                                               ByRef pblend As BLENDFUNCTION, dwFlags As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("user32.dll")>
    Public Shared Function GetDC(hwnd As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Public Shared Function ReleaseDC(hwnd As IntPtr, hdc As IntPtr) As Integer
    End Function

    <DllImport("user32.dll")>
    Public Shared Function SetWindowPos(hwnd As IntPtr, hwndInsertAfter As IntPtr, x As Integer, y As Integer, cx As Integer, cy As Integer, flags As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("user32.dll")>
    Public Shared Function GetForegroundWindow() As IntPtr
    End Function

    <DllImport("user32.dll")>
    Public Shared Function SetForegroundWindow(hwnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("gdi32.dll")>
    Public Shared Function CreateCompatibleDC(hdc As IntPtr) As IntPtr
    End Function

    <DllImport("gdi32.dll")>
    Public Shared Function DeleteDC(hdc As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("gdi32.dll")>
    Public Shared Function SelectObject(hdc As IntPtr, obj As IntPtr) As IntPtr
    End Function

    <DllImport("gdi32.dll")>
    Public Shared Function DeleteObject(obj As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    ''' <summary>Windows "de verdade" (no Mono/Linux usamos o modo simples, sem sombra).</summary>
    Public Shared ReadOnly Property LayeredSupported As Boolean
        Get
            Return Environment.OSVersion.Platform = PlatformID.Win32NT AndAlso Type.GetType("Mono.Runtime") Is Nothing
        End Get
    End Property
End Class
