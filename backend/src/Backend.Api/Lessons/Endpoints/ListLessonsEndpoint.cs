using Backend.Api.Common;
using Backend.App.Lessons.Queries.ListLessons;
using Backend.Contracts.Lessons;
using FastEndpoints;
using MediatR;

namespace Backend.Api.Lessons.Endpoints;

public class ListLessonsEndpoint : Ep.NoReq.Res<LessonListResponse> {
    public const string Name = "ListLessons";

    public required ISender Mediator { get; init; }

    public override void Configure() {
        AllowAnonymous();
        Get(ApiEndpoints.Lessons.List);
        Options(b => b
            .WithName(Name)
            .WithSummary("List lessons")
            .WithTags(ApiTags.Lessons)
        );
    }

    public override async Task HandleAsync(CancellationToken ct) {
        Logger.LogDebug("GET {EndpointName} received.", Name);

        var lessonList = await Mediator.Send(new ListLessonsQuery(), ct);

        Logger.LogInformation("POST {EndpointName} succeeded", Name);

        await Send.OkAsync(lessonList.MapToListResponse(), ct);
    }
}
