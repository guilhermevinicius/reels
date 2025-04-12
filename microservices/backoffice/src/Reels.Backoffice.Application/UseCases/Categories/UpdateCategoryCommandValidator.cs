using FluentValidation;

namespace Reels.Backoffice.Application.UseCases.Categories;

public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .NotEqual(Guid.Empty);

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is required.");
    }
}