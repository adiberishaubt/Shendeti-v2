using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Domain.Interfaces;

public interface IEducationService
{
    Task CreateAsync(EducationRequest entity, string doctorId);
    Task<Education> ReadAsync(int id);
    Task<List<Education>> ReadAllAsync();
    Task UpdateAsync(int id, EducationRequest updatedEntity, string doctorId);
    Task DeleteAsync(int id, string doctorId);
    Task<List<Education>> ReadMyEducationsAsync(string doctorId);
}