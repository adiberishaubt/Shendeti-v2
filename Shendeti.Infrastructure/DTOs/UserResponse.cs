using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.DTOs;

public class UserResponse
{
    public string Id { get; set; }
    public string userName { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public string gender { get; set; }
    public string Role { get; set; }
    public BloodType? BloodType { get; set; }
    public bool GivesBlood { get; set; }
    public int XP { get; set; }
    public Level? Level { get; set; }
    public City? City { get; set; }
}