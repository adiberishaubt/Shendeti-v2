using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.DTOs;

public class BloodRequestDto
{
    public int CityId { get; set; }
    public string Spitali { get; set; }
    public BloodType BloodType { get; set; }
    public bool Urgent { get; set; }
    public Status Status { get; set; }
}