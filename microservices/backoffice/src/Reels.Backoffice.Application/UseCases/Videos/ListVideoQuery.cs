using Reels.Backoffice.Application.SeedWorks.Messaging;

namespace Reels.Backoffice.Application.UseCases.Videos;

public sealed class ListVideoQuery 
    : IQuery<IEnumerable<GetVideoResponse>>;