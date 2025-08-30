using Microsoft.EntityFrameworkCore;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Extensions;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Repositories;

public class EducationRepository : IEducationRepository
{
    private readonly DataContext _dataContext;

    public EducationRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    /*public override async Task UpdateAsync(int id, Education entity)
    {
        var entityFetched = await _dbContext.Educations.FirstOrThrowAsync(e => e.Id == id);

        if (entityFetched.DoctorId != entity.DoctorId)
        {
            throw new Exception("This education is not yours");
        }
        
        await base.UpdateAsync(id, entity);
    }*/

    public async Task CreateAsync(Education entity)
    {
        await _dataContext.Educations.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<Education> ReadAsync(int id)
    {
        return await _dataContext.Educations.Include(e => e.Doctor).FirstOrThrowAsync(id);
    }

    public async Task<List<Education>> ReadAllAsync()
    {
        return await _dataContext.Educations.Include(e => e.Doctor).ToListAsync();
    }

    public async Task UpdateAsync(Education updatedEntity)
    {
        _dataContext.Educations.Update(updatedEntity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Education entity)
    {
        _dataContext.Educations.Remove(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<Education>> ReadMyEducationsAsync(string doctorId)
    {
        return await _dataContext.Educations.Where(e => e.DoctorId == doctorId).ToListAsync();
    }
}