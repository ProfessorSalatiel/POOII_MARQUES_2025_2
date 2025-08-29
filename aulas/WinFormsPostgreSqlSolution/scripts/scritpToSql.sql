-- SQL Server / Azure SQL
-- Banco: POOII_Lab
-- Tabela: dbo.Alunos (Id INT IDENTITY, Nome NVARCHAR(120), Email NVARCHAR(200), DataCadastro DATETIME2 DEFAULT SYSUTCDATETIME())
-- Índice único: UX_Alunos_Email em dbo.Alunos(Email)

/* =====================================================
   1) Criar banco POOII_Lab (se não existir) e usar o banco
   ===================================================== */
IF DB_ID(N'POOII_Lab') IS NULL
BEGIN
    CREATE DATABASE [POOII_Lab];
END
GO

USE [POOII_Lab];
GO

/* =====================================
   2) Garantir schema dbo (por segurança)
   ===================================== */
IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = N'dbo')
BEGIN
    EXEC('CREATE SCHEMA dbo');
END
GO

/* =====================================
   3) Criar tabela dbo.Alunos (se não existir)
   ===================================== */
IF OBJECT_ID(N'dbo.Alunos', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Alunos
    (
        Id            BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Alunos PRIMARY KEY,
        Nome          NVARCHAR(120)        NOT NULL,
        Email         NVARCHAR(200)        NOT NULL,
        DataCadastro  DATETIME2(7)         NOT NULL CONSTRAINT DF_Alunos_DataCadastro DEFAULT SYSUTCDATETIME()
    );
END
GO

/* ====================================================
   4) Criar índice único por e-mail (se ainda não existir)
   ==================================================== */
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = N'UX_Alunos_Email'
      AND object_id = OBJECT_ID(N'dbo.Alunos')
)
BEGIN
    CREATE UNIQUE INDEX UX_Alunos_Email ON dbo.Alunos(Email);
END
GO


/* ====================================================
   5) Explicação sobre os campos
   ==================================================== */

/*
BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Alunos PRIMARY KEY

BIGINT: Use BIGINT se existe chance real de ultrapassar o limite do INT (ou se suas chaves/FX externas já são BIGINT).

IDENTITY(1,1): auto-incremento; começa em 1 e soma 1 a cada linha inserida (não precisa informar o Id no INSERT).

PRIMARY KEY: chave primária → garante unicidade e não nulo; por padrão cria (geralmente) o índice clustered da tabela.

Nome NVARCHAR(120) NOT NULL

NVARCHAR(120): texto Unicode de tamanho variável até 120 caracteres (não bytes). Ideal para nomes com acentos/emoji.

NOT NULL: obrigatório; a coluna não aceita NULL.

Email NVARCHAR(200) NOT NULL

NVARCHAR(200): texto Unicode até 200 caracteres (tamanho variável).

NOT NULL: obrigatório.

DataCadastro DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()

DATETIME2: data/hora com maior faixa e precisão que DATETIME (precisão padrão 7, até ~100 ns).

NOT NULL: obrigatório.

DEFAULT SYSUTCDATETIME(): se você não informar o valor, o SQL Server grava a data/hora atual 
em UTC (função retorna datetime2(7)).
*/