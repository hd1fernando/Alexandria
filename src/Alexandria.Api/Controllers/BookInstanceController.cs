using Alexandria.ApplicationService.Dtos.Request;
using Alexandria.Bussiness.Interfaces.Notifications;
using Alexandria.Bussiness.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alexandria.Api.Controllers;

public class BookInstanceController : MainController
{
    private readonly IBookInstanceService _bookInstanceService;

    public BookInstanceController(INotifier notifier, IBookInstanceService bookInstanceService) : base(notifier)
        => _bookInstanceService = bookInstanceService;

    [HttpPost]
    public async Task<ActionResult> Create(BookInstanceViewModel bookInstanceView, CancellationToken cancellationToken)
    {
        await _bookInstanceService.CreateBookInstanceAsync(bookInstanceView.ISBN!, bookInstanceView.CiculationType!, cancellationToken);

        return CustomResponse();
    }
}

