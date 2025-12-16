using Backend.Domain.Lessons;

namespace Backend.App.Common.Interfaces;

public interface ILessonsRepo {
    Task<List<Lesson>> ListAsync();
    Task AddAsync(Lesson lesson);
    Task SaveChangesAsync();
}
