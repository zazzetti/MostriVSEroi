CREATE DATABASE MostriVSEroi
USE MostriVSEroi
GO


CREATE TABLE Giocatore (
ID INT IDENTITY(1,1) PRIMARY KEY, 
Nome VARCHAR(255) UNIQUE NOT NULL,
IsAdmin BIT NOT NULL
)

CREATE TABLE Classe (
ID INT IDENTITY(1,1) PRIMARY KEY, 
Nome VARCHAR(255) UNIQUE NOT NULL,
IsEroe BIT NOT NULL
)

CREATE TABLE Arma (
ID INT IDENTITY(1,1) PRIMARY KEY, 
Nome VARCHAR(255) NOT NULL,
PuntiDanno INT NOT NULL,
IDClasse INT NOT NULL FOREIGN KEY REFERENCES Classe(ID),
)

CREATE TABLE Livello (
ID INT PRIMARY KEY, 
PuntiVita INT NOT NULL,
PuntiAccumulati INT NOT NULL
)

CREATE TABLE Eroe (
ID INT IDENTITY(1,1) PRIMARY KEY, 
Nome VARCHAR(255) NOT NULL,
IDGiocatore INT NOT NULL FOREIGN KEY REFERENCES Giocatore(ID),
IDClasse INT NOT NULL FOREIGN KEY REFERENCES Classe(ID),
IDArma INT NOT NULL FOREIGN KEY REFERENCES Arma(ID),
IDLivello INT NOT NULL FOREIGN KEY REFERENCES Livello(ID),
PuntiVita INT NOT NULL,
PuntiAccumulati INT NOT NULL,
TempoDiGioco INT NOT NULL,
HasWon BIT NOT NULL)

CREATE TABLE Mostro (
ID INT IDENTITY(1,1) PRIMARY KEY, 
Nome VARCHAR(255) NOT NULL,
IDClasse INT NOT NULL FOREIGN KEY REFERENCES Classe(ID),
IDArma INT NOT NULL FOREIGN KEY REFERENCES Arma(ID),
IDLivello INT NOT NULL FOREIGN KEY REFERENCES Livello(ID)
)

INSERT INTO Giocatore
VALUES('ElenaZaz', 1), ('UserProva', 0) 

SELECT * FROM Giocatore

INSERT INTO Classe
VALUES('Guerriero', 1), ('Mago', 1),('Cultista', 0),('Orco', 0),('Signore del Male', 0)

SELECT * FROM Classe

INSERT INTO Arma
VALUES ('Spada', 15, 1), ('Arco', 10, 1),('Ascia', 20, 1),
('Scettro', 10, 2),('Incantesimo', 9,2),
('Falce', 7,3),('Flagello', 12, 3),
('Ascia',8,4),('Martello chiodato', 10,4),
('Alabarda',11,5), ('Frusta',8,5)


SELECT * FROM Arma

INSERT INTO Livello
VALUES(1, 20, 0), (2, 40,30), (3,60, 60), (4, 80, 90), (5, 100, 120) 

SELECT * FROM Livello

SELECT * FROM Eroe
SELECT * FROM Giocatore
SELECT * FROM Arma
SELECT * FROM Livello
SELECT * FROM Mostro
SELECT * FROM Classe
INSERT INTO Eroe

VALUES ('EroeProvaAdm', 1, 1, 1, 1, 20, 0, 0,0), ('EroeProvaUser', 2, 2, 4, 1, 20, 0, 0,0)

SELECT * FROM Mostro

INSERT INTO Mostro
VALUES ('Cultista Nero', 3,5,1 ),
('Cultista Fanatico', 3, 6, 2),
('Goblin',4, 7,3),
('Uruk',4, 8,4),
('Gong',4, 7,3),
('Signore Oscuro', 5, 9, 5),
('Maleficent', 5, 10,2)



