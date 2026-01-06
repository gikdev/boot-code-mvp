using Backend.App.Common.Interfaces;
using MediatR;

namespace Backend.App.Others;

public record CreateAdminSessionCommand : IRequest<bool> {
    public required string Passcode { get; init; }
}

public class CreateAdminSessionCommandHandler(
    IOthersRepo othersRepo
) : IRequestHandler<CreateAdminSessionCommand, bool> {
    public async Task<bool> Handle(
        CreateAdminSessionCommand request,
        CancellationToken         cancellationToken
    ) {
        var correctPasscode = await othersRepo.GetPasscodeAsync();
        var isAdmin         = request.Passcode == correctPasscode;
        return isAdmin;
    }
}
