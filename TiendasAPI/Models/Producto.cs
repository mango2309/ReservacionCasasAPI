﻿using System.ComponentModel.DataAnnotations;

namespace TiendasAPI.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int IdTienda { get; set; }
        public Tienda Tienda { get; set; }
    }
}
