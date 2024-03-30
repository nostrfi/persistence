using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Nostrfi.Database.Persistence.Entities;
using Nostrfi.Database.Persistence.Integration.Tests.Collections;
using Nostrfi.Database.Persistence.Integration.Tests.Fixtures;
using Nostrfi.Relay.Persistence;

namespace Nostrfi.Database.Persistence.Integration.Tests;

[Collection(nameof(PostgreCollection))]
public class MigrationTests(PostgreSqlContainerFixture fixture) : IAsyncLifetime
{
    private NostrContext _context = null!;


    public async Task InitializeAsync()
    {
        await fixture.InitializeAsync();
        var options = new DbContextOptionsBuilder<NostrContext>()
            .UseNpgsql(fixture.ConnectionString)
            .Options;

        _context = new NostrContext(options);
    }

    public Task DisposeAsync()
    {
        _context.Dispose();
        return Task.CompletedTask;
    }

    [Fact]
    [Description("Should have at least 1 migration applied")]
    public async Task ShouldMigrate()
    {
        var migrations = await _context.Database.GetAppliedMigrationsAsync();
        migrations.ShouldNotBeNull();
        migrations.ShouldNotBeEmpty();
        migrations.Count().ShouldBeGreaterThanOrEqualTo(1);
    }

    [Fact]
    public void AllBaseTablesShouldExist()
    {
        _context.Set<Events>().ShouldNotBeNull();
     
    }
}