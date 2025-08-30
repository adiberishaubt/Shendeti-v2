using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Entities;

public class Level : IModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int RequiredXP { get; set; }
}