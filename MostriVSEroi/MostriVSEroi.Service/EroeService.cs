using MostriVSEroi.Core.Entities;
using MostriVSEroi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MostriVSEroi.Service
{    /// <summary>
     /// Business layer per Eroe
     /// </summary>
    public class EroeService
    {
        // Tra i campi ho la repository relativa a Eroe e altre repository utili per la creazione di Eroe
        private IEroeRepository _repo;
        private IClasseRepository _classeRepo;
        private IArmaRepository _armaRepo;
        private ILivelloRepository _livelloRepo;
        
        // Costruttore
        public EroeService(IEroeRepository repo, IClasseRepository classeRepo, IArmaRepository armaRepo, ILivelloRepository livelloRepo)
        {
            _repo = repo;
            _classeRepo = classeRepo;
            _armaRepo = armaRepo;
            _livelloRepo = livelloRepo;
        }


        //Metodi

        /// <summary>
        /// Metodo per la creazione di un eroe
        /// Si chiedono da Console le informazioni relative all'eroe da creare mostrando le scelte (ottenute attraverso le repository _classeRepo e _armaRepo)
        /// il livello viene settato di default al primo (ottenuto attraverso la repository _livelloRepo), dopodiché chiama il metodo Create da repository _repo
        /// e metodo GetMaxIDEroeByGiocatoreID per ottenere l'ID appena creato
        /// </summary>
        /// <param name="giocatore">Giocatore che sta creando l'eroe, di tipo Giocatore</param>
        /// <returns>Restituisce l'eroe appena creato se tutto è andato a buon fine, valore di default altrimenti (se nessuna eccezione è stata generata nelle varie chiamate)</returns>

        public Eroe CreateEroe(Giocatore giocatore)
        {


            string nome;
            do
            {
                Console.WriteLine("Scegli un nome per il tuo eroe");
                nome = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nome)) Console.WriteLine("Nome non valido\n"); // Nome non valido se nullo o fatto da spazi
            } while (string.IsNullOrWhiteSpace(nome));
            nome = nome.Trim();
            Console.WriteLine("Scegli la classe del tuo eroe tra queste:");
            
            IEnumerable<Classe> classi = _classeRepo.GetAllClassiByIsEroe(true); // con true accedo alle classi di Eroe
            if (classi == null) return default; // Eccezione generata

            int j;
            for( int i=0; i<classi.Count(); i++)
            {
                Console.WriteLine("Premi " + i.ToString()+ " per " + classi.ElementAt(i).Nome);
            }
            while(int.TryParse(Console.ReadLine(), out j)==false || j<0 || j >= classi.Count())
            {
                Console.WriteLine("Non valido. Digita nuovamente\n");
            }
            Console.WriteLine("Scegli l'arma per il tuo eroe tra queste:");

            IEnumerable<Arma> armi = _armaRepo.GetArmiByClasseID(classi.ElementAt(j).ID);
            if (armi == null)  return default; // Eccezione generata

            int index_arma;
            for (int i = 0; i < armi.Count(); i++)
            {
                Console.WriteLine("Premi " + i.ToString() + " per " + armi.ElementAt(i).Nome);
            }
            while (int.TryParse(Console.ReadLine(), out index_arma) == false || index_arma < 0 || index_arma >= armi.Count())
            {
                Console.WriteLine("Non valido. Digita nuovamente\n");
            }


            Livello livello = _livelloRepo.GetByID(1); // Scelgo di default il primo livello per l'eroe appena creato
            if (livello == null)  return default; // Eccezione generata

            Eroe eroe = new Eroe(nome, armi.ElementAt(index_arma), livello, giocatore);

            if (_repo.Create(eroe))
            {
                eroe.ID=GetMaxIDEroeByGiocatoreID(giocatore.ID);
                if (eroe.ID != -1)
                { // se nessuna eccezione generata in GetMaxIDEroeByGiocatoreID
                    Console.WriteLine("Eroe creato!\n");
                    return eroe;
                }
            }

            // Eccezione generata in _repo.Create(Eroe eroe)
            return default;

        }


        /// <summary>
        /// Metodo che richiama da repository _repo <c>GetMaxIDEroeByGiocatoreID</c> per ottenere il massimo ID degli eroi di un giocatore, cioè l'ID dell'ultimo eroe creato
        /// </summary>
        /// <param name="giocatoreID">ID del giocatore, di tipo intero</param>
        /// <returns>Restituisce l'ID cercato se tutto è andato a buon fine, -1 se viene generata un'eccezione nel metodo chiamato</returns>
        public int GetMaxIDEroeByGiocatoreID(int giocatoreID)
        {
            return _repo.GetMaxIDEroeByGiocatoreID(giocatoreID);
        }

        /// <summary>
        ///  Metodo che richiama da repository _repo <c>GetAll</c> per ottenere tutti gli eroi
        /// </summary>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Eroe se tutto va a buon fine, null (valore di default) se viene generata un'eccezione </returns>
        public IEnumerable<Eroe> GetAllEroi()
        {
            return _repo.GetAll();

        }

        /// <summary>
        ///  Metodo che richiama da repository _repo <c>GetAllByGiocatoreID</c> per ottenere tutti gli eroi di un giocatore
        /// </summary>
        /// <param name="giocatoreID">ID del giocatore, di tipo intero</param>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Eroe se tutto va a buon fine, null (valore di default) se viene generata un'eccezione</returns>
        public IEnumerable<Eroe> GetAllEroiByGiocatoreID(int giocatoreID)
        {
            return _repo.GetAllByGiocatoreID(giocatoreID);

        }

        /// <summary>
        /// Metodo per ottenere un eroe scegliendo tra gli eroi del giocatore.
        /// Richiama il metodo <c>GetAllEroiByGiocatoreID</c>
        /// </summary>
        /// <param name="giocatoreID">ID del giocatore, di tipo intero</param>
        /// <returns>Restituisce l'eroe scelto, di tipo Eroe</returns>
        public Eroe GetEroeFromAllEroiByGiocatoreID(int giocatoreID)
        {

            IEnumerable<Eroe> eroi = GetAllEroiByGiocatoreID(giocatoreID);
            if (eroi == null) return default; // Eccezione generata
            if (eroi.Count() == 0) { Console.WriteLine("Non hai eroi salvati\n"); return null; }

            Console.WriteLine("Scegli uno dei tuoi eroi:");

            int j;
            for (int i = 0; i < eroi.Count(); i++)
            {
                Console.WriteLine("Premi " + i.ToString() + " per giocare con " + eroi.ElementAt(i).ToString());
            }
            while (int.TryParse(Console.ReadLine(), out j) == false || j < 0 || j >= eroi.Count())
            {
                Console.WriteLine("Non valido. Digita nuovamente");
            }
            return eroi.ElementAt(j);
        }

        /// <summary>
        /// Metodo che richiama da repository _repo <c>DeleteEroe</c> per eliminare un eroe
        /// </summary>
        /// <param name="eroe"></param>
        /// <returns>Restituisce un booleano: true se è andata a buon fine l'eliminazione, false se viene generata un'eccezione nel metodo chiamato</returns>
        public bool DeleteEroe(Eroe eroe)
        {

            return _repo.Delete(eroe);
        }

        /// <summary>
        /// Metodo che richiama da repository _repo <c>Update</c> per fare l'update di un eroe
        /// Scrive a console che i progressi sono salvati se il metodo <c>Update</c> è andato a buon fine
        /// </summary>
        /// <param name="eroe"></param>
        /// <returns>Restituisce un booleano: true se è andato a buon fine l'update, false se viene generata un'eccezione nel metodo chiamato</returns>
        public bool UpdateEroe(Eroe eroe)
        {
            if (_repo.Update(eroe))
            {// se nessuna eccezione generata
                Console.WriteLine("Progressi salvati\n");
                return true;
            }
            else return false;
            
        }

        /// <summary>
        ///  Metodo per eliminare uno degli eroi di un giocatore. Richiama il metodo <c>GetAllEroiByGiocatoreID</c> per mostrare gli eroi
        ///  e <c>DeleteEroe</c> per eliminare l'eroe scelto.  Scrive a console che l'eroe è stato eliminato se nessuna eccezione è stata generata
        /// </summary>
        /// <param name="giocatoreID">ID del giocatore, di tipo intero</param>
        /// <returns>Restituisce un booleano: true se tutto è andato a buon fine , false se viene generata un'eccezione nei metodi chiamati</returns>
        public bool DeleteEroeByGiocatoreID(int giocatoreID)
            {

            IEnumerable<Eroe> eroi = GetAllEroiByGiocatoreID(giocatoreID);
            if (eroi == null)  return false; // Eccezione generata

            if (eroi.Count() == 0) { Console.WriteLine("Non hai eroi da eliminare"); return false; }

            Console.WriteLine("Quale eroe vuoi eliminare?");

            int j;
            for (int i = 0; i < eroi.Count(); i++)
            {
                Console.WriteLine("Premi " + i.ToString() + " per eliminare" + eroi.ElementAt(i).ToString());
            }
            while (int.TryParse(Console.ReadLine(), out j) == false || j < 0 || j >= eroi.Count())
            {
                Console.WriteLine("Non valido. Digita nuovamente");
            }

            if (DeleteEroe(eroi.ElementAt(j))) // se nessuna eccezione generata
            {
                Console.WriteLine("Eroe eliminato");
                return true;
            }
            return false;
        }

    }
}
