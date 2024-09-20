namespace Frituur.Models
{
    public class Gebruiker
    {
        public int Id { get; set; }
        public string? Naam { get; set; }
        public ICollection<Bestelling> Bestellingen { get; set; } = new List<Bestelling>();
    }
}
