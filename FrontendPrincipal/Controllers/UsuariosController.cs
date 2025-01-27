using FrontendPrincipal.Models;
using FrontendPrincipal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FrontendPrincipal.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuariosService _usuariosService;
        private readonly ReservacionService _reservacionService;

        public UsuariosController(UsuariosService usuariosService, ReservacionService reservacionService)
        {
            _usuariosService = usuariosService;
            _reservacionService = reservacionService;
        }

        // Vista de inicio de sesión
        public IActionResult Login()
        {
            return View();
        }

        // Acción para realizar el login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var usuario = await _usuariosService.LoginAsync(email, password);

                // Guardar el usuario en la sesión
                HttpContext.Session.SetString("UserId", usuario.IdUsuario.ToString());
                HttpContext.Session.SetString("UserEmail", usuario.Email);

                // Redirigir al usuario a la página de reservas
                return RedirectToAction("Reservar", "Reservaciones");
            }
            catch (Exception ex)
            {
                // Mostrar el mensaje de error
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // Acción para cerrar sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();  // Limpiar la sesión
            return RedirectToAction("Login");
        }
    }
}
