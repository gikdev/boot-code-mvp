namespace Backend.Contracts.Lessons;

public record GetLessonByIdRequest {
    public required Guid Id { get; init; }
}
