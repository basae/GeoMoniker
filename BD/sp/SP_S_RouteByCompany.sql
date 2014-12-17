SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_S_RouteByCompany
	-- Add the parameters for the stored procedure here
	@IdCompany BIGINT
AS
BEGIN
	SELECT 
	R.ID,
	R.NAME,
	(SELECT (FIRSTNAME+' '+LASTNAME) FROM USERS WHERE ID=R.USERINSERT) AS INSERTUSER,
	R.INSERTDATE,
	(SELECT (FIRSTNAME+' '+LASTNAME) FROM USERS WHERE ID=R.USERUPDATE) AS UPDATEUSER,
	R.UPDATEDATE,
	C.NAME AS COMPANY
	FROM 
	ROUTE R INNER JOIN COMPANY C
		ON R.IDCOMPANY=C.ID
	WHERE IdCompany=@IdCompany
END
GO
