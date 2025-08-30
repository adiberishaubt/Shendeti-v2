using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.DTOs;

public class RegisterUserRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public bool GivesBlood { get; set; }
    public int CityId { get; set; }
    public string Gender { get; set; }
    public BloodType BloodType { get; set; }
}