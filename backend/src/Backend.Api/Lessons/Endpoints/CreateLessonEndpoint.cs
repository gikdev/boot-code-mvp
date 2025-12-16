using Backend.Api.Common;
using Backend.Contracts.Lessons;
using FastEndpoints;
using MediatR;

namespace Backend.Api.Lessons.Endpoints;

public class CreateLessonEndpoint : Ep.Req<CreateLessonRequest>.Res<LessonSmallResponse> {
    public const string Name = "CreateLesson";

    public required ISender Mediator { get; init; }

    public override void Configure() {
        AllowAnonymous();
        Post(ApiEndpoints.Lessons.Create);
        Description(b => b.WithName(Name).WithTags(ApiTags.Lessons));
        Summary(s => s.Summary = "Create a new lesson");
    }

    public override async Task HandleAsync(CreateLessonRequest req, CancellationToken ct) {
        Logger.LogDebug("POST {EndpointName} received.", Name);

        var result = await Mediator.Send(req.MapToCommand(), ct);

        if (result.IsError) {
            var errors = result.Errors;
            Logger.LogInformation("POST {EndpointName} failed with {ErrorCount} errors.", Name, errors.Count);
            await Send.SendErrorListAsync(errors);
            return;
        }

        var lesson = result.Value;
        Logger.LogInformation("POST {EndpointName} succeeded with LessonId {LessonId}", Name, lesson.Id);
        await Send.OkAsync(lesson.MapToSmallResponse(), ct);
    }
}
