using Nostrfi.Persistence.Entities;
using Threenine.Database.Extensions;

namespace Nostrfi.Persistence.Configurations;

public class KindConfiguration : IEntityTypeConfiguration<Kinds>
{
    public void Configure(EntityTypeBuilder<Kinds> builder)
    {
        builder.ToTable(nameof(Kinds).ToSnakeCase());

        builder.HasKey(x => new { x.Id });
        builder.HasIndex(x => x.Name).IsUnique();
        
        builder.Property(x => x.Id)
            .HasColumnName(nameof(Kinds.Id).ToSnakeCase())
            .HasColumnType(ColumnTypes.Integer)
            .IsRequired();
        
        builder.Property(x => x.Name)
            .HasColumnName(nameof(Kinds.Name).ToSnakeCase())
            .HasColumnType(ColumnTypes.Varchar)
            .HasMaxLength(35)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnName(nameof(Kinds.Description).ToSnakeCase())
            .HasColumnType(ColumnTypes.Text);
    }
}