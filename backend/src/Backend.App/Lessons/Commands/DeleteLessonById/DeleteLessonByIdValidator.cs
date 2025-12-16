using FluentValidation;

namespace Backend.App.Lessons.Commands.DeleteLessonById;

public class DeleteLessonByIdValidator : AbstractValidator<DeleteLessonByIdCommand> {
    public DeleteLessonByIdValidator() {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
