namespace Backend.Contracts.Lessons;

public record CreateLessonRequest {
    public required string Title { get; init; }
    public int? Position { get; init; }
    public string? Content { get; init; }
}
