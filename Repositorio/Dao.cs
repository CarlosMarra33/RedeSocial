using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetodeBloco.Repositorio
{
    public class Dao
    {
        public static string Caminho()
        {
            string ConexaoString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Carlos\Desktop\ProjetodeBloco\ProjetodeBloco\ProjetodeBloco\App_Data\BDProjeto.mdf;Integrated Security=True";
            return ConexaoString;
        }
    }
}