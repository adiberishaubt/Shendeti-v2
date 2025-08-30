using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.Exceptions;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Repositories;

public abstract class Repository<T> : IRepository<T> where T : class, IModel
{
    private readonly DataContext _dbContext;
    private readonly DbSet<T> _dbSet;

    protected Repository(DataContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }
    
    public virtual async Task<T> ReadAsync(int id, bool includeReferencedEntities)
    {
        var entitySet = includeReferencedEntities ? AddDetailIncludes(_dbSet) : _dbSet;
        var entity = await entitySet.FirstOrDefaultAsync(e => e.Id == id);
        CheckEntityIsNotNull(entity, id);
        return entity;
    }
    
    protected virtual IQueryable<T> AddDetailIncludes(DbSet<T> dbSet)
    {
        return dbSet;
    }

    public virtual async Task<List<T>> ReadAllAsync(bool includeReferencedEntities)
    {
        var entitySet = includeReferencedEntities ? AddDetailIncludes(_dbSet) : _dbSet;
        return await entitySet.ToListAsync();
    }

    public virtual async Task CreateAsync(T entity)
    {
        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync();
    }
    
    public virtual async Task UpdateAsync(int id, T entity)
    {
        var dbEntity = await LoadDbEntityForUpdate(id);
        ModifyChangedDbEntityProperties(dbEntity, entity);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<T> LoadDbEntityForUpdate(int id)
    {
        var dbSetWithIncludes = AddDetailIncludes(_dbSet);
        var dbEntity = await dbSetWithIncludes.SingleOrDefaultAsync(x => x.Id == id);
        CheckEntityIsNotNull(dbEntity, id);
        
        return dbEntity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbContext.FindAsync<T>(id);
        CheckEntityIsNotNull(entity, id);
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
    
    private static void ModifyChangedDbEntityProperties(T dbEntity, T entity)
    {
        var propertiesToUpdate = entity.GetType()
            .GetProperties()
            .Where(x => x.Name != "Id" && (!x.PropertyType.IsClass || x.PropertyType == typeof(string)));

        foreach (var property in propertiesToUpdate)
        {
            var sourceValue = property.GetValue(entity);
            var destinationValue = property.GetValue(dbEntity);
            if (!ArePropertyValuesEqual(sourceValue, destinationValue))
            {
                property.SetValue(dbEntity, sourceValue);
            }
        }
    }
    
    public static void CheckEntityIsNotNull(T? entity, int entityId)
    {
        if (entity == null)
        {
            throw new EntityNullException(typeof(T), entityId);
        }
    }


    private static bool ArePropertyValuesEqual(object sourceValue, object destinationValue)
    {
        if (sourceValue == null)
        {
            return destinationValue == null;
        }

        return sourceValue.Equals(destinationValue);
    }
}
