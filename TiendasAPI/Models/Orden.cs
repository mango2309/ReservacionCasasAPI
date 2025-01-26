namespace TiendasAPI.Models
{
    public class Orden
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<Producto> Productos { get; set; }
    }
}
