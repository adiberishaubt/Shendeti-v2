using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.Interfaces;

public interface IBloodRequestRepository
{
    Task CreateAsync(BloodRequest entity);
    Task<BloodRequest> ReadAsync(int id);
    Task<List<BloodRequest>> ReadAllAsync();
    Task UpdateAsync(BloodRequest updatedEntity);
    Task DeleteAsync(BloodRequest entity);
    Task<List<BloodRequest>> ReadRequestsByStatusAsync(Status status);
    Task<List<BloodRequest>> ReadWaitingRequestsAsync();
}