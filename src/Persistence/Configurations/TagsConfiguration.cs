using Nostrfi.Persistence.Entities;
using Threenine.Database.Extensions;

namespace Nostrfi.Persistence.Configurations;

public class TagsConfiguration : IEntityTypeConfiguration<Tags>
{
    public void Configure(EntityTypeBuilder<Tags> builder)
    {
        builder.ToTable(nameof(Tags).ToSnakeCase());

        builder.HasKey(x => new { x.EventId, x.Identifier });
        builder.Property(x => x.EventId)
            .HasColumnName(nameof(Tags.EventId).ToSnakeCase())
            .HasColumnType(ColumnTypes.Varchar)
            .HasMaxLength(65)
            .IsRequired();
        
        builder.Property(x => x.Identifier)
            .HasColumnName(nameof(Tags.Identifier).ToSnakeCase())
            .HasColumnType(ColumnTypes.Varchar)
            .HasMaxLength(35)
            .IsRequired();
        
        builder.Property(t => t.Data)
            .HasColumnType("text[]");
        
        builder.HasOne(x => x.Event)
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.EventId);

    }
}