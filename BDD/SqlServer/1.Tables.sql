/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2012                    */
/* Created on:     13/08/2015 18:25:46                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_OBJECT_CONTENT') and o.name = 'FK_T_OBJECT_CONTENT_REFERENCE_2_T_PROJECT')
alter table T_OBJECT_CONTENT
   drop constraint FK_T_OBJECT_CONTENT_REFERENCE_2_T_PROJECT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_PARAM_CONTENT') and o.name = 'FK_T_PARAM_CONTENT_REFERENCE_3_T_OBJECT_CONTENT')
alter table T_PARAM_CONTENT
   drop constraint FK_T_PARAM_CONTENT_REFERENCE_3_T_OBJECT_CONTENT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_OBJECT_CONTENT')
            and   type = 'U')
   drop table T_OBJECT_CONTENT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_PARAM_CONTENT')
            and   type = 'U')
   drop table T_PARAM_CONTENT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_PROJECT')
            and   type = 'U')
   drop table T_PROJECT
go

/*==============================================================*/
/* Table: T_OBJECT_CONTENT                                      */
/*==============================================================*/
create table T_OBJECT_CONTENT (
   NAME                 varchar(64)          null,
   VERSION              varchar(16)          null,
   OBJECT_CONTENT_ID    varchar(60)          not null,
   OBJECTTYPE           varchar(60)          not null,
   FILENAME             varchar(128)         not null,
   POSITION             int                  not null,
   constraint PK_T_OBJECT_CONTENT primary key (OBJECT_CONTENT_ID)
)
go

/*==============================================================*/
/* Table: T_PARAM_CONTENT                                       */
/*==============================================================*/
create table T_PARAM_CONTENT (
   PARAM_CONTENT_ID     varchar(60)          not null,
   OBJECT_CONTENT_ID    varchar(60)          null,
   PARAMNAME            varchar(60)          not null,
   PARAMVALUE           varchar(128)         null,
   constraint PK_T_PARAM_CONTENT primary key (PARAM_CONTENT_ID)
)
go

/*==============================================================*/
/* Table: T_PROJECT                                             */
/*==============================================================*/
create table T_PROJECT (
   NAME                 varchar(64)          not null,
   VERSION              varchar(16)          not null,
   constraint PK_T_PROJECT primary key (NAME, VERSION)
)
go

alter table T_OBJECT_CONTENT
   add constraint FK_T_OBJECT_CONTENT_REFERENCE_2_T_PROJECT foreign key (NAME, VERSION)
      references T_PROJECT (NAME, VERSION)
         on update cascade on delete cascade 
         not for replication
go

alter table T_PARAM_CONTENT
   add constraint FK_T_PARAM_CONTENT_REFERENCE_3_T_OBJECT_CONTENT foreign key (OBJECT_CONTENT_ID)
      references T_OBJECT_CONTENT (OBJECT_CONTENT_ID)
         
         not for replication
go

