namespace Frituur.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public string? Photo { get; set; }
        public double? Discount { get; set; }
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
