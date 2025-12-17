using Backend.Api.Common;
using Backend.Contracts.Lessons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Lessons.Endpoints;

public class CreateLessonEndpointMarker {
}

public static class CreateLessonEndpoint {
    public const string Name = "CreateLesson";

    public static IEndpointRouteBuilder MapCreateLesson(this IEndpointRouteBuilder app) {
        app
            .MapPost(ApiEndpoints.Lessons.Create, Handle)
            .Produces<LessonSmallResponse>(StatusCodes.Status201Created)
            .WithSummary("Create a new lesson")
            .WithTags(ApiTags.Lessons)
            .WithName(Name);

        return app;
    }

    private static async Task<IResult> Handle(
        [FromServices] ILogger<CreateLessonEndpointMarker> logger,
        [FromServices] ISender mediator,
        [FromBody] CreateLessonRequest request
    ) {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("POST {EndpointName} received with {@Data}.", Name, request);

        var result = await mediator.Send(request.MapToCommand());

        if (result.IsError) {
            var errors = result.Errors;
            logger.LogInformation("POST {EndpointName} failed with {ErrorCount} errors.", Name, errors.Count);
            return ApiResults.Problem(errors);
        }

        var lesson = result.Value;
        logger.LogInformation("POST {EndpointName} succeeded with LessonId {LessonId}", Name, lesson.Id);
        return Results.Ok(lesson.MapToSmallResponse());
    }
}
