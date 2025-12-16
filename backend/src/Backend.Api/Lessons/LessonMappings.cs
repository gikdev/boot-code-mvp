using Backend.App.Lessons.Commands;
using Backend.Contracts.Lessons;
using Backend.Domain.Lessons;

namespace Backend.Api.Lessons;

public static class LessonMappings {
    public static CreateLessonCommand MapToCommand(this CreateLessonRequest req)
        => new(req.Title, req.Position, req.Content);

    public static LessonSmallResponse MapToResponse(this Lesson lesson)
        => new() {
            Id = lesson.Id,
            Position = lesson.Position,
            Title = lesson.Title,
        };
}
