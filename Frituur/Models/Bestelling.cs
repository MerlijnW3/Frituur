namespace Frituur.Models
{
    public class Bestelling
    {
        public int Id { get; set; }
        public int GebruikerId { get; set; }
        public Gebruiker Gebruiker { get; set; } = null!;
    }
}
