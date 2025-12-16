using Backend.App.Common.Interfaces;
using Backend.Domain.Lessons;
using Backend.Infra.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Lessons.Persistence;

public class LessonsRepo(MainDbCtx db) : ILessonsRepo {
    public async Task AddAsync(Lesson lesson) {
        await db.AddAsync(lesson);
    }

    public async Task<List<Lesson>> ListAsync() {
        return await db.Lessons.ToListAsync();
    }

    public async Task SaveChangesAsync() {
        await db.SaveChangesAsync();
    }
}
