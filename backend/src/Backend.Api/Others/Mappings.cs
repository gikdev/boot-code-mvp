using Backend.App.Others;
using Backend.Contracts.Others;

namespace Backend.Api.Others;

internal static class Mappings {
    internal static AdminSessionResponse CreateAdminSessionResponse(bool isAdmin) {
        return new AdminSessionResponse {
            IsAdmin = isAdmin,
        };
    }

    internal static CreateAdminSessionCommand MapToCommand(this CreateAdminSessionRequest request) {
        return new CreateAdminSessionCommand {
            Passcode = request.Passcode,
        };
    }
}
