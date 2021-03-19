using MostriVSEroi.Core.Entities;
using MostriVSEroi.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Diagnostics;

namespace MostriVSEroi
{
    /// <summary>
    /// Classe MostriVSEroi statica e pubblica
    /// </summary>
    public static class MostriVSEroi
    {

        // Campi 

        /// lista dei mostri da cui estrarre per non riaccedere ogni volta al database
        private static List<Mostro> ListaMostri { get; set; } = new List<Mostro>();

        /// livello massimo dei mostri da cui estrarre
        private static int LivelloMostri { get; set; }

        /// Lista di tutti i livelli
        private static List<Livello> ListaLivelli { get; set; } = new List<Livello>();

        // salvo la lista dei livelli per non riaccedere ogni volta al database

        /// Massimo livello raggiungibile da un eroe
        private static int MaxLivello { get; set; }

        /// Punti accumulati necessari per vincere
        private static int Vittoria { get; set; } = 200;

        // Salvo Giocatore che fa il login
        // e Eroe e Mostro che si scontrano, aggiornandoli, per evitare di passarli in input tra i vari metodi
        private static Giocatore Giocatore { get; set; }
        private static Eroe Eroe { get; set; }
        private static Mostro Mostro { get; set; }

        /// watch per misurare il tempo di gioco degli eroi
        private static Stopwatch watch { get; set; } = new Stopwatch();

        //Provider dei servizi
        private static ServiceProvider serviceProvider { get;} = DIConfiguration.Configurazione();
        private static MostroService mostroService { get; } = serviceProvider.GetService<MostroService>();
        private static GiocatoreService giocatoreService { get; } = serviceProvider.GetService<GiocatoreService>();
        private static LivelloService livelloService { get; } = serviceProvider.GetService<LivelloService>();
        private static EroeService eroeService { get; } = serviceProvider.GetService<EroeService>();






        #region Partita

        /// <summary>
        /// Metodo per ottenere il massimo livello possibile con i punti accumulati minori o uguali a quelli dell'eroe
        /// </summary>
        /// <returns></returns>
        private static Livello GetMaxLivelloWithPuntiAccumulatiLessOrEqualThan()
        {
            Livello newLivello=Eroe.Livello;

            for(int i=Eroe.Livello.ID; i<ListaLivelli.Count(); i++)
            {
                if (ListaLivelli[i].PuntiAccumulati <= Eroe.PuntiAccumulati)
                {
                    newLivello = ListaLivelli[i];
                }
                else return newLivello;
            }
            return newLivello;
        }

        /// <summary>
        /// Metodo per controllare se l'eroe sale o meno di livello
        /// e se vince la partita
        /// </summary>
        private static void CheckLivello()
        {
            if (Eroe.Livello.ID != MaxLivello) // se ho già raggiunto il maxLivello non controllo
            {
                if (Eroe.PuntiAccumulati > Eroe.Livello.PuntiAccumulati) // se i punti accumulati non superano quelli del livello attuale non controllo
                {

                    Livello nuovoLivello = GetMaxLivelloWithPuntiAccumulatiLessOrEqualThan();
                    if (Eroe.Livello.ID < nuovoLivello.ID) // se i punti accumulati superano quelli del mio livello ma non superano quelli di un livello successivo non cambio livello
                    {

                        Eroe.Livello = nuovoLivello; // nuovo livello
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Congratulazioni! Sei passato al livello: " + Eroe.Livello.ID + "\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        Eroe.PuntiVita = Eroe.Livello.PuntiVita; // aggiorno punti vita
                    }
                }
            
            }

            if (Eroe.PuntiAccumulati >= Vittoria) // controllo vittoria
            {


                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Congratulazioni! Hai vinto con " + Eroe.Nome+ "\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;

                Eroe.HasWon = true; 
            }

        }

        /// <summary>
        /// Attacco, metodo utilizzato sia per attacco Mostro-Eroe che per Eroe-Mostro
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>true se vince p1, false se vince p2</returns>
        private static bool Attacco(Personaggio p1, Personaggio p2)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            if (p1 is Eroe)
                Console.WriteLine("Turno del tuo eroe " + p1.Nome);
            else Console.WriteLine("Turno del mostro " + p1.Nome);

