using Microsoft.EntityFrameworkCore;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.Models.Category;
using Reels.Backoffice.Persistence.Configurations;

namespace Reels.Backoffice.Persistence.Repository;

internal sealed class CategoryRepository(DataContext context) : ICategoryRepository
{
    private readonly DbSet<Category> _categories = context.Set<Category>();

    public async Task InsertAsync(Category entity, CancellationToken cancellationToken)
    {
        await _categories.AddAsync(entity, cancellationToken);
    }

    public void DeleteAsync(Category entity)
    {
        _categories.Remove(entity);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _categories.FirstOrDefaultAsync(x => 
            x.Id == id, cancellationToken);
    }
}