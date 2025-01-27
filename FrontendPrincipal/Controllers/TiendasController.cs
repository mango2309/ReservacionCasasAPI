using FrontendPrincipal.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontendReservacionCasas.Controllers
{
    public class TiendasController : Controller
    {
        private readonly TiendaService _tiendaService;

        public TiendasController(TiendaService tiendaService)
        {
            _tiendaService = tiendaService;
        }

        public async Task<IActionResult> Index()
        {
            var tiendas = await _tiendaService.GetTiendasAsync();
            return View(tiendas);
        }
    }
}
