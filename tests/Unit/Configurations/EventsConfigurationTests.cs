using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Nostrfi.Database.Persistence.Configurations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.Conventions;

namespace Nostrfi.Database.Persistence.Unit.Tests.Configurations;

public class EventsConfigurationTests
{
    private readonly EventsConfiguration _entityTypeConfiguration;
    private readonly ModelBuilder _modelBuilder;

    public EventsConfigurationTests()
    {
        var builder = new DbContextOptionsBuilder<NostrfiContext>();
        builder.UseInMemoryDatabase("Test");

        using var context = new NostrfiContext(builder.Options);
        _modelBuilder = new ModelBuilder(NpgsqlConventionSetBuilder.Build());
        _entityTypeConfiguration = new EventsConfiguration();
    }

    [Fact]
    [Description("Should create a Events Entity")]
    public void ShouldBuildLocationEntity()
    {
        // Arrange
        _entityTypeConfiguration.Configure(_modelBuilder.Entity<Events>());

        // Act
        var model = _modelBuilder.Model;
        var entityType = model.FindEntityType(typeof(Events));

        //Assert
        entityType.ShouldNotBeNull();
    }

    [Fact]
    [Description("Should have a UserRoles Relation")]
    public void ShouldHaveUserLocationsRelationDefined()
    {
        // Arrange
        _entityTypeConfiguration.Configure(_modelBuilder.Entity<Events>());
        // Act
        var model = _modelBuilder.Model;
        var entityType = model.FindEntityType(typeof(Events));
        // Assert
        entityType?.FindNavigation(nameof(Events.Tags)).ShouldNotBeNull();
    }
}