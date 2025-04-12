using FluentValidation;

namespace Reels.Backoffice.Application.UseCases.Categories;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Description is required.");
    }
}