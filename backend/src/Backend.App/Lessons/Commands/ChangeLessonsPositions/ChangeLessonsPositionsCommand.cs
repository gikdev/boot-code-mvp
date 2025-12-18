using ErrorOr;
using MediatR;

namespace Backend.App.Lessons.Commands.ChangeLessonsPositions;

public record ChangeLessonsPositionsCommand(
   IEnumerable<ChangeLessonPositionDto> Lessons
) : IRequest<ErrorOr<Success>>;

public record ChangeLessonPositionDto(
    Guid LessonId,
    int NewPosition
);
