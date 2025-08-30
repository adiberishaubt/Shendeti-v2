using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Entities;

public class City : IModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Country Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var otherCity = (City)obj;
        return Id == otherCity.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    
}