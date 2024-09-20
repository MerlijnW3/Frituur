namespace Frituur.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Naam { get; set; }
        public float? Prijs { get; set; }
        public string? Foto { get; set; }
        public float? Korting { get; set; }
    }
}
