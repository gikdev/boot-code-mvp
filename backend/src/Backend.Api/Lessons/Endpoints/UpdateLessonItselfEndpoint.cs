using Backend.Api.Common;
using Backend.Contracts.Lessons;
using FastEndpoints;
using MediatR;

namespace Backend.Api.Lessons.Endpoints;

public class UpdateLessonItselfByIdEndpoint : Ep.Req<UpdateLessonItselfByIdRequest>.NoRes {
    public const string Name = "UpdateLessonItselfById";

    public required ISender Mediator { get; init; }

    public override void Configure() {
        AllowAnonymous();
        Put(ApiEndpoints.Lessons.UpdateItselfById);
        Description(b => b.WithName(Name).WithTags(ApiTags.Lessons));
        Summary(s => s.Summary = "Update lesson itself by ID");
    }

    public override async Task HandleAsync(UpdateLessonItselfByIdRequest req, CancellationToken ct) {
        Logger.LogDebug("PUT {EndpointName} #{LessonId} received with.", Name, req.Id);

        var result = await Mediator.Send(req.MapToCommand(), ct);

        if (result.IsError) {
            var errors = result.Errors;
            Logger.LogInformation("PUT {EndpointName} #{LessonId} failed with {ErrorCount} errors.", Name, req.Id, errors.Count);
            await Send.SendErrorListAsync(errors);
            return;
        }

        Logger.LogInformation("PUT {EndpointName} #{LessonId} succeeded.", Name, req.Id);
        await Send.NoContentAsync(ct);
    }
}
