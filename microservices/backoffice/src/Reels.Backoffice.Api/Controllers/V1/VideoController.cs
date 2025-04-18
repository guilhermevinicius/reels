using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reels.Backoffice.Api.Controllers.V1.Requests;
using Reels.Backoffice.Application.UseCases.Videos;
using Reels.Backoffice.Domain.ValueObjects;

namespace Reels.Backoffice.Api.Controllers.V1;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/videos")]
public class VideoController(
    ISender sender)
    : BackofficeBaseController
{
    [HttpGet("{videoId:guid}")]
    public async Task<IActionResult> Get(Guid videoId, CancellationToken cancellationToken)
    {
        var query = new GetVideoQuery(videoId);
        var response = await sender.Send(query, cancellationToken);
        return CustomResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var query = new ListVideoQuery();
        var response = await sender.Send(query, cancellationToken);
        return CustomResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateVideoRequest request, IFormFile thumb, IFormFile thumbHalf,
        CancellationToken cancellationToken)
    {
        var command = new CreateVideoCommand(
           request.Title,
           request.Description,
           request.YearLaunched,
           request.Opened,
           request.Published,
           request.Duration,
           request.Rating,
           new MediaMetadata(
               thumb.FileName,
               thumb.ContentType,
               thumb.OpenReadStream()),
           new MediaMetadata(
               thumbHalf.FileName,
               thumbHalf.ContentType,
               thumbHalf.OpenReadStream()));

        var response = await sender.Send(command, cancellationToken);
        return CustomResponse(response);
    }
}