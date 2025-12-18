using FluentValidation;

namespace Backend.App.Lessons.Commands.DeleteLessonById;

internal class DeleteLessonByIdValidator : AbstractValidator<DeleteLessonByIdCommand> {
    internal DeleteLessonByIdValidator() {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
