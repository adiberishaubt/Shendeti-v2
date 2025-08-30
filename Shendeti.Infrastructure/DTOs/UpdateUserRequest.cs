using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.DTOs;

public class UpdateUserRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public BloodType BloodType { get; set; }
    public int CityId { get; set; }
}