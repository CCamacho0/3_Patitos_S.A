using _3_Patitos_S.A.Controllers;
using _3_Patitos_S.A.Models;
using Microsoft.EntityFrameworkCore;

namespace _3_Patitos_S.A.Data
{
    public class Db_Context: DbContext
    {
        public Db_Context(DbContextOptions<Db_Context> options) : base(options)
        {

        }

        public DbSet<Persona> Persona { get; set; }
        public DbSet<Categoria> Categoria { get; set;}
        public DbSet<Rol> Rol { get; set;}
        public DbSet<Estado_Usuario> Estado_Usuario { get;set; }
        public DbSet<Estado_Productos> Estado_Productos { get; set; }
        public DbSet<Tipo_Entrega> Tipo_Entrega { get; set; }
        public DbSet<Productos> Productos { get; set; }
    }
}
