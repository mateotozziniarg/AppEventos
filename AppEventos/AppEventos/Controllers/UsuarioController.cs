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
        public ActionResult EditarPerfil(string Nombre, string Apellido, string Descripcion, HttpPostedFileBase Imagen) {

            usuario user = RNUsuario.Buscar(SessionHelper.UsuarioLogueado.Id);
            user.Nombre = Nombre;
            user.Apellido = Apellido;
            user.Descripcion = Descripcion;

            if (Imagen != null) { 
            user.Imagen = Imagen.FileName;
            

             if (Imagen != null && Imagen.ContentLength > 0)
                {
                 try
                  {
                    string pathEvento = Server.MapPath("~/Content/Usuarios/" + user.Email);
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
            }


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
