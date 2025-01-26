using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendasAPI.Data;
using TiendasAPI.Models;

namespace TiendasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly TiendasApiContext _context;

        public OrdenesController(TiendasApiContext context)
        {
            _context = context;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orden>>> GetOrders()
        {
            // Incluir productos relacionados con cada orden
            var orders = await _context.Ordenes.Include(o => o.Productos).ToListAsync();
            return Ok(orders);
        }

        // GET: api/orders/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Orden>> GetOrder(int id)
        {
            var order = await _context.Ordenes
                .Include(o => o.Productos)  // Incluir productos relacionados con la orden
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Orden>> PostOrder(Orden order)
        {
            // Calcular el total de la orden sumando los precios de los productos
            order.TotalAmount = order.Productos.Sum(p => p.Precio);

            _context.Ordenes.Add(order);
            await _context.SaveChangesAsync();

            // Crear una respuesta que incluya la URL para obtener la orden recién creada
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        // PUT: api/orders/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Orden order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            // Calcular nuevamente el total de la orden
            order.TotalAmount = order.Productos.Sum(p => p.Precio);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // DELETE: api/orders/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Ordenes.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Ordenes.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Ordenes.Any(e => e.Id == id);
        }
    }
}
