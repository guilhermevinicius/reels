using FluentValidation;

namespace Reels.Backoffice.Application.UseCases.Videos;

public class GetVideoQueryValidator : AbstractValidator<GetVideoQuery>
{
    public GetVideoQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be empty.");
    }
}