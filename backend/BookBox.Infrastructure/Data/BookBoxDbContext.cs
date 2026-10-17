using Microsoft.EntityFrameworkCore;
using BookBox.Domain.Entities;

namespace BookBox.Infrastructure.Data;

public class BookBoxDbContext : DbContext 
{
    public BookBoxDbContext(DbContextOptions<BookBoxDbContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserBook> UserBooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>(e =>
        {
            e.Property(b => b.Title).HasMaxLength(100).IsRequired();
            e.Property(b => b.Author).HasMaxLength(50).IsRequired();
            e.Property(b => b.Genre).HasMaxLength(25);
        });

        modelBuilder.Entity<User>(e =>
        {
            e.Property(u => u.Name).HasMaxLength(30).IsRequired();
            e.Property(u => u.Email).HasMaxLength(100);
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.PasswordHash).HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<UserBook>(e =>
        {
            e.HasKey(x => new { x.UserId, x.BookId });
            e.Property(x => x.Status).HasMaxLength(20).IsRequired();
            e.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
            e.HasOne<Book>().WithMany().HasForeignKey(x => x.BookId);
        });
    }
} 