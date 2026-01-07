using Backend.App.Common.Interfaces;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Optional;

namespace Backend.App.Lessons.Commands.ChangeLessonsPositions;

internal class ChangeLessonsPositionsHandler(
    ILogger<ChangeLessonsPositionsHandler> logger,
    ILessonsRepo                           lessonsRepo
) : IRequestHandler<ChangeLessonsPositionsCommand, ErrorOr<Success>> {
    public async Task<ErrorOr<Success>> Handle(ChangeLessonsPositionsCommand req, CancellationToken ct) {
        var ids = req.Lessons.Select(x => x.LessonId).ToList();

        var lessons = await lessonsRepo.ListByIdsAsync(ids);

        foreach (var lesson in lessons) {
            var item = req.Lessons.FirstOrDefault(l => l.LessonId == lesson.Id);

            if (item is null) {
                logger.LogWarning("Lesson {LessonId} not found in request", lesson.Id);
                return Error.NotFound("درس پیدا نشد.");
            }

            if (lesson.Position != item.NewPosition) {
                logger.LogDebug(
                    "Updating lesson {LessonId} position from {OldPos} to {NewPos}",
                    lesson.Id,
                    lesson.Position,
                    item.NewPosition
                );

                lesson.Update(position: Option.Some(item.NewPosition));
            } else {
                logger.LogDebug("Lesson {LessonId} position unchanged ({Position})", lesson.Id, lesson.Position);
            }
        }

        await lessonsRepo.UpdateListAsync(lessons);
        await lessonsRepo.SaveChangesAsync();

        logger.LogInformation("Successfully updated positions for {Count} lessons", lessons.Count);

        return Result.Success;
    }
}
