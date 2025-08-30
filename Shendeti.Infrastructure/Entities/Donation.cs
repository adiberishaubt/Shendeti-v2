using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Entities;

public class Donation : IModel
{
    public int Id { get; set; }
    public User Receiver { get; set; }
    public User Donor { get; set; }
}