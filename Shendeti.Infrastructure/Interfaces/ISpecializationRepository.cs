using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Infrastructure.Interfaces;

public interface ISpecializationRepository
{
    Task CreateAsync(Specialization entity);
    Task<Specialization> ReadAsync(int id);
    Task<List<Specialization>> ReadAllAsync();
    Task<PaginatedList<Specialization>> ReadAllAsync(int pageIndex, int pageSize);
    Task UpdateAsync(Specialization updatedEntity);
    Task DeleteAsync(Specialization entity);
    Task<List<Service>> ReadServicesAsync(int id);
}