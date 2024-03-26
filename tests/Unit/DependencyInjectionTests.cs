using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Nostrfi.Database.Persistence.Exceptions;
using Threenine.Database.Extensions;

namespace Nostrfi.Database.Persistence.Unit.Tests;

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
        Should.Throw<NostrDbException>(() =>  services.AddNostrDatabase(configuration) ) ;
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
         Should.Throw<NostrDbException>(() =>  services.AddNostrDatabase(configuration) ) ;
    }


    
   
}