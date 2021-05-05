using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class BookeryDbContext : DbContext
    {
        public DbSet<Container> Containers { get; set; }
        public DbSet<Blob> Blobs { get; set; }

        public BookeryDbContext(DbContextOptions options) : base(options) { }
    }
}
