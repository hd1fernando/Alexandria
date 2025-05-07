using Alexandria.Api.Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Alexandria.Api.Controllers;

public class AuthorController : MainController
{
    [HttpPost]
    public Task<ActionResult> CreateAsync(CreateAuthorRequest authorRequest, CancellationToken cancellation)
    {
        return null;
    }

}
