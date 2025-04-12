using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Persistence.Configurations;

namespace Reels.Backoffice.Persistence.Repository.Generics;

internal class RepositoryQuery(DataContext context) : IRepositoryQuery
{
    public IQueryable<T> QueryAsNoTracking<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
    {
        var query = context.Set<T>().AsQueryable().AsNoTrackingWithIdentityResolution();

        if (includeProperties != null)
        {
            query = includeProperties.Aggregate(query, (current, includeProperty) =>
                current.Include(includeProperty));
        }

        return query;
    }

    public IQueryable<T> Query<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
    {
        var query = context.Set<T>().AsQueryable();

        if (includeProperties != null)
        {
            query = includeProperties.Aggregate(query, (current, includeProperty) =>
                current.Include(includeProperty));
        }

        return query;
    }

    public IQueryable<T> QueryIncludeStringProperties<T>(params string[] includeProperties) where T : class
    {
        var query = context.Set<T>().AsQueryable();
        if (includeProperties is { Length: > 0 })
            query = includeProperties.Aggregate(query, (current, includeProperty) =>
                current.Include(includeProperty));

        return query;
    }
}