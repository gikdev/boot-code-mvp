using Backend.App.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace Backend.App.Lessons.Commands.UpdateLessonContentById;

internal class UpdateLessonContentByIdHandler(
    ILessonsRepo lessonsRepo
) : IRequestHandler<UpdateLessonContentByIdCommand, ErrorOr<Success>> {
    public async Task<ErrorOr<Success>> Handle(UpdateLessonContentByIdCommand req, CancellationToken ct) {
        var lesson = await lessonsRepo.GetOneByIdAsync(req.Id);
        if (lesson is null) return Error.NotFound("درس پیدا نشد.");

        lesson.ChangeContent(req.Content);

        await lessonsRepo.UpdateAsync(lesson);
        await lessonsRepo.SaveChangesAsync();

        return Result.Success;
    }
}
