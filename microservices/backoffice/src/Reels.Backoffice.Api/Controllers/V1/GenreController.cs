using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reels.Backoffice.Api.Controllers.V1.Requests;
using Reels.Backoffice.Application.UseCases.Genres;

namespace Reels.Backoffice.Api.Controllers.V1;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/genres")]
public class GenreController(
    ISender sender) 
    : BackofficeBaseController
{
    [HttpGet("{genreId:guid}")]
    public async Task<IActionResult> GetCategories(Guid genreId, CancellationToken cancellationToken)
    {
        var query = new GetGenreQuery(genreId);
        var response = await sender.Send(query, cancellationToken);
        return CustomResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var query = new ListGenreQuery();
        var response = await sender.Send(query, cancellationToken);
        return CustomResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateGenreCommand command, CancellationToken cancellationToken)
    {
        var response = await sender.Send(command, cancellationToken);
        return CustomResponse(response);
    }

    [HttpPut("{genreId:guid}")]
    public async Task<IActionResult> Update(Guid genreId, UpdateGenreRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateGenreCommand(
            genreId,
            request.Name,
            request.IsActive);

        var response = await sender.Send(command, cancellationToken);
        return CustomResponse(response);
    }

    [HttpDelete("{genreId:guid}")]
    public async Task<IActionResult> Delete(Guid genreId, CancellationToken cancellationToken)
    {
        var command = new DeleteGenreCommand(genreId);
        var response = await sender.Send(command, cancellationToken);
        return CustomResponse(response);
    }
}