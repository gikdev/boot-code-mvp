using Backend.Api.Common;
using Backend.Contracts.Lessons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Lessons.Endpoints;

internal class UpdateLessonContentByIdEndpoint : EndpointBase {
    internal override string Name => "UpdateLessonContentById";

    internal override void MapEndpoint(IEndpointRouteBuilder app) {
        app
            .MapPut(ApiEndpoints.Lessons.UpdateContentById, Handle)
            .WithName(Name)
            .WithSummary("Update lesson content by ID")
            .WithTags(ApiTags.Lessons)
            .Produces(StatusCodes.Status204NoContent);
    }

    private async Task<IResult> Handle(
        [FromServices] ILogger<UpdateLessonContentByIdEndpoint> logger,
        [FromServices] ISender mediator,
        [FromRoute] Guid id,
        [FromBody] UpdateLessonContentByIdRequest request
    ) {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("PUT {EndpointName} #{LessonId} received with {@Request}.", Name, id, request);

        var result = await mediator.Send(request.MapToCommand(id));

        if (result.IsError) {
            var errors = result.Errors;
            logger.LogInformation("PUT {EndpointName} #{LessonId} failed with {ErrorCount} errors.", Name, id, errors.Count);
            return Problem(errors);
        }

        logger.LogInformation("PUT {EndpointName} #{LessonId} succeeded.", Name, id);

        return Results.NoContent();
    }
}
