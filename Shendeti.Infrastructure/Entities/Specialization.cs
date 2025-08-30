using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Entities;

public class Specialization : IModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}