USE [SuporteUnip];
GO

/*********** PROCEDURE: Listar (tudo ou por id) ***********/
IF OBJECT_ID('dbo.sp_TB_Chamado_Listar', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_TB_Chamado_Listar;
GO

CREATE PROCEDURE dbo.sp_TB_Chamado_Listar
    @id_chamado BIGINT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF @id_chamado IS NULL
        BEGIN
            SELECT id_chamado, desc_chamado, status_chamado, dt_criacao_chamado
            FROM dbo.TB_Chamado
            ORDER BY id_chamado;
        END
        ELSE
        BEGIN
            SELECT id_chamado, desc_chamado, status_chamado, dt_criacao_chamado
            FROM dbo.TB_Chamado
            WHERE id_chamado = @id_chamado;
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrNum INT = ERROR_NUMBER();
        RAISERROR('Erro ao listar chamados. Msg: %s', 16, 1, @ErrMsg);
        RETURN @ErrNum;
    END CATCH
END
GO

/*********** PROCEDURE: Inserir ***********/
IF OBJECT_ID('dbo.sp_TB_Chamado_Inserir', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_TB_Chamado_Inserir;
GO

CREATE PROCEDURE dbo.sp_TB_Chamado_Inserir
    @desc_chamado VARCHAR(50) = NULL,
    @status_chamado BIT = 0,
    @dt_criacao_chamado DATETIME = NULL,
    @new_id_chamado BIGINT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @dt_criacao_chamado IS NULL
            SET @dt_criacao_chamado = GETDATE();

        INSERT INTO dbo.TB_Chamado (desc_chamado, status_chamado, dt_criacao_chamado)
        VALUES (@desc_chamado, @status_chamado, @dt_criacao_chamado);

        SET @new_id_chamado = SCOPE_IDENTITY();

        COMMIT TRANSACTION;
        RETURN 0;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrNum INT = ERROR_NUMBER();
        RAISERROR('Erro ao inserir chamado. Msg: %s', 16, 1, @ErrMsg);
        RETURN @ErrNum;
    END CATCH
END
GO

/*********** PROCEDURE: Alterar ***********/
IF OBJECT_ID('dbo.sp_TB_Chamado_Alterar', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_TB_Chamado_Alterar;
GO

CREATE PROCEDURE dbo.sp_TB_Chamado_Alterar
    @id_chamado BIGINT,
    @desc_chamado VARCHAR(50) = NULL,
    @status_chamado BIT = NULL,
    @dt_criacao_chamado DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM dbo.TB_Chamado WHERE id_chamado = @id_chamado)
        BEGIN
            RAISERROR('Chamado com id %d não encontrado.', 16, 1, @id_chamado);
            ROLLBACK TRANSACTION;
            RETURN 1;
        END

        UPDATE dbo.TB_Chamado
        SET
            desc_chamado = COALESCE(@desc_chamado, desc_chamado),
            status_chamado = COALESCE(@status_chamado, status_chamado),
            dt_criacao_chamado = COALESCE(@dt_criacao_chamado, dt_criacao_chamado)
        WHERE id_chamado = @id_chamado;

        COMMIT TRANSACTION;
        RETURN 0;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrNum INT = ERROR_NUMBER();
        RAISERROR('Erro ao alterar chamado. Msg: %s', 16, 1, @ErrMsg);
        RETURN @ErrNum;
    END CATCH
END
GO

/*********** PROCEDURE: Excluir ***********/
IF OBJECT_ID('dbo.sp_TB_Chamado_Excluir', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_TB_Chamado_Excluir;
GO

CREATE PROCEDURE dbo.sp_TB_Chamado_Excluir
    @id_chamado BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM dbo.TB_Chamado WHERE id_chamado = @id_chamado)
        BEGIN
            RAISERROR('Chamado com id %d não encontrado para exclusão.', 16, 1, @id_chamado);
            ROLLBACK TRANSACTION;
            RETURN 1;
        END

        DELETE FROM dbo.TB_Chamado WHERE id_chamado = @id_chamado;

        COMMIT TRANSACTION;
        RETURN 0;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrNum INT = ERROR_NUMBER();
        RAISERROR('Erro ao excluir chamado. Msg: %s', 16, 1, @ErrMsg);
        RETURN @ErrNum;
    END CATCH
END
GO
