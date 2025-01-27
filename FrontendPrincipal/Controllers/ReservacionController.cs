using FrontendPrincipal.Models;
using FrontendPrincipal.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontendPrincipal.Controllers
{
    public class ReservacionesController : Controller
    {
        private readonly ReservacionService _reservacionService;

        public ReservacionesController(ReservacionService reservacionService)
        {
            _reservacionService = reservacionService;
        }

        public async Task<IActionResult> Reservar()
        {
            // Obtener las casas disponibles desde la API
            var casas = await _reservacionService.GetCasasAsync();
            return View(casas);  // Mostrar las casas en la vista
        }

        // Acción para realizar la reserva
        [HttpPost]
        public async Task<IActionResult> RealizarReserva(int idCasa, DateTime fechaInicio, DateTime fechaFin)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

            var reserva = new Reservacion
            {
                IdUsuario = int.Parse(userId),
                IdCasa = idCasa,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };

            await _reservacionService.CrearReservaAsync(reserva);  // Llamar al servicio para crear la reserva

            return RedirectToAction("ConfirmacionReserva");
        }

        // Vista de confirmación de reserva
        public IActionResult ConfirmacionReserva()
        {
            return View();  // Mostrar confirmación
        }

    }
}
