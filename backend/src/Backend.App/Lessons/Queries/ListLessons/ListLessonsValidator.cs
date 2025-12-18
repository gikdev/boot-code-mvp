using FluentValidation;

namespace Backend.App.Lessons.Queries.ListLessons;

public class ListLessonsValidator : AbstractValidator<ListLessonsQuery> {
    public ListLessonsValidator() {}
}
