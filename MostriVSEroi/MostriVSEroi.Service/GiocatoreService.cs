using MostriVSEroi.Core.Entities;
using MostriVSEroi.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace MostriVSEroi.Service
{
    /// <summary>
    /// Business layer per Giocatore
    /// </summary>
    public class GiocatoreService
    {
        // campo provato repository di Giocatore
        private IGiocatoreRepository _repo;



        // Costruttore

        public GiocatoreService(IGiocatoreRepository repo)
        {
            _repo = repo;
        }


        //Metodi


        /// <summary>
        ///  Metodo che richiama da repository _repo <c>GetAll</c> per ottenere tutti i giocatori
        /// </summary>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Giocatore se tutto va a buon fine, null (valore di default) se viene generata un'eccezione nel metodo chiamato </returns>
        public IEnumerable<Giocatore> GetAllGiocatori()
        {
            return _repo.GetAll();

        }


        /// <summary>
        /// Metodo che richiama da repository _repo <c>GetByNome</c> per ottenere un giocatore attraverso il nome (univoco)
        /// </summary>
        /// <param name="nome">Nome del giocatore, di tipo stringa</param>
        /// <returns>Restituisce un oggetto di tipo Giocatore se tutto va a buon fine, altrimenti null (valore di default) se viene generata un'eccezione nel metodo chiamato </returns>
        public Giocatore GetGiocatoreByNome(string nome)
        {
            return _repo.GetByNome(nome);

        }



        /// <summary>
        /// Metodo per la creazione di un giocatore. Si richiede da Console il nome del giocatore
        /// </summary>
        /// <param name="nome">Nome del giocatore, di tipo stringa</param>
        /// <returns>Restituisce il giocatore appena creato se tutto è andato a buon fine, valore di default altrimenti</returns>

        public Giocatore CreateGiocatore(string nome)
        {
            while (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome non valido. Inserisci uno UserName valido\n");
                nome = Console.ReadLine();
            }
            

            Giocatore g = new Giocatore
            {
                Nome = nome,
                IsAdmin = false
            };

            if( _repo.Create(g)) return _repo.GetByNome(g.Nome); // se la creazione è andata a buon fine recupera il giocatore con ID creato da database

            // Eccezione generata in _repo.Create(Giocatore g)
            return default;
        }


    }
}

