namespace Reels.Backoffice.Domain.Exceptions;

public class EntityValidationException : Exception
{
    public IReadOnlyCollection<string>? Errors { get; }
    public EntityValidationException(
        string? message, 
        IReadOnlyCollection<string>? errors = null
    ) : base(message) 
        => Errors = errors;
}