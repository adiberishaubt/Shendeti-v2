using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shendeti.Infrastructure.Entities;

public class Doctor
{
    public string Office { get; set; }
    [Key]
    [ForeignKey("User")]
    public string UserId { get; set; }
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public List<Education> Educations { get; set; }
    public List<Specialization> Specializations { get; set; } = new ();
    public List<Service> Services { get; set; } = new ();
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public List<Schedule> Schedules { get; set; } = new ();
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public User User { get; set; }
}