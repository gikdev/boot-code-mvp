using ErrorOr;

using MediatR;

namespace Backend.App.Lessons.Commands.DeleteLessonById;

public record DeleteLessonByIdCommand(
    Guid Id
) : IRequest<ErrorOr<Success>>;
