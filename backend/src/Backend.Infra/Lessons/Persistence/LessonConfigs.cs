using Backend.Domain.Lessons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infra.Lessons.Persistence;

public class LessonConfigs : IEntityTypeConfiguration<Lesson> {
    public void Configure(EntityTypeBuilder<Lesson> builder) {
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Id)
            .ValueGeneratedNever();
    }
}
