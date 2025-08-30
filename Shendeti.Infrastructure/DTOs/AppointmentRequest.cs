using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.DTOs;

public class AppointmentRequest
{
    public string DoctorId { get; set; }
    public int SlotId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public MeetingType? MeetingType { get; set; }
    public string? MeetingPlace { get; set; }
}