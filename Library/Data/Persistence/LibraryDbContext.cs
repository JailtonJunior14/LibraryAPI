using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Persistence
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base (options)
        {
            
        }

        public DbSet<User> User { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(e =>
            {
                e.HasKey(de => de.Id);
            });
        }
    }
}
