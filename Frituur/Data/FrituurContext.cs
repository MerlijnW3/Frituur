using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Frituur.Models;

namespace Frituur.Data
{
    public class FrituurContext : DbContext
    {
        public FrituurContext (DbContextOptions<FrituurContext> options)
            : base(options)
        {
        }

        public DbSet<Frituur.Models.Gebruiker> Gebruiker { get; set; } = default!;
        public DbSet<Frituur.Models.Product> Product { get; set; } = default!;
        public DbSet<Frituur.Models.Bestelling> Bestelling { get; set; } = default!;
        public DbSet<Frituur.Models.BestProdKoppel> BestProdKoppel { get; set; } = default!;
    }
}
