using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AppEventos
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Reglas.RNUsuario.Register(new Entidades.Usuario()
            {
                Activo = true,
                Password = "123",
                Username = "dguillermi",
                Nombre = "Damian",
                Email = "123@gmail.com",
                Apellido = "Guillermi"
            }
                );
            Reglas.RNUsuario.Register(new Entidades.Usuario()
            {
                Activo = true,
                Password = "123",
                Username = "mtozzini",
                Nombre = "Mateo",
                Email = "123@gmail.com",
                Apellido = "Tozzini"
            }
                );

            Reglas.RNUsuario.Register(new Entidades.Usuario()
            {
                Activo = true,
                Password = "123",
                Username = "ldeni",
                Nombre = "Lucas",
                Email = "123@gmail.com",
                Apellido = "Denicola"
            }
                );

            Reglas.RNUsuario.Register(new Entidades.Usuario()
            {
                Activo = true,
                Password = "123",
                Username = "efleire",
                Nombre = "Ezequiel",
                Email = "123@gmail.com",
                Apellido = "Fleire"
            }
                );
        }
    }
}
