
namespace AppEventos
{
    using AppEventos.Reglas;
    using System;
    using System.Collections.Generic;

    public partial class usuario
    {
        public bool Save()
        {
            var success = RNUsuario.SaveUser(this);
            return success;
        }
        public bool ChangePassword(string password)
        {
            return false;
        }

        public bool DarAltaVendedor() {
            if (this.Id == null || this.Id == 0 ) { return false; }

        }
    }
}
