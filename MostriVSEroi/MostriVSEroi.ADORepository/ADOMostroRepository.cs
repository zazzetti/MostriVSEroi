using MostriVSEroi.ADORepository.Extensions;
using MostriVSEroi.Core.Entities;
using MostriVSEroi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MostriVSEroi.ADORepository
{
    /// <summary>
    /// Classe implementazione dell'interfaccia <c>IMostroRepository</c>
    /// </summary>
    public class ADOMostroRepository : IMostroRepository
    {
        const string connectionString = @"Persist Security Info=False; Integrated Security=True; Initial Catalog= MostriVSEroi; Server=.\SQLEXPRESS";

        /// <summary>
        /// Implementazione del metodo <c>Create</c> per creare un nuovo mostro nel database dato un oggetto di tipo Mostro
        /// </summary>
        /// <param name="obj">oggetto di tipo Mostro</param>
        /// <returns>Restituisce un booleano: true se è andata a buon fine la creazione, false se viene generata un'eccezione</returns>
        public bool Create(Mostro obj)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    //Aprire connessione
                    connection.Open();

                    //Creare comando
                    SqlCommand insertCommand = new SqlCommand();
                    insertCommand.Connection = connection;
                    insertCommand.CommandType = System.Data.CommandType.Text;
                    insertCommand.CommandText = "INSERT INTO Mostro VALUES(@Nome, @IDClasse, @IDArma, @IDLivello)";


                    //Creare param
                    insertCommand.Parameters.AddWithValue("@Nome", obj.Nome);
                    insertCommand.Parameters.AddWithValue("@IDClasse", obj.Classe.ID);
                    insertCommand.Parameters.AddWithValue("@IDArma", obj.Arma.ID);
                    insertCommand.Parameters.AddWithValue("@IDLivello", obj.Livello.ID);


                    //Esecuzione dei comandi

                    insertCommand.ExecuteNonQuery();
                    connection.Close();
                    return true;


                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    connection.Close();
                    return false;
                }
              
            }
        }


        /// Metodo al momento non implementato perché non necessario per il gioco
        public bool Delete(Mostro obj)
        {
            throw new NotImplementedException();
        }

        /// Metodo al momento non implementato perché non necessario per il gioco
        public IEnumerable<Mostro> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implementazione del metodo <c>GetAllWithLevelLessOrEqualThan</c> per ottenere tutti i mostri di livello inferiore o uguale a quello dato in input
        /// </summary>
        /// <param name="levelIDLimit">Id del livello massimo dei mostri che voglio ottenere, di tipo intero</param>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Mostro se tutto va a buon fine, null (valore di default) se viene generata un'eccezione</returns>

        public IEnumerable<Mostro> GetAllWithLevelLessOrEqualThan(int levelIDLimit)
        {
            List<Mostro> mostri = new List<Mostro>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //Aprire la connessione
                    connection.Open();

                    //Comando
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM VistaMostro WHERE IDLivello<=@IDLivello";

                    command.Parameters.AddWithValue("@IDLivello", levelIDLimit);

                    //Esecuzione comando
                    SqlDataReader reader = command.ExecuteReader();

                    //Lettura dei dati
                    while (reader.Read())
                    {
                        mostri.Add(reader.ToMostro());
                    }

                    //Chiusura connessione
                    reader.Close();
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    connection.Close();
                    return default;
                }

            }
            return mostri;
        }

        /// <summary>
        /// Implementazione del metodo <c>GetAllWithLevelBetween</c> per ottenere tutti i mostri di livello compreso tra due livelli (limite minimo escluso)
        /// metodo utile per integrare la lista dei mostri qualora ci sia stato un passaggio di livello o un cambio eroe con livello maggiore
        /// </summary>
        /// <param name="levelIDMin">ID del livello minimo dei mostri che voglio ottenere, di tipo intero</param>
        /// <param name="levelIDMax">ID del livello massimo dei mostri che voglio ottenere, di tipo intero</param>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Mostro se tutto va a buon fine, null (valore di default) se viene generata un'eccezione</returns>
        public IEnumerable<Mostro> GetAllWithLevelBetween(int levelIDMin, int levelIDMax)
        {
            List<Mostro> mostri = new List<Mostro>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //Aprire la connessione
                    connection.Open();

                    //Comando
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM VistaMostro WHERE IDLivello<=@LivelloIDMax AND IDLivello>@LivelloIDMin ";

                    command.Parameters.AddWithValue("@LivelloIDMax", levelIDMax);
                    command.Parameters.AddWithValue("@LivelloIDMin", levelIDMin);
                    //Esecuzione comando
                    SqlDataReader reader = command.ExecuteReader();

                    //Lettura dei dati
                    while (reader.Read())
                    {
                        mostri.Add(reader.ToMostro());
                    }

                    //Chiusura connessione
                    reader.Close();
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    connection.Close();
                    return default;
                }

            }
            return mostri;
        }


        /// Metodo al momento non implementato perché non necessario per il gioco
        public Mostro GetByID(int ID)
        {
            throw new NotImplementedException();
        }


        /// Metodo al momento non implementato perché non necessario per il gioco
        public bool Update(Mostro obj)
        {
            throw new NotImplementedException();
        }
    }
}
