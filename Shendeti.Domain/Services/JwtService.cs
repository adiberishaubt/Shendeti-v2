using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Domain.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public JwtService(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<string> GenerateJwt(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user)).First())
        };
        
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(1),
            signingCredentials: cred
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        return jwt;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public async Task<string> RefreshToken(string refreshToken)
    {
        var user = await _userManager.Users.FirstAsync(u => u.RefreshToken.Equals(refreshToken));

        if (user == null)
        {
            // throw error
        }

        if (user.RefreshTokenExpires < DateTime.Now)
        {
            // throw error
        }
        
        return await GenerateJwt(user);
    }
}