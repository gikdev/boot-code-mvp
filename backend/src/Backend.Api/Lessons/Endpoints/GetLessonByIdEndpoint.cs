using Backend.Api.Common;
using Backend.App.Lessons.Queries.GetLessonById;
using Backend.Contracts.Lessons;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Lessons.Endpoints;

internal class GetLessonByIdEndpoint : EndpointBase {
    internal override string Name => "GetLessonById";

    internal override void MapEndpoint(IEndpointRouteBuilder app) {
        app
            .MapGet(ApiEndpoints.Lessons.GetById, Handle)
            .WithName(Name)
            .WithSummary("Get lesson by ID")
            .WithTags(ApiTags.Lessons)
            .Produces<LessonFullResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }

    private async Task<IResult> Handle(
        [FromServices] ILogger<GetLessonByIdEndpoint> logger,
        [FromServices] ISender mediator,
        [FromRoute] Guid id
    ) {
        logger.LogDebug("GET {EndpointName} #{LessonId} received.", Name, id);

        var lesson = await mediator.Send(new GetLessonByIdQuery(id));

        if (lesson is null) {
            var error = Error.NotFound("درس پیدا نشد.");
            logger.LogInformation("GET {EndpointName} #{LessonId} failed with 1 error (not found).", Name, id);
            return Problem(error);
        }

        logger.LogInformation("GET {EndpointName} #{LessonId} succeeded", Name, lesson.Id);
        return Results.Ok(lesson.MapToFullResponse());
    }
}
