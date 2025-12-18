using Backend.Api.Lessons.Validators;
using Backend.App.Lessons.Commands.ChangeLessonsPositions;
using Backend.App.Lessons.Commands.CreateLesson;
using Backend.App.Lessons.Commands.UpdateLessonContentById;
using Backend.App.Lessons.Commands.UpdateLessonItselfById;
using Backend.Contracts.Lessons;
using Backend.Domain.Lessons;

namespace Backend.Api.Lessons;

internal static class Mappings {
    internal static CreateLessonCommand MapToCommand(this CreateLessonRequest req) {
        return new CreateLessonCommand(req.Title, req.Position, req.Content);
    }

    internal static LessonSmallResponse MapToSmallResponse(this Lesson lesson) {
        return new LessonSmallResponse {
            Id = lesson.Id,
            Position = lesson.Position,
            Title = lesson.Title
        };
    }

    internal static LessonListResponse MapToListResponse(this List<Lesson> lessonList) {
        return new LessonListResponse {
            Items = lessonList.Select(l => l.MapToSmallResponse())
        };
    }

    internal static LessonFullResponse MapToFullResponse(this Lesson lesson) {
        return new LessonFullResponse {
            Id = lesson.Id,
            Title = lesson.Title,
            Content = lesson.Content,
            Position = lesson.Position
        };
    }

    internal static UpdateLessonItselfByIdCommand MapToCommand(this UpdateLessonItselfByIdRequest request, Guid id) {
        return new UpdateLessonItselfByIdCommand(id, request.Title, request.Position);
    }

    internal static UpdateLessonContentByIdCommand MapToCommand(this UpdateLessonContentByIdRequest request, Guid id) {
        return new UpdateLessonContentByIdCommand(id, request.Content);
    }

    internal static ChangeLessonsPositionsCommand MapToCommand(this ChangeLessonsPositionsRequest request) {
        var dtos = request.Lessons.Select(x => new ChangeLessonPositionDto(x.LessonId, x.NewPosition));

        return new ChangeLessonsPositionsCommand(dtos);
    }
}
