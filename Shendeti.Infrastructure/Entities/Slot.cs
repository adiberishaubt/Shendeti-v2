using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Entities;

public class Slot : IModel
{
    public int Id { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int ScheduleId { get; set; }
    public Schedule Schedule { get; set; }
}