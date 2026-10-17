using BookBox.Application.Features.UserBooks.DTOs;
using FluentValidation;

namespace BookBox.Application.Features.UserBooks.Validators;

public class CreateUserBookValidator : AbstractValidator<CreateUserBookRequestDto>
{
    public CreateUserBookValidator()
    {
        RuleFor(x => x.BookId)
            .GreaterThan(0).WithMessage("Debes indicar un libro válido.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("El estado es obligatorio.")
            .Must(s => ReadingStatus.All.Contains(s))
            .WithMessage($"El estado debe ser uno de: {string.Join(", ", ReadingStatus.All)}.");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("La calificación debe estar entre 1 y 5.")
            .When(x => x.Rating.HasValue);

        RuleFor(x => x.PersonalReview)
            .MaximumLength(2000).WithMessage("La reseña no puede pasar de 2000 caracteres.");

        RuleFor(x => x.FinishedAt)
            .NotNull().WithMessage("Si el libro está terminado, debes indicar la fecha de fin.")
            .When(x => x.Status == ReadingStatus.Finished);

        RuleFor(x => x.FinishedAt)
            .Must(f => f.HasValue && f.Value <= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("La fecha de fin no puede estar en el futuro.")
            .When(x => x.FinishedAt.HasValue);
    }
}
