using FizzWare.NBuilder;
using Nostrfi.Database.Persistence.Entities;
using Nostrfi.Database.Persistence.Integration.Tests.Collections;
using Nostrfi.Database.Persistence.Integration.Tests.Fixtures;

namespace Nostrfi.Database.Persistence.Integration.Tests.Persistence.Events;

[Collection(nameof(PostgreCollection))]
public class EventsAddTests(PostgreSqlContainerFixture fixture): BasePersistenceTests(fixture)
{
    [Fact]
    public void ShouldSaveAnEvent()
    {

        var addEvent = SampleEvent;
        Context.Set<Entities.Events>().Add(addEvent);
        Context.SaveChanges();
        
        var savedEvent = Context.Set<Entities.Events>().FirstOrDefault(e => e.Id == addEvent.Id);
        Assert.NotNull(savedEvent);
        Assert.Equal(addEvent.Id, savedEvent.Id);
        Assert.Equal(addEvent.PublicKey, savedEvent.PublicKey);
        Assert.Equal(addEvent.CreatedAt, savedEvent.CreatedAt);
        Assert.Equal(addEvent.Kind, savedEvent.Kind);
        Assert.Equal(addEvent.Content, savedEvent.Content);
        Assert.Equal(addEvent.Signature, savedEvent.Signature);
        Assert.Equal(addEvent.Tags.Count, savedEvent.Tags.Count);
        
    }


    private static Entities.Events SampleEvent => Builder<Entities.Events>.CreateNew()
            .With(x => x.Id ="4376c65d2f232afbe9b882a35baa4f6fe8667c4e684749af565f981833ed6a65")
            .With(x => x.PublicKey = "6e468422dfb74a5738702a8823b9b28168abab8655faacb6853cd0ee15deee93")
            .With(x => x.CreatedAt = DateTimeOffset.UtcNow)
            .With(x => x.Kind = 1)
            .With(x => x.Content = "Walled gardens became prisons, and nostr is the first step towards tearing down the prison walls.")
            .With(x => x.Signature = "908a15e46fb4d8675bab026fc230a0e3542bfade63da02d542fb78b2a8513fcd0092619a2c8c1221e581946e0191f2af505dfdf8657a414dbca329186f009262")
            .With(x => x.Tags = Builder<Tags>.CreateListOfSize(1)
                .TheFirst(1)
                .With(t => t.Tag = "e")
                .With(t => t.Value = "3da979448d9ba263864c4d6f14984c423a3838364ec255f03c7904b1ae77f206")
                .Build().ToList()
            )
            .Build();
    }
