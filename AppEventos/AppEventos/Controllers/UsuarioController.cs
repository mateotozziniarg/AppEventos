using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppEventos.Models;
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
                //Agregar viewbag de que la session se vencio
                return View("~/Views/Login/Login.cshtml");
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditarPerfil(string Nombre, string Apellido, string Descripcion) {

            usuario user = RNUsuario.Buscar(SessionHelper.UsuarioLogueado.Id);
            user.Nombre = Nombre;
            user.Apellido = Apellido;
            user.Descripcion = Descripcion;
            var rsp = user.Save();
            if (rsp)
            {
                ViewBag.Success = "Guardado con éxito";
                SessionHelper.UsuarioLogueado = user;
            }
            else {
                ViewBag.Error = "Surgio un error al intentar guardar los cambios.";
            }
            return View();
        }

        public ActionResult CambiarPass()
        {
            if (Session["UsuarioLogeado"] == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CambiarPass(String Password, String NewPassword, String NewPasswordConfirm)
        {
            if (Session["UsuarioLogeado"] == null)
            {   
                //Agregar viewbag de que la session se vencio
                return View("~/Views/Login/Login.cshtml");
            }
            if (!Password.Equals(SessionHelper.UsuarioLogueado.Password))
            {
                ViewBag.Error = "Contraseña actual incorrecta";
                return View();
            }
            if (!NewPassword.Equals(NewPasswordConfirm))
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            else if(NewPassword.Equals(SessionHelper.UsuarioLogueado.Password))
            {
                ViewBag.Error = "No puede cambiar su contraseña por la misma.";
                return View();
            }

            usuario user = RNUsuario.Buscar(SessionHelper.UsuarioLogueado.Id);

            if (user.ChangePassword(NewPassword)) {
                ViewBag.Success = "Cambio de contraseña exitoso.";
                return View();
            }

            ViewBag.Error = "Surgio un error.";
            return View();
        }

        public ActionResult AltaVendedor()
        {

            if (Session["UsuarioLogeado"] == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AltaVendedor(String descripcion)
        {
            if (Session["UsuarioLogeado"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var user = RNUsuario.Buscar(SessionHelper.UsuarioLogueado.Id);

            if (descripcion == "") {
                ViewBag.Error = "Por favor incluya una descripción.";
                return View();
            }
            user.Descripcion = descripcion;
            user.Vendedor = true;
            user.Save();

            return View();
        }

        //public ActionResult AltaVendedor(String descripcion)
        //{
        //    if (SessionHelper.UsuarioLogueado != null) {
        //        var user = RNUsuario.Buscar(SessionHelper.UsuarioLogueado.Id);
        //    }
        //    return View();
        //}


    }
}
