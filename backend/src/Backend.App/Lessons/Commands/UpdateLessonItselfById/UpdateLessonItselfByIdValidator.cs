using FluentValidation;

namespace Backend.App.Lessons.Commands.UpdateLessonItselfById;

internal class UpdateLessonItselfByIdValidator : AbstractValidator<UpdateLessonItselfByIdCommand> {
    internal UpdateLessonItselfByIdValidator() {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان نباید خالی باشد.");
    }
}
