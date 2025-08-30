using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.DTOs;

public class ScheduleRequest
{
    public Day Day { get; set; }
    public List<SlotRequest> Slots { get; set; }
}