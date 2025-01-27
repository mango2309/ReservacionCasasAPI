using System.ComponentModel.DataAnnotations;

namespace FrontendReservacionCasas.Models
{
    public class Lavanderia
    {
        [Key]
        public int IdLavanderia { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public string Telefono { get; set; }
        public decimal PrecioPorKg { get; set; }
    }
}
