IF OBJECT_ID('[dbo].[V_FUNCTIONS]') IS NOT NULL
	drop view [dbo].V_FUNCTIONS;
go

----------------------------------------------------------------------------------------------------
-- FONCTIONS
----------------------------------------------------------------------------------------------------

create view V_FUNCTIONS as
select
	o.ID,
	o.FILENAME,
	o.position,
	n.value as name
	from T_OBJECT_CONTENT o
	cross apply(select top(1) PARAMVALUE as value from T_PARAM_CONTENT where PARAMNAME = 'name' and ID = o.ID) n
	where o.OBJECTTYPE = 'function'
;
go
