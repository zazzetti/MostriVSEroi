using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi.Core.Entities
{
    /// <summary>
    ///  Classe <c>Classe</c>
    /// </summary>
    public class Classe
    {
        //CAMPI
        

        ///ID della classe, di tipo intero
        public int ID { get; set; }

        /// Nome della classe, di tipo stringa
        public string Nome { get; set; }

        /// Flag che indica se è una classe di eroe o mostro, di tipo booleano
        public bool IsEroe { get; set; }
    }
}
