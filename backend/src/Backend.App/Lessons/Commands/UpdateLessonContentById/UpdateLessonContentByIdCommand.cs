using Backend.Domain.Lessons;
using ErrorOr;
using MediatR;

namespace Backend.App.Lessons.Commands.UpdateLessonContentById;

public record UpdateLessonContentByIdCommand(
    Guid Id,
    string? TextContent = null,
    string? AudioUrl = null,
    string? VideoUrl = null,
    List<Resource>? Resources = null
) : IRequest<ErrorOr<Success>>;
