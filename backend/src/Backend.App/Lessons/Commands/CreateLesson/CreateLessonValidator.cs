using FluentValidation;

namespace Backend.App.Lessons.Commands.CreateLesson;

public class CreateLessonValidator : AbstractValidator<CreateLessonCommand> {
    public CreateLessonValidator() {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان نباید خالی باشد.");
    }
}
