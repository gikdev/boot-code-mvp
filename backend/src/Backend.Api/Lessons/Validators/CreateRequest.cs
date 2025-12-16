using Backend.Contracts.Lessons;
using FastEndpoints;
using FluentValidation;

namespace Backend.Api.Lessons.Validators;

public class CreateRequest : Validator<CreateLessonRequest> {
    public CreateRequest() {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان نباید خالی باشد.");
    }
}
