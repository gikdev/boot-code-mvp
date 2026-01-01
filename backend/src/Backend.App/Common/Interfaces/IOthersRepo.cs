namespace Backend.App.Common.Interfaces;

public interface IOthersRepo {
    public Task<string> GetPasscodeAsync();
}
