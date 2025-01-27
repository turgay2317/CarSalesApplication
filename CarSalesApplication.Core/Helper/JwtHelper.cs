using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CarSalesApplication.Core.Helper;

public class JwtHelper
{
    private readonly string _secretKey = "HERE MUST BE MIN. 256-BIT CHARACTERS!!!";
    public string GenerateToken(int userId,string userRole)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, userRole),
        };
        
        var token = new JwtSecurityToken(
            issuer: "yourapp",
            audience: "yourapp",
            claims: claims,
            expires: DateTime.Now.AddHours(4),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = "yourapp",
                ValidAudience = "yourapp"
            }, out var validatedToken);

            return principal;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
