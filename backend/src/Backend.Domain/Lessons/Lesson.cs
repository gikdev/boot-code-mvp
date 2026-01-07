using Backend.Domain.Common;
using ErrorOr;
using Optional;
using Optional.Unsafe;

namespace Backend.Domain.Lessons;

#pragma warning disable CS8618 // For EF Core!

public class Lesson : AggregateRoot {
    private const int DefaultPosition = 1;

    private Lesson() { }

    private Lesson(
        string          title,
        int             position,
        string?         textContent = null,
        string?         audioUrl    = null,
        string?         imageUrl    = null,
        string?         videoUrl    = null,
        List<Resource>? resources   = null,
        Guid?           id          = null
    ) : base(id ?? Guid.NewGuid()) {
        Title       = title;
        Position    = position;
        TextContent = textContent;
        AudioUrl    = audioUrl;
        ImageUrl    = imageUrl;
        VideoUrl    = videoUrl;
        Resources   = resources ?? [];
    }

    public string         Title       { get; private set; }
    public int            Position    { get; private set; }
    public string?        TextContent { get; private set; }
    public string?        AudioUrl    { get; private set; }
    public string?        ImageUrl    { get; private set; }
    public string?        VideoUrl    { get; private set; }
    public List<Resource> Resources   { get; private set; }

    public ErrorOr<Success> Update(
        Option<string>?          title       = null,
        Option<int>?             position    = null,
        Option<string?>?         textContent = null,
        Option<string?>?         audioUrl    = null,
        Option<string?>?         imageUrl    = null,
        Option<string?>?         videoUrl    = null,
        Option<List<Resource>?>? resources   = null
    ) {
        if (title.HasValue)
            if (string.IsNullOrWhiteSpace(title.Value.ValueOrDefault()))
                return LessonErrors.TitleEmpty;

        title?.MatchSome(v => Title             = v);
        position?.MatchSome(v => Position       = v);
        textContent?.MatchSome(v => TextContent = v);
        audioUrl?.MatchSome(v => AudioUrl       = v);
        imageUrl?.MatchSome(v => ImageUrl       = v);
        videoUrl?.MatchSome(v => VideoUrl       = v);
        resources?.MatchSome(v => Resources     = v ?? []);

        return Result.Success;
    }

    public static ErrorOr<Lesson> Create(
        string          title,
        int?            position    = null,
        string?         textContent = null,
        string?         audioUrl    = null,
        string?         imageUrl    = null,
        string?         videoUrl    = null,
        List<Resource>? resources   = null,
        Guid?           id          = null
    ) {
        if (string.IsNullOrWhiteSpace(title))
            return LessonErrors.TitleEmpty;

        return new Lesson(
            id: id,
            resources: resources ?? [],
            videoUrl: videoUrl,
            imageUrl: imageUrl,
            audioUrl: audioUrl,
            textContent: textContent,
            position: position ?? DefaultPosition,
            title: title
        );
    }
}
