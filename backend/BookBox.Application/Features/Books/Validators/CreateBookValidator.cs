using BookBox.Application.Features.Books.DTOs;
using FluentValidation;

namespace BookBox.Application.Features.Books.Validators;

public class CreateBookValidator : AbstractValidator<CreateBookRequestDto>
{
    public CreateBookValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El título es obligatorio.")
            .MaximumLength(100).WithMessage("El título no puede pasar de 100 caracteres.");

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("El autor es obligatorio.")
            .MaximumLength(50).WithMessage("El autor no puede pasar de 50 caracteres.");

        RuleFor(x => x.Genre)
            .MaximumLength(25).WithMessage("El género no puede pasar de 25 caracteres.");

        RuleFor(x => x.TotalPages)
            .GreaterThan(0).WithMessage("Las páginas totales deben ser mayores que 0.");
    }
}
