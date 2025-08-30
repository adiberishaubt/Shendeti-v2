using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Domain.Interfaces;

public interface IScheduleService
{
    Task CreateAsync(ScheduleRequest entity, string doctorId);
    Task<Schedule> ReadAsync(int id, bool includeDoctor);
    Task<List<Schedule>> ReadAllAsync(bool includeDoctor);
    Task UpdateAsync(int id, UpdateScheduleRequest updatedEntity, string doctorId);
    Task DeleteAsync(int id, string doctorId);
    Task<List<Schedule>> ReadByDoctor(string doctorId);
}