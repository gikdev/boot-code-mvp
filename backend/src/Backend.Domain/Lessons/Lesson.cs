using ErrorOr;

namespace Backend.Domain.Lessons;

public class Lesson {
#pragma warning disable CS8618
    private Lesson() {
    }
#pragma warning restore CS8618

    private Lesson(
        string title,
        int position,
        string? content = null,
        Guid? id = null
    ) {
        Title = title;
        Position = position;
        Content = content;
        Id = id ?? Guid.NewGuid();
    }

    public Guid Id { get; }
    public string Title { get; private set; }
    public int Position { get; private set; }
    public string? Content { get; private set; }

    public void RenameTitle(string newTitle) {
        Title = newTitle;
    }

    public void ChangePosition(int newPosition) {
        Position = newPosition;
    }

    public void ChangeContent(string? newContent) {
        Content = newContent;
    }

    public static ErrorOr<Lesson> Create(
        string title,
        int position,
        string? content = null,
        Guid? id = null
    ) {
        if (string.IsNullOrWhiteSpace(title))
            return LessonErrors.TitleEmpty;

        return new Lesson(title, position, content, id);
    }
}
