using Nostrfi.Persistence.Entities;
using Threenine.Database.Extensions;

namespace Nostrfi.Persistence.Configurations;

public class EventsConfiguration : IEntityTypeConfiguration<Events>
{
    public void Configure(EntityTypeBuilder<Events> builder)
    {
        builder.ToTable(nameof(Events).ToSnakeCase());

        builder.HasKey(e => e.Id)
            .HasName(nameof(Events.Id).ToSnakeCase());

        builder.HasIndex(x => new { x.Id, PublicKey = x.PublicKey }).IsUnique();

        builder.Property(x => x.Id)
            .HasColumnName(nameof(Events.Id).ToSnakeCase())
            .HasColumnType(ColumnTypes.Varchar)
            .HasMaxLength(65)
            .IsRequired();

        builder.Property(x => x.PublicKey)
            .HasColumnName(nameof(Events.PublicKey).ToLower())
            .HasColumnType(ColumnTypes.Varchar)
            .HasMaxLength(65)
            .IsRequired();

        builder.Property(x => x.KindId)
            .HasColumnName(nameof(Events.KindId).ToSnakeCase())
            .HasColumnType(ColumnTypes.Integer)
            .IsRequired();

        builder.Property(x => x.Content)
            .HasColumnName(nameof(Events.Content).ToLower())
            .HasColumnType(ColumnTypes.Text)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName(nameof(Events.CreatedAt).ToSnakeCase())
            .HasColumnType(ColumnTypes.DateTimeOffSet)
            .IsRequired();

        builder.Property(x => x.Signature)
            .HasColumnName(nameof(Events.Signature).ToSnakeCase())
            .HasColumnType(ColumnTypes.Varchar)
            .HasMaxLength(128)
            .IsRequired();

        builder.HasOne(x => x.Kind)
            .WithMany(x => x.Events)
            .HasForeignKey(x => x.KindId);
    }
}