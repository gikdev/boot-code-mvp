namespace Backend.Contracts.Lessons;

public record CreateLessonRequest {
    public required string                    Title       { get; init; }
    public          int?                      Position    { get; init; }
    public          string?                   TextContent { get; init; }
    public          string?                   AudioUrl    { get; init; }
    public          string?                   VideoUrl    { get; init; }
    public          IEnumerable<ResourceDto>? Resources   { get; init; }
}
