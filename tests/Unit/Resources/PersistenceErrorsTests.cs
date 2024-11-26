using System.ComponentModel;
using System.Reflection;
using System.Resources;

namespace Nostrfi.Persistence.Unit.Tests.Resources;

public class PersistenceErrorsTests
{
    private const string PersistenceErrorsResourceFileName = "Nostrfi.Persistence.PersistenceErrors.resources";
    private const string AssemblyName = "Nostrfi.Persistence";

    [Fact]
    [Description("Resource file should exist")]
    public void Should_exist_resource_file()
    {
        var assembly = Assembly.Load(AssemblyName);

        var resourceNames = assembly.GetManifestResourceNames();
        resourceNames.ShouldContain(PersistenceErrorsResourceFileName);
    }

    [Theory]
    [Description("Should Contain keys")]
    [InlineData("NoConnectionStringDefined")]
    [InlineData("ConnectionStringsInvalid")]
    public void ShouldContainKey(string key)
    {
        var assembly = Assembly.Load(AssemblyName);
        var resourceManager = new ResourceManager("Nostrfi.Persistence.PersistenceErrors", assembly);
        var value = resourceManager.GetString(key);
        value.ShouldNotBeNull();
        value.ShouldNotBeEmpty();
    }  
}