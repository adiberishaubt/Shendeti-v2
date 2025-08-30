using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Domain.Interfaces;

public interface IServiceService
{
    Task CreateAsync(ServiceRequest entity);
    Task<Service> ReadAsync(int id);
    Task<PaginatedList<Service>> ReadAllAsync(int pageIndex, int pageSize);
    Task UpdateAsync(int id, ServiceRequest updatedEntity);
    Task DeleteAsync(int id);
}