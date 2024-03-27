using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Nostrfi.Database.Persistence.Configurations;
using Nostrfi.Database.Persistence.Entities;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.Conventions;

namespace Nostrfi.Database.Persistence.Unit.Tests.Configurations;

public class TagsConfigurationTests
{
    private readonly TagsConfiguration _entityTypeConfiguration;
    private readonly ModelBuilder _modelBuilder;

    public TagsConfigurationTests()
    {
        var builder = new DbContextOptionsBuilder<NostrfiContext>();
        builder.UseInMemoryDatabase("Test");

        using var context = new NostrfiContext(builder.Options);
        _modelBuilder = new ModelBuilder(NpgsqlConventionSetBuilder.Build());
        _entityTypeConfiguration = new TagsConfiguration();
    }

    [Fact]
    [Description("Should create a Tags Entity")]
    public void ShouldBuildLocationEntity()
    {
        // Arrange
        _entityTypeConfiguration.Configure(_modelBuilder.Entity<Tags>());

        // Act
        var model = _modelBuilder.Model;
        var entityType = model.FindEntityType(typeof(Tags));

        //Assert
        entityType.ShouldNotBeNull();
    }

    [Fact]
    [Description("Should have a Tags Relation")]
    public void ShouldHaveUserLocationsRelationDefined()
    {
        // Arrange
        _entityTypeConfiguration.Configure(_modelBuilder.Entity<Tags>());
        // Act
        var model = _modelBuilder.Model;
        var entityType = model.FindEntityType(typeof(Events));
        // Assert
        entityType?.FindNavigation(nameof(Events.Tags)).ShouldNotBeNull();
    }
}