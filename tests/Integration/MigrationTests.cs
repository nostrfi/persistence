using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Nostrfi.Database.Persistence.Integration.Tests.Collections;
using Nostrfi.Database.Persistence.Integration.Tests.Fixtures;

namespace Nostrfi.Database.Persistence.Integration.Tests;

[Collection(nameof(PostgreCollection))]
public class MigrationTests(PostgreSqlContainerFixture fixture): IAsyncLifetime
{
    private NostrfiContext _context = null!;

    [Fact, Description("Should have at least 1 migration applied")]
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
        _context.Set<Tags>().ShouldNotBeNull();
    }


    public async Task InitializeAsync()
    {
        await fixture.InitializeAsync();
        var options = new DbContextOptionsBuilder<NostrfiContext>()
            .UseNpgsql(fixture.ConnectionString)
            .Options;

        _context = new NostrfiContext(options);
    }

    public Task DisposeAsync()
    {
        _context.Dispose();
        return Task.CompletedTask;
    }
}