namespace Backend.Contracts.Lessons;

public record DeleteLessonByIdRequest {
    public required Guid Id { get; init; }
}
