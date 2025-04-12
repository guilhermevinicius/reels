using FluentResults;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.Models.Category;
using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Application.UseCases.Categories;

internal sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<CreateCategoryCommand, bool>
{
    public async Task<Result<bool>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = Category.Create(
            command.Name,
            command.Description);

        await categoryRepository.InsertAsync(category, cancellationToken);
        return await unitOfWork.CommitAsync(cancellationToken);
    }
}