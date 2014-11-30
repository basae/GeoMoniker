SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_D_Company
	-- Add the parameters for the stored procedure here
	@Id bigint
AS
BEGIN
	UPDATE dbo.Company SET
	Active=0
	WHERE Id=@Id
END
GO
