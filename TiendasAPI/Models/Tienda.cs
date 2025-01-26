using System.ComponentModel.DataAnnotations;

namespace TiendasAPI.Models
{
    public class Tienda
    {
        [Key]
        public int IdTienda { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public string Contacto { get; set; }
    }
}
