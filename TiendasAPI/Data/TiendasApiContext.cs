using Microsoft.EntityFrameworkCore;
using TiendasAPI.Models;

namespace TiendasAPI.Data
{
    public class TiendasApiContext : DbContext
    {
        public TiendasApiContext(DbContextOptions<TiendasApiContext> options) : base(options) {}

        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Tienda> Tiendas { get; set; }
    }
}
