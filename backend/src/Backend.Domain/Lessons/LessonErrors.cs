using ErrorOr;

namespace Backend.Domain.Lessons;

// Error code format:
// <Aggregate>.<Thing>.<Reason>
public static class LessonErrors {
    public static Error TitleEmpty =>
        Error.Validation(
            "Lesson.Title.Empty",
            "Lesson title is empty! (It shouldn't!)"
        );
}
