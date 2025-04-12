using FluentValidation;

namespace Reels.Backoffice.Application.UseCases.Categories;

public sealed class GetCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
{
    public GetCategoryQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be empty.");
    }
}