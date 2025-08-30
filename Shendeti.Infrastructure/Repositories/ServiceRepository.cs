using Microsoft.EntityFrameworkCore;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Extensions;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly DataContext _dataContext;

    public ServiceRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task CreateAsync(Service entity)
    {
        await _dataContext.Services.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<Service> ReadAsync(int id)
    {
        return await _dataContext.Services.Include(s => s.Specialization).FirstOrThrowAsync(id);
    }

    public async Task<PaginatedList<Service>> ReadAllAsync(int pageIndex, int pageSize)
    {
        var services = await _dataContext.Services
            .Include(s => s.Specialization)
            .OrderBy(b => b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var count = await _dataContext.Services.CountAsync();
        var totalPages = (int) Math.Ceiling(count / (double) pageSize);

        return new PaginatedList<Service> { Items = services, PageIndex = pageIndex, TotalPages = totalPages};
    }

    public async Task UpdateAsync(Service updatedEntity)
    {
        _dataContext.Services.Update(updatedEntity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Service entity)
    {
        _dataContext.Services.Remove(entity);
        await _dataContext.SaveChangesAsync();
    }
}