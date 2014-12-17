SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_IU_Route
	-- Add the parameters for the stored procedure here
	@Id bigint = NULL OUTPUT,
	@Name varchar(40),
	@UserInsert bigint,
	@IdCompany bigint
	
AS
BEGIN
BEGIN TRY
	IF(@Id = 0 OR @Id IS NULL)
		BEGIN
		IF((SELECT COUNT(*) FROM COMPANY WHERE id=@IdCompany)=0 OR (SELECT ACTIVE FROM COMPANY WHERE id=@IdCompany)=0)
		BEGIN
			RAISERROR ('LA EMPRESA INGRESADA NO EXISTE O FUE DADA DE BAJA',
						   16,
						   1
						   );
		END

		IF((SELECT COUNT(*) FROM USERS WHERE ID=@UserInsert)=0 OR(SELECT ACTIVE FROM USERS WHERE id=@UserInsert)=0)
		BEGIN
			RAISERROR ('EL USUARIO NO EXISTE O ESTA INACTIVO',
						   16,
						   1
						   );
		END

		INSERT INTO dbo.Route
		(Name,InsertDate,UserInsert,IdCompany)
		VALUES
		(@Name,GETDATE(),@UserInsert,@IdCompany)		
		SET @Id=(SELECT SCOPE_IDENTITY())
		END
	ELSE
		BEGIN
			UPDATE dbo.Route SET
			Name=@Name,
			UpdateDate=GETDATE(),
			UserUpdate=@UserInsert
			WHERE Id=@Id
			SET @Id=0;
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
          select @E_MESSAGE
          RAISERROR(@E_MESSAGE ,16 ,1)
          RETURN
  END CATCH
END
GO
