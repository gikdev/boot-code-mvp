using Backend.App.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace Backend.App.Lessons.Commands.UpdateLessonItselfById;

internal class UpdateLessonItselfByIdHandler(
    ILessonsRepo lessonsRepo
) : IRequestHandler<UpdateLessonItselfByIdCommand, ErrorOr<Success>> {
    public async Task<ErrorOr<Success>> Handle(UpdateLessonItselfByIdCommand req, CancellationToken ct) {
        var lesson = await lessonsRepo.GetOneByIdAsync(req.Id);
        if (lesson is null) return Error.NotFound("درس پیدا نشد.");

        lesson.RenameTitle(req.Title);
        lesson.ChangePosition(req.Position);

        await lessonsRepo.UpdateAsync(lesson);
        await lessonsRepo.SaveChangesAsync();

        return Result.Success;
    }
}
