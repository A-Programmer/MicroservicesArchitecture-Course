using Microsoft.EntityFrameworkCore;
using PostsService.Models;

namespace PostsService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Post> Posts { get; set; }
    }
}