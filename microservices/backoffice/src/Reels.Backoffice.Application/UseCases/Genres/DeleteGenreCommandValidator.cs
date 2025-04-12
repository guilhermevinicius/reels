using FluentValidation;

namespace Reels.Backoffice.Application.UseCases.Genres;

public sealed class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be empty.");
    }
}