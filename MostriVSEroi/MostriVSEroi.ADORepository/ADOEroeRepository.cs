using MostriVSEroi.Core.Entities;
using MostriVSEroi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using MostriVSEroi.ADORepository.Extensions;


namespace MostriVSEroi.ADORepository
{
    /// <summary>
    /// Classe implementazione dell'interfaccia <c>IEroeRepository</c>
    /// </summary>
    public class ADOEroeRepository : IEroeRepository
    {
        const string connectionString = @"Persist Security Info=False; Integrated Security=True; Initial Catalog= MostriVSEroi; Server=.\SQLEXPRESS";

        /// <summary>
        /// Implementazione del metodo <c>Create</c> per creare un nuovo eroe nel database dato un oggetto di tipo Eroe
        /// </summary>
        /// <param name="obj">oggetto di tipo Eroe</param>
        /// <returns>Restituisce un booleano: true se è andata a buon fine la creazione, false se viene generata un'eccezione</returns>
        public bool Create(Eroe obj)
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
                    insertCommand.CommandText = "INSERT INTO Eroe VALUES(@Nome,@IDGiocatore, @IDClasse, @IDArma, @IDLivello, @PuntiVita, @PuntiAccumulati, @TempodiGioco, @HasWon)";


                    //Creare param
                    insertCommand.Parameters.AddWithValue("@Nome", obj.Nome);
                    insertCommand.Parameters.AddWithValue("@IDGiocatore", obj.Giocatore.ID);
                    insertCommand.Parameters.AddWithValue("@IDClasse", obj.Classe.ID);
                    insertCommand.Parameters.AddWithValue("@IDArma", obj.Arma.ID);
                    insertCommand.Parameters.AddWithValue("@IDLivello", obj.Livello.ID);
                    insertCommand.Parameters.AddWithValue("@PuntiVita", obj.PuntiVita);
                    insertCommand.Parameters.AddWithValue("@PuntiAccumulati", obj.PuntiAccumulati);
                    insertCommand.Parameters.AddWithValue("@TempodiGioco", obj.TempoDiGioco);
                    insertCommand.Parameters.AddWithValue("@HasWon", obj.HasWon);

                    //Esecuzione dei comandi

                    insertCommand.ExecuteNonQuery();
                    connection.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    connection.Close();
                    return false;
                }

