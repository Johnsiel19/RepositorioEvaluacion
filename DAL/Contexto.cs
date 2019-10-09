using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.Entity;

namespace DAL
{
    public class Contexto : DbContext

    {
        public DbSet<Evaluaciones> Evaluaciones { get; set; }
        public DbSet<Estudiantes> Estudiantes { get; set; }

        public DbSet<Categorias> Categorias { get; set; }

        public Contexto() : base("ConStr") { }
    }
}
