namespace BookBox.Application.Interfaces;

public interface IUserRepository
{
    Task<bool> ExistsAsync(int id);
}
