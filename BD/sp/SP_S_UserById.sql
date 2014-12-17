SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_S_UserById
	-- Add the parameters for the stored procedure here
	@Id bigint
AS
BEGIN
	SELECT 
	U.ID,
	FIRSTNAME,
	LASTNAME,
	CELPHONE,
	EMAIL,
	USERTYPEID,
	IDCOMPANY,
	STREET,
	NUMBER,
	CP,
	NEIGHBORHOOD,
	STATE,
	COUNTRY	
	FROM dbo.Users U INNER JOIN dbo.UserAddress UA
	ON U.ID=UA.IDUSER
	WHERE U.Id=@Id AND ACTIVE=1
END
GO
