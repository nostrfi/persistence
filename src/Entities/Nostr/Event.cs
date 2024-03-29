namespace Nostrfi.Database.Persistence.Entities.Nostr;

public abstract class Event
{
  
  public string Id { get; set; } = string.Empty;
  public string PublicKey { get; set; } = string.Empty;
  public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
  public int Kind { get; set; }
  public string Content { get; set; } = string.Empty;
  public string Signature { get; set; } = string.Empty;
  public List<Tags> Tags { get; set; } = [];
 
}