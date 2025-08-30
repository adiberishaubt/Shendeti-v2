using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Domain.Interfaces;

public interface IMailService
{
    Task<bool> SendMailAsync(MailData mailData);
    Task SendRequestMailAsync(PotentialDonor donor);
    Task SendCompletedRequestMailAsync(BloodRequest request, User donor);
    Task SendNoDonorFoundMailAsync(BloodRequest request);
}