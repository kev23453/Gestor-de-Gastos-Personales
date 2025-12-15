using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositorios;
using Dominio.Entidades;
using Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Persistencia.Repositorio
{
    public class MetodoPagoRepositorio : IMetodoPagoRepositorio
    {
        private readonly DbContextOnwer dbContextOnwer;
        public MetodoPagoRepositorio(DbContextOnwer dbContextOnwer)
        {
            this.dbContextOnwer = dbContextOnwer;
        }

        public void delete(int id)
        {
           dbContextOnwer.metodoPago.Where(metodoPago => metodoPago.Id == id).ExecuteDelete();
        }

        public MetodoPago Get(int id)
        {
            return dbContextOnwer.metodoPago.Find(id);
        }

        public IEnumerable<MetodoPago> GetAll()
        {
            return dbContextOnwer.metodoPago.ToList();
        }

        public MetodoPago GetByName(string name)
        {
            return dbContextOnwer.metodoPago.FirstOrDefault(metodoPago => metodoPago.Descripcion == name);
        }

        public void Insert(MetodoPago metodoPago)
        {
            dbContextOnwer.Add(metodoPago);
            dbContextOnwer.SaveChanges();
        }

        public void update(int id, MetodoPago metodoPago)
        {
            dbContextOnwer.Update(metodoPago);
            dbContextOnwer.SaveChanges();
        }
    }
}
