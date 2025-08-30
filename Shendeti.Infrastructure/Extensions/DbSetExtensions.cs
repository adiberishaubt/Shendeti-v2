using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shendeti.Infrastructure.Exceptions;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Extensions;

public static class DbSetExtensions
{
    public static async Task<T> FirstOrThrowAsync<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> filter) where T : class
    {
        var item = await dbSet.FirstOrDefaultAsync(filter);
        
        if (item == null)
        {
            throw new EntityNullException(typeof(T));
        }

        return item;
    } 
    
    public static async Task<T> FirstOrThrowAsync<T>(this DbSet<T> dbSet, int id) where T : class, IModel
    {
        var item = await dbSet.FirstOrDefaultAsync(i => i.Id == id);
        
        if (item == null)
        {
            throw new EntityNullException(typeof(T), id);
        }

        return item;
    } 
    
}