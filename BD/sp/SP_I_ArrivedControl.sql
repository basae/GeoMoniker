SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE SP_I_ArrivedControl
	-- Add the parameters for the stored procedure here
	@Lat decimal(10,6),
	@Lng decimal(10,6),
	@Imei bigint	
AS
BEGIN
BEGIN TRY
	declare @unit bigint;
	set @unit=(select id from oneness where Imei=@Imei);
	if(@unit is not null)
	begin
		declare @pointset bigint
		set @pointset=(select id from point where
		(latareamax>=@Lat and latareamin<=@Lat)
		and
		(lngareamax>=@Lng and lngareamin<=@Lng))
		if(@pointset is not null)
		begin
			declare @idturnset bigint
			set @idturnset=(select tc.id from turncontrol tc inner join turns t 
			on t.id=tc.idturn
			where 
				(tc.idoneness=@Unit) 
				and 
				(convert(char,tc.datecontrol,103)=convert(char,getdate(),103))
				and 
				((tc.arrived=0) or (tc.arrived is null))
				and
				t.idpoint=@pointset
				)
			update turncontrol set arrived=1 where id=@idturnset
			if(
				(select convert(char,datecontrol,103) from turncontrol where id=@idturnset)=convert(char,getdate(),103)
				)
				begin 
				insert into arrivecontrol(idturn,actualarrival)
				values(@idturnset,getdate());
			end
		end
	end
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
