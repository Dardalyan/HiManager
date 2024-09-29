using HiManager.Model;

namespace HiManager.Service.jwt;

public interface ITokenService
{
    public string GenerateJWTToken(User user);
}