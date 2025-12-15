using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces.Servicios
{
    public interface ICategoriaServicio
    {
        void Crear(int usuario, CategoriaDTO categoria);
        void Eliminar(EliminarCategoriaDTO eliminarCategoriaDTO);
        void Editar(int id, CategoriaDTO categoria);
        CategoriaDTO ObtenerPorID(int id);
        IEnumerable<CategoriaDTO> ObtenerTodos();
    }
}
