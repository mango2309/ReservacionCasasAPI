using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendasAPI.Data;
using TiendasAPI.Models;

namespace TiendasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendasController : ControllerBase
    {
        private readonly TiendasApiContext _context;

        public TiendasController (TiendasApiContext context)
        {
            _context = context;
        }

        // GET: api/stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tienda>>> GetStores()
        {
            // Obtiene todas las tiendas de la base de datos
            var stores = await _context.Tiendas.ToListAsync();
            return Ok(stores); // Devuelve todas las tiendas
        }

        // GET: api/stores/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Tienda>> GetStore(int id)
        {
            var store = await _context.Tiendas.FindAsync(id);

            if (store == null)
            {
                return NotFound(); // Si no se encuentra la tienda con el ID dado
            }

            return Ok(store); // Devuelve la tienda encontrada
        }

        // POST: api/stores
        [HttpPost]
        public async Task<ActionResult<Tienda>> PostStore(Tienda store)
        {
            _context.Tiendas.Add(store);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStore), new { id = store.IdTienda }, store);
        }

        // PUT: api/stores/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(int id, Tienda store)
        {
            if (id != store.IdTienda)
            {
                return BadRequest();
            }

            _context.Entry(store).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
                {
                    return NotFound(); 
                }
                else
                {
                    throw; // Si ocurre un error en la concurrencia de la base de datos
                }
            }

            return NoContent(); // Devuelve un 204 No Content si todo está bien
        }

        // DELETE: api/stores/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var store = await _context.Tiendas.FindAsync(id);
            if (store == null)
            {
                return NotFound(); // Si no se encuentra la tienda
            }

            _context.Tiendas.Remove(store); // Elimina la tienda de la base de datos
            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve un 204 No Content si la eliminación fue exitosa
        }

        // Método privado para comprobar si la tienda existe
        private bool StoreExists(int id)
        {
            return _context.Tiendas.Any(e => e.IdTienda == id);
        }
    }
}
