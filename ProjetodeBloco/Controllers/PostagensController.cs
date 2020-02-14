using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetodeBloco.Controllers
{
    public class PostagensController : Controller
    {
        public ActionResult CriarPostagens()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CriarPostagens(Models.Postagens postagem, HttpPostedFileBase fileBase, Models.Usuario usuario)
        {

            if (Session["Objeto"] != null)
            {
                usuario = (Models.Usuario)Session["Objeto"];
                Repositorio.PostagenRepositorio repositorio = new Repositorio.PostagenRepositorio();

                repositorio.Postar(postagem, fileBase, usuario);
                return RedirectToAction("Feed", "Postagens");
            }
            else
            {
                return RedirectToAction("Logar", "Default");
            }
        }

        public ActionResult Feed()
        {
            Models.Usuario usuario = (Models.Usuario)Session["Objeto"];
            Repositorio.PostagenRepositorio repositorio = new Repositorio.PostagenRepositorio();
            List<Models.Postagens> posts = new List<Models.Postagens>();
            
            foreach (Models.Postagens i in repositorio.ListaPostagem(usuario))
            {
                i.NumCurtidas = Repositorio.PostagenRepositorio.ContaLike(i);
                posts.Add(i);
            }
            return View(posts.OrderByDescending(p => p.Data).ToList());
        }

        public ActionResult MeuFeed()
        {
            Models.Usuario usuario = (Models.Usuario)Session["Objeto"];
            Repositorio.PostagenRepositorio repositorio = new Repositorio.PostagenRepositorio();
            List<Models.Postagens> posts = new List<Models.Postagens>();

            foreach (Models.Postagens i in repositorio.MeuFeed(usuario))
            {
                posts.Add(i);
            }
            return View(posts);
        }

        public ActionResult EditarPostagem(int id)
        {
            return View(Repositorio.PostagenRepositorio.SelecionarPost(id));
        }

        [HttpPost]
        public ActionResult EditarPostagem(Models.Postagens postagens, HttpPostedFileBase fileBase)
        {
            Repositorio.PostagenRepositorio repos = new Repositorio.PostagenRepositorio();
            repos.EditarPostagem(postagens, fileBase);
            return RedirectToAction("MeuFeed", "Postagens");
        }

        public ActionResult DeletarPostagens(int id)
        {
            return View(Repositorio.PostagenRepositorio.SelecionarPost(id));
        }

        [HttpPost]
        public ActionResult DeletarPostagens(int id, Models.Postagens postagens)
        {
            Repositorio.PostagenRepositorio repos = new Repositorio.PostagenRepositorio();
            repos.DeletarPostagem(id);
            return RedirectToAction("MeuFeed", "Postagens");
        }

        public ActionResult CompartilharPost(int PostId)
        {
            Repositorio.PostagenRepositorio reposPost = new Repositorio.PostagenRepositorio();
            Models.Usuario usuario = new Models.Usuario();
            usuario = (Models.Usuario)Session["Objeto"];

            reposPost.Compartilhar(PostId, usuario);

            return RedirectToAction("Feed", "Postagens");
        }
    }
}