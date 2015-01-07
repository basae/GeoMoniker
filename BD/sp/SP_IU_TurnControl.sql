SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_IU_TurnControl
	-- Add the parameters for the stored procedure here
	@Id BIGINT = NULL OUTPUT,
	@DateControl DATETIME,
	@IdTurn BIGINT,
	@IdOneness BIGINT
	
AS
BEGIN
BEGIN TRY
	IF(@Id = 0 OR @Id IS NULL)
		BEGIN
			IF((SELECT COUNT(*) FROM TURNS WHERE ID=@IdTurn)=0)
			BEGIN
				RAISERROR('LA VUELTA NO EXISTE' ,16 ,1)
			END
			
			IF((SELECT COUNT(*) FROM ONENESS WHERE ID=@IdOneness)=0)
			BEGIN
				RAISERROR('LA UNIDAD NO EXISTE' ,16 ,1)
			END
			
			IF((SELECT COUNT(*) FROM TURNCONTROL WHERE DATECONTROL=@DateControl AND (IDTURN=@IdTurn OR IDONENESS=@IdOneness))>0)
			BEGIN
				RAISERROR('LA UNIDAD O VUELTA YA SE HA ASIGNADO EN ESTE DIA' ,16 ,1)
			END

			INSERT INTO dbo.TURNCONTROL
			(DateControl,IdTurn,IdOneness)
			VALUES
			(@DateControl,@IdTurn,@IdOneness)		
			SET @Id=(SELECT SCOPE_IDENTITY())
		END
	ELSE
		BEGIN
			IF((SELECT COUNT(*) FROM TURNCONTROL WHERE DATECONTROL=@DateControl AND (IDTURN=@IdTurn OR IDONENESS=@IdOneness))>0)
			BEGIN
				RAISERROR('LA UNIDAD O VUELTA YA SE HA ASIGNADO EN ESTE DIA' ,16 ,1)
			END
			
			UPDATE dbo.TURNCONTROL SET
			DateControl=@DateControl,
			IdTurn=@IdTurn,
			IdOneness=@IdOneness	
			WHERE Id=@Id
			SET @Id=1;
		END
END TRY
BEGIN CATCH
IF @@TRANCOUNT > 0 ROLLBACK TRAN
          DECLARE @E_NUMBER     INT
                 ,@E_SEVERITY   INT
                 ,@E_STATE      INT
                 ,@E_PROCEDURE  NVARCHAR(126)
                 ,@E_LINE       INT
                 ,@E_MESSAGE    NVARCHAR(2048)
                 ,@Mensaje      NVARCHAR(MAX)
          
          SELECT @E_NUMBER = ERROR_NUMBER()
                ,@E_SEVERITY = ERROR_SEVERITY()
                ,@E_STATE = ERROR_STATE()
                ,@E_PROCEDURE = ERROR_PROCEDURE()
                ,@E_LINE = ERROR_LINE()
                ,@E_MESSAGE = ERROR_MESSAGE()
          
          SELECT @Mensaje = 'Hubo un error en el SP '+ @E_PROCEDURE +
                         ', en la línea ' + CAST(@E_LINE AS VARCHAR(10)) +
                         ', con número ' +CAST(@E_NUMBER AS VARCHAR(10)) +
                         ', estado ' + CAST(@E_STATE AS VARCHAR(10)) +
                         ', severidad ' + CAST(@E_SEVERITY AS VARCHAR(10)) +
                         ', y el mensaje de error es: ' + @E_MESSAGE
          select @mensaje
          RAISERROR(@E_MESSAGE ,16 ,1)
          RETURN
  END CATCH
END
GO
