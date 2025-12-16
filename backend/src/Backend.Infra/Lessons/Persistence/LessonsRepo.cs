using Backend.App.Common.Interfaces;
using Backend.Domain.Lessons;
using Backend.Infra.Common.Persistence;

namespace Backend.Infra.Lessons.Persistence;

public class LessonsRepo(MainDbCtx db) : ILessonsRepo {
    public async Task AddAsync(Lesson lesson) {
        await db.AddAsync(lesson);
    }

    public async Task SaveChangesAsync() {
        await db.SaveChangesAsync();
    }
}
