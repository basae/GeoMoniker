SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_IU_Point
	-- Add the parameters for the stored procedure here
	@Id BIGINT = NULL OUTPUT,
	@Description VARCHAR(100),
	@Lat DECIMAL(10,6),
	@Lng DECIMAL(10,6),
	@IdRute BIGINT,
	@IsStart BIT,
	@IsEnd BIT,
	@OrderRoute INT,
	@LatAreaMax decimal(10,6),
	@LatAreaMin decimal(10,6),
	@LngAreaMax decimal(10,6),
	@LngAreaMin decimal(10,6)
	
AS
BEGIN
BEGIN TRY
	IF(@Id = 0 OR @Id IS NULL)
		BEGIN
			IF((SELECT COUNT(*) FROM ROUTE WHERE ID=@IdRute)=0)
			BEGIN
				RAISERROR('LA RUTA NO EXISTE' ,16 ,1)
			END

			INSERT INTO dbo.Point
			(Description,Lat,Lng,IdRute,IsStart,IsEnd,OrderRoute,LatAreaMax,LatAreaMin,LngAreaMax,LngAreaMin)
			VALUES
			(@Description,@Lat,@Lng,@IdRute,@IsStart,@IsEnd,@OrderRoute,@LatAreaMax,@LatAreaMin,@LngAreaMax,@LngAreaMin)		
			SET @Id=(SELECT SCOPE_IDENTITY())
		END
	ELSE
		BEGIN
			UPDATE dbo.Point SET
			Description=@Description,
			Lat=@Lat,
			Lng=@Lng,
			IsStart=@IsStart,
			IsEnd=@IsEnd,
			OrderRoute=@OrderRoute,
			LatAreaMax=@LatAreaMax,
			LatAreaMin=@LatAreaMin,
			LngAreaMax=@LngAreaMax,
			LngAreaMin=@LngAreaMin		
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
                         ', en la l�nea ' + CAST(@E_LINE AS VARCHAR(10)) +
                         ', con n�mero ' +CAST(@E_NUMBER AS VARCHAR(10)) +
                         ', estado ' + CAST(@E_STATE AS VARCHAR(10)) +
                         ', severidad ' + CAST(@E_SEVERITY AS VARCHAR(10)) +
                         ', y el mensaje de error es: ' + @E_MESSAGE
          select @mensaje
          RAISERROR(@E_MESSAGE ,16 ,1)
          RETURN
  END CATCH
END
GO
