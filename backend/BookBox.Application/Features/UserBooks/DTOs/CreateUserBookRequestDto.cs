namespace BookBox.Application.Features.UserBooks.DTOs;

// El UserId NO va aqui: viene de la ruta (api/users/{userId}/books)
public class CreateUserBookRequestDto
{
    public int BookId { get; set; }
    public string Status { get; set; } = string.Empty;
    public int? Rating { get; set; }
    public string? PersonalReview { get; set; }
    public DateOnly? FinishedAt { get; set; }
}
