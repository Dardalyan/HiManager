using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HiManager.Model;
using Microsoft.IdentityModel.Tokens;

namespace HiManager.Service.jwt;

public class TokenService: ITokenService
{
    public string GenerateJWTToken(User user) {
        var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
           // new Claim("role",string.Join(", ", user.Roles))
        };
        var jwtToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("jwt_secret_key")!)
                ),
                SecurityAlgorithms.HmacSha256Signature)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}