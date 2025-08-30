using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Domain.Interfaces;

public interface ILevelService
{
    Task CreateAsync(LevelRequest entity);
    Task<Level> ReadAsync(int id);
    Task<PaginatedList<Level>> ReadAllAsync(int pageIndex, int pageSize);
    Task UpdateAsync(int id, LevelRequest updatedEntity);
    Task DeleteAsync(int id);
}