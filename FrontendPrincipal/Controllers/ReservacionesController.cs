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
            var casas = await _reservacionService.GetCasasAsync();
            return View(casas);  
        }

        [HttpPost]
        public async Task<IActionResult> RealizarReserva(int idCasa, DateTime fechaInicio, DateTime fechaFin)
        {
            var reserva = new Reservacion
            {
                IdUsuario = 2, // Usar un ID de usuario fijo o manejarlo desde otra lógica
                IdCasa = idCasa,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };

            await _reservacionService.CrearReservaAsync(reserva);  

            TempData["SuccessMessage"] = "¡Su reserva ha sido confirmada exitosamente!";

            return RedirectToAction("ConfirmarReserva", new { idCasa, fechaInicio, fechaFin }); 
        }

        public async Task<IActionResult> ConfirmarReserva(int idCasa, DateTime fechaInicio, DateTime fechaFin)
        {
            var casa = await _reservacionService.GetCasaByIdAsync(idCasa);

            if (casa == null)
            {
                return NotFound("La casa seleccionada no existe.");
            }

            ViewData["fechaInicio"] = fechaInicio.ToString("yyyy-MM-dd"); 
            ViewData["fechaFin"] = fechaFin.ToString("yyyy-MM-dd");

            return View(casa);  
        }

    }
}
