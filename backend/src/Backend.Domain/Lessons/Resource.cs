using Ardalis.GuardClauses;
using Backend.Domain.Common;

namespace Backend.Domain.Lessons;

public class Resource : ValueObject {
    public string Title { get; }
    public string Url { get; }

    public override IEnumerable<object?> GetEqualityComponents() {
        yield return Title;
        yield return Url;
    }

    public Resource(string title, string url) {
        Title = Guard.Against.NullOrWhiteSpace(title);
        Url = Guard.Against.NullOrWhiteSpace(url);
    }

#pragma warning disable CS8618 // For EF Core
    private Resource() { }
#pragma warning restore CS8618
}
