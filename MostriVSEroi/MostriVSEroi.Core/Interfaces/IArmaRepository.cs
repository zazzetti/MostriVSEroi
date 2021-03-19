using MostriVSEroi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi.Core.Interfaces
{
    /// <summary>
    /// Interfaccia <c>IArmaRepository</c> che eredita dall'interfaccia <c>IRepository<T></c> con T = Arma
    /// </summary>
    public interface IArmaRepository: IRepository<Arma>
    {
        /// <summary>
        /// Metodo per ottenere tutte le armi associate a una determinata classe
        /// </summary>
        /// <param name="classeID">Id della classe di cui voglio ottenere le armi, di tipo intero</param>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Arma</returns>
        IEnumerable<Arma> GetArmiByClasseID(int classeID);
    }
}
