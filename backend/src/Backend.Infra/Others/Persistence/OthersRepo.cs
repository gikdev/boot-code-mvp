using Backend.App.Common.Interfaces;

using Microsoft.Extensions.Configuration;

namespace Backend.Infra.Others.Persistence;

public class OthersRepo(
    IConfiguration config
) : IOthersRepo {
    public async Task<string> GetPasscodeAsync() {
        var passcode = config.GetValue<string>("PASSCODE");

        if (string.IsNullOrWhiteSpace(passcode))
            throw new Exception("PASSCODE env var ain't set or is empty.");

        return passcode;
    }
}
