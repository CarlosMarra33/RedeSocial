using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjetodeBloco.Repositorio
{
    public class PostagenRepositorio
    {
        //tenho q colocar no banco o nome do usuario q postou, nao posso pegar esse nome da session quando vou fazer o feed 
        //é so colocar o nome q vem da session pro banco de dados, tenho q fazer isso até amanha rápido.
        public void Postar(Models.Postagens postagem, HttpPostedFileBase fileBase, Models.Usuario usuario)
        {
            var conexaoString = Dao.Caminho();

            using (var conexao = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO POSTAGENS (Conteudo, UsuarioId, Data, Midia, Nome) Values(@Conteudo,@UsuarioId, @Data, @Midia, @Nome)", conexao);
                command.Parameters.AddWithValue("@Conteudo", postagem.Conteudo);
                command.Parameters.AddWithValue("@UsuarioId", usuario.UsuarioId);
                command.Parameters.AddWithValue("@Data", DateTime.Now);
                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                if (fileBase != null)
                {
                    string Imagens = Path.Combine(HttpContext.Current.Server.MapPath("~/Imagens"), fileBase.FileName);
                    fileBase.SaveAs(Imagens);
                    command.Parameters.AddWithValue("Midia", fileBase.FileName);
                }
                else
                {
                    command.Parameters.AddWithValue("Midia", "");
                }
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

        public  IEnumerable<Models.Postagens> ListaPostagem(Models.Usuario usuario)
        {
            List<Models.Postagens> listaPost = new List<Models.Postagens>();

            var conexaoString = Dao.Caminho();

            using (var conexao = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM AMIZADE, POSTAGENS WHERE (OrigemId = @DestinoId OR DestinoId = @DestinoId) AND (Aceito = @Aceito)", conexao);
                command.Parameters.AddWithValue("@DestinoId", usuario.UsuarioId);
                command.Parameters.AddWithValue("Aceito", true);
                try
                {
                    conexao.Open();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (reader.Read())
                        {
                           

                            using (SqlConnection connection = new SqlConnection(conexaoString))
                            {
                                SqlCommand command2 = new SqlCommand($"SELECT * FROM POSTAGENS WHERE ", connection);
                                try
                                {
                                    connection.Open();
                                    while (reader.Read())
                                    {
                                        Models.Amigo user2 = new Models.Amigo();
                                        user2.OrigemId = (int)reader["OrigemId"];
                                        int UsuarioSession = usuario.UsuarioId;
                                        int IdAmigo = 0;

                                        if (user2.OrigemId != usuario.UsuarioId)
                                        {
                                            user2.OrigemId = (int)reader["OrigemId"];
                                            IdAmigo = user2.OrigemId;
                                        }
                                        else
                                        {
                                            user2.DestinoId = (int)reader["DestinoId"];
                                            IdAmigo = user2.DestinoId;
                                        }

                                        Models.Postagens posts = new Models.Postagens();
                                        posts.UsuarioId = (int)reader["UsuarioId"];
                                        //if (posts.UsuarioId == usuario.UsuarioId)
                                        //{
                                        //    posts.PostId = (int)reader["PostId"];
                                        //    posts.UsuarioId = (int)reader["UsuarioId"];
                                        //    posts.Data = (DateTime)reader["Data"];
                                        //    posts.Midia = reader["Midia"].ToString();
                                        //    posts.Nome = reader["Nome"].ToString(); ;
                                        //    posts.Conteudo = reader["Conteudo"].ToString();

                                        //    listaPost.Add(posts);
                                        //}

                                        if (posts.UsuarioId == IdAmigo)
                                        {
                                            posts.PostId = (int)reader["PostId"];
                                            posts.UsuarioId = (int)reader["UsuarioId"];
                                            posts.Data = (DateTime)reader["Data"];
                                            posts.Midia = reader["Midia"].ToString();
                                            posts.Nome = reader["Nome"].ToString(); ;
                                            posts.Conteudo = reader["Conteudo"].ToString();

                                            listaPost.Add(posts);
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
                        }
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conexao.Close();
                }
                return listaPost;
            }
        }

        public static int ContaLike(Models.Postagens postagens)
        {
            var conexaoString = Dao.Caminho();
            Models.Postagens Algo = new Models.Postagens();

            using (var conexao = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand($"SELECT Count(CurtidaId) FROM Curtida WHERE PostId =  '{postagens.PostId}'", conexao);
                try
                {
                    conexao.Open();
                    Algo.NumCurtidas = (int)command.ExecuteScalar();
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
            return Algo.NumCurtidas;
        }

            public IEnumerable<Models.Postagens> MeuFeed(Models.Usuario usuario)
        {
            List<Models.Postagens> listaPost = new List<Models.Postagens>();

            var conexaoString = Dao.Caminho();

            using (var conexao = new SqlConnection(conexaoString))
            {

                SqlCommand command = new SqlCommand("SELECT * FROM POSTAGENS WHERE UsuarioId = @UsuarioId", conexao);
                command.Parameters.AddWithValue("@UsuarioId", usuario.UsuarioId);
                try
                {
                    conexao.Open();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (reader.Read())
                        {
                            Models.Postagens posts = new Models.Postagens();

                            posts.PostId = (int)reader["PostId"];
                            posts.UsuarioId = (int)reader["UsuarioId"];
                            posts.Data = (DateTime)reader["Data"];
                            posts.Midia = reader["Midia"].ToString();
                            posts.Nome = reader["Nome"].ToString();
                            posts.Conteudo = reader["Conteudo"].ToString();



                            listaPost.Add(posts);
                        }
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conexao.Close();
                }

                return listaPost;
            }
        }

        public static Models.Postagens SelecionarPost(int id)
        {
            string conexaoString = Dao.Caminho();
            Models.Postagens posts = new Models.Postagens();

            using (SqlConnection conexao = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM POSTAGENS WHERE PostId = {id}", conexao);
                try
                {
                    conexao.Open();
                    using(var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            posts.PostId = (int)reader["PostId"];
                            posts.UsuarioId = (int)reader["UsuarioId"];
                            posts.Data = (DateTime)reader["Data"];
                            posts.Midia = reader["Midia"].ToString();
                            posts.Conteudo = reader["Conteudo"].ToString();
                            posts.Nome = reader["Nome"].ToString();
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
                return posts;

            }

        }

        public void EditarPostagem(Models.Postagens postagens,  HttpPostedFileBase fileBase)
        {
            var conectionString = Dao.Caminho();
            using(SqlConnection conexao = new SqlConnection(conectionString))

            {

                SqlCommand command = new SqlCommand($"UPDATE POSTAGENS SET Conteudo = '{postagens.Conteudo}', Midia = '{fileBase.FileName}', Data = '{DateTime.Now}' WHERE PostId = '{postagens.PostId}'", conexao);
                string Imagens = Path.Combine(HttpContext.Current.Server.MapPath("~/Imagens"), fileBase.FileName);

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

        public void DeletarPostagem(int id)
        {
            var conexaoString = Dao.Caminho();

            using(SqlConnection conexao = new SqlConnection(conexaoString))
            {
                SqlCommand command = new SqlCommand($"DELETE FROM POSTAGENS WHERE PostId = '{id}'", conexao);
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

        public void Compartilhar(int id, Models.Usuario usuario)
        {
            var connectionString = Dao.Caminho();
            Models.Postagens postagens = new Models.Postagens();
            postagens = SelecionarPost(id);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO POSTAGENS (UsuarioId, Conteudo, Data, Midia, Nome) VALUES (@UsuarioId, @Conteudo, @Data, @Midia, @Nome)", connection);
                command.Parameters.AddWithValue("@UsuarioId", usuario.UsuarioId);
                command.Parameters.AddWithValue("@Conteudo", postagens.Conteudo);
                command.Parameters.AddWithValue("@Data", DateTime.Now);
                command.Parameters.AddWithValue("@Midia", postagens.Midia);
                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }finally
                {
                    connection.Close();
                }
            }
        }
    }
}