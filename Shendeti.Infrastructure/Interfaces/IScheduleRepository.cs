using Shendeti.Infrastructure.Entities;

namespace Shendeti.Infrastructure.Interfaces;

public interface IScheduleRepository
{
    Task CreateAsync(Schedule entity);
    Task<Schedule> ReadAsync(int id, bool includeDoctor);
    Task<List<Schedule>> ReadByDoctor(string doctorId);
    Task<List<Schedule>> ReadAllAsync(bool includeDoctor);
    Task UpdateAsync(Schedule updatedEntity);  
    Task DeleteAsync(Schedule entity);
}