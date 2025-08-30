using Microsoft.AspNetCore.Identity;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.Entities;

public class User : IdentityUser
{
    public bool GivesBlood { get; set; }
    public int? xp { get; set; } = 0;
    public Level? Level { get; set; }
    public BloodType? BloodType { get; set; }
    public string Gender { get; set; }
    public City? City { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }
}