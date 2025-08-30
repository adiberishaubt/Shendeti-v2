using Microsoft.EntityFrameworkCore;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Extensions;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly DataContext _dataContext;
    
    public CityRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task CreateAsync(City entity)
    {
        await _dataContext.Cities.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<City> ReadAsync(int id)
    {
        return await _dataContext.Cities.Include(c => c.Country).FirstOrThrowAsync(id);
    }

    public async Task<List<City>> ReadAllAsync()
    {
        return await _dataContext.Cities.Include(c => c.Country).ToListAsync();
    }

    public async Task<PaginatedList<City>> ReadAllAsync(int pageIndex, int pageSize)
    {
        var cities = await _dataContext.Cities
            .Include(c => c.Country)
            .OrderBy(b => b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var count = await _dataContext.Cities.CountAsync();
        var totalPages = (int) Math.Ceiling(count / (double) pageSize);

        return new PaginatedList<City> { Items = cities, PageIndex = pageIndex, TotalPages = totalPages};
    }

    public async Task UpdateAsync(City updatedEntity)
    {
        _dataContext.Cities.Update(updatedEntity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(City entity)
    {
        _dataContext.Cities.Remove(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<City> FindNearestCity(BloodRequest bloodRequest, HashSet<City> cities)
    {
        var sql =
            $"SELECT *, " +
            $"2 * ASIN(SQRT(POWER(SIN(({bloodRequest.City.Latitude} - ABS(Latitude)) * PI() / 180 / 2), 2) + \n    COS({bloodRequest.City.Latitude} * PI() / 180 ) * COS(ABS(Latitude) * PI() / 180) * POWER(SIN(({bloodRequest.City.Longitude} - Longitude) * PI() / 180 / 2), 2))) * 6371 AS Distance\n" +
            $"FROM Cities\n" +
            $"WHERE Id IN ({string.Join(",", cities.Select(c => c.Id))})\n" +
            $"ORDER BY Distance \n" +
            $"OFFSET {bloodRequest.Offset} ROWS\n" +
            $"FETCH NEXT 1 ROWS ONLY";

        bloodRequest.Offset++;
        await _dataContext.SaveChangesAsync();

        var city = _dataContext.Cities.FromSqlRaw(sql);
        return await city.FirstOrDefaultAsync();
    }
}