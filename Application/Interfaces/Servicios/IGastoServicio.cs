using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces.Servicios
{
    public interface IGastoServicio
    {
        void Crear(GastoDTO gasto);
        void Borrar(int id);
        void Modificar(int id, GastoDTO gasto);
        GastoDTO GetById(int id);
        IEnumerable<GastoDTO> Listar();
        GastoDTO GastoXcategoria(int idCategoria);
        List<GastoDTO> FiltrarGastos(FiltroGastosDTO filtros);
        Task<ResultadoImportacionDTO> ImportarExcel(Stream stream, int usuarioId);
    }
}
