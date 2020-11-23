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
using System.Web.Routing;

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
            if (ImagenPortada == null) { ViewData["Error"] = "La imagen es obligatoria!"; return View(); }

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
                Imagen_portada = ImagenPortada.FileName,
                Fecha_Creacion = DateTime.Now
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
        public ActionResult Evento(int id, String errorMsg = "")
        {
            var Evento = RNEvento.getById(id);
            SessionHelper.EventoActual = Evento;
            var Usuario = RNUsuario.Buscar(Evento.Id_autor);
            if (errorMsg != "") {
                ViewData["ErrorMessage"] = errorMsg;
            }
            ViewData["UsuarioEvento"] = Usuario;
            return View(Evento);
        }

        [HttpPost]
        public ActionResult ComprarEntrada(int cantidad)
        {

            if (Session["UsuarioLogeado"] == null)
            {
                ViewData["SessionExpirada"] = "Para poder realizar una compra debes logearte.";
                return View("~/Views/Login/Login.cshtml");
            }
            if (cantidad == null) {
                cantidad = 1;
            }
            int entradas = SessionHelper.EventoActual.topeEntradas();
            if(entradas + cantidad > SessionHelper.EventoActual.Tope_gente)
             {
                ViewData["ErrorMessage"] = "El evento ya alcanzo el maximo de entradas vendidas";
                //return RedirectToAction("Evento", "Evento");
                return RedirectToAction("Evento", new RouteValueDictionary(
                          new { controller = "Evento", action = "Evento", id = SessionHelper.EventoActual.Id, errorMsg = "El evento ya alcanzo el maximo de entradas vendidas" })
                        );
            }
            usuario_evento usuario_evento = new usuario_evento
            {
                Id = 0,
                Id_Evento = SessionHelper.EventoActual.Id,
                Id_Usuario = SessionHelper.UsuarioLogueado.Id,
                Activo = true,
                Cantidad = cantidad,
                Fecha_Creacion = DateTime.Now
            };
            var success = RNEvento.ComprarEntrada(usuario_evento);
            if (!success)
            {
                ViewData["Error"] = "Surgio un error intentado comprar la entrada. Revise los datos e intente de nuevo.";
                return View("~/Views/Evento/Evento.cshtml",SessionHelper.EventoActual.Id);
            }

            return RedirectToAction("Index", "Home");

        }
        public ActionResult MisEventos()
        {
            var misEventos = RNEvento.getByAutor(SessionHelper.UsuarioLogueado.Id);

            return View(misEventos);
        }
    }

}
