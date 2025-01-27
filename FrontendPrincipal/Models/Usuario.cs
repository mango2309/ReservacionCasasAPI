using System.ComponentModel.DataAnnotations;

namespace FrontendPrincipal.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NombreCompleto { get; set; }
    }
}
