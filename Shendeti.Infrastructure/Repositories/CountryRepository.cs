using Microsoft.EntityFrameworkCore;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Extensions;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly DataContext _dataContext;
    
    public CountryRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task CreateAsync(Country entity)
    {
        await _dataContext.Countries.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<Country> ReadAsync(int id)
    {
        return await _dataContext.Countries.FirstOrThrowAsync(id);
    }

    public async Task<List<Country>> ReadAllAsync()
    {
        return await _dataContext.Countries.ToListAsync();
    }

    public async Task<PaginatedList<Country>> ReadAllAsync(int pageIndex, int pageSize)
    {
        var countries = await _dataContext.Countries
            .OrderBy(b => b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var count = await _dataContext.Countries.CountAsync();
        var totalPages = (int) Math.Ceiling(count / (double) pageSize);

        return new PaginatedList<Country> { Items = countries, PageIndex = pageIndex, TotalPages = totalPages};
    }

    public async Task UpdateAsync(Country updatedEntity)
    {
        _dataContext.Update(updatedEntity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Country entity)
    {
        _dataContext.Countries.Remove(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<City>> ReadCitiesAsync(int id)
    {
        return await _dataContext.Cities.Where(c => c.Country.Id == id).ToListAsync();
    }

    public async Task<Country> ReadAsync(string name)
    {
        return await _dataContext.Countries.FirstOrDefaultAsync(c => c.Name == name);
    }
}