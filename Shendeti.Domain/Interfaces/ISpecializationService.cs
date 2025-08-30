using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Domain.Interfaces;

public interface ISpecializationService
{
    Task CreateAsync(SpecializationRequest entity);
    Task<Specialization> ReadAsync(int id);
    Task<List<Specialization>> ReadAllAsync();
    Task<PaginatedList<Specialization>> ReadAllAsync(int pageIndex, int pageSize);
    Task UpdateAsync(int id, SpecializationRequest updatedEntity);
    Task DeleteAsync(int id);
    Task<List<Service>> ReadServicesAsync(int id);
}