using Microsoft.EntityFrameworkCore;
using BookBox.Domain.Entities;

namespace BookBox.Infrastructure.Data;

public class BookBoxDbContext : DbContext 
{
    public BookBoxDbContext(DbContextOptions<BookBoxDbContext> options) : base(options) { }
    
   
    public DbSet<Book> Books { get; set; }
} 