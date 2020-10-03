using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppEventos.Models;
using AppEventos.Entidades;
using System.Web;
using AppEventos.Reglas;
using System.Web.Mvc;

namespace AppEventos.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Perfil()
        {
            if (Session["UsuarioLogeado"] == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }
            ViewBag.Usuario = Session["UsuarioLogeado"];
            return View();
        }
        public ActionResult EditarPerfil()
        {
            if (Session["UsuarioLogeado"] == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditarPerfil(string Nombre, string Apellido, string Descripcion) {


            return null;
        }
    }
}
