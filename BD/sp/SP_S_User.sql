SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_S_User
	-- Add the parameters for the stored procedure here
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
	ON UA.IDUSER=U.ID
	WHERE U.ACTIVE=1
END
GO
