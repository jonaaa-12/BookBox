using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BookBox.Application.Interfaces;
using BookBox.Domain.Entities;
using BookBox.Application.Features.UserBooks.DTOs;

namespace BookBox.API.Controllers;

[ApiController]
[Route("api/users/{userId:int}/books")]
public class UserBooksController : ControllerBase
{
    private readonly IUserBookRepository _userBookRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<CreateUserBookRequestDto> _createValidator;
    private readonly IValidator<UpdateUserBookRequestDto> _updateValidator;

    public UserBooksController(
        IUserBookRepository userBookRepository,
        IBookRepository bookRepository,
        IUserRepository userRepository,
        IValidator<CreateUserBookRequestDto> createValidator,
        IValidator<UpdateUserBookRequestDto> updateValidator)
    {
        _userBookRepository = userBookRepository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int userId)
    {
        var items = await _userBookRepository.GetByUserAsync(userId);

        return Ok(items.Select(ToDto));
    }

    [HttpGet("{bookId:int}")]
    public async Task<IActionResult> GetById(int userId, int bookId)
    {
        var item = await _userBookRepository.GetAsync(userId, bookId);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(ToDto(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create(int userId, [FromBody] CreateUserBookRequestDto request)
    {
        // 1. Validar usando FluentValidation
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        // 2. Verificar que existan el usuario y el libro
        if (!await _userRepository.ExistsAsync(userId))
        {
            return NotFound(new { mensaje = "El usuario indicado no existe." });
        }

        var book = await _bookRepository.GetByIdAsync(request.BookId);
        if (book == null)
        {
            return NotFound(new { mensaje = "El libro indicado no existe." });
        }

        // 3. La llave compuesta no permite repetir el mismo libro para el mismo usuario
        var existing = await _userBookRepository.GetAsync(userId, request.BookId);
        if (existing != null)
        {
            return Conflict(new { mensaje = "Ese libro ya está registrado para este usuario. Usa PUT para actualizarlo." });
        }

        // 4. Mapear DTO a Entidad (el UserId sale de la ruta, no del JSON)
        var userBook = new UserBook
        {
            UserId = userId,
            BookId = request.BookId,
            Status = request.Status,
            Rating = request.Rating,
            PersonalReview = request.PersonalReview,
            FinishedAt = request.FinishedAt
        };

        // 5. Guardar y responder
        await _userBookRepository.AddAsync(userBook);

        return CreatedAtAction(
            nameof(GetById),
            new { userId = userBook.UserId, bookId = userBook.BookId },
            ToDto(userBook));
    }

    [HttpPut("{bookId:int}")]
    public async Task<IActionResult> Update(int userId, int bookId, [FromBody] UpdateUserBookRequestDto request)
    {
        // 1. Validar usando FluentValidation
        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        // 2. Buscar el registro existente
        var userBook = await _userBookRepository.GetAsync(userId, bookId);
        if (userBook == null)
        {
            return NotFound();
        }

        // 3. Copiar SOLO los campos permitidos (la llave compuesta no cambia)
        userBook.Status = request.Status;
        userBook.Rating = request.Rating;
        userBook.PersonalReview = request.PersonalReview;
        userBook.FinishedAt = request.FinishedAt;

        // 4. Guardar y responder
        await _userBookRepository.UpdateAsync(userBook);

        return Ok(ToDto(userBook));
    }

    private static UserBookResponseDto ToDto(UserBook u) => new()
    {
        UserId = u.UserId,
        BookId = u.BookId,
        Status = u.Status,
        Rating = u.Rating,
        PersonalReview = u.PersonalReview,
        FinishedAt = u.FinishedAt
    };
}
