using Backend.Domain.Lessons;
using ErrorOr;
using MediatR;

namespace Backend.App.Lessons.Commands.CreateLesson;

public record CreateLessonCommand(
    string Title
) : IRequest<ErrorOr<Lesson>>;
