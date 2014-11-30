SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_S_CompanyByID
	-- Add the parameters for the stored procedure here
	@Id bigint
AS
BEGIN
	SELECT * FROM dbo.Company
	WHERE Id=@Id AND ACTIVE=1
END