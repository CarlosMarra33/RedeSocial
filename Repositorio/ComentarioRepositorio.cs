using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetodeBloco.Repositorio
{
    public class ComentarioRepositorio
    {
        public void AddComentario(Models.Comentario comentario, int id, Models.Usuario usuario)
        {
            var connectionString = Dao.Caminho();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO COMENTARIO (Texto, PostId, UsuarioId) Values (@Texto, @PostId, @UsuarioId)", connection);
                command.Parameters.AddWithValue("@Texto", comentario.Texto);
                command.Parameters.AddWithValue("@PostId", id);
                command.Parameters.AddWithValue("@UsuarioId", usuario.UsuarioId);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Models.Comentario> ListaComentarios(int id)
        {
            var connectionString = Dao.Caminho();
            List<Models.Comentario> comentariosList = new List<Models.Comentario>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM COMENTARIO WHERE PostId = '{id}'", connection);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Models.Comentario comentario = new Models.Comentario();
                            comentario.Texto = reader["Texto"].ToString();
                            comentario.Nome = UsuarioRepositorio.SelecionarUsuario((int)reader["UsuarioId"]).Nome;
                            comentariosList.Add(comentario);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return comentariosList;
        }

        public void DeletarComentario(int id)
        {
            var connectionString = Dao.Caminho();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand($"DELETE FROM COMENTARIO WHERE ComentarioId = '{id}'", connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}