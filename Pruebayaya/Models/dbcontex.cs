using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pruebayaya.Models
{
    public class dbcontex : DbContext
    {
        protected readonly IConfiguration configuration;
        public dbcontex(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(configuration.GetConnectionString("conexion"));
        }
        public virtual DbSet<Categorias> Categorias { get; set; }
    }
}
