using Backend.App.Common.Interfaces;
using Backend.Domain.Lessons;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Backend.App.Lessons.Commands.CreateLesson;

public class CreateLessonHandler(
    ILessonsRepo lessonsRepo,
    ILogger<CreateLessonHandler> logger
) : IRequestHandler<CreateLessonCommand, ErrorOr<Lesson>> {
    public async Task<ErrorOr<Lesson>> Handle(CreateLessonCommand req, CancellationToken ct) {
        logger.LogDebug("Creating lesson with title '{Title}' at position '{Position}', which has content '{HasContent}'", req.Title, req.Position, req.Content != null);

        var result = Lesson.Create(
            req.Title,
            req.Position ?? 1,
            req.Content
        );

        if (result.IsError) {
            logger.LogInformation("Lesson creation failed to domain validation errors {@Errors}", result.Errors);
            return result.Errors;
        }

        var lesson = result.Value;

        try {
            await lessonsRepo.AddAsync(lesson);
            await lessonsRepo.SaveChangesAsync();
        }
        catch (Exception ex) {
            logger.LogError(ex, "Failed to persist lesson {LessonId}", lesson.Id);
            throw;
        }

        logger.LogInformation("Lesson #{LessonId} created successfully.", lesson.Id);

        return lesson;
    }
}
