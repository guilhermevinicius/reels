using FluentResults;
using MediatR;

namespace Reels.Backoffice.Application.SeedWorks.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;