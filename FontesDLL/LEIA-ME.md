# Fontes das DLLs próprias (ChatUI e ToastUI)

Código-fonte das DLLs que o Documentacti usa em `Documentacti\Lib`. Fica no repositório para não se perder.

| DLL em `Documentacti\Lib` | Projeto |
|---|---|
| `ChatUI.dll` / `ChatUI.xml` | `ChatUI\src\ChatUI\ChatUI.vbproj` (chat dos chamados, com o botão "atualizar" do composer) |
| `ToastUI.dll` / `ToastUI.xml` | `ChatUI\src\ToastUI\ToastUI.vbproj` (notificações estilo Windows) |

`ChatUI\samples` tem as telas de demonstração das duas DLLs. `ChatUI\README.md` documenta o uso.

## Como gerar a DLL e atualizar o Documentacti

1. Abra `ChatUI\ChatUI.sln` (ou compile só o `.vbproj` da DLL) em **Release**. Alvo: .NET Framework 4.5, o mesmo do Documentacti.
2. Copie `ToastUI.dll` + `ToastUI.xml` (ou `ChatUI.dll` + `ChatUI.xml`) da saída para `Documentacti\Lib`.
3. Recompile o Documentacti.

As DLLs em uso foram geradas com o MSBuild do VS 2019 BuildTools. Compilando estes fontes com ele, a ChatUI sai idêntica à DLL em uso, membro a membro e IL a IL. Com o VS 2022 só muda a forma como o compilador gera 4 métodos internos da coleção de mensagens, sem mudar o comportamento.

## Histórico

- 22/10/2026: versão inicial da ChatUI e da ToastUI.
- 27/10/2026: botão "atualizar" no composer da ChatUI (`ShowRefreshButton` / `RefreshRequested`).
- 13/11/2026: botão **OK (check)** na ToastUI.
  - `Toast.ShowOkButton` e `Toast.OkText` ligam o botão e definem o texto.
  - O clique dispara os eventos `Toast.OkClick` e `ToastManager.ToastOkClicked`.
  - A notificação fecha com `ToastCloseReason.OkButton`.
  - No Documentacti, o OK marca o aviso como lido no sino; no chat, marca a conversa como lida.
