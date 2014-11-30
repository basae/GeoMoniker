SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE SP_IU_User
	-- Add the parameters for the stored procedure here
	@Id bigint=null output,
	@FirstName varchar(40),
	@LastName varchar(40),
	@Celphone varchar(30)=null,
	@Email varchar(100)=null,
	@UserTypeId bigint,
	@IdCompany bigint,
	--address params
	@Street varchar(50)=null,
	@Number varchar(30)=null,
	@Cp varchar(15)=null,
	@Neighborhood varchar(30)=null,
	@State varchar(30)=null,
	@Country varchar(40)=null
AS
BEGIN TRAN
	BEGIN TRY
		IF(@Id = 0 OR @Id is null)
			BEGIN
				if((select active from company where id=@IdCompany)=1)
					BEGIN
						INSERT INTO dbo.Users
						(FirstName,LastName,Celphone,Email,Active,UserTypeId,IdCompany)
						VALUES
						(@FirstName,@LastName,@Celphone,@Email,1,@UserTypeId,@IdCompany)
				
						SET @Id= (SELECT SCOPE_IDENTITY())
						INSERT INTO dbo.UserAddress(Street,Number,Cp,Neighborhood,State,Country,IdUser)
						VALUES
						(@Street,@Number,@Cp,@Neighborhood,@State,@Country,@Id)
					END
				ELSE
					BEGIN
					DECLARE @ERR_MESSAGE NVARCHAR(255);
					SET @ERR_MESSAGE='LA EMPRESA EN DONDE SE ESTA DANDO DE ALTA EL USUARIO ESTA DADA DE BAJA';
						RAISERROR (@ERR_MESSAGE, -- Message text.
						   16, -- Severity,
						   1 -- State,
						   ); -- Second argument.
					END
			END
		ELSE
			BEGIN
			if((SELECT COUNT(*) FROM USERS WHERE ID=@Id)>0)
				BEGIN
					UPDATE dbo.Users SET
					FirstName=@FirstName,
					LastName=@LastName,
					Celphone=@Celphone,
					Email=@Email
					WHERE Id=@Id
				
					UPDATE dbo.UserAddress SET
					Street=@Street,
					Number=@Number,
					Cp=@Cp,
					Neighborhood=@Neighborhood,
					State=@State,
					Country=@Country
					WHERE IdUser=@Id
				
					SET @Id=0
				END
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
      COMMIT TRAN
GO
