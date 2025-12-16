using Backend.Api.Common;
using Backend.Contracts.Lessons;
using FastEndpoints;
using MediatR;

namespace Backend.Api.Lessons;

public class CreateLessonEndpoint : Ep.Req<CreateLessonRequest>.Res<LessonSmallResponse> {
    public const string Name = "CreateLesson";

    public required ISender Mediator { get; init; }

    public override void Configure() {
        AllowAnonymous();
        Post(ApiEndpoints.Lessons.Create);
        Options(b => b
            .WithName(Name)
            .WithSummary("Create a new lesson")
            .WithTags(ApiTags.Lessons)
        );
    }

    public override async Task HandleAsync(CreateLessonRequest req, CancellationToken ct) {
        var result = await Mediator.Send(req.MapToCommand(), ct);

        await result.Match(
            lesson => Send.OkAsync(lesson.MapToResponse()),
            errors => Send.ResultAsync(ApiResults.Problem(errors))
        );
    }
}
