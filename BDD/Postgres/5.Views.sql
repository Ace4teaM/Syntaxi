
----------------------------------------------------------------------------------------------------
-- FONCTIONS
----------------------------------------------------------------------------------------------------
drop view if exists v_functions;

create view v_functions as
select
	o."name" as libname,
	o."version" as libver,
	o.OBJECT_CONTENT_ID,
	o.FILENAME,
	o.fileposition,
	nam.paramvalue as "name",
	dep.paramvalue as "description",
	par.paramvalue as "params",
	ret.paramvalue as "return",
	rem.paramvalue as "remarks",
	exp.paramvalue as "examples"
	from T_OBJECT_CONTENT o
	inner join T_PARAM_CONTENT nam on nam.PARAMNAME = 'name' and nam.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT dep on dep.PARAMNAME = 'description' and dep.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT par on par.PARAMNAME = 'params' and par.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT ret on ret.PARAMNAME = 'return' and ret.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT rem on rem.PARAMNAME = 'remark' and rem.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT exp on exp.PARAMNAME = 'example' and exp.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	where o.OBJECTTYPE = 'function'
;

----------------------------------------------------------------------------------------------------
-- CLASSES
----------------------------------------------------------------------------------------------------
drop view if exists v_classes;

create view v_classes as
select
	o."name" as libname,
	o."version" as libver,
	o.OBJECT_CONTENT_ID,
	o.FILENAME,
	o.fileposition,
	nam.paramvalue as "name",
	dep.paramvalue as "description",
	ret.paramvalue as "return",
	rem.paramvalue as "remarks",
	exp.paramvalue as "examples"
	from T_OBJECT_CONTENT o
	inner join T_PARAM_CONTENT nam on nam.PARAMNAME = 'name' and nam.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT dep on dep.PARAMNAME = 'description' and dep.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT ret on ret.PARAMNAME = 'return' and ret.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT rem on rem.PARAMNAME = 'remark' and rem.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT exp on exp.PARAMNAME = 'example' and exp.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	where o.OBJECTTYPE = 'class'
;
