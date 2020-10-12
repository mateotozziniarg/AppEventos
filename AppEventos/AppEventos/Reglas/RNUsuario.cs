using AppEventos.Models;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Messaging;

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
                    db.Database.Connection.Open();
                    usuario user = db.usuario.ToList().Where(usu => usu.Username.Equals(username)).FirstOrDefault();
                    db.Database.Connection.Close();
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
                    db.Database.Connection.Open();
                    usuario user = db.usuario.ToList().Where(usu => usu.Email.Equals(email)).FirstOrDefault();
                    db.Database.Connection.Close();
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
                    db.Database.Connection.Open();
                    usuario user = db.usuario.ToList().Where(usu => usu.Id == id).FirstOrDefault();
                    db.Database.Connection.Close();
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
                    db.Database.Connection.Open();
                    db.usuario.Add(user);
                    db.SaveChanges();
                    db.Database.Connection.Close();
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
                    db.Database.Connection.Open();
                    var user = db.usuario.SingleOrDefault(x => x.Id == usuario.Id);
                    if (user != null) {
                        user.Nombre = usuario.Nombre;
                        user.Apellido = usuario.Apellido;
                        user.Descripcion = usuario.Descripcion;
                        db.SaveChanges();
                    }
                    db.Database.Connection.Close();
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
            usuario.Password = NewPassword;
            //bdUsuario[bdUsuario.FindIndex(x => x.Id == usuario.Id)] = usuario;
            return true;
        }


    }
}