/**
	Fonctions d'interface avec l'éditeur de projet
	
	RemoveProject     : Supprime le projet
	AddObjectContent  : Ajoute un objet
	AddParamContent   : Ajoute un paramètre objet
*/

-- Supprime le projet
IF OBJECT_ID('[dbo].[ResetProject]') IS NOT NULL
	drop procedure [dbo].[ResetProject];
go
CREATE PROCEDURE [dbo].[ResetProject]
(
	@Name varchar(128),
	@Version varchar(128)
)
AS
BEGIN
	delete T_PROJECT where NAME = @Name and Version = @Version;
	insert into T_PROJECT (NAME,Version) values(@Name,@Version);
END 
go

-- Ajoute un objet
IF OBJECT_ID('[dbo].[AddObjectContent]') IS NOT NULL
	drop procedure [dbo].[AddObjectContent];
go
CREATE PROCEDURE [dbo].[AddObjectContent]
(
	@GUID varchar(128),
	@Name varchar(128),
	@Version varchar(128)
)
AS
BEGIN
	insert into T_OBJECT_CONTENT (NAME,Version) values(@Name,@Version);
END 
go

-- Ajoute un paramètre objet
IF OBJECT_ID('[dbo].[AddParamContent]') IS NOT NULL
	drop procedure [dbo].[AddParamContent];
go
CREATE PROCEDURE [dbo].[AddParamContent]
(
	@Name varchar(128),
	@Version varchar(128)
)
AS
BEGIN
	insert into T_OBJECT_CONTENT (NAME,Version) values(@Name,@Version);
END 
go
