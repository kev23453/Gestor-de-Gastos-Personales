using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ResultadoImportacionDTO
    {
        public int RegistrosExitosos { get; set; } = 0;
        public List<string> Errores { get; set; } = new List<string>();
        public bool TieneErrores => Errores.Any();
    }
}
