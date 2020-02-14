using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetodeBloco.Repositorio
{
    public class CurtidaRepositorio
    {
        public bool VerificaCurtida(Models.Postagens postagens, Models.Usuario usuario)
        {
            bool verifica = false;
            var conexaoString = Dao.Caminho();

            using (SqlConnection conexao = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Curtida WHERE PostId = '{postagens.PostId}' AND UsuarioId = '{usuario.UsuarioId}'", conexao);

                try
                {
                    conexao.Open();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            verifica = true;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conexao.Close();
                }
                return verifica;
            }

        }

        public void Curtir(Models.Postagens postagens, Models.Usuario usuario)
        {
            string conexaoString = Dao.Caminho();
            using (SqlConnection conexao = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Curtida(UsuarioId, PostId) Values(@UsuarioId, @PostId)", conexao);
                command.Parameters.AddWithValue("@UsuarioId", usuario.UsuarioId);
                command.Parameters.AddWithValue("@PostId", postagens.PostId);
                try
                {
                    conexao.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conexao.Close();
                }
            };
        }

        public void DeletarCurtida(Models.Postagens postagens, Models.Usuario usuario)
        {
            string conexaoString = Dao.Caminho();
            using (SqlConnection conexao = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand($"DELETE FROM Curtida WHERE PostId = '{postagens.PostId}' AND UsuarioId = {usuario.UsuarioId}", conexao);
                try
                {
                    conexao.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }


    }
}