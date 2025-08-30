using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.DTOs;

public class PotentialDonor
{
    public int RequestId { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public BloodType BloodType { get; set; }
    public string CityName { get; set; }
    public string Spitali { get; set; }
    public bool Urgent { get; set; }
}