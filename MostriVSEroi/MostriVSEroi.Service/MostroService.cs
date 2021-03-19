using MostriVSEroi.Core.Entities;
using MostriVSEroi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MostriVSEroi.Service
{
    public class MostroService
    {

        // Tra i campi ho la repository relativa a Mostro e altre repository utili per la creazione di Mostro
        private IMostroRepository _repo;
        private IClasseRepository _classeRepo;
        private IArmaRepository _armaRepo;
        private ILivelloRepository _livelloRepo;

        //Costruttore
        public MostroService(IMostroRepository repo, IClasseRepository classeRepo, IArmaRepository armaRepo, ILivelloRepository livelloRepo)
        {
            _repo = repo;
            _classeRepo = classeRepo;
            _armaRepo = armaRepo;
            _livelloRepo = livelloRepo;
        }

        //Metodi

        /// <summary>
        /// Metodo che richiama da repository _repo <c>GetAllWithLevelLessOrEqualThan</c> per ottenere tutti i mostri di livello inferiore o uguale a quello dato in input
        /// </summary>
        /// <param name="levelIDLimit">Id del livello massimo dei mostri che voglio ottenere, di tipo intero</param>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Mostro se tutto va a buon fine, valore di default se viene generata un'eccezione</returns>
        public IEnumerable<Mostro> GetAllMostriWithLevelLessOrEqualThan(int levelIDLimit)
        {
            return _repo.GetAllWithLevelLessOrEqualThan(levelIDLimit);

        }

        /// <summary>
        /// Metodo che richiama da repository _repo <c>GetAllWithLevelBetween</c> per ottenere tutti i mostri di livello compreso tra due livelli (limite minimo escluso)
        /// </summary>
        /// <param name="levelIDMin">ID del livello minimo dei mostri che voglio ottenere, di tipo intero</param>
        /// <param name="levelIDMax">ID del livello massimo dei mostri che voglio ottenere, di tipo intero</param>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Mostro se tutto va a buon fine, valore di default se viene generata un'eccezione</returns>
        public IEnumerable<Mostro> GetAllMostriWithLevelBetween(int levelIDMin, int levelIDMax)
        {
            return _repo.GetAllWithLevelBetween(levelIDMin, levelIDMax);

        }




        /// <summary>
        /// Metodo per la creazione di un mostro
        /// Si chiedono da Console le informazioni relative al mostro da creare mostrando le scelte (ottenute attraverso le repository _classeRepo e _armaRepo e _livelloRepo)
        /// dopodiché chiama il metodo Create da repository _repo
        /// </summary>
        /// <returns>Restituisce un booleano: true se tutto è andato a buon fine, false altrimenti (se nessuna eccezione è stata generata nelle varie chiamate)</returns>

        public bool CreateMostro()
        {
            string nome;
            do
            {
                Console.WriteLine("Scegli un nome per il mostro");
                nome = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nome)) Console.WriteLine("Nome non valido\n");
            } while (string.IsNullOrWhiteSpace(nome));

            nome = nome.Trim();
            Console.WriteLine("Scegli la classe del mostro tra queste:");

            IEnumerable<Classe> classi = _classeRepo.GetAllClassiByIsEroe(false);
            if (classi == null)  return false; // Eccezione generata

            int j;
            for (int i = 0; i < classi.Count(); i++)
            {
                Console.WriteLine("Premi " + i.ToString() + " per " + classi.ElementAt(i).Nome);
            }
            while (int.TryParse(Console.ReadLine(), out j) == false || j < 0 || j >= classi.Count())
            {
                Console.WriteLine("Digita nuovamente");
            }
            Console.WriteLine("Scegli l'arma per il mostro tra queste:");

            IEnumerable<Arma> armi = _armaRepo.GetArmiByClasseID(classi.ElementAt(j).ID);
            if (armi == null)  return false; // Eccezione generata

            int index_arma;
            for (int i = 0; i < armi.Count(); i++)
            {
                Console.WriteLine("Premi " + i.ToString() + " per " + armi.ElementAt(i).Nome);
            }
            while (int.TryParse(Console.ReadLine(), out index_arma) == false || index_arma < 0 || index_arma >= armi.Count())
            {
                Console.WriteLine("Digita nuovamente");
            }


            IEnumerable<Livello> livelli = _livelloRepo.GetAll();
            if (livelli == null)  return false; // Eccezione generata

            int index_lev;
            Console.WriteLine("Scegli un livello da 1 a " + livelli.Count());
            
            while (int.TryParse(Console.ReadLine(), out index_lev) == false || index_lev < 1 || index_lev > livelli.Count())
            {
                Console.WriteLine("Digita nuovamente");
            }


            Mostro mostro = new Mostro(nome, armi.ElementAt(index_arma), livelli.ElementAt(index_lev-1));


            if (_repo.Create(mostro))
            {
                Console.WriteLine("Mostro creato!\n");
                return true;
            }
            else return false; // Eccezione generata

        }
    }
}
