using System.ComponentModel;
using Nostrfi.Persistence.Integration.Tests.Collections;
using Nostrfi.Persistence.Integration.Tests.Fixtures;

namespace Nostrfi.Persistence.Integration.Tests.Persistence.Events;

[Collection(nameof(PostgreCollection))]
public class AddMultipleEvents(PostgreSqlContainerFixture fixture) : BasePersistenceTests(fixture)
{
    [Fact, Description("Add Multiple Events (40)")]
    public async Task ShouldSaveAnEvent()
    {
        var dbEvents =  new EventsFaker().Generate(40);;
        
        Context.Set<Entities.Events>().AddRange(dbEvents);
        await Context.SaveChangesAsync(default);

       Context.Set<Entities.Events>().ShouldSatisfyAllConditions(
           x => x.Count().Should().Be(40)
           );
    }
}