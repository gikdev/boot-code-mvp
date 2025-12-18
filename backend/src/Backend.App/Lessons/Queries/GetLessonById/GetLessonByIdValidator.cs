using FluentValidation;

namespace Backend.App.Lessons.Queries.GetLessonById;

public class GetLessonByIdValidator : AbstractValidator<GetLessonByIdQuery> {
    public GetLessonByIdValidator() {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
