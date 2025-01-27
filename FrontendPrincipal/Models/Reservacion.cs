using System.ComponentModel.DataAnnotations;

namespace FrontendPrincipal.Models
{

    public class Reservacion
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdCasa { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

}
