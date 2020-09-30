using AppEventos.Entidades;
using AppEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppEventos.Reglas
{
    public static class RNUsuario
    {
        private static List<Usuario> bdUsuario = new List<Usuario>();

        public static List<Usuario> getUsuarios()
        {
            return bdUsuario;
        }

        public static Usuario getByUsername(String username) 
        {
            var usuarioEnLista = bdUsuario.Where(usu => usu.Username.Equals(username)).FirstOrDefault();
            return usuarioEnLista;
        }

        public static Usuario Buscar(int id)
        {
            var usuarioEnLista = bdUsuario.Where(usu => usu.Id == id).FirstOrDefault();
            return usuarioEnLista;
        }
        public static void Register(Usuario usuario)
        {
            usuario.Id = bdUsuario.Count + 1;
            bdUsuario.Add(usuario);
        }
        public static Usuario Login(UsuarioLogin user) {
            Usuario userValidation = getByUsername(user.Username);
            if (userValidation == null) { return null; };
            if (userValidation.Password == user.Password) {
                return userValidation;
            }
            else {
                return null;
            }
        }


    }
}