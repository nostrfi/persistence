using System.Reflection;
using Nostrfi.Database.Persistence.Entities;

namespace Nostrfi.Database.Persistence;

public class NostrfiContext : DbContext
{
    public NostrfiContext(DbContextOptions<NostrfiContext> options) : base(options)
    {
    }
    
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
            .Where(e => e is { Entity: Events, State: EntityState.Added or EntityState.Modified })
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