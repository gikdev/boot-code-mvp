using Backend.App.Common.Interfaces;
using ErrorOr;
using MediatR;
using Optional;

namespace Backend.App.Lessons.Commands.UpdateLessonContentById;

internal class UpdateLessonContentByIdHandler(
    ILessonsRepo lessonsRepo
) : IRequestHandler<UpdateLessonContentByIdCommand, ErrorOr<Success>> {
    public async Task<ErrorOr<Success>> Handle(UpdateLessonContentByIdCommand req, CancellationToken ct) {
        var lesson = await lessonsRepo.GetOneByIdAsync(req.Id);
        if (lesson is null) return Error.NotFound("درس پیدا نشد.");

        lesson.Update(
            audioUrl: Option.Some(req.AudioUrl),
            imageUrl: Option.Some(req.ImageUrl),
            resources: Option.Some(req.Resources),
            textContent: Option.Some(req.TextContent),
            videoUrl: Option.Some(req.VideoUrl)
        );

        await lessonsRepo.UpdateAsync(lesson);
        await lessonsRepo.SaveChangesAsync();

        return Result.Success;
    }
}
