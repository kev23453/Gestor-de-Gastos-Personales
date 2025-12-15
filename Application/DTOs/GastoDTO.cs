using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GastoDTO
    {
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int CategoriaId { get; set; }
        public int MetodoPagoId { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
    }
}
