using Backend.Domain.Lessons;
using MediatR;

namespace Backend.App.Lessons.Queries.ListLessons;

public record ListLessonsQuery : IRequest<List<Lesson>>;
