namespace BookBox.Domain.Entities;

// Tabla intermedia Usuarios-Libros: llave primaria compuesta (UserId, BookId)
public class UserBook
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public string Status { get; set; } = string.Empty;
    public int? Rating { get; set; }
    public string? PersonalReview { get; set; }
    public DateOnly? FinishedAt { get; set; }
}
