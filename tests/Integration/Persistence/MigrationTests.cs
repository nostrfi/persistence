using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Nostrfi.Relay.Persistence.Integration.Tests.Collections;
using Nostrfi.Relay.Persistence.Integration.Tests.Fixtures;

namespace Nostrfi.Relay.Persistence.Integration.Tests.Persistence;

[Collection(nameof(PostgreCollection))]
public class MigrationTests(PostgreSqlContainerFixture fixture) : BasePersistenceTests(fixture)
{
    [Fact]
    [Description("Should have at least 1 migration applied")]
    public async Task ShouldMigrate()
    {
        var migrations = await Context.Database.GetAppliedMigrationsAsync();
        migrations.ShouldNotBeNull();
        migrations.ShouldNotBeEmpty();
        migrations.Count().ShouldBeGreaterThanOrEqualTo(1);
    }

    [Fact]
    public void AllBaseTablesShouldExist()
    {
        Context.Set<Entities.Events>().ShouldNotBeNull();
    }
}