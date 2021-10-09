using Microsoft.EntityFrameworkCore;
using UsersService.Models;

namespace UsersService.Data
{
    public  class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Posts)
                .WithOne(x => x.Owner)
                .HasForeignKey(x => x.UserId);
            
            modelBuilder.Entity<Post>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.UserId);
        }
    }
}