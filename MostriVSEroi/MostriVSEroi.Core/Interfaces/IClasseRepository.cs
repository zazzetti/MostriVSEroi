using MostriVSEroi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi.Core.Interfaces
{
    /// <summary>
    /// Interfaccia <c>IClasseRepository</c> che eredita dall'interfaccia <c>IRepository<T></c> con T = Classe
    /// </summary>
    public interface IClasseRepository: IRepository<Classe>
    {
        /// <summary>
        /// Metodo per ottenere le classi associate agli eroi o hai mostri
        /// </summary>
        /// <param name="isEroe">Parametro di tipo booleano: true se voglio ottenere le classi degli eroi,
        /// false se voglio ottenere quelle dei mostri</param>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Classe</returns>
        IEnumerable<Classe> GetAllClassiByIsEroe(bool isEroe);

    }
}
