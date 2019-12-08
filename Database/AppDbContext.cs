using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Iata> Iatas { get; set; }
    }
}
