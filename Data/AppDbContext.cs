using Microsoft.EntityFrameworkCore;
using MinAPI.Models;

namespace MinAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<TableRow> TableRow => Set<TableRow>();
        public DbSet<PostRow> PostRow => Set<PostRow>();
    }
}