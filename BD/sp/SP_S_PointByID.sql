SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_S_PointByID
	-- Add the parameters for the stored procedure here
	@IdPoint BIGINT
	
AS
BEGIN
BEGIN TRY
	IF((SELECT COUNT(*) FROM POINT WHERE ID=@IdPoint)=0)
	BEGIN
		RAISERROR('LA TERMINAL NO EXISTE' ,16 ,1)
	END

	SELECT ID,DESCRIPTION,LAT,LNG,ISSTART,ISEND,ORDERROUTE,LatAreaMax,LatAreaMin,LngAreaMax,LngAreaMin
	FROM
	POINT
	WHERE ID=@IdPoint

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
