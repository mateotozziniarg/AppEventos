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

        public ActionResult CrearEvento(string Titulo, string Resumen, string Descripcion, int TopeGente, System.DateTime FechaDesde, System.DateTime FechaHasta, string Ubicacion/*,bool Online,  string ImagenPortada*/)
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
                titulo = Titulo,
                resumen = Resumen,
                descripcion = Descripcion,
                tope_gente = TopeGente,
                online = true,
                activo = true,
                id_autor = 1,/*Ver como solucionar*/
                fecha_desde = FechaDesde,
                fecha_hasta = FechaHasta,
                ubicacion = Ubicacion,
                imagen_portada = " - "
                //Agregar fecha de creacion a la tabla modificar la clase y agregar aca fecha creacion = DateTime.Now;
            };
            var success = RNEvento.CrearEvento(evento);
            if (!success) 
            {
                ViewData["Error"] = "Surgio un error intentado guardar el evento. Revise los datos e intente de nuevo.";
                return View();
            }
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
