using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shendeti.Infrastructure.Exceptions;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static async Task<T> FirstOrThrowAsync<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> filter) where T : class, IModel
    {
        var item = await queryable.FirstOrDefaultAsync(filter);
        
        if (item == null)
        {
            throw new EntityNullException(typeof(T));
        }

        return item;
    } 
    
    public static async Task<T> FirstOrThrowAsync<T>(this IQueryable<T> queryable, int id) where T : class, IModel
    {
        var item = await queryable.FirstOrDefaultAsync(i => i.Id == id);
        
        if (item == null)
        {
            throw new EntityNullException(typeof(T), id);
        }

        return item;
    } 
}