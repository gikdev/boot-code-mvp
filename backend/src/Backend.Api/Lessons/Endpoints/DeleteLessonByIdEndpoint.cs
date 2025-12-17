using Backend.Api.Common;
using Backend.App.Lessons.Commands.DeleteLessonById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Lessons.Endpoints;

public class DeleteLessonByIdEndpointMarker {
}

public static class DeleteLessonByIdEndpoint {
    public const string Name = "DeleteLessonById";

    public static IEndpointRouteBuilder MapDeleteLessonById(this IEndpointRouteBuilder app) {
        app
            .MapDelete(ApiEndpoints.Lessons.DeleteById, Handle)
            .Produces(StatusCodes.Status204NoContent)
            .WithSummary("Delete a lesson by ID")
            .WithTags(ApiTags.Lessons)
            .WithName(Name);

        return app;
    }

    private static async Task<IResult> Handle(
        [FromServices] ILogger<DeleteLessonByIdEndpointMarker> logger,
        [FromServices] ISender mediator,
        [FromRoute] Guid id
    ) {
        logger.LogDebug("DELETE {EndpointName} #{LessonId} received.", Name, id);

        var result = await mediator.Send(new DeleteLessonByIdCommand(id));

        if (result.IsError) {
            var errors = result.Errors;
            logger.LogInformation("DELETE {EndpointName} #{LessonId} failed with {ErrorCount} errors.", Name, id,
                errors.Count);
            return ApiResults.Problem(errors);
        }

        logger.LogInformation("DELETE {EndpointName} #{LessonId} succeeded.", Name, id);
        return Results.NoContent();
    }
}
