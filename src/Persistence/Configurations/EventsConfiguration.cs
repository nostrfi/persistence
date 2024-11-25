using Nostrfi.Relay.Persistence.Entities;
using Threenine.Database.Extensions;

namespace Nostrfi.Relay.Persistence.Configurations;

public class EventsConfiguration : IEntityTypeConfiguration<Events>
{
    public void Configure(EntityTypeBuilder<Events> builder)
    {
        builder.ToTable(nameof(Events).ToSnakeCase());
        
        builder.HasKey(e => e.Id)
            .HasName(nameof(Events.Id).ToSnakeCase());

        builder.HasIndex(x => new { x.Id, x.PublicKey }).IsUnique();
        
        builder.Property(x => x.Id)
            .HasColumnName(nameof(Events.Id).ToSnakeCase())
            .HasColumnType(ColumnTypes.Varchar)
            .HasMaxLength(85)
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .HasColumnName(nameof(Events.CreatedAt).ToSnakeCase())
            .HasColumnType(ColumnTypes.DateTimeOffSet)
            .IsRequired();
      
        builder.OwnsMany(ne => ne.Tags, b => { b.ToJson();}); 
    
    }
}