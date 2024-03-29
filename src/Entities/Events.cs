using Nostrfi.Database.Persistence.Entities.Nostr;

namespace Nostrfi.Database.Persistence.Entities;


public class Events : Event
{
    public Guid Identifier { get; set; }
    public DateTimeOffset Received { get; set; }
}