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

            TempData["SuccessMessage"] = "¡Su reserva ha sido confirmada exitosamente!";

            return RedirectToAction("ConfirmarReserva", new { idCasa, fechaInicio, fechaFin }); // Redirigir a la página de confirmación
        }

        // Vista de confirmación de reserva
        public async Task<IActionResult> ConfirmarReserva(int idCasa, DateTime fechaInicio, DateTime fechaFin)
        {
            // Obtener los detalles de la casa seleccionada desde el servicio
            var casa = await _reservacionService.GetCasaByIdAsync(idCasa);

            if (casa == null)
            {
                return NotFound("La casa seleccionada no existe.");
            }

            ViewData["fechaInicio"] = fechaInicio.ToString("yyyy-MM-dd"); // Aseguramos el formato de fecha
            ViewData["fechaFin"] = fechaFin.ToString("yyyy-MM-dd");

            return View(casa);  // Mostrar confirmación
        }

    }
}
