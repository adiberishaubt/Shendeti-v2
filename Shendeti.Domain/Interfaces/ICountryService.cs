using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Domain.Interfaces;

public interface ICountryService
{
    Task CreateAsync(CountryRequest entity);
    Task<Country> ReadAsync(int id);
    Task<List<Country>> ReadAllAsync();
    Task<PaginatedList<Country>> ReadAllAsync(int pageIndex, int pageSize);
    Task UpdateAsync(int id, CountryRequest entity);
    Task DeleteAsync(int id);
    Task<List<City>> ReadCitiesAsync(int id);
}