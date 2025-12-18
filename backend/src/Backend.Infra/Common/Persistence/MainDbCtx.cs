using System.Reflection;
using Backend.Domain.Lessons;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Common.Persistence;

internal class MainDbCtx(DbContextOptions options) : DbContext(options) {
    internal DbSet<Lesson> Lessons => Set<Lesson>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
