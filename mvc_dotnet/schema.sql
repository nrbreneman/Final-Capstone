
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
 SecondaryVenue varchar(50) not null,

 constraint pk_teams primary key (id)

);

CREATE TABLE EventDates 
(
 id int identity(1,1),
 TeamID int not null,
 Date DateTime not null,
 Home bit not null,

 constraint pk_dates primary key (id),

);

ALTER TABLE EventDates
ADD CONSTRAINT fk_dates
FOREIGN KEY (TeamID) REFERENCES TEAMS(id);


INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES ('Browns', 'NFL', 'NFL1', 'Cleveland', 'Cleveland Heights');
INSERT INTO EventDates(TeamID, Date, Home) VALUES(1, '2020-02-02', 1);


COMMIT TRANSACTION;