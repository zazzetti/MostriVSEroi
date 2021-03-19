using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi.Core.Entities
{
    /// <summary>
    /// Classe <c>Arma</c>
    /// </summary>
    public class Arma
    {
        //CAMPI
        


        ///ID dell'arma, di tipo intero
        public int ID { get; set; }

        ///Nome dell'arma, di tipo stringa
        public string Nome { get; set; }

        ///Punti danno che infligge l'arma, di tipo intero
        public int PuntiDanno { get; set; }

        ///Classe dell'arma, di tipo Classe
        public Classe Classe { get; set; }

    }
}
