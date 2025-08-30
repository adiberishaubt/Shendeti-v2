using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Domain.Interfaces;

public interface ICityService
{
    Task CreateAsync(CityRequest entity);
    Task<City> ReadAsync(int id);
    Task<List<City>> ReadAllAsync();
    Task<PaginatedList<City>> ReadAllAsync(int pageIndex, int pageSize);
    Task UpdateAsync(int id, CityRequest updatedEntity);
    Task DeleteAsync(int id);
    Task<City> FindNearestCity(BloodRequest bloodRequest, HashSet<City> cities);
}