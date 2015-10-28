IF OBJECT_ID('[dbo].[V_FUNCTIONS]') IS NOT NULL
	drop view [dbo].V_FUNCTIONS;
go

----------------------------------------------------------------------------------------------------
-- FONCTIONS
----------------------------------------------------------------------------------------------------

create view V_FUNCTIONS as
select
	o.Object_Content_id as id,
	o.[Filename],
	o.FilePosition,
	n.value as name
	from T_OBJECT_CONTENT o
	cross apply(select top(1) PARAMVALUE as value from T_PARAM_CONTENT where PARAMNAME = 'name' and Object_content_id = o.Object_Content_id) n
	where o.OBJECTTYPE = 'function'
;
go
