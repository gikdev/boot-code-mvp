namespace Backend.Contracts.Lessons;

public record LessonFullResponse : LessonSmallResponse {
    public required string? Content { get; init; }
}
