using Backend.App.Common.Interfaces;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Backend.App.Lessons.Commands;

public record DeleteLessonByIdCommand(
    Guid Id
) : IRequest<ErrorOr<Success>>;

public class DeleteLessonByIdValidator : AbstractValidator<DeleteLessonByIdCommand> {
    public DeleteLessonByIdValidator() {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

public class DeleteLessonByIdHandler(
    ILessonsRepo lessonsRepo
) : IRequestHandler<DeleteLessonByIdCommand, ErrorOr<Success>> {
    public async Task<ErrorOr<Success>> Handle(DeleteLessonByIdCommand req, CancellationToken ct) {
        var lesson = await lessonsRepo.GetOneByIdAsync(req.Id);
        if (lesson is null) return Error.NotFound("درس پیدا نشد.");

        await lessonsRepo.RemoveAsync(lesson);
        await lessonsRepo.SaveChangesAsync();

        return Result.Success;
    }
}
