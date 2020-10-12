using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppEventos.Controllers
{
    public static class  SessionHelper 
    {
        public static usuario UsuarioLogueado {
            get
            {
                return (usuario)HttpContext.Current.Session["UsuarioLogeado"];
            }
            set
            {
                HttpContext.Current.Session["UsuarioLogeado"] = value;
            }
        }

    }
}