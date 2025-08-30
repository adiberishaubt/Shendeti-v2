
using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.Entities;

public class BloodRequest : IModel
{
    public int Id { get; set; }
    public User User { get; set; }
    public City City { get; set; }
    public string Spitali { get; set; }
    public BloodType BloodType { get; set; }
    public bool MailSent { get; set; } = false;
    public bool Urgent { get; set; }
    public Status Status { get; set; }
    public int Offset { get; set; } = 0;
    public DateTime DateTime { get; set; }
}