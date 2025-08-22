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
