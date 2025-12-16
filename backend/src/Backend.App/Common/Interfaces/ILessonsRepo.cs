using Backend.Domain.Lessons;

namespace Backend.App.Common.Interfaces;

public interface ILessonsRepo {
    Task AddAsync(Lesson lesson);
    Task SaveChangesAsync();
}
