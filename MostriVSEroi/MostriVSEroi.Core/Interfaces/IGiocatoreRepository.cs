using MostriVSEroi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi.Core.Interfaces
{
    /// <summary>
    /// Interfaccia <c>IGiocatoreRepository</c> che eredita dall'interfaccia <c>IRepository<T></c> con T = Giocatore
    /// </summary>
    public interface IGiocatoreRepository: IRepository<Giocatore>
    {
        /// <summary>
        /// Metodo per ottenere il giocatore attraverso il nome (univoco)
        /// </summary>
        /// <param name="nome">Nome del giocatore, di tipo stringa</param>
        /// <returns>Restituisce oggetto di tipo Giocatore</returns>
        Giocatore GetByNome(string nome);

    }
}
