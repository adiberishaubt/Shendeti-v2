using Shendeti.Infrastructure.Entities;

namespace Shendeti.Domain.Interfaces;

public interface IJwtService
{
    Task<string> GenerateJwt(User user);
    string GenerateRefreshToken();
    Task<string> RefreshToken(string refreshToken);
}