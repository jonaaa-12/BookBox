using Microsoft.AspNetCore.Mvc;
using BookBox.Application.Interfaces;
using BookBox.Domain.Entities;

namespace BookBox.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    // La API recibe el IBookRepository mediante Inyección de Dependencias
    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _bookRepository.GetAllAsync();
        return Ok(books);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Book book)
    {
        await _bookRepository.AddAsync(book);
        return CreatedAtAction(nameof(GetAll), new { id = book.Id }, book);
    }
} 