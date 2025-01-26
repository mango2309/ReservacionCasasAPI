using System.ComponentModel.DataAnnotations;

namespace SistemaLavanderiasAPI.Models
{
    public class SolicitudServicio
    {
        [Key]
        public int IdSolicitud { get; set; } 
        public int IdLavanderia { get; set; } 
        public string NombreCliente { get; set; }  
        public DateTime FechaSolicitud { get; set; } 
        public string Estado { get; set; } 
       
    }

}
