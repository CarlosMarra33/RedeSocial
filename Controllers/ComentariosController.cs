using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetodeBloco.Controllers
{
    public class ComentariosController : Controller
    {

        // tambem tenho q redirecionar para a lista de comentarios
        public ActionResult Comentar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Comentar(Models.Comentario comentario ,Models.Usuario usuario, int id)
        {
            Repositorio.ComentarioRepositorio reposComentarios = new Repositorio.ComentarioRepositorio();
            usuario = (Models.Usuario)Session["Objeto"];

            reposComentarios.AddComentario(comentario, id, usuario);

            return RedirectToAction("Feed", "Postagens");
        }

        public ActionResult ListaComentarios(int id)
        {
            Repositorio.ComentarioRepositorio reposComentarios = new Repositorio.ComentarioRepositorio();
            List<Models.Comentario> comentarios = new List<Models.Comentario>();
            foreach (var p in reposComentarios.ListaComentarios(id))
            {
                comentarios.Add(p);
            }
                return View(comentarios);
        }
        // Tenho q redirecionar para  a lista de comentarios
        public ActionResult Deletarcomentario(int id)
        {
            Repositorio.ComentarioRepositorio reposComentario = new Repositorio.ComentarioRepositorio();
            reposComentario.DeletarComentario(id);

            return RedirectToAction("Feed", "Postagens");
        }
    }
}