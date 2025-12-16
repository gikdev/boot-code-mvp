using Backend.App.Common.Interfaces;
using Backend.Domain.Lessons;
using FluentValidation;
using MediatR;

namespace Backend.App.Lessons.Queries;

public record GetLessonByIdQuery(
    Guid Id
) : IRequest<Lesson?>;

public class GetLessonByIdValidator : AbstractValidator<GetLessonByIdQuery> {
    public GetLessonByIdValidator() {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

public class GetLessonByIdHandler(
    ILessonsRepo lessonsRepo
) : IRequestHandler<GetLessonByIdQuery, Lesson?> {
    public async Task<Lesson?> Handle(
        GetLessonByIdQuery request,
        CancellationToken cancellationToken
    ) {
        var lesson = await lessonsRepo.GetOneById(request.Id);
        return lesson;
    }
}
