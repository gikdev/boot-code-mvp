using Backend.App.Common.Interfaces;
using Backend.Domain.Lessons;
using MediatR;

namespace Backend.App.Lessons.Queries;

public record ListLessonsQuery : IRequest<List<Lesson>>;

public class ListLessonsHandler(
    ILessonsRepo lessonsRepo
) : IRequestHandler<ListLessonsQuery, List<Lesson>> {
    public async Task<List<Lesson>> Handle(ListLessonsQuery request, CancellationToken cancellationToken) {
        var lessonList = await lessonsRepo.ListAsync();
        return lessonList;
    }
}
