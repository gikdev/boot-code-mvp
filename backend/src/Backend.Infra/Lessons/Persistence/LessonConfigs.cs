using Backend.Domain.Lessons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infra.Lessons.Persistence;

internal class LessonConfigs : IEntityTypeConfiguration<Lesson> {
    public void Configure(EntityTypeBuilder<Lesson> builder) {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Title);
        builder.Property(x => x.Position);
        builder.Property(x => x.Content);
    }
}
