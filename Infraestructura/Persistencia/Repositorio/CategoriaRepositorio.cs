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
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly DbContextOnwer _context;
        public CategoriaRepositorio(DbContextOnwer context)
        {
            _context = context;
        }
        public void Eliminar(int id)
        {
            _context.Categoria.Where(categoria => categoria.Id == id).ExecuteDelete();
        }

        public IEnumerable<Categoria> GetAll()
        {
            return _context.Categoria.ToList();
        }

        public Categoria GetCategoria(int id)
        {
            return _context.Categoria.Find(id);
        }

        public Categoria GetCategoriaByName(string name)
        {
            return _context.Categoria.FirstOrDefault(categoria => categoria.Descripcion == name);
        }

        public void Insertar(Categoria categoria)
        {
            _context.Categoria.Add(categoria);
            _context.SaveChanges();
        }

        public void Update(int id, Categoria categoria)
        {
            _context.Categoria.Update(categoria);
            _context.SaveChanges();
        }
    }
}
