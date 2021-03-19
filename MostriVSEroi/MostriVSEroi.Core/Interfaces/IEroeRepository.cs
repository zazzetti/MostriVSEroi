using MostriVSEroi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi.Core.Interfaces
{
    /// <summary>
    /// Interfaccia <c>IEroeRepository</c> che eredita dall'interfaccia <c>IRepository<T></c> con T = Eroe
    /// </summary>
    public interface IEroeRepository : IRepository<Eroe>
    {
        /// <summary>
        /// Metodo per ottenere tutti gli eroi di un giocatore
        /// </summary>
        /// <param name="giocatoreID">ID del giocatore, di tipo intero</param>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Eroe se tutto va a buon fine, valore di default se viene generata un'eccezione</returns>

        IEnumerable<Eroe> GetAllByGiocatoreID(int giocatoreID);

        /// <summary>
        /// Metodo per ottenere il massimo ID degli eroi di un giocatore, cioè l'ID dell'ultimo eroe creato
        /// metodo utile per restituire l'eroe appena creato con l'ID generato nel database
        /// </summary>
        /// <param name="giocatoreID">ID del giocatore, di tipo intero</param>
        /// <returns>Restituisce l'ID cercato se tutto è andato a buon fine, -1 se viene generata un'eccezione</returns>
        int GetMaxIDEroeByGiocatoreID(int giocatoreID);
    }
     

}
