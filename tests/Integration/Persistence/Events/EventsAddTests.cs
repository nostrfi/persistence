using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Nostrfi.Relay.Persistence.Entities.Nostr;
using Nostrfi.Relay.Persistence.Integration.Tests.Collections;
using Nostrfi.Relay.Persistence.Integration.Tests.Fixtures;

namespace Nostrfi.Relay.Persistence.Integration.Tests.Persistence.Events;

[Collection(nameof(PostgreCollection))]
public class EventsAddTests(PostgreSqlContainerFixture fixture) : BasePersistenceTests(fixture)
{
    [Fact]
    public void ShouldSaveAnEvent()
    {
        var dbEvent = new Entities.Events
        {
            Event = NostrEvent
        };
        Context.Set<Entities.Events>().Add(dbEvent);
        Context.SaveChanges();

        var savedEvent = Context.Set<Entities.Events>().FirstOrDefault(e => e.Identifier.Equals(dbEvent.Identifier));
        savedEvent.ShouldSatisfyAllConditions(
            x => x.ShouldNotBeNull(),
            x => x.Event.ShouldNotBeNull(),
            x => x.Event.Tags.ShouldNotBeNull(),
            x => x.Identifier.ShouldBeOfType<Guid>()
            );
    }
    [Fact]
    public async Task ShouldSaveAnEventAsync()
    {
        var dbEvent = new Entities.Events
        {
            Event = NostrEvent
        };
       await Context.Set<Entities.Events>().AddAsync(dbEvent);
        await Context.SaveChangesAsync();

        var savedEvent = await Context.Set<Entities.Events>().FirstOrDefaultAsync(e => e.Identifier.Equals(dbEvent.Identifier));
        
        savedEvent.ShouldSatisfyAllConditions(
            x => x.ShouldNotBeNull(),
            x => x.Event.ShouldNotBeNull(),
            x => x.Event.Tags.ShouldNotBeNull(),
            x => x.Identifier.ShouldBeOfType<Guid>()
        );
    }

    private static Event NostrEvent => Builder<Event>.CreateNew()
        .With(x => x.Id = "4376c65d2f232afbe9b882a35baa4f6fe8667c4e684749af565f981833ed6a65")
        .With(x => x.PublicKey = "6e468422dfb74a5738702a8823b9b28168abab8655faacb6853cd0ee15deee93")
        .With(x => x.CreatedAt = DateTimeOffset.UtcNow)
        .With(x => x.Kind = 1)
        .With(x => x.Content =
            "Walled gardens became prisons, and nostr is the first step towards tearing down the prison walls.")
        .With(x => x.Sig =
            "908a15e46fb4d8675bab026fc230a0e3542bfade63da02d542fb78b2a8513fcd0092619a2c8c1221e581946e0191f2af505dfdf8657a414dbca329186f009262")
        .With( x => x.Tags = Tags)

        .Build();

    private static List<string[]> Tags =>
    [
        ["e", "3da979448d9ba263864c4d6f14984c423a3838364ec255f03c7904b1ae77f206"],

    ];
  

}




/*Builder<Tag>.CreateListOfSize(1)
.TheFirst(1)
    .With(t => t.Name = "e")
.With(t => t.Value = "3da979448d9ba263864c4d6f14984c423a3838364ec255f03c7904b1ae77f206")
.Build().ToList()*/
