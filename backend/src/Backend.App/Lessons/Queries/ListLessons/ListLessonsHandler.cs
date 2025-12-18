using Backend.App.Common.Interfaces;
using Backend.Domain.Lessons;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Backend.App.Lessons.Queries.ListLessons;

internal class ListLessonsHandler(
    ILessonsRepo lessonsRepo,
    ILogger<ListLessonsHandler> logger
) : IRequestHandler<ListLessonsQuery, List<Lesson>> {
    public async Task<List<Lesson>> Handle(ListLessonsQuery request, CancellationToken cancellationToken) {
        logger.LogDebug("Getting lessons list.");

        var lessonList = await lessonsRepo.ListAsync();

        return lessonList;
    }
}
