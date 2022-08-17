using Alexandria.ApplicationService.Dtos.Request;
using Alexandria.ApplicationService.Dtos.Response;
using Alexandria.ApplicationService.Interfaces;
using Alexandria.Identity.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Alexandria.Identity.Services;

public class IdentityService : IIdentityService
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

    public async Task<CreatedUserResponse> CreateUser(UserViewModel userViewModel)
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

    public async Task<UserLoginRespose> Login(UserLoginViewModel userLoginViewModel)
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

    private async Task<UserLoginRespose> GenerateTokenAsync(string? email)
    {
        var user = await _usermanger.FindByEmailAsync(email);
        var tokenClaims = await GetClaimsAsync(user);

        var expirationTime = DateTime.Now.AddSeconds(_jwtOptions.Expiration);

        var jwt = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            tokenClaims,
             DateTime.Now,
             expirationTime,
             _jwtOptions.SigningCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new UserLoginRespose(true, token, expirationTime);
    }

    private async Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
    {
        var claims = await _usermanger.GetClaimsAsync(user);
        var roles = await _usermanger.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));


        foreach (var role in roles)
            claims.Add(new Claim("role", role));

        return claims;
    }

    private static long ToUnixEpochDate(DateTime date)
      => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

}