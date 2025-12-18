using FluentValidation;

namespace Backend.App.Lessons.Queries.GetLessonById;

internal class GetLessonByIdValidator : AbstractValidator<GetLessonByIdQuery> {
    internal GetLessonByIdValidator() {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
