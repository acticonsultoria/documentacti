Namespace My

    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    Partial Friend Class MyApplication

        Shared Sub New()

            'DLLs do Lib embutidas no executável: precisa vir antes de qualquer tela (Janus/Dundas)
            Documentacti.clsDllEmbutida.Registrar()

        End Sub

        Private Sub MyApplication_StartupNextInstance(sender As Object, e As ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance

            'Instância única: um segundo exe aberto (ex.: Jump List da barra de tarefas) repassa os parâmetros para cá e fecha
            Try
                e.BringToForeground = True
                Documentacti.frmMain.ExecutarComandoJumpList(e.CommandLine)
            Catch
            End Try

        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException

            Try
                Call TratamentoErro(e.Exception.Message, "Documentacti")
                e.ExitApplication = False
            Catch
            End Try

        End Sub

    End Class

End Namespace
