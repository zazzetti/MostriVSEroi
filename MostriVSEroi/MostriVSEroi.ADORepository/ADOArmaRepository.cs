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
    /// Classe implementazione dell'interfaccia <c>IArmaRepository</c>
    /// </summary>
    public class ADOArmaRepository : IArmaRepository
    {
        const string connectionString = @"Persist Security Info=False; Integrated Security=True; Initial Catalog= MostriVSEroi; Server=.\SQLEXPRESS";

        /// Metodo al momento non implementato perché non necessario per il gioco
        public bool Create(Arma obj)
        {
            throw new NotImplementedException();
        }

        /// Metodo al momento non implementato perché non necessario per il gioco
        public bool Delete(Arma obj)
        {
            throw new NotImplementedException();
        }

        /// Metodo al momento non implementato perché non necessario per il gioco
        public IEnumerable<Arma> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implementazione del metodo <c>GetArmiByClasseID</c> 
        /// Metodo per ottenere tutte le armi associate a una determinata classe
        /// </summary>
        /// <param name="classeID">Id della classe di cui voglio ottenere le armi, di tipo intero</param>
        /// <returns>Restituisce un IEnumerable di oggetti di tipo Arma se tutto va a buon fine, null (valore di default) se viene generata un'eccezione</returns>
        public IEnumerable<Arma> GetArmiByClasseID(int classeID)
        {
            List<Arma> armi = new List<Arma>();

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
                // Per istanziare gli oggetti di tipo Arma necessito anche delle informazioni relative a Classe,
                // quindi utilizzo la vista VistaArma con tutte le informazioni
                command.CommandText = "SELECT * FROM VistaArma WHERE IDClasse=@IDClasse";
                command.Parameters.AddWithValue("@IDClasse", classeID);

                //Esecuzione comando
                SqlDataReader reader = command.ExecuteReader();

                //Lettura dei dati
                while (reader.Read())
                {
                    armi.Add(reader.ToArma());
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
            return armi;
        }


        /// Metodo al momento non implementato perché non necessario per il gioco
        public Arma GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        /// Metodo al momento non implementato perché non necessario per il gioco
        public bool Update(Arma obj)
        {
            throw new NotImplementedException();
        }
    }
}
