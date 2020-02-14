using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetodeBloco.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult inicio()
        {
            if(Session["Objeto"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Default");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("inicio", "Home");
        }
    }
}