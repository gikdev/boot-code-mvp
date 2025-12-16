using Backend.App.Common.Interfaces;
using Backend.Domain.Lessons;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Backend.App.Lessons.Commands;

public record CreateLessonCommand(
    string Title,
    int? Position = null,
    string? Content = null
) : IRequest<ErrorOr<Lesson>>;

public class CreateLessonValidator : AbstractValidator<CreateLessonCommand> {
    public CreateLessonValidator() {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان نباید خالی باشد.");
    }
}

public class CreateLessonHandler(
    ILessonsRepo lessonsRepo
) : IRequestHandler<CreateLessonCommand, ErrorOr<Lesson>> {
    public async Task<ErrorOr<Lesson>> Handle(CreateLessonCommand req, CancellationToken ct) {
        var result = Lesson.Create(
            req.Title,
            req.Position ?? 1,
            req.Content
        );
        if (result.IsError) return result.Errors;
        var lesson = result.Value;

        await lessonsRepo.AddAsync(lesson);
        await lessonsRepo.SaveChangesAsync();

        return lesson;
    }
}
