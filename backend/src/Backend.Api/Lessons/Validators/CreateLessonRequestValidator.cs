using Backend.Contracts.Lessons;
using FastEndpoints;
using FluentValidation;

namespace Backend.Api.Lessons.Validators;

public class CreateLessonRequestValidator : Validator<CreateLessonRequest> {
    public CreateLessonRequestValidator() {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان نباید خالی باشد.");
    }
}
