namespace Frituur.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public User? User { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
