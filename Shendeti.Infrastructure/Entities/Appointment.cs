using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.Entities;

public class Appointment : IModel
{
    public int Id { get; set; }
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
    public Slot Slot { get; set; }
    public string? MeetLink { get; set; }
    public DateTime AppointmentDate { get; set; }
    public MeetingType? MeetingType { get; set; }
    public string? MeetingPlace { get; set; }
}