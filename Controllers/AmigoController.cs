using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetodeBloco.Controllers
{
    public class AmigoController : Controller
    {
        public ActionResult PesquisarAmigo()
        {
            List<Models.Usuario> usuarios = new List<Models.Usuario>();
            return View(usuarios);
        }

        [HttpPost]
        public ActionResult PesquisarAmigo(string pesquisa)
        {
            List<Models.Usuario> amigos = new List<Models.Usuario>();

            foreach (Models.Usuario p in Repositorio.AmigoRepositorio.ListarUsuarios())
            {
                if (p.Nome.Contains(pesquisa))
                {
                    amigos.Add(p);

                }
            }
            return View(amigos);
        }

        public ActionResult EnviarSolicitacao(int id, Models.Usuario usuario)
        {
            Repositorio.AmigoRepositorio reposAmig = new Repositorio.AmigoRepositorio();
            usuario = (Models.Usuario)Session["Objeto"];
            reposAmig.EnviarSolicitacao(id, usuario);

            return RedirectToAction("PesquisarAmigo", "Amigo");
        }

        public ActionResult ListarPedidos(Models.Usuario usuario)
        {
            Repositorio.AmigoRepositorio reposAmig = new Repositorio.AmigoRepositorio();
            usuario = (Models.Usuario)Session["Objeto"];

            return View(reposAmig.ListaDeSolicitacao(usuario));
        }

        public ActionResult AceitarPedido(int id)
        {
            Repositorio.AmigoRepositorio reposAmigo = new Repositorio.AmigoRepositorio();
            reposAmigo.AceitarAmigo(id);

            return RedirectToAction("ListarPedidos", "Amigo");
        }

        public ActionResult ListaAmigos(Models.Usuario usuario)
        {
            usuario = (Models.Usuario)Session["Objeto"];
            List<Models.Usuario> Lista = new List<Models.Usuario>();
            foreach (var p in Repositorio.AmigoRepositorio.AmigosLista(usuario))
            {
                Lista.Add(p);
            }
            return View(Lista);
        }

        public ActionResult DeletarAmigo(int DelId)
        {
            Models.Usuario usuario = new Models.Usuario();
            usuario = (Models.Usuario)Session["Objeto"];
            Repositorio.AmigoRepositorio.DeleteAmigo(DelId, usuario);

            return RedirectToAction("ListaAmigos", "Amigo");
        }
    }
}