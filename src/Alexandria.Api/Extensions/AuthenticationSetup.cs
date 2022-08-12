using Alexandria.Identity.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Alexandria.Api.Extensions;
public static class AuthenticationSetup
{
    public static void AddAuthentitcation(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettingOptions = configuration.GetSection("JwtOptions");
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtOptions:SigningCredentials").Value));

        services.Configure<JwtOptions>(options =>
        {
            options.Issuer = jwtSettingOptions["Issuer"];
            options.Audience = jwtSettingOptions["Audience"];
            options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            options.Expiration = int.Parse(jwtSettingOptions["Expiration"] ?? "0");
        });

        var isRequiredForPassoword = false;
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireNonAlphanumeric = isRequiredForPassoword;
            options.Password.RequireDigit = isRequiredForPassoword;
            options.Password.RequireLowercase = isRequiredForPassoword;
            options.Password.RequireUppercase = isRequiredForPassoword;
            options.Password.RequiredLength = 6;
        });

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // issuer deve ser o mesmo descrito no appsettings.json
            ValidIssuer = configuration.GetSection("JwtOptions:Issuer").Value,

            ValidateAudience = true,
            ValidAudience = configuration.GetSection("JwtOptions:Audience").Value,

            ValidateIssuerSigningKey = true, // validar assinatura
            IssuerSigningKey = securityKey,

            RequireExpirationTime = true,
            ValidateLifetime = true,

            ClockSkew = TimeSpan.Zero
        };

        services.AddAuthentication(options =>
        {
            // iformamos que iremos trabalhar com autenticação e ele virá no formato JWT Bearer
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            // adiciona suporte ao JWT informado que ele deve serguir a validação criada no objeto tokenValidationParameters
            options.TokenValidationParameters = tokenValidationParameters;
        });
    }
}