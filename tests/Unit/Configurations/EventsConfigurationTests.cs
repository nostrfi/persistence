using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Nostrfi.Database.Persistence.Configurations;
using Nostrfi.Database.Persistence.Entities;
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
    [Description("Should have a Tags Relation")]
    public void ShouldHaveTagsRelationDefined()
    {
        // Arrange
        _entityTypeConfiguration.Configure(_modelBuilder.Entity<Events>());
        // Act
        var model = _modelBuilder.Model;
        var entityType = model.FindEntityType(typeof(Events));
        // Assert
        entityType.ShouldNotBeNull();
        entityType.FindNavigation(nameof(Events.Tags)).ShouldNotBeNull();
        entityType.FindNavigation(nameof(Events.Tags)).ForeignKey.IsRequired.ShouldBeTrue();
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

        entityType.FindProperty(nameof(Events.Id)).ShouldNotBeNull();
        entityType.FindProperty(nameof(Events.CreatedAt)).ShouldNotBeNull();
        entityType.FindProperty(nameof(Events.PublicKey)).ShouldNotBeNull();
        entityType.FindProperty(nameof(Events.Signature)).ShouldNotBeNull();
        entityType.FindProperty(nameof(Events.Kind)).ShouldNotBeNull();
        
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

      
        var id = entityType.FindProperty(nameof(Events.Id))!.ClrType;
       
        id.ShouldBe(typeof(string));
       
        var createdAt = entityType.FindProperty(nameof(Events.CreatedAt))!.ClrType;
       
        createdAt.ShouldBe(typeof(DateTimeOffset));
       
        var signature = entityType.FindProperty(nameof(Events.Signature))!.ClrType;
       
        signature.ShouldBe(typeof(string));
       
        var content = entityType.FindProperty(nameof(Events.Content))!.ClrType;
       
        content.ShouldBe(typeof(string));
        
        var kind = entityType.FindProperty(nameof(Events.Kind))!.ClrType;
       
        kind.ShouldBe(typeof(int));





    }
}