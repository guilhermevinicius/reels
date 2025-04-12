using FluentValidation;

namespace Reels.Backoffice.Application.UseCases.Categories;

public sealed class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be empty.");
    }
}