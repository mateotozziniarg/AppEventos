using AppEventos.Models;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Messaging;
using System.ComponentModel.DataAnnotations;
using AppEventos.Controllers;
using System.Collections;

namespace AppEventos.Reglas
{
    public static class RNEvento
    {

        public static List<evento> getEventos()
        {
            using (eventsEntities1 db = new eventsEntities1())
            {
                return db.evento.ToList();
            }
        }

        public static evento getById(int id)
        {
            //var usuarioEnLista = .Where(usu => usu.Username.Equals(username)).FirstOrDefault();
            try {
                using (eventsEntities1 db = new eventsEntities1())
                {
                    evento evento = db.evento.ToList().Where(ev => ev.Id == id).FirstOrDefault();
                    return evento;
                }
            }catch{
                return null;
            }
        }
        public static List<evento> getByAutor(int idAutor)
        {
            try
            {
                using (eventsEntities1 db = new eventsEntities1())
                {
                    List<evento> eventos = db.evento.Where(x => x.Id_autor == idAutor).ToList();
                    return eventos;
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool CrearEvento(evento evento)
        {

            try
            {
                using (eventsEntities1 db = new eventsEntities1())
                {
                    db.evento.Add(evento);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool SaveEvento(evento evento)
        {
            try
            {
                using (eventsEntities1 db = new eventsEntities1())
                {
                    var ev = db.evento.SingleOrDefault(x => x.Id == evento.Id);
                    if (evento != null)
                    {
                        ev.Titulo = evento.Titulo;
                        ev.Resumen = evento.Resumen;
                        ev.Descripcion = evento.Descripcion;
                        ev.Tope_gente = evento.Tope_gente;
                        ev.Online = evento.Online;
                        ev.Fecha_desde = evento.Fecha_desde;
                        ev.Fecha_hasta = evento.Fecha_hasta;
                        ev.Ubicacion = evento.Ubicacion;
                        ev.Imagen_portada = evento.Imagen_portada;
                        db.SaveChanges();
                    }
                }  
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ComprarEntrada(usuario_evento usuario_evento)
        {
            try
            {
                using(eventsEntities1 db = new eventsEntities1())
                {
                    db.usuario_evento.Add(usuario_evento);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<usuario_evento> GetEntradas(evento evento)
        {
            try
            {
                using(eventsEntities1 db = new eventsEntities1())
                {
                    List < usuario_evento > usuario_Evento = db.usuario_evento.Where(x => x.Id_Evento == evento.Id).ToList();
                    return usuario_Evento;
                }
                
            }
            catch
            {
                return null;
            }
        }

        public static bool CancelarEvento(int id) {
            try {
                using (eventsEntities1 db = new eventsEntities1()) {

                    evento evento = db.evento.SingleOrDefault(x => x.Id == id);

                    evento.Activo = false;

                    db.SaveChanges();
                }
                return true;
            }
            catch {
                return false;
            }
        }

    }
}