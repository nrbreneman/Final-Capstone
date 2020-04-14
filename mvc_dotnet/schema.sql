
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

CREATE TABLE Users
(
	id			int			identity(1,1),
	username	varchar(50)	not null,
	password	varchar(50)	not null,
	salt		varchar(50)	not null,
	role		varchar(50)	default('user'),
	teamID		int,

	constraint pk_Users primary key (id)
);

CREATE TABLE Teams 
(
 id int identity(1,1),
 Name varchar(50) not null,
 League varchar(50) not null,
 Org varchar(50) not null,
 PrimaryVenue varchar(50) not null,
 SecondaryVenue varchar(50),
 UserID int not null,
 constraint pk_teams primary key (id)

);

CREATE TABLE EventDates 
(
 id int identity(1,1),
 TeamName varchar(50),
 TeamID int not null,
 Date Date,
 Home bit not null,

 constraint pk_dates primary key (id),

);

CREATE TABLE Leagues
(
id int identity(1,1),
leagueName varchar(25) not null,
org varchar(25) not null,
sport varchar(25) not null,

constraint pk_leagueID primary key (id),
);

CREATE TABLE Messages 
(
id int identity(1,1),
SentByUserID int not null,
toUserID int not null,
messageBody varchar(300),
userAccepted varchar(20) default('no'),
adminAccepted varchar(20) default('no'),

constraint pk_messageID primary key (id)
);

CREATE TABLE Schedule
(
matchID int identity(1,1),
homeTeam varchar(50) not null,
awayTeam varchar(50) not null,
date Date not null,
venue varchar(50) not null,

constraint pk_gameID primary key (matchID)
);

CREATE TABLE Roster
(
rosterID int identity(1,1),
firstName varchar(50) not null,
lastName varchar(50) not null,
email varchar(50),
phone varchar(10),
teamID int not null,

constraint pk_rosterID primary key (rosterID)
);

ALTER TABLE EventDates
ADD CONSTRAINT fk_dates
FOREIGN KEY (TeamID) REFERENCES Teams(id);

ALTER TABLE Teams
ADD CONSTRAINT fk_Users
FOREIGN KEY (UserID) REFERENCES Users(id);

ALTER TABLE Messages
ADD CONSTRAINT fk_userTo
FOREIGN KEY (toUserID) REFERENCES Users(id);


