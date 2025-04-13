using System.Net;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Reels.Backoffice.Api.DTOs;

namespace Reels.Backoffice.Api.Controllers;

[ApiController]
public abstract class BackofficeBaseController : ControllerBase
{
      protected ActionResult CustomResponse(ResultBase response, HttpStatusCode httpStatusCode)
    {
        return BuildCustomResponse(response, response.IsSuccess, httpStatusCode);
    }

    protected ActionResult CustomResponse<T>(Result<T> response, HttpStatusCode httpStatusCode)
    {
        return BuildCustomResponse(response, response.ValueOrDefault, httpStatusCode);
    }
    
    protected ActionResult CustomResponse<T>(Result<T> response)
    {
        return BuildCustomResponse(response, response.ValueOrDefault, HttpStatusCode.OK);
    }

    protected ActionResult CustomResponse<T>(PagerResponse<T> response)
    {
        var result = Result.Ok(response);
        return BuildCustomResponse(result, response, HttpStatusCode.OK);
    }

    private ActionResult BuildCustomResponse<TValue>(IResultBase response, TValue value, HttpStatusCode httpStatusCode)
    {
        if (response.IsSuccess)
            return httpStatusCode switch
            {
                HttpStatusCode.Created => Created(string.Empty, value),
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.OK => Ok(BuildSuccessResponse(httpStatusCode, value)),
                _ => Ok()
            };

        var (isNotFound, error) = IsNotFound(response);
        return isNotFound switch
        {
            true => BuildNotFoundResponse(error!),
            _ => BuildBadRequestResponse(response)
        };
    }

    private static CustomResponse BuildSuccessResponse<TData>(HttpStatusCode httpStatusCode, TData data)
    {
        return new CustomResponse(true, (int)httpStatusCode, data, null, DateTimeOffset.UtcNow);
    }

    private BadRequestObjectResult BuildBadRequestResponse(IResultBase result)
    {
        var messages = result.Errors.Select(e => e.Message).ToList();
        var resultFail = new CustomResponse(false, (int)HttpStatusCode.BadRequest, null, messages,
            DateTimeOffset.UtcNow);

        return BadRequest(resultFail);
    }

    private NotFoundObjectResult BuildNotFoundResponse(IReason error)
    {
        return NotFound(string.IsNullOrEmpty(error.Message) ? string.Empty : error.Message);
    }

    private static (bool, IError) IsNotFound(IResultBase result)
    {
        var error = result.Errors
            .FirstOrDefault(error => error.Metadata.Any(pair => pair.Key == $"{(int)HttpStatusCode.NotFound}"));

        return (error != null, error);
    }
}