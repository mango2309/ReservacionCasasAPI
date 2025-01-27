using System.ComponentModel.DataAnnotations;

namespace FrontendPrincipal.Models
{
    public class Casa
    {
        [Key]
        public int IdCasa { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public decimal Precios { get; set; }
        public string Descripcion { get; set; }
    }
}
