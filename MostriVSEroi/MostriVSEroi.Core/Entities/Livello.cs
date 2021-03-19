using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi.Core.Entities
{
    /// <summary>
    /// Classe <c>Livello</c>
    /// </summary>
    public class Livello
    {

        //CAMPI


        /// ID del livello (che corrisponde al livello stesso), di tipo intero
        public int ID { get; set; }

        /// Punti vita corrispondenti al livello, di tipo intero
        public int PuntiVita { get; set; }

        /// Minimo punti accumulati che deve avere un eroe per passare a questo livello, di tipo intero
        public int PuntiAccumulati { get; set; }
    }
}
