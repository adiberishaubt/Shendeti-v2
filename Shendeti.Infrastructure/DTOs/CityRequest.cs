using Shendeti.Infrastructure.Entities;

namespace Shendeti.Infrastructure.DTOs;

public class CityRequest
{
    public string Name { get; set; }
    public int CountryId { get; set; }
    public Country? Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}