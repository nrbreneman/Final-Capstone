
-- Switch to the system (aka master) database
USE master;
GO

-- Delete the DemoDB Database (IF EXISTS)
IF EXISTS(select * from sys.databases where name='DemoDB')
DROP DATABASE DemoDB;
GO

-- Create a new DemoDB Database
CREATE DATABASE DemoDB;
GO

-- Switch to the DemoDB Database
USE DemoDB
GO

BEGIN TRANSACTION;

CREATE TABLE users
(
	id			int			identity(1,1),
	username	varchar(50)	not null,
	password	varchar(50)	not null,
	salt		varchar(50)	not null,
	role		varchar(50)	default('user'),

	constraint pk_users primary key (id)
);

CREATE TABLE TEAMS 
(
 id int identity(1,1),
 Name varchar(50) not null,
 League varchar(50) not null,
 Org varchar(50) not null,
 PrimaryVenue varchar(50) not null,
 SecondaryVenue varchar(50),

 constraint pk_teams primary key (id)

);

CREATE TABLE EventDates 
(
 id int identity(1,1),
 TeamName varchar(50),
 TeamID int not null,
 Date DateTime,
 Home bit not null,

 constraint pk_dates primary key (id),

);

ALTER TABLE EventDates
ADD CONSTRAINT fk_dates
FOREIGN KEY (TeamID) REFERENCES TEAMS(id);




INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Chicago Lions', 'Womens Midwest D1', 'USAR', 'Solider Field', 'Mile Drive Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Minnesota Valkyries', 'Womens Midwest D1', 'USAR', 'Chase Field', 'Riverview Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Chicago Rugby', 'Womens Midwest D1', 'USAR', 'Solider Field', 'Lake Shore Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Detroit Rugby', 'Womens Midwest D1', 'USAR', 'Detroit Field', 'Ford Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Wisconsin Rugby', 'Womens Midwest D1', 'USAR', 'Lambeau Field', 'Cheese Head Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Buffalo Rugby', 'Womens Midwest D2', 'USAR', 'Thunder Field', 'Lake Front Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Pittsburgh Forge', 'Womens Midwest D2', 'USAR', 'Heinz Field', 'Three Rivers Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('North Buffalo Ninjas', 'Womens Midwest D2', 'USAR', 'Thunder Field', 'Ninja Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('South Buffalo Rugby', 'Womens Midwest D2', 'USAR', 'Thunder Field', 'Buffalo Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Cleveland Iron Maidens', 'Womens Midwest D2', 'USAR', 'Cleveland Field', 'Lake Front Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Akron Rugby', 'Womens Midwest D2', 'USAR', 'Homerun Field', 'Winter Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Cincinnati Kelts', 'Womens Midwest D2', 'USAR', 'Hunter Field', 'Knoks Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Columbus Rugby', 'Womens Midwest D2', 'USAR', 'OSU Field', 'Beekman Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Fort Wayne Rugby', 'Womens Midwest D2', 'USAR', 'Fort Wayne Field', 'Riverbend Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Grand Rapids Rugby', 'Womens Midwest D2', 'USAR', 'Hammer Field', 'Grand Rapids Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Dayton Flying Pigs', 'Womens Midwest D2', 'USAR', 'Wright Field', 'First Flight Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Louisville Riversharks', 'Womens Midwest D2', 'USAR', 'Bourbon Field', 'Lake Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Chicgao Lions', 'Mens Midwest D1', 'USAR', 'Solider Field', 'Mile Drive Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Detroit Tradesmen', 'Mens Midwest D1', 'USAR', 'Detroit Field', 'Ford Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Cincinnati Wolfhounds', 'Mens Midwest D1', 'USAR', 'Great American Field', 'Richard Ford Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Columbus Rugby', 'Mens Midwest D1', 'USAR', 'OSU Field', 'Beekman Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Chicgao Griffins', 'Mens Midwest D1', 'USAR', 'Solider Field', 'Mile Drive Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Glendale Merlins', 'Womens Frontier D1', 'USAR', 'Glendale Field', 'Mile Drive Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Austin Valkyries', 'Womens Frontier D1', 'USAR', 'Cowboy Field', 'Steer Drive Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Houston Athletic', 'Womens Frontier D1', 'USAR', 'Crew Field', 'Hammock Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Utah Vipers', 'Womens Frontier D1', 'USAR', 'Salt Lake Field', 'Desert Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Black Ice Rugby', 'Womens Frontier D1', 'USAR', 'Fire Field', 'Ice Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Dallas Harlequins', 'Womens Frontier D1', 'USAR', 'Jerryland Field', 'Cody Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('St. Louis Royal Ramblers', 'Mens Frontier D2', 'USAR', 'Blues Field', 'Cardnial Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Wichita Barbarians', 'Mens Frontier D2', 'USAR', 'Ghangis Khan Field', 'Huns Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Kansas City Rugby', 'Mens Frontier D2', 'USAR', 'BBQ Field', 'Ribs Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Belmont Shore Rugby', 'Mens Pacific D1', 'USAR', 'Whaler Field', 'Beach Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Life West Gladiators', 'Mens Pacific D1', 'USAR', 'The Field', 'Funtime Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Old Mission Beach Athletic', 'Mens Pacific D1', 'USAR', 'Happy Field', 'Rock Park');
INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Glendale Merlins', 'Mens Pacific D1', 'USAR', 'Whaler Field', 'Beach Park');


INSERT INTO EventDates(TeamID, Date, Home) VALUES(1, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(2, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(3, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(4, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(5, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(6, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(7, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(8, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(9, '2020-09-12', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(10, '2020-09-12', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(11, '2020-09-12', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(12, '2020-09-12', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(13, '2020-09-12', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(14, '2020-09-12', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(15, '2020-09-12', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(16, '2020-09-12', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(17, '2020-09-12', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(18, '2020-09-12', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(19, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(20, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(21, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(22, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(23, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(24, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(25, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(26, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(27, '2020-09-12', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(28, '2020-09-12', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(29, '2020-09-12', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(30, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(31, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(32, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(33, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(34, '2020-09-05', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(35, '2020-09-05', 1);

INSERT INTO EventDates(TeamID, Date, Home) VALUES(1, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(2, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(3, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(4, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(5, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(6, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(7, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(8, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(9, '2020-09-05', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(10, '2020-09-05', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(11, '2020-09-05', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(12, '2020-09-05', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(13, '2020-09-05', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(14, '2020-09-05', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(15, '2020-09-05', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(16, '2020-09-05', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(17, '2020-09-05', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(18, '2020-09-05', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(19, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(20, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(21, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(22, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(23, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(24, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(25, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(26, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(27, '2020-09-05', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(28, '2020-09-05', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(29, '2020-09-05', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(30, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(31, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(32, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(33, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(34, '2020-09-12', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(35, '2020-09-12', 0);

INSERT INTO EventDates(TeamID, Date, Home) VALUES(1, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(2, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(3, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(4, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(5, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(6, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(7, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(8, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(9, '2020-09-19', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(10, '2020-09-19', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(11, '2020-09-19', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(12, '2020-09-19', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(13, '2020-09-19', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(14, '2020-09-19', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(15, '2020-09-19', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(16, '2020-09-19', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(17, '2020-09-19', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(18, '2020-09-19', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(19, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(20, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(21, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(22, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(23, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(24, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(25, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(26, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(27, '2020-09-19', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(28, '2020-09-19', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(29, '2020-09-19', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(30, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(31, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(32, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(33, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(34, '2020-09-26', 1);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(35, '2020-09-26', 1);

INSERT INTO EventDates(TeamID, Date, Home) VALUES(1, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(2, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(3, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(4, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(5, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(6, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(7, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(8, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(9, '2020-09-26', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(10, '2020-09-26', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(11, '2020-09-26', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(12, '2020-09-26', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(13, '2020-09-26', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(14, '2020-09-26', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(15, '2020-09-26', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(16, '2020-09-26', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(17, '2020-09-26', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(18, '2020-09-26', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(19, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(20, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(21, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(22, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(23, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(24, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(25, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(26, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(27, '2020-09-26', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(28, '2020-09-26', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(29, '2020-09-26', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(30, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(31, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(32, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(33, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(34, '2020-09-19', 0);
INSERT INTO EventDates(TeamID, Date, Home) VALUES(35, '2020-09-19', 0);


COMMIT TRANSACTION;