using BookBox.Application.Interfaces;
using BookBox.Domain.Entities;
using BookBox.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookBox.Infrastructure.Repositories;

public class UserBookRepository : IUserBookRepository
{
    private readonly BookBoxDbContext _context;

    public UserBookRepository(BookBoxDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserBook>> GetByUserAsync(int userId)
    {
        return await _context.UserBooks
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    // FindAsync recibe los valores en el MISMO orden de la llave compuesta (UserId, BookId)
    public async Task<UserBook?> GetAsync(int userId, int bookId)
    {
        return await _context.UserBooks.FindAsync(userId, bookId);
    }

    public async Task<UserBook> AddAsync(UserBook userBook)
    {
        _context.UserBooks.Add(userBook);
        await _context.SaveChangesAsync();
        return userBook;
    }

    public async Task UpdateAsync(UserBook userBook)
    {
        _context.UserBooks.Update(userBook);
        await _context.SaveChangesAsync();
    }
}
