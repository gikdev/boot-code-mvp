namespace Backend.Contracts.Others;

public record CreateAdminSessionRequest {
    public required string Passcode { get; init; }
}
