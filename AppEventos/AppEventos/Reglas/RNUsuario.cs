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

namespace AppEventos.Reglas
{
    public static class RNUsuario
    {

        public static List<usuario> getUsuarios()
        {
            using (eventsEntities1 db = new eventsEntities1())
            {
                return db.usuario.ToList();
            }
        }

        public static usuario getByUsername(String username)
        {
            //var usuarioEnLista = .Where(usu => usu.Username.Equals(username)).FirstOrDefault();
            try {
                using (eventsEntities1 db = new eventsEntities1()) {
                    usuario user = db.usuario.ToList().Where(usu => usu.Username.Equals(username)).FirstOrDefault();
                    return user;
                }
            }catch{
                return null;
            }
        }
        public static usuario getByEmail(String email)
        {
            try
            {
                using (eventsEntities1 db = new eventsEntities1())
                {
                    usuario user = db.usuario.ToList().Where(usu => usu.Email.Equals(email)).FirstOrDefault();
                    return user;
                }
            }
            catch
            {
                return null;
            }
        }

        public static usuario Buscar(int id)
        {
            try
            {
                using (eventsEntities1 db = new eventsEntities1())
                {
                    usuario user = db.usuario.ToList().Where(usu => usu.Id == id).FirstOrDefault();
                    return user;
                }
            }
            catch
            {
                return null;
            }
        }
        public static void Register(usuario user)
        {

            try
            {
                using (eventsEntities1 db = new eventsEntities1())
                {
                    db.usuario.Add(user);
                    db.SaveChanges();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }
        public static usuario Login(UsuarioLogin user)
        {
            usuario userValidation = getByUsername(user.Username);
            if (userValidation == null) { return null; };
            if (userValidation.Password == user.Password)
            {
                return userValidation;
            }
            else
            {
                return null;
            }
        }

        public static bool SaveUser(usuario usuario)
        {
            try
            {
                using (eventsEntities1 db = new eventsEntities1())
                {
                    var user = db.usuario.SingleOrDefault(x => x.Id == usuario.Id);
                    if (user != null) {
                        user.Nombre = usuario.Nombre;
                        user.Apellido = usuario.Apellido;
                        user.Descripcion = usuario.Descripcion;
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

        public static bool ChangePassword(usuario usuario, String NewPassword)
        {
            if (NewPassword == "" || usuario == null) { return false; }
            try
            {
                using (eventsEntities1 db = new eventsEntities1())
                {
                    var user = db.usuario.Where(x => x.Id == usuario.Id).FirstOrDefault();
                    user.Password = NewPassword;
                    db.SaveChanges();
                    SessionHelper.UsuarioLogueado = user;
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }


    }
}