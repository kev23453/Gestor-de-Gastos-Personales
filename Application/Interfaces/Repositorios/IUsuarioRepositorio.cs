using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;

namespace Application.Interfaces.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Usuario getByEmail(string email);
        Usuario getById(int id);
        void insertar(Usuario usuario);
        void Modificar(Usuario usuario);
        Usuario verificarUsuario(string email);
        Usuario ObtenerLoginUsuario(string email, bool password);
    }
}
