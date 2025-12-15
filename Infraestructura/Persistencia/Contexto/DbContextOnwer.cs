using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Persistencia.Contexto
{
    public class DbContextOnwer :  DbContext
    {
        public DbContextOnwer(DbContextOptions<DbContextOnwer> options) : base(options){}
        public DbSet<MetodoPago> metodoPago { get; set; }
        public DbSet<Gasto> gasto { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

    }
}
