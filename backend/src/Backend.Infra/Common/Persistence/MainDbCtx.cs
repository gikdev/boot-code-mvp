using System.Reflection;
using Backend.Domain.Lessons;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Common.Persistence;

public class MainDbCtx(DbContextOptions options) : DbContext(options) {
    public DbSet<Lesson> Lessons => Set<Lesson>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
