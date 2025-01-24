using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservacionCasasAPI.Data;
using ReservacionCasasAPI.Models;

namespace ReservacionCasasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservacionesController : ControllerBase
    {
        private readonly ReservacionContext _context;

        public ReservacionesController(ReservacionContext context)
        {
            _context = context;
        }

        // GET: api/Reservaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservacion>>> GetReservaciones()
        {
            return await _context.Reservaciones.ToListAsync();
        }

        // GET: api/Reservaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservacion>> GetReservacion(int id)
        {
            var reservacion = await _context.Reservaciones.FindAsync(id);

            if (reservacion == null)
            {
                return NotFound();
            }

            return reservacion;
        }

        // POST: api/Reservaciones
        [HttpPost]
        public async Task<ActionResult<Reservacion>> PostReservacion(Reservacion reservacion)
        {
            _context.Reservaciones.Add(reservacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReservacion), new { id = reservacion.Id }, reservacion);
        }

        // PUT: api/Reservaciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservacion(int id, Reservacion reservacion)
        {
            if (id != reservacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Reservaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservacion(int id)
        {
            var reservacion = await _context.Reservaciones.FindAsync(id);
            if (reservacion == null)
            {
                return NotFound();
            }

            _context.Reservaciones.Remove(reservacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
