namespace Frituur.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Boolean? IsAdmin { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
