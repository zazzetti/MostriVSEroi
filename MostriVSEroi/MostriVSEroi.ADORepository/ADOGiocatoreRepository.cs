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
    /// Classe implementazione dell'interfaccia <c>IGiocatoreRepository</c>
    /// </summary>
    public class ADOGiocatoreRepository: IGiocatoreRepository
    {
        const string connectionString = @"Persist Security Info=False; Integrated Security=True; Initial Catalog= MostriVSEroi; Server=.\SQLEXPRESS";


        /// <summary>
        /// Implementazione del metodo <c>Create</c> per creare un nuovo giocatore nel database dato un oggetto di tipo Giocatore
        /// </summary>
        /// <param name="obj">oggetto di tipo Giocatore</param>
        /// <returns>Restituisce un booleano: true se è andata a buon fine la creazione, false se viene generata un'eccezione </returns>
        public bool Create(Giocatore obj)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                    try { 
                    //Aprire connessione
                    connection.Open();

                    //Creare comando
                    SqlCommand insertCommand = new SqlCommand();
                    insertCommand.Connection = connection;
                    insertCommand.CommandType = System.Data.CommandType.Text;
                    insertCommand.CommandText = "INSERT INTO Giocatore VALUES(@Nome,@IsAdmin)";


                    //Creare param
                    insertCommand.Parameters.AddWithValue("@Nome", obj.Nome);
                    insertCommand.Parameters.AddWithValue("@IsAdmin", obj.IsAdmin);

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
        public bool Delete(Giocatore obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implementazione del metodo <c>GetAll</c> per ottenere tutti i giocatori
        /// </summary>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Giocatore se tutto va a buon fine, null (valore di default) se viene generata un'eccezione</returns>
        public IEnumerable<Giocatore> GetAll()
        {

            List<Giocatore> giocatori = new List<Giocatore>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                    try { 
                    //Aprire la connessione
                    connection.Open();

                    //Comando
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT ID as IDGiocatore, Nome as NomeGiocatore, IsAdmin FROM Giocatore";

                    //Esecuzione comando
                    SqlDataReader reader = command.ExecuteReader();

                    //Lettura dei dati
                    while (reader.Read())
                    {
                        giocatori.Add(reader.ToGiocatore());
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
            return giocatori;
        }

        /// Metodo al momento non implementato perché non necessario per il gioco
        public Giocatore GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implementazione del metodo <c>GetByNome</c> per ottenere un giocatore attraverso il nome (univoco)
        /// </summary>
        /// <param name="nome">Nome del giocatore, di tipo stringa</param>
        /// <returns>Restituisce un oggetto di tipo Giocatore se tutto va a buon fine (se non trovato nel database avrà ID=0 essendo inizializzato con new),
        /// altrimenti null (valore di default) se viene generata un'eccezione </returns>
        public Giocatore GetByNome(string nome)
        {
            Giocatore giocatore = new Giocatore();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try { 
                    //Aprire la connessione
                    connection.Open();

                    //Comando
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT  ID as IDGiocatore, Nome as NomeGiocatore, IsAdmin FROM Giocatore WHERE Nome=@Nome";

                    command.Parameters.AddWithValue("@Nome", nome);

                    //Esecuzione comando
                    SqlDataReader reader = command.ExecuteReader();

                    //Lettura dei dati
                    if (reader.Read())
                        giocatore = reader.ToGiocatore();
                    


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
            return giocatore;
        }

        /// Metodo al momento non implementato perché non necessario per il gioco
        public bool Update(Giocatore obj)
        {
            throw new NotImplementedException();
        }
    }
}
