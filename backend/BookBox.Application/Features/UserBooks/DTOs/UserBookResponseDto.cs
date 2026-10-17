namespace BookBox.Application.Features.UserBooks.DTOs;

public class UserBookResponseDto
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public string Status { get; set; } = string.Empty;
    public int? Rating { get; set; }
    public string? PersonalReview { get; set; }
    public DateOnly? FinishedAt { get; set; }
}
