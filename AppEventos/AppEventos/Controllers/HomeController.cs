using AppEventos.Models;
using AppEventos.Reglas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppEventos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (SessionHelper.UsuarioLogueado != null && SessionHelper.UsuarioLogueado.Id != 0) {
                var user = RNUsuario.Buscar(SessionHelper.UsuarioLogueado.Id);
                SessionHelper.UsuarioLogueado = user;
                SessionHelper.EventosUsuario = user.GetEventosComprados();
            }
            //Esto de arriba podria hacerse una funcion para reutilizarla en distintos ActionResults. Asi nos aseguramos que la session coincida con la base de datos. 
            var eventos = RNEvento.getEventos();
            return View(eventos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}

