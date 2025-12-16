using Backend.Domain.Lessons;
using MediatR;

namespace Backend.App.Lessons.Queries.GetLessonById;

public record GetLessonByIdQuery(
    Guid Id
) : IRequest<Lesson?>;
