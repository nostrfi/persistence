using System.Reflection;
using Nostrfi.Persistence.Entities;

namespace Nostrfi.Persistence;

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

   
    public async Task MigrateAsync()
    {
        await Database.MigrateAsync();
    }

    public void Migrate()
    {
        Database.Migrate();
    }
}