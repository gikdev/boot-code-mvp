using ErrorOr;
using MediatR;

namespace Backend.App.Lessons.Commands.UpdateLessonItselfById;

public record UpdateLessonItselfByIdCommand(
    Guid   Id,
    string Title,
    int    Position
) : IRequest<ErrorOr<Success>>;
