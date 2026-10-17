using BookBox.Domain.Entities;

namespace BookBox.Application.Interfaces;

public interface IUserBookRepository
{
    Task<IEnumerable<UserBook>> GetByUserAsync(int userId);
    Task<UserBook?> GetAsync(int userId, int bookId);
    Task<UserBook> AddAsync(UserBook userBook);
    Task UpdateAsync(UserBook userBook);
}
