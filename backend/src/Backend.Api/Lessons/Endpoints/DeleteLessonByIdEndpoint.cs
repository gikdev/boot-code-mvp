using Backend.Api.Common;
using Backend.Contracts.Lessons;
using FastEndpoints;
using MediatR;

namespace Backend.Api.Lessons.Endpoints;

public class DeleteLessonByIdEndpoint : Ep.Req<DeleteLessonByIdRequest>.NoRes {
    public const string Name = "DeleteLessonById";

    public required ISender Mediator { get; init; }

    public override void Configure() {
        AllowAnonymous();
        Delete(ApiEndpoints.Lessons.DeleteById);
        Description(b => b.WithName(Name).WithTags(ApiTags.Lessons));
        Summary(s => s.Summary = "Delete a lesson by ID");
    }

    public override async Task HandleAsync(DeleteLessonByIdRequest req, CancellationToken ct) {
        Logger.LogDebug("DELETE {EndpointName} #{LessonId} received.", Name, req.Id);

        var result = await Mediator.Send(req.MapToCommand(), ct);

        if (result.IsError) {
            var errors = result.Errors;
            Logger.LogInformation("DELETE {EndpointName} #{LessonId} failed with {ErrorCount} errors.", Name, req.Id, errors.Count);
            await Send.SendErrorListAsync(errors);
            return;
        }

        Logger.LogInformation("DELETE {EndpointName} #{LessonId} succeeded.", Name, req.Id);
        await Send.NoContentAsync(ct);
    }
}
