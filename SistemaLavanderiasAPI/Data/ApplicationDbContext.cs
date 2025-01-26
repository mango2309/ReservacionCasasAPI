using Microsoft.EntityFrameworkCore;
using SistemaLavanderiasAPI.Models;

namespace SistemaLavanderiasAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lavanderia> Lavanderias { get; set; }
        public DbSet<Servicio> Servicios{ get; set; }
        public DbSet<SolicitudServicio> SolicitudesServicios { get; set; }

    }
}
