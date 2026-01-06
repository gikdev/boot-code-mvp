using Backend.Api.Common;
using Backend.Contracts.Lessons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Lessons.Endpoints;

internal class ChangeLessonsPositionsEndpoint : EndpointBase {
    internal override string Name => "ChangeLessonsPositions";

    internal override void MapEndpoint(IEndpointRouteBuilder app) {
        app
            .MapPatch(ApiEndpoints.Lessons.ChangePositions, Handle)
            .WithName(Name)
            .WithSummary("Change lessons positions")
            .WithTags(ApiTags.Lessons)
            .Produces(StatusCodes.Status204NoContent);
    }

    private async Task<IResult> Handle(
        [FromServices] ILogger<ChangeLessonsPositionsEndpoint> logger,
        [FromServices] ISender                                 mediator,
        [FromBody]     ChangeLessonsPositionsRequest           request
    ) {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("PATCH {EndpointName} received with {@Request}.", Name, request);

        var result = await mediator.Send(request.MapToCommand());

        if (result.IsError) {
            var errors = result.Errors;
            logger.LogInformation("PATCH {EndpointName} failed with {ErrorCount} errors.", Name, errors.Count);
            return Problem(errors);
        }

        logger.LogInformation("PATCH {EndpointName} succeeded.", Name);

        return Results.NoContent();
    }
}
