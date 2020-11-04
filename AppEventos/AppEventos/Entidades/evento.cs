namespace AppEventos
{
    using AppEventos.Reglas;
    using System;
    using System.Collections.Generic;
    using WebGrease.Css.Ast.Selectors;

    public partial class evento
    {
        public string getAutorName()
        {
            var autor = RNUsuario.Buscar(this.Id_autor);
            return autor.Nombre;
        }
    }
}