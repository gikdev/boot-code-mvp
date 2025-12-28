using Backend.App.Lessons.Commands.ChangeLessonsPositions;
using Backend.App.Lessons.Commands.CreateLesson;
using Backend.App.Lessons.Commands.UpdateLessonContentById;
using Backend.App.Lessons.Commands.UpdateLessonItselfById;
using Backend.Contracts.Lessons;
using Backend.Domain.Lessons;

namespace Backend.Api.Lessons;

internal static class Mappings {
    internal static CreateLessonCommand MapToCommand(this CreateLessonRequest request) {
        return new CreateLessonCommand(
            request.Title,
            request.Position,
            request.TextContent,
            request.AudioUrl,
            request.VideoUrl,
            [.. (request.Resources ?? []).Select(x => x.MapToDomain())]
        );
    }

    internal static LessonSmallResponse MapToSmallResponse(this Lesson lesson) {
        return new LessonSmallResponse {
            Id       = lesson.Id,
            Position = lesson.Position,
            Title    = lesson.Title
        };
    }

    internal static LessonListResponse MapToListResponse(this List<Lesson> lessonList) {
        return new LessonListResponse {
            Items = lessonList.Select(l => l.MapToSmallResponse())
        };
    }

    internal static LessonFullResponse MapToFullResponse(this Lesson lesson) {
        return new LessonFullResponse {
            Id          = lesson.Id,
            Title       = lesson.Title,
            TextContent = lesson.TextContent,
            Position    = lesson.Position,
            AudioUrl    = lesson.AudioUrl,
            Resources   = lesson.Resources.Select(x => new ResourceDto { Title = x.Title, Url = x.Url }),
            VideoUrl    = lesson.VideoUrl
        };
    }

    internal static ResourceDto MapToDto(this Resource resource) {
        return new ResourceDto {
            Title = resource.Title,
            Url   = resource.Url
        };
    }

    internal static UpdateLessonItselfByIdCommand MapToCommand(this UpdateLessonItselfByIdRequest request, Guid id) {
        return new UpdateLessonItselfByIdCommand(id, request.Title, request.Position);
    }

    internal static UpdateLessonContentByIdCommand MapToCommand(this UpdateLessonContentByIdRequest request, Guid id) {
        return new UpdateLessonContentByIdCommand(
            id,
            request.TextContent,
            request.AudioUrl,
            request.VideoUrl,
            [.. (request.Resources ?? []).Select(x => x.MapToDomain())]
        );
    }

    internal static Resource MapToDomain(this ResourceDto dto) {
        return new Resource(
            dto.Title,
            dto.Url
        );
    }

    internal static ChangeLessonsPositionsCommand MapToCommand(this ChangeLessonsPositionsRequest request) {
        var dtos = request.Lessons.Select(x => x.MapToDto());

        return new ChangeLessonsPositionsCommand(dtos);
    }

    internal static ChangeLessonPositionDto MapToDto(this ChangeLessonPositionRequest request) {
        return new ChangeLessonPositionDto(
            request.LessonId,
            request.NewPosition
        );
    }
}
