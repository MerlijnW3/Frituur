using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Frituur.Models;

namespace WebAPI.Data
{
    public class WebAPIContext : DbContext
    {
        public WebAPIContext (DbContextOptions<WebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Frituur.Models.User> User { get; set; } = default!;
    }
}
