using Alexandria.Api.Dtos;
using Alexandria.Bussiness.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alexandria.Api.Controllers;

public class BookController : MainController
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    public async Task<ActionResult> Create(BookViewModel bookViewModel, CancellationToken cancellation)
    {
        var book = bookViewModel.ToEntity();

        await _bookService.CreateBookAsync(book, cancellation);

        return Ok();
    }
}

