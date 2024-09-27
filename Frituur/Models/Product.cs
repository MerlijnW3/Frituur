namespace Frituur.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }
        public string? Photo { get; set; }
        public float? Discount { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
