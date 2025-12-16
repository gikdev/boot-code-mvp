using Backend.App.Lessons.Commands;
using Backend.Contracts.Lessons;
using Backend.Domain.Lessons;

namespace Backend.Api.Lessons;

public static class Mappings {
    public static CreateLessonCommand MapToCommand(this CreateLessonRequest req) {
        return new CreateLessonCommand(req.Title, req.Position, req.Content);
    }

    public static LessonSmallResponse MapToResponse(this Lesson lesson) {
        return new LessonSmallResponse {
            Id = lesson.Id,
            Position = lesson.Position,
            Title = lesson.Title
        };
    }
}