                return true;
            }

        }

        /// <summary>
        /// Implementazione del metodo <c>Delete</c> per eliminare dal database un eroe dato un oggetto di tipo Eroe
        /// Viene eliminato il record con ID corrispondente all'ID dell'oggetto passato (ID univoco)
        /// </summary>
        /// <param name="obj">oggetto di tipo Eroe</param>
        /// <returns>Restituisce un booleano: true se è andata a buon fine l'eliminazione, false se viene generata un'eccezione </returns>
        public bool Delete(Eroe obj)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    //Aprire connessione
                    connection.Open();

                    //Creare comando
                    SqlCommand deleteCommand = new SqlCommand();
                    deleteCommand.Connection = connection;
                    deleteCommand.CommandType = System.Data.CommandType.Text;
                    deleteCommand.CommandText = "DELETE FROM Eroe WHERE ID=@Id ";

                 
                    //Creare il parametro
                    deleteCommand.Parameters.AddWithValue("@Id", obj.ID);

                    //Esecuzione dei comandi

                    deleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    connection.Close();
                    return false;
                }

                
                return true;

            }
        }

        /// <summary>
        /// Implementazione del metodo <c>GetMaxIDEroeByGiocatoreID</c> per ottenere il massimo ID degli eroi di un giocatore, cioè l'ID dell'ultimo eroe creato
        /// metodo utile per restituire l'eroe appena creato con l'ID generato nel database
        /// </summary>
        /// <param name="giocatoreID">ID del giocatore, di tipo intero</param>
        /// <returns>Restituisce l'ID cercato se tutto è andato a buon fine, 0 se viene generata un'eccezione</returns>
        public int GetMaxIDEroeByGiocatoreID(int giocatoreID)
        {

            int id;
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
                    command.CommandText = "SELECT MAX(ID) AS ID FROM Eroe WHERE IDGiocatore = @IDGiocatore";

                    command.Parameters.AddWithValue("@IDGiocatore", giocatoreID);



                    //Esecuzione comando
                    SqlDataReader reader = command.ExecuteReader();

                    //Lettura dei dati
                    reader.Read();
                    id = (int) reader["ID"];
                    

                    //Chiusura connessione
                    reader.Close();
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    connection.Close();
                    return -1;
                }

            }
            return id;
        }

        /// <summary>
        /// Implementazione del metodo <c>GetAll</c> per ottenere tutti gli eroi
        /// </summary>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Eroe se tutto va a buon fine, null (valore di default) se viene generata un'eccezione</returns>
        public IEnumerable<Eroe> GetAll()
        {
            List<Eroe> eroi = new List<Eroe>();

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
                    // Per istanziare gli oggetti di tipo Eroe necessito delle informazioni relative anche a Giocatore, Livello, Arma e Classe,
                    // quindi utilizzo la vista VistaEroe con tutte le informazioni
                    command.CommandText = "SELECT * FROM VistaEroe";
                    
                    //Esecuzione comando
                    SqlDataReader reader = command.ExecuteReader();

                    //Lettura dei dati
                    while (reader.Read())
                    {
                        eroi.Add(reader.ToEroe());
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
            return eroi;
        }


        /// <summary>
        /// Implementazione del metodo <c>GetAllByGiocatoreID</c> per ottenere tutti gli eroi di un giocatore
        /// </summary>
        /// <param name="giocatoreID">ID del giocatore, di tipo intero</param>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Eroe se tutto va a buon fine, null (valore di default) se viene generata un'eccezione</returns>
        public IEnumerable<Eroe> GetAllByGiocatoreID(int giocatoreID)
        {
            List<Eroe> eroi = new List<Eroe>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try { 
                    //Aprire la connessione
                    connection.Open();

                    //Comando
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    // Per istanziare gli oggetti di tipo Eroe necessito delle informazioni relative anche a Giocatore, Livello, Arma e Classe,
                    // quindi utilizzo la vista VistaEroe con tutte le informazioni
                    command.CommandText = "SELECT * FROM VistaEroe WHERE IDGiocatore=@IDGiocatore";
                    command.Parameters.AddWithValue("@IDGiocatore", giocatoreID);




                    //Esecuzione comando
                    SqlDataReader reader = command.ExecuteReader();

                    //Lettura dei dati
                    while (reader.Read())
                    {
                        eroi.Add(reader.ToEroe());
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
            return eroi;
        }

        /// Metodo al momento non implementato perché non necessario per il gioco
        public Eroe GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implementazione del metodo <c>Update</c> per aggiornare un eroe nel database dato un oggetto di tipo Eroe
        /// </summary>
        /// <param name="obj">oggetto di tipo Eroe</param>
        /// <returns>Restituisce un booleano: true se è andata a buon fine l'update, false se viene generata un'eccezione </returns>
        public bool Update(Eroe obj)
        {
          
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {

                    //Aprire connessione
                    connection.Open();

                    //Creare comando
                    SqlCommand updateCommand = new SqlCommand();
                    updateCommand.Connection = connection;
                    updateCommand.CommandType = System.Data.CommandType.Text;
                    updateCommand.CommandText = "UPDATE Eroe SET IDLivello=@IDLivello, PuntiVita=@PuntiVita, PuntiAccumulati=@PuntiAccumulati, TempoDiGioco=@TempoDiGioco, HasWon=@HasWon WHERE ID=@Id";


                    //Creare param
                    updateCommand.Parameters.AddWithValue("@IDLivello", obj.Livello.ID);
                    updateCommand.Parameters.AddWithValue("@PuntiVita", obj.PuntiVita);
                    updateCommand.Parameters.AddWithValue("@PuntiAccumulati", obj.PuntiAccumulati);
                    updateCommand.Parameters.AddWithValue("@TempoDiGioco", obj.TempoDiGioco);
                    updateCommand.Parameters.AddWithValue("@HasWon", obj.HasWon);
                    updateCommand.Parameters.AddWithValue("@Id", obj.ID);

                    //Esecuzione dei comandi
                    updateCommand.ExecuteNonQuery();

                    connection.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    connection.Close(); 
                    return false;
                }
                
                return true;
            }
        }
    }
}
