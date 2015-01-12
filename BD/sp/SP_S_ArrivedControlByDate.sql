SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE SP_S_ArrivedControlByDate
	-- Add the parameters for the stored procedure here
	@DateControl datetime
	
AS
BEGIN
BEGIN TRY
select
	ac.id, 
	onn.name as Unit,
	p.description as Terminal,
	convert(time,ts.awaitedarrival) as awaitedarrival,
	convert(time,ac.actualarrival) as actualarrival,
	datediff(minute,convert(time,ts.awaitedarrival),convert(time,ac.actualarrival)) as difminutes
from arrivecontrol ac 
inner join 	turncontrol tc 
	on tc.id=ac.idturn 
inner join turns ts
	on ts.id=tc.idturn 
inner join point p 
	on p.id=ts.idpoint
inner join oneness onn
	on onn.id=tc.idoneness
where convert(date,tc.datecontrol)=convert(date,@DateControl)
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
