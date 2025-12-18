using Backend.Contracts.Lessons;
using FluentValidation;

namespace Backend.Api.Lessons.Validators;

public class UpdateLessonItselfByIdRequestValidator : AbstractValidator<UpdateLessonItselfByIdRequest> {
    public UpdateLessonItselfByIdRequestValidator() {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان نباید خالی باشد.");
    }
}
