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
	@Id bigint,
	@Name varchar(70)
AS
BEGIN
	IF(@Id = 0 OR @Id=NULL)
		BEGIN
			INSERT INTO dbo.Company
			(Name,InsertDate,Active)
			VALUES
			(@Name,GETDATE(),1)
		END
	ELSE
		BEGIN
			UPDATE dbo.Company SET
			Name=@Name,
			UpdateDate=GETDATE()
			WHERE Id=@Id
		END	
	SELECT SCOPE_IDENTITY();
END
GO
