using Microsoft.EntityFrameworkCore;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Extensions;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Repositories;

public class SpecializationRepository : ISpecializationRepository
{
    private readonly DataContext _dataContext;

    public SpecializationRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task CreateAsync(Specialization entity)
    {
        await _dataContext.Specializations.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<Specialization> ReadAsync(int id)
    {
        return await _dataContext.Specializations.FirstOrThrowAsync(id);
    }

    public async Task<List<Specialization>> ReadAllAsync()
    {
        return await _dataContext.Specializations.ToListAsync();
    }

    public async Task<PaginatedList<Specialization>> ReadAllAsync(int pageIndex, int pageSize)
    {
        var specializations = await _dataContext.Specializations
            .OrderBy(b => b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var count = await _dataContext.Specializations.CountAsync();
        var totalPages = (int) Math.Ceiling(count / (double) pageSize);

        return new PaginatedList<Specialization> { Items = specializations, PageIndex = pageIndex, TotalPages = totalPages};
    }

    public async Task UpdateAsync(Specialization updatedEntity)
    {
        _dataContext.Specializations.Update(updatedEntity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Specialization specialization)
    {
        _dataContext.Remove(specialization);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<Service>> ReadServicesAsync(int id)
    {
        return await _dataContext.Services.Where(s => s.Specialization.Id == id).ToListAsync();
    }
}