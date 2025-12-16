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
        Options(b => b
            .WithName(Name)
            .WithSummary("Delete a lesson by ID")
            .WithTags(ApiTags.Lessons)
        );
    }

    public override async Task HandleAsync(DeleteLessonByIdRequest req, CancellationToken ct) {
        var result = await Mediator.Send(req.MapToCommand(), ct);

        await result.Match(
            _ => Send.NoContentAsync(),
            errors => Send.SendErrorListAsync(errors)
        );
    }
}
