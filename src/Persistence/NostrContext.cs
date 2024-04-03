using System.Reflection;
using Nostrfi.Relay.Persistence.Entities;

namespace Nostrfi.Relay.Persistence;

public class NostrContext : DbContext
{
    public NostrContext(DbContextOptions<NostrContext> options) : base(options)
    {
    }

    public DbSet<Events> Events { get; set; } 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.Name);
        modelBuilder.HasPostgresExtension(PostgreExtensions.UUIDGenerator);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
     
        base.OnModelCreating(modelBuilder);
    }

    private void TimeStamp()
    {
        var events = ChangeTracker.Entries()
            .Where(e => e is { Entity: Entities.Events, State: EntityState.Added or EntityState.Modified })
            .Select(x => x.Entity).Cast<Events>().ToList();
        
        events.ForEach(e =>
        {
            if (e.Received == default) e.Received = DateTimeOffset.UtcNow;
        });
    }
    
    public override int SaveChanges()
    {
        
       TimeStamp();
        return base.SaveChanges();
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        TimeStamp();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        TimeStamp();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    public async Task MigrateAsync()
    {
        await Database.MigrateAsync();
    }

    public void Migrate()
    {
        Database.Migrate();
    }
}