INSERT INTO Roster(firstName, lastName, email, phone, teamID) VALUES('Nathan', 'Breneman', 'nrbreneman@gmail.com', '6149052372', 21);
INSERT INTO Users(username, password ,salt ,role)  VALUES ('admin@gmail.com','Zn3FdCdRkqZbUxfSoc1o7aCEkVk=','/NWc+euho+Y=', 'Admin');
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('1@1.com',	'dUQ7LIuKzjJiNYsOSvzS0B5nQQ4=',	'Yk65cf+kbPg=', 1);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('2@1.com',	'15fxnVEz27mM6c42ZOVXXx1lnPc=', 'TfAwiQDlbKY=', 2);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('3@1.com',	'C6GYJziREkRYrwxrfvvIHYc9PLI=', 'sridDRrV8Ss=', 3);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('4@1.com',	'F9IHHMDArLbxbinbRnHRTNqU5yk=', 'e2lPXI3nUhQ=', 4);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('5@1.com',	'o0L+Hw9HXO3eqqI1gJ0asHL3RO0=', 'h2/9FNv4Byo=', 4);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('6@1.com',	'o4T1bTDsvd1C8BkwaEpzH/aLpfE=', 'amPpDDGF4EI=', 6);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('7@1.com',	'C1dsfIGauuR8z3K+5b1rWSX27zU=', 'aPp9rSiz+HU=', 7);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('8@1.com',	'rMmrL/gzr5/LMn9a0ISW+Mj+/yM=', 'FBfbiRk4FII=', 8);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('9@1.com',	'PwajjMu08rBiH5HaNI1loZPY7NM=', 'gGpx026EVe8=', 9);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('10@1.com', 'O0EEoqpg78zAmRhVD8NGqh8hDCo=', 'VwolU1KIuvM=', 10);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('11@1.com', 'vFx2hWoYMVWfYGGX/QxVxip+ihE=', 'oH+LexZCgWE=', 11);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('12@1.com', 'uu/o47aNYQam6d3y+K6/sWWo0Zo=', 'HeUtVRp2H6g=', 12);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('13@1.com', 'gk9gFhbGhAvV4XiH0RpCXQpFhb4=', 'YR4ZQ/Od9oo=', 13);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('14@1.com', 'y3fTSE3Shn9iK+kBDeyJ1RSXyTk=', '3zx+d+DGuEc=', 14);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('15@1.com', 'Ssf1O+W7Y5JJSsstflzTSxdCicI=', '9jV2cTKA18Y=', 15);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('16@1.com', 'W4vcVdkaFe2biRYaC4XMoEgTiAQ=', 'FNi7NNCORHQ=', 16);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('17@1.com', 'EO21OWo7jYCK1Jo4R+7ziBPuvnA=', 'MGEn3rQCT50=', 17);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('18@1.com', 'MQY+8k4th1ygdiO/obOjrcs5d38=', 'tTh+57qxXe0=', 18);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('19@1.com', '/1JR7Ow5K2O9Dp0/1ZF7ny7wxzc=', 'rQ0goxnUusU=', 19);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('20@1.com', 'dB5meWHrXcTyyjZzYmDzNFW/CxI=', '0ckkAxyeczc=', 20);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('21@1.com', 'l8plJ5tnyNdcVI9rP9IwNUaxWJA=', 'UG4cQFZPB6A=', 21);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('22@1.com', 'vEKzh3nxnsa3otLbtCAY/0d4yM8=', 'x8Y7nBRXlhE=', 22);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('23@1.com', '+JeqSW2RFKCfp8wCpdqC9309uq8=', 'lgNdWV0zYZI=', 23);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('24@1.com', 'XEGT4sY0BdZXP6u6zw087ZlJR80=', 'WhJaN9XsbX8=', 24);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('25@1.com', '2kD8FohutJG5lmAbpWQdA7OOZvA=', '/se5j7fZ1eU=', 25);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('26@1.com', 'L5LTg9m0jb+DAxopGCGPL8mXGk4=', '/RW2twF2+iI=', 26);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('27@1.com', 'SXciwoGNG7pfaQ7lOvrXwWbFt5c=', 'FjHgH5JfW64=', 27);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('28@1.com', '+ddJXoHutcyGFgwbr5PMPWrwTfM=', 'topMUnjraRY=', 28);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('29@1.com', 'y2i2uNLDcc7ReS3EuCm2Tat248E=', 'Fb6/EbzeLYA=', 29);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('30@1.com', '1xQx0/Rkm36mb8nBWfQ0uN2ryZM=', 'uW1gxfgjYME=', 30);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('31@1.com', '7d29q+4rPlaXEPQ0w8+WYZd2+AA=', 'hRWYx8qdR9A=', 31);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('32@1.com', 'oT5zN69KSYF0wWBJUE3x/dj/Foc=', 'HJpkNEBtmSg=', 32);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('33@1.com', 'NSepkL0+GAeoU8mL6T9R+X7NqWQ=', 'r8n3gDgO3kw=', 33);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('34@1.com', 'UKvl6WSy+aQFAc8lG9hC2JemZoo=', 'AJEa1n1cAzg=', 34);
INSERT INTO Users(username, password ,salt, teamID)  VALUES ('35@1.com', 'eFeOdylt1LoYsOvdwwSR8+YWkRQ=', 'brFG27EVUI4=', 35);


INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Chicago Lions', 'Womens Midwest D1', 'USAR', 'Solider Field', 'Mile Drive Park', 2);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Minnesota Valkyries', 'Womens Midwest D1', 'USAR', 'Chase Field', 'Riverview Park', 3);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Chicago Rugby', 'Womens Midwest D1', 'USAR', 'Solider Field', 'Lake Shore Park', 4);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Detroit Rugby', 'Womens Midwest D1', 'USAR', 'Detroit Field', 'Ford Park', 5);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Wisconsin Rugby', 'Womens Midwest D1', 'USAR', 'Lambeau Field', 'Cheese Head Park', 6);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Buffalo Rugby', 'Womens Midwest D2', 'USAR', 'Thunder Field', 'Lake Front Park', 7);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Pittsburgh Forge', 'Womens Midwest D2', 'USAR', 'Heinz Field', 'Three Rivers Park', 8);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('North Buffalo Ninjas', 'Womens Midwest D2', 'USAR', 'Thunder Field', 'Ninja Park', 9);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('South Buffalo Rugby', 'Womens Midwest D2', 'USAR', 'Thunder Field', 'Buffalo Park', 10);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Cleveland Iron Maidens', 'Womens Midwest D2', 'USAR', 'Cleveland Field', 'Lake Front Park', 11);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Akron Rugby', 'Womens Midwest D2', 'USAR', 'Homerun Field', 'Winter Park', 12);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Cincinnati Kelts', 'Womens Midwest D2', 'USAR', 'Hunter Field', 'Knoks Park', 13);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Columbus Rugby', 'Womens Midwest D2', 'USAR', 'OSU Field', 'Beekman Park', 14);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Fort Wayne Rugby', 'Womens Midwest D2', 'USAR', 'Fort Wayne Field', 'Riverbend Park', 15);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Grand Rapids Rugby', 'Womens Midwest D2', 'USAR', 'Hammer Field', 'Grand Rapids Park', 16);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Dayton Flying Pigs', 'Womens Midwest D2', 'USAR', 'Wright Field', 'First Flight Park', 17);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Louisville Riversharks', 'Womens Midwest D2', 'USAR', 'Bourbon Field', 'Lake Park', 18);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Chicago Lions', 'Mens Midwest D1', 'USAR', 'Solider Field', 'Mile Drive Park', 19);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Detroit Tradesmen', 'Mens Midwest D1', 'USAR', 'Detroit Field', 'Ford Park', 20);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Cincinnati Wolfhounds', 'Mens Midwest D1', 'USAR', 'Great American Field', 'Richard Ford Park', 21);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Columbus Rugby', 'Mens Midwest D1', 'USAR', 'OSU Field', 'Beekman Park', 22);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Chicago Griffins', 'Mens Midwest D1', 'USAR', 'Solider Field', 'Mile Drive Park', 23);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Glendale Merlins', 'Womens Frontier D1', 'USAR', 'Glendale Field', 'Mile Drive Park', 24);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Austin Valkyries', 'Womens Frontier D1', 'USAR', 'Cowboy Field', 'Steer Drive Park', 25);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Houston Athletic', 'Womens Frontier D1', 'USAR', 'Crew Field', 'Hammock Park', 26);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Utah Vipers', 'Womens Frontier D1', 'USAR', 'Salt Lake Field', 'Desert Park', 27);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Black Ice Rugby', 'Womens Frontier D1', 'USAR', 'Fire Field', 'Ice Park', 28);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Dallas Harlequins', 'Womens Frontier D1', 'USAR', 'Jerryland Field', 'Cody Park', 29);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('St. Louis Royal Ramblers', 'Mens Frontier D2', 'USAR', 'Blues Field', 'Cardnial Park', 30);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Wichita Barbarians', 'Mens Frontier D2', 'USAR', 'Ghangis Khan Field', 'Huns Park', 31);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Kansas City Rugby', 'Mens Frontier D2', 'USAR', 'BBQ Field', 'Ribs Park', 32);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Belmont Shore Rugby', 'Mens Pacific D1', 'USAR', 'Whaler Field', 'Beach Park', 33);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Life West Gladiators', 'Mens Pacific D1', 'USAR', 'The Field', 'Funtime Park', 34);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Old Mission Beach Athletic', 'Mens Pacific D1', 'USAR', 'Happy Field', 'Rock Park', 35);
INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES ('Glendale Merlins', 'Mens Pacific D1', 'USAR', 'Whaler Field', 'Beach Park', 36);


