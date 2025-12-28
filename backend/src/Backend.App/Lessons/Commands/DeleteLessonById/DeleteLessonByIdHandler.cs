using Backend.App.Common.Interfaces;

using ErrorOr;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Backend.App.Lessons.Commands.DeleteLessonById;

internal class DeleteLessonByIdHandler(
    ILessonsRepo                     lessonsRepo,
    ILogger<DeleteLessonByIdHandler> logger
) : IRequestHandler<DeleteLessonByIdCommand, ErrorOr<Success>> {
    public async Task<ErrorOr<Success>> Handle(DeleteLessonByIdCommand req, CancellationToken ct) {
        logger.LogDebug("Deleting lesson #{LessonId}", req.Id);

        var lesson = await lessonsRepo.GetOneByIdAsync(req.Id);

        if (lesson is null) {
            logger.LogInformation("Lesson #{LessonId} was not found!", req.Id);
            return Error.NotFound("درس پیدا نشد.");
        }

        try {
            await lessonsRepo.RemoveAsync(lesson);
            await lessonsRepo.SaveChangesAsync();
        } catch (Exception ex) {
            logger.LogError(ex, "Failed to persist (delete) lesson {LessonId}", lesson.Id);
            throw;
        }

        logger.LogInformation("Lesson #{LessonId} deleted successfully.", lesson.Id);

        return Result.Success;
    }
}
