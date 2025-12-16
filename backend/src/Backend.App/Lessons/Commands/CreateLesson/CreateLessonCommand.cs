using Backend.Domain.Lessons;
using ErrorOr;
using MediatR;

namespace Backend.App.Lessons.Commands.CreateLesson;

public record CreateLessonCommand(
    string Title,
    int? Position = null,
    string? Content = null
) : IRequest<ErrorOr<Lesson>>;
