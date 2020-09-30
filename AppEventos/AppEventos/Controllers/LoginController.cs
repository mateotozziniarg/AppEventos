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
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            UsuarioLogin usuarioNuevo = new UsuarioLogin();
            usuarioNuevo.Username = Username;
            usuarioNuevo.Password = Password;
            Usuario userLoged = RNUsuario.Login(usuarioNuevo);
            if (userLoged == null)
            {
                ViewBag.Error = "No se ha logrado logearse.";
                return View();
            }
            else
            {
                Session["UsuarioLogeado"] = userLoged;
                ViewBag.Usuario = Session["UsuarioLogeado"];
                return View("~/Views/Home/Index.cshtml");
            }
        }
        public ActionResult Logout() {
            Session["UsuarioLogeado"] = null;
            return View("~/Views/Home/Index.cshtml");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string Email, string Nombre, string Apellido, string Username, string Password)
        {
            Usuario usuarioRegister = new Usuario();
            usuarioRegister.Email = Email;
            usuarioRegister.Nombre = Nombre;
            usuarioRegister.Apellido = Apellido;
            usuarioRegister.Username = Username;
            usuarioRegister.Password = Password;
            usuarioRegister.Activo = true;
            usuarioRegister.Vendedor = false;
            RNUsuario.Register(usuarioRegister);
            return Login(usuarioRegister.Username, usuarioRegister.Password);
        }
    }
}
