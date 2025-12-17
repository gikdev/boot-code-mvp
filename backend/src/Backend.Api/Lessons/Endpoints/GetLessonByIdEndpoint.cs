using Backend.Api.Common;
using Backend.App.Lessons.Queries.GetLessonById;
using Backend.Contracts.Lessons;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Lessons.Endpoints;

public class GetLessonByIdEndpointMarker {
}

public static class GetLessonByIdEndpoint {
    public const string Name = "GetLessonById";

    public static IEndpointRouteBuilder MapGetLessonById(this IEndpointRouteBuilder app) {
        app
            .MapGet(ApiEndpoints.Lessons.GetById, Handle)
            .Produces<LessonFullResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get lesson by ID")
            .WithTags(ApiTags.Lessons)
            .WithName(Name);

        return app;
    }

    private static async Task<IResult> Handle(
        [FromServices] ILogger<GetLessonByIdEndpointMarker> logger,
        [FromServices] ISender mediator,
        [FromRoute] Guid id
    ) {
        logger.LogDebug("GET {EndpointName} #{LessonId} received.", Name, id);

        var lesson = await mediator.Send(new GetLessonByIdQuery(id));

        if (lesson is null) {
            List<Error> errors = [Error.NotFound("درس پیدا نشد.")];
            logger.LogInformation("GET {EndpointName} #{LessonId} failed with {ErrorCount} errors.", Name, id,
                errors.Count);
            return ApiResults.Problem(errors);
        }

        logger.LogInformation("GET {EndpointName} #{LessonId} succeeded", Name, lesson.Id);
        return Results.Ok(lesson.MapToFullResponse());
    }
}
