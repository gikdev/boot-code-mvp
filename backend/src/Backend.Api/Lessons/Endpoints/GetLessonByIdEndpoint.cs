using Backend.Api.Common;
using Backend.Contracts.Lessons;
using ErrorOr;
using FastEndpoints;
using MediatR;

namespace Backend.Api.Lessons.Endpoints;

public class GetLessonByIdEndpoint : Ep.Req<GetLessonByIdRequest>.Res<LessonFullResponse> {
    public const string Name = "GetLessonById";

    public required ISender Mediator { get; init; }

    public override void Configure() {
        AllowAnonymous();
        Get(ApiEndpoints.Lessons.GetById);
        Description(b => b.WithName(Name).WithTags(ApiTags.Lessons));
        Summary(s => s.Summary = "Get lesson by ID");
    }

    public override async Task HandleAsync(GetLessonByIdRequest req, CancellationToken ct) {
        Logger.LogDebug("GET {EndpointName} #{LessonId} received.", Name, req.Id);

        var lesson = await Mediator.Send(req.MapToQuery(), ct);

        if (lesson is null) {
            List<Error> errors = [Error.NotFound("درس پیدا نشد.")];
            Logger.LogInformation("GET {EndpointName} #{LessonId} failed with {ErrorCount} errors.", Name, req.Id, errors.Count);
            await Send.SendErrorListAsync(errors);
            return;
        }

        Logger.LogInformation("GET {EndpointName} #{LessonId} succeeded", Name, lesson.Id);
        await Send.OkAsync(lesson.MapToFullResponse(), ct);
    }
}
