using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class BookeryDbContext : DbContext
    {
        public BookeryDbContext(DbContextOptions options) : base(options) { }
    }
}
