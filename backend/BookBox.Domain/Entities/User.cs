namespace BookBox.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateOnly RegisteredAt { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
}
