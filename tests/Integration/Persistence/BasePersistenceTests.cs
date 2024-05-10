using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nostrfi.Relay.Persistence.Integration.Tests.Collections;
using Nostrfi.Relay.Persistence.Integration.Tests.Fixtures;

namespace Nostrfi.Relay.Persistence.Integration.Tests.Persistence;

[Collection(nameof(PostgreCollection))]
public abstract class BasePersistenceTests(PostgreSqlContainerFixture fixture): IAsyncLifetime
{
    protected NostrContext Context { get; set; } = null!;
    
    
    public async Task InitializeAsync()
    {
        await fixture.InitializeAsync();
        var builder = WebApplication
            .CreateBuilder();
        builder.Configuration.AddInMemoryCollection(ConnectionStringConfiguration).Build();
        builder.Services.AddNostrDatabase(builder.Configuration);

        var app = builder.Build();

        await app.UseNostrDatabaseAsync();
        var serviceProvider = builder.Services.BuildServiceProvider();
        Context =  serviceProvider.GetRequiredService<NostrContext>();
    }

    public async Task DisposeAsync()
    {
        await Context.DisposeAsync();
    }
    
    private Dictionary<string, string> ConnectionStringConfiguration => new()
    {
        { "ConnectionStrings:Nostr", $"{fixture.ConnectionString}" }
    };
}