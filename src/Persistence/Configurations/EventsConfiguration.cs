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

        builder.HasIndex(x => new { x.Id, PublicKey = x.PubKey }).IsUnique();
        
        builder.Property(x => x.Id)
            .HasColumnName(nameof(Events.Id).ToSnakeCase())
            .HasColumnType(ColumnTypes.Varchar)
            .HasMaxLength(65)
            .IsRequired();
        
        builder.Property(x => x.PubKey)
            .HasColumnName(nameof(Events.PubKey).ToLower())
            .HasColumnType(ColumnTypes.Varchar)
            .HasMaxLength(65)
            .IsRequired();
        
        builder.Property(x => x.Kind)
            .HasColumnName(nameof(Events.Kind).ToLower())
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
        
        builder.Property(x => x.Sig)
            .HasColumnName(nameof(Events.Sig).ToSnakeCase())
            .HasColumnType(ColumnTypes.Varchar)
            .HasMaxLength(128)
            .IsRequired();
      
        builder.Property(e => e.Tags)
            .HasColumnName(nameof(Events.Tags).ToSnakeCase())
            .HasColumnType(ColumnTypes.JsonB);
    
    }
}