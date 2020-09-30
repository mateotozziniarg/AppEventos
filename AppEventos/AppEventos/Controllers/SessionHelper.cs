using AppEventos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppEventos.Controllers
{
    public static class  SessionHelper 
    {
        public static Usuario UsuarioLogueado {
            get
            {
                return (Usuario)HttpContext.Current.Session["UsuarioLogeado"];
            }
            set
            {
                HttpContext.Current.Session["UsuarioLogeado"] = value;
            }
        }

    }
}