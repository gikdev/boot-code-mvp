namespace Backend.Contracts.Lessons;

public record LessonListResponse {
    public required IEnumerable<LessonSmallResponse> Items { get; init; }
}
