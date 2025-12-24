using Backend.App.Common.Interfaces;
using ErrorOr;
using MediatR;
using Optional;

namespace Backend.App.Lessons.Commands.ChangeLessonsPositions;

internal class ChangeLessonsPositionsHandler(
    ILessonsRepo lessonsRepo
) : IRequestHandler<ChangeLessonsPositionsCommand, ErrorOr<Success>> {
    public async Task<ErrorOr<Success>> Handle(ChangeLessonsPositionsCommand req, CancellationToken ct) {
        var ids = req.Lessons.Select(x => x.LessonId).ToList();

        var lessons = await lessonsRepo.ListByIdsAsync(ids);

        foreach (var lesson in lessons) {
            var item = req.Lessons.FirstOrDefault(l => l.LessonId == lesson.Id);
            if (item is null) return Error.NotFound("درس پیدا نشد.");

            lesson.Update(
                position: Option.Some(lesson.Position)
            );
        }

        await lessonsRepo.UpdateListAsync(lessons);
        await lessonsRepo.SaveChangesAsync();

        return Result.Success;
    }
}
