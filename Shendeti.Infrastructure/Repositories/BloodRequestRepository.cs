using Microsoft.EntityFrameworkCore;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Extensions;
using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.Repositories;

public class BloodRequestRepository : IBloodRequestRepository
{
    private readonly DataContext _dataContext;

    public BloodRequestRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task CreateAsync(BloodRequest entity)
    {
        await _dataContext.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<BloodRequest> ReadAsync(int id)
    {
        return await _dataContext.BloodRequests.Include(d => d.User).Include(d => d.City).FirstOrThrowAsync(id);
    }

    public async Task<List<BloodRequest>> ReadAllAsync()
    {
        return await _dataContext.BloodRequests.Include(d => d.User).Include(d => d.City).ToListAsync();
    }

    public async Task UpdateAsync(BloodRequest updatedEntity)
    {
        _dataContext.BloodRequests.Update(updatedEntity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(BloodRequest entity)
    {
        _dataContext.BloodRequests.Remove(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<BloodRequest>> ReadRequestsByStatusAsync(Status status)
    {
        return await _dataContext.BloodRequests
            .Include(b => b.City)
            .Include(b => b.User)
            .Where(b => b.Status == status)
            .OrderByDescending(b => b.Urgent)
            .ToListAsync();
    }

    public async Task<List<BloodRequest>> ReadWaitingRequestsAsync()
    {
        var fiveMinutesAgo = DateTime.Now - TimeSpan.FromMinutes(5);
            
        return await _dataContext.BloodRequests
            .Include(b => b.City)
            .Include(b => b.User)
            .Where(b => (int)b.Status == 3 && b.Urgent && fiveMinutesAgo >= b.DateTime)
            .ToListAsync();
    }
}