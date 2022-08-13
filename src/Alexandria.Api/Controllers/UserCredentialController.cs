using Alexandria.ApplicationService.Dtos.Request;
using Alexandria.ApplicationService.Dtos.Response;
using Alexandria.ApplicationService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alexandria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserCredentialController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public UserCredentialController(IIdentityService identityService)
        => _identityService = identityService;

    [HttpPost("Create")]
    public async Task<ActionResult<CreatedUserResponse>> Create(UserViewModel userViewModel)
    {
        var result = await _identityService.CreateUser(userViewModel);
        if (result.Sucess)
            return Ok(result);
        else if (result.Errors.Count > 0)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserLoginRespose>> Login(UserLoginViewModel userLoginViewModel)
    {
        var result = await _identityService.Login(userLoginViewModel);

        if (result.Success)
            return Ok(result);

        return Unauthorized(result);
    }

}

