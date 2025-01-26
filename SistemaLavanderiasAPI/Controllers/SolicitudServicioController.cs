using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaLavanderiasAPI.Data;
using SistemaLavanderiasAPI.Models;

namespace SistemaLavanderiasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudServicioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SolicitudServicioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todas las solicitudes de servicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolicitudServicio>>> ObtenerSolicitudes()
        {
            return await _context.SolicitudesServicios.ToListAsync();
        }

        // Obtener solicitud de servicio por id
        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitudServicio>> ObtenerSolicitud(int id)
        {
            var solicitud = await _context.SolicitudesServicios.FindAsync(id);

            if (solicitud == null)
            {
                return NotFound();
            }

            return solicitud;
        }

        // Crear una nueva solicitud de servicio
        [HttpPost]
        public async Task<ActionResult<SolicitudServicio>> CrearSolicitud(SolicitudServicio solicitud)
        {
            _context.SolicitudesServicios.Add(solicitud);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerSolicitud), new { id = solicitud.IdSolicitud }, solicitud);
        }

        // Actualizar solicitud de servicio existente
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarSolicitud(int id, SolicitudServicio solicitud)
        {
            if (id != solicitud.IdSolicitud)
            {
                return BadRequest();
            }

            _context.Entry(solicitud).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudServicioExists(id))
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

        // Eliminar solicitud de servicio
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarSolicitud(int id)
        {
            var solicitud = await _context.SolicitudesServicios.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }

            _context.SolicitudesServicios.Remove(solicitud);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SolicitudServicioExists(int id)
        {
            return _context.SolicitudesServicios.Any(e => e.IdSolicitud == id);
        }
    }
}
