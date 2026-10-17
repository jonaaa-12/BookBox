using BookBox.Application.Interfaces;
using BookBox.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookBox.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BookBoxDbContext _context;

    public UserRepository(BookBoxDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Users.AnyAsync(u => u.Id == id);
    }
}
