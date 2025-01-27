using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservacionCasasAPI.Data;
using ReservacionCasasAPI.Models;

namespace ReservacionCasasAPI.Controllers
{
    [Route("api/casas")]
    [ApiController]
    public class CasasController : ControllerBase
    {
        private readonly ReservacionContext _context;

        public CasasController(ReservacionContext context)
        {
            _context = context;
        }

        // GET: api/Casas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Casa>>> GetCasas()
        {
            return await _context.Casas.ToListAsync();
        }

        // GET: api/Casas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Casa>> GetCasa(int id)
        {
            var casa = await _context.Casas.FindAsync(id);

            if (casa == null)
            {
                return NotFound();
            }

            return casa;
        }

        // POST: api/Casas
        [HttpPost]
        public async Task<ActionResult<Casa>> PostCasa(Casa casa)
        {
            _context.Casas.Add(casa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCasa), new { id = casa.IdCasa }, casa);
        }

        // PUT: api/Casas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCasa(int id, Casa casa)
        {
            if (id != casa.IdCasa)
            {
                return BadRequest();
            }

            _context.Entry(casa).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Casas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCasa(int id)
        {
            var casa = await _context.Casas.FindAsync(id);
            if (casa == null)
            {
                return NotFound();
            }

            _context.Casas.Remove(casa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
