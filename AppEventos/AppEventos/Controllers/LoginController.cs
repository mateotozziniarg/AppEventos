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
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            UsuarioLogin usuarioNuevo = new UsuarioLogin
            {
                Username = username,
                Password = password
            };
            usuario userLoged = RNUsuario.Login(usuarioNuevo);
            if (userLoged == null)
            {
                ViewBag.Error = "No se ha logrado logearse.";
                return View();
            }
            else
            {
                SessionHelper.UsuarioLogueado = userLoged;
                var eventos = userLoged.GetEventosComprados();
                SessionHelper.EventosUsuario = eventos;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Logout() {
            Session["UsuarioLogeado"] = null;
            SessionHelper.EventosUsuario = null;
            SessionHelper.EventoActual = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string Email, string Nombre, string Apellido, string Username, string Password)
        {
            var emailUsed = RNUsuario.getByEmail(Email);
            var usernameUsed = RNUsuario.getByUsername(Username);
            if ( emailUsed != null) { ViewData["EmailUsed"] = "El correo electronico ya esta en uso"; return View("Login"); }
            if ( usernameUsed != null ) { ViewData["UsernameUsed"] = "El usuario ya esta en uso"; return View("Login"); }
            usuario usuarioRegister = new usuario
            {
                Email = Email,
                Nombre = Nombre,
                Apellido = Apellido,
                Username = Username,
                Password = Password,
                Activo = true,
                Vendedor = true,
                Descripcion = ""
            };
            try
            {
                RNUsuario.Register(usuarioRegister);
                SessionHelper.UsuarioLogueado = usuarioRegister;
                return RedirectToAction("Index", "Home");
            }
            catch(Exception e) {
                ViewBag.Error = e;
                return View("Login");
            }
            //return Login(usuarioRegister.Username, usuarioRegister.Password);
        }
    }
}
