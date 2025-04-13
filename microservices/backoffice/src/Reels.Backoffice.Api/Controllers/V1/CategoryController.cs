using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reels.Backoffice.Api.Controllers.V1.Requests;
using Reels.Backoffice.Application.UseCases.Categories;

namespace Reels.Backoffice.Api.Controllers.V1;

[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/categories")]
public sealed class CategoryController(
    ISender sender)
    : BackofficeBaseController
{
    [HttpGet("{categoryId:guid}")]
    public async Task<IActionResult> GetCategories(Guid categoryId, CancellationToken cancellationToken)
    {
        var query = new GetCategoryQuery(categoryId);
        var response = await sender.Send(query, cancellationToken);
        return CustomResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var query = new ListCategoryQuery();
        var response = await sender.Send(query, cancellationToken);
        return CustomResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var response = await sender.Send(command, cancellationToken);
        return CustomResponse(response);
    }

    [HttpPut("{categoryId:guid}")]
    public async Task<IActionResult> Update(Guid categoryId, UpdateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCategoryCommand(
            categoryId,
            request.Name,
            request.Description,
            request.IsActive);

        var response = await sender.Send(command, cancellationToken);
        return CustomResponse(response);
    }

    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> Delete(Guid categoryId, CancellationToken cancellationToken)
    {
        var command = new DeleteCategoryCommand(categoryId);
        var response = await sender.Send(command, cancellationToken);
        return CustomResponse(response);
    }
}