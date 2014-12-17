SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_IU_Company
	-- Add the parameters for the stored procedure here
	@Id bigint=NULL output,
	@Name varchar(MAX)
AS
BEGIN
	IF(@Id = 0 OR @Id IS NULL)
		BEGIN
			INSERT INTO dbo.Company
			(Name,InsertDate,Active)
			VALUES
			(@Name,GETDATE(),1)

			SET @Id=(SELECT SCOPE_IDENTITY());
		END
	ELSE
		BEGIN
			UPDATE dbo.Company SET
			Name=@Name,
			UpdateDate=GETDATE()
			WHERE Id=@Id
			SET @Id=0;
		END	
END
GO
