using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetodeBloco.Controllers
{
    public class CurtidaController : Controller
    {
        public ActionResult Curtida(int postId, Models.Usuario usuario, Models.Postagens postagens)
        {
            postagens = Repositorio.PostagenRepositorio.SelecionarPost(postId);
            usuario = (Models.Usuario)Session["Objeto"];
            bool verifica = false;
            Repositorio.CurtidaRepositorio reposCurtida = new Repositorio.CurtidaRepositorio();


            verifica = reposCurtida.VerificaCurtida(postagens, usuario);
            if (verifica != true)
            {
                reposCurtida.Curtir(postagens, usuario);

            }
            else
            {
                Models.Curtida curtida = new Models.Curtida();
                reposCurtida.DeletarCurtida(postagens, usuario);

            }
            return RedirectToAction("Feed", "Postagens");
        }

    }
}