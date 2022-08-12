using Microsoft.IdentityModel.Tokens;

namespace Alexandria.Identity.Configurations;
public class JwtOptions
{
    public string? Issuer { get; set; } // Quem é o emissor do Token
    public string? Audience { get; set; } // Para quem o Token está sendo emitido
    public string? SecurityKey { get; set; }
    public SigningCredentials? SigningCredentials { get; set; }
    public string? Subject { get; set; }
    public int Expiration { get; set; }
    public DateTime NotBefore { get; set; }
    public DateTime IssuedAt { get; set; } // Quando o Token foi emitido
    public string? Jti { get; set; } // Identificador do jwt (jwt id)

}