using ErrorOr;
using MediatR;

namespace Backend.App.Lessons.Commands.UpdateLessonContentById;

public record UpdateLessonContentByIdCommand(
    Guid Id,
    string? Content = null
) : IRequest<ErrorOr<Success>>;
