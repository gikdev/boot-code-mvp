using Backend.Domain.Lessons;

namespace Backend.App.Common.Interfaces;

public interface ILessonsRepo {
    Task<List<Lesson>> ListAsync();
    Task<List<Lesson>> ListByIdsAsync(List<Guid> ids);
    Task<Lesson?> GetOneByIdAsync(Guid id);
    Task AddAsync(Lesson lesson);
    Task UpdateAsync(Lesson lesson);
    Task UpdateListAsync(List<Lesson> lessons);
    Task RemoveAsync(Lesson lesson);
    Task SaveChangesAsync();
}
