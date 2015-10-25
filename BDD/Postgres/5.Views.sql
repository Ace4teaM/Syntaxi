
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
	exp.paramvalue as "examples",
	grp.paramvalue as "groups"
	from T_OBJECT_CONTENT o
	inner join T_PARAM_CONTENT nam on lower(nam.PARAMNAME) = 'name' and nam.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT dep on lower(dep.PARAMNAME) = 'brief' and dep.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT par on lower(par.PARAMNAME) = 'params' and par.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT ret on lower(ret.PARAMNAME) = 'return' and ret.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT rem on lower(rem.PARAMNAME) = 'remark' and rem.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT exp on lower(exp.PARAMNAME) = 'example' and exp.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT grp on lower(grp.PARAMNAME) = 'group' and grp.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
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
	exp.paramvalue as "examples",
	grp.paramvalue as "groups"
	from T_OBJECT_CONTENT o
	inner join T_PARAM_CONTENT nam on lower(nam.PARAMNAME) = 'name' and nam.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT dep on lower(dep.PARAMNAME) = 'brief' and dep.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT ret on lower(ret.PARAMNAME) = 'return' and ret.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT rem on lower(rem.PARAMNAME) = 'remark' and rem.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT exp on lower(exp.PARAMNAME) = 'example' and exp.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT grp on lower(grp.PARAMNAME) = 'group' and grp.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	where o.OBJECTTYPE = 'class'
;



----------------------------------------------------------------------------------------------------
-- METHODS
----------------------------------------------------------------------------------------------------
drop view if exists v_methods;

create view v_methods as
select
	m."name" as libname,
	m."version" as libver,
	m.object_content_id as method_content_id,
	s.object_content_id as section_content_id,
	m.filename,
	m.fileposition,
	n.paramvalue as method_name,
	c.paramvalue as class_name,
	d.paramvalue as description,
	p.paramvalue as params,
	r.paramvalue as "return",
	re.paramvalue as "remarks",
	e.paramvalue as "examples"

	from t_object_content m
	-- Section précédent l'objet method
	JOIN t_object_content s ON s.object_content_id = (
	    SELECT object_content_id FROM t_object_content WHERE objecttype = 'section' and filename = m.filename and fileposition < m.fileposition order by fileposition desc limit 1
	)
	-- Nom de la classe définit par la section
	full join t_param_content c on c.paramname = 'class' and c.object_content_id = s.object_content_id
	-- Infos
	full join t_param_content n on lower(n.paramname) = 'name' and n.object_content_id = m.object_content_id
	full join t_param_content d on lower(d.paramname) = 'brief' and d.object_content_id = m.object_content_id
	full join t_param_content p on lower(p.paramname) = 'params' and p.object_content_id = m.object_content_id
	full join t_param_content r on lower(r.paramname) = 'return' and r.object_content_id = m.object_content_id
	full join t_param_content re on lower(re.paramname) = 'remarks' and re.object_content_id = m.object_content_id
	full join t_param_content e on lower(e.paramname) = 'example' and e.object_content_id = m.object_content_id
	--
	where m.objecttype = 'method'
;

----------------------------------------------------------------------------------------------------
-- MEMBERS
----------------------------------------------------------------------------------------------------
drop view if exists v_members;

create view v_members as
select
	m."name" as libname,
	m."version" as libver,
	m.object_content_id as member_content_id,
	s.object_content_id as section_content_id,
	m.filename,
	m.fileposition,
	n.paramvalue as member_name,
	c.paramvalue as class_name,
	d.paramvalue as description,
	p.paramvalue as params,
	r.paramvalue as "return",
	re.paramvalue as "remarks",
	e.paramvalue as "examples"

	from t_object_content m
	-- Section précédent l'objet method
	JOIN t_object_content s ON s.object_content_id = (
	    SELECT object_content_id FROM t_object_content WHERE objecttype = 'section' and filename = m.filename and fileposition < m.fileposition order by fileposition desc limit 1
	)
	-- Nom de la classe définit par la section
	JOIN t_param_content c  ON c.param_content_id = (
	    SELECT param_content_id FROM t_param_content WHERE object_content_id = s.object_content_id and paramname = 'class' limit 1
	)
	-- Infos
	full join t_param_content n on lower(n.paramname) = 'name' and n.object_content_id = m.object_content_id
	full join t_param_content d on lower(d.paramname) = 'brief' and d.object_content_id = m.object_content_id
	full join t_param_content p on lower(p.paramname) = 'params' and p.object_content_id = m.object_content_id
	full join t_param_content r on lower(r.paramname) = 'return' and r.object_content_id = m.object_content_id
	full join t_param_content re on lower(re.paramname) = 'remarks' and re.object_content_id = m.object_content_id
	full join t_param_content e on lower(e.paramname) = 'example' and e.object_content_id = m.object_content_id
	-- 
	where m.objecttype = 'member'
;

----------------------------------------------------------------------------------------------------
-- CONTROLER
----------------------------------------------------------------------------------------------------
drop view if exists v_controlers;

create view v_controlers as
select
	o."name" as libname,
	o."version" as libver,
	o.OBJECT_CONTENT_ID,
	o.FILENAME,
	o.fileposition,
	nam.paramvalue as "name",
	dep.paramvalue as "description",
	rol.paramvalue as "role",
	uc.paramvalue as "uc",
	mod.paramvalue as "module",
	rem.paramvalue as "remarque",
	grp.paramvalue as "groups"
	from T_OBJECT_CONTENT o
	inner join T_PARAM_CONTENT nam on lower(nam.PARAMNAME) = 'name' and nam.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT dep on lower(dep.PARAMNAME) = 'description' and dep.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT rol on lower(rol.PARAMNAME) = 'role' and rol.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT uc on lower(uc.PARAMNAME) = 'uc' and uc.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT mod on lower(mod.PARAMNAME) = 'module' and mod.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT rem on lower(rem.PARAMNAME) = 'remarque' and rem.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	full join T_PARAM_CONTENT grp on lower(grp.PARAMNAME) = 'group' and grp.OBJECT_CONTENT_ID = o.OBJECT_CONTENT_ID
	where o.OBJECTTYPE = 'controler'
;

