namespace Frituur.Models
{
    public class BestProdKoppel
    {
        public int Id { get; set; }
        public int BestellingId { get; set; }
        public Bestelling Bestelling { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Aantal { get; set; }
    }
}
