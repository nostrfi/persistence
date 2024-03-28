using System.Reflection;

namespace Nostrfi.Database.Persistence;

public class NostrfiContext : DbContext
{
    public NostrfiContext(DbContextOptions<NostrfiContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchema.Name);
        modelBuilder.HasPostgresExtension(PostgreExtensions.UUIDGenerator);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
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