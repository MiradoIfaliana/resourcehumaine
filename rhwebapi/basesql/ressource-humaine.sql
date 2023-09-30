create database resourcehumaine;
\c resourcehumaine
    -- genre INT CHECK (genre IN (0, 1))

create table critere(
    idcritere SERIAL PRIMARY KEY,
    nomcritere VARCHAR(100) NOT NULL
);
INSERT INTO critere (nomcritere)
VALUES
    ('diplome'),
    ('experience'),
    ('genre'),
    ('situation matrimoniale'),
    ('nationalite');
--select * from souscritere where idcritere=(select idcritere from critere where nomcritere='genre') and idsouscritere=3;
create table service(
    idservice SERIAL PRIMARY KEY,
    nomservice VARCHAR(100) NOT NULL
);
INSERT INTO service (nomservice)
VALUES
    ('Info-web'),
    ('Info-dev'),
    ('Securite'),
    ('Menage');

create table souscritere(
    idsouscritere SERIAL PRIMARY KEY,
    idcritere int REFERENCES critere(idcritere),
    nomsouscritere VARCHAR(100) NOT NULL,
    grade int NOT NULL
); 
INSERT INTO souscritere (idcritere,nomsouscritere,grade)
VALUES
    (1,'Cepe',1),
    (1,'Bepc',3),
    (1,'Bacc',5),
    (1,'License',7),
    (1,'Master',9),
    (1,'Doctora',11),
    (2,'Experience',1),
    (3,'Femme',1),
    (3,'Homme',1),
    (4,'Marie',1),
    (4,'Celibataire',1),
    (5,'Malagasy',1),
    (5,'Etrange',1);
create table annonce(
    idannonce SERIAL PRIMARY KEY,
    idservice int REFERENCES service(idservice),
    estdispo INT CHECK (estdispo IN (0, 1)),
    dateannonce date,
    heurejournalier float ,
    heurehebdomadaire float
); 

create table besoin(
    idbesoin SERIAL PRIMARY KEY,
    idannonce int REFERENCES annonce(idannonce),
    idsouscritere int REFERENCES souscritere(idsouscritere),
    note float NOT NULL
); 

create table coefcritere(
    idcoefcritere SERIAL PRIMARY KEY,
    idcritere int REFERENCES critere(idcritere),
    idannonce int REFERENCES annonce(idannonce),
    coeficient int NOT NULL CHECK (coeficient = 0 or coeficient > 0)
); 

create table joursemaine(
    idjoursemaine SERIAL PRIMARY KEY,
    nomjoursemaine VARCHAR(100) NOT NULL
); 

--au cas ou lasa misy demi journee dia ampina AM / PM
INSERT INTO joursemaine (nomjoursemaine)
VALUES
    ('lundi'),
    ('mardi'),
    ('mercredi'),
    ('jeudi'),
    ('vendredi'),
    ('samedi'),
    ('dimanche');

create table calendarjob(
    idcalendarjob SERIAL PRIMARY KEY,
    idannonce int REFERENCES annonce(idannonce),
    idjoursemaine int REFERENCES joursemaine(idjoursemaine)
); 
