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
                Email = "Damian@gmail.com",
                Apellido = "Guillermi",
                Descripcion = "Vendedor de cursos online de programacion.",
                Vendedor = true
            }
                );
            Reglas.RNUsuario.Register(new Entidades.Usuario()
            {
                Activo = true,
                Password = "123",
                Username = "mtozzini",
                Nombre = "Mateo",
                Email = "Mateo@gmail.com",
                Apellido = "Tozzini",
                Descripcion = "Vendedor de cursos online de cocina.",
                Vendedor = true
            }
                );

            Reglas.RNUsuario.Register(new Entidades.Usuario()
            {
                Activo = true,
                Password = "123",
                Username = "ldeni",
                Nombre = "Lucas",
                Email = "Lucas@gmail.com",
                Apellido = "Denicola",
                Descripcion = "Vendedor de cursos online de Anime.",
                Vendedor = true
            }
                );

            Reglas.RNUsuario.Register(new Entidades.Usuario()
            {
                Activo = true,
                Password = "123",
                Username = "efleire",
                Nombre = "Ezequiel",
                Email = "Ezequiel@gmail.com",
                Apellido = "Fleire",
                Descripcion = "Vendedor de cursos online de guitarra.",
                Vendedor = true
            }
                );
        }
    }
}
