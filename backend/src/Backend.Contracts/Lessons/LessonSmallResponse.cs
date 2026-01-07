namespace Backend.Contracts.Lessons;

public record LessonSmallResponse {
    public required Guid   Id       { get; init; }
    public required string Title    { get; init; }
    public required int    Position { get; init; }
}