            Console.WriteLine(p1.Nome + " ha inflitto con "+ p1.Arma.Nome +" "+ p1.Arma.PuntiDanno + " punti vita a " + p2.Nome+ " su " + p2.PuntiVita);

            p2.PuntiVita -= p1.Arma.PuntiDanno;
            Console.WriteLine(p2.Nome + " rimane con " + p2.PuntiVita + " punti vita\n");

            Console.ForegroundColor = ConsoleColor.Cyan;

            if (p2.PuntiVita <= 0) return true;

            return false;

        }
        /// <summary>
        /// Metodo per Tentare la fuga
        /// </summary>
        /// <returns>true se la fuga è riuscita, false se non è riuscita</returns>
        private static bool TentaFuga()
        {
            Random random = new Random();
            int numeroCasuale = random.Next(0,2); // 0 o 1
            if (numeroCasuale == 0) //Fallita
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fuga Fallita\n");
                Console.ForegroundColor = ConsoleColor.Cyan;

                return false;
            }
            else // Fuga riuscita
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Fuga Riuscita! Purtroppo hai perso: " + (Mostro.Livello.ID * 5).ToString()+ " punti\n" );
                Console.ForegroundColor = ConsoleColor.Cyan;

                Eroe.PuntiAccumulati -= Mostro.Livello.ID * 5;
                return true;
            }

        }

        /// <summary>
        /// Metodo chiamato da Partita per lo scontro
        /// </summary>
        /// <returns></returns>
        private static bool Scontro()
        {
            bool vinta;
            string input;
            do
            {
                
                vinta=false;
                bool flag;
                do
                {

                    flag = true;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Tenti la fuga o attacchi il mostro? Digita: T per tentare la fuga, A per attaccare");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    input = Console.ReadLine();
                    if (input != "T" && input != "A"){
                        Console.WriteLine("Comando non valido"); 
                        flag = false;
                        }

                } while (flag==false);

                if (input == "T" && TentaFuga())
                {
                    
                    return true;
                }
                    
                        
                 if( input == "A") // se hai Tentato la fuga e non è andata a buon fine o hai attaccato ->Attacca
                {

                    vinta = Attacco(Eroe, Mostro);

                }


                if (vinta) // se hai attaccato e vinto, aggiorna i punti accumulati e finisci turno
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Hai ucciso il mostro\n");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Eroe.PuntiAccumulati += Mostro.Livello.ID * 10;
                    if(Eroe.HasWon == false) CheckLivello();

                    return true;
                }
                if (Attacco(Mostro, Eroe)) // Il mostro attacca e se return value= true hai perso
                {
                    
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hai perso! Il tuo eroe "+ Eroe.Nome +" è morto\n");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    eroeService.DeleteEroe(Eroe);
                    return false;
                }
                //Se nessuno dei due ha sconfitto l'altro continua
            } while (true);

        }

        private static void EstraiMostro()
        {


            int livello = Eroe.Livello.ID;
            if (livello < LivelloMostri) // Se ho cambiato eroe ed ha un livello inferiore, elimino dalla lista i mostri con livello superiore
            {


                int n = ListaMostri.Count();
                for (int i = 0; i < n; i++)
                {
                    if (ListaMostri[i].Livello.ID > livello)
                    {
                        ListaMostri.RemoveAt(i);
                        n--;
                        i--;
                    }
                }



                LivelloMostri = livello;


            }
            else if (livello > LivelloMostri && LivelloMostri != 0) // Se passo di livello superiore, aggiungo quelli dal livello precedente all'attuale
            {
                IEnumerable<Mostro> mostriAdd = mostroService.GetAllMostriWithLevelBetween(LivelloMostri, livello);
                if (mostriAdd == null) return; // Eccezione generata

                ListaMostri.AddRange(mostriAdd.ToList());

                LivelloMostri = livello;

            }
            else if (LivelloMostri == 0) //Se è la prima volta che gioco estraggo tutti i mostri con livello inferiore del mio
            {
                IEnumerable<Mostro> mostriTemp = mostroService.GetAllMostriWithLevelLessOrEqualThan(livello);
                if (mostriTemp == null) return; // Eccezione generata

                ListaMostri = mostriTemp.ToList();
                LivelloMostri = livello;
            }
            // se uguale non modifico la lista


            Random random = new Random();
            int numeroCasuale = random.Next(0, ListaMostri.Count());

            Mostro = ListaMostri[numeroCasuale];

        }


        // ALTERNATIVA (senza salvare la lista dei mostri)
        //private static void EstraiMostro2()
        //{
                        
        //     IEnumerable<Mostro> mostri = mostroService.GetAllMostriWithLevelLessOrEqualThan(Eroe.Livello.ID);
        //        if (mostri == null) return; // Eccezione generata


        //    Random random = new Random();
        //    int numeroCasuale = random.Next(0, mostri.Count());

        //    Mostro = mostri.ElementAt(numeroCasuale);

        //}




        /// <summary>
        /// Metodo per gestire la partita. 
        /// </summary>
        private static void Partita()
        {

       
            do
            {
                //Estrazione Mostro
                EstraiMostro();
                if (Mostro == null) return; //Eccezione generata

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Dovrai combattere contro " + Mostro.Nome +"\n");
                Console.ForegroundColor = ConsoleColor.Cyan;


                //Scontro
                if (!Scontro()) // se lo scontro non è andato a buon fine
                {
                    Mostro.PuntiVita = Mostro.Livello.PuntiVita; // resetto i punti vita di mostro
                    watch.Reset();
                    return;
                }
                Mostro.PuntiVita = Mostro.Livello.PuntiVita; // resetto i punti vita di mostro


                bool flag;
                do
                {
                    
                    string input;
                    Console.WriteLine("Digita: \nM per tornare al Menu senza salvare \nC per combattere contro un altro mostro \nS per salvare i progressi e tornare al Menu");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "M":
                            watch.Reset();
                            return;
                        case "C":
                            flag = true;
                            break;
                        case "S":
                            watch.Stop();
                            Eroe.TempoDiGioco += (int)watch.ElapsedMilliseconds; // Aggiungo i tempi di gioco dell'eroe
                            watch.Reset();
                            eroeService.UpdateEroe(Eroe);
                            return;
                        default:
                            Console.WriteLine("Comando non valido\n");
                            flag = false;
                            break;

                    };
                } while (flag == false);

            } while (true);

        }

        #endregion


        /// <summary>
        /// Statistiche dei Giocatori. 
        /// Se un giocatore non è admin può vedere le proprie statistiche
        /// Se il giocatore è un admin può vedere le statistiche di tutti i giocatori o scegliere un giocatore di cui vedere le statistiche
        /// se ha scelto "Vedere le classifiche globali " in Menu Admin, altrimenti se ha scelto "Vedere le proprie statistiche" nel Menu Giocatore
        /// vedrà solo le proprie come un giocatore non admin
        /// </summary>
        /// <param name="globali">booleano: true per vedere le classifiche globali o false per vedere solo quelle del proprio giocatore</param>
        private static void Statistiche(bool globali=false)
        {
            List<Eroe> eroi = new List<Eroe>();

            if (globali == true)
            {
                string input;
                bool flag;
                do
                {
                    flag = true;
                    Console.WriteLine("Digita T per vedere le statistiche di tutti i giocatori , altrimenti S per scegliere un Giocatore di cui vuoi vedere le statistiche");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "T":
                            IEnumerable<Eroe> allEroiTemp = eroeService.GetAllEroi();
                            if (allEroiTemp == null) return; // Eccezione generata
                            eroi = allEroiTemp.ToList();
                            break;
                        case "S":
                            IEnumerable<Giocatore> giocatori = giocatoreService.GetAllGiocatori();
                            if (giocatori == null) return; // Eccezione generata

                            int index;
                            for (int i = 0; i < giocatori.Count(); i++)
                            {
                                Console.WriteLine("Premi " + i.ToString() + " per visualizzare le statistiche di " + giocatori.ElementAt(i).Nome);
                            }
                            while (int.TryParse(Console.ReadLine(), out index) == false || index < 0 || index >= giocatori.Count())
                            {
                                Console.WriteLine("Digita nuovamente\n");
                            }
                            IEnumerable<Eroe> eroiTempAllGioc = eroeService.GetAllEroiByGiocatoreID(giocatori.ElementAt(index).ID);
                            if (eroiTempAllGioc == null) return; // Eccezione generata
                            eroi = eroiTempAllGioc.ToList();
                            break;
                        default:
                            Console.WriteLine("Comando non valido\n");
                            flag = false;
                            break;

                    };
                } while (flag == false);



            }
            else
            {
                IEnumerable<Eroe> eroiTempGioc = eroeService.GetAllEroiByGiocatoreID(Giocatore.ID);
                if (eroiTempGioc == null) return; // Eccezione generata
                eroi = eroiTempGioc.ToList();
            }



            if (eroi.Count() == 0) Console.WriteLine("Non ci sono eroi\n");
            else
            {
                eroi.Sort(delegate (Eroe c1, Eroe c2) { return c2.PuntiAccumulati.CompareTo(c1.PuntiAccumulati); });

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Classifica con Punteggio Accumulato Eroe decrescente:");
                Console.ForegroundColor = ConsoleColor.Cyan;

                foreach (Eroe it in eroi)
                {
                    Console.WriteLine("Giocatore: " + it.Giocatore.Nome + " - Eroe: " + it.DisplayStat());
                }
                Console.WriteLine("");

            }
        }


        #region Menu

        /// <summary>
        /// Menu Admin con possibilità di creare un nuovo mostro, vedere le classifiche globali, andare al menu giocatore o uscire
        /// </summary>
        /// <returns>Restituisce un booleano: true se vuole andare al menu giocatore, false se vuole uscire </returns>
        private static bool MenuADMIN()
        {

            string input;
            bool flag;
            do
            {


                Console.WriteLine("Digita:\nC per creare un nuovo mostro \n" +
                    "V per vedere le classifiche globali \nG per andare al Menu Giocatore \nU per uscire");
                input = Console.ReadLine();
                switch (input)
                {
                    case "C":
                        mostroService.CreateMostro();
                        flag = false;
                        break;
                    case "V":
                        Statistiche(true);
                        flag = false;
                        break;
                    case "G": // True: Continua con Menu User
                        return true;
                    case "U":
                        return false; // false: Esci
                    default:
                        Console.WriteLine("Comando non valido\n");
                        flag = false;
                        break;

                };
            } while (flag == false);

            return false;
        }

        /// Enum creato per gestire i valori di ritorno di MenuUser
        private enum MenuReturn
        {
            IniziaPartita,
            MenuAdmin,
            Esci
        }

        /// <summary>
        /// Menu Giocatore con possibilità di creare un nuovo eroe, eliminarlo, sceglierne uno con cui giocare, vedere le statistiche personali o uscire
        /// Se il giocatore è admin ha anche la possibilità di tornare al Menu Admin
        /// </summary>
        /// <returns></returns>
        private static MenuReturn MenuUser()
        {

            string input;
            bool flag;
            do
            {
                flag = true;
                if(Giocatore.IsAdmin==false)
                    Console.WriteLine("Digita: \nC per creare un nuovo Eroe \nE per eliminare un eroe \nS per scegliere un Eroe con cui giocare \nV per vedere le tue statistiche \nU per uscire");
                else  Console.WriteLine("Digita: \nC per creare un nuovo Eroe \nE per eliminare  un eroe \nS per scegliere un Eroe con cui giocare \nV per vedere le tue statistiche \nU per uscire \nA per tornare al Menu Admin");
                input = Console.ReadLine();
                switch (input)
                {
                    case "C":
                        Eroe= eroeService.CreateEroe(Giocatore);
                        if (Eroe != null) // se nessuna eccezione generata
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;

                            Console.WriteLine("Partita avviata con " + Eroe.Nome+ "\n");
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            watch.Reset();
                            watch.Start();
                            return MenuReturn.IniziaPartita; // True: Inizia partita
                        }
                        else flag = false; // Eccezione generata
                        break;
                    case "E":
                        flag = false; //Continua con Menu
                        eroeService.DeleteEroeByGiocatoreID(Giocatore.ID);
                        break;
                    case "V":
                        flag = false; //Continua con Menu
                        Statistiche();
                        break;
                    case "S":
                        Eroe = eroeService.GetEroeFromAllEroiByGiocatoreID(Giocatore.ID);
                        if (Eroe != null) // se nessuna eccezione generata
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("Partita avviata con " + Eroe.Nome + "\n");
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            watch.Reset();
                            watch.Start();
                            return MenuReturn.IniziaPartita; // True: Inizia partita
                        }
                        else flag = false; // Non avevi Eroe da scegliere o eccezione generata
                        break;
                    case "U":
                        return MenuReturn.Esci; // False: Esci
                    case "A":
                        if (Giocatore.IsAdmin == false)
                        {
                            Console.WriteLine("Comando non valido\n");
                            flag = false;
                            break;
                        }else return MenuReturn.MenuAdmin;
                    default:
                        Console.WriteLine("Comando non valido\n");
                        flag = false;
                        break;

                };
            } while (flag == false);

            return MenuReturn.Esci;

        }

        #endregion
        /// <summary>
        /// Metodo privato per gestire il LogIn
        /// Ottiene il giocatore da database o crea un nuovo giocatore se non presente nel database
        /// </summary>
        private static void LogIn()
        {

            // Scegli Giocatore o crea
            string input;

            Console.WriteLine("Scrivi il tuo UserName!");
            input = Console.ReadLine();
            input = input.Trim();
            Giocatore = giocatoreService.GetGiocatoreByNome(input);
            if (Giocatore == null) return; // Eccezione generata in GetGiocatoreByNome


            if (Giocatore.ID == 0) // Giocatore non presente nel database-> creazione
            {
                Console.WriteLine("Creazione nuovo Giocatore ...\n");
                Giocatore = giocatoreService.CreateGiocatore(input);
                if (Giocatore == null) return; // Eccezione generata in CreateGiocatore

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.Cyan;


                Console.WriteLine("Benvenuto/a " + Giocatore.Nome + "\n");

                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else // Giocatore già presente nel database
            {

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.Cyan;


                if(Giocatore.IsAdmin==false)  Console.WriteLine("Bentornato/a " + Giocatore.Nome + "\n");
                else Console.WriteLine("Bentornato Admin " + Giocatore.Nome + "\n");

                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            



        }


        /// <summary>
        /// Metodo pubblico da chiamare da program per avviare il gioco
        /// </summary>
        public static void MostriVSEroiMain( )
        {
            
            Console.ForegroundColor = ConsoleColor.Cyan;

            //LogIn 
            //Controllo che non siano generate eccezioni

            string input="";
            do
            {
                LogIn();
                
                if (Giocatore == null) // Eccezione generata
                {
                    do
                    {
                        Console.WriteLine("C'è stato un problema. Digita R per riprovare o U per uscire");
                        input = Console.ReadLine();
                        if(input != "R" && input != "U")
                        {
                            Console.WriteLine("Comando non valido");
                        }

                    } while (input!="R" && input!="U");

                    if (input == "U")
                    {
                        Console.ResetColor();
                        return;
                    }
                }
            } while (input == "R");


            // Salvo i possibili livelli in ListaLivelli
            // e controllo non siano generate eccezioni 

            input = "";
            IEnumerable<Livello> livelliTemp;
            do {
                livelliTemp = livelloService.GetAllLivelli();

                if (livelliTemp == null)  // Eccezione generata
                {
                    do
                    {
                        Console.WriteLine("C'è stato un problema per ottenere i livelli per il gioco. Digita R per riprovare o U per uscire");
                        input = Console.ReadLine();
                        if (input != "R" && input != "U")
                        {
                            Console.WriteLine("Comando non valido");
                        }

                    } while (input != "R" && input != "U");

                    if (input == "U")
                    {
                        Console.ResetColor();
                        return;
                    }
 
                }

            } while (input == "R");



            ListaLivelli = livelliTemp.ToList();

            ListaLivelli.Sort(delegate (Livello l1, Livello l2) { return l1.ID.CompareTo(l2.ID); }); // ordino per sicurezza, potrebbero essere stati inseriti nel database non in ordine
            MaxLivello = ListaLivelli.Count();

            // Se Admin parte da MenuADMIN con possibilità di andare al Menu giocatore e da lì tornare al MenuADMIN
            // controllo che non siano generate eccezioni
            if (Giocatore.IsAdmin == true && MenuADMIN() || Giocatore.IsAdmin == false)
            {
                bool cont;
                do
                {
                    cont = false;
                    switch (MenuUser())
                    {
                        case MenuReturn.Esci:
                            Console.ResetColor();
                            return;
                        case MenuReturn.IniziaPartita:
                            Partita();
                            cont = true;
                            break;
                        case MenuReturn.MenuAdmin:
                            if (MenuADMIN())
                                cont = true;
                            else
                            {
                                Console.ResetColor();
                                return;
                            }
                            break;
                    }
                } while (cont == true);

            }
            Console.ResetColor();



        }





    }
}
