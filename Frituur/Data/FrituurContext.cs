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
        public FrituurContext(DbContextOptions<FrituurContext> options)
            : base(options)
        {
        }
        public DbSet<Frituur.Models.Product> Product { get; set; } = default!;
        public DbSet<Frituur.Models.User> User { get; set; } = default!;
        public DbSet<Frituur.Models.Order> Order { get; set; } = default!;
    }
}

