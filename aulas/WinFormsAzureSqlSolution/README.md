# WinForms + Azure SQL Database (CRUD Alunos) — Projeto de Aula

Este repositório contém um exemplo completo de **Windows Forms (C#)** com **CRUD** contra **Azure SQL Database**, incluindo **testes unitários (MSTest)**.

## Estrutura
- `WinFormsAzureSql/` — Aplicação WinForms
  - `Models/Aluno.cs`
  - `Data/IAlunoRepository.cs` e `AlunoRepository.cs` (ADO.NET com `Microsoft.Data.SqlClient`)
  - `Services/AlunoService.cs` (validações e orquestração)
  - `Form1.*` (UI com DataGridView + TextBoxes + Botões)
  - `App.config` (connection string)
- `WinFormsAzureSql.Tests/` — Testes unitários MSTest
  - `Fakes/FakeAlunoRepository.cs` (repositório em memória)
  - `AlunoServiceTests.cs`

## Pré‑requisitos
- **.NET 8 SDK** (ou superior)
- **Visual Studio 2022** (ou VS Code)
- Uma instância de **Azure SQL Database** acessível

## Connection String
A aplicação busca a string nesta ordem:
1. Variável de ambiente `AZURE_SQL_CONNECTION_STRING`
2. `App.config` chave `AzureSql`

Exemplo de connection string:
```
Server=tcp:<seu-servidor>.database.windows.net,1433;
Initial Catalog=<sua-base>;
Persist Security Info=False;
User ID=<seu-login>;
Password=<sua-senha>;
MultipleActiveResultSets=False;
Encrypt=True;
TrustServerCertificate=False;
Connection Timeout=30;
```

## Banco e tabela
Crie a tabela com o script em `scripts/schema.sql` ou execute:
```sql
CREATE TABLE Alunos (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Nome NVARCHAR(120) NOT NULL,
  Email NVARCHAR(200) NOT NULL,
  DataCadastro DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
```

## Como executar
1. Abra a solução `WinFormsAzureSql.sln` no Visual Studio.
2. Configure a connection string (variável de ambiente ou App.config).
3. Selecione o projeto **WinFormsAzureSql** como *Startup Project* e rode (F5).

## Testes
- Projeto `WinFormsAzureSql.Tests` (MSTest).
- Execute os testes pelo Test Explorer no Visual Studio.