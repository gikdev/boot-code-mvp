using Backend.App.Common.Interfaces;
using Backend.Domain.Lessons;
using Backend.Infra.Common.Persistence;

using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Lessons.Persistence;

internal class LessonsRepo(MainDbCtx db) : ILessonsRepo {
    public async Task AddAsync(Lesson lesson) {
        await db.AddAsync(lesson);
    }

    public async Task<Lesson?> GetOneByIdAsync(Guid id) {
        return await db.Lessons.FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<List<Lesson>> ListAsync() {
        return await db.Lessons.ToListAsync();
    }

    public async Task<List<Lesson>> ListByIdsAsync(List<Guid> ids) {
        return await db.Lessons.Where(l => ids.Contains(l.Id)).ToListAsync();
    }

    public Task RemoveAsync(Lesson lesson) {
        db.Remove(lesson);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync() {
        await db.SaveChangesAsync();
    }

    public Task UpdateAsync(Lesson lesson) {
        db.Lessons.Update(lesson);
        return Task.CompletedTask;
    }

    public async Task UpdateListAsync(List<Lesson> lessons) {
        foreach (var lesson in lessons)
            await UpdateAsync(lesson);
    }
}
