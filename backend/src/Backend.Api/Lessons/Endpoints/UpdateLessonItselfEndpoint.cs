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
        Options(b => b
            .WithName(Name)
            .WithSummary("Update lesson itself by ID")
            .WithTags(ApiTags.Lessons)
        );
    }

    public override async Task HandleAsync(UpdateLessonItselfByIdRequest req, CancellationToken ct) {
        var result = await Mediator.Send(req.MapToCommand(), ct);

        await result.Match(
            _ => Send.NoContentAsync(),
            errors => Send.SendErrorListAsync(errors)
        );
    }
}
