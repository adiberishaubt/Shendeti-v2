namespace Shendeti.Infrastructure.Interfaces;

public interface IRepository<T> where T : class
{
    Task CreateAsync(T entity);
    Task<T> ReadAsync(int id, bool includeReferencedEntities);
    Task<List<T>> ReadAllAsync(bool includeReferencedEntities);
    Task UpdateAsync(int id, T entity);
    Task DeleteAsync(int id);
    
}