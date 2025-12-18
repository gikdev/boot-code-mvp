using Backend.Api.Common;
using Backend.App.Lessons.Commands.DeleteLessonById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Lessons.Endpoints;

internal class DeleteLessonByIdEndpoint : EndpointBase {
    internal override string Name =>  "DeleteLessonById";

    internal override void MapEndpoint(IEndpointRouteBuilder app) {
        app
            .MapDelete(ApiEndpoints.Lessons.DeleteById, Handle)
            .WithName(Name)
            .WithSummary("Delete a lesson by ID")
            .WithTags(ApiTags.Lessons)
            // .Accepts("application/json")
            .Produces(StatusCodes.Status204NoContent);
    }

    private async Task<IResult> Handle(
        [FromServices] ILogger<DeleteLessonByIdEndpoint> logger,
        [FromServices] ISender mediator,
        [FromRoute] Guid id
    ) {
        logger.LogDebug("DELETE {EndpointName} #{LessonId} received.", Name, id);

        var result = await mediator.Send(new DeleteLessonByIdCommand(id));

        if (result.IsError) {
            var errors = result.Errors;
            logger.LogInformation("DELETE {EndpointName} #{LessonId} failed with {ErrorCount} errors.", Name, id,
                errors.Count);
            return Problem(errors);
        }

        logger.LogInformation("DELETE {EndpointName} #{LessonId} succeeded.", Name, id);
        return Results.NoContent();
    }
}