INSERT INTO Leagues (leagueName, org, sport) VALUES ('Mens Pacific D1', 'USAR', 'Rugby');
INSERT INTO Leagues (leagueName, org, sport) VALUES ('Womens Midwest D1', 'USAR', 'Rugby');
INSERT INTO Leagues (leagueName, org, sport) VALUES ('Womens Midwest D2', 'USAR', 'Rugby');
INSERT INTO Leagues (leagueName, org, sport) VALUES ('Mens Midwest D1', 'USAR', 'Rugby');
INSERT INTO Leagues (leagueName, org, sport) VALUES ('Womens Frontier D1', 'USAR', 'Rugby');
INSERT INTO Leagues (leagueName, org, sport) VALUES ('Mens Frontier D2', 'USAR', 'Rugby');

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

INSERT INTO Schedule(homeTeam, awayTeam, date, venue) VALUES ('Minnesota Valkyries' , 'Detroit Rugby' , '2020-09-05', 'Chase Field');
INSERT INTO Schedule(homeTeam, awayTeam, date, venue) VALUES ('Minnesota Valkyries' , 'Wisconsin Rugby' , '2020-09-06', 'Chase Field');
INSERT INTO Schedule(homeTeam, awayTeam, date, venue) VALUES ('Minnesota Valkyries' , 'Chicago Lions' , '2020-09-07', 'Chase Field');
INSERT INTO Schedule(homeTeam, awayTeam, date, venue) VALUES ('Minnesota Valkyries' , 'Wisconsin Rugby' , '2020-09-08', 'Chase Field');
INSERT INTO Schedule(homeTeam, awayTeam, date, venue) VALUES ('Chicago Lions' , 'Minnesota Valkyries' , '2020-09-09', 'Solider Field');
INSERT INTO Schedule(homeTeam, awayTeam, date, venue) VALUES ('Detroit Rugby', 'Minnesota Valkyries' , '2020-09-10', 'Detroit Field');
INSERT INTO Schedule(homeTeam, awayTeam, date, venue) VALUES ('Wisconsin Rugby' , 'Minnesota Valkyries' , '2020-09-11',  'Lambeau Field');
INSERT INTO Schedule(homeTeam, awayTeam, date, venue) VALUES ('Chicago Rugby' , 'Minnesota Valkyries' , '2020-09-12', 'Solider Field');

INSERT INTO Messages(SentByUserID, toUserID, messageBody) VALUES (1,5, 'Minnesota Valkyries would like to play you on 09/05/2020 at Chase Field')
INSERT INTO Messages(SentByUserID, toUserID, messageBody) VALUES (1,6, 'Minnesota Valkyries would like to play you on 09/06/2020 at Chase Field')
INSERT INTO Messages(SentByUserID, toUserID, messageBody) VALUES (1,2, 'Minnesota Valkyries would like to play you on 09/07/2020 at Chase Field')
INSERT INTO Messages(SentByUserID, toUserID, messageBody) VALUES (1,6, 'Minnesota Valkyries would like to play you on 09/08/2020 at Chase Field')
INSERT INTO Messages(SentByUserID, toUserID, messageBody) VALUES (2,1, 'Chicago Lions would like to play you on 09/09/2020 at Solider Field')
INSERT INTO Messages(SentByUserID, toUserID, messageBody) VALUES (5,1, 'Detroit Rugby would like to play you on 09/10/2020 at Detroit Field')
INSERT INTO Messages(SentByUserID, toUserID, messageBody) VALUES (6,1, 'Wisconsin Rugby would like to play you on 09/11/2020 at Lambeau Field')
INSERT INTO Messages(SentByUserID, toUserID, messageBody) VALUES (2,1, 'Chicago Rugby would like to play you on 09/12/2020 at Solider Field')
INSERT INTO Messages(SentByUserID, toUserID, messageBody) VALUES (2,3, 'Chicago Rugby would like to play you on 09/12/2020 at Solider Field')
INSERT INTO Messages(SentByUserID, toUserID, messageBody, userAccepted) VALUES (2,3, 'Chicago Rugby would like to play you on 09/12/2020 at Solider Field', 'Accepted')INSERT INTO Messages(SentByUserID, toUserID, messageBody, userAccepted) VALUES (2,4, 'Chicago Rugby would like to play you on 09/19/2020 at Solider Field', 'Accepted')


COMMIT TRANSACTION;

--Roster SQL to get all "SELECT firstName, lastName, email, phone, Teams.Name FROM Roster JOIN Teams on Roster.teamID = Teams.id WHERE Teams.id = @teamID";

