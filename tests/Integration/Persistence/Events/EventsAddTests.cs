using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Nostrfi.Persistence.Entities;
using Nostrfi.Persistence.Integration.Tests.Collections;
using Nostrfi.Persistence.Integration.Tests.Fixtures;

namespace Nostrfi.Persistence.Integration.Tests.Persistence.Events;

[Collection(nameof(PostgreCollection))]
public class EventsAddTests(PostgreSqlContainerFixture fixture) : BasePersistenceTests(fixture)
{
    [Fact]
    public async Task ShouldSaveAnEvent()
    {


        var dbEvent = NostrEvents;
        
        Context.Set<Entities.Events>().Add(dbEvent);
        await Context.SaveChangesAsync(default);

        var savedEvent = await Context.Set<Entities.Events>().FirstOrDefaultAsync(e => e.Id.Equals(dbEvent.Id));
        Assert.NotNull(savedEvent);


    }


    private static Entities.Events NostrEvents => Builder<Entities.Events>.CreateNew()
        .With(x => x.Id = "4376c65d2f232afbe9b882a35baa4f6fe8667c4e684749af565f981833ed6a65")
        .With(x => x.PubKey = "6e468422dfb74a5738702a8823b9b28168abab8655faacb6853cd0ee15deee93")
        .With(x => x.CreatedAt = DateTimeOffset.UtcNow)
        .With(x => x.KindId = 1)
        .With(x => x.Content =
            "Walled gardens became prisons, and nostr is the first step towards tearing down the prison walls.")
        .With(x => x.Sig =
            "908a15e46fb4d8675bab026fc230a0e3542bfade63da02d542fb78b2a8513fcd0092619a2c8c1221e581946e0191f2af505dfdf8657a414dbca329186f009262")
        .With( x => x.Tags = Tags)

        .Build();

    private static List<Tags> Tags =>
    [
        new Tags
            {
                Identifier = "e",
                Data = ["3da979448d9ba263864c4d6f14984c423a3838364ec255f03c7904b1ae77f206"]
            }
      

    ];
   

}

