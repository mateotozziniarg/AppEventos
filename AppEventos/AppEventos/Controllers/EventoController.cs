using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppEventos.Models;
using System.Web;
using AppEventos.Reglas;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.IO;

namespace AppEventos.Controllers
{
    public class EventoController : Controller
    {
        public ActionResult CrearEvento()
        {
            if (Session["UsuarioLogeado"] == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }
            ViewBag.Usuario = Session["UsuarioLogeado"];
            return View();
        }
        [HttpPost]

        public ActionResult CrearEvento(string Titulo, string Resumen, string Descripcion, System.DateTime FechaDesde, System.DateTime FechaHasta, string Ubicacion, HttpPostedFileBase ImagenPortada,
            bool Online = false, int TopeGente = 0)
        {
            if (Session["UsuarioLogeado"] == null)
            {
                ViewData["SessionExpirada"] = "Se vencio la sesion. Ingrese de nuevo.";
                return View("~/Views/Login/Login.cshtml");
            }
            //Validacion de datos como : 
            if (Titulo.Length < 1) { ViewData["Error"] = "El titulo es obligatorio."; return View(); }

            evento evento = new evento
            {
                Titulo = Titulo,
                Resumen = Resumen,
                Descripcion = Descripcion,
                Tope_gente = TopeGente,
                Online = Online,
                Activo = true,
                Id_autor = SessionHelper.UsuarioLogueado.Id,
                Fecha_desde = FechaDesde,
                Fecha_hasta = FechaHasta,
                Ubicacion = Ubicacion,
                Imagen_portada = ImagenPortada.FileName
                //Agregar fecha de creacion a la tabla modificar la clase y agregar aca fecha creacion = DateTime.Now;
            };
            var success = RNEvento.CrearEvento(evento);
            if (!success)
            {
                ViewData["Error"] = "Surgio un error intentado guardar el evento. Revise los datos e intente de nuevo.";
                return View();
            }


            if (ImagenPortada != null && ImagenPortada.ContentLength > 0)
            {
                try
                {
                    string pathEvento = Server.MapPath("~/Content/Eventos/" + evento.Id);
                    var di = new DirectoryInfo(pathEvento);
                    if (!di.Exists)
                        di.Create();

                    string path = Path.Combine(pathEvento,
                                               Path.GetFileName(ImagenPortada.FileName));
                    ImagenPortada.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Evento(int id)
        {
            var Evento = RNEvento.getById(id);
            var Usuario = RNUsuario.Buscar(Evento.Id_autor);

            ViewData["UsuarioEvento"] = Usuario;

            return View(Evento);
        }

        [HttpPost]
        public ActionResult AltaVendedor(String descripcion)
        {
            var user = RNUsuario.Buscar(SessionHelper.UsuarioLogueado.Id);


            return View();
        }
    }

}
