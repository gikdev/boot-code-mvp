using Backend.Domain.Lessons;
using Backend.Infra.Common.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infra.Lessons.Persistence;

internal class LessonConfigs : IEntityTypeConfiguration<Lesson> {
    public void Configure(EntityTypeBuilder<Lesson> builder) {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(PersistenceConstants.MaxLengthMedium);

        builder.Property(x => x.Position)
            .IsRequired();

        builder.Property(x => x.TextContent);

        builder.Property(x => x.AudioUrl)
            .HasMaxLength(PersistenceConstants.MaxLengthMedium);

        builder.Property(x => x.VideoUrl)
            .HasMaxLength(PersistenceConstants.MaxLengthMedium);

        builder.OwnsMany(x => x.Resources, ConfigResource);
    }

    private void ConfigResource(OwnedNavigationBuilder<Lesson, Resource> r) {
        r.WithOwner().HasForeignKey("LessonId");
        r.Property<Guid>("Id");
        r.HasKey("Id");

        r.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(PersistenceConstants.MaxLengthMedium);

        r.Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(PersistenceConstants.MaxLengthMedium);

        r.ToTable("LessonResources");
    }
}
