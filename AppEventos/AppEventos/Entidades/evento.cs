﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppEventos
{
    using AppEventos.Reglas;
    using System;
    using System.Collections.Generic;
    using WebGrease.Css.Ast.Selectors;

    public partial class evento
    {
        public string getAutorName() {

            var autor = RNUsuario.Buscar(this.id_autor);
            return autor.Nombre;
        }
    }
}
