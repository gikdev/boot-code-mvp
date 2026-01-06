using Backend.Domain.Lessons;
using ErrorOr;
using MediatR;

namespace Backend.App.Lessons.Commands.CreateLesson;

public record CreateLessonCommand(
    string          Title,
    int?            Position    = null,
    string?         TextContent = null,
    string?         AudioUrl    = null,
    string?         VideoUrl    = null,
    List<Resource>? Resources   = null
) : IRequest<ErrorOr<Lesson>>;
