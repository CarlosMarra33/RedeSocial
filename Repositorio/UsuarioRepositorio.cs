using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ProjetodeBloco.Repositorio
{
    public class UsuarioRepositorio
    {
        public bool ValidaEmail(Models.Usuario usuario)
        {
            bool validEmail = false;
            var conexaoString = Dao.Caminho();

            using (SqlConnection conexao = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM USUARIO1 WHERE Email = {usuario.Email}", conexao);
                try
                {
                    if (usuario.Senha == usuario.ConfirmaSenha)
                    {
                        conexao.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                validEmail = true;
                            }
                        }
                    }
                    else
                    {
                        validEmail = true;
                        throw new Exception("Senhas não são iguais");

                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    conexao.Close();
                }
                return validEmail;
            }
        }

        public void CadastrarAmigo(Models.Usuario usuario)
        {
            var conexaoString = Dao.Caminho();


            using (SqlConnection conexao = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO USUARIO1 (Nome, Email, Senha, DataDeNascimento, Genero) Values(@Nome, @Email, @Senha, @DataDeNascimento, @Genero )", conexao);
                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Senha", usuario.Senha);
                command.Parameters.AddWithValue("@DataDeNascimento", usuario.DataDeNascimento);
                command.Parameters.AddWithValue("@Genero", usuario.Genero);
                try
                {
                    conexao.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception erro) { throw erro; }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public Models.Usuario FazerLogin(Models.Usuario usuario)
        {
            var conexaoString = Dao.Caminho();

            using (SqlConnection conexao = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM USUARIO1 WHERE Email = '{usuario.Email}'", conexao);
                try
                {
                    conexao.Open();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        if (reader.HasRows)
                        {
                            reader.Read();
                            if (usuario.Senha == reader["Senha"].ToString())
                            {
                                usuario.Nome = reader["Nome"].ToString();
                                usuario.Email = reader["Email"].ToString();
                                usuario.UsuarioId = (int)reader["UsuarioId"];
                                usuario.DataDeNascimento = (DateTime)reader["DataDeNascimento"];
                                //usuario.Genero = (char)reader["Genero"];
                            }
                            else
                            {
                                usuario = null;
                            }
                        }
                        else
                        {
                            usuario = null;
                        }
                    }
                }
                catch { }
                finally
                {
                    conexao.Close();
                }
                return usuario;
            }
        }

        public static Models.Usuario SelecionarUsuario(int id)
        {
            var conexaoString = Dao.Caminho();
            Models.Usuario usuario = new Models.Usuario();

            using (SqlConnection conexao = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM USUARIO1 WHERE UsuarioId = '{id}'", conexao);
                try
                {
                    conexao.Open();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        if (reader.Read())
                        {
                            usuario.UsuarioId = (int)reader["UsuarioId"];
                            usuario.Nome = reader["Nome"].ToString();
                            usuario.Email = reader["Email"].ToString();
                            usuario.DataDeNascimento = (DateTime)reader["DataDeNascimento"];

                        }
                    }
                }
                catch { }
                finally
                {
                    conexao.Close();
                }
                return usuario;
            }

        }

        //public Models.Usuario VerPerfil(Models.Usuario usuario)
        //{
        //    var conmectionString = Dao.Caminho();
        //    Models.Usuario usuario2 = new Models.Usuario();

        //    using (SqlConnection connection = new SqlConnection(conmectionString))
        //    {
        //        SqlCommand command = new SqlCommand($"SELECT * FROM USUARIO1 WHERE UsuarioId = '{usuario.UsuarioId}'", connection);
        //        try
        //        {
        //            connection.Open();
        //            using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
        //            {
        //                usuario2.Nome = reader["Nome"].ToString();
        //                usuario2.Email = reader["Email"].ToString();
        //                usuario2.Genero = (char)reader["Genero"];
        //                usuario2.Senha = reader["Senha"].ToString();
        //                usuario2.DataDeNascimento = (DateTime)reader["DataDeNascimento"];
        //            }

        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //    return usuario2;

        //}

        public void EditarPerfil(Models.Usuario usuario)
        {
            var conmectionString = Dao.Caminho();

            using (SqlConnection connection = new SqlConnection(conmectionString))
            {
                SqlCommand command = new SqlCommand($"UPDATE USUARIO1 SET Nome = '{usuario.Nome}', Email = '{usuario.Email}', Senha = '{usuario.Senha}', Genero = '{usuario.Genero}' WHERE UsuarioId = '{usuario.UsuarioId}'", connection);
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

        public void DeletarPerfil(int id)
        {
            var conmectionString = Dao.Caminho();
            Models.Usuario usuario2 = new Models.Usuario();

            using (SqlConnection connection = new SqlConnection(conmectionString))
            {
                SqlCommand command = new SqlCommand($"DELETE FROM USUARIO1 WHERE UsuarioId = '{id}'", connection);
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