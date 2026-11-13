Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports ToastUI

''' <summary>
''' Exemplos de uso genérico da ToastUI: cadastro salvo, erro, aviso, lembrete fixo, notificação que se atualiza,
''' notificação disparada de outra thread, imagem própria, e configuração de posição/tema.
''' </summary>
Public Class DemoForm
    Inherits Form

    Private ReadOnly _toasts As ToastManager
    Private ReadOnly _log As ListBox
    Private _count As Integer

    Public Sub New()
        Text = "ToastUI — Demo"
        ClientSize = New Size(640, 470)
        StartPosition = FormStartPosition.CenterScreen
        Font = New Font("Segoe UI", 9.0F)

        _toasts = New ToastManager() With {.AppCaption = "Sistema ACTI", .PlaySounds = False}
        AddHandler _toasts.ToastClosed, Sub(s, e) Log(String.Format("Fechada: ""{0}"" ({1})", e.Toast.Title, e.Reason))
        AddHandler _toasts.ToastClicked, Sub(s, e) Log(String.Format("Clique: ""{0}""", e.Toast.Title))

        Dim flow As New FlowLayoutPanel() With {.Dock = DockStyle.Top, .Height = 250, .Padding = New Padding(10), .AutoScroll = True}
        Controls.Add(flow)

        Dim addBtn = Sub(caption As String, action As Action)
                         Dim b As New Button() With {.Text = caption, .Width = 196, .Height = 32, .Margin = New Padding(4)}
                         AddHandler b.Click, Sub() action()
                         flow.Controls.Add(b)
                     End Sub

        addBtn("Cadastro salvo (sucesso)", Sub() _toasts.ShowSuccess("Cliente salvo", "Os dados de ""Padaria Pão Quente Ltda."" foram gravados."))
        addBtn("Informação", Sub() _toasts.ShowInfo("Nova versão disponível", "A versão 3.2 será instalada na próxima vez que o sistema abrir."))
        addBtn("Aviso", Sub() _toasts.ShowWarning("Estoque baixo", "Restam apenas 3 unidades do produto 7891234 (Café 500 g)."))
        addBtn("Erro (fica até fechar)", AddressOf ShowErrorSticky)
        addBtn("Lembrete com clique", AddressOf ShowReminder)
        addBtn("Mensagem de pessoa", Sub() _toasts.ShowMessage("Ana Souza", "Pode revisar o orçamento antes das 16h?"))
        addBtn("Notificação que atualiza", AddressOf ShowUpdating)
        addBtn("De outra thread", AddressOf ShowFromThread)
        addBtn("Imagem própria", AddressOf ShowCustomImage)
        addBtn("Várias de uma vez (fila)", AddressOf ShowMany)
        addBtn("Fechar todas", Sub() _toasts.CloseAll())

        Dim opts As New FlowLayoutPanel() With {.Dock = DockStyle.Top, .Height = 40, .Padding = New Padding(14, 6, 0, 0)}
        Dim pos As New ComboBox() With {.DropDownStyle = ComboBoxStyle.DropDownList, .Width = 140}
        pos.Items.AddRange(New Object() {ToastPosition.BottomRight, ToastPosition.TopRight, ToastPosition.BottomLeft, ToastPosition.TopLeft})
        pos.SelectedIndex = 0
        AddHandler pos.SelectedIndexChanged, Sub() _toasts.Position = CType(pos.SelectedItem, ToastPosition)
        Dim dark As New CheckBox() With {.Text = "Tema escuro", .AutoSize = True, .Margin = New Padding(12, 3, 0, 0)}
        AddHandler dark.CheckedChanged, Sub()
                                            If dark.Checked Then _toasts.Theme.ApplyDark() Else _toasts.Theme.ApplyLight()
                                        End Sub
        Dim sound As New CheckBox() With {.Text = "Som", .AutoSize = True, .Margin = New Padding(12, 3, 0, 0)}
        AddHandler sound.CheckedChanged, Sub() _toasts.PlaySounds = sound.Checked
        opts.Controls.Add(New Label() With {.Text = "Posição:", .AutoSize = True, .Margin = New Padding(0, 6, 4, 0)})
        opts.Controls.Add(pos)
        opts.Controls.Add(dark)
        opts.Controls.Add(sound)
        Controls.Add(opts)
        opts.BringToFront()

        _log = New ListBox() With {.Dock = DockStyle.Fill, .IntegralHeight = False}
        Controls.Add(_log)
        _log.BringToFront()
    End Sub

    Private Sub ShowErrorSticky()
        Dim t As New Toast("Falha ao emitir NF-e", "Rejeição 539: duplicidade de NF-e com diferença na chave de acesso. Clique para ver os detalhes.", ToastType.Error) With {.Duration = 0}
        AddHandler t.Click, Sub(s, e) MessageBox.Show(Me, "Aqui você abriria a tela de detalhes do erro.", t.Title)
        _toasts.Show(t)
    End Sub

    Private Sub ShowReminder()
        Dim t As New Toast("Reunião em 10 minutos", "Alinhamento semanal — sala 2. Clique para adiar 5 min.", ToastType.Info) With {.Duration = 0, .Caption = "Agenda"}
        AddHandler t.Click, Sub(s, e)
                                e.CloseToast = True
                                Log("Lembrete adiado")
                            End Sub
        _toasts.Show(t)
    End Sub

    ''' <summary>Mesma notificação sendo atualizada (ex.: importação em andamento).</summary>
    Private Sub ShowUpdating()
        Dim t As New Toast("Importando planilha", "0 de 250 linhas...", ToastType.Info) With {.Duration = 0, .ShowProgress = False}
        _toasts.Show(t)
        Dim n = 0
        Dim timer As New Timer() With {.Interval = 120}
        AddHandler timer.Tick, Sub()
                                   If Not t.IsOpen Then timer.Dispose() : Return
                                   n += 10
                                   If n < 250 Then
                                       t.Message = String.Format("{0} de 250 linhas...", n)
                                   Else
                                       timer.Dispose()
                                       t.Title = "Importação concluída"
                                       t.Message = "250 linhas importadas, nenhuma com erro."
                                       t.Type = ToastType.Success
                                       t.ShowProgress = True
                                       t.Duration = 4000
                                       t.RestartTimer()
                                   End If
                               End Sub
        timer.Start()
    End Sub

    ''' <summary>Show pode ser chamado de qualquer thread (ex.: callback de socket, Timer de System.Threading).</summary>
    Private Sub ShowFromThread()
        Dim th As New Threading.Thread(Sub()
                                           Threading.Thread.Sleep(800)
                                           _toasts.ShowInfo("Chegou do servidor", "Esta notificação foi disparada por uma thread de fundo.")
                                       End Sub) With {.IsBackground = True}
        th.Start()
    End Sub

    Private Sub ShowCustomImage()
        Dim icon As New Bitmap(64, 64)
        Using g = Graphics.FromImage(icon)
            g.SmoothingMode = SmoothingMode.AntiAlias
            Using b As New LinearGradientBrush(New Rectangle(0, 0, 64, 64), Color.FromArgb(124, 58, 237), Color.FromArgb(236, 72, 153), 45.0F)
                g.FillRectangle(b, 4, 4, 56, 56)
            End Using
            TextRenderer.DrawText(g, "PDF", New Font("Segoe UI", 13.0F, FontStyle.Bold), New Rectangle(0, 0, 64, 64), Color.White,
                                  TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
        End Using
        _toasts.Show(New Toast("Relatório pronto", "Vendas_Outubro.pdf (1,8 MB) foi gerado.", ToastType.Plain) With {.Image = icon, .AccentColor = Color.FromArgb(168, 85, 247)})
    End Sub

    Private Sub ShowMany()
        For i = 1 To 8
            _count += 1
            _toasts.Show(String.Format("Notificação {0}", _count), "As que não cabem esperam na fila e entram quando outra sai.",
                         CType(i Mod 4, ToastType), 3000)
        Next
    End Sub

    Private Sub Log(text As String)
        _log.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss  ") & text)
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        _toasts.Dispose()
        MyBase.OnFormClosed(e)
    End Sub
End Class
