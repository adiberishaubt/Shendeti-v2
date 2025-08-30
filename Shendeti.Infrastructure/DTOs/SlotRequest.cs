namespace Shendeti.Infrastructure.DTOs;

public class SlotRequest
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int ScheduleId { get; set; }
}