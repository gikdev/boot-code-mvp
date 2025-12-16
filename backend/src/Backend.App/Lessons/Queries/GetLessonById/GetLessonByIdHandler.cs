using Backend.App.Common.Interfaces;
using Backend.Domain.Lessons;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Backend.App.Lessons.Queries.GetLessonById;

public class GetLessonByIdHandler(
    ILessonsRepo lessonsRepo,
    ILogger<GetLessonByIdHandler> logger
) : IRequestHandler<GetLessonByIdQuery, Lesson?> {
    public async Task<Lesson?> Handle(
        GetLessonByIdQuery request,
        CancellationToken cancellationToken
    ) {
        logger.LogDebug("Getting lesson by id {LessonId}", request.Id);

        var lesson = await lessonsRepo.GetOneByIdAsync(request.Id);

        if (lesson is null) {
            logger.LogInformation("Lesson #{LessonId} was not found!", request.Id);
            return null;
        }

        return lesson;
    }
}
