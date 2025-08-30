using Microsoft.EntityFrameworkCore;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Extensions;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly DataContext _dataContext;

    public ScheduleRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task CreateAsync(Schedule entity)
    {
        await _dataContext.Schedules.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<Schedule> ReadAsync(int id, bool includeDoctor)
    {
        if (includeDoctor)
        {
            return await _dataContext.Schedules.Include(s => s.Slots.OrderBy(sc => sc.StartTime)).Include(s => s.Doctor).FirstOrThrowAsync(id);
        }
        
        return await _dataContext.Schedules.Include(s => s.Slots.OrderBy(sc => sc.StartTime)).FirstOrThrowAsync(id);
    }

    public async Task<List<Schedule>> ReadByDoctor(string doctorId)
    {
        return await _dataContext.Schedules.Include(s => s.Slots.OrderBy(sc => sc.StartTime)).Where(s => s.Doctor.UserId == doctorId).ToListAsync();
    }

    public async Task<List<Schedule>> ReadAllAsync(bool includeDoctor)
    {
        if (includeDoctor)
        {
            return await _dataContext.Schedules.Include(s => s.Slots.OrderBy(sc => sc.StartTime)).Include(s => s.Doctor).ToListAsync();
        }
        
        return await _dataContext.Schedules.Include(s => s.Slots.OrderBy(sc => sc.StartTime)).ToListAsync();
    }

    public async Task UpdateAsync(Schedule updatedEntity)
    {
        _dataContext.Schedules.Update(updatedEntity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Schedule entity)
    {
        _dataContext.Schedules.Remove(entity);
        await _dataContext.SaveChangesAsync();
    }
}