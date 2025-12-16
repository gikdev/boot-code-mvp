using Backend.App.Common.Interfaces;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Backend.App.Lessons.Commands;

public record UpdateLessonItselfByIdCommand(
    Guid Id,
    string Title,
    int Position
) : IRequest<ErrorOr<Success>>;

public class UpdateLessonItselfByIdValidator : AbstractValidator<UpdateLessonItselfByIdCommand> {
    public UpdateLessonItselfByIdValidator() {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان نباید خالی باشد.");
    }
}

public class UpdateLessonItselfByIdHandler(
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
