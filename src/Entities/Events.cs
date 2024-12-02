namespace Nostrfi.Persistence.Entities;

public sealed class Events
{
    public string Id { get; set; } = string.Empty;
    public string PubKey { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public int KindId { get; set; }
    public Kinds Kind { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Sig { get; set; } = string.Empty;
    public ICollection<Tags> Tags { get; set; } = [];
}