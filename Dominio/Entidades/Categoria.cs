using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Exepciones;

namespace Dominio.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Presupuesto { get; set; }
        public int Id_usuario { get; set; }
        public bool estadoActivo { get; set; }

        public Categoria() { }

        public Categoria(string _descripcion, decimal _Presupuesto, int _Usuario, bool _estadoActivo)
        {
            if (string.IsNullOrWhiteSpace(_descripcion))
            {
                throw new DomainExeption("El nombre es obligatorio");
            }

            if(_Presupuesto < 0)
            {
                throw new DomainExeption("Presupuesto invalido");
            }

            Descripcion = _descripcion;
            Presupuesto = _Presupuesto;
            Id_usuario = _Usuario;
            estadoActivo = _estadoActivo;
        }
    }
}
