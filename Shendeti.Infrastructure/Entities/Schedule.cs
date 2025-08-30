using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.Entities;

public class Schedule : IModel
{
    public int Id { get; set; }
    public Day Day { get; set; }
    public List<Slot> Slots { get; set; }
    public Doctor Doctor { get; set; }
}