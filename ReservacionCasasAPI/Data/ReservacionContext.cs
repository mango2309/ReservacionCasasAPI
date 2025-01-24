using Microsoft.EntityFrameworkCore;
using ReservacionCasasAPI.Models;

namespace ReservacionCasasAPI.Data
{
    public class ReservacionContext : DbContext // Heredamos de DbContext
    {
        public ReservacionContext(DbContextOptions<ReservacionContext> options) : base(options) { }

        // Definición de las tablas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Casa> Casas { get; set; }
        public DbSet<Reservacion> Reservaciones { get; set; }
    }
}
