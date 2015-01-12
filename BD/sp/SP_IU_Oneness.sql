SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE SP_IU_Oneness
	-- Add the parameters for the stored procedure here
	@Id BIGINT = NULL OUTPUT,
	@Name VARCHAR(100),
	@Owner VARCHAR(50)=NULL,
	@IdCompany bigint,
	@IdUser bigint,
	@Lat DECIMAL(10,6) = NULL,
	@Lng DECIMAL(10,6)=NULL
	
AS
BEGIN
BEGIN TRY
	IF(@Id = 0 OR @Id IS NULL)
		BEGIN
			IF((SELECT COUNT(*) FROM COMPANY WHERE ID=@IdCompany)=0)
			BEGIN
				RAISERROR('LA COMPAÑIA NO EXISTE' ,16 ,1)
			END
			
			IF((SELECT COUNT(*) FROM USERS WHERE ID=@IdUser AND ACTIVE=1)=0)
			BEGIN
				RAISERROR('EL USUARIO NO EXISTE O ESTA INACTIVO' ,16 ,1)
			END

			INSERT INTO dbo.ONENESS
			(NAME,OWNER,INSERTDATE,USERINSERT,IDCOMPANY,LAT,LNG)
			VALUES
			(@Name,@Owner,GETDATE(),@IdUser,@IdCompany,@Lat,@Lng)		
			SET @Id=(SELECT SCOPE_IDENTITY())
		END
	ELSE
		BEGIN
			UPDATE dbo.ONENESS SET
			Name=@Name,
			Owner=@Owner,
			UpdateDate=GETDATE(),
			UserUpdate=@IdUser,
			Lat=@Lat,
			Lng=@Lng
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
