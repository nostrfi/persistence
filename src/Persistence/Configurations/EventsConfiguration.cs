﻿using Nostrfi.Relay.Persistence.Entities;
using Nostrfi.Relay.Persistence.Entities.Nostr;
using Threenine.Database.Extensions;

namespace Nostrfi.Relay.Persistence.Configurations;

public class EventsConfiguration : IEntityTypeConfiguration<Events>
{
    public void Configure(EntityTypeBuilder<Events> builder)
    {
        builder.ToTable(nameof(Events).ToSnakeCase());
        
        builder.HasKey(e => e.Identifier)
            .HasName(nameof(Events.Identifier).ToSnakeCase());

        builder.HasIndex(x => new { x.Identifier, x.Received }).IsUnique();
        
        builder.Property(x => x.Identifier)
            .HasColumnName(nameof(Events.Identifier).ToSnakeCase())
            .HasColumnType(ColumnTypes.UniqueIdentifier)
            .HasDefaultValueSql(PostgreExtensions.UUIDAlgorithm)
            .IsRequired();
        
        builder.Property(x => x.Received)
            .HasColumnName(nameof(Events.Received).ToSnakeCase())
            .HasColumnType(ColumnTypes.DateTimeOffSet)
            .IsRequired();
      
        builder.OwnsOne(ne => ne.Event, b =>
        {
           b.ToJson(nameof(Events.Event).ToSnakeCase())
                .OwnsOne(t => t.Tags, nt => { nt.ToJson(nameof(Events.Event.Tags).ToSnakeCase());});
        });
    }
}