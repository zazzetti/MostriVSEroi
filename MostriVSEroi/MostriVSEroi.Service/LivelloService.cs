using MostriVSEroi.Core.Entities;
using MostriVSEroi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi.Service
{
    /// <summary>
    /// Business layer per Livello
    /// </summary>
    public class LivelloService
    {
        // campo: repository di Livello
        private ILivelloRepository _repo;

        // Costruttore
        public LivelloService(ILivelloRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Metodo che richiama da repository <c>GetAll</c> per ottenere tutti i livelli
        /// </summary>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Livello</returns>
        public IEnumerable<Livello> GetAllLivelli()
        {
            return _repo.GetAll();

        }
    }
}
