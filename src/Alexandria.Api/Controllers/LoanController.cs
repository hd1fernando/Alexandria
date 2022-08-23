using Alexandria.ApplicationService.Dtos.Request;
using Alexandria.Bussiness.Interfaces.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Alexandria.Api.Controllers;

public class LoanController : MainController
{
    public LoanController(INotifier notifier) : base(notifier)
    {

    }

    [HttpPost]
    public Task<ActionResult> MakeLoan(LoanViewModel loanViewModel)
    {

        return CustomResponse();
    }
}

