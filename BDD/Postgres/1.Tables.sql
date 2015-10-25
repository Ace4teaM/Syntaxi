/*==============================================================*/
/* DBMS name:      PostgreSQL 9.x                               */
/* Created on:     07/09/2015 20:58:17                          */
/*==============================================================*/


drop cascade table if exists T_OBJECT_CONTENT;

drop  cascade table if exists T_PARAM_CONTENT;

drop  cascade table if exists T_PROJECT;

/*==============================================================*/
/* Table: T_OBJECT_CONTENT                                      */
/*==============================================================*/
create table T_OBJECT_CONTENT (
   NAME                 VARCHAR(64)          null,
   VERSION              VARCHAR(16)          null,
   OBJECT_CONTENT_ID    VARCHAR(60)          not null,
   OBJECTTYPE           VARCHAR(60)          not null,
   FILENAME             VARCHAR(128)         not null,
   FILEPOSITION         INT4                 not null,
   constraint PK_T_OBJECT_CONTENT primary key (OBJECT_CONTENT_ID)
);

/*==============================================================*/
/* Table: T_PARAM_CONTENT                                       */
/*==============================================================*/
create table T_PARAM_CONTENT (
   PARAM_CONTENT_ID     VARCHAR(60)          not null,
   OBJECT_CONTENT_ID    VARCHAR(60)          null,
   PARAMNAME            VARCHAR(60)          not null,
   PARAMVALUE           TEXT                 null,
   constraint PK_T_PARAM_CONTENT primary key (PARAM_CONTENT_ID)
);

/*==============================================================*/
/* Table: T_PROJECT                                             */
/*==============================================================*/
create table T_PROJECT (
   NAME                 VARCHAR(64)          not null,
   VERSION              VARCHAR(16)          not null,
   constraint PK_T_PROJECT primary key (NAME, VERSION)
);

alter table T_OBJECT_CONTENT
   add constraint FK_T_OBJECT_REFERENCE_T_PROJEC foreign key (NAME, VERSION)
      references T_PROJECT (NAME, VERSION)
      on delete cascade on update cascade;

alter table T_PARAM_CONTENT
   add constraint FK_T_PARAM__REFERENCE_T_OBJECT foreign key (OBJECT_CONTENT_ID)
      references T_OBJECT_CONTENT (OBJECT_CONTENT_ID)
      on delete cascade on update cascade;

