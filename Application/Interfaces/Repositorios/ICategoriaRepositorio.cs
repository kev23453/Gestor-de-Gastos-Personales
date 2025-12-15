using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;

namespace Application.Interfaces.Repositorios
{
    public interface ICategoriaRepositorio
    {
        void Insertar(Categoria categoria);
        void Eliminar(int id);
        void Update(int id, Categoria categoria);
        Categoria GetCategoria(int id);
        IEnumerable<Categoria> GetAll();
        Categoria GetCategoriaByName(string name);
    }
}
