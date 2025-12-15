using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class FiltroGastosDTO
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? CategoriaId { get; set; }
        public int? MetodoPagoId { get; set; }
        public string? Descripcion { get; set; }
    }
}
