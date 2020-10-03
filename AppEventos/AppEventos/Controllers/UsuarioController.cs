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

            Usuario user = RNUsuario.Buscar(SessionHelper.UsuarioLogueado.Id);
            user.Nombre = Nombre;
            user.Apellido = Apellido;
            user.Descripcion = Descripcion;
            var rsp = user.Save();
            if (rsp)
            {
                ViewBag.Success = "Guardado con éxito";
            }
            else {
                ViewBag.Error = "Surgio un error al intentar guardar los cambios.";
            }
            return View();
        }
    }
}
