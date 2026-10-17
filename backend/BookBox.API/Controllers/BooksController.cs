using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BookBox.Application.Interfaces;
using BookBox.Domain.Entities;
using BookBox.Application.Features.Books.DTOs;

namespace BookBox.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly IValidator<CreateBookRequestDto> _createValidator;
    private readonly IValidator<UpdateBookRequestDto> _updateValidator;

    public BooksController(
        IBookRepository bookRepository,
        IValidator<CreateBookRequestDto> createValidator,
        IValidator<UpdateBookRequestDto> updateValidator)
    {
        _bookRepository = bookRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _bookRepository.GetAllAsync();

        return Ok(books.Select(ToDto));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(ToDto(book));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookRequestDto request)
    {
        // 1. Validar usando FluentValidation
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        // 2. Mapear DTO a Entidad (solo lo permitido)
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            TotalPages = request.TotalPages
        };

        // 3. Guardar en repositorio
        await _bookRepository.AddAsync(book);

        // 4. Mapear Entidad a DTO de Respuesta
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, ToDto(book));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookRequestDto request)
    {
        // 1. Validar usando FluentValidation
        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        // 2. Buscar el libro existente
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        // 3. Copiar SOLO los campos permitidos (el Id no se toca)
        book.Title = request.Title;
        book.Author = request.Author;
        book.Genre = request.Genre;
        book.TotalPages = request.TotalPages;

        // 4. Guardar y responder
        await _bookRepository.UpdateAsync(book);

        return Ok(ToDto(book));
    }

    private static BookResponseDto ToDto(Book b) => new()
    {
        Id = b.Id,
        Title = b.Title,
        Author = b.Author,
        Genre = b.Genre,
        TotalPages = b.TotalPages
    };
}
