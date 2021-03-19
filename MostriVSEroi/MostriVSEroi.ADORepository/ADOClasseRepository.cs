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
    /// Classe implementazione dell'interfaccia <c>IClasseRepository</c>
    /// </summary>
    public class ADOClasseRepository : IClasseRepository
    {

        const string connectionString = @"Persist Security Info=False; Integrated Security=True; Initial Catalog= MostriVSEroi; Server=.\SQLEXPRESS";

        /// Metodo al momento non implementato perché non necessario per il gioco
        public bool Create(Classe obj)
        {
            throw new NotImplementedException();
        }


        /// Metodo al momento non implementato perché non necessario per il gioco
        public bool Delete(Classe obj)
        {
            throw new NotImplementedException();
        }

        /// Metodo al momento non implementato perché non necessario per il gioco
        public IEnumerable<Classe> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implementazione del metodo <c>GetAllClassiByIsEroe</c> 
        /// </summary>
        /// <param name="isEroe">Parametro di tipo booleano: true se voglio ottenere le classi degli eroi,
        /// false se voglio ottenere quelle dei mostri</param>
        /// <returns>Restituisce una IEnumerable di oggetti di tipo Classe se tutto va a buon fine, null (valore di default) se viene generata un'eccezione</returns>
        public IEnumerable<Classe> GetAllClassiByIsEroe(bool isEroe)
        {
            List<Classe> classi = new List<Classe>();

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
                    command.CommandText = "SELECT ID as IDClasse, Nome as NomeClasse, IsEroe FROM Classe where IsEroe=@isEroe";

                    command.Parameters.AddWithValue("@isEroe", isEroe);

                    //Esecuzione comando
                    SqlDataReader reader = command.ExecuteReader();

                    //Lettura dei dati
                    while (reader.Read())
                    {
                        classi.Add(reader.ToClasse());
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
            return classi;
        }

        /// Metodo al momento non implementato perché non necessario per il gioco
        public Classe GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        /// Metodo al momento non implementato perché non necessario per il gioco
        public bool Update(Classe obj)
        {
            throw new NotImplementedException();
        }
    }
}
