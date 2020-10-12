using AppEventos.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AppEventos.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AppContext : DbContext
    {
        public DbSet<Usuario> usuario { get; set; }
    }
}