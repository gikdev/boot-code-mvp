using ErrorOr;
using MediatR;

namespace Backend.App.Lessons.Commands.ChangeLessonsPositions;

public record ChangeLessonsPositionsCommand : IRequest<ErrorOr<Success>> {
    public required IEnumerable<ChangeLessonPositionDto> Lessons { get; init; }
}

public record ChangeLessonPositionDto(
    Guid LessonId,
    int  NewPosition
);
