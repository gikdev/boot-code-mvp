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
        Options(b => b
            .WithName(Name)
            .WithSummary("Get lesson by ID")
            .WithTags(ApiTags.Lessons)
        );
    }

    public override async Task HandleAsync(GetLessonByIdRequest req, CancellationToken ct) {
        var lesson = await Mediator.Send(req.MapToQuery(), ct);

        if (lesson is null) {
            List<Error> errors = [Error.NotFound("درس پیدا نشد.")];
            await Send.SendErrorListAsync(errors);
            return;
        }

        await Send.OkAsync(lesson.MapToFullResponse(), ct);
    }
}
