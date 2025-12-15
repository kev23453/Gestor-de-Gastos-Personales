using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CategoriaDTO
    {
        public string Descripcion { get; set; }
        public decimal Presupuesto { get; set; }
        public int Usuario { get; set; }
        public bool estado {  get; set; }
    }
}
