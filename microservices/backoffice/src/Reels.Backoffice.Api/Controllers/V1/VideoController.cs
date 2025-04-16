using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reels.Backoffice.Application.UseCases.Videos;

namespace Reels.Backoffice.Api.Controllers.V1;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/videos")]
public class VideoController(
    ISender sender)
    : BackofficeBaseController
{
    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var query = new ListVideoQuery();
        var response = await sender.Send(query, cancellationToken);
        return CustomResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVideoCommand command, CancellationToken cancellationToken)
    {
        var response = await sender.Send(command, cancellationToken);
        return CustomResponse(response);
    }
}