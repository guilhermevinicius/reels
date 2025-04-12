using FluentResults;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Application.UseCases.Categories;

internal sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<UpdateCategoryCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(command.Id, cancellationToken);
        if (category is null)
            return Result.Fail("Category not found");

        category.Update(command.Name, command.Description);

        if (command.IsActive is not null && command.IsActive != category.IsActive)
        {
            if ((bool)command.IsActive!)
                category.Activate();
            else
                category.Deactivate();
        }

        return await unitOfWork.CommitAsync(cancellationToken);
    }
}