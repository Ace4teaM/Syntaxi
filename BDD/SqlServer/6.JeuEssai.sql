/*
	Jeu d'essai
*/

SET NOCOUNT ON;

delete intervention;
delete r2;
delete poste;
delete employe;
delete societe;
go

-- Reinitialise les sequences d'indentités à 1 (Ids)
DBCC CHECKIDENT('intervention', RESEED, 1) WITH NO_INFOMSGS;
DBCC CHECKIDENT('poste', RESEED, 1) WITH NO_INFOMSGS;
DBCC CHECKIDENT('employe', RESEED, 1) WITH NO_INFOMSGS;
DBCC CHECKIDENT('societe', RESEED, 1) WITH NO_INFOMSGS;
go

insert into societe (RaisonSocial, Siret) values('ASPI','4546-456');
insert into societe (RaisonSocial, Siret) values('SIT','4752-145');

declare @aspi_id integer;
select top(1) @aspi_id=societe_id from societe where RaisonSocial = 'ASPI';

declare @sit_id integer;
select top(1) @sit_id=societe_id from societe where RaisonSocial = 'SIT';

insert into employe (Nom, Prenom, Societe_Id) values('AUGUEY','Thomas',@aspi_id);
insert into employe (Nom, Prenom, Societe_Id) values('GOUTAL','Fabrice',@aspi_id);
insert into employe (Nom, Prenom, Societe_Id) values('CHAUDAIS','Alexandre',@sit_id);
insert into employe (Nom, Prenom, Societe_Id) values('ROULLIER','Jean-François',@sit_id);

insert into employe (Nom, Prenom, Societe_Id) values('Agathe','thepower',@aspi_id);
insert into employe (Nom, Prenom, Societe_Id) values('Toto','lafrite',@aspi_id);
insert into employe (Nom, Prenom, Societe_Id) values('Romain','legras',@aspi_id);

insert into poste (Intitule, Niveau, Coeff, [Desc]) values('Développeur',1,1,'Analyste programmeur en informatique');
insert into poste (Intitule, Niveau, Coeff, [Desc]) values('Automaticien',1,1,'Analyste programmeur en automatisme');
insert into poste (Intitule, Niveau, Coeff, [Desc]) values('Électricien',1,1,'Concepteur dessinateur en électricité');
insert into poste (Intitule, Niveau, Coeff, [Desc]) values('Admin. Réseau',1,1,'Administrateur en réseau informatique');

declare @tau_id integer;
select top(1) @tau_id=employe_id from employe where Nom = 'auguey';

insert into Intervention (Employe_Id,Intitule,[Date]) values(@tau_id,'Remplacement supervision celia',CURRENT_TIMESTAMP);


insert into R2 (Employe_Id, Poste_Id)
	select e.Employe_Id, Poste_Id from Employe e
		inner join Poste p on p.Intitule = 'Développeur'
		where e.Nom in( 'AUGUEY', 'CHAUDAIS', 'ROULLIER' );
		
insert into R2 (Employe_Id, Poste_Id)
	select e.Employe_Id, Poste_Id from Employe e
		inner join Poste p on p.Intitule = 'Admin. Réseau'
		where e.Nom in( 'AUGUEY' );

insert into R2 (Employe_Id, Poste_Id)
	select e.Employe_Id, Poste_Id from Employe e
		inner join Poste p on p.Intitule = 'Électricien'
		where e.Nom in( 'Toto', 'Romain' );

