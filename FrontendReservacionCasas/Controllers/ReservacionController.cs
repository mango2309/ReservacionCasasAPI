using FrontendReservacionCasas.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontendReservacionCasas.Controllers
{
    public class ReservacionesController : Controller
    {
        private readonly ReservacionService _reservacionService;

        public ReservacionesController(ReservacionService reservacionService)
        {
            _reservacionService = reservacionService;
        }

        public async Task<IActionResult> Index()
        {
            var reservaciones = await _reservacionService.GetReservacionesAsync();
            return View(reservaciones);
        }
    }
}
