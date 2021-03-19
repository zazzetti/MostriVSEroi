using MostriVSEroi.Core.Entities;
using MostriVSEroi.ADORepository.Extensions;
using MostriVSEroi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MostriVSEroi.ADORepository
{
    /// <summary>
    /// Classe implementazione dell'interfaccia <c>ILivelloRepository</c>
    /// </summary>
    public class ADOLivelloRepository : ILivelloRepository
    {
        const string connectionString = @"Persist Security Info=False; Integrated Security=True; Initial Catalog= MostriVSEroi; Server=.\SQLEXPRESS";

        /// Metodo al momento non implementato perché non necessario per il gioco
        public bool Create(Livello obj)
        {
            throw new NotImplementedException();
        }

        /// Metodo al momento non implementato perché non necessario per il gioco
        public bool Delete(Livello obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implementazione del metodo <c>GetAll</c> per ottenere tutti i livelli
        /// </summary>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Livello se tutto va a buon fine, null (valore di default) se viene generata un'eccezione</returns>
        public IEnumerable<Livello> GetAll()
        {
            List<Livello> livelli = new List<Livello>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try { 
                    //Aprire la connessione
                    connection.Open();

                    //Comando
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT ID as IDLivello, PuntiVita as PuntiVitaLivello, PuntiAccumulati as PuntiAccumulatiLivello FROM Livello";

                    //Esecuzione comando
                    SqlDataReader reader = command.ExecuteReader();

                    //Lettura dei dati
                    while (reader.Read())
                    {
                        livelli.Add(reader.ToLivello());
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
            return livelli;
        }

        /// <summary>
        /// Implementazione del metodo <c>GetByID</c> per ottenere il livello mediante l'ID
        /// </summary>
        /// <param name="ID">ID del livello da ottenere, di tipo intero</param>
        /// <returns>Restituisce un oggetto di tipo Livello se tutto va a buon fine (se non trovato nel database avrà ID=0 essendo inizializzato con new),
        /// altrimenti null (valore di default) se viene generata un'eccezione </returns>
        public Livello GetByID(int ID)
        {
            Livello livello = new Livello();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try { 
                    //Aprire la connessione
                    connection.Open();

                    //Comando
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT ID as IDLivello, PuntiVita as PuntiVitaLivello, PuntiAccumulati as PuntiAccumulatiLivello FROM Livello WHERE ID=@Id";

                    command.Parameters.AddWithValue("@Id", ID);

                    //Esecuzione comando
                    SqlDataReader reader = command.ExecuteReader();

                    //Lettura dei dati
                    if (reader.Read())
                        livello = reader.ToLivello();
                    else livello = default;


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
            return livello;
        }




        /// Metodo al momento non implementato perché non necessario per il gioco
        public bool Update(Livello obj)
        {
            throw new NotImplementedException();
        }
    }
}
