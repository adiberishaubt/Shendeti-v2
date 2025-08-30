using Microsoft.EntityFrameworkCore;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Extensions;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Repositories;

public class LevelRepository : ILevelRepository
{
    private readonly DataContext _dataContext;

    public LevelRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task CreateAsync(Level entity)
    {
        await _dataContext.Levels.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<Level> ReadAsync(int id)
    {
        return await _dataContext.Levels.FirstOrThrowAsync(id);
    }

    public async Task<PaginatedList<Level>> ReadAllAsync(int pageIndex, int pageSize)
    {
        var levels = await _dataContext.Levels
            .OrderBy(b => b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var count = await _dataContext.Levels.CountAsync();
        var totalPages = (int) Math.Ceiling(count / (double) pageSize);

        return new PaginatedList<Level> { Items = levels, PageIndex = pageIndex, TotalPages = totalPages};
    }

    public async Task UpdateAsync(Level updatedEntity)
    {
        _dataContext.Levels.Update(updatedEntity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Level entity)
    {
        _dataContext.Levels.Remove(entity);
        await _dataContext.SaveChangesAsync();
    }
}