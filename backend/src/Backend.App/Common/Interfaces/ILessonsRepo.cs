using Backend.Domain.Lessons;

namespace Backend.App.Common.Interfaces;

public interface ILessonsRepo {
    Task<List<Lesson>> ListAsync();
    Task<Lesson?> GetOneByIdAsync(Guid id);
    Task AddAsync(Lesson lesson);
    Task UpdateAsync(Lesson lesson);
    Task RemoveAsync(Lesson lesson);
    Task SaveChangesAsync();
}
