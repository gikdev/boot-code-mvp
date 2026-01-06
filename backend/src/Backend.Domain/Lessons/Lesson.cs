using Backend.Domain.Common;
using ErrorOr;
using Optional;
using Optional.Unsafe;

namespace Backend.Domain.Lessons;

#pragma warning disable CS8618 // For EF Core!

public class Lesson : AggregateRoot {
    private Lesson() { }

    private Lesson(
        string          title,
        int             position,
        string?         textContent = null,
        string?         audioUrl    = null,
        string?         videoUrl    = null,
        List<Resource>? resources   = null,
        Guid?           id          = null
    ) : base(id ?? Guid.NewGuid()) {
        Title       = title;
        Position    = position;
        TextContent = textContent;
        AudioUrl    = audioUrl;
        VideoUrl    = videoUrl;
        Resources   = resources ?? [];
    }

    public string Title    { get; private set; }
    public int    Position { get; private set; }

    public string?        TextContent { get; private set; }
    public string?        AudioUrl    { get; private set; }
    public string?        VideoUrl    { get; private set; }
    public List<Resource> Resources   { get; private set; }

    // Flexible Update method with three-state logic
    public ErrorOr<Success> Update(
        Option<string>?          title       = null,
        Option<int>?             position    = null,
        Option<string?>?         textContent = null,
        Option<string?>?         audioUrl    = null,
        Option<string?>?         videoUrl    = null,
        Option<List<Resource>?>? resources   = null
    ) {
        if (title.HasValue)
            if (string.IsNullOrWhiteSpace(title.Value.ValueOrDefault()))
                return LessonErrors.TitleEmpty;

        if (title.HasValue) Title       = title.Value.ValueOr(() => Title);
        if (position.HasValue) Position = position.Value.ValueOr(() => Position);

        if (textContent.HasValue) TextContent = textContent.Value.Match(v => v, () => null);
        if (audioUrl.HasValue) AudioUrl       = audioUrl.Value.Match(v => v, () => null);
        if (videoUrl.HasValue) VideoUrl       = videoUrl.Value.Match(v => v, () => null);

        if (resources.HasValue)
            Resources = resources.Value.Match(
                v => v ?? [],
                () => Resources
            );

        return Result.Success;
    }

    public static ErrorOr<Lesson> Create(
        string          title,
        int             position,
        string?         textContent = null,
        string?         audioUrl    = null,
        string?         videoUrl    = null,
        List<Resource>? resources   = null,
        Guid?           id          = null
    ) {
        if (string.IsNullOrWhiteSpace(title))
            return LessonErrors.TitleEmpty;

        return new Lesson(title, position, textContent, audioUrl, videoUrl, resources, id);
    }
}
