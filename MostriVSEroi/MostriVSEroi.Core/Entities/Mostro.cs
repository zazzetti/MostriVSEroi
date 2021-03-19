using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi.Core.Entities
{
    /// <summary>
    /// Classe <c>Mostro</c> che deriva dalla classe <c>Personaggio</c>
    /// </summary>
    public class Mostro : Personaggio
    {

        // COSTRUTTORI
  


        /// <summary>
        /// Costruttore utile per creare l'oggetto Mostro leggendo da Database
        /// Dati i requisiti del gioco la classe del mostro sarà necessariamente quella dell'arma scelta mentre i punti vita del mostro saranno uguali ai punti vita del livello assegnato
        /// </summary>
        /// <param name="id">Id del mostro, di tipo intero </param>
        /// <param name="nome">Nome del mostro, di tipo stringa </param>
        /// <param name="arma">Arma assegnata al mostro, oggetto di tipo Arma</param>
        /// <param name="livello"> Livello del mostro, oggetto di tipo Livello</param>

        public Mostro(int id, string nome, Arma arma, Livello livello)
        {
            ID = id;
            Nome = nome;
            Arma = arma;
            Classe = arma.Classe;
            Livello = livello;
            PuntiVita = livello.PuntiVita;
        }


        /// <summary>
        /// Costruttore utile per Inserire un nuovo mostro a database (ID non necessario per creare oggetto Mostro, nel database viene creato in automatico) 
        /// Dati i requisiti del gioco la Classe del mostro sarà necessariamente quella dell'arma scelta mentre i punti vita del mostro saranno uguali ai punti vita del livello assegnato
        /// </summary>
        /// <param name="nome"> Nome del mostro in formato stringa </param>
        /// <param name="arma"> Arma assegnata al mostro, oggetto di tipo Arma</param>
        /// <param name="livello"> Livello del mostro, oggetto di tipo Livello </param>
        public Mostro(string nome, Arma arma, Livello livello)
        {
            Nome = nome;
            Arma = arma;
            Classe = arma.Classe;
            Livello = livello;
            PuntiVita = livello.PuntiVita;
        }
    }
}
