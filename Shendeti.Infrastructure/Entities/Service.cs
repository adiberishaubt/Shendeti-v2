using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Entities;

public class Service : IModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Specialization Specialization { get; set; }
}