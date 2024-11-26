using System.ComponentModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nostrfi.Persistence.Exceptions;

namespace Nostrfi.Persistence.Unit.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void AddNostrDatabase_Throws_Exception_Empty_ConnectionString()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:nostrfi", "" }
            })
            .Build();

        // Act
        Should.Throw<NostrDbException>(() => services.AddNostrDatabase(configuration));
    }

    [Fact]
    public void AddNostrDatabase_Throws_Exception_No_Valid_ConnectionString()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:nostrfi", "nostrfi" }
            })
            .Build();

        // Act
        Should.Throw<NostrDbException>(() => services.AddNostrDatabase(configuration));
    }
    
    [Fact]
    [Description("The connection string is a valid format but cannot validate because cannot conenct")]
    public void AddNostrDatabase_Throws_Exception_No_Validate_ConnectionString()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:nostrfi", "User ID=root;Password=myPassword;Host=localhost;Port=5432;Database=myDataBase;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;" }
            })
            .Build();

        // Act
        Should.Throw<NostrDbException>(() => services.AddNostrDatabase(configuration));
    }
    
    
}