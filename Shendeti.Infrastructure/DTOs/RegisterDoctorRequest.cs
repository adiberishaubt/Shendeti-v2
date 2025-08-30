namespace Shendeti.Infrastructure.DTOs;

public class RegisterDoctorRequest : RegisterUserRequest
{
    public string Office { get; set; }
    public List<int> Specializations { get; set; }
    public List<int> Services { get; set; }
    public List<string> Educations { get; set; }
    public List<ScheduleRequest> Schedules { get; set; }
}