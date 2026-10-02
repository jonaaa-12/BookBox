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
    private readonly IValidator<CreateBookRequestDto> _validator;

    public BooksController(
        IBookRepository bookRepository, 
        IValidator<CreateBookRequestDto> validator)
    {
        _bookRepository = bookRepository;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _bookRepository.GetAllAsync();

        var response = books.Select(b => new BookResponseDto
        {
            Id = b.Id,
            Title = b.Title
        });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        var response = new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookRequestDto request)
    {
        // 1. Validar usando FluentValidation
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        // 2. Mapear DTO a Entidad
        var book = new Book
        {
            Title = request.Title
        };

        // 3. Guardar en repositorio (mantiene tu lógica original)
        await _bookRepository.AddAsync(book);

        // 4. Mapear Entidad a DTO de Respuesta
        var response = new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title
        };

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
} 