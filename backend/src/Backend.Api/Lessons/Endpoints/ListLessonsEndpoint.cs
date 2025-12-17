using Backend.Api.Common;
using Backend.App.Lessons.Queries.ListLessons;
using Backend.Contracts.Lessons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Lessons.Endpoints;

public class ListLessonsEndpointMarker {
}

public static class ListLessonsEndpoint {
    public const string Name = "ListLessons";

    public static IEndpointRouteBuilder MapListLessons(this IEndpointRouteBuilder app) {
        app
            .MapGet(ApiEndpoints.Lessons.List, Handle)
            .Produces<LessonListResponse>()
            .WithSummary("List lessons")
            .WithTags(ApiTags.Lessons)
            .WithName(Name);

        return app;
    }

    private static async Task<IResult> Handle(
        [FromServices] ILogger<ListLessonsEndpointMarker> logger,
        [FromServices] ISender mediator
    ) {
        logger.LogDebug("GET {EndpointName} received.", Name);

        var lessonList = await mediator.Send(new ListLessonsQuery());

        logger.LogInformation("POST {EndpointName} succeeded", Name);

        return Results.Ok(lessonList.MapToListResponse());
    }
}
