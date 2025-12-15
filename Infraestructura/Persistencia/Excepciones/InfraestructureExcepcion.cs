using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Persistencia.Excepciones
{
    public class InfraestructureExcepcion : Exception
    {
        public InfraestructureExcepcion(string message) : base(message) { }
    }
}
