using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaLavanderiasAPI.Data;
using SistemaLavanderiasAPI.Models;

namespace SistemaLavanderiasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LavanderiaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LavanderiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todas las lavanderías
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lavanderia>>> ObtenerLavanderias()
        {
            return await _context.Lavanderias.ToListAsync();
        }

        // Obtener lavandería por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Lavanderia>> ObtenerLavanderia(int id)
        {
            var lavanderia = await _context.Lavanderias.FindAsync(id);

            if (lavanderia == null)
            {
                return NotFound();
            }

            return lavanderia;
        }

        // Crear una nueva lavandería
        [HttpPost]
        public async Task<ActionResult<Lavanderia>> CrearLavanderia(Lavanderia lavanderia)
        {
            _context.Lavanderias.Add(lavanderia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerLavanderia), new { id = lavanderia.IdLavanderia }, lavanderia);
        }

        // Actualizar lavandería existente
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarLavanderia(int id, Lavanderia lavanderia)
        {
            if (id != lavanderia.IdLavanderia)
            {
                return BadRequest();
            }

            _context.Entry(lavanderia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LavanderiaExists(id))
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

        // Eliminar lavandería
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarLavanderia(int id)
        {
            var lavanderia = await _context.Lavanderias.FindAsync(id);
            if (lavanderia == null)
            {
                return NotFound();
            }

            _context.Lavanderias.Remove(lavanderia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LavanderiaExists(int id)
        {
            return _context.Lavanderias.Any(e => e.IdLavanderia == id);
        }
    }
}
