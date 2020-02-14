using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetodeBloco.Repositorio
{
    public class AmigoRepositorio
    {
        public static List<Models.Usuario> ListarUsuarios()
        {
            List<Models.Usuario> amigoList = new List<Models.Usuario>();
            var conexaoString = Dao.Caminho();

            using (SqlConnection connection = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM USUARIO1  ", connection);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Models.Usuario usuario = new Models.Usuario();
                            usuario.UsuarioId = (int)reader["UsuarioId"];
                            usuario.Nome = reader["Nome"].ToString();
                            usuario.Email = reader["Email"].ToString();
                            usuario.DataDeNascimento = (DateTime)reader["DataDeNascimento"];

                            if (VerificaAmigo(usuario) != true)
                            {
                                amigoList.Add(usuario);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return amigoList;
        }

        public void EnviarSolicitacao(int id, Models.Usuario usuario)
        {
            var conexaoString = Dao.Caminho();

            using (SqlConnection connection = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand($"INSERT INTO AMIZADE (OrigemId, DestinoId) Values(@OrigemId, @DestinoId)", connection);
                command.Parameters.AddWithValue("OrigemId", usuario.UsuarioId);
                command.Parameters.AddWithValue("DestinoId", id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<Models.Usuario> ListaDeSolicitacao(Models.Usuario usuario)
        {
            var conexaoString = Dao.Caminho();
            List<Models.Usuario> ListaAmigo = new List<Models.Usuario>();

            using (SqlConnection connection = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM AMIZADE WHERE Aceito = '{false}' AND DestinoId = {usuario.UsuarioId}", connection);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {

                            Models.Amigo amigo = new Models.Amigo();
                            amigo.OrigemId = (int)reader["OrigemId"];

                            ListaAmigo.Add(UsuarioRepositorio.SelecionarUsuario(amigo.OrigemId));


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
                return ListaAmigo;

            }
        }

        public void AceitarAmigo(int id)
        {
            var conexaoString = Dao.Caminho();

            using (SqlConnection connection = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand($"UPDATE AMIZADE SET Aceito = '{true}' WHERE OrigemId = '{id}' ", connection);

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

        public static bool VerificaAmigo(Models.Usuario usuario)
        {
            var conexaoString = Dao.Caminho();
            bool verifica = false;
            using (SqlConnection connection = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM AMIZADE WHERE DestinoId = '{usuario.UsuarioId}'", connection);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
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
                    connection.Close();
                }
                return verifica;
            }
        }

        public static List<Models.Usuario> AmigosLista(Models.Usuario usuario)
        {
            List<Models.Usuario> usuariosList = new List<Models.Usuario>();

            var connectionString = Dao.Caminho();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM AMIZADE WHERE (OrigemId = @DestinoId OR DestinoId = @DestinoId) AND (Aceito = @Aceito)", connection);
                command.Parameters.AddWithValue("@DestinoId", usuario.UsuarioId);
                command.Parameters.AddWithValue("@Aceito", true);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Models.Amigo user = new Models.Amigo();
                            user.OrigemId = (int)reader["OrigemId"];
                            if (user.OrigemId != usuario.UsuarioId)
                            {
                                usuariosList.Add(UsuarioRepositorio.SelecionarUsuario(user.OrigemId));
                            }
                            else
                            {
                                user.DestinoId = (int)reader["DestinoId"];
                                usuariosList.Add(UsuarioRepositorio.SelecionarUsuario(user.DestinoId));
                            }
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
                return usuariosList;
            }
        }

        public static void DeleteAmigo(int id, Models.Usuario usuario)
        {
            var connectionString = Dao.Caminho();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM AMIZADE WHERE (DestinoId = @DestinoId AND OrigemId = @OrigemId) OR (DestinoId = @OrigemId AND OrigemId = @DestinoId)", connection);
                command.Parameters.AddWithValue("@DestinoId", id);
                command.Parameters.AddWithValue("@OrigemId", usuario.UsuarioId);

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