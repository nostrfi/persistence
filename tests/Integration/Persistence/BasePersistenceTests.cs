using Microsoft.EntityFrameworkCore;
using Nostrfi.Persistence.Integration.Tests.Collections;
using Nostrfi.Persistence.Integration.Tests.Fixtures;

namespace Nostrfi.Persistence.Integration.Tests.Persistence;

[Collection(nameof(PostgreCollection))]
public abstract class BasePersistenceTests(PostgreSqlContainerFixture fixture) : IAsyncLifetime
{
    protected NostrContext Context { get; set; } = null!;


    public async Task InitializeAsync()
    {
        await fixture.InitializeAsync();
        var options = new DbContextOptionsBuilder<NostrContext>()
            .UseNpgsql(fixture.ConnectionString)
            .Options;

        Context = new NostrContext(options);
    }

    public async Task DisposeAsync()
    {
        await Context.DisposeAsync();
    }
}