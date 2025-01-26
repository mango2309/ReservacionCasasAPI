using System.ComponentModel.DataAnnotations;

namespace SistemaLavanderiasAPI.Models
{
    public class Servicio
    {
        [Key]
        public int IdServicio { get; set; } 
        public string NombreServicio { get; set; } 
        public string Descripcion { get; set; } 
        public decimal Precio { get; set; } 
        public int IdLavanderia { get; set; } 
        public Lavanderia Lavanderia { get; set; } 
    }

}
