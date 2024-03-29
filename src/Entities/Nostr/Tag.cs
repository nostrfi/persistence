namespace Nostrfi.Database.Persistence.Entities.Nostr;

public abstract class Tag
{
    public string Id { get; set; } = string.Empty;
    public string EventId { get; set; } = string.Empty;
    public Events Event { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}