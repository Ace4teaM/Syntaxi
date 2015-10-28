--------------------------------------------------------
--
-- Création des bases de données
-- Notes: Le codage des caractères est insensible à la casse (CI)
--
--------------------------------------------------------

USE Master;
GO

-- Instance de test
if db_id('syntaxi') is not null
begin
	-- deconnecte les utilisateurs
	ALTER DATABASE [syntaxi] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	drop database [syntaxi];
end
create database [syntaxi] COLLATE SQL_Latin1_General_CP1_CI_AS;

-- Utilisateur USER
IF NOT EXISTS(SELECT principal_id FROM sys.server_principals WHERE name = 'user') BEGIN
	CREATE LOGIN [user] WITH PASSWORD=N'', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;
END
GO
