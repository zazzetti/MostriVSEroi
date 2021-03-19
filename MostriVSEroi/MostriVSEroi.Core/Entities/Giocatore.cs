using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi.Core.Entities
{
    /// <summary>
    /// Classe <c>Giocatore</c>
    /// </summary>
    public class Giocatore
    {

        // CAMPI

        ///ID del giocatore, di tipo intero
        public int ID { get; set; }

        ///Nome del giocatore, di tipo stringa
        public string Nome { get; set; }

        ///Flag che indica se il giocatore è un admin o no, di tipo booleano
        public bool IsAdmin { get; set; }

    }
}

