using Microsoft.EntityFrameworkCore;
using BookBox.Application.Interfaces;
using BookBox.Domain.Entities;
using BookBox.Infrastructure.Data;

namespace BookBox.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookBoxDbContext _context;

    public BookRepository(BookBoxDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }
} 