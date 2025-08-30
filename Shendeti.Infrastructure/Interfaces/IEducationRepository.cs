using Shendeti.Infrastructure.Entities;

namespace Shendeti.Infrastructure.Interfaces;

public interface IEducationRepository
{
    Task CreateAsync(Education entity);
    Task<Education> ReadAsync(int id);
    Task<List<Education>> ReadAllAsync();
    Task UpdateAsync(Education updatedEntity);
    Task DeleteAsync(Education entity);
    Task<List<Education>> ReadMyEducationsAsync(string doctorId);
}