/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2012                    */
/* Created on:     28/10/2015 18:32:37                          */
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
   NAME                 VARCHAR(64)          null,
   VERSION              VARCHAR(16)          null,
   OBJECT_CONTENT_ID    VARCHAR(60)          not null,
   OBJECTTYPE           VARCHAR(60)          not null,
   FILENAME             VARCHAR(128)         not null,
   FILEPOSITION         integer              not null,
   constraint PK_T_OBJECT_CONTENT primary key nonclustered (OBJECT_CONTENT_ID)
)
go

/*==============================================================*/
/* Table: T_PARAM_CONTENT                                       */
/*==============================================================*/
create table T_PARAM_CONTENT (
   PARAM_CONTENT_ID     VARCHAR(60)          not null,
   OBJECT_CONTENT_ID    VARCHAR(60)          null,
   PARAMNAME            VARCHAR(60)          not null,
   PARAMVALUE           TEXT                 null,
   constraint PK_T_PARAM_CONTENT primary key nonclustered (PARAM_CONTENT_ID)
)
go

/*==============================================================*/
/* Table: T_PROJECT                                             */
/*==============================================================*/
create table T_PROJECT (
   NAME                 VARCHAR(64)          not null,
   VERSION              VARCHAR(16)          not null,
   constraint PK_T_PROJECT primary key nonclustered (NAME, VERSION)
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
         on update cascade on delete cascade 
         not for replication
go

