using FluentValidation;

namespace Backend.App.Lessons.Commands.UpdateLessonItselfById;

public class UpdateLessonItselfByIdValidator : AbstractValidator<UpdateLessonItselfByIdCommand> {
    public UpdateLessonItselfByIdValidator() {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان نباید خالی باشد.");
    }
}
