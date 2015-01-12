declare @Lat decimal(10,6);
declare @Lng decimal(10,6);
declare @unit bigint
set @unit=1
set @Lat=(select lat from oneness where name='147')
set @Lng=(select lng from oneness where name='147');
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
	(tc.idoneness=1) 
	and 
	(convert(char,tc.datecontrol,103)=convert(char,getdate(),103))
	and 
	((tc.arrived=0) or (tc.arrived is null))
	and
	t.idpoint=1
	)
update turncontrol set arrived=1 where id=@idturnset
if(
	(select convert(date,datecontrol) from turncontrol where id=@idturnset)=convert(date,getdate())
	)
	begin 
insert into arrivecontrol(idturn,actualarrival)
values(@idturnset,getdate());
end
end

select *from turncontrol
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


--update arrivecontrol set actualarrival='2015-01-12T05:01:00' where id=2




