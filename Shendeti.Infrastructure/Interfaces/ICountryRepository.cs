using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Infrastructure.Interfaces;

public interface ICountryRepository
{
    Task CreateAsync(Country entity);
    Task<Country> ReadAsync(int id);
    Task<Country> ReadAsync(string name);
    Task<List<Country>> ReadAllAsync();
    Task<PaginatedList<Country>> ReadAllAsync(int pageIndex, int pageSize);
    Task UpdateAsync(Country updatedEntity);
    Task DeleteAsync(Country entity);
    Task<List<City>> ReadCitiesAsync(int id);
}