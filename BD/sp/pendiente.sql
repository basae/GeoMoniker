declare @Lat decimal(10,6);
declare @Lng decimal(10,6);
declare @unit bigint
set @unit=1
set @Lat=18.143377;
set @Lng=-94.415320;

declare @pointset bigint
set @pointset=(select id from point where
(latareamax>=@Lat and latareamin<=@Lat)
and
(lngareamax>=@Lng and lngareamin<=@Lng))
declare @idturnset bigint

set @idturnset=(select idturn from turncontrol where idoneness=@unit and convert(char,datecontrol,103)=convert(char,getdate(),103))
select *from turns where idpoint=@pointset and id=@idturnset

--insert into arrivecontrol(idturn,actualarrival)
--values(@idturnset,getdate());

select *from point

select tc.datecontrol,ts.awaitedarrival,ac.actualarrival,p.description from arrivecontrol ac inner join
turncontrol tc on tc.id=ac.idturn inner join turns ts
on ts.id=tc.idturn inner join point p on p.id=ts.idpoint





