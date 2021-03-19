using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi.Core.Entities
{
    /// <summary>
    /// Classe <c>Eroe</c> che deriva dalla classe <c>Personaggio</c>
    /// </summary>
    public class Eroe : Personaggio
    {
        //CAMPI
 
        /// Giocatore associato all'eroe, di tipo Giocatore
        public Giocatore Giocatore { get; set; }


        ///  Punteggio accumulato dell'eroe, di tipo intero
        public int PuntiAccumulati { get; set; }


        /// Tempo di gioco dell'eroe (espresso in millisecondi), di tipo intero
        public int TempoDiGioco { get; set; }


        /// Flag che indica se l'eroe ha già vinto, di tipo booleano
        public bool HasWon { get; set; }



        //COSTRUTTORI

        /// <summary>
        /// Costruttore utile per Inserire un nuovo eroe a database (ID non necessario per creare oggetto Eroe, nel database viene creato in automatico) 
        /// Creando un nuovo Eroe con questo costruttore i punti accumulati e il tempo di gioco saranno di default zero, mentre la flag HasWon sarà di default false mentre i punti vita saranno quelli del livello (il primo)
        /// Dati i requisiti del gioco la Classe dell'eroe sarà necessariamente quella dell'arma scelta
        /// </summary>
        /// <param name="nome"> Nome dell'eroe in formato stringa </param>
        /// <param name="arma"> Arma assegnata all'eroe, oggetto di tipo Arma</param>
        /// <param name="livello"> Livello dell'eroe, oggetto di tipo Livello </param>
        /// <param name="giocatore"> Giocatore associato all'eroe, oggetto di tipo Giocatore</param>
        public Eroe(string nome, Arma arma, Livello livello, Giocatore giocatore)
        {

            Nome = nome;
            Arma = arma;
            Classe = arma.Classe;
            Livello = livello;
            PuntiVita = livello.PuntiVita;
            Giocatore = giocatore;
            PuntiAccumulati = 0;
            TempoDiGioco = 0;
            HasWon = false;
        }

        /// <summary>
        /// Costruttore utile per creare l'oggetto Eroe leggendo da Database
        ///  Dati i requisiti del gioco la classe dell'eroe sarà necessariamente quella dell'arma scelta
        /// </summary>
        /// <param name="id">Id dell'eroe, di tipo intero </param>
        /// <param name="nome">Nome dell'eroe, di tipo stringa </param>
        /// <param name="arma">Arma assegnata all'eroe, oggetto di tipo Arma</param>
        /// <param name="livello"> Livello dell'eroe, oggetto di tipo Livello</param>
        /// <param name="giocatore"> Giocatore associato all'eroe, oggetto di tipo Giocatore </param>
        /// <param name="puntiVita"> Punti vita dell'eroe di tipo intero</param>
        /// <param name="puntiAccumulati"> Punteggio accumulato dell'eroe, di tipo intero</param>
        /// <param name="tempoDiGioco"> Tempo di gioco dell'eroe espresso in millisecondi, di tipo intero </param>
        /// <param name="hasWon"> Flag che indica se l'eroe ha già vinto, di tipo booleano </param>
        public Eroe(int id, string nome, Arma arma, Livello livello, Giocatore giocatore, int puntiVita, int puntiAccumulati, int tempoDiGioco, bool hasWon)
        {
            ID = id;
            Nome = nome;
            Arma = arma;
            Classe = arma.Classe;
            Livello = livello;
            PuntiVita = puntiVita;
            Giocatore = giocatore;
            PuntiAccumulati = puntiAccumulati;
            TempoDiGioco = tempoDiGioco;
            HasWon = HasWon;
        }
       


        //METODI

        /// <summary>
        /// Ovveride del metodo <c>ToString()</c>
        /// </summary>
        /// <returns>Restituisce una stringa con le informazioni relative all'eroe</returns>
        public override string ToString()
        {
            return Nome + " -> Arma: " + Arma.Nome + " - Punti vita: " + PuntiVita.ToString() + " - Punti accumulati: " + PuntiAccumulati.ToString();
        }

        /// <summary>
        /// Metodo utile per il display delle statistiche dell'eroe
        /// </summary>
        /// <returns>Restituisce una stringa con le informazioni relative alle statistiche</returns>
        public string DisplayStat()
        {
            return Nome + " - " + PuntiAccumulati.ToString() + " punti accumulati - " + (TempoDiGioco / 60000).ToString() + " minuti giocati";
        }


    }
}

