using Backend.Api.Common;
using Backend.Contracts.Lessons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Lessons.Endpoints;

public class UpdateLessonItselfByIdEndpointMarker {
}

public static class UpdateLessonItselfByIdEndpoint {
    public const string Name = "UpdateLessonItselfById";

    public static IEndpointRouteBuilder MapUpdateLessonItselfById(this IEndpointRouteBuilder app) {
        app
            .MapPut(ApiEndpoints.Lessons.UpdateItselfById, Handle)
            .Produces(StatusCodes.Status204NoContent)
            .WithSummary("Update lesson itself by ID")
            .WithTags(ApiTags.Lessons)
            .WithName(Name);

        return app;
    }

    private static async Task<IResult> Handle(
        [FromServices] ILogger<UpdateLessonItselfByIdEndpointMarker> logger,
        [FromServices] ISender mediator,
        [FromRoute] Guid id,
        [FromBody] UpdateLessonItselfByIdRequest request
    ) {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("PUT {EndpointName} #{LessonId} received with {@Data}.", Name, id, request);

        var result = await mediator.Send(request.MapToCommand(id));

        if (result.IsError) {
            var errors = result.Errors;
            logger.LogInformation("PUT {EndpointName} #{LessonId} failed with {ErrorCount} errors.", Name, id,
                errors.Count);
            return ApiResults.Problem(errors);
        }

        logger.LogInformation("PUT {EndpointName} #{LessonId} succeeded.", Name, id);

        return Results.NoContent();
    }
}
