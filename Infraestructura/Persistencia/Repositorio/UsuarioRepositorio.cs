using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositorios;
using Application.Utils;
using Dominio.Entidades;
using Infraestructura.Persistencia.Contexto;

namespace Infraestructura.Persistencia.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DbContextOnwer _context;
        public UsuarioRepositorio(DbContextOnwer context)
        {
            _context = context;
        }
        public Usuario getByEmail(string email)
        {
            return _context.usuarios.FirstOrDefault(user => user.email == email);
        }

        public Usuario getById(int id)
        {
            return _context.usuarios.Find(id);
        }

        public void insertar(Usuario usuario)
        {
            _context.usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Modificar(Usuario usuario)
        {
            _context.usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public Usuario ObtenerLoginUsuario(string email, bool password)
        {
            return _context.usuarios.FirstOrDefault(usuario => usuario.email == email && password == true);
        }

        public Usuario verificarUsuario(string email)
        {
            return _context.usuarios.FirstOrDefault(usuario => usuario.email == email);
        }
    }
}
