namespace Backend.Contracts.Lessons;

public record ResourceDto {
    public required string Title { get; init; }
    public required string Url   { get; init; }
}
