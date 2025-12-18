namespace Backend.Contracts.Lessons;

public record UpdateLessonContentByIdRequest {
    public required string? Content { get; init; }
}
