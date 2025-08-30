using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.DTOs;

public class DoctorResponse
{
    public string Id { get; set; }
    public string userName { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public string gender { get; set; }
    public BloodType BloodType { get; set; }
    public int XP { get; set; }
    public Level Level { get; set; }
    public City City { get; set; }
    public List<Education> Educations { get; set; }
    public List<Specialization> Specializations { get; set; }
    public List<Service> Services { get; set; }
    public List<Schedule> Schedules { get; set; }
}