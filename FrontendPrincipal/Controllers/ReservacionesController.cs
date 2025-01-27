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

        // Acción para reservar una casa
        [HttpPost]
        public async Task<IActionResult> RealizarReserva(int idCasa, DateTime fechaInicio, DateTime fechaFin)
        {

            // Crear una nueva reserva con los datos proporcionados
            var reserva = new Reservacion
            {
                IdUsuario = 2, // Usar un ID de usuario fijo o manejarlo desde otra lógica
                IdCasa = idCasa,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };

            await _reservacionService.CrearReservaAsync(reserva);  // Llamar al servicio para registrar la reserva

            return RedirectToAction("ConfirmarReserva", new { idCasa }); // Redirigir a la página de confirmación
        }

        // Vista de confirmación de reserva
        public async Task<IActionResult> ConfirmarReserva(int idCasa)
        {
            // Obtener los detalles de la casa seleccionada desde el servicio
            var casa = await _reservacionService.GetCasaByIdAsync(idCasa);

            if (casa == null)
            {
                return NotFound("La casa seleccionada no existe.");
            }

            return View(casa);  // Mostrar confirmación
        }

    }
}
