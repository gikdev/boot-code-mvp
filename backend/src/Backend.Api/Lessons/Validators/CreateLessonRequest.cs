using Backend.Contracts.Lessons;
using FluentValidation;

namespace Backend.Api.Lessons.Validators;

public class CreateLessonRequestValidator : AbstractValidator<CreateLessonRequest> {
    public CreateLessonRequestValidator() {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان نباید خالی باشد.");
    }
}
