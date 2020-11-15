using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppEventos.Controllers
{
    public static class  SessionHelper
    {
        public static usuario UsuarioLogueado
        {
            get
            {
                return (usuario)HttpContext.Current.Session["UsuarioLogeado"];
            }
            set
            {
                HttpContext.Current.Session["UsuarioLogeado"] = value;
            }
        }
        public static List<evento> EventosUsuario
        {
            get
            {
                return (List<evento>)HttpContext.Current.Session["ListaEventos"];
            }
            set
            {
                HttpContext.Current.Session["ListaEventos"] = value;
            }
        }

        public static evento EventoActual
        {
            get
            {
                return (evento)HttpContext.Current.Session["EventoActual"];
            }
            set
            {
                HttpContext.Current.Session["EventoActual"] = value;
            }
        }

    }
}