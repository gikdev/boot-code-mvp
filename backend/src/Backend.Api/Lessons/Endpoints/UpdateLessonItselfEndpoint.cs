using Backend.Api.Common;
using Backend.Contracts.Lessons;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Lessons.Endpoints;

internal class UpdateLessonItselfByIdEndpoint : EndpointBase {
    internal override string Name => "UpdateLessonItselfById";

    internal override void MapEndpoint(IEndpointRouteBuilder app) {
        app
            .MapPut(ApiEndpoints.Lessons.UpdateItselfById, Handle)
            .WithName(Name)
            .WithSummary("Update lesson itself by ID")
            .WithTags(ApiTags.Lessons)
            .Produces(StatusCodes.Status204NoContent);
    }

    private async Task<IResult> Handle(
        [FromServices] ILogger<UpdateLessonItselfByIdEndpoint>   logger,
        [FromServices] ISender                                   mediator,
        [FromServices] IValidator<UpdateLessonItselfByIdRequest> validator,
        [FromRoute]    Guid                                      id,
        [FromBody]     UpdateLessonItselfByIdRequest             request
    ) {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("PUT {EndpointName} #{LessonId} received with {@Request}.", Name, id, request);

        var result = await mediator.Send(request.MapToCommand(id));

        if (result.IsError) {
            var errors = result.Errors;

            logger.LogInformation("PUT {EndpointName} #{LessonId} failed with {ErrorCount} errors.", Name, id,
                errors.Count);

            return Problem(errors);
        }

        logger.LogInformation("PUT {EndpointName} #{LessonId} succeeded.", Name, id);

        return Results.NoContent();
    }
}
