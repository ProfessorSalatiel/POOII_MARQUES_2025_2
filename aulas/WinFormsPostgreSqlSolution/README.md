# WinForms + Postgre (CRUD Alunos) — Projeto de Aula

Este repositório contém um exemplo completo de **Windows Forms (C#)** com **CRUD** em **Postgre**, incluindo **testes unitários (MSTest)**.

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
- Uma instância no **Postgre** acessível e/ou
- Uma instância no **SQL Server**.
- **Observação para SQL:** Para validar a conexão da chave `SqlLab` que encontra-se no projeto no arquivo `app.config` rodar os seguintes comandos no prompt:

   ```bash
   
   sqllocaldb info
   sqllocaldb start MSSQLLocalDB
   sqllocaldb info MSSQLLocalDB   # só para ver o estado

   sqlcmd -S "(localdb)\MSSQLLocalDB" -d master -Q "SELECT name FROM sys.databases WHERE name='POOII_Lab';"

   sqlcmd -S "(localdb)\MSSQLLocalDB" -d master -Q "IF DB_ID('POOII_Lab') IS NULL CREATE DATABASE [POOII_Lab];"

   ```

## Connection String
A aplicação busca a string nesta ordem:
1. `App.config` chave `Postgres` e/ou
2. `App.config` chave `SqlLab`

Exemplo de connection string:

```sql
<add name="SqlLab"
		connectionString="Data Source=(localdb)\MSSQLLocalDB;
    Initial Catalog=POOII_Lab;
    Integrated Security=True;
    Encrypt=True;
    TrustServerCertificate=True;" />
```

```postgre
<add name="Postgres"
      connectionString="Host=SUPABASE_URL;
      Port=5432;Database=postgres;
      Username=postgres;
      Password=SUA_SENHA;
      SSL Mode=Require;
      Trust Server Certificate=true;" />
```

## Banco e tabela
Crie a tabela com o script em `scripts/schema.sql` ou execute:

```sql
-- Criar o banco de dados (se não existir)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'POOII_Lab')
BEGIN
    CREATE DATABASE POOII_Lab;
END
GO

-- Usar o banco criado
USE POOII_Lab;
GO

-- Criar a tabela Alunos
IF OBJECT_ID('Alunos', 'U') IS NULL
BEGIN
    CREATE TABLE Alunos (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nome NVARCHAR(120) NOT NULL,
        Email NVARCHAR(200) NOT NULL,
        DataCadastro DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
    );
END
GO
```

```postgre

--Criar schema para seu lab (opcional)
create schema if not exists public;

--Tabela dentro do schema public
create table if not exists public.alunos (
  id             bigserial primary key,
  nome           varchar(120) not null,
  email          varchar(200) not null unique,
  data_cadastro  timestamptz  not null default now()
);

create unique index if not exists ux_alunos_email
  on public.alunos (email);

--Testar a tabela
insert into public.alunos (nome, email)
values ('Ana Souza','ana@example.com');

```

## Como executar
1. Abra a solução `WinFormsAzureSql.sln` no Visual Studio.
2. Configure a connection string (App.config).
3. Selecione o projeto **WinFormsAzureSql** como *Startup Project* e rode (F5).

## Testes
- Projeto `WinFormsAzureSql.Tests` (MSTest).
- Execute os testes pelo Test Explorer no Visual Studio.

## Referências
- **Site:** [Supabase](https://supabase.com/ "Site oficial")