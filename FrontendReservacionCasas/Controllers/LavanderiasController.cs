using FrontendReservacionCasas.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontendReservacionCasas.Controllers
{
    public class LavanderiasController : Controller
    {
        private readonly LavanderiaService _lavanderiaService;

        public LavanderiasController(LavanderiaService lavanderiaService)
        {
            _lavanderiaService = lavanderiaService;
        }

        public async Task<IActionResult> Index()
        {
            var lavanderias = await _lavanderiaService.GetLavanderiasAsync();
            return View(lavanderias);
        }
    }
}
