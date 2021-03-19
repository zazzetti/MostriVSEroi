using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi.Core.Entities
{
    /// <summary>
    /// Classe astratta <c>Personaggio</c> da cui derivano le classi <c>Eroe</c> e <c>Mostro</c>
    /// </summary>
    public abstract class Personaggio
    {

        //CAMPI


        ///ID del Personaggio, di tipo intero
        public int ID { get; set; }

        /// Nome del Personaggio, di tipo stringa
        public string Nome { get; set; }

        /// Classe associata al Personaggio, di tipo Classe
        public Classe Classe { get; set; }

        /// Arma associata al Personaggio, di tipo Arma
        public Arma Arma { get; set; }

        /// Livello del Personaggio, di tipo Livello
        public Livello Livello { get; set; }

        /// Punti vita del Personaggio, di tipo intero
        public int PuntiVita { get; set; }


        


    }
}
