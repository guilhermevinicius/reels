using Reels.Backoffice.Application.SeedWorks.Messaging;

namespace Reels.Backoffice.Application.UseCases.Videos;

public sealed record GetVideoQuery(
    Guid Id) 
    : IQuery<GetVideoResponse>;