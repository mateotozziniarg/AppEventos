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

        public int topeEntradas()
        {
            
            List<usuario_evento> usuario_Evento = RNEvento.GetEntradas(this);

            int cantidad = 0;
            foreach (usuario_evento UE in usuario_Evento) 
            {
                cantidad += UE.Cantidad;  
            }
            return cantidad;

        }
        public int entradasPorVender()
        {

            int cantidad = this.Tope_gente - this.topeEntradas();
            return cantidad;

        }
    }
}