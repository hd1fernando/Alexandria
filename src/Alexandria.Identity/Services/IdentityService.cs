using Alexandria.ApplicationService.Dtos.Request;
using Alexandria.ApplicationService.Dtos.Response;
using Alexandria.ApplicationService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Alexandria.Identity.Services;

internal class IdentityService : IIdentityService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _usermanger;
    private readonly JwtOptions _jwtOptions;

    public IdentityService(SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> usermanger,
        IOptions<JwtOptions> jwtOptions)
    {
        _signInManager = signInManager;
        _usermanger = usermanger;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<CreatedUserResponse> CreateUser(UserViewModel userViewModel, CancellationToken cancellationToken)
    {
        var identityUser = new IdentityUser
        {
            UserName = userViewModel.Email,
            Email = userViewModel.Email,
            NormalizedEmail = userViewModel.Email?.ToUpper(),
            EmailConfirmed = true
        };

        var result = await _usermanger.CreateAsync(identityUser, userViewModel.Password);
        if (result.Succeeded)
            await _usermanger.SetLockoutEnabledAsync(identityUser, false);

        var createUserResponse = new CreatedUserResponse(result.Succeeded);
        if (result.Succeeded == false && result.Errors.Any())
            createUserResponse.AddErrors(result.Errors.Select(x => x.Description));

        return createUserResponse;
    }

    public async Task<UserLoginRespose> Login(UserLoginViewModel userLoginViewModel, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(userLoginViewModel.Email, userLoginViewModel.Password, false, true);

        if (result.Succeeded)
            return await GenerateTokenAsync(userLoginViewModel.Email);

        var userLoginResponse = new UserLoginRespose(result.Succeeded);
        if (result.Succeeded == false)
        {
            if (result.IsLockedOut)
                userLoginResponse.AddError("Esse conta está bloqueada");
            else if (result.IsNotAllowed)
                userLoginResponse.AddError("Essa conta não tem permissão para fazer login");
            else if (result.RequiresTwoFactor)
                userLoginResponse.AddError("Confirme o login no segundo fator de autenticação");
            else
                userLoginResponse.AddError("Usario ou senha incorretos.");
        }

        return userLoginResponse;
    }

    private Task<UserLoginRespose> GenerateTokenAsync(string? email)
    {
        throw new NotImplementedException();
    }
}