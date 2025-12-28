using Backend.Api.Common;
using Backend.Contracts.Lessons;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Lessons.Endpoints;

internal class CreateLessonEndpoint : EndpointBase {
    internal override string Name => "CreateLesson";

    internal override void MapEndpoint(IEndpointRouteBuilder app) {
        app
            .MapPost(ApiEndpoints.Lessons.Create, Handle)
            .WithName(Name)
            .WithSummary("Create a new lesson")
            .WithTags(ApiTags.Lessons)
            .Accepts<CreateLessonRequest>("application/json")
            .Produces<LessonSmallResponse>(StatusCodes.Status201Created);
    }

    private async Task<IResult> Handle(
        [FromServices] ILogger<CreateLessonEndpoint>   logger,
        [FromServices] ISender                         mediator,
        [FromServices] IValidator<CreateLessonRequest> validator,
        [FromBody]     CreateLessonRequest             request
    ) {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("POST {EndpointName} received with {@Request}.", Name, request);

        var result = await mediator.Send(request.MapToCommand());

        if (result.IsError) {
            var errors = result.Errors;
            logger.LogInformation("POST {EndpointName} failed with {ErrorCount} errors.", Name, errors.Count);
            return Problem(errors);
        }

        var lesson = result.Value;
        logger.LogInformation("POST {EndpointName} succeeded with LessonId {LessonId}", Name, lesson.Id);
        return Results.Ok(lesson.MapToSmallResponse());
    }
}
