using Backend.Domain.Lessons;

namespace Backend.App.Common.Interfaces;

public interface ILessonsRepo {
    Task<List<Lesson>> ListAsync();
    Task<Lesson?> GetOneById(Guid id);
    Task AddAsync(Lesson lesson);
    Task RemoveAsync(Lesson lesson);
    Task SaveChangesAsync();
}
