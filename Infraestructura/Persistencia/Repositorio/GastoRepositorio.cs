using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Repositorios;
using DocumentFormat.OpenXml.InkML;
using Dominio.Entidades;
using Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Persistencia.Repositorio
{
    public class GastoRepositorio : IGastoRepositorio
    {
        private readonly DbContextOnwer _Context;
        public GastoRepositorio (DbContextOnwer Context)
        {
            _Context = Context;
        }

        public async Task<List<Gasto>> ObtenerGastosPorMes(int año, int mes)
        {
            return await _Context.gasto
                .Where(x => x.Fecha.Year == año && x.Fecha.Month == mes)
                .ToListAsync();
        }
        public void actualizar(int id, Gasto gasto)
        {
            _Context.gasto.Add(gasto);
            _Context.SaveChanges();
        }

        public void eliminar(int id)
        {
            _Context.gasto.Where(gasto => gasto.id == id).ExecuteDelete();
        }

        public Gasto GastoXcategoria(int categoriaId)
        {
            return _Context.gasto.FirstOrDefault(gasto => gasto.CategoriaId == categoriaId);
        }

        public Gasto get(int id)
        {
            return _Context.gasto.Find(id);
        }

        public IEnumerable<Gasto> GetAll()
        {
            return _Context.gasto.ToList();
        }

        public void insertar(Gasto gasto)
        {
            _Context.gasto.Add(gasto);
            _Context.SaveChanges();
        }

        public IQueryable<Gasto> Query()
        {
            return _Context.gasto.AsQueryable();
        }
    }
}
