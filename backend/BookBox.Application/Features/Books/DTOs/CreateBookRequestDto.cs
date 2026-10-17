namespace BookBox.Application.Features.Books.DTOs;

public class CreateBookRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string? Genre { get; set; }
    public int TotalPages { get; set; }
}
