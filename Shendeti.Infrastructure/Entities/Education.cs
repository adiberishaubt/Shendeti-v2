using System.ComponentModel.DataAnnotations.Schema;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Entities;

public class Education : IModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    [ForeignKey("Doctor")]
    public string DoctorId { get; set; }
    public Doctor Doctor { get; set; }
}