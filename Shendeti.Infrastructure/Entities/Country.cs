using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Entities;

public class Country : IModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<City> Cities { get; set; }
}