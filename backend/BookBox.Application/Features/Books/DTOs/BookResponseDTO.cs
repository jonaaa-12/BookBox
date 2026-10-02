namespace BookBox.Application.Features.Books.DTOs;

public class BookResponseDto 
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
} 