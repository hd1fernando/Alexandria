using Alexandria.Bussiness.Interfaces.Notifications;
using Alexandria.Bussiness.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alexandria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public abstract class MainController : ControllerBase
{
    private readonly INotifier _notifier;

    protected MainController(INotifier notifier)
        => _notifier = notifier;

    protected ActionResult CustomResponse(object result = null)
        => _notifier.HasNotification()
        ? BadRequest(new
        {
            erros = GetNotifications()
        })
        : Ok(result);

    protected ActionResult CustomResponse(ModelStateDictionary keyValuePairs)
    {
        if (keyValuePairs.IsValid == false)
            NotifyInvalidModel(keyValuePairs);

        return CustomResponse();
    }

    protected void NotifyInvalidModel(ModelStateDictionary keyValues)
    {
        var errors = keyValues.Values.SelectMany(e => e.Errors);

        foreach (var error in errors)
        {
            var errorMessage = error.Exception is null ? error.ErrorMessage : error.Exception.Message;
            ErrorNotify(errorMessage);
        }
    }

    protected void ErrorNotify(string message)
        => _notifier.Handler(new Notification(message));

    private IEnumerable<string> GetNotifications()
        => _notifier.GetNotifications().Select(x => x.Message);
}
