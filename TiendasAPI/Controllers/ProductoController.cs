using Microsoft.AspNetCore.Mvc;
using TiendasAPI.Models;

namespace TiendasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private static List<Producto> Productos = new List<Producto>
        {
            new Producto { IdProducto = 1, Nombre = "Producto A", Precio = 10.5m },
            new Producto { IdProducto = 2, Nombre = "Producto B", Precio = 20.0m },
            new Producto { IdProducto = 3, Nombre = "Producto C", Precio = 15.75m }
        };

        // GET: api/productos
        [HttpGet]
        public ActionResult<IEnumerable<Producto>> Get()
        {
            return Ok(Productos);
        }

        // GET: api/productos/{id}
        [HttpGet("{id}")]
        public ActionResult<Producto> GetById(int id)
        {
            var producto = Productos.FirstOrDefault(p => p.IdProducto == id);
            if (producto == null)
            {
                return NotFound(new { mensaje = "Producto no encontrado." });
            }

            return Ok(producto);
        }

        // POST: api/productos
        [HttpPost]
        public ActionResult<Producto> Create([FromBody] Producto nuevoProducto)
        {
            if (nuevoProducto == null)
            {
                return BadRequest(new { mensaje = "El producto no puede ser nulo." });
            }

            nuevoProducto.IdProducto = Productos.Count > 0 ? Productos.Max(p => p.IdProducto) + 1 : 1;
            Productos.Add(nuevoProducto);

            return CreatedAtAction(nameof(GetById), new { id = nuevoProducto.IdProducto }, nuevoProducto);
        }

        // PUT: api/productos/{id}
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Producto productoActualizado)
        {
            var producto = Productos.FirstOrDefault(p => p.IdProducto == id);
            if (producto == null)
            {
                return NotFound(new { mensaje = "Producto no encontrado." });
            }

            producto.Nombre = productoActualizado.Nombre;
            producto.Precio = productoActualizado.Precio;

            return NoContent();
        }

        // DELETE: api/productos/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var producto = Productos.FirstOrDefault(p => p.IdProducto == id);
            if (producto == null)
            {
                return NotFound(new { mensaje = "Producto no encontrado." });
            }

            Productos.Remove(producto);

            return NoContent();
        }
    }

}
