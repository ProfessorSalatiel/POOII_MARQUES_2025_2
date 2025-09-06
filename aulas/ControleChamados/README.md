# 🧰 Controle de Chamados — Exercício em Sala (UNIP)

**Professor:** Salatiel Luz Marinho  
**Curso:** Análise e Desenvolvimento de Sistemas — *Universidade Paulista (UNIP)*  
**Projeto:** Aplicação **Windows Forms (.NET Framework 4.7.2)** para **abrir chamados** e persistir no **SQL Server** via **ADO.NET**.

---

## ✨ Objetivo do exercício
Construir uma telinha WinForms para **incluir um chamado** (descrição → salvar no banco). O foco é entender:
- 🖱️ **Eventos** de UI (clique do botão `Cadastrar`)
- 🧩 **Componentes** WinForms (`Label`, `TextBox`, `Button`)
- 🔗 **ADO.NET** (`SqlConnection`, `SqlCommand`, `ExecuteNonQuery`)
- 🛠️ **ConnectionStrings** no `App.config`
- 🗄️ **Criação de database e tabela** no SQL Server

---

## 🗂️ Estrutura (resumo)
```
ControleChamados/
 ├─ ControleChamados.sln
 └─ ControleChamados/
    ├─ Program.cs                 → ponto de entrada
    ├─ frmIncluirChamado.cs       → lógica do formulário
    ├─ frmIncluirChamado.Designer.cs
    ├─ App.config                 → connectionStrings (⚠️ ajustar)
    └─ Properties/…
```

> 🧭 **Tela principal**: `frmIncluirChamado` com `txtDescricaoChamado` e `btnCadastrarChamado`.
No clique do botão, a aplicação abre conexão (string **PROFSALATIEL**), monta o `INSERT` e grava na tabela `TB_Chamado`.

---

## ▶️ Como rodar
1. **Pré‑requisitos**
   - Windows 10/11
   - **Visual Studio 2019/2022** (Desktop development with .NET)
   - **SQL Server** (LocalDB, Express ou Developer) + SQL Server Management Studio (SSMS)

2. **Clonar/abrir o projeto**
   - Abra `ControleChamados.sln` no Visual Studio
   - Se necessário, selecione **Any CPU** e **Debug**

3. **Criar o banco e a tabela**
   - Abra o **SSMS** e execute os scripts abaixo (primeiro o *database*, depois a *tabela*).

4. **Ajustar a connection string**
   - Edite `App.config` → `connectionStrings` → **PROFSALATIEL**
   - Exemplo para **LocalDB** (padrão do projeto):
     ```xml
     <add name="PROFSALATIEL"
          connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SuporteUnip;Integrated Security=True;Encrypt=False"/>
     ```
   - Se usar **SQL Server Express/Developer** com login/senha:
     ```xml
     <add name="PROFSALATIEL"
          connectionString="Server=SEU_SERVIDOR,1433;Database=SuporteUnip;User Id=SEU_LOGIN;Password=SUA_SENHA;TrustServerCertificate=True"/>
     ```

5. **Executar**
   - `F5` (Debug) → a tela abre
   - Digite a **descrição do chamado** → clique em **Cadastrar**
   - Uma mensagem de **sucesso** aparece; o campo é limpo e volta o foco ✨

---

## 🧠 Conceitos aplicados (com “ícones” no texto)
- ▶️ **Windows Forms**: programação **orientada a eventos** (ex.: `btnCadastrarChamado_Click`).
- 🧪 **Validação mínima**: garantir que a descrição não esteja vazia antes de gravar.
- 🧵 **`STAThread`**: `Program.cs` inicia a aplicação WinForms com `Application.Run(...)`.
- 🔐 **`App.config`**: centraliza as **connectionStrings** (evita hard‑code no código).
- 🧭 **ADO.NET**: uso de `SqlConnection` (abre/fecha), `SqlCommand` (comando SQL) e `ExecuteNonQuery` (linha(s) afetada(s)).
- 🧹 **Boas práticas**: `using`/`try‑finally` para garantir `Dispose/Close`, feedback ao usuário, limpar campos, foco de volta.

> 💡 *Extensões didáticas*: listar chamados com `SELECT`, marcar como **resolvido** (coluna `status_chamado`), e exibir **data/hora** de abertura.

---

## 🗃️ Scripts SQL (fornecidos)

### 1) 📦 Criar **database** `scripts/SuporteUnip.sql`
> **Observação:** o caminho de arquivos abaixo é específico do Windows do professor.  
> Se preferir usar **LocalDB** sem caminho físico, veja uma **alternativa** logo após este script.

```sql
USE [master];
GO

CREATE DATABASE SuporteUnip;
GO
```

---

### 2) 🧾 Criar **tabela** `scripts/TB_Chamado.sql`
```sql
USE [SuporteUnip]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TB_Chamado](
	[id_chamado] [bigint] IDENTITY(1,1) NOT NULL,
	[desc_chamado] [varchar](50) NULL,
	[status_chamado] [bit] NULL,
	[dt_criacao_chamado] [datetime] NULL,
 CONSTRAINT [PK_TB_Chamado] PRIMARY KEY CLUSTERED 
(
	[id_chamado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```

**Inserir um registro de teste (opcional):**
```sql
INSERT INTO dbo.TB_Chamado (desc_chamado, status_chamado, dt_criacao_chamado)
VALUES ('Primeiro chamado (teste)', 1, GETDATE());
```

---

## 🔧 Onde a connection string é usada?
No `App.config`:
```xml
<connectionStrings>
  <add name="PROFSALATIEL"
       connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SuporteUnip;Integrated Security=True;Encrypt=False"/>
</connectionStrings>
```
No código do formulário (em `frmIncluirChamado.cs`), é lida via `ConfigurationManager.ConnectionStrings["PROFSALATIEL"].ConnectionString`.  
A gravação usa `SqlConnection`, `SqlCommand` e `ExecuteNonQuery()`.

> ✅ Ao salvar com sucesso, o formulário **limpa o campo** e **retorna o foco** para `txtDescricaoChamado`.

---

## 🧭 Próximos passos sugeridos (para fixação)
- 📋 **Listar chamados** em um `DataGridView` (`SELECT ... ORDER BY dt_criacao_chamado DESC`)
- ✅ **Concluir chamado**: atualizar `status_chamado = 1`
- 🕒 **Preencher automaticamente** `dt_criacao_chamado = GETDATE()` no `INSERT`
- 🧪 **Tratamento de erros** com `try/catch` + `MessageBox.Show` amigável
- 🧼 **`using`** (ou `try/finally`) em todas as conexões/comandos
- ✍️ **Validação** do tamanho do texto (`varchar(50)` no banco)
- 🧱 **Camadas** (futuro): separar **DAL/Repository** do **Form**

---

## ❓FAQ rápido
- **Erro de conexão?** Verifique se o banco `SuporteUnip` existe e se a connection string aponta para ele.
- **Sem LocalDB?** Instale o *SQL Server Express* ou *Developer* e ajuste a connection string.
- **Sem SSMS?** Pode usar o *SQL Server Data Tools* (no próprio VS) ou rodar os scripts pelo *sqlcmd*.

---

**Bons estudos!** 👩‍💻👨‍💻  
*UNIP — Disciplina prática com foco em fundamentos sólidos e mão na massa - Professor Salatiel Luz Marinho*