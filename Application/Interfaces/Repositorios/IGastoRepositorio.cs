using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Dominio.Entidades;

namespace Application.Interfaces.Repositorios
{
    public interface IGastoRepositorio
    {
        void insertar(Gasto gasto);
        void eliminar(int id);
        void actualizar(int id, Gasto gasto);
        Gasto get(int id);
        IEnumerable<Gasto> GetAll();
        Gasto GastoXcategoria(int categoriaId);
        IQueryable<Gasto> Query();
        Task<List<Gasto>> ObtenerGastosPorMes(int año, int mes);

    }
}
