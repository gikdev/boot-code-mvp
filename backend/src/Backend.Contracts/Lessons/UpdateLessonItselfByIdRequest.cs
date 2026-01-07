namespace Backend.Contracts.Lessons;

public record UpdateLessonItselfByIdRequest {
    public required string Title { get; init; }
}
