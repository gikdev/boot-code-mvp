using Backend.Domain.Lessons;
using ErrorOr;
using MediatR;

namespace Backend.App.Lessons.Commands.UpdateLessonContentById;

public record UpdateLessonContentByIdCommand : IRequest<ErrorOr<Success>> {
    public required Guid            Id          { get; init; }
    public required string?         TextContent { get; init; }
    public required string?         AudioUrl    { get; init; }
    public required string?         ImageUrl    { get; init; }
    public required string?         VideoUrl    { get; init; }
    public required List<Resource>? Resources   { get; init; }
}
