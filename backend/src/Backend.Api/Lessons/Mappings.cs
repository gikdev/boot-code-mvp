using Backend.App.Lessons.Commands.CreateLesson;
using Backend.App.Lessons.Commands.DeleteLessonById;
using Backend.App.Lessons.Commands.UpdateLessonItselfById;
using Backend.App.Lessons.Queries.GetLessonById;
using Backend.Contracts.Lessons;
using Backend.Domain.Lessons;

namespace Backend.Api.Lessons;

public static class Mappings {
    public static CreateLessonCommand MapToCommand(this CreateLessonRequest req) {
        return new CreateLessonCommand(req.Title, req.Position, req.Content);
    }

    public static LessonSmallResponse MapToSmallResponse(this Lesson lesson) {
        return new LessonSmallResponse {
            Id = lesson.Id,
            Position = lesson.Position,
            Title = lesson.Title
        };
    }

    public static LessonListResponse MapToListResponse(this List<Lesson> lessonList) {
        return new LessonListResponse {
            Items = lessonList.Select(l => l.MapToSmallResponse()),
        };
    }

    public static LessonFullResponse MapToFullResponse(this Lesson lesson) {
        return new LessonFullResponse {
            Id = lesson.Id,
            Title = lesson.Title,
            Content = lesson.Content,
            Position = lesson.Position
        };
    }

    public static GetLessonByIdQuery MapToQuery(this GetLessonByIdRequest request) {
        return new GetLessonByIdQuery(request.Id);
    }

    public static DeleteLessonByIdCommand MapToCommand(this DeleteLessonByIdRequest request) {
        return new DeleteLessonByIdCommand(request.Id);
    }

    public static UpdateLessonItselfByIdCommand MapToCommand(this UpdateLessonItselfByIdRequest request) {
        return new UpdateLessonItselfByIdCommand(request.Id, request.Title, request.Position);
    }
}
