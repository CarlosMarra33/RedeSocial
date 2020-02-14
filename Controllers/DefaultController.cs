using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using ProjetodeBloco.Repositorio;


namespace ProjetodeBloco.Controllers
{
    public class DefaultController : Controller
    {
        private UsuarioRepositorio repos = new UsuarioRepositorio();


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Models.Usuario usuario)
        {
            bool validEmail = false;
            validEmail = repos.ValidaEmail(usuario);
            try
            {
                if (validEmail == false)
                {
                    repos = new UsuarioRepositorio();
                    repos.CadastrarAmigo(usuario);
                    return RedirectToAction("Index");
                }
                else {
                    //ModelState.AddModelError()
                    return View();
                }
            }
            catch (Exception) { }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Usuario usuario)
        {
            Models.Usuario pessoa = repos.FazerLogin(usuario);

            if (pessoa != null) {
                Session["Objeto"] = pessoa;
                ViewData.Model = usuario.Nome;
                return RedirectToAction("inicio", "Home");
            }
            else
            {
                ModelState.AddModelError("Email", "Email não é válido");
                ModelState.AddModelError("Senha", "Senha não é válida");
                return View();
            }
        }

        public ActionResult VerPerfil()
        {
            Models.Usuario usuario = new Models.Usuario();
            usuario = (Models.Usuario)Session["Objeto"];
            //repos.VerPerfil(usuario);
            return View(usuario);
        }

        public ActionResult EditarPerfil(int id)
        {
            return View(UsuarioRepositorio.SelecionarUsuario(id));
        }

        [HttpPost]
        public ActionResult EditarPerfil(Models.Usuario usuario)
        {
            repos.EditarPerfil(usuario);
            return View();
        }

        public ActionResult DeletarPerfil()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeletarPerfil(int id)
        {
            repos.DeletarPerfil(id);

            return RedirectToAction("Login", "Default");
        }
    }
    
}
