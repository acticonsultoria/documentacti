Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports ChatUI
Imports ToastUI

''' <summary>
''' Demonstração do ChatUI: várias conversas em abas, bot que responde, grupo, imagens, anexos e status.
''' Tudo é criado por código para ficar fácil de ler; no seu projeto você pode arrastar o ChatTabs/ChatBox da Toolbox.
''' </summary>
Public Class MainForm
    Inherits Form

    Private ReadOnly _chat As ChatTabs
    Private ReadOnly _toasts As New ToastManager() With {.AppCaption = "Chat ACTI", .MaxVisible = 4}
    Private ReadOnly _convToasts As New Dictionary(Of ChatConversation, Toast)()
    Private ReadOnly _me As New ChatUser("me", "Gabriel Ribeiro")
    Private ReadOnly _bot As New ChatUser("bot", "Assistente ACTI") With {.IsOnline = True, .AvatarColor = Color.FromArgb(37, 99, 235)}
    Private ReadOnly _ana As New ChatUser("ana", "Ana Souza") With {.IsOnline = True}
    Private ReadOnly _marcos As New ChatUser("marcos", "Marcos Lima") With {.StatusText = "visto por último hoje às 09:12"}
    Private ReadOnly _carlos As New ChatUser("carlos", "Carlos Mendes")
    Private ReadOnly _julia As New ChatUser("julia", "Júlia Castro")
    Private ReadOnly _group As New ChatUser("erp", "Projeto ERP") With {.StatusText = "Carlos, Júlia, você"}
    Private ReadOnly _rnd As New Random(7)
    Private ReadOnly _shotDir As String
    Private _olderLoaded As New HashSet(Of ChatConversation)()

    Public Sub New(args As String())
        Text = "ChatUI — Demo (WinForms .NET 4.5.1)"
        ClientSize = New Size(1000, 660)
        StartPosition = FormStartPosition.CenterScreen
        Font = New Font("Segoe UI", 9.0F)
        If args IsNot Nothing AndAlso args.Length >= 2 AndAlso args(0) = "--shot" Then _shotDir = args(1)

        ' ---- barra de ferramentas da demo
        Dim tool As New ToolStrip() With {.GripStyle = ToolStripGripStyle.Hidden, .Padding = New Padding(6, 2, 6, 2)}
        tool.Items.Add(New ToolStripButton("Nova conversa", Nothing, AddressOf OnNewConversation))
        tool.Items.Add(New ToolStripButton("Simular mensagem recebida", Nothing, AddressOf OnSimulateIncoming))
        tool.Items.Add(New ToolStripButton("Carregar 500 mensagens", Nothing, AddressOf OnBulk))
        tool.Items.Add(New ToolStripSeparator())
        Dim btnList As New ToolStripButton("Estilo lista") With {.CheckOnClick = True}
        AddHandler btnList.CheckedChanged, Sub() _chat.TabStyle = If(btnList.Checked, ChatTabStyle.List, ChatTabStyle.Tabs)
        Dim btnDark As New ToolStripButton("Tema escuro") With {.CheckOnClick = True}
        AddHandler btnDark.CheckedChanged, Sub()
                                                If btnDark.Checked Then _chat.Theme.ApplyDark() Else _chat.Theme.ApplyLight()
                                                If btnDark.Checked Then _toasts.Theme.ApplyDark() Else _toasts.Theme.ApplyLight()
                                            End Sub
        tool.Items.Add(btnList)
        tool.Items.Add(btnDark)

        ' ---- o componente
        _chat = New ChatTabs() With {.Dock = DockStyle.Fill}
        _chat.CurrentUser = _me
        Controls.Add(_chat)
        Controls.Add(tool)

        AddHandler _chat.MessageSubmitted, AddressOf OnMessageSubmitted
        AddHandler _chat.MessageReceived, AddressOf OnMessageReceived
        AddHandler _chat.SelectedConversationChanged, Sub(s, e) UpdateTitle()
        AddHandler _chat.ChatBox.RetryRequested, AddressOf OnRetry
        AddHandler _chat.ChatBox.LoadOlderRequested, AddressOf OnLoadOlder
        AddHandler _chat.ConversationClosing, Sub(s, e)
                                                   If e.Conversation.Contact Is _bot Then
                                                       e.Cancel = MessageBox.Show(Me, "Fechar a conversa com o assistente?", "ChatUI",
                                                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No
                                                   End If
                                               End Sub

        _ana.Avatar = MakeAvatarPhoto(Color.FromArgb(251, 146, 60), Color.FromArgb(244, 63, 94))
        _carlos.Avatar = MakeAvatarPhoto(Color.FromArgb(45, 212, 191), Color.FromArgb(14, 116, 144))
        SeedData()
    End Sub

#Region "Dados de exemplo"

    Private Sub SeedData()
        Dim today = Date.Today
        Dim yesterday = today.AddDays(-1)

        ' ---------- Assistente (bot)
        Dim bot = _chat.AddConversation(_bot, False)
        Dim list As New List(Of ChatMessage)()
        list.Add(Sys("Conversa criptografada de ponta a ponta", yesterday.AddHours(17)))
        list.Add(Msg(_bot, "Olá, Gabriel! Sou o assistente da ACTI. Posso abrir chamados, consultar status e enviar relatórios.", yesterday.AddHours(17).AddMinutes(2), MessageDirection.Incoming))
        list.Add(Msg(_me, "Oi! Me manda o relatório de outubro, por favor.", yesterday.AddHours(17).AddMinutes(4), MessageDirection.Outgoing, MessageStatus.Read))
        Dim pdf = Msg(_bot, "", yesterday.AddHours(17).AddMinutes(5), MessageDirection.Incoming)
        pdf.Attachment = New ChatAttachment("Relatorio_Outubro_2026.pdf", Nothing, 2516582)
        list.Add(pdf)
        list.Add(Msg(_bot, "Aqui está. Também gerei o gráfico de chamados por semana:", yesterday.AddHours(17).AddMinutes(5), MessageDirection.Incoming))
        Dim chart = Msg(_bot, "", yesterday.AddHours(17).AddMinutes(6), MessageDirection.Incoming)
        chart.Image = MakeChartImage()
        list.Add(chart)
        list.Add(Msg(_me, "Perfeito, obrigado!", today.AddHours(8).AddMinutes(41), MessageDirection.Outgoing, MessageStatus.Read))
        list.Add(Msg(_me, "Consegue abrir um chamado para a impressora do 3º andar? Está com atolamento de papel desde ontem e o pessoal do financeiro precisa imprimir as notas hoje até as 16h.", today.AddHours(8).AddMinutes(42), MessageDirection.Outgoing, MessageStatus.Delivered))
        bot.Messages.AddRange(list)

        ' ---------- Ana (com foto, mensagens não lidas)
        Dim ana = _chat.AddConversation(_ana, False)
        ana.Messages.AddRange(New ChatMessage() {
            Msg(_ana, "Bom dia! Conseguiu ver a proposta?", today.AddHours(9).AddMinutes(3), MessageDirection.Incoming),
            Msg(_me, "Bom dia, Ana! Vi sim, ficou ótima!", today.AddHours(9).AddMinutes(10), MessageDirection.Outgoing, MessageStatus.Read),
            Msg(_ana, "Que bom! Posso enviar para o cliente então?", today.AddHours(9).AddMinutes(11), MessageDirection.Incoming),
            Msg(_ana, "Ah, e a reunião de amanhã mudou para 14h.", today.AddHours(9).AddMinutes(11), MessageDirection.Incoming)})
        ana.UnreadCount = 2

        ' ---------- Grupo
        Dim grp As New ChatConversation("erp", "Projeto ERP", True) With {.Contact = _group}
        grp.Messages.AddRange(New ChatMessage() {
            Sys("Carlos adicionou você ao grupo", today.AddHours(10)),
            Msg(_carlos, "Pessoal, o deploy da versão 3.2 ficou para sexta.", today.AddHours(10).AddMinutes(5), MessageDirection.Incoming),
            Msg(_carlos, "Alguém consegue revisar o PR do módulo fiscal?", today.AddHours(10).AddMinutes(6), MessageDirection.Incoming),
            Msg(_julia, "Eu reviso depois do almoço.", today.AddHours(10).AddMinutes(12), MessageDirection.Incoming),
            Msg(_me, "Fechado. Eu cuido dos testes de integração.", today.AddHours(10).AddMinutes(15), MessageDirection.Outgoing, MessageStatus.Read)})
        _chat.AddConversation(grp, False)
        grp.TypingUser = _carlos
        grp.IsTyping = True

        ' ---------- Marcos (offline, mensagem com falha)
        Dim marcos = _chat.AddConversation(_marcos, False)
        marcos.Messages.AddRange(New ChatMessage() {
            Msg(_marcos, "Gabriel, segue o link da documentação da API. Qualquer dúvida me chama.", today.AddDays(-3).AddHours(15), MessageDirection.Incoming),
            Msg(_me, "Valeu, Marcos!", today.AddHours(11).AddMinutes(2), MessageDirection.Outgoing, MessageStatus.Failed)})

        _chat.SelectedConversation = bot
        bot.TypingUser = _bot
    End Sub

    Private Shared Function Msg(sender As ChatUser, text As String, ts As DateTime, dir As MessageDirection, Optional status As MessageStatus = MessageStatus.None) As ChatMessage
        Return New ChatMessage(sender, text, dir) With {.Timestamp = ts, .Status = status}
    End Function

    Private Shared Function Sys(text As String, ts As DateTime) As ChatMessage
        Return New ChatMessage(Nothing, text, MessageDirection.System) With {.Timestamp = ts}
    End Function

#End Region

#Region "Notificações (ToastUI)"

    ''' <summary>Mensagem recebida: se o usuário não está olhando essa conversa, mostra uma notificação.</summary>
    Private Sub OnMessageReceived(sender As Object, e As ChatMessageSubmittedEventArgs)
        UpdateTitle()
        Dim looking = Form.ActiveForm Is Me AndAlso WindowState <> FormWindowState.Minimized AndAlso _chat.SelectedConversation Is e.Conversation
        If looking Then Return

        Dim m = e.Message
        Dim who = m.Sender
        Dim title = If(who Is Nothing, e.Conversation.Title, who.DisplayName)
        If e.Conversation.IsGroup Then title = title & " · " & e.Conversation.Title
        Dim body = m.Text
        If m.Image IsNot Nothing Then
            body = If(body.Length > 0, "Foto: " & body, "Foto")
        ElseIf m.Attachment IsNot Nothing Then
            body = "Arquivo: " & m.Attachment.FileName
        End If

        ' uma notificação por conversa: se já existe uma aberta, só atualiza o texto
        Dim existing As Toast = Nothing
        If _convToasts.TryGetValue(e.Conversation, existing) AndAlso existing.IsOpen Then
            existing.Title = title
            existing.Message = body
            existing.Caption = String.Format("Chat ACTI  ·  {0} novas", e.Conversation.UnreadCount)
            existing.RestartTimer()
            Return
        End If

        Dim t As New Toast(title, body, ToastType.Message) With {.Tag = e.Conversation, .Duration = 6000}
        If who IsNot Nothing Then
            t.Image = who.Avatar
            t.AccentColor = who.EffectiveColor
        End If
        AddHandler t.Click, Sub(s, a) OpenConversation(DirectCast(t.Tag, ChatConversation))
        _convToasts(e.Conversation) = t
        _toasts.Show(t)
    End Sub

    ''' <summary>Clique na notificação: traz a janela para frente e abre a conversa.</summary>
    Private Sub OpenConversation(conv As ChatConversation)
        If WindowState = FormWindowState.Minimized Then WindowState = FormWindowState.Normal
        Show()
        Activate()
        _chat.SelectedConversation = conv
        _chat.ChatBox.FocusComposer()
    End Sub

    Protected Overrides Sub OnActivated(e As EventArgs)
        MyBase.OnActivated(e)
        ' ao voltar para a conversa que tinha notificação aberta, fecha a notificação
        Dim t As Toast = Nothing
        If _chat IsNot Nothing AndAlso _chat.SelectedConversation IsNot Nothing AndAlso _convToasts.TryGetValue(_chat.SelectedConversation, t) Then t.Close()
    End Sub

#End Region

#Region "Simulação de servidor"

    ''' <summary>Aqui você mandaria a mensagem para o seu backend. Na demo, simulamos os status e uma resposta.</summary>
    Private Sub OnMessageSubmitted(sender As Object, e As ChatMessageSubmittedEventArgs)
        Dim m = e.Message
        Dim conv = e.Conversation
        Later(300, Sub() m.Status = MessageStatus.Sent)
        Later(800, Sub() m.Status = MessageStatus.Delivered)
        If conv.Contact Is _marcos Then Return ' offline: fica em "entregue"
        Later(1300, Sub()
                        m.Status = MessageStatus.Read
                        If conv.IsGroup Then conv.TypingUser = If(_rnd.Next(2) = 0, _carlos, _julia)
                        conv.IsTyping = True
                    End Sub)
        Later(2700, Sub()
                        conv.IsTyping = False
                        Dim who = If(conv.IsGroup, conv.TypingUser, conv.Contact)
                        conv.AddIncoming(who, ReplyFor(m))
                    End Sub)
    End Sub

    Private Function ReplyFor(m As ChatMessage) As String
        If m.Image IsNot Nothing Then Return "Recebi a imagem, obrigado!"
        If m.Attachment IsNot Nothing Then
            Return String.Format("Recebi o arquivo {0}{1}.", m.Attachment.FileName,
                                 If(m.Attachment.Size >= 0, " (" & (m.Attachment.Size / 1024.0).ToString("0.#") & " KB)", ""))
        End If
        Dim answers = {"Certo, anotado!", "Pode deixar, já vejo isso.", "Entendi: """ & m.Text & """", "Perfeito!", "Combinado. Te aviso quando terminar."}
        Return answers(_rnd.Next(answers.Length))
    End Function

    Private Sub OnRetry(sender As Object, e As ChatMessageEventArgs)
        e.Message.Status = MessageStatus.Sending
        Later(700, Sub() e.Message.Status = MessageStatus.Sent)
        Later(1200, Sub() e.Message.Status = MessageStatus.Delivered)
    End Sub

    ''' <summary>Histórico sob demanda: ao rolar até o topo, insere mensagens antigas.</summary>
    Private Sub OnLoadOlder(sender As Object, e As ChatConversationEventArgs)
        Dim conv = e.Conversation
        If conv Is Nothing OrElse _olderLoaded.Contains(conv) OrElse conv.Messages.Count = 0 Then Return
        _olderLoaded.Add(conv)
        Dim first = conv.Messages(0).Timestamp
        Dim older As New List(Of ChatMessage)()
        For i = 1 To 20
            Dim incoming = (i Mod 3 <> 0)
            older.Add(Msg(If(incoming, If(conv.Contact, _bot), _me), "Mensagem antiga nº " & i.ToString() & " carregada sob demanda.",
                          first.AddDays(-2).AddMinutes(i * 7), If(incoming, MessageDirection.Incoming, MessageDirection.Outgoing),
                          If(incoming, MessageStatus.None, MessageStatus.Read)))
        Next
        conv.Messages.InsertRange(0, older)
    End Sub

    Private Sub OnSimulateIncoming(sender As Object, e As EventArgs)
        Dim targets = New ChatConversation() {_chat.Conversations.FindById("ana"), _chat.Conversations.FindById("erp")}
        Dim conv = targets(_rnd.Next(targets.Length))
        If conv Is Nothing Then conv = _chat.SelectedConversation
        If conv Is Nothing Then Return
        Dim who = If(conv.IsGroup, If(_rnd.Next(2) = 0, _carlos, _julia), conv.Contact)
        conv.IsTyping = False
        conv.AddIncoming(who, "Mensagem nova às " & DateTime.Now.ToString("HH:mm:ss"))
    End Sub

    Private Sub OnBulk(sender As Object, e As EventArgs)
        Dim conv = _chat.SelectedConversation
        If conv Is Nothing Then Return
        Dim baseTime = DateTime.Now.AddDays(-4)
        Dim items As New List(Of ChatMessage)()
        Dim words = "lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua".Split(" "c)
        For i = 1 To 500
            Dim n = _rnd.Next(2, 40)
            Dim sb As New Global.System.Text.StringBuilder()
            For w = 1 To n
                sb.Append(words(_rnd.Next(words.Length))).Append(" "c)
            Next
            Dim incoming = _rnd.Next(2) = 0
            items.Add(Msg(If(incoming, If(conv.Contact, _bot), _me), sb.ToString().Trim(), baseTime.AddMinutes(i * 11),
                          If(incoming, MessageDirection.Incoming, MessageDirection.Outgoing), If(incoming, MessageStatus.None, MessageStatus.Read)))
        Next
        conv.Messages.AddRange(items)
    End Sub

    Private Sub OnNewConversation(sender As Object, e As EventArgs)
        Dim names = {"Beatriz Alves", "Rafael Torres", "Camila Nunes", "Lucas Prado", "Fernanda Reis"}
        Dim name = names(_rnd.Next(names.Length))
        Dim u As New ChatUser(Guid.NewGuid().ToString("N"), name) With {.IsOnline = _rnd.Next(2) = 0}
        Dim c = _chat.AddConversation(u)
        c.AddSystem("Nova conversa com " & name)
        _chat.ChatBox.FocusComposer()
    End Sub

    Private Sub UpdateTitle()
        Dim n = _chat.TotalUnread
        Text = If(n > 0, String.Format("({0}) ChatUI — Demo", n), "ChatUI — Demo (WinForms .NET 4.5.1)")
    End Sub

    ''' <summary>Executa uma ação na thread da UI depois de alguns milissegundos.</summary>
    Private Sub Later(ms As Integer, action As Action)
        Dim t As New Timer() With {.Interval = ms}
        AddHandler t.Tick, Sub()
                               t.Stop()
                               t.Dispose()
                               If Not IsDisposed Then action()
                           End Sub
        t.Start()
    End Sub

#End Region

#Region "Imagens geradas (para a demo não depender de arquivos)"

    Private Shared Function MakeAvatarPhoto(c1 As Color, c2 As Color) As Bitmap
        Dim bmp As New Bitmap(128, 128)
        Using g = Graphics.FromImage(bmp)
            g.SmoothingMode = SmoothingMode.AntiAlias
            Using lg As New LinearGradientBrush(New Rectangle(0, 0, 128, 128), c1, c2, 45.0F)
                g.FillRectangle(lg, 0, 0, 128, 128)
            End Using
            Using b As New SolidBrush(Color.FromArgb(235, 255, 255, 255))
                g.FillEllipse(b, 40, 22, 48, 48)
                g.FillEllipse(b, 18, 78, 92, 80)
            End Using
        End Using
        Return bmp
    End Function

    Private Shared Function MakeChartImage() As Bitmap
        Dim w = 560, h = 340
        Dim bmp As New Bitmap(w, h)
        Using g = Graphics.FromImage(bmp)
            g.SmoothingMode = SmoothingMode.AntiAlias
            Using lg As New LinearGradientBrush(New Rectangle(0, 0, w, h), Color.FromArgb(30, 41, 59), Color.FromArgb(15, 23, 42), 90.0F)
                g.FillRectangle(lg, 0, 0, w, h)
            End Using
            Using f As New Font("Segoe UI", 15.0F, FontStyle.Bold), fs As New Font("Segoe UI", 10.0F)
                TextRenderer.DrawText(g, "Chamados por semana", f, New Point(28, 22), Color.White)
                TextRenderer.DrawText(g, "Outubro / 2026", fs, New Point(30, 52), Color.FromArgb(148, 163, 184))
                Dim vals = {34, 52, 41, 67, 58, 73}
                Dim bw = 52, gap = 30, x0 = 40, baseY = h - 50
                For i = 0 To vals.Length - 1
                    Dim bh = vals(i) * 3
                    Dim rc As New Rectangle(x0 + i * (bw + gap), baseY - bh, bw, bh)
                    Using br As New LinearGradientBrush(rc, Color.FromArgb(96, 165, 250), Color.FromArgb(37, 99, 235), 90.0F)
                        g.FillRectangle(br, rc)
                    End Using
                    TextRenderer.DrawText(g, "S" & (i + 1).ToString(), fs, New Rectangle(rc.X, baseY + 8, bw, 20), Color.FromArgb(148, 163, 184), TextFormatFlags.HorizontalCenter)
                    TextRenderer.DrawText(g, vals(i).ToString(), fs, New Rectangle(rc.X, rc.Y - 22, bw, 20), Color.White, TextFormatFlags.HorizontalCenter)
                Next
            End Using
        End Using
        Return bmp
    End Function

#End Region

#Region "Captura de tela automática (usada só para gerar as imagens do README)"

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        If String.IsNullOrEmpty(_shotDir) Then Return
        Later(1500, Sub()
                        Shot("01-abas-claro.png")
                        _chat.TabStyle = ChatTabStyle.List
                        _chat.Theme.ApplyDark()
                        Later(1200, Sub()
                                        Shot("02-lista-escuro.png")
                                        _chat.SelectedIndex = 2
                                        Later(1200, Sub()
                                                        Shot("03-grupo-escuro.png")
                                                        Close()
                                                    End Sub)
                                    End Sub)
                    End Sub)
    End Sub

    Private Sub Shot(name As String)
        Dim rc = RectangleToScreen(ClientRectangle)
        Using bmp As New Bitmap(rc.Width, rc.Height)
            Using g = Graphics.FromImage(bmp)
                g.CopyFromScreen(rc.Location, Point.Empty, rc.Size)
            End Using
            bmp.Save(IO.Path.Combine(_shotDir, name), Imaging.ImageFormat.Png)
        End Using
    End Sub

#End Region

End Class
