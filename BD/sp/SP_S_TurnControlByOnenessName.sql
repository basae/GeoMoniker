SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_S_TurnControlByOnenessName
	-- Add the parameters for the stored procedure here
	@OnenessName varchar(30),
	@DateControl DATETIME
	
AS
BEGIN
BEGIN TRY
	DECLARE @IDONENESS BIGINT = NULL
	
	SET @IDONENESS=(SELECT ID FROM ONENESS WHERE NAME=@OnenessName)
	
	
	IF (@IDONENESS IS NULL)
	BEGIN
		RAISERROR('LA UNIDAD NO EXISTE' ,16 ,1)
	END
	ELSE
	BEGIN
		
		IF ((SELECT COUNT(*) FROM TURNCONTROL WHERE DATECONTROL=@DateControl AND IDONENESS=@IDONENESS) = 0)
		BEGIN
			RAISERROR('NO EXISTE REGISTRO DE ESTA UNIDAD PARA ESTE DIA' ,16 ,1)
		END
			
		SELECT TC.* FROM
		TURNCONTROL TC INNER JOIN 
		ONENESS ONN ON TC.IDONENESS=ONN.ID 
		WHERE
		ONN.NAME=@OnenessName AND TC.DATECONTROL=@DateControl
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
          
          --SELECT @Mensaje = 'Hubo un error en el SP '+ @E_PROCEDURE +
          --               ', en la línea ' + CAST(@E_LINE AS VARCHAR(10)) +
          --               ', con número ' +CAST(@E_NUMBER AS VARCHAR(10)) +
          --               ', estado ' + CAST(@E_STATE AS VARCHAR(10)) +
          --               ', severidad ' + CAST(@E_SEVERITY AS VARCHAR(10)) +
          --               ', y el mensaje de error es: ' + @E_MESSAGE
          --select @mensaje
          RAISERROR(@E_MESSAGE ,16 ,1)
          RETURN
  END CATCH
END
GO
