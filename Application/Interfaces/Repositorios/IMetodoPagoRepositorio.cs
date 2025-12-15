using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;

namespace Application.Interfaces.Repositorios
{
    public interface IMetodoPagoRepositorio
    {
        void Insert(MetodoPago metodoPago);
        IEnumerable<MetodoPago> GetAll();
        MetodoPago Get(int id);
        void delete(int id);
        void update(int id, MetodoPago metodoPago);
        MetodoPago GetByName(string name);
    }
}
