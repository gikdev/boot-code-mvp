using Backend.Api.Common;
using Backend.App.Lessons.Queries.ListLessons;
using Backend.Contracts.Lessons;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Lessons.Endpoints;

internal class ListLessonsEndpoint : EndpointBase {
    internal override string Name => "ListLessons";

    internal override void MapEndpoint(IEndpointRouteBuilder app) {
        app
            .MapGet(ApiEndpoints.Lessons.List, Handle)
            .WithName(Name)
            .WithSummary("List lessons")
            .WithTags(ApiTags.Lessons)
            .Produces<LessonListResponse>();
    }

    private async Task<IResult> Handle(
        [FromServices] ILogger<ListLessonsEndpoint> logger,
        [FromServices] ISender                      mediator
    ) {
        logger.LogDebug("GET {EndpointName} received.", Name);

        var lessonList = await mediator.Send(new ListLessonsQuery());

        logger.LogInformation("POST {EndpointName} succeeded", Name);

        return Results.Ok(lessonList.MapToListResponse());
    }
}
