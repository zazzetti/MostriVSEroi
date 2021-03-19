CREATE VIEW VistaArma AS
SELECT Arma.ID as IDArma, Arma.Nome as NomeArma, PuntiDanno, IDClasse, Classe.Nome as NomeClasse, IsEroe
FROM  Arma INNER JOIN Classe on Arma.IDClasse=Classe.ID 


CREATE VIEW VistaEroe AS
SELECT Eroe.ID as IDEroe , Eroe.Nome as NomeEroe, TempoDiGioco,
Eroe.PuntiAccumulati as PuntiAccumulatiEroe, Eroe.PuntiVita as PuntiVitaEroe, HasWon,
Eroe.IDArma, NomeArma, PuntiDanno, 
Eroe.IDClasse, NomeClasse, IsEroe,
IDLivello, Livello.PuntiAccumulati as PuntiAccumulatiLivello, Livello.PuntiVita as PuntiVitaLivello,
IDGiocatore, IsAdmin, Giocatore.Nome as NomeGiocatore
FROM  Eroe INNER JOIN Giocatore on Eroe.IDGiocatore=Giocatore.ID
INNER JOIN VistaArma on Eroe.IDArma=VistaArma.IDArma
INNER JOIN Livello on Eroe.IDLivello=Livello.ID




CREATE VIEW VistaMostro AS
SELECT Mostro.ID as IDMostro , Mostro.Nome as NomeMostro,
Mostro.IDArma, NomeArma, PuntiDanno, 
Mostro.IDClasse, NomeClasse, IsEroe,
IDLivello, PuntiAccumulati as PuntiAccumulatiLivello, PuntiVita as PuntiVitaLivello
FROM  Mostro INNER JOIN VistaArma on Mostro.IDArma=VistaArma.IDArma
INNER JOIN Livello on Mostro.IDLivello=Livello.ID
