namespace BookBox.Application.Features.UserBooks.DTOs;

// Ni UserId ni BookId: forman la llave compuesta y vienen en la ruta
public class UpdateUserBookRequestDto
{
    public string Status { get; set; } = string.Empty;
    public int? Rating { get; set; }
    public string? PersonalReview { get; set; }
    public DateOnly? FinishedAt { get; set; }
}
