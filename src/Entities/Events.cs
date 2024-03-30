using Nostrfi.Database.Persistence.Entities.Nostr;

namespace Nostrfi.Database.Persistence.Entities;


public class Events 
{
    public Guid Identifier { get; set; }
    public DateTimeOffset Received { get;  set; } = DateTimeOffset.UtcNow;
    
    public Event Event { get; set; }
    
}