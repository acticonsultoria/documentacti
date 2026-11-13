Imports System.Windows.Forms

Module Program
    <STAThread()>
    Sub Main(args As String())
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New MainForm(args))
    End Sub
End Module
