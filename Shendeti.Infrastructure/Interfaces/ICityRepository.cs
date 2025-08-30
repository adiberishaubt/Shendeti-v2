using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Infrastructure.Interfaces;

public interface ICityRepository
{
    Task CreateAsync(City entity);
    Task<City> ReadAsync(int id);
    Task<List<City>> ReadAllAsync();
    Task<PaginatedList<City>> ReadAllAsync(int pageIndex, int pageSize);
    Task UpdateAsync(City updatedEntity);
    Task DeleteAsync(City entity);
    Task<City> FindNearestCity(BloodRequest bloodRequest, HashSet<City> cities);
}