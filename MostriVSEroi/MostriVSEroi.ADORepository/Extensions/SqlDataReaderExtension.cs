using MostriVSEroi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MostriVSEroi.ADORepository.Extensions
{


    public static class SqlDataReaderExtension
    {
        public static Livello ToLivello(this SqlDataReader reader)
        {
            return new Livello()
            {

                ID = (int)reader["IDLivello"],
                PuntiVita = (int)reader["PuntiVitaLivello"],
                PuntiAccumulati = (int)reader["PuntiAccumulatiLivello"],

            };
        }

        public static Classe ToClasse(this SqlDataReader reader)
        {
            return new Classe()
            {

                ID = (int)reader["IDClasse"],
                Nome = reader["NomeClasse"].ToString(),
                IsEroe = (bool)reader["IsEroe"]

            };
        }

        public static Arma ToArma(this SqlDataReader reader)
        {
            return new Arma()
            {

                ID = (int)reader["IDArma"],
                Nome = reader["NomeArma"].ToString(),
                PuntiDanno = (int)reader["PuntiDanno"],
                Classe=reader.ToClasse()
            };
        }

        public static Giocatore ToGiocatore(this SqlDataReader reader)
        {
            return new Giocatore()
            {

                ID = (int)reader["IDGiocatore"],
                Nome = reader["NomeGiocatore"].ToString(),
                IsAdmin = (bool)reader["IsAdmin"],

            };
        }




        public static Eroe ToEroe(this SqlDataReader reader)
        {
            return new Eroe(
             (int)reader["IDEroe"],
             reader["NomeEroe"].ToString(),
             reader.ToArma(),
             reader.ToLivello(),
             reader.ToGiocatore(),
             (int)reader["PuntiVitaEroe"],
             (int)reader["PuntiAccumulatiEroe"],
             (int)reader["TempoDiGioco"],
             (bool) reader["HasWon"]);



        }





        public static Mostro ToMostro(this SqlDataReader reader)
        {
            return new Mostro((int)reader["IDMostro"], reader["NomeMostro"].ToString(), reader.ToArma(), reader.ToLivello());

        }





    }
}
