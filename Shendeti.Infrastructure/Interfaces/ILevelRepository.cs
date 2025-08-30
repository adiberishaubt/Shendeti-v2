using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Infrastructure.Interfaces;

public interface ILevelRepository
{
    Task CreateAsync(Level entity);
    Task<Level> ReadAsync(int id);
    Task<PaginatedList<Level>> ReadAllAsync(int pageIndex, int pageSize);
    Task UpdateAsync(Level updatedEntity);
    Task DeleteAsync(Level entity);
}