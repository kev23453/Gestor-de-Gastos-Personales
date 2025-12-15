using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Exepciones;

namespace Dominio.Entidades
{
    public class Gasto
    {
        public int id { get; set;}
        public decimal Monto { get; set;}
        public DateTime Fecha { get; set;}
        public int CategoriaId { get; set;}
        public int MetodoPagoId { get; set;}
        public string Descripcion { get; set;}
        public int UsuarioId { get; set;}

        // para EF
        public Gasto() { }

        public Gasto(
            decimal _Monto,
            DateTime _Fecha,
            int _CategoriaID,
            int _MetodoPagoID,
            string _Descripcion,
            int _UsuarioId
        )
        { 
            if(_Monto <= 0)
            {
                throw new DomainExeption("Monto de gasto invalido"); 
            }

            Fecha = _Fecha;
            Monto = _Monto;
            Fecha = _Fecha;
            CategoriaId = _CategoriaID;
            MetodoPagoId = _MetodoPagoID;
            Descripcion = _Descripcion;
            UsuarioId = _UsuarioId;
        }
    }
}
