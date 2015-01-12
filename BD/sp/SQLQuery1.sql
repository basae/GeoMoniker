--declare @Lat decimal(10,6)
--declare @Lng decimal(10,6)
--set @Lat=18.139189
--set @Lng=-94.522310
--SELECT P.id,P.description 
--FROM point P
--WHERE
--	( 
--	LatAreaMax >= @Lat
--	AND
--	LatAreaMin <=@Lat
--	)
--AND
--	(
--	LngAreaMax>=@Lng
--	AND
--	LngAreaMin<=@Lng
--	)


--select *from point order by orderroute

select *from turncontrol
select *from turns
select *from oneness

insert into turncontrol(datecontrol,idturn,idoneness,arrived)



