namespace Backend.Contracts.Lessons;

public record LessonFullResponse : LessonSmallResponse {
    public required string? TextContent { get; init; }
    public required string? AudioUrl { get; init; }
    public required string? VideoUrl { get; init; }
    public required IEnumerable<ResourceDto>? Resources { get; init; }
}
