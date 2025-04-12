using FluentResults;
using MediatR;

namespace Reels.Backoffice.Application.SeedWorks.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;