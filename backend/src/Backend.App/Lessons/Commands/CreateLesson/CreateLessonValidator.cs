using FluentValidation;

namespace Backend.App.Lessons.Commands.CreateLesson;

internal class CreateLessonValidator : AbstractValidator<CreateLessonCommand> {
    internal CreateLessonValidator() {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان نباید خالی باشد.");
    }
}
