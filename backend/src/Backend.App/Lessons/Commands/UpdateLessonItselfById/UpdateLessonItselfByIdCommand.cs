using ErrorOr;
using MediatR;

namespace Backend.App.Lessons.Commands.UpdateLessonItselfById;

public record UpdateLessonItselfByIdCommand : IRequest<ErrorOr<Success>> {
    public required Guid   Id    { get; init; }
    public required string Title { get; init; }
}
