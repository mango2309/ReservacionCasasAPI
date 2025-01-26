using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaLavanderiasAPI.Data;
using SistemaLavanderiasAPI.Models;

namespace SistemaLavanderiasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServicioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todos los servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicio>>> ObtenerServicios()
        {
            return await _context.Servicios.Include(s => s.Lavanderia).ToListAsync();
        }

        // Obtener servicio por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Servicio>> ObtenerServicio(int id)
        {
            var servicio = await _context.Servicios.Include(s => s.Lavanderia).FirstOrDefaultAsync(s => s.IdServicio == id);

            if (servicio == null)
            {
                return NotFound();
            }

            return servicio;
        }

        // Crear un nuevo servicio
        [HttpPost]
        public async Task<ActionResult<Servicio>> CrearServicio(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerServicio), new { id = servicio.IdServicio }, servicio);
        }

        // Actualizar servicio existente
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarServicio(int id, Servicio servicio)
        {
            if (id != servicio.IdServicio)
            {
                return BadRequest();
            }

            _context.Entry(servicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Eliminar servicio
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }

            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicios.Any(e => e.IdServicio == id);
        }
    }
}
