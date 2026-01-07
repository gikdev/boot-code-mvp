using Backend.App.Lessons.Commands.ChangeLessonsPositions;
using Backend.App.Lessons.Commands.CreateLesson;
using Backend.App.Lessons.Commands.UpdateLessonContentById;
using Backend.App.Lessons.Commands.UpdateLessonItselfById;
using Backend.Contracts.Lessons;
using Backend.Domain.Lessons;

namespace Backend.Api.Lessons;

internal static class Mappings {
    internal static CreateLessonCommand MapToCommand(this CreateLessonRequest request) {
        return new CreateLessonCommand(request.Title);
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
            ImageUrl    = lesson.ImageUrl,
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
        return new UpdateLessonItselfByIdCommand {
            Id    = id,
            Title = request.Title
        };
    }

    internal static UpdateLessonContentByIdCommand MapToCommand(this UpdateLessonContentByIdRequest request, Guid id) {
        return new UpdateLessonContentByIdCommand {
            Id          = id,
            TextContent = request.TextContent,
            ImageUrl    = request.ImageUrl,
            AudioUrl    = request.AudioUrl,
            Resources   = (request.Resources ?? []).Select(r => r.MapToDomain()).ToList(),
            VideoUrl    = request.VideoUrl
        };
    }

    internal static Resource MapToDomain(this ResourceDto dto) {
        return new Resource(
            dto.Title,
            dto.Url
        );
    }

    internal static ChangeLessonsPositionsCommand MapToCommand(this ChangeLessonsPositionsRequest request) {
        var dtoList = request.Lessons.Select(x => x.MapToDto());

        return new ChangeLessonsPositionsCommand {
            Lessons = dtoList
        };
    }

    internal static ChangeLessonPositionDto MapToDto(this ChangeLessonPositionRequest request) {
        return new ChangeLessonPositionDto(
            request.LessonId,
            request.NewPosition
        );
    }
}
