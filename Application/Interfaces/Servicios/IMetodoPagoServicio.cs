using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces.Servicios
{
    public interface IMetodoPagoServicio
    {
        void crearMetodo(CrearMetodoPagoDTO metodo);
        IEnumerable<MetodoPagoDTO> ObtenerMetodos();
        MetodoPagoDTO ObtenerPorId(int id);
        void Actualizar(int id, ActualizarMetodoPagoDTO metodo);
        void Eliminar(int id);
    }
}
