using Microsoft.EntityFrameworkCore;
using Nostrfi.Database.Persistence.Integration.Tests.Collections;
using Nostrfi.Database.Persistence.Integration.Tests.Fixtures;

namespace Nostrfi.Database.Persistence.Integration.Tests.Persistence;

[Collection(nameof(PostgreCollection))]
public abstract class BasePersistenceTests(PostgreSqlContainerFixture fixture): IAsyncLifetime
{
    protected NostrfiContext Context { get; set; } = null!;
    
    
    public async Task InitializeAsync()
    {
        await fixture.InitializeAsync();
        var options = new DbContextOptionsBuilder<NostrfiContext>()
            .UseNpgsql(fixture.ConnectionString)
            .Options;

        Context = new NostrfiContext(options);
    }

    public async Task DisposeAsync()
    {
        await Context.DisposeAsync();
    }
}