namespace Backend.Contracts.Lessons;

public record ChangeLessonsPositionsRequest {
    public required IEnumerable<ChangeLessonPositionRequest> Lessons { get; init; }
}

public record ChangeLessonPositionRequest {
    public required Guid LessonId { get; init; }
    public required int NewPosition { get; init; }
}
