using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppEventos.Models;
using System.Web;
using AppEventos.Reglas;
using System.Web.Mvc;
using System.IO;

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

        [HttpPost]
        public ActionResult Register(string Email, string Nombre, string Apellido, string Username, string Password, HttpPostedFileBase Imagen)
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
                Descripcion = "",
                Imagen = Imagen.FileName

            };

            if (Imagen != null && Imagen.ContentLength > 0)
            {
                try
                {
                    string pathEvento = Server.MapPath("~/Content/Usuarios/" + usuarioRegister.Email);
                    var di = new DirectoryInfo(pathEvento);
                    if (!di.Exists)
                        di.Create();

                    string path = Path.Combine(pathEvento,
                                               Path.GetFileName(Imagen.FileName));
                    Imagen.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }

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
