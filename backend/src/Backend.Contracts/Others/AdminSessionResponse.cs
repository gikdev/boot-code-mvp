namespace Backend.Contracts.Others;

public record AdminSessionResponse {
    public required bool IsAdmin { get; init; }
}
