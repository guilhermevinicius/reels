using FluentResults;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Application.UseCases.Categories;

internal sealed class DeleteCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<DeleteCategoryCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(command.Id, cancellationToken);
        if (category is null)
            return Result.Fail("Category not found");

        categoryRepository.DeleteAsync(category);
        return await unitOfWork.CommitAsync(cancellationToken);
    }
}