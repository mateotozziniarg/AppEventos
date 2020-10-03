using AppEventos.Reglas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppEventos.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public bool Activo { get; set; }
        public bool Vendedor { get; set; }
        public String Username { get; set; }
        public String Descripcion { get; set; }


        public bool Save()
        {
            var rsp = RNUsuario.SaveUser(this);
            return rsp;
        }

    }
}
