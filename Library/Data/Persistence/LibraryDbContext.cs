using Library.Data.Entities;
using Library.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Persistence
{
    public class LibraryDbContext : DbContext
    {
        
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base (options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(u =>
            {
                u.HasKey(de => de.Id);
                u.HasIndex(u => u.Email).IsUnique();
                
            });

            builder.Entity<Book>(b =>
            {
                b.HasKey(de => de.Id);
                b.HasIndex(b => b.ISBN).IsUnique();
            });

            builder.Entity<Loan>(l =>
            {
                l.HasKey(de => de.Id);
                l.HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                l.HasOne(l => l.Librarian)
                .WithMany()
                .HasForeignKey(l => l.LibrarianId)
                .OnDelete(DeleteBehavior.Restrict);

                l.HasOne(l => l.Book)
                .WithMany()
                .HasForeignKey(l => l.BookId);
            });

            
        }
    }
}
