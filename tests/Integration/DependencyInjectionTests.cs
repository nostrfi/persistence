using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nostrfi.Relay.Persistence.Exceptions;
using Nostrfi.Relay.Persistence.Integration.Tests.Collections;
using Nostrfi.Relay.Persistence.Integration.Tests.Fixtures;

namespace Nostrfi.Relay.Persistence.Integration.Tests;


[Collection(nameof(PostgreCollection))]
public class DependencyInjectionTests(PostgreSqlContainerFixture fixture) : IAsyncLifetime
{
    private string ConnectionString = String.Empty;


    public async Task InitializeAsync()
    {
        await fixture.InitializeAsync();
        ConnectionString = fixture.ConnectionString;

    }

    
    public async Task DisposeAsync()
    {
        await fixture.DisposeAsync();
    }
    [Fact]
    public void AddNostrDatabase_Throws_Exception_No_Valid_ConnectionString()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:Nostr", ConnectionString }
            })
            .Build();

        
        // Act
        services.AddNostrDatabase(configuration);
       
    }
}