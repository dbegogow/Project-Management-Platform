using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthenticationService.Infrastructure.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService.Services;

public class IdentityService : IIdentityService
{
    private readonly IConfiguration _configuration;

    public IdentityService(IConfiguration configuration)
        => this._configuration = configuration;

    public string GenerateJwtToken(string userId, string userName)
    {
        var jwtConfiguration = this._configuration.GetJwtConfigurations();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtConfiguration.Secret);

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescription);
        var encryptedToken = tokenHandler.WriteToken(token);

        return encryptedToken;
    }
}
