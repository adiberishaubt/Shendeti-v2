using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Infrastructure.Interfaces;

public interface IServiceRepository
{
    Task CreateAsync(Service entity);
    Task<Service> ReadAsync(int id);
    Task<PaginatedList<Service>> ReadAllAsync(int pageIndex, int pageSize);
    Task UpdateAsync(Service updatedEntity);
    Task DeleteAsync(Service entity);
}