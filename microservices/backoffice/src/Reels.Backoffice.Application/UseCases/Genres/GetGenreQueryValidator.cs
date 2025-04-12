using FluentValidation;

namespace Reels.Backoffice.Application.UseCases.Genres;

public sealed class GetGenreQueryValidator : AbstractValidator<GetGenreQuery>
{
    public GetGenreQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be empty.");
    }
}