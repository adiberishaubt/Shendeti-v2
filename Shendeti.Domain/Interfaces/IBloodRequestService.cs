using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Domain.Interfaces;

public interface IBloodRequestService
{
    Task CreateAsync(BloodRequestDto bloodRequest, string id);
    Task<BloodRequest> ReadAsync(int id);
    Task<List<BloodRequest>> ReadAllAsync();
    Task UpdateAsync(int id, BloodRequest entity);
    Task DeleteAsync(int id, string patientId);
    Task ProcessRequestsAsync();
    Task ProcessWaitingRequestsAsync();
    Task AcceptRequestAsync(int id, string donorId);
    Task ConfirmRequestCompletionAsync(int id, string donorId);
}