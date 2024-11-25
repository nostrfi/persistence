using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Nostrfi.Relay.Persistence;
using Nostrfi.Relay.Persistence.Configurations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.Conventions;

namespace Nostrfi.Database.Persistence.Unit.Tests.Configurations;

public class EventsConfigurationTests
{
    private readonly EventsConfiguration _entityTypeConfiguration;
    private readonly ModelBuilder _modelBuilder;

    public EventsConfigurationTests()
    {
        var builder = new DbContextOptionsBuilder<NostrContext>();
        builder.UseInMemoryDatabase("Test");

        using var context = new NostrContext(builder.Options);
        _modelBuilder = new ModelBuilder(NpgsqlConventionSetBuilder.Build());
        _entityTypeConfiguration = new EventsConfiguration();
    }

    [Fact]
    [Description("Should create a Events Entity")]
    public void ShouldBuildEventsEntity()
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
    [Description("Should have a Tags fields Relation")]
    public void ShouldHaveTagsFieldsDefined()
    {
        // Arrange
        _entityTypeConfiguration.Configure(_modelBuilder.Entity<Events>());
        // Act
        var model = _modelBuilder.Model;
        var entityType = model.FindEntityType(typeof(Events));
        // Assert
        entityType.ShouldNotBeNull();

        entityType.FindProperty(nameof(Events.Identifier)).ShouldNotBeNull();
        entityType.FindProperty(nameof(Events.Received)).ShouldNotBeNull();
      
      
        
    }
    
    [Fact]
    [Description("Should have a Tags Relation")]
    public void ShouldHaveTagsFieldsCorrectTypesDefined()
    {
        // Arrange
        _entityTypeConfiguration.Configure(_modelBuilder.Entity<Events>());
        // Act
        var model = _modelBuilder.Model;
        var entityType = model.FindEntityType(typeof(Events));
        // Assert
        entityType.ShouldNotBeNull();

        var createdAt = entityType.FindProperty(nameof(Events.Received))!.ClrType;
       
        createdAt.ShouldBe(typeof(DateTimeOffset));
       
        var signature = entityType.FindProperty(nameof(Events.Identifier))!.ClrType;
       
        signature.ShouldBe(typeof(Guid));
       
       
    }
}