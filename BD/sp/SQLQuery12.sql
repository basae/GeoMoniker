select pt_llamadasdeposito,(select fullname from systemuser where systemuserid=pt_modificadopor),statuscode,pt_resultadollamadaid,pt_motivodecancelacionid,*from pt_agenteentramite where pt_numagentetramite='24337'

--update pt_agenteentramite set
--statuscode=9,
--statecode=0
--where pt_numagentetramite='24337'

--update pt_agenteentramite set
--pt_modificadopor=null,
--pt_resultadollamadaid=null,
--pt_motivodecancelacionid=null,
--pt_llamadasdeposito=0
--where pt_numagentetramite='24337'
