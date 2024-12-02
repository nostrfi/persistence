using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Nostrfi.Persistence.Entities;
using Nostrfi.Persistence.Integration.Tests.Collections;
using Nostrfi.Persistence.Integration.Tests.Fixtures;

namespace Nostrfi.Persistence.Integration.Tests;

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

        migrations.ShouldSatisfyAllConditions(
            x => x.ShouldNotBeNull(),
            x => x.ShouldNotBeEmpty(),
            x => x.Count().ShouldBeGreaterThanOrEqualTo(1)
        );
    }

    [Fact]
    [Description("Verify the core tables exist")]
    public void AllBaseTablesShouldExist()
    {
        _context.Set<Events>().ShouldNotBeNull();
        _context.Set<Kinds>().ShouldNotBeNull();
        _context.Set<Tags>().ShouldNotBeNull();
    }

    [Fact]
    [Description("The Kinds table should exist and have 100 records")]
    public void KindsShouldExist()
    {
        // The first migration we run will have the initial set of 100 Kinds identified
        // This could change as we continue to evolve this project.
        _context.Set<Kinds>().ShouldSatisfyAllConditions(
            x => x.ShouldNotBeNull(),
            x => x.Count().Should().Be(100)
        );
    }
}