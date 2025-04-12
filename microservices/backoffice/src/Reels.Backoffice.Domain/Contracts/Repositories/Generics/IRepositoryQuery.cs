using System.Linq.Expressions;

namespace Reels.Backoffice.Domain.Contracts.Repositories.Generics;

public interface IRepositoryQuery
{
    IQueryable<T> Query<T>(params Expression<Func<T, object>>[] includeProperties) where T : class;
    IQueryable<T> QueryAsNoTracking<T>(params Expression<Func<T, object>>[] includeProperties) where T : class;
}