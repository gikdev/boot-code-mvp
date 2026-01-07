namespace Backend.Contracts.Lessons;

public record CreateLessonRequest {
    public required string Title { get; init; }
}
