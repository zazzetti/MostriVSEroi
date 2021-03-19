using MostriVSEroi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi.Core.Interfaces
{
    /// <summary>
    /// Interfaccia <c>IMostroRepository</c> che eredita dall'interfaccia <c>IRepository<T></c> con T = Mostro
    /// </summary>
    public interface IMostroRepository: IRepository<Mostro>
    {
        /// <summary>
        /// Metodo per ottenere tutti i mostri di livello inferiore o uguale a quello dato in input
        /// </summary>
        /// <param name="levelIDLimit">Id del livello massimo dei mostri che voglio ottenere, di tipo intero</param>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Mostro se tutto va a buon fine, valore di default se viene generata un'eccezione</returns>
        IEnumerable<Mostro> GetAllWithLevelLessOrEqualThan(int levelIDLimit);

        /// <summary>
        /// Metodo per ottenere tutti i mostri di livello compreso tra due livelli (limite minimo escluso)
        /// metodo utile per integrare la lista dei mostri qualora ci sia stato un passaggio di livello o un cambio eroe con livello maggiore
        /// </summary>
        /// <param name="levelIDMin">ID del livello minimo dei mostri che voglio ottenere, di tipo intero</param>
        /// <param name="levelIDMax">ID del livello massimo dei mostri che voglio ottenere, di tipo intero</param>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Mostro se tutto va a buon fine, valore di default se viene generata un'eccezione</returns>
        IEnumerable<Mostro> GetAllWithLevelBetween(int levelIDMin, int levelIDMax);

    }
}
