using FrontendPrincipal.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontendPrincipal.Controllers
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
