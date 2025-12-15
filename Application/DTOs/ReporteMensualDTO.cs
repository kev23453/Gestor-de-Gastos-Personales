using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ReporteMensualDTO
    {
        public decimal TotalGastado { get; set; }
        public Dictionary<string, decimal> GastosPorCategoria { get; set; }
        public decimal DiferenciaVsMesAnterior { get; set; }
        public List<string> TopCategorias { get; set; }
    }
}
