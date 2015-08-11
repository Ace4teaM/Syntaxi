/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2012                    */
/* Created on:     11/08/2015 18:42:28                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_PARAM_CONTENT') and o.name = 'FK_T_PARAM_CONTENT_REFERENCE_2_T_OBJECT_CONTENT')
alter table T_PARAM_CONTENT
   drop constraint FK_T_PARAM_CONTENT_REFERENCE_2_T_OBJECT_CONTENT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_PROJECT') and o.name = 'FK_T_PROJECT_REFERENCE_1_T_OBJECT_CONTENT')
alter table T_PROJECT
   drop constraint FK_T_PROJECT_REFERENCE_1_T_OBJECT_CONTENT
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
   ID                   varchar(60)          not null,
   OBJECTTYPE           varchar(60)          not null,
   FILENAME             varchar(128)         not null,
   POSITION             integer              not null,
   constraint PK_T_OBJECT_CONTENT primary key (ID)
)
go

/*==============================================================*/
/* Table: T_PARAM_CONTENT                                       */
/*==============================================================*/
create table T_PARAM_CONTENT (
   ID                   varchar(60)          null,
   PARAMNAME            varchar(64)          null,
   PARAMCONTENT         varchar(128)         null
)
go

/*==============================================================*/
/* Table: T_PROJECT                                             */
/*==============================================================*/
create table T_PROJECT (
   NAME                 varchar(64)          not null,
   VERSION              varchar(16)          not null,
   ID                   varchar(60)          null,
   constraint PK_T_PROJECT primary key (NAME, VERSION)
)
go

alter table T_PARAM_CONTENT
   add constraint FK_T_PARAM_CONTENT_REFERENCE_2_T_OBJECT_CONTENT foreign key (ID)
      references T_OBJECT_CONTENT (ID)
         
         not for replication
go

alter table T_PROJECT
   add constraint FK_T_PROJECT_REFERENCE_1_T_OBJECT_CONTENT foreign key (ID)
      references T_OBJECT_CONTENT (ID)
         
         not for replication
go